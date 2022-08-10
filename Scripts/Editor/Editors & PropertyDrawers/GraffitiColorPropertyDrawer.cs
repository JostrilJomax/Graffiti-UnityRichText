// TODO: This script is obviously bad and needs refactoring.
// But it does it's work so I will leave it as it is for now.

using Graffiti;
using Graffiti.Internal;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomPropertyDrawer(typeof(GffColor))]
public class GraffitiColorPropertyDrawer : PropertyDrawer {

	private const int BORDER = 5;
	private const int SPACE  = 3;

	private int   rvHeightMultiplier;
	private float contentHeight;
	private bool  isInitialized;
	private Color mainColor;

	private GffColor.Modifier exampleTextColor = GffColor.Modifier.None;

	private bool showFull;

	private bool showExampleText = true;
	private string exampleStr = GraffitiConfigEditor.LONG_LOREM_IPSUM_TEXT;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		if (showFull)
			rvHeightMultiplier = (showExampleText ? 10 : 4);
		else
			rvHeightMultiplier = 1;
		contentHeight = base.GetPropertyHeight(property, label)*rvHeightMultiplier;

		if (showFull)
			return contentHeight + BORDER*2 + SPACE*7;
		return contentHeight + BORDER*2 + SPACE*2;
	}


	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		var colorNormalUnityColor = property.FindPropertyRelative_BackingField(GffColor.__nameof_UnityColor);
		var colorNormalShortHex   = property.FindPropertyRelative_BackingField(GffColor.__nameof_ShortHex);


		float rvHeightStep = contentHeight/rvHeightMultiplier;

		if (!isInitialized) {
			isInitialized = true;
			mainColor = colorNormalUnityColor.colorValue;
		}


		//GUI.Box(position, GUIContent.none);
		position.y += BORDER;

		// Color Name
		var gLabel = new GUIStyle("TextField") {
			fontStyle = FontStyle.Bold,
			richText = true,
			alignment = TextAnchor.MiddleCenter,
		};
		var rLabel = new Rect(position.x, position.y, 24*3, rvHeightStep);
		string labelText = label.text;
		label.text = GraffitiStylist.AddTag.Color(label.text, colorNormalShortHex.stringValue);
		label.tooltip = labelText + " color.";
		EditorGUI.LabelField(rLabel, label, gLabel);
		rLabel.y += 13;
		EditorGUI.LabelField(rLabel, new GUIContent("(" + labelText + ")"), EditorStyles.miniLabel);

		// Color Selector
		float rvSmallColorBoxX = position.x + rLabel.width + 12;
		float rvSmallColorBoxWidthStep = 16;
		float rvSmallColorBoxWidth = rvSmallColorBoxWidthStep*4;

		var rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidthStep, rvHeightStep - 4);
		// GraffitiGUI.DrawRect(rSmallColorBox, colorLightUnityColor.colorValue);
		rvSmallColorBoxX += rvSmallColorBoxWidthStep + 4;

		rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidthStep, rvHeightStep - 4);
		// GraffitiGUI.DrawRect(rSmallColorBox, colorDarkUnityColor.colorValue);
		rvSmallColorBoxX += rvSmallColorBoxWidthStep - 12;

		rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidth, rvHeightStep);
		mainColor = EditorGUI.ColorField(rSmallColorBox, colorNormalUnityColor.colorValue);
		UpdateColors();


		// More/Less Button
		var lMoreLessBut = new GUIContent(showFull ? "Less." : "More?");
		var rMoreLessBut = new Rect(rvSmallColorBoxX + rvSmallColorBoxWidth + 12, position.y,
			position.width - rvSmallColorBoxX - rvSmallColorBoxWidth - 12, rvHeightStep);
		if (GUI.Button(rMoreLessBut, lMoreLessBut))
			showFull = !showFull;

		if (!showFull)
			return;

		position.y += SPACE;
		position.y += SPACE;
		position.y += SPACE;
		position.y += SPACE;

		// Hex values
		var rect_HexBut = new Rect(position.x, position.y + rvHeightStep, 60, rvHeightStep);

		rect_HexBut.x += 80;
		if (GUI.Button(rect_HexBut, "Normal", EditorStyles.miniButton))
			exampleTextColor = GffColor.Modifier.None;

		rect_HexBut.x += 70;
		if (GUI.Button(rect_HexBut, "Light", EditorStyles.miniButton))
			exampleTextColor = GffColor.Modifier.Light;

		rect_HexBut.x += 70;
		if (GUI.Button(rect_HexBut, "Dark", EditorStyles.miniButton))
			exampleTextColor = GffColor.Modifier.Dark;

		position.y += SPACE;


		position.y += SPACE;

		var content_Hex = new GUIContent("Short Hex:");
		var rShortHex   = new Rect(position.x, position.y + rvHeightStep *3, 80, rvHeightStep);
		EditorGUI.LabelField(rShortHex, content_Hex);
		rShortHex.x += 65;
		EditorGUI.TextField(rShortHex, colorNormalShortHex.stringValue);
		// rShortHex.x += 70;
		// EditorGUI.TextField(rShortHex, colorLightShortHex.stringValue);
		// rShortHex.x += 70;
		// EditorGUI.TextField(rShortHex, colorDarkShortHex.stringValue);

		position.y += SPACE;
		position.y += SPACE;
		position.y += SPACE;

		if (showExampleText) {
			var rExample = new Rect(position.x, position.y + rvHeightStep*4, position.width, rvHeightStep*(10 - 4));
			string colorForExampleText = exampleTextColor switch {
				GffColor.Modifier.None  => colorNormalShortHex.stringValue,
				GffColor.Modifier.Dark  => colorNormalShortHex.stringValue,
				GffColor.Modifier.Light => colorNormalShortHex.stringValue,
				_                       => colorNormalShortHex.stringValue
			};
			exampleStr = GraffitiStylist.ReplaceTag.Color(exampleStr, colorForExampleText);
			var gTextArea = new GUIStyle("TextArea");
			gTextArea.richText = true;
			exampleStr = EditorGUI.TextArea(rExample, exampleStr, gTextArea);
		}


		void UpdateColors() {
			// Update colors if a new color is selected from the color picker.
			if (mainColor != colorNormalUnityColor.colorValue) {
				colorNormalUnityColor.colorValue = mainColor;
				// vNormalHex.stringValue = ColorConvertor.ToHexColor(mainColor);
				colorNormalShortHex.stringValue = ColorConvertor.ToShortHexColor(mainColor);

				// var newDark = new GffColor(colorNormalUnityColor);
				// newDark.MakeDarker();
				// colorDarkUnityColor.colorValue = newDark.UnityColor;
				// vDarkHex.stringValue = newDark.Hex;
				// colorDarkShortHex.stringValue = newDark.ShortHex;
				//
				// var newLight = new GffColor(colorNormalUnityColor);
				// newLight.MakeLighter();
				// colorLightUnityColor.colorValue = newLight.UnityColor;
				// vLightHex.stringValue = newLight.Hex;
				// colorLightShortHex.stringValue = newLight.ShortHex;
			}
		}
	}
}
}
