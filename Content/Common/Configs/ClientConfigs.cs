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
        [Range(4, 8)]
        [Slider]
        [ReloadRequired]
        public int Wheels;

        [Label("$Mods.ItemWheel.Config.ItemScale.Label")]
        [Tooltip("$Mods.ItemWheel.Config.ItemScale.Tooltip")]
        [DefaultValue(1)]
        [Increment(0.2f)]
        [Range(0.4f, 2.6f)]
        [Slider]
        [ReloadRequired]
        public float ItemScale;

        [Label("$Mods.ItemWheel.Config.Deadzone.Label")]
        [Tooltip("$Mods.ItemWheel.Config.Deadzone.Tooltip")]
        [DefaultValue(40)]
        [Increment(8)]
        [Range(0, 88)]
        [Slider]
        [ReloadRequired]
        public int Deadzone;
    }

    internal enum WheelPlacement
    {
        PLAYER,
        MOUSE
    }
}
