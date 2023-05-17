namespace The_Final_Battle;

public class VinFletcher : Character
{
    private new readonly string Name = "Vin Fletcher";
    public int Hp { get; set; } = 15;
    public int MaxHp { get; set; } = 15;
    public List<Attack> AttackList { get; set; } = new List<Attack>() {Attack.Punch, Attack.Shoot};
    public Gear EquippedItem { get; set; } = Gear.VinsBow;
    public Controller ControlledBy = Controller.Computer;

    public VinFletcher() : base("Vin Fletcher", 15, new List<Attack>() {Attack.Punch, Attack.Shoot}, Controller.Computer)
    {
        
    }
}