using ItemWheel.Content.Common.Configs;
using ItemWheel.Content.UI.Element;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ItemWheel.Content.UI
{
    internal class FourWheel : WheelUI
    {
        public FourWheel(ClientConfigs clientConfigs) : base(4, clientConfigs)
        {
            _wheelVectors[0] = new Vector2(-105f, 66f);
            _wheelVectors[1] = new Vector2(-154f, -105f);
            _wheelVectors[2] = new Vector2(-105f, -154f);
            _wheelVectors[3] = new Vector2(66f, -105f);

            Vector2 halfItemSize = new Vector2(clientConfigs.ItemSize / 2);

            _itemVectors[0] = new Vector2(0, 120) - halfItemSize;
            _itemVectors[1] = new Vector2(-120, 0) - halfItemSize;
            _itemVectors[2] = new Vector2(0, -120) - halfItemSize;
            _itemVectors[3] = new Vector2(120, 0) - halfItemSize;
        }
    }
}
