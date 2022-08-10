using System;
using System.Collections.Generic;
using Graffiti;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
/// <summary> Editor GUI helper class. </summary>
public static class GraffitiGUI {

	// Other =====================================================================================================================================================

	public static void Space(int size = 10) => GUILayout.Space(size);

	public static T CastTo<T>([NotNull] this SerializedProperty from) where T: ScriptableObject
		=> (T) from.objectReferenceValue;

	public static SerializedProperty FindPropertyRelative_BackingField(this SerializedProperty self, string fieldName)
		=> self.FindPropertyRelative($"<{fieldName}>k__BackingField");


	// Draw Texture ==============================================================================================================================================

	public static Color guiBgColor;
	public static void  SaveGuiBgColor()   => guiBgColor = GUI.backgroundColor;
	public static void  RevertGuiBgColor() => GUI.backgroundColor = guiBgColor;

	public static void SetGuiBgColor(Color color) {
		SaveGuiBgColor();
		GUI.backgroundColor = color;
	}

	private static readonly Texture2D backgroundTexture = Texture2D.whiteTexture;
	private static readonly GUIStyle textureStyle =
		new GUIStyle {normal = new GUIStyleState {background = backgroundTexture}};

	public static void DrawRect(Rect position, Color color, GUIContent content = null) {
		var backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = color;
		GUI.Box(position, content ?? GUIContent.none, textureStyle);
		GUI.backgroundColor = backgroundColor;
	}

	public static void LayoutBox(Color color, GUIContent content = null) {
		var backgroundColor = GUI.backgroundColor;
		GUI.backgroundColor = color;
		GUILayout.Box(content ?? GUIContent.none, textureStyle);
		GUI.backgroundColor = backgroundColor;
	}


	// Scopes ====================================================================================================================================================

	public static EditorGUI.IndentLevelScope      IndentLevel(int value)   => new EditorGUI.IndentLevelScope(value);
	public static EditorGUI.DisabledScope         EnabledIf(bool  enabled) => new EditorGUI.DisabledScope(enabled == false);
	public static EditorGUILayout.HorizontalScope Horizontal               => new EditorGUILayout.HorizontalScope();
	public static EditorGUILayout.VerticalScope   Vertical                 => new EditorGUILayout.VerticalScope();



	public static GroupScope Group(string label = null, GUIStyle stl = null) => new GroupScope(label, stl);
	public struct GroupScope : IDisposable {
		private static void BeginGroup() => GUILayout.BeginVertical(EditorStyles.helpBox);
		private static void EndGroup()   => GUILayout.EndVertical();
		public GroupScope(string label, GUIStyle stl = null) {
			using (IndentLevel(1))
				BeginGroup();
			if (label != null)
				GUILayout.Label(label, stl ?? EditorStyles.boldLabel);
		}
		public void Dispose() => EndGroup();
	}


	public static LabelWidthScope LabelWidth(float width) => new LabelWidthScope(width);
	public struct LabelWidthScope : IDisposable {
		private static readonly Stack<float> _temporaryWidths = new Stack<float>();
		public LabelWidthScope(float width) {
			_temporaryWidths.Push(EditorGUIUtility.labelWidth);
			EditorGUIUtility.labelWidth = width;
		}
		public void Dispose() => EditorGUIUtility.labelWidth = _temporaryWidths.Pop();
	}

	public static FieldWidthScope FieldWidth(float width) => new FieldWidthScope(width);
	public struct FieldWidthScope : IDisposable {
		private static readonly Stack<float> _temporaryWidths = new Stack<float>();
		public FieldWidthScope(float width) {
			_temporaryWidths.Push(EditorGUIUtility.fieldWidth);
			EditorGUIUtility.fieldWidth = width;
		}
		public void Dispose() => EditorGUIUtility.fieldWidth = _temporaryWidths.Pop();
	}


	// GUI Objects ===============================================================================================================================================

	public static void DrawGraffitiVersion() {
		string version = "Version: "           +
		                 GraffitiInfo.Version  +
		                 "  |  Version Date: " +
		                 GraffitiInfo.VersionDate.ToShortDateString();
		using (Group(version, EditorStyles.miniLabel)) {}
	}

	public static bool CenteredButton(string label, Action action, int width = -1, int height = -1) {
		bool pressed = CenteredButton(label, width, height);
		if (pressed)
			action?.Invoke();
		return pressed;
	}

	public static bool CenteredButton(string label, int width = -1, int height = -1) {
		bool pressed;
		using (Horizontal) {
			GUILayout.FlexibleSpace();
			pressed = Button(label, width, height);
			GUILayout.FlexibleSpace();
		}
		return pressed;
	}


	public static bool Button(string label, int width = -1, int height = -1) {
		return GUILayout.Button(label, CreateOptions(false, width, height));

		static GUILayoutOption[] CreateOptions(bool expandWidth, int width, int height) {
			var options = new GUILayoutOption[3];
			options[0] = GUILayout.ExpandWidth(expandWidth);
			options[1] = width  > -1 ? GUILayout.Width(width) : options[0];
			options[2] = height > -1 ? GUILayout.Height(height) : options[0];
			return options;
		}
	}



	public static void DrawProperty([CanBeNull] SerializedProperty prop, int indentLevel = 0) {
		if (prop != null && prop.propertyType == SerializedPropertyType.Generic)
			using (new EditorGUI.IndentLevelScope(indentLevel + 1))
				EditorGUILayout.PropertyField(prop);
		else
			EditorGUILayout.PropertyField(prop);
	}


	/// <remarks> Idea: https://gist.github.com/tomkail/ba4136e6aa990f4dc94e0d39ec6a058c </remarks>
	public static void DrawScriptableObject([NotNull] ScriptableObject obj, bool drawScriptName = false) {
		var serializedObject = new SerializedObject(obj);
		var iterator         = serializedObject.GetIterator();

		iterator.NextVisible(true);
		if (drawScriptName)
			EditorGUILayout.PropertyField(iterator);
		while(iterator.NextVisible(false)) {
			EditorGUILayout.PropertyField(iterator);
		}
		if (GUI.changed)
			serializedObject.ApplyModifiedProperties();
	}


	/// <remarks> Idea: https://github.com/WooshiiDev/HierarchyDecorator/blob/master/HierarchyDecorator/Scripts/Editor/Util/SerializedPropertyUtility.cs </remarks>
	public static void DrawChildrenProperties([NotNull] SerializedProperty property, bool showChildrenRecursive = false) {
		var iterator = property.Copy();
		while (iterator.NextVisible(true)) {
			EditorGUILayout.PropertyField(iterator, showChildrenRecursive);
		}
	}
}
}
