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

	private bool  _isInitialized;
	private int   _heightMultiplier;
	private float _contentHeight;
	private Color _mainColor;

	private bool _showMore;

	private bool _showExampleText = true;
	private string _exampleText = GraffitiConfigEditor.LONG_LOREM_IPSUM_TEXT;
	private GffColor.Modifier _exampleTextColorModifier = GffColor.Modifier.None;

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		if (_showMore)
			_heightMultiplier = _showExampleText ? 10 : 4;
		else
			_heightMultiplier = 1;
		_contentHeight = base.GetPropertyHeight(property, label)*_heightMultiplier;

		if (_showMore)
			return _contentHeight + BORDER*2 + SPACE*7;
		return _contentHeight + BORDER*2 + SPACE*2;
	}


	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		SerializedProperty gffColor_UnityColor = property.FindPropertyRelative_BackingField(GffColor.__nameof_UnityColor);
		SerializedProperty gffColor_ShortHex   = property.FindPropertyRelative_BackingField(GffColor.__nameof_ShortHex);

		var gffColor = new GffColor(gffColor_UnityColor.colorValue, gffColor_ShortHex.stringValue);

		float heightStep = _contentHeight /_heightMultiplier;

		if (!_isInitialized) {
			_isInitialized = true;
			_mainColor = gffColor_UnityColor.colorValue;
		}


		// Color Name
		var rect_ColoredHeaderText = new Rect(position.x, position.y, 140, heightStep);
		EditorGUI.TextField(rect_ColoredHeaderText,
		                    GraffitiStylist.AddTag.Color(label.text, gffColor_ShortHex.stringValue),
		                    new GUIStyle("TextField") {
			                    alignment = TextAnchor.MiddleCenter,
			                    fontStyle = FontStyle.Bold,
			                    richText  = true,
		                    });


		// Color Selector
		rect_ColoredHeaderText.x += rect_ColoredHeaderText.width;
		_mainColor = EditorGUI.ColorField(rect_ColoredHeaderText, gffColor_UnityColor.colorValue);
		UpdateColors();


		// More/Less Button
		rect_ColoredHeaderText.x += rect_ColoredHeaderText.width + 15;
		if (GUI.Button(rect_ColoredHeaderText, _showMore ? "Show Less" : "Show More"))
			_showMore = !_showMore;

		if (!_showMore)
			return;

		// Hex Buttons
		var rect_HexBut = new Rect(position.x + 30, position.y + heightStep + 15, 120, heightStep);

		if (GUI.Button(rect_HexBut, "Show Normal", EditorStyles.miniButton))
			_exampleTextColorModifier = GffColor.Modifier.None;

		rect_HexBut.x += 125;
		if (GUI.Button(rect_HexBut, "Show Lighter", EditorStyles.miniButton))
			_exampleTextColorModifier = GffColor.Modifier.Light;

		rect_HexBut.x += 125;
		if (GUI.Button(rect_HexBut, "Show Darker", EditorStyles.miniButton))
			_exampleTextColorModifier = GffColor.Modifier.Dark;

		string currentShortHexForExampleText = _exampleTextColorModifier switch {
			GffColor.Modifier.None  => gffColor.ShortHex,
			GffColor.Modifier.Dark  => gffColor.Clone().MakeDarker().ShortHex,
			GffColor.Modifier.Light => gffColor.Clone().MakeLighter().ShortHex,
			_                       => gffColor.ShortHex,
		};


		// Short Hex Value Text
		var rect_ShortHexText = new Rect(position.x, rect_HexBut.y + heightStep + 5, 110, heightStep);
		EditorGUI.LabelField(rect_ShortHexText, "Current Short Hex:");
		rect_ShortHexText.x += 125;
		EditorGUI.TextField(rect_ShortHexText, currentShortHexForExampleText);


		// Warning
		if (_exampleTextColorModifier != GffColor.Modifier.None && gffColor.ShortHex == currentShortHexForExampleText) {
			EditorGUI.LabelField(new Rect(position.x, rect_HexBut.y + heightStep + heightStep + 5, 460, heightStep),
			                     "Can't change color's luminosity, color is already too bright/dim !");
		}



		if (!_showExampleText)
			return;


		// Example Text
		var rect_ExampleText = new Rect(position.x, rect_ShortHexText.y + heightStep + 20, position.width, heightStep *(10 - 4));
		_exampleText = GraffitiStylist.ReplaceTag.Color(_exampleText, currentShortHexForExampleText);
		_exampleText = EditorGUI.TextArea(rect_ExampleText, _exampleText,
		                                  new GUIStyle("TextArea") {
			                                  richText = true,
		                                  });


		void UpdateColors() {
			if (_mainColor == gffColor_UnityColor.colorValue)
				return;

			gffColor_UnityColor.colorValue = _mainColor;
			gffColor_ShortHex.stringValue = ColorConvertor.ToShortHexColor(_mainColor);
		}
	}
}
}
