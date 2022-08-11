using Graffiti;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomPropertyDrawer(typeof(ColorPalette))]
public class ColorPalette_Drawer : PropertyDrawer {

	private const float HeightUnit = 18;
	private const int SPACE = 5;

	private readonly GraffitiGUI.ChainableRect _rect = new GraffitiGUI.ChainableRect();


	private bool _isExpanded_palette = true;



	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return _rect.AccumulatedHeight + SPACE;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

		_rect.Initialize(position, HeightUnit);

		if (GUI.Button(_rect, $"{(_isExpanded_palette ? "▲" : "▼")} {label}",
		               new GUIStyle("Button") { alignment = TextAnchor.MiddleLeft }))
			_isExpanded_palette = !_isExpanded_palette;

		if (!_isExpanded_palette)
			return;


		property.NextVisible(true);
		do {
			_rect.OffsetYByHeight(SPACE).SetHeight(EditorGUI.GetPropertyHeight(property));
			EditorGUI.PropertyField(_rect, property);
		} while (property.NextVisible(false));
	}
}
}
