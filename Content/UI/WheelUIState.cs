using ItemWheel.Content.Common.Configs;
using ItemWheel.Content.UI.Element;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemWheel.Content.UI
{
    internal class WheelUIState : UIState
    {
        private WheelUI _wheel;

        public WheelUIState()
        {
            ClientConfigs clientConfigs = ModContent.GetInstance<ClientConfigs>();

            switch(clientConfigs.Wheels)
            {
                case 4:
                    _wheel = new FourWheel(clientConfigs);
                    break;
                case 6:
                    _wheel = new SixWheel(clientConfigs);
                    break;
                case 8:
                    _wheel = new EightWheel(clientConfigs);
                    break;
                default:
                    _wheel = new FourWheel(clientConfigs);
                    break;
            }
            
            Append(_wheel);
        }
    }
}
