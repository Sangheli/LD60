public class PlayerInputHuman : IPlayerInput
{
    public void UseAbility()
    {
        ability.Use();
    }

    public AbilityView ability { get; set; }
}