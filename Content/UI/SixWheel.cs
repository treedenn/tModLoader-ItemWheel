using ItemWheel.Content.Common.Configs;
using ItemWheel.Content.UI.Element;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemWheel.Content.UI
{
    internal class SixWheel : WheelUI
    {
        public SixWheel(ClientConfigs clientConfigs) : base(6, clientConfigs)
        {
            _wheelVectors[0] = new Vector2(-72, 79);
            _wheelVectors[1] = new Vector2(-154, 5);
            _wheelVectors[2] = new Vector2(-154, -130);
            _wheelVectors[3] = new Vector2(-72, -154);
            _wheelVectors[4] = new Vector2(49, -130);
            _wheelVectors[5] = new Vector2(49, 5);

            _itemVectors[0] = new Vector2(0, 120);
            _itemVectors[1] = new Vector2(-105, 60);
            _itemVectors[2] = new Vector2(-105, -60);
            _itemVectors[3] = new Vector2(0, -120);
            _itemVectors[4] = new Vector2(105, -60);
            _itemVectors[5] = new Vector2(105, 60);
        }
    }
}
