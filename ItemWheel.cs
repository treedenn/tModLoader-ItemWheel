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
        private WheelUIState UIUIState;
        private UserInterface _uiUIState;

        public override void Load()
        {
            base.Load();

            if (!Main.dedServ)
            {
                this.UIUIState = new WheelUIState();
                this.UIUIState.Activate();

                this._uiUIState = new UserInterface();
                this._uiUIState.SetState(UIUIState);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            this._uiUIState.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);

            var hotbarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Hotbar"));
            if (hotbarIndex != -1)
            {
                layers.Insert(hotbarIndex, new LegacyGameInterfaceLayer(
                    "ItemWheel: Wheel",
                    delegate
                    {
                        _uiUIState.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}