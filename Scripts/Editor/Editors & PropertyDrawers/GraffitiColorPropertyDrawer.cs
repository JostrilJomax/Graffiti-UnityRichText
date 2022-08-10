// TODO: This script is obviously bad and needs refactoring.
// But it does it's work so I will leave it as it is for now.

using Graffiti;
using Graffiti.Internal;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomPropertyDrawer(typeof(Color3Set))]
public class GraffitiColorPropertyDrawer : PropertyDrawer {

	private const int BORDER = 5;
	private const int SPACE  = 3;

	private int   rvHeightMultiplier;
	private float contentHeight;
	private bool  isInitialized;
	private Color mainColor;

	private Color3Set.Modifier exampleTextColor = Color3Set.Modifier.None;

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

		var valueNormal     = property.FindPropertyRelative_BackingField(Color3Set.__nameof_Value);
		var valueDark       = property.FindPropertyRelative_BackingField(Color3Set.__nameof_ValueDarker);
		var valueLight      = property.FindPropertyRelative_BackingField(Color3Set.__nameof_ValueLighter);

		var vNormalColor    = valueNormal.FindPropertyRelative(Color3.__nameof_UnityColor);
		var vNormalHex      = valueNormal.FindPropertyRelative(Color3.__nameof_Hex);
		var vNormalShortHex = valueNormal.FindPropertyRelative(Color3.__nameof_ShortHex);
		var vDarkColor      = valueDark  .FindPropertyRelative(Color3.__nameof_UnityColor);
		var vDarkHex        = valueDark  .FindPropertyRelative(Color3.__nameof_Hex);
		var vDarkShortHex   = valueDark  .FindPropertyRelative(Color3.__nameof_ShortHex);
		var vLightColor     = valueLight .FindPropertyRelative(Color3.__nameof_UnityColor);
		var vLightHex       = valueLight .FindPropertyRelative(Color3.__nameof_Hex);
		var vLightShortHex  = valueLight .FindPropertyRelative(Color3.__nameof_ShortHex);


		float rvHeightStep = contentHeight/rvHeightMultiplier;

		if (!isInitialized) {
			isInitialized = true;
			mainColor = vNormalColor.colorValue;
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
		label.text = GraffitiStylist.AddTag.Color(label.text, vNormalShortHex.stringValue);
		label.tooltip = labelText + " color.";
		EditorGUI.LabelField(rLabel, label, gLabel);
		rLabel.y += 13;
		EditorGUI.LabelField(rLabel, new GUIContent("(" + labelText + ")"), EditorStyles.miniLabel);

		// Color Selector
		float rvSmallColorBoxX = position.x + rLabel.width + 12;
		float rvSmallColorBoxWidthStep = 16;
		float rvSmallColorBoxWidth = rvSmallColorBoxWidthStep*4;

		var rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidthStep, rvHeightStep - 4);
		GraffitiGUI.DrawRect(rSmallColorBox, vLightColor.colorValue);
		rvSmallColorBoxX += rvSmallColorBoxWidthStep + 4;

		rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidthStep, rvHeightStep - 4);
		GraffitiGUI.DrawRect(rSmallColorBox, vDarkColor.colorValue);
		rvSmallColorBoxX += rvSmallColorBoxWidthStep - 12;

		rSmallColorBox = new Rect(rvSmallColorBoxX, position.y, rvSmallColorBoxWidth, rvHeightStep);
		mainColor = EditorGUI.ColorField(rSmallColorBox, vNormalColor.colorValue);
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
			exampleTextColor = Color3Set.Modifier.None;

		rect_HexBut.x += 70;
		if (GUI.Button(rect_HexBut, "Light", EditorStyles.miniButton))
			exampleTextColor = Color3Set.Modifier.Light;

		rect_HexBut.x += 70;
		if (GUI.Button(rect_HexBut, "Dark", EditorStyles.miniButton))
			exampleTextColor = Color3Set.Modifier.Dark;

		position.y += SPACE;

		var content_Hex = new GUIContent("Hex:");
		var rect_Hex = new Rect(position.x, position.y + rvHeightStep *2, 80, rvHeightStep);
		EditorGUI.LabelField(rect_Hex, content_Hex);
		rect_Hex.x += 65;
		EditorGUI.TextField(rect_Hex, vNormalHex.stringValue);
		rect_Hex.x += 70;
		EditorGUI.TextField(rect_Hex, vLightHex.stringValue);
		rect_Hex.x += 70;
		EditorGUI.TextField(rect_Hex, vDarkHex.stringValue);

		position.y += SPACE;

		content_Hex.text = "Short Hex:";
		var rShortHex = new Rect(position.x, position.y + rvHeightStep*3, 80, rvHeightStep);
		EditorGUI.LabelField(rShortHex, content_Hex);
		rShortHex.x += 65;
		EditorGUI.TextField(rShortHex, vNormalShortHex.stringValue);
		rShortHex.x += 70;
		EditorGUI.TextField(rShortHex, vLightShortHex.stringValue);
		rShortHex.x += 70;
		EditorGUI.TextField(rShortHex, vDarkShortHex.stringValue);

		position.y += SPACE;
		position.y += SPACE;
		position.y += SPACE;

		if (showExampleText) {
			var rExample = new Rect(position.x, position.y + rvHeightStep*4, position.width, rvHeightStep*(10 - 4));
			string colorForExampleText = exampleTextColor switch {
				Color3Set.Modifier.None  => vNormalShortHex.stringValue,
				Color3Set.Modifier.Dark  => vDarkShortHex.stringValue,
				Color3Set.Modifier.Light => vLightShortHex.stringValue,
				_                        => vNormalShortHex.stringValue
			};
			exampleStr = GraffitiStylist.ReplaceTag.Color(exampleStr, colorForExampleText);
			var gTextArea = new GUIStyle("TextArea");
			gTextArea.richText = true;
			exampleStr = EditorGUI.TextArea(rExample, exampleStr, gTextArea);
		}


		void UpdateColors() {
			// Update colors if a new color is selected from the color picker.
			if (mainColor != vNormalColor.colorValue) {
				vNormalColor.colorValue = mainColor;
				vNormalHex.stringValue = ColorConvertor.ToHexColor(mainColor);
				vNormalShortHex.stringValue = ColorConvertor.ToShortHexColor(mainColor);

				var newDark = new Color3();
				newDark.MakeDarker(mainColor);
				vDarkColor.colorValue = newDark.UnityColor;
				vDarkHex.stringValue = newDark.Hex;
				vDarkShortHex.stringValue = newDark.ShortHex;

				var newLight = new Color3();
				newLight.MakeLighter(mainColor);
				vLightColor.colorValue = newLight.UnityColor;
				vLightHex.stringValue = newLight.Hex;
				vLightShortHex.stringValue = newLight.ShortHex;
			}
		}
	}
}
}
