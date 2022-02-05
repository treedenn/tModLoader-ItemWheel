using ItemWheel.Content.Common.Configs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.UI;

namespace ItemWheel.Content.UI.Element
{
    internal abstract class WheelUI : UIElement
    {
        public static string WheelAssetPath = "ItemWheel/Assets/Textures/UI";

        public Vector2 Anchor { get; set; }
        public Vector2 MouseAnchor { get; set; } // screen vector, center (0, 0) is Anchor

        protected WheelElement[] _wheelElements;
        protected Vector2[] _elementBorders;
        protected Vector2[] _wheelVectors;
        protected Vector2[] _itemVectors; // center of item position

        private float angle { get => 360f / _wheelElements.Length; }

        private Action _updateAction;

        protected WheelUI(int wheelSize, ClientConfigs clientConfigs)
        {
            HandleConfiguration(clientConfigs);

            _wheelElements = new WheelElement[wheelSize];
            _elementBorders = new Vector2[wheelSize];
            _wheelVectors = new Vector2[wheelSize];
            _itemVectors = new Vector2[wheelSize];

            for (int i = 0; i < wheelSize; i++)
            {
                _elementBorders[i] = Vector2.UnitY.RotatedBy((i * angle - angle / 2) * Math.PI / 180);
            }

            string texturePath = $"{WheelAssetPath}/{wheelSize}wheel/{wheelSize}-";
            for (int i = 0; i < wheelSize; i++)
            {
                var texture = ModContent.Request<Texture2D>(texturePath + i, ReLogic.Content.AssetRequestMode.ImmediateLoad);

                _wheelElements[i] = new WheelElement(texture);
                _wheelElements[i].SetBorders(_elementBorders[i % _elementBorders.Length], _elementBorders[(i + 1) % _elementBorders.Length]);

                Append(_wheelElements[i]);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _updateAction();
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!ItemWheel.ToggleWheelKey.JustPressed && ItemWheel.ToggleWheelKey.Current)
            {
                base.Draw(spriteBatch);
            }
        }

        private void HandleConfiguration(ClientConfigs clientConfigs)
        {
            WheelElement.ItemScale = clientConfigs.ItemScale;
            WheelElement.Deadzone = clientConfigs.Deadzone;

            switch (clientConfigs.WheelPlacement)
            {
                case WheelPlacement.PLAYER:
                    _updateAction = () =>
                    {
                        UpdateAnchorAtPlayer();
                        UpdateWheelAnchor();
                        UpdateWheelPosition();
                        UpdateMouseAnchor();
                    };
                    break;
                case WheelPlacement.MOUSE:
                    _updateAction = () =>
                    {
                        if (ItemWheel.ToggleWheelKey.JustPressed)
                        {
                            UpdateAnchorAtMouse();
                            UpdateWheelAnchor();
                            UpdateWheelPosition();
                        }
                        
                        UpdateMouseAnchor();
                    };
                    break;
            }
        }

        private void UpdateAnchorAtPlayer()
        {
            Vector2 playerCenter = Main.LocalPlayer.Center.ToScreenPosition();
            Anchor = new((int)playerCenter.X, (int)playerCenter.Y);
        }

        private void UpdateAnchorAtMouse()
        {
            Vector2 mouse = Main.MouseScreen;
            Anchor = new((int)mouse.X, (int)mouse.Y);
        }

        private void UpdateWheelAnchor()
        {
            WheelElement.Anchor = Anchor;
        }

        private void UpdateWheelPosition()
        {
            for (int i = 0; i < _wheelElements.Length; i++)
            {
                _wheelElements[i].Left.Set(Anchor.X + _wheelVectors[i].X, 0f);
                _wheelElements[i].Top.Set(Anchor.Y + _wheelVectors[i].Y, 0f);
                _wheelElements[i].ItemPosition = new Vector2(Anchor.X + _itemVectors[i].X, Anchor.Y + _itemVectors[i].Y);
            }
        }

        private void UpdateMouseAnchor()
        {
            MouseAnchor = Main.MouseScreen - Anchor;

            for (int i = 0; i < _wheelElements.Length; i++)
            {
                _wheelElements[i].MouseAnchor = MouseAnchor;
            }
        }
    }
}
