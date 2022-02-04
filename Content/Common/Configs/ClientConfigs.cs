using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace ItemWheel.Content.Common.Configs
{
    internal class ClientConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("$Mods.ItemWheel.Config.ItemHeader")]
        [Label("$Mods.ItemWheel.Config.WheelPlacement.Label")]
        [Tooltip("$Mods.ItemWheel.Config.WheelPlacement.Tooltip")]
        [DefaultValue(WheelPlacement.PLAYER)]
        [ReloadRequired]
        public WheelPlacement WheelPlacement; // To see the implementation of this option, see ExampleWings.cs

        [Label("$Mods.ItemWheel.Config.Wheels.Label")]
        [Tooltip("$Mods.ItemWheel.Config.Wheels.Tooltip")]
        [DefaultValue(4)]
        [Increment(2)]
        [Range(4, 6)]
        [Slider]
        [ReloadRequired]
        public int Wheels;

        [Label("$Mods.ItemWheel.Config.ItemSize.Label")]
        [Tooltip("$Mods.ItemWheel.Config.ItemSize.Tooltip")]
        [DefaultValue(40)]
        [Increment(2)]
        [Range(32, 48)]
        [Slider]
        [ReloadRequired]
        public int ItemSize;
    }

    internal enum WheelPlacement
    {
        PLAYER,
        MOUSE
    }
}
