using Graffiti;
using Graffiti.Tests;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomEditor(typeof(GraffitiConfigSo))]
public class GraffitiConfigEditor : Editor {

	public const string LONG_LOREM_IPSUM_TEXT =
		"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer tellus lorem, commodo eu mauris eu, " +
		"sodales auctor eros. Suspendisse sit amet quam diam. Vestibulum sagittis magna eu lacus semper, " +
		"ac dapibus sem rhoncus. In quis interdum urna. Sed ac mattis sapien. In fringilla condimentum rutrum. " +
		"Duis auctor ipsum nec urna tincidunt, non tincidunt quam facilisis.";

	private GraffitiConfigSo   _target;
	private SerializedProperty _spPaletteSO;
	private ColorPaletteSo _paletteSO;
	private SerializedProperty _spConfig;



	private void OnEnable() {
		_target = target as GraffitiConfigSo;
		_spPaletteSO = serializedObject.FindProperty("_colorPalette");
		_paletteSO = (ColorPaletteSo) _spPaletteSO.objectReferenceValue;
		_spConfig = serializedObject.FindProperty("_config");
	}


	public override void OnInspectorGUI() {

		serializedObject.Update();

		using (GraffitiUI.Group)
			GUILayout.Label(new GUIContent("Version: " + GraffitiInfo.Version +
				"  |  Version Date: " + GraffitiInfo.VersionDate.ToShortDateString()),
				EditorStyles.miniLabel);

		using (new EditorGUI.IndentLevelScope(1)) {
			using (GraffitiUI.Group) {
				EditorGUILayout.PropertyField(_spPaletteSO);
				GraffitiUI.DrawPropertyFields(_paletteSO, "Expand Color Palette");
			}

			using (GraffitiUI.TempLabelWidth(250))
				using (GraffitiUI.Group)
					EditorGUILayout.PropertyField(_spConfig);
		}

		using (GraffitiUI.Group) {
			var gTextArea = new GUIStyle("TextArea");
			gTextArea.richText = true;
			GUILayout.Label(new GUIContent("Example long text with lots of modifiers:"), EditorStyles.boldLabel);
			EditorGUILayout.TextArea(LONG_LOREM_IPSUM_TEXT
			                         .Stylize(..).Green.Blue.Red.Purple
			                         .And(..1).Size(18)
			                         .And(2, 15).Italic.Bold
			                         .And(-.5f).Underline[Style.Purple.Yellow]
			                         .And(24..30).Strikethrough[Style.DefaultColor],
				                     gTextArea);

			GUILayout.Space(10);

			if (GraffitiUI.CenteredButton(new GUIContent("Log Cool Test")))
				GraffitiTests.Run_OnlyInteresting();

			GUILayout.Space(10);

			if (GraffitiUI.CenteredButton(new GUIContent("Log Boring Test")))
				GraffitiTests.Run_All();

			GUILayout.Space(10);
		}

		serializedObject.ApplyModifiedProperties();
	}
}
}
