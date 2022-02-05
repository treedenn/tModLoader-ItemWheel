using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace ItemWheel.Content.UI.Element
{
    internal class WheelElement : UIElement
    {
        private static float DefaultItemSize = 32f;

        public static Vector2 Anchor { get; set; }
        public static float ItemScale { get; set; }
        public static int Deadzone { get; set; }

        public Vector2 MouseAnchor { get; set; }
        public Vector2[] Borders { get; private set; }
        public Item HoldingItem { get; private set; }
        public Vector2 ItemPosition { get; set; }

        private Asset<Texture2D> _texture;

        private Asset<Texture2D> _itemTexture;
        private float _itemScale;
        private Rectangle _itemRectangle;

        public WheelElement(Asset<Texture2D> texture) : base()
        {
            Borders = new Vector2[2];
            HoldingItem = null;

            _texture = texture;

            Width.Set(_texture.Width(), 0f);
            Height.Set(_texture.Height(), 0f);
        }

        public void SetBorders(Vector2 v0, Vector2 v1)
        {
            Borders[0] = v0;
            Borders[1] = v1;
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsSelected()) return;

            // swap to item on release
            if (ItemWheel.ToggleWheelKey.JustReleased && Main.mouseItem.IsAir)
            {
                if (HoldingItem != null)
                {
                    int invIndex = FindItemInInventory(HoldingItem.type);
                    if (invIndex != -1)
                    {
                        Utils.Swap(ref Main.LocalPlayer.inventory[invIndex], ref Main.LocalPlayer.inventory[9]);
                        Main.LocalPlayer.selectedItem = 9;
                    }
                    else
                    {
                        HoldingItem = null;
                    }
                }
            }
            else if (ItemWheel.ToggleWheelKey.Current)
            {
                Main.LocalPlayer.mouseInterface = true;

                if (Main.keyState.PressingShift())
                {
                    if (Main.mouseLeft && HoldingItem == null && !Main.mouseItem.IsAir)
                    {
                        // add item to item wheel
                        HoldingItem = ContentSamples.ItemsByType[Main.mouseItem.type];
                        LoadItem(HoldingItem);
                    }
                    else if (Main.mouseRight && HoldingItem != null)
                    {
                        // remove item from item wheel
                        HoldingItem = null;
                    }
                }
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(FontAssets.MouseText.Value, "", GetDimensions().Position(), Color.White);

            if (IsSelected())
            {
                spriteBatch.Draw(_texture.Value, GetDimensions().Position(), Color.CornflowerBlue);
            }
            else
            {
                spriteBatch.Draw(_texture.Value, GetDimensions().Position(), new Color(50f, 200f, 50f, 0.50f));
            }

            if (HoldingItem != null)
            {
                DrawItem(spriteBatch);
            }
        }

        private void LoadItem(Item item)
        {
            Main.instance.LoadItem(item.type);
            _itemTexture = TextureAssets.Item[item.type];
            _itemRectangle = (Main.itemAnimations[item.type] == null) ? _itemTexture.Frame() : Main.itemAnimations[item.type].GetFrame(_itemTexture.Value);

            float ratioX = _itemTexture.Width() / _itemTexture.Height();
            float ratioY = _itemTexture.Height() / _itemTexture.Width();

            if (ratioX > ratioY)
            {
                _itemScale = DefaultItemSize / _itemTexture.Width();
            }
            else if (ratioX < ratioY)
            {
                _itemScale = DefaultItemSize / _itemTexture.Height();
            }
            else
            {
                _itemScale = DefaultItemSize / _itemTexture.Width();
            }

            _itemScale *= ItemScale;
        }

        private void DrawItem(SpriteBatch spriteBatch)
        {
            var topLeftItemPosition = ItemPosition - new Vector2(_itemTexture.Width(), _itemTexture.Height()) * _itemScale / 2;
            spriteBatch.Draw(_itemTexture.Value, topLeftItemPosition, _itemRectangle, HoldingItem.GetAlpha(Color.White), 
                0f, Vector2.Zero, _itemScale, SpriteEffects.None, 0f);
        }

        private bool IsSelected()
        {
            return Vector2.Distance(MouseAnchor, new Vector2()) > Deadzone
                && - CrossProduct(MouseAnchor, Borders[0]) > 0
                && CrossProduct(MouseAnchor, Borders[1]) > 0;
        }

        private static int FindItemInInventory(int itemType)
        {
            Item[] inventory = Main.LocalPlayer.inventory;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].type == itemType)
                    return i;
            }
            return -1;
        }

        private static float CrossProduct(Vector2 v0, Vector2 v1)
        {
            return (v0.X * v1.Y) - (v0.Y * v1.X);
        }
    }
}
