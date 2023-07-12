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
        new UsageExample { Method = txt => txt.Stylize().Bold, MethodScript = "txt.Stylize().Bold", Description = "Bold text" },
        new UsageExample {
            Method = txt => txt.Stylize().Size(24),
            MethodScript = "text.Stylize().Size(24)",
            Description = "Text with size of 24 (double than default)",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Underline, MethodScript = "text.Stylize().Underline", Description = "Underlined text",
        },
        new UsageExample { Method = txt => txt.Stylize().Red, MethodScript = "text.Stylize().Red", Description = "Colored text" },
        new UsageExample {
            Method = txt => txt.Stylize().Red.Green, MethodScript = "text.Stylize().Red.Green", Description = "Gradient (Red.Green) text",
        },
        new UsageExample {
            Method = txt => txt.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24),
            MethodScript = "text.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24)",
            Description = "Gradient text with size 24",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1).Blue, MethodScript = "text.Stylize(1).Blue", Description = "The second word is Colored",
        },
        new UsageExample {
            Method = txt => txt.Stylize(^1).Purple,
            MethodScript = "text.Stylize(^1).Purple",
            Description = "The Second word from the end is Colored",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1, true).Purple,
            MethodScript = "text.Stylize(1, true).Purple",
            Description = "(Same, but different writing style)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(..1).Violet.Bold,
            MethodScript = "text.Stylize(..1).Violet.Bold",
            Description = "First 2 words are Colored and Bold",
        },
        new UsageExample {
            Method = txt => txt.Stylize(0, 1).Violet.Bold,
            MethodScript = "text.Stylize(0, 1).Violet.Bold",
            Description = "(Same, but different writing style)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(^1..).Red.Bold,
            MethodScript = "text.Stylize(^1..).Red.Bold",
            Description = "Last 2 words are Colored and Bold",
        },
        new UsageExample {
            Method = txt => txt.Stylize(1, 0, true).Red.Bold,
            MethodScript = "text.Stylize(1, 0, true).Red.Bold",
            Description = "(Same, but different writing style)",
        },
        new UsageExample {
            Method = txt => txt.Stylize(-.3f).Orange.Size(24),
            MethodScript = "text.Stylize(-.3f).Orange.Size(24)",
            Description = "Last 30% of the words are Colored and has size 24",
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

        using (GUI.CollapsableGroup(ref _isExpanded_ExampleText, "Example text")) {
            if (_isExpanded_ExampleText) {
                string Txt = "You can type here...";

                for (int i = 0; i < _usageExamples.Length; i++) {
                    GUI.Space();


                    _stopWatch.Restart();
                    string exampleText = _usageExamples[i].Method(Txt);
                    _exampleMethodTicks[i] = _stopWatch.Elapsed;
                    _stopWatch.Stop();

                    using (GUI.Horizontal()) {
                        GUILayout.TextArea(exampleText, _gui_testCaseTextArea);
                        if (GUILayout.Button("<b>C#</b>", _guis_testInfoButton)) {
                            _isScriptExpanded[i] = !_isScriptExpanded[i];
                        }

                        if (GUI.Button("<b>?</b>", _guis_testInfoButton)) {
                            _isDescriptionExpanded[i] = !_isDescriptionExpanded[i];
                        }

                        if (GUI.Button("<b>txt</b>", _guis_testInfoButton)) {
                            _isTxtExpanded[i] = !_isTxtExpanded[i];
                        }
                    }

                    if (_isScriptExpanded[i]) {
                        if (GUILayout.Button($"<b>C#:</b> <i>{_usageExamples[i].MethodScript}</i>", _guis_testScriptHelpBox)) {
                            Debug.Log(GraffitiAssetDatabase.FindPathToFile(nameof(GraffitiSettingsSo_Editor), true)[0]);
                            AssetDatabase.OpenAsset(
                                AssetDatabase.LoadAssetAtPath<TextAsset>(
                                    GraffitiAssetDatabase.FindPathToFile(ExamplesFileName, true)[0]),
                                OpenFileDefaultLineNumber + LineNumberStep * i,
                                OpenFileColumnNumber
                            );
                        }
                    }

                    if (_isDescriptionExpanded[i]) {
                        GUILayout.Label($"<b>?:</b> {_usageExamples[i].Description}", _guis_testDescriptionHelpBox);
                        GUILayout.Label($"Benchmark: {_exampleMethodTicks[i].Milliseconds} ms; {_exampleMethodTicks[i].Ticks} ticks", _guis_testDescriptionHelpBox);
                    }

                    if (_isTxtExpanded[i]) {
                        GUILayout.Label($"txt:\n{exampleText}", _gui_textArea);
                    }
                }

                GUI.Space();
            }
        }

        using (GUI.Group("Debug.Log examples")) {
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

    private struct UsageExample {

        public Func<string, string> Method;
        public string               MethodScript;
        public string               Description;

    }

}
}
