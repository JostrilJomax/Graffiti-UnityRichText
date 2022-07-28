using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
/// <summary> Editor GUI helper class. </summary>
public static class GraffitiUI {

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

	public static EditorGUI.DisabledScope EnabledIf(bool enabled) => new EditorGUI.DisabledScope(enabled == false);

	private static void BeginGroup() => GUILayout.BeginVertical(EditorStyles.helpBox);
	private static void EndGroup()   => GUILayout.EndVertical();

	public static EditorGUILayout.HorizontalScope Horizontal => new EditorGUILayout.HorizontalScope();
	public static EditorGUILayout.VerticalScope   Vertical   => new EditorGUILayout.VerticalScope();

	public static GroupScope                      Group      => new GroupScope();
	public class GroupScope : IDisposable {
		public GroupScope() => BeginGroup();
		public void Dispose() => EndGroup();
	}

	public static TemporaryLabelWidth TempLabelWidth(float width) => new TemporaryLabelWidth(width);
	public struct TemporaryLabelWidth : IDisposable {
		private static readonly Stack<float> temporaryWidths = new Stack<float>();
		public TemporaryLabelWidth(float width) {
			temporaryWidths.Push(EditorGUIUtility.labelWidth);
			EditorGUIUtility.labelWidth = width;
		}
		public void Dispose() => EditorGUIUtility.labelWidth = temporaryWidths.Pop();
	}

	public static TemporaryFieldWidth TempFieldWidth(float width) => new TemporaryFieldWidth(width);
	public struct TemporaryFieldWidth : IDisposable {
		private static readonly Stack<float> temporaryWidths = new Stack<float>();
		public TemporaryFieldWidth(float width) {
			temporaryWidths.Push(EditorGUIUtility.fieldWidth);
			EditorGUIUtility.fieldWidth = width;
		}
		public void Dispose() => EditorGUIUtility.fieldWidth = temporaryWidths.Pop();
	}

	public static bool CenteredButton(GUIContent content) {
		var pressed = false;
		using (Horizontal) {
			GUILayout.FlexibleSpace();
			pressed = GUILayout.Button(content, GUILayout.ExpandWidth(false));
			GUILayout.FlexibleSpace();
		}

		return pressed;
	}

	/// <summary> Source: https://gist.github.com/tomkail/ba4136e6aa990f4dc94e0d39ec6a058c </summary>
	public static void DrawPropertyFields(ScriptableObject so, string label = null) {
		if (so == null) return;
		var serializedObject = new SerializedObject(so);
		var prop = serializedObject.GetIterator();
		if (prop.NextVisible(true))
			do {
				if(prop.name == "m_Script") continue;
				if (label == null)
					EditorGUILayout.PropertyField(prop, true);
				else
					EditorGUILayout.PropertyField(prop, new GUIContent(label), true);
			} while (prop.NextVisible(false));
		if (GUI.changed)
			serializedObject.ApplyModifiedProperties();
	}
}
}
