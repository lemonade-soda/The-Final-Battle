namespace The_Final_Battle;

public class VinFletcher : Character
{
    private new readonly string Name = "Vin Fletcher";
    public int Hp { get; set; } = 15;
    public int MaxHp { get; set; } = 15;
    public List<Attack> AttackList { get; set; } = new List<Attack>() {Character.Attack.Shoot};
    public Gear EquippedItem { get; set; } = Gear.None;
    public Controller ControlledBy = Controller.Computer;
    
    public VinFletcher(string name, int maxHp, List<Attack> attackList, Controller controller) : base(name, maxHp, attackList, controller)
    {
        
    }

    public VinFletcher(string name, int maxHp, List<Attack> attackList, Controller controller, Gear gear) : base(name, maxHp, attackList, controller, gear)
    {
        
    }
}