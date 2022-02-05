using ItemWheel.Content.Common.Configs;
using ItemWheel.Content.UI.Element;
using Microsoft.Xna.Framework;

namespace ItemWheel.Content.UI
{
    internal class EightWheel : WheelUI
    {
        public EightWheel(ClientConfigs clientConfigs) : base(8, clientConfigs)
        {
            _wheelVectors[0] = new Vector2(-53, 84);
            _wheelVectors[1] = new Vector2(-140, 39);
            _wheelVectors[2] = new Vector2(-154, -53);
            _wheelVectors[3] = new Vector2(-140, -140);
            _wheelVectors[4] = new Vector2(-53, -154);
            _wheelVectors[5] = new Vector2(39, -140);
            _wheelVectors[6] = new Vector2(84, -53);
            _wheelVectors[7] = new Vector2(39, 39);

            _itemVectors[0] = new Vector2(0, 120);
            _itemVectors[1] = new Vector2(-86, 86);
            _itemVectors[2] = new Vector2(-120, 0);
            _itemVectors[3] = new Vector2(-86, -86);
            _itemVectors[4] = new Vector2(0, -120);
            _itemVectors[5] = new Vector2(86, -86);
            _itemVectors[6] = new Vector2(120, 0);
            _itemVectors[7] = new Vector2(86, 86);
        }
    }
}
