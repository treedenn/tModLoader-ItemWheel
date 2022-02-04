using ItemWheel.Content.Common.Configs;
using ItemWheel.Content.UI.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                    _wheel = new FourWheel(clientConfigs.ItemSize);
                    break;
                default:
                    _wheel = new FourWheel(clientConfigs.ItemSize);
                    break;
            }
            
            Append(_wheel);
        }
    }
}
