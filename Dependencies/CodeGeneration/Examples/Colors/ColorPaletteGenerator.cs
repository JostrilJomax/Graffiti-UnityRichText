using System.Collections.Generic;

namespace CodeGeneration.Examples {
public static class ColorPaletteGenerator {

    // Some data to the generation method
    private static readonly List<(string name, string trim, string uColor, string hexColor)> ColorFields
                      = new List<(string name, string trim, string uColor, string hexColor)> {
        ("White", " ", "new Color(0.93f, 0.93f, 0.93f)", "#eeeeee"),
        ("Grey", "  ", "new Color(0.67f, 0.67f, 0.67f)", "#aaaaaa"),
        ("Black", " ", "new Color(0.07f, 0.07f, 0.07f)", "#111111"),
        ("Red", "   ", "new Color(0.93f, 0.23f, 0.25f)", "#EC3A41"),
        ("Orange", "", "new Color(0.93f, 0.59f, 0.18f)", "#ED962F"),
        ("Yellow", "", "new Color(1f,    0.89f, 0.26f)", "#FFE443"),
        ("Green", " ", "new Color(0.22f,  0.8f,  0.5f)", "#39CD7F"),
        ("Blue", "  ", "new Color(0.19f, 0.63f, 0.69f)", "#30a0b0"),
        ("Purple", "", "new Color(0.89f, 0.27f, 0.69f)", "#E245B0"),
        ("Violet", "", "new Color(0.49f, 0.38f, 0.76f)", "#7d60c3"),
    };

    // Note that this method will only generate a string. It will not write this string to a file.
    public static string Generate()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        // Will add a header at the top of a file
        b.Header(nameof(ColorPaletteGenerator)).Br();

        // Using UnityEditor namespace to get MenuItem Attribute
        b.Using("UnityEditor");
        // Using UnityEngine namespace to get Color and Debug.Log()
        b.Using("UnityEngine").Br();

        // Namespace will not be indented, that is just my preference. Currently it is not configurable.
        b.Namespace.Name("Generated").Body(() => {

            b.Summary("Class that will contain different variations of colors");

            b.Public.Class.Name("NeatColor").Body(() => {

                b.Public.Property.Returns("Color").Name("UColor").GetPrivateSet();
                b.Public.Property.Returns("string").Name("HexColor").GetPrivateSet();

                // Constructor: (currently constructor auto-creation is not supported)
                b.Public.Method.Name(b.Self).Params(("Color", "uColor"), ("string", "hexColor")).Body(() => {
                    b.Writeln("UColor = uColor;");
                    b.Writeln("HexColor = hexColor;");
                });
            }).Br();

            b.Summary("Class that contains generated colors");

            b.Public.Partial.Class.Name("ColorPalette").Body(() => {
                foreach (var field in ColorFields)
                    b.Public.Property.Returns("NeatColor").Name(field.name + field.trim)
                     .Get($"new NeatColor({field.uColor}, \"{field.hexColor})\")");
            }).Br();

            b.Summary("Class with a method that logs all generated colors to the unity console");

            b.Public.Static.Class.Name("ColorPaletteLogger").Body(() => {
                b.Writeln("[MenuItem(\"Plugins/*Generated/Log Generated Colors\")]");
                b.Public.Static.Method.Returns().Name("Log").Params().Body(() => {
                    foreach (var field in ColorFields)
                        b.Writeln($"Debug.Log(\"<color={field.hexColor}>Code Generation is Working! ({field.name})</color>\");");
                });
            });

        });

        return b.ToString();
    }

}
}
