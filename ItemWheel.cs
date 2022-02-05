using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using ItemWheel.Content.UI;

namespace ItemWheel
{
    public class ItemWheel : Mod
    {
        public static ModKeybind ToggleWheelKey;

        public override void Load()
        {
            base.Load();

            ToggleWheelKey = KeybindLoader.RegisterKeybind(this, "Toggle Weapon Wheel", "OemPipe");
        }

        public override void Unload()
        {
            base.Unload();

            ToggleWheelKey = null;
        }
    }

    internal class WeaponWheelSystem : ModSystem
    {
        private WheelUIState _wheelUIState;
        private UserInterface _userInterface;

        public override void Load()
        {
            base.Load();

            if (!Main.dedServ)
            {
                _wheelUIState = new WheelUIState();
                _wheelUIState.Activate();

                _userInterface = new UserInterface();
                _userInterface.SetState(_wheelUIState);
            }
        }

        public override void Unload()
        {
            base.Unload();

            _wheelUIState = null;
            _userInterface = null;
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _userInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            var hotbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (hotbarIndex != -1)
            {
                layers.Insert(hotbarIndex, new LegacyGameInterfaceLayer(
                    "ItemWheel: Wheel",
                    delegate
                    {
                        _userInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}