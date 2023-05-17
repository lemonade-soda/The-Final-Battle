using System.ComponentModel;

namespace The_Final_Battle;

public class Game
{
    private static readonly List<Character.Attack> SkeletonAttacks = new List<Character.Attack>() { Character.Attack.BoneCrunch };
    private static readonly List<Character.Attack> TrueProgrammerAttacks = new List<Character.Attack>() { Character.Attack.Punch };
    private static readonly List<Character.Attack> UncodedOneAttacks = new List<Character.Attack>() { Character.Attack.Unravel };
    private static readonly List<Character.Attack> VinFletcherAttacks = new List<Character.Attack>() { Character.Attack.Punch,Character.Attack.Shoot };

    private static readonly List<Character.Attack> StoneAmarokAttacks = new List<Character.Attack>()
        { Character.Attack.Bite };

    private static Character _TheUncodedOne = new Character("The Uncoded One", 15, UncodedOneAttacks,
        Character.Controller.Computer);
    
    private static Character _skeleton = new Character("Skeleton", 5, SkeletonAttacks,
        Character.Controller.Computer, Character.Gear.Dagger);
    
    private Character _vinFletcher = new VinFletcher();
    
    private Party Heroes;
    
    private Party FirstVillains = new Party(_skeleton);
    private Party SecondVillains = new Party(new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
                                             new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
                                             Character.Gear.Dagger, Character.Gear.Dagger);
    private Party ThirdVillains =
        new Party(new Character("Stone Amarok", 4, StoneAmarokAttacks, Character.Controller.Computer,
                Character.DefensiveModifier.StoneArmor),
                  new Character("Stone Amarok", 4, StoneAmarokAttacks, Character.Controller.Computer,
                      Character.DefensiveModifier.StoneArmor));

    private static List<Character> listOfVillains = new List<Character>()
    {
        new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
        new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
        new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
        new Character("Skeleton", 5, SkeletonAttacks, Character.Controller.Computer),
        new Character("Stone Amarok", 4, StoneAmarokAttacks, Character.Controller.Computer,
            Character.DefensiveModifier.StoneArmor),
        new Character("Stone Amarok", 4, StoneAmarokAttacks, Character.Controller.Computer,
            Character.DefensiveModifier.StoneArmor)
    };

    private Party FourthVillains = new Party(listOfVillains);
    private Party TheUncodedOne = new Party(_TheUncodedOne);
        
    private Random random = new Random();
    private string _trueName;
    
    

    public void Start()
    {
        Combat(Heroes, FirstVillains);
        Combat(Heroes, SecondVillains);
        Combat(Heroes, ThirdVillains);
        Combat(Heroes, FourthVillains);
        Combat(Heroes, TheUncodedOne);
    }
    public void InitializeHeroParty(string trueName, Character.Controller controller = Character.Controller.Player1)
    {
        if (controller == Character.Controller.Player1)
        {
            Heroes = new Party(new Character(trueName, 25, TrueProgrammerAttacks,Character.Controller.Player1, Character.DefensiveModifier.ObjectSight),
                _vinFletcher);
            _trueName = trueName;
        }
        else
        {
            Heroes = new Party(new Character(trueName, 25, TrueProgrammerAttacks,Character.Controller.Computer),
                _vinFletcher);
            _trueName = trueName;
        }
        
    }

    private void AnnounceUnitTurn(string unitName)
    {
        Console.WriteLine($"It is {unitName.ToUpper()}'S turn...");
    }

    private void Combat(Party heroes, Party villains)
    {
        bool fighting = true;
        while (fighting == true)
        {
            /*foreach (var hero in heroes.partyMembers)
            {
                if (hero.ControlledBy == Unit.Controller.Player1)
                {
                    AnnounceUnitTurn(hero.Name);
                    
                    PromptAction(hero, heroes, villains);
                }
                else if (hero.ControlledBy == Unit.Controller.Computer)
                {
                    AnnounceUnitTurn(hero.Name);
                    
                    SelectAttack(hero, villains);
                    Thread.Sleep(500);
                }
                
            }*/
            
            if (heroes.IsPartyDead())
            {
                Console.WriteLine("The heroes have lost, the Uncoded One's forces have prevailed.");
                fighting = false;
                break;
            }
            
            for (var i = 0; i < heroes.PartyMembers.Count; i ++)
            {
                if (villains.IsPartyDead())
                {
                    CleanupVillains(heroes, villains);
                    fighting = false;
                    break;
                    /*Console.WriteLine("Press any key to continue...");
                    ConsoleKeyInfo keyPressed;
                    while (Console.KeyAvailable == false)
                    {
                            
                    }*/
                }
                
                if (heroes.PartyMembers[i].ControlledBy == Character.Controller.Player1)
                {
                    Console.Clear();
                    AnnounceUnitTurn(heroes.PartyMembers[i].Name);
                    DrawGameStatus(heroes, villains, i,false);
                    PromptAction(heroes.PartyMembers[i], heroes, villains);
                    
                    
                }
                
                else if (heroes.PartyMembers[i].ControlledBy == Character.Controller.Computer)
                {
                    
                    if (villains.IsPartyDead())
                    {
                        CleanupVillains(heroes, villains);
                        fighting = false;
                        break;
                        /*Console.WriteLine("Press any key to continue...");
                        ConsoleKeyInfo keyPressed;
                        while (Console.KeyAvailable == false)
                        {
                                
                        }*/
                    }
                    Console.Clear();
                    AnnounceUnitTurn(heroes.PartyMembers[i].Name);
                    DrawGameStatus(heroes, villains, i, false);
                    ConsiderItem(heroes.PartyMembers[i], heroes);
                    ConsiderGear(heroes.PartyMembers[i], heroes);
                    SelectAttack(heroes.PartyMembers[i], villains);
                    
                    Thread.Sleep(500);
                }
                
            }

            /*foreach (var villain in villains.partyMembers)
            {
                if (villain.Hp == 0)
                    continue;
                AnnounceUnitTurn(villain.Name);
                
                SelectAttack(villain, heroes);
                Thread.Sleep(500);
            }*/
            if (villains.PartyMembers.Any())
            {
                for (var i = 0; i < villains.PartyMembers.Count; i++)
                {
                    Console.Clear();
                    if (villains.PartyMembers[i].Hp == 0)
                        continue;
                    AnnounceUnitTurn(villains.PartyMembers[i].Name);
                    DrawGameStatus(heroes, villains, i, true);
                    bool turnTaken = false;
                    while(turnTaken == false)
                    {
                        turnTaken = ConsiderGear(villains.PartyMembers[i], villains);
                        turnTaken = SelectAttack(villains.PartyMembers[i], heroes);
                    }
                
                    Thread.Sleep(500);
                }
            }
            

            
            /*else if (villains.IsPartyDead())
            {
                CleanupVillains(heroes, villains);
                break;
            }*/
        }
    }

    private void CleanupVillains(Party heroes, Party villains)
    {
        Console.WriteLine("The heroes have won the battle!");
        if (villains.Equipment.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("You have looted a ");
            foreach (var gear in villains.Equipment)
            {
                if(gear != Character.Gear.None)
                    Console.Write($"{gear}");
            }
            Console.WriteLine(".");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var gear in villains.Equipment)
            {
                if(gear != Character.Gear.None)
                    heroes.Equipment.Add(gear);
            }
            heroes.Equipment.AddRange(villains.Equipment);
            
            Console.WriteLine("Press any key to continue...");
            ConsoleKeyInfo keyPressed;
            while (Console.KeyAvailable == false)
            {
                        
            }
        }
        
    }

    private void UseAction(Character.Actions action, string unitName)
    {
        if (action == Character.Actions.DoNothing)
        {
            Console.WriteLine($"{unitName.ToUpper()} did NOTHING.");
        }
    }

    private void PromptAction(Character character, Party allies, Party enemies)
    {
        bool takenAction = false;
        while (takenAction == false)
        {
            Console.WriteLine($"{character.Name.ToUpper()}, what do you want to do? Options are: 'attack', 'use item'," +
                              $"'gear', or 'do nothing'");
            var input = Console.ReadLine();
            input = input.ToLower();

            switch (input)
            {
                case "attack":
                    takenAction = SelectAttack(character, enemies, allies);
                    break;
                case "use item":
                    takenAction = SelectItem(character, allies);
                    break;
                case "gear":
                    takenAction = SelectGear(character, allies);
                    break;
                case "do nothing":
                    UseAction(Character.Actions.DoNothing, character.Name);
                    takenAction = true;
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }
            
        }
        
    }

    private bool ConsiderGear(Character character, Party allies)
    {
        if (character.EquippedItem == Character.Gear.None)
        {
            int roll = this.random.Next(100);
            if ((roll + 1) >= 50 && allies.Equipment.Count > 0)
            {
                /*allies.CPUEquipGear(character, allies.Equipment[random.Next(allies.Equipment.Count)]);
                return true;*/
                if(allies.Equipment.Contains(Character.Gear.Sword))
                {
                    allies.CPUEquipGear(character, Character.Gear.Sword);
                }
                else if (allies.Equipment.Contains(Character.Gear.Dagger))
                {
                    allies.CPUEquipGear(character, Character.Gear.Dagger );
                }
                
            }
        }
        return false;
    }

    private bool SelectGear(Character character, Party allies)
    {
        if (!allies.Equipment.Any())
        {
            Console.WriteLine("You have no items!");
            return false;
        }
        while (true)
        {
                
            allies.ListGear();
            return allies.EquipGear(character, allies);
        }
    }

    private bool SelectItem(Character character, Party allies)
    {
            if (!allies.Inventory.Any())
            {
                Console.WriteLine("You have no items!");
                return false;
            }
            while (true)
            {
                
                allies.ListItems();
                return allies.UseItem(character);
            }
    }

    private void ConsiderItem(Character character, Party allies)
    {
        if(character.Hp <= (character.MaxHp - 10) && allies.Inventory.Contains(Party.Item.HealthPotion))
        {
            var roll = random.Next(100);
            if ((roll + 1) >= 66)
            {
                allies.CPUUseItem(character, Party.Item.HealthPotion);
            }
            
        }
    }

    private bool SelectAttack(Character character, Party enemies, Party heroes = null)
    {
        if (character.ControlledBy == Character.Controller.Player1)
        {
            while (true)
            {
                for (var i = 0; i < character.AttackList.Count; i++)
                {
                    string attackName = ReadAttackName(character.AttackList[i]);
                    Console.WriteLine($"Type {i + 1} to {attackName}.");
                }
                Console.WriteLine("Or, type 'back' to change action.");

                var input = Console.ReadLine();
                input = input.ToLower();
        
                switch (input)
                {
                    case "punch" or "1" when character.AttackList.Contains(Character.Attack.Punch):
                        return ChooseTarget(enemies, Character.Attack.Punch, heroes);
                        break;
                    case "slash" or "2" when character.AttackList.Contains(Character.Attack.Slash):
                        return ChooseTarget(enemies, Character.Attack.Slash, heroes);
                    case "stab" or "3" when character.AttackList.Contains(Character.Attack.Stab):
                        return ChooseTarget(enemies, Character.Attack.Stab,heroes);
                    case "back":
                        return false;
                }

                return true;
            }
        }
        
        else if (character.ControlledBy == Character.Controller.Computer)
        {
            int enemySelected = random.Next(enemies.PartyMembers.Count);
            var targetDMod = enemies.PartyMembers[enemySelected]._defensiveModifier;
            
            if (character.Name == "Skeleton" && !character.AttackList.Contains(Character.Attack.Stab))
            {
                
                var rolledDamage = CalculateDamage(Character.Attack.BoneCrunch,targetDMod);
                
                enemies.PartyMembers[enemySelected].Hp -= rolledDamage;
                if (enemies.PartyMembers[enemySelected].Hp <= 0)
                {
                    enemies.PartyMembers[enemySelected].Hp = 0;
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {rolledDamage} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {rolledDamage} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    return true;
                }
                
            }
            else if (character.Name == "Skeleton" && character.AttackList.Contains(Character.Attack.Stab))
            {
                var damageDealt = CalculateDamage(Character.Attack.Stab, targetDMod);
                if (enemies.PartyMembers[enemySelected].Hp <= 0)
                {
                    enemies.PartyMembers[enemySelected].Hp = 0;
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    return true;
                }
            }
            else if (character.Name == "The Uncoded One")
            {
                var rolledDamage = CalculateDamage(Character.Attack.Unravel, targetDMod);
                
                enemies.PartyMembers[enemySelected].Hp -= rolledDamage;
                if (enemies.PartyMembers[enemySelected].Hp <= 0)
                {
                    enemies.PartyMembers[enemySelected].Hp = 0;
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {rolledDamage} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {rolledDamage} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    return true;
                }
                
                
            }
            else if (character.Name == "Vin Fletcher")
            {
                if (character.AttackList.Contains(Character.Attack.Shoot))
                {
                    int roll = random.Next(100);
                    {
                        if (roll + 1 > 50)
                        {
                            var damageDealt = CalculateDamage(Character.Attack.Shoot, targetDMod);
                            enemies.PartyMembers[enemySelected].Hp -= damageDealt;
                            if (enemies.PartyMembers[enemySelected].Hp <= 0)
                            {
                                enemies.PartyMembers[enemySelected].Hp = 0;
                                Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                                  $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                                  $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                                Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name} has been defeated!");
                                enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                                return true;
                            }
                            else
                            {
                                Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                                  $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                                  $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                                return true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Vin Fletcher missed!");
                            return true;
                        }
                    }
                    
                    
                }
                enemies.PartyMembers[enemySelected].Hp -= 1;
                if (enemies.PartyMembers[enemySelected].Hp <= 0)
                {
                    enemies.PartyMembers[enemySelected].Hp = 0;
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken 1 damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name} has been defeated!");
                    enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken 1 damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    return true;
                }
            }
            else if (character.Name == _trueName)
            {
                if (character.AttackList.Contains(Character.Attack.Slash))
                {
                    var damageDealt = CalculateDamage(Character.Attack.Slash, targetDMod);
                    enemies.PartyMembers[enemySelected].Hp -= damageDealt;
                    if (enemies.PartyMembers[enemySelected].Hp <= 0)
                    {
                        enemies.PartyMembers[enemySelected].Hp = 0;
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                          $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                          $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name} has been defeated!");
                        enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                          $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                          $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                        return true;
                    }
                }
                else
                {
                    var damageDealt = CalculateDamage(Character.Attack.Punch, targetDMod);
                    enemies.PartyMembers[enemySelected].Hp -= damageDealt;
                    if (enemies.PartyMembers[enemySelected].Hp <= 0)
                    {
                        enemies.PartyMembers[enemySelected].Hp = 0;
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                          $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                          $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name} has been defeated!");
                        enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                          $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                          $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                        return true;
                    }
                }
                
                
                
            }
            else if (character.Name == "Stone Amarok")
            {
                var damageDealt = CalculateDamage(Character.Attack.Bite, targetDMod);
                enemies.PartyMembers[enemySelected].Hp -= damageDealt;
                if (enemies.PartyMembers[enemySelected].Hp <= 0)
                {
                    enemies.PartyMembers[enemySelected].Hp = 0;
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    enemies.PartyMembers.Remove(enemies.PartyMembers[enemySelected]);
                    return true;
                }
                else
                {
                    Console.WriteLine($"{enemies.PartyMembers[enemySelected].Name.ToUpper()} has taken {damageDealt} damage and is now at " +
                                      $"{enemies.PartyMembers[enemySelected].Hp}/" +
                                      $"{enemies.PartyMembers[enemySelected].MaxHp} HP.");
                    return true;
                }
                
            }
            
        }

        return false;

    }

    private string ReadAttackName(Character.Attack attack)
    {
        if (attack == Character.Attack.Punch)
            return "punch";
        else if (attack == Character.Attack.BoneCrunch)
            return "bone crunch";
        else if (attack == Character.Attack.Slash)
        {
            return "slash";
        }
        else if (attack == Character.Attack.Stab)
        {
            return "stab";
        }
            
        return "yeah something bad happened";
    }

    private int AttackInfo(Character.Attack attack)
    {
        return attack switch
        {
            Character.Attack.Punch => 1,
            Character.Attack.BoneCrunch => random.Next(2),
            Character.Attack.Slash => 2,
            Character.Attack.Stab => 1,
            Character.Attack.Unravel => random.Next(5),
            Character.Attack.Shoot => 3,
            Character.Attack.Bite => 1,
            _ => 0
        };
    }

    private bool ChooseTarget(Party enemies, Character.Attack attack, Party? heroes = null)
    {
        while (true)
        {
            for (var i = 0; i < enemies.PartyMembers.Count; i++)
            {
                Console.WriteLine($"Type {i} to attack {enemies.PartyMembers[i].Name}");
            }

            Console.WriteLine("Or type back to change attack.");
            string input = Console.ReadLine();
            input = input.ToLower();

            if (input == "back")
            {
                return false;
            }
            
            var targetIndex = Convert.ToInt32(input);

            if (targetIndex >= 0 && targetIndex <= enemies.PartyMembers.Count)
            {
                var damageDealt = 0;
                var targetDMod = enemies.PartyMembers[targetIndex]._defensiveModifier;
                switch (attack)
                {
                    case Character.Attack.Punch:
                        damageDealt = CalculateDamage(Character.Attack.Punch, targetDMod);
                        enemies.PartyMembers[targetIndex].Hp -= damageDealt;
                        break;
                    case Character.Attack.Slash:
                        damageDealt = CalculateDamage(Character.Attack.Slash, targetDMod);
                        enemies.PartyMembers[targetIndex].Hp -= damageDealt;
                        break;
                    case Character.Attack.BoneCrunch:
                        damageDealt = CalculateDamage(Character.Attack.BoneCrunch, targetDMod);
                        enemies.PartyMembers[targetIndex].Hp -= damageDealt;
                        break;
                    case Character.Attack.Stab:
                        damageDealt = CalculateDamage(Character.Attack.Stab, targetDMod);
                        enemies.PartyMembers[targetIndex].Hp -= damageDealt;
                        break;
                }

                if (enemies.PartyMembers[targetIndex].Hp <= 0)
                {
                    enemies.PartyMembers[targetIndex].Hp = 0;
                    Console.WriteLine(
                        $"{enemies.PartyMembers[targetIndex].Name} has taken {damageDealt} damage and is now at " +
                        $"{enemies.PartyMembers[targetIndex].Hp}/" +
                        $"{enemies.PartyMembers[targetIndex].MaxHp} HP.");
                    Console.WriteLine($"{enemies.PartyMembers[targetIndex].Name} has been defeated!");
                    heroes?.Equipment.Add(enemies.PartyMembers[targetIndex].EquippedItem);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"You have looted a {enemies.PartyMembers[targetIndex].EquippedItem}.");
                    Console.ForegroundColor = ConsoleColor.White;
                    enemies.PartyMembers.Remove(enemies.PartyMembers[targetIndex]);
                }
                else
                {
                    Console.WriteLine(
                        $"{enemies.PartyMembers[targetIndex].Name} has taken {damageDealt} damage and is now at " +
                        $"{enemies.PartyMembers[targetIndex].Hp}/" +
                        $"{enemies.PartyMembers[targetIndex].MaxHp} HP.");
                }
                /*else if (enemies.PartyMembers[targetIndex]._defensiveModifier ==
                         Character.DefensiveModifier.StoneArmor)
                {
                    switch (attack)
                    {
                        case Character.Attack.Punch:
                            enemies.PartyMembers[targetIndex].Hp -= (AttackInfo(Character.Attack.Punch) - 1);
                            break;
                        case Character.Attack.Slash:
                            enemies.PartyMembers[targetIndex].Hp -= (AttackInfo(Character.Attack.Slash) - 1);
                            break;
                        case Character.Attack.BoneCrunch:
                            enemies.PartyMembers[targetIndex].Hp -= ;
                            break;
                        case Character.Attack.Stab:
                            enemies.PartyMembers[targetIndex].Hp -= (AttackInfo(Character.Attack.Stab) - 1);
                            break;
                    }

                    if (enemies.PartyMembers[targetIndex].Hp <= 0)
                    {
                        enemies.PartyMembers[targetIndex].Hp = 0;
                        Console.WriteLine(
                            $"{enemies.PartyMembers[targetIndex].Name} has taken 1 damage and is now at " +
                            $"{enemies.PartyMembers[targetIndex].Hp}/" +
                            $"{enemies.PartyMembers[targetIndex].MaxHp} HP.");
                        Console.WriteLine($"{enemies.PartyMembers[targetIndex].Name} has been defeated!");
                        heroes?.Equipment.Add(enemies.PartyMembers[targetIndex].EquippedItem);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"You have looted a {enemies.PartyMembers[targetIndex].EquippedItem}.");
                        Console.ForegroundColor = ConsoleColor.White;
                        enemies.PartyMembers.Remove(enemies.PartyMembers[targetIndex]);
                    }
                    else
                    {
                        Console.WriteLine(
                            $"{enemies.PartyMembers[targetIndex].Name} has taken 1 damage and is now at " +
                            $"{enemies.PartyMembers[targetIndex].Hp}/" +
                            $"{enemies.PartyMembers[targetIndex].MaxHp} HP.");
                    }
                }*/
                
            
                return true;
            }
        }
        

        return false;
    }

    private int CalculateDamage(Character.Attack attack, Character.DefensiveModifier dmod)
    {
        switch (dmod)
        {
            case Character.DefensiveModifier.None when attack is Character.Attack.Punch:
                return AttackInfo(Character.Attack.Punch);
            
            case Character.DefensiveModifier.None when attack is Character.Attack.Slash:
                return AttackInfo(Character.Attack.Slash);
            
            case Character.DefensiveModifier.None when attack is Character.Attack.BoneCrunch:
                return AttackInfo(Character.Attack.BoneCrunch);
            
            case Character.DefensiveModifier.None when attack is Character.Attack.Stab:
                return AttackInfo(Character.Attack.Stab);
                
            case Character.DefensiveModifier.None when attack is Character.Attack.Unravel:
                return AttackInfo(Character.Attack.Unravel);
                
            case Character.DefensiveModifier.None when attack is Character.Attack.Bite:
                return AttackInfo(Character.Attack.Bite);
                
            case Character.DefensiveModifier.None when attack is Character.Attack.Shoot:
                return AttackInfo(Character.Attack.Shoot);
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.Punch:
                return (AttackInfo(Character.Attack.Punch) - 1);
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.Slash:
                return (AttackInfo(Character.Attack.Slash) -1);
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.BoneCrunch:
            {
                var roll = AttackInfo(Character.Attack.BoneCrunch);
                if (roll > 0)
                    return roll - 1;
                return roll;
            }
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.Stab:
                return (AttackInfo(Character.Attack.Stab) -1);
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.Bite:
                return (AttackInfo(Character.Attack.Bite) - 1);
                
            case Character.DefensiveModifier.StoneArmor when attack is Character.Attack.Shoot:
                return (AttackInfo(Character.Attack.Shoot) - 1);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Punch:
                return AttackInfo(Character.Attack.Punch);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Slash:
                return AttackInfo(Character.Attack.Slash);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.BoneCrunch:
                return AttackInfo(Character.Attack.BoneCrunch);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Stab:
                return AttackInfo(Character.Attack.Stab);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Bite:
                return AttackInfo(Character.Attack.Bite);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Shoot:
                return AttackInfo(Character.Attack.Shoot);
                
            case Character.DefensiveModifier.ObjectSight when attack is Character.Attack.Unravel:
            {
                var premitigatedDamage = AttackInfo(Character.Attack.Unravel);
                if (premitigatedDamage >= 3)
                {
                    return premitigatedDamage - 2;
                }
                return 0;
            };
            default:
                throw new ArgumentOutOfRangeException(nameof(dmod), dmod, "Calculate damage was passed a" +
                                                                                 "strange defensive mod");
        }
    }

    private void DrawGameStatus(Party heroes, Party villains, int turn, bool villainTurn)
    {
        bool heroTurn = !villainTurn;
        Console.WriteLine("=============================================" +
                          " BATTLE " +
                          "=============================================");
        DrawHeroPartyStatus(heroes, turn, heroTurn);
        Console.WriteLine("---------------------------------------------" +
                          "---VS---" +
                          "---------------------------------------------");
        DrawVillainPartyStatus(villains, turn, villainTurn);
        Console.WriteLine("=============================================" +
                          "========" +
                          "=============================================");
        
    }

    private void DrawHeroPartyStatus(Party party,int turn, bool partyTurn)
    {
        const int offset = -34;
        void DrawPartyHelper(Party party, int turn, int index)
        {
            if (partyTurn == true)
            {
                if (index == turn)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{party.PartyMembers[index].Name,offset}");
                    //Console.WriteLine($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                    DrawHp(party.PartyMembers[index],true);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write($"{party.PartyMembers[index].Name,offset}");
                    //Console.WriteLine($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                    DrawHp(party.PartyMembers[index],true);
                }
            }
            else
            {
                Console.Write($"{party.PartyMembers[index].Name,offset}");
                //Console.WriteLine($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                DrawHp(party.PartyMembers[index],true);
            }
            
        }
        for (var i = 0; i < party.PartyMembers.Count(); i++)
        {
            DrawPartyHelper(party, turn, i);
        }
    }
    
    private void DrawVillainPartyStatus(Party party,int turn, bool partyTurn = false)
    {
        const int offset = 34;
        void DrawPartyHelper(Party party, int turn, int index, bool villainParty)
        {
            if (partyTurn == true)
            {
                if (index == turn)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write($"{" ", 53}");
                    //Console.Write($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                    DrawHp(party.PartyMembers[index],false);
                    Console.WriteLine($"{party.PartyMembers[index].Name,offset}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.Write($"{" ", 53}");
                    //Console.Write($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                    DrawHp(party.PartyMembers[index],false);
                    Console.WriteLine($"{party.PartyMembers[index].Name,offset}");
                }
            }
            else
            {
                Console.Write($"{" ", 53}");
                //Console.Write($"({party.PartyMembers[index].Hp,4}/{party.PartyMembers[index].MaxHp,-4})");
                DrawHp(party.PartyMembers[index],false);
                Console.WriteLine($"{party.PartyMembers[index].Name,offset}");
            }
            
            
        }
        for (var i = 0; i < party.PartyMembers.Count(); i++)
        {
            DrawPartyHelper(party, turn, i, false);
        }
    }

    public void DrawHp(Character character, bool heroTurn)
    {
        if (heroTurn)
        {
            if (character.Hp <= character.MaxHp / 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"(!{character.Hp,3}/{character.MaxHp,-3}!)");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (character.Hp <= character.MaxHp / 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"(!{character.Hp,3}/{character.MaxHp,-3}!)");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine($"({character.Hp,4}/{character.MaxHp,-4})");
            }
        }
        else
        {
            if (character.Hp <= character.MaxHp / 4)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"(!{character.Hp,3}/{character.MaxHp,-3}!)");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (character.Hp <= character.MaxHp / 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"(!{character.Hp,3}/{character.MaxHp,-3}!)");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.Write($"({character.Hp,4}/{character.MaxHp,-4})");
            }
        }
        
        
    }

    public void WantToPlay(string input)
    {
        string PromptForName()
        {
            while(true)
            {
                Console.WriteLine("True programmer, what is your name?");
                var userInput = Console.ReadLine();

                if (!string.IsNullOrEmpty(userInput))
                {
                    return userInput;
                }
                Console.WriteLine("Try again.");
            }
        }
        input = input.ToLower();
        if (input == "yes" || input == "play")
        {
            var trueName = PromptForName();
            InitializeHeroParty(trueName);
        }
        else
        {
            var trueName = PromptForName();
            InitializeHeroParty(trueName, Character.Controller.Computer);
        }
    }
}