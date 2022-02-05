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
            _wheelVectors[0] = new Vector2(-105, 66);
            _wheelVectors[1] = new Vector2(-154, -105);
            _wheelVectors[2] = new Vector2(-105, -154);
            _wheelVectors[3] = new Vector2(66, -105);

            _itemVectors[0] = new Vector2(0, 120);
            _itemVectors[1] = new Vector2(-120, 0);
            _itemVectors[2] = new Vector2(0, -120);
            _itemVectors[3] = new Vector2(120, 0);
        }
    }
}
