using ItemWheel.Content.UI.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace ItemWheel.Content.UI
{
    internal class WheelUIState : UIState
    {
        private WheelUI _wheel;

        public WheelUIState()
        {
            _wheel = new FourWheel(40);
            Append(_wheel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
