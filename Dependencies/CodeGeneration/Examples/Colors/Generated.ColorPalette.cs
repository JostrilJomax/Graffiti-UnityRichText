//--------------------------------------------------------------------------------------
// This file is generated. Modifications to this file won't be saved.
// Creation time: 2022-08-26 19:19:10
// If you want to make any permanent changes, go to the class ColorPaletteGenerator
// At: ???
//--------------------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Generated {
    /// <summary>Class that will contain different variations of colors</summary>
    public class NeatColor {
        public Color UColor { get; private set; }
        public string HexColor { get; private set; }
        public NeatColor(Color uColor, string hexColor) {
            UColor = uColor;
            HexColor = hexColor;
        }
    }

    /// <summary>Class that contains generated colors</summary>
    public partial class ColorPalette {
        public NeatColor White  { get; } = new NeatColor(new Color(0.93f, 0.93f, 0.93f), "#eeeeee)");
        public NeatColor Grey   { get; } = new NeatColor(new Color(0.67f, 0.67f, 0.67f), "#aaaaaa)");
        public NeatColor Black  { get; } = new NeatColor(new Color(0.07f, 0.07f, 0.07f), "#111111)");
        public NeatColor Red    { get; } = new NeatColor(new Color(0.93f, 0.23f, 0.25f), "#EC3A41)");
        public NeatColor Orange { get; } = new NeatColor(new Color(0.93f, 0.59f, 0.18f), "#ED962F)");
        public NeatColor Yellow { get; } = new NeatColor(new Color(1f,    0.89f, 0.26f), "#FFE443)");
        public NeatColor Green  { get; } = new NeatColor(new Color(0.22f,  0.8f,  0.5f), "#39CD7F)");
        public NeatColor Blue   { get; } = new NeatColor(new Color(0.19f, 0.63f, 0.69f), "#30a0b0)");
        public NeatColor Purple { get; } = new NeatColor(new Color(0.89f, 0.27f, 0.69f), "#E245B0)");
        public NeatColor Violet { get; } = new NeatColor(new Color(0.49f, 0.38f, 0.76f), "#7d60c3)");
    }

    /// <summary>Class with a method that logs all generated colors to the unity console</summary>
    public static class ColorPaletteLogger {
        [MenuItem("Plugins/*Generated/Log Generated Colors")]
        public static void Log() {
            Debug.Log("<color=#eeeeee>Code Generation is Working! (White)</color>");
            Debug.Log("<color=#aaaaaa>Code Generation is Working! (Grey)</color>");
            Debug.Log("<color=#111111>Code Generation is Working! (Black)</color>");
            Debug.Log("<color=#EC3A41>Code Generation is Working! (Red)</color>");
            Debug.Log("<color=#ED962F>Code Generation is Working! (Orange)</color>");
            Debug.Log("<color=#FFE443>Code Generation is Working! (Yellow)</color>");
            Debug.Log("<color=#39CD7F>Code Generation is Working! (Green)</color>");
            Debug.Log("<color=#30a0b0>Code Generation is Working! (Blue)</color>");
            Debug.Log("<color=#E245B0>Code Generation is Working! (Purple)</color>");
            Debug.Log("<color=#7d60c3>Code Generation is Working! (Violet)</color>");
        }
    }
}
