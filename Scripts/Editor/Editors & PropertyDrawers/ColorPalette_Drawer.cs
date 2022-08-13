using Graffiti;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomPropertyDrawer(typeof(ColorPalette))]
public class ColorPalette_Drawer : PropertyDrawer {

	private readonly GraffitiGUI.FluentRect _rect = new GraffitiGUI.FluentRect();

	private bool _isExpanded_palette = true;


	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return _rect.AccumulatedHeight + GraffitiGUI.Padding;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		using (GraffitiGUI.IndentationFixer(out float indentDelta)) {
			_rect.Initialize(position, indentDelta);

			if (GUI.Button(_rect, $"{(_isExpanded_palette ? "▲" : "▼")} {label}",
			               new GUIStyle("Button") { alignment = TextAnchor.MiddleLeft }))
				_isExpanded_palette = !_isExpanded_palette;

			if (!_isExpanded_palette)
				return;


			property.NextVisible(true);
			do {
				_rect.OffsetYByHeight(GraffitiGUI.Padding).SetHeight(EditorGUI.GetPropertyHeight(property));
				EditorGUI.PropertyField(_rect, property);
			} while (property.NextVisible(false));
		}
	}
}
}
