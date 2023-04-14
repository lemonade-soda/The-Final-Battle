using System.Collections;

namespace The_Final_Battle;

public class Party
{
    public List<Character> PartyMembers { get; } = new List<Character>();
    public List<Item> Inventory { get; } = new List<Item>() { Item.HealthPotion };
    public List<Character.Gear> Equipment { get; } = new List<Character.Gear>() { Character.Gear.Sword };

    public bool IsPartyDead()
    {
        for (var i = 0; i < PartyMembers.Count; i++)
        {
            if (PartyMembers[i].Hp > 0)
                return false;
        }

        return true;
    }

    public void ListGear()
    {
        if (Equipment.Contains(Character.Gear.Sword))
        {
            Console.WriteLine("You have a sword.");
        }
    }

    public bool EquipGear(Character character, Party party)
    {
        Console.WriteLine("What gear do you want to equip? Or type 'back' to do something else.");
        string input = Console.ReadLine();
        input = input.ToLower();

        if (input == "sword")
        {
            if (!Equipment.Contains(Character.Gear.Sword))
            {
                Console.WriteLine("You don't have a sword!");
                return false;
            }
            else
            {
                Console.WriteLine("You equip the sword.");
                character.EquippedItem = Character.Gear.Sword;
                character.AttackList.Add(Character.Attack.Slash);
                party.Equipment.Remove(Character.Gear.Sword);
                return true;
            }
            
        }

        if (input == "dagger")
        {
            if (!Equipment.Contains(Character.Gear.Dagger))
            {
                Console.WriteLine("You don't have a dagger!");
                return false;
            }
            else
            {
                Console.WriteLine("You equip the dagger.");
                character.EquippedItem = Character.Gear.Dagger;
                character.AttackList.Add(Character.Attack.Stab);
                party.Equipment.Remove(Character.Gear.Dagger);
                return true;
            }
        }
        

        if (input == "back")
        {
            return false;
        }

        return false;
    }

    public void ListItems()
    {
        if (Inventory.Contains(Item.HealthPotion))
        {
            Console.WriteLine("You have a health potion.");
        }

        if (Inventory.Contains(Item.SimulasSoup))
        {
            Console.WriteLine("You have some of Simula's soup!");
        }
    }

    public void CPUUseItem(Character character, Item item)
    {
        if (item == Item.HealthPotion)
        {
            if (character.Hp <= character.MaxHp - 10)
            {
                character.Hp += 10;
                Inventory.Remove(Item.HealthPotion);
            }
            else
            {
                character.Hp = character.MaxHp;
                Inventory.Remove(Item.HealthPotion);
            }
        }
    }

    public void CPUEquipGear(Character character, Character.Gear gear)
    {
        if (gear == Character.Gear.Dagger)
        {
            character.EquippedItem = gear;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{character.Name} equipped a dagger!");
            Equipment.Remove(gear);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        if (gear == Character.Gear.Sword)
        {
            character.EquippedItem = gear;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{character.Name} equipped a sword!");
            Equipment.Remove(gear);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
    }
    public bool UseItem(Character character)
    {
        Console.WriteLine("What item do you want to use? Or type 'back' to do something else.");
        string input = Console.ReadLine();
        input = input.ToLower();

        if (input == "health potion")
        {
            if (!Inventory.Contains(Item.HealthPotion))
            {
                Console.WriteLine("You don't have a health potion!");
                return false;
            }
            else
            {
                Console.WriteLine("You drink the health potion.");
                if (character.Hp <= character.MaxHp - 10)
                {
                    character.Hp += 10;
                }
                else
                {
                    character.Hp = character.MaxHp;
                }
                return true;
            }
            
        }
        if (input == "simula's soup")
        {
            if (!Inventory.Contains(Item.SimulasSoup))
            {
                Console.WriteLine("You don't have any more!");
                return false;
            }
            else
            {
                Console.WriteLine("You eat the delicious soup!.");
                character.Hp = character.MaxHp;
                return true;
            }
        }

        if (input == "back")
        {
            return false;
        }

        return false;
    }
    
    public Party(List<Character> units)
    {
        PartyMembers = units;
    }
    
    public Party(Character character)
    {
        PartyMembers.Add(character);
    }

    public Party(Character unit1, Character unit2)
    {
        PartyMembers.Add(unit1);
        PartyMembers.Add(unit2);
    }
    
    public Party(Character unit1, Character unit2, Character.Gear gear1, Character.Gear gear2)
    {
        PartyMembers.Add(unit1);
        PartyMembers.Add(unit2);
        
        Equipment.Add(gear1);
        Equipment.Add(gear2);
    }

    public Party()
    {
        PartyMembers = new List<Character>();
    }

    

    public enum Item
    {
        HealthPotion,
        SimulasSoup
    }
    
}