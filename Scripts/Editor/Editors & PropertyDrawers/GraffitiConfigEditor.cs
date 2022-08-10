using System.Runtime.CompilerServices;
using Graffiti;
using Graffiti.Tests;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using GUI = GraffitiEditor.GraffitiGUI;


namespace GraffitiEditor {
[CustomEditor(typeof(GraffitiConfigSo))]
public class GraffitiConfigEditor : Editor {

	public const string LONG_LOREM_IPSUM_TEXT =
		"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer tellus lorem, commodo eu mauris eu, " +
		"sodales auctor eros. Suspendisse sit amet quam diam. Vestibulum sagittis magna eu lacus semper, " +
		"ac dapibus sem rhoncus. In quis interdum urna. Sed ac mattis sapien. In fringilla condimentum rutrum. " +
		"Duis auctor ipsum nec urna tincidunt, non tincidunt quam facilisis.";

	private GraffitiConfigSo   _target;
	private SerializedProperty _SP_config;
	[CanBeNull] private SerializedProperty _SP_palette;
	[CanBeNull] private ColorPaletteSo     _SO_palette;


	private void OnEnable() {
		UpdateVariables();
	}

	private void UpdateVariables() {
		_target     = target as GraffitiConfigSo;
		_SP_config  = serializedObject.FindProperty(GraffitiConfigSo.__nameof_config);
		_SP_palette = serializedObject.FindProperty(GraffitiConfigSo.__nameof_colorPalette);
		_SO_palette = _SP_palette?.CastTo<ColorPaletteSo>();
	}


	public override void OnInspectorGUI() {

		UpdateVariables();
		serializedObject.Update();

		GUI.DrawGraffitiVersion();

		using (GUI.Group("Color Palette Selection")) {
			GUI.DrawProperty(_SP_palette);

			using (GUI.IndentLevel(1))
				if (_SO_palette != null)
					GUI.DrawScriptableObject(_SO_palette);
		}

		using (GUI.Group("Settings")) {
			using (GUI.IndentLevel(1))
			using (GUI.LabelWidth(250))
				GUI.DrawChildrenProperties(_SP_config, false);
		}

		using (GUI.Group("Example text")) {
			EditorGUILayout.TextArea(LONG_LOREM_IPSUM_TEXT
			                         .Stylize(..).Green.Blue.Red.Purple
			                         .And(..1).Size(18)
			                         .And(2, 15).Italic.Bold
			                         .And(-.5f).Underline[Style.Purple.Yellow]
			                         .And(24..30).Strikethrough[Style.DefaultColor],
			                         new GUIStyle("TextArea") { richText = true, });
		}

		using (GUI.Group("Debug.Log examples")) {
			GUI.Space();
			GUI.CenteredButton("Log Cool Test", width: 250, height: 26,
			                   action: AdditionalTests.Run_OnlyInteresting);
			GUI.Space();
			GUI.CenteredButton("Log Boring Test", width: 250, height: 26,
			                   action: AdditionalTests.Run_All);
			GUI.Space();
			GUI.CenteredButton("Tests With Description", width: 250, height: 26,
			                   action: SimpleTests.RunAllTests);
			GUI.Space();
			GUI.CenteredButton("Old Tests", width: 250, height: 26,
			                   action: Tests.RunAllTests);
			GUI.Space();
		}

		serializedObject.ApplyModifiedProperties();
	}
}
}
