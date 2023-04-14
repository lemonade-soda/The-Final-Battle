namespace The_Final_Battle;

public class Character
{
    public readonly string Name;
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public List<Attack> AttackList { get; set; }
    public Gear EquippedItem { get; set; } = Gear.None;
    public Controller ControlledBy = Controller.Computer;
    public DefensiveModifier _defensiveModifier { get; set; } = DefensiveModifier.None;

    

    public Character(string name, int maxHp, List<Attack> attackList, Controller controller)
    {
        Name = name;
        Hp = maxHp;
        MaxHp = maxHp;
        AttackList = attackList;
        ControlledBy = controller;
    }
    
    public Character(string name, int maxHp, List<Attack> attackList, Controller controller, DefensiveModifier dmod)
    {
        Name = name;
        Hp = maxHp;
        MaxHp = maxHp;
        AttackList = attackList;
        ControlledBy = controller;
        _defensiveModifier = dmod;
    }
    
    public Character(string name, int maxHp, List<Attack> attackList, Controller controller, Gear gear)
    {
        Name = name;
        Hp = maxHp;
        MaxHp = maxHp;
        AttackList = attackList;
        ControlledBy = controller;
        EquippedItem = gear;

        if (gear == Gear.Dagger)
        {
            attackList.Add(Attack.Stab);
        }
    }
    


    
    public enum Attack
    {
        Punch,
        BoneCrunch,
        Unravel,
        Slash,
        Stab,
        Shoot,
        Bite
    }

    public enum Actions
    {
        Attack,
        UseItem,
        DoNothing
    }

    public enum Gear
    {
        Sword,
        Dagger,
        VinsBow,
        None
    }
    public enum Controller
    {
        Player1,
        Computer
    }

    public enum DefensiveModifier
    {
        StoneArmor,
        ObjectSight,
        None
    }
    
}