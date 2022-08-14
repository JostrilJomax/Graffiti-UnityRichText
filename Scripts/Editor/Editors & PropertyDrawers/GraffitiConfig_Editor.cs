using Graffiti;
using Graffiti.Tests;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using GUI = GraffitiEditor.GraffitiGUI;


namespace GraffitiEditor {
[CustomEditor(typeof(GraffitiSettingsSo))]
public class GraffitiConfig_Editor : Editor {

	public const string LONG_LOREM_IPSUM_TEXT =
		"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer tellus lorem, commodo eu mauris eu, " +
		"sodales auctor eros. Suspendisse sit amet quam diam. Vestibulum sagittis magna eu lacus semper, " +
		"ac dapibus sem rhoncus. In quis interdum urna. Sed ac mattis sapien. In fringilla condimentum rutrum. " +
		"Duis auctor ipsum nec urna tincidunt, non tincidunt quam facilisis.";

	private GraffitiSettingsSo _target;
	private SerializedProperty _SP_config;
	[CanBeNull] private SerializedProperty _SP_palette;
	[CanBeNull] private ColorPaletteSo     _SO_palette;

	private bool _isExpanded_ColorPaletteSelection = true;
	private bool _isExpanded_Settings = true;
	private bool _isExpanded_ExampleText = true;


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
				EditorGUILayout.TextArea(LONG_LOREM_IPSUM_TEXT
				                        .Stylize(..).Green.Blue.Red.Purple
				                        .And(..1).Size(18)
				                        .And(2, 15).Italic.Bold
				                        .And(-.5f).Underline[Style.Purple.Yellow]
				                        .And(24..30).Strikethrough[Style.DefaultColor],
				                         new GUIStyle("TextArea") { richText = true, });
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
}
}
