using System;
using System.Diagnostics;
using System.Linq;
using Graffiti;
using Graffiti.Internal;
using Graffiti.Tests;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;
using GUI = GraffitiEditor.GraffitiGUI;


namespace GraffitiEditor {
[CustomEditor(typeof(GraffitiSettingsSo))]
public class GraffitiSettingsSo_Editor : Editor {

    private const int    OpenFileDefaultLineNumber = 18;
    private const int    LineNumberStep            = 5;
    private const int    OpenFileColumnNumber      = 37;
    private const string ExamplesFileName          = nameof(GraffitiSettingsSo_Editor);

    public const string LONG_LOREM_IPSUM_TEXT =
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer tellus lorem, commodo eu mauris eu, "
          + "sodales auctor eros. Suspendisse sit amet quam diam. Vestibulum sagittis magna eu lacus semper, "
          + "ac dapibus sem rhoncus. In quis interdum urna. Sed ac mattis sapien. In fringilla condimentum rutrum. "
          + "Duis auctor ipsum nec urna tincidunt, non tincidunt quam facilisis.";

    private static readonly UsageExample[] _usageExamples = {
        new UsageExample {
            Header = "Basic"
        },
        new UsageExample {
            Method = txt => txt.Stylize().Bold,
            MethodScript = "txt.Stylize().Bold",
            Description = "You can add font style. Current styles: Bold, Italic"
        },
        new UsageExample {
            Method = txt => txt.Stylize().Size(24),
            MethodScript = "txt.Stylize().Size(24)",
            Description = "You can change the of text. Default size is 12",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Red,
            MethodScript = "txt.Stylize().Red",
            Description = "You can colorize text. Current colors: White, Grey, Black, Red, Orange, Yellow, Green, Blue, Purple, Violet",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Red.Yellow,
            MethodScript = "txt.Stylize().Red.Yellow",
            Description = "You can combine several colors to create gradient from first to last. The limit of colors is 8",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Underline,
            MethodScript = "txt.Stylize().Underline",
            Description = "You can add modifier characters. Current modifiers: SmokingHot, Strikethrough, WavyStrikethrough, Slash, HighSlash, Underline, DoubleUnderline, Dotted, Wheel",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Blue.Italic.Underline.Red.Bold,
            MethodScript = "txt.Stylize().Blue.Italic.Underline.Red.Bold",
            Description = "You can apply style to modifier character. Everything written after modifier character will be considered as it's own style."
              + " In this case Underline will be Red and Bold",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..2).Yellow.Blue.Underline.Red.And(^0).Red,
            MethodScript = "txt.Stylize(..2).Yellow.Blue.Underline.Red.And(^0).Red",
            Description = "You can apply another style with it's own range and modifiers using method .And()",
        },
        new UsageExample {
            Method = txt => txt.Stylize(0).Violet.Purple.And(1).Yellow.Blue.And(2).Bold.Red.And(3).Blue.Underline.Red,
            MethodScript = "txt.Stylize(0).Violet.Purple.And(1).Yellow.Blue.And(2).Bold.Red.And(3).Blue.Underline.Red",
            Description = "You can create as many styles as you want",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..1).Violet.Purple.Bold.And(^2..).Yellow.Blue,
            MethodScript = "txt.Stylize(..1).Violet.Purple.Bold.And(^2..).Yellow.Blue",
            Description = "Your stiles can overlap. The last style will be prioritized, but it will contain every other unique modifier."
                        + " Where styles overlap the Bold modifier of the first style is present, but gradient is of the last style",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..1).Violet.Purple.Underline.Red.And(^2..).Yellow.Blue.Strikethrough.Black,
            MethodScript = "txt.Stylize(..1).Violet.Purple.Underline.Red.And(^2..).Yellow.Blue.Strikethrough.Black",
            Description = "You can also overlap modifier characters, they will be combined",
        },


        new UsageExample {
            Header = "Range specification"
        },
        new UsageExample {
            Method = txt => txt.Stylize(1).Blue,
            MethodScript = "txt.Stylize(1).Blue",
            Description = "You can specify the index of a word to modify (index of the first word is 0)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1, true).Purple,
            MethodScript = "txt.Stylize(1, true).Purple",
            Description = "You can specify the index of a word from the end",
        },
        new UsageExample {
            Method = txt => txt.Stylize(^1).Purple,
            MethodScript = "txt.Stylize(^1).Purple",
            Description = "You can also use IndexRange",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..1).Violet.Bold,
            MethodScript = "txt.Stylize(..1).Violet.Bold",
            Description = "IndexRange from the first word to the second",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1..).Red.Bold,
            MethodScript = "txt.Stylize(1..).Red.Bold",
            Description = "IndexRange from the second word to the last",
        },
        new UsageExample {
            Method = txt => txt.Stylize(0, 1).Violet.Bold,
            MethodScript = "txt.Stylize(0, 1).Violet.Bold",
            Description = "This is an alternative to IndexRange. You provide 2 integers (startIndex, endIndex) (including)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1, 0, true).Red.Bold,
            MethodScript = "txt.Stylize(1, 0, true).Red.Bold",
            Description = "This is an alternative to IndexRange. You provide 2 integers (startIndex, endIndex, fromEnd?)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(-.3f).Orange.Size(24),
            MethodScript = "txt.Stylize(-.3f).Orange.Size(24)",
            Description = "You can specify a percentage of words to modify. This example modifies last 30% of the words",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..2).Orange.And(3..).Blue,
            MethodScript = "txt.Stylize(..2).Orange.And(3..).Blue",
            Description = "All range options work with .And() method",
        },

        new UsageExample {
            Header = "Complex"
        },
        new UsageExample {
            Method = txt => txt.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24),
            MethodScript = "text.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24)",
            Description = "Gradient text with size 24",
        },
        new UsageExample {
            Method = txt => LONG_LOREM_IPSUM_TEXT
                           .Stylize(..).Green.Blue.Red.Purple
                           .And(..1).Size(18)
                           .And(2, 15).Italic.Bold
                           .And(-.5f).Underline[Style.Purple.Yellow]
                           .And(24..30).Strikethrough[Style.DefaultColor],
            MethodScript = "txt.Stylize(..).Green.Blue.Red.Purple"
                          + ".And(..1).Size(18)"
                          + ".And(2, 15).Italic.Bold"
                          + ".And(-.5f).Underline[Style.Purple.Yellow]"
                          + ".And(24..30).Strikethrough[Style.DefaultColor]",
            Description = "All text - gradient Green-Blue-Red-Purple\n"
                          + "2 first word - size 18\n"
                          + "words 3 to 15 - Italic-Bold\n"
                          + "last 50% - Purple-Yellow Underline\n"
                          + "words 25 to 30 - Strikethrough",
        },
    };

    private static readonly bool[] _isDescriptionExpanded = new bool[_usageExamples.Length];
    private static readonly bool[] _isBenchmarkExpanded   = new bool[_usageExamples.Length];
    private static readonly bool[] _isTxtExpanded         = new bool[_usageExamples.Length];
    private static readonly bool[] _isScriptExpanded      = new bool[_usageExamples.Length];


    private static GUIStyle _gui_textArea => new GUIStyle("TextArea") {
    };

    private static GUIStyle _gui_richTextTextArea => new GUIStyle("TextArea") {
        richText = true
    };

    private static GUIStyle _gui_testCaseTextArea => new GUIStyle("TextArea") {
        richText = true, alignment = TextAnchor.MiddleLeft, stretchHeight = true
    };

    private static GUIStyle _guis_testDescriptionHelpBox => new GUIStyle("HelpBox") {
        richText = true, stretchWidth = true, fontSize = 12
    };

    private static GUIStyle _guis_testScriptHelpBox      => new GUIStyle("HelpBox") {
        richText = true, stretchWidth = true, fontSize = 12
    };

    private static GUIStyle _guis_testInfoButton => new GUIStyle("Button") {
        richText = true, fixedWidth = 28, fixedHeight = GUI.DefaultPropertyHeight * 2
    };

    private bool _isExpanded_ColorPaletteSelection;
    private bool _isExpanded_Settings;
    private bool _isExpanded_ExampleText = true;

    private             SerializedProperty _SP_config;
    [CanBeNull] private ColorPaletteSo     _SO_palette;
    [CanBeNull] private SerializedProperty _SP_palette;

    private GraffitiSettingsSo _target;

    private readonly Stopwatch          _stopWatch          = new Stopwatch();
    private readonly TimeSpan[]     _exampleMethodTicks = new TimeSpan[_usageExamples.Length];



    private void UpdateVariables()
    {
        _target = target as GraffitiSettingsSo;
        _SP_config = serializedObject.FindProperty(GraffitiSettingsSo.__nameof_config);
        _SP_palette = serializedObject.FindProperty(GraffitiSettingsSo.__nameof_colorPalette);
        _SO_palette = _SP_palette?.CastTo<ColorPaletteSo>();
    }

    public override void OnInspectorGUI()
    {
        UpdateVariables();
        serializedObject.Update();

        GUI.DrawGraffitiVersion();

        using (GUI.CollapsableGroup(ref _isExpanded_ColorPaletteSelection, "Color Palette Selection")) {
            if (_isExpanded_ColorPaletteSelection) {
                GUI.DrawProperty(_SP_palette);

                using (GUI.IndentLevel(1)) {
                    GUI.DrawScriptableObject(_SO_palette);
                }
            }
        }

        using (GUI.CollapsableGroup(ref _isExpanded_Settings, "Settings")) {
            if (_isExpanded_Settings) {
                using (GUI.IndentLevel(1))
                using (GUI.LabelWidth(250)) {
                    GUI.DrawChildrenProperties(_SP_config);
                }
            }
        }

        using (GUI.CollapsableGroup(ref _isExpanded_ExampleText, "Examples")) {
            if (_isExpanded_ExampleText) {

                using (GUI.Horizontal()) {
                    if (GUILayout.Button("<b>C#</b>", _guis_testInfoButton))
                        ToggleAll(_isScriptExpanded);
                    if (GUI.Button("<b>?</b>", _guis_testInfoButton))
                        ToggleAll(_isDescriptionExpanded);
                    if (GUI.Button("<b>ms</b>", _guis_testInfoButton))
                        ToggleAll(_isBenchmarkExpanded);
                    if (GUI.Button("<b>txt</b>", _guis_testInfoButton))
                        ToggleAll(_isTxtExpanded);
                }


                //string Txt = "You can type here...";
                string Txt = "Something smart, something unique";

                int examplesCount = _usageExamples.Length;
                GUI.Space();


                if (_usageExamples[0].Header == null)
                    _usageExamples[0].Header = "Please, add header as element 0";

                for (int i = 0; i < examplesCount;) {
                    int headerIndex = i;
                    using (GUI.CollapsableGroup(ref _usageExamples[i].IsHeaderExpanded, _usageExamples[i].Header)) {
                        for (i += 1; i < examplesCount; i++) {

                            if (_usageExamples[i].Header != null)
                                break;

                            if (_usageExamples[headerIndex].IsHeaderExpanded) {
                                // if (i == ) {
                                //     Debug.Log("");
                                // }

                                RenderExample(i);
                            }
                        }
                    }
                }

                void RenderExample(int i)
                {
                    GUI.Space();

                    _stopWatch.Restart();
                    string exampleText = _usageExamples[i].Method(Txt);
                    _exampleMethodTicks[i] = _stopWatch.Elapsed;
                    _stopWatch.Stop();

                    using (GUI.Horizontal()) {
                        GUILayout.TextArea(exampleText, _gui_testCaseTextArea);
                        if (GUILayout.Button("<b>C#</b>", _guis_testInfoButton))
                            _isScriptExpanded[i] = !_isScriptExpanded[i];
                        if (GUI.Button("<b>?</b>", _guis_testInfoButton))
                            _isDescriptionExpanded[i] = !_isDescriptionExpanded[i];
                        if (GUI.Button("<b>ms</b>", _guis_testInfoButton))
                            _isBenchmarkExpanded[i] = !_isBenchmarkExpanded[i];
                        if (GUI.Button("<b>txt</b>", _guis_testInfoButton))
                            _isTxtExpanded[i] = !_isTxtExpanded[i];
                    }

                    if (_isScriptExpanded[i]
                     || _isDescriptionExpanded[i]
                     || _isBenchmarkExpanded[i]
                     || _isTxtExpanded[i]) {
                        GUI.Space(3);
                    }

                    if (_isScriptExpanded[i]) {
                        if (GUILayout.Button($"<b>C#:</b> <size=14>{_usageExamples[i].MethodScript}</size>",
                            _gui_richTextTextArea)) {
                            Debug.Log(GraffitiAssetDatabase.FindPathToFile(nameof(GraffitiSettingsSo_Editor),
                                true)[0]);
                            AssetDatabase.OpenAsset(
                                AssetDatabase.LoadAssetAtPath<TextAsset>(
                                    GraffitiAssetDatabase.FindPathToFile(ExamplesFileName, true)[0]),
                                OpenFileDefaultLineNumber + LineNumberStep * i,
                                OpenFileColumnNumber
                            );
                        }
                    }

                    if (_isDescriptionExpanded[i]) {
                        GUILayout.TextArea($"<b>?:</b> {_usageExamples[i].Description}",
                            _gui_richTextTextArea);
                    }

                    if (_isBenchmarkExpanded[i]) {
                        GUILayout.Label(
                            $"Benchmark: {_exampleMethodTicks[i].Milliseconds} ms; {_exampleMethodTicks[i].Ticks} ticks",
                            _guis_testDescriptionHelpBox);
                    }

                    if (_isTxtExpanded[i]) {
                        GUILayout.TextArea($"{exampleText}", _gui_textArea);
                    }
                }
            }
        }

        using (GUI.Group("")) {
            ExampleCenteredButton("Log Cool Test", AdditionalTests.Run_OnlyInteresting);
            ExampleCenteredButton("Log Boring Test", AdditionalTests.Run_All);
            ExampleCenteredButton("Tests With Description", SimpleTests.RunAllTests);
            ExampleCenteredButton("Old Tests", Tests.RunAllTests);
            GUI.Space();

            void ExampleCenteredButton(string buttonName, Action action)
            {
                if (GUI.CenteredButton(buttonName, options: GUI.CreateOptions().Width(250).Height(26))) {
                    action();
                }

                GUI.Space();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }


    private static void ToggleAll(bool[] array)
    {
        if (array.All((x) => x))
            for (int i = 0; i < array.Length; i++)
                array[i] = false;
        else
            for (int i = 0; i < array.Length; i++)
                array[i] = true;
    }

    private class UsageExample
    {

        public string Header;
        public bool   IsHeaderExpanded = true;

        public Func<string, string> Method;
        public string               MethodScript;
        public string               Description;

    }

}
}
