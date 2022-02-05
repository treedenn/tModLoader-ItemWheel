using ItemWheel.Content.Common.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemWheel.Content.UI.Element
{
    internal abstract class WheelUI : UIElement
    {
        public static string WheelAssetPath = "ItemWheel/Assets/Textures/UI";

        public Vector2 Anchor { get; set; }
        public Vector2 MouseAnchor { get; set; } // Vector from Anchor

        protected WheelElement[] _wheelElements;
        protected Vector2[] _elementBorders;
        protected Vector2[] _wheelVectors;
        protected Vector2[] _itemVectors;

        private float angle { get => 360f / _wheelElements.Length; }

        private Action funcAnchor;

        protected WheelUI(int wheelSize, int itemSize)
        {
            ClientConfigs clientConfigs = ModContent.GetInstance<ClientConfigs>();

            switch (clientConfigs.WheelPlacement)
            {
                case WheelPlacement.PLAYER:
                    funcAnchor = () => UpdateAnchorAtPlayer();
                    break;
                case WheelPlacement.MOUSE:
                    funcAnchor = () => UpdateAnchorAtMouse();
                    break;
            }

            _wheelElements = new WheelElement[wheelSize];
            _elementBorders = new Vector2[wheelSize];
            _wheelVectors = new Vector2[wheelSize];
            _itemVectors = new Vector2[wheelSize];

            string texturePath = $"{WheelAssetPath}/{wheelSize}wheel/{wheelSize}-";
            for (int i = 0; i < wheelSize; i++)
            {
                var texture = ModContent.Request<Texture2D>(texturePath + i, ReLogic.Content.AssetRequestMode.ImmediateLoad);
                _wheelElements[i] = new WheelElement(texture, itemSize);
                Append(_wheelElements[i]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            UpdateWheelElements();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ItemWheel.ToggleWheelKey.Current) {
                base.Draw(spriteBatch);
            }
        }

        protected void UpdateAnchor()
        {
            funcAnchor();
        }

        protected Vector2 UpdateMouseAnchor()
        {
            MouseAnchor = Main.MouseScreen - Anchor;
            return MouseAnchor;
        }

        // TODO: allow this to be changed depending on configuration (mostly anchor placement)
        // to increase performance - only run code when required
        // f.x when wheel toggle is pressed
        // Solution could be use Action depending the certain configurations, similar to Anchor
        protected virtual void UpdateWheelElements()
        {
            UpdateAnchor();
            UpdateMouseAnchor();

            for (int i = 0; i < _wheelElements.Length; i++)
            {
                _wheelElements[i].Left.Set(Anchor.X + _wheelVectors[i].X, 0f);
                _wheelElements[i].Top.Set(Anchor.Y + _wheelVectors[i].Y, 0f);
                _wheelElements[i].ItemPosition = new Vector2(Anchor.X + _itemVectors[i].X, Anchor.Y + _itemVectors[i].Y);

                _wheelElements[i].Anchor = Anchor;
                _wheelElements[i].MouseAnchor = MouseAnchor;
                _wheelElements[i].SetBorders(_elementBorders[i % _elementBorders.Length], _elementBorders[(i + 1) % _elementBorders.Length]);
            }

            for (int i = 0; i < _elementBorders.Length; i++)
            {
                _elementBorders[i] = Vector2.UnitY.RotatedBy((i * angle - 45f) * Math.PI / 180);
            }
        }

        private void UpdateAnchorAtPlayer()
        {
            Vector2 playerCenter = Main.LocalPlayer.Center.ToScreenPosition();
            Anchor = new((int)playerCenter.X, (int)playerCenter.Y);
        }

        private void UpdateAnchorAtMouse()
        {
            // only update when toggle is pressed
            if (!ItemWheel.ToggleWheelKey.JustPressed) return;

            Vector2 mouse = Main.MouseScreen;
            Anchor = new((int)mouse.X, (int)mouse.Y);
        }
    }
}
