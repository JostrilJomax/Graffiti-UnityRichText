using System;
using System.Collections.Generic;
using Graffiti;
using Graffiti.Tests;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using GUI = GraffitiEditor.GraffitiGUI;


namespace GraffitiEditor {
[CustomEditor(typeof(GraffitiSettingsSo))]
public class GraffitiSettingsSo_Editor : Editor {

	public const string LONG_LOREM_IPSUM_TEXT =
		"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer tellus lorem, commodo eu mauris eu, " +
		"sodales auctor eros. Suspendisse sit amet quam diam. Vestibulum sagittis magna eu lacus semper, " +
		"ac dapibus sem rhoncus. In quis interdum urna. Sed ac mattis sapien. In fringilla condimentum rutrum. " +
		"Duis auctor ipsum nec urna tincidunt, non tincidunt quam facilisis.";

	private GraffitiSettingsSo _target;
	private SerializedProperty _SP_config;
	[CanBeNull] private SerializedProperty _SP_palette;
	[CanBeNull] private ColorPaletteSo     _SO_palette;

	private static bool _isExpanded_ColorPaletteSelection = false;
	private static bool _isExpanded_Settings = false;
	private static bool _isExpanded_ExampleText = true;

	private static GUIStyle _guis_richText => new GUIStyle("TextArea") { richText = true };
	private static GUIStyle _guis_exampleHeader => new GUIStyle("TextArea") { richText = true, fixedHeight = GUI.DefaultPropertyHeight*2, alignment = TextAnchor.MiddleLeft};
	private static GUIStyle _guis_exampleDescription  => new GUIStyle("HelpBox") { richText = true, stretchWidth = true, fontSize = 12 };
	private static GUIStyle _guis_exampleScript => new GUIStyle("HelpBox") { richText = true, stretchWidth = true, fontSize = 12 };
	private static GUIStyle _guis_exampleButton => new GUIStyle("Button") { richText = true, fixedWidth = 28, fixedHeight = GUI.DefaultPropertyHeight*2 };

	private static GraffitiGUI.Dispenser<bool[]> _dispenser_example = new GraffitiGUI.Dispenser<bool[]>(new []{ false, false});

	private struct UsageExample {
		public Func<string, string> Method;
		public string MethodScript;
		public string Description;
	}

#line 10000
	private static UsageExample[] _usageExamples = {
			new UsageExample {
					Method = txt => txt.Stylize().Bold,
					MethodScript = "txt.Stylize().Bold",
					Description = "Bold text",
			},

			new UsageExample {
					Method = txt => txt.Stylize().Size(24),
					MethodScript = "text.Stylize().Size(24)",
					Description = "Text with size of 24 (double than default)",
			},
			new UsageExample {
					Method = txt => txt.Stylize().Underline,
					MethodScript = "text.Stylize().Underline",
					Description = "Underlined text",
			},
			new UsageExample {
					Method = txt => txt.Stylize().Red,
					MethodScript = "text.Stylize().Red",
					Description = "Colored text",
			},
			new UsageExample {
					Method = txt => txt.Stylize().Red.Green,
					MethodScript = "text.Stylize().Red.Green",
					Description = "Gradient (Red.Green) text",
			},
			new UsageExample {
					Method = txt => txt.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24),
					MethodScript = "text.Stylize().Red.Yellow.Green.Blue.Purple.Orange.Size(24)",
					Description = "Gradient text with size 24",
			},
			new UsageExample {
					Method = txt => txt.Stylize(1).Blue,
					MethodScript = "text.Stylize(1).Blue",
					Description = "The second word is Colored",
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
	};
#line default


	private void UpdateVariables() {
		_target     = target as GraffitiSettingsSo;
		_SP_config  = serializedObject.FindProperty(GraffitiSettingsSo.__nameof_config);
		_SP_palette = serializedObject.FindProperty(GraffitiSettingsSo.__nameof_colorPalette);
		_SO_palette = _SP_palette?.CastTo<ColorPaletteSo>();
	}

	public override void OnInspectorGUI() {

		UpdateVariables();
		serializedObject.Update();

		GUI.DrawGraffitiVersion();

		using (GUI.CollapsableGroup(ref _isExpanded_ColorPaletteSelection, "Color Palette Selection")) {
			if (_isExpanded_ColorPaletteSelection) {
				GUI.DrawProperty(_SP_palette);

				using (GUI.IndentLevel(1))
					GUI.DrawScriptableObject(_SO_palette);
			}
		}

		using (GUI.CollapsableGroup(ref _isExpanded_Settings, "Settings")) {
			if (_isExpanded_Settings) {
				using (GUI.IndentLevel(1))
				using (GUI.LabelWidth(250))
					GUI.DrawChildrenProperties(_SP_config, false);
			}
		}

		using (GUI.CollapsableGroup(ref _isExpanded_ExampleText, "Example text")) {
			if (_isExpanded_ExampleText) {


				string Txt  = "You can type here...";

				using (GUI.DispenserGroup(_dispenser_example)) {

					foreach (UsageExample example in _usageExamples) {
						_dispenser_example.Set(DrawExampleText(_dispenser_example.Get[0], _dispenser_example.RepeatGet[1], example.Method(Txt), example.MethodScript, example.Description));
					}
				}


				GUILayout.TextArea(
						LONG_LOREM_IPSUM_TEXT
							   .Stylize(..).Green.Blue.Red.Purple
							   .And(..1).Size(18)
							   .And(2, 15).Italic.Bold
							   .And(-.5f).Underline[Style.Purple.Yellow]
							   .And(24..30).Strikethrough[Style.DefaultColor],
						_guis_richText);
			}
		}

		using (GUI.Group("Debug.Log examples")) {
			GUI.Space();
			if (GUI.CenteredButton("Log Cool Test", options: GUI.CreateOptions().Width(250).Height(26)))
				AdditionalTests.Run_OnlyInteresting();
			GUI.Space();
			if (GUI.CenteredButton("Log Boring Test", options: GUI.CreateOptions().Width(250).Height(26)))
				AdditionalTests.Run_All();
			GUI.Space();
			if (GUI.CenteredButton("Tests With Description", options: GUI.CreateOptions().Width(250).Height(26)))
				SimpleTests.RunAllTests();
			GUI.Space();
			if (GUI.CenteredButton("Old Tests", options: GUI.CreateOptions().Width(250).Height(26)))
				Tests.RunAllTests();
			GUI.Space();
		}

		serializedObject.ApplyModifiedProperties();
	}

	private bool[] DrawExampleText(bool isDescriptionExpanded, bool isScriptExpanded, string exampleText, string writingStyle, string description) {
		GUILayout.Space(10);

		using (GUI.Horizontal) {
			GUILayout.TextArea(exampleText, _guis_exampleHeader);
			if (GUILayout.Button("<b>C#</b>", _guis_exampleButton))
				isScriptExpanded = !isScriptExpanded;
			if (GUI.Button("<b>?</b>", _guis_exampleButton))
				isDescriptionExpanded = !isDescriptionExpanded;
		}

		if (isDescriptionExpanded)
			GUILayout.Label($"<b>?:</b> {description}", _guis_exampleDescription);

		if (isScriptExpanded)
			GUILayout.Label($"<b>C#:</b> <i>{writingStyle}</i>", _guis_exampleScript);

		return new []{isDescriptionExpanded, isScriptExpanded};
	}

}
}
