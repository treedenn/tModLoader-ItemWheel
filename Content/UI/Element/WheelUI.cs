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

        protected WheelUI(int wheelSize, int itemSize)
        {
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

            UpdateWheelElements();
        }

        public override void Update(GameTime gameTime)
        {
            if (ItemWheel.ToggleWheelKey.Current || ItemWheel.ToggleWheelKey.JustReleased)
            {
                base.Update(gameTime);
                UpdateWheelElements();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ItemWheel.ToggleWheelKey.Current)
            {
                base.Draw(spriteBatch);
            }
        }

        protected Vector2 UpdateAnchor()
        {
            Vector2 playerCenter = Main.LocalPlayer.Center.ToScreenPosition();
            Anchor = new((int)playerCenter.X, (int)playerCenter.Y);
            return Anchor;
        }

        protected Vector2 UpdateMouseAnchor()
        {
            MouseAnchor = Main.MouseScreen - Anchor;
            return MouseAnchor;
        }

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
                _wheelElements[i].SetWheelVectors(_elementBorders[i % _elementBorders.Length], _elementBorders[(i + 1) % _elementBorders.Length]);
            }

            for (int i = 0; i < _elementBorders.Length; i++)
            {
                _elementBorders[i] = Vector2.UnitY.RotatedBy((i * angle - 45f) * Math.PI / 180);
            }
        }
    }
}
