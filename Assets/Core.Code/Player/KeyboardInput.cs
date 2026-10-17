using UnityStandardAssets.CrossPlatformInput;

namespace Core.Code.Player
{
    public class KeyboardInput
    {
        public IPlayerInput Input;

        public void Update()
        {
            var isButtonFire = CrossPlatformInputManager.GetButton("Fire1");

            if (isButtonFire)
            {
                Input.UseAbility();
            }
        }
    }
}