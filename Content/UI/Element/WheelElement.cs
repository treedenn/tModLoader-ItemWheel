using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace ItemWheel.Content.UI.Element
{
    internal class WheelElement : UIElement
    {
        public Vector2 Anchor { get; set; }
        public Vector2 MouseAnchor { get; set; }
        public Vector2[] Borders { get; private set; }
        public Item HoldingItem { get; private set; }
        public Vector2 ItemPosition { get; set; }

        public float HalfWidth { get => Width.Pixels / 2; }
        public float HalfHeight { get => Height.Pixels / 2; }

        private Asset<Texture2D> _texture;
        private float _itemSize;

        public WheelElement(Asset<Texture2D> texture, float itemSize) : base()
        {
            Borders = new Vector2[2];
            HoldingItem = null;

            _itemSize = itemSize;
            _texture = texture;

            Width.Set(_texture.Width(), 0f);
            Height.Set(_texture.Height(), 0f);
        }

        public override void Update(GameTime gameTime)
        {
            // Add item when holding shift + left - click(empty)
            // Delete item when holding shift + right - click
            // Select item on mouse release, no shift

            if (!BetweenBorders()) return;

            if (ItemWheel.ToggleWheelKey.JustReleased && Main.mouseItem.IsAir)
            {
                if (HoldingItem != null)
                {
                    int invIndex = FindItemInInventory(HoldingItem.type);
                    if (invIndex != -1)
                    {
                        Utils.Swap(ref Main.LocalPlayer.inventory[invIndex], ref Main.LocalPlayer.inventory[9]);
                        Main.LocalPlayer.selectedItem = 9;
                    } else
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
                    if (Main.mouseRight && HoldingItem != null)
                    {
                        HoldingItem = null;
                    }
                    else if (Main.mouseLeft && HoldingItem == null && !Main.mouseItem.IsAir)
                    {
                        HoldingItem = ContentSamples.ItemsByType[Main.mouseItem.type];
                    }
                }
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawString(FontAssets.MouseText.Value, "", GetDimensions().Position(), Color.White);

            if (BetweenBorders())
            {
                spriteBatch.Draw(_texture.Value, GetDimensions().Position(), Color.CornflowerBlue);
            }
            else
            {
                spriteBatch.Draw(_texture.Value, GetDimensions().Position(), new Color(50f, 200f, 50f, 0.50f));
            }

            if (HoldingItem != null)
            {
                DrawItem(spriteBatch, HoldingItem);
            }
        }

        public void SetBorders(Vector2 v0, Vector2 v1)
        {
            Borders[0] = v0;
            Borders[1] = v1;
        }

        protected void DrawItem(SpriteBatch spriteBatch, Item item)
        {
            Main.instance.LoadItem(item.type);
            Asset<Texture2D> itemTexture = TextureAssets.Item[item.type];

            Rectangle rectangle = (Main.itemAnimations[item.type] == null) ? itemTexture.Frame() : Main.itemAnimations[item.type].GetFrame(itemTexture.Value);
            float scale = _itemSize / rectangle.Height;

            spriteBatch.Draw(itemTexture.Value, ItemPosition, rectangle, item.GetAlpha(Color.White), 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        protected bool BetweenBorders()
        {
            return -CrossProduct(MouseAnchor, Borders[0]) > 0 && CrossProduct(MouseAnchor, Borders[1]) > 0;
        }

        protected static int FindItemInInventory(int itemType)
        {
            Item[] inventory = Main.LocalPlayer.inventory;
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i].type == itemType)
                    return i;
            }
            return -1;
        }

        protected static float CrossProduct(Vector2 v0, Vector2 v1)
        {
            return (v0.X * v1.Y) - (v0.Y * v1.X);
        }
    }
}
