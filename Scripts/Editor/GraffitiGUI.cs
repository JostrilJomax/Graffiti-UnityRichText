using System;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.Commands;
using Graffiti;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
/// <summary> Editor GUI helper class. </summary>
public static class GraffitiGUI {

	// -------------------------------------------------------------------------------
	// Other
	// -------------------------------------------------------------------------------

	public static void Space(int size = 10) => GUILayout.Space(size);

	public static T CastTo<T>([NotNull] this SerializedProperty from) where T: ScriptableObject
		=> (T) from.objectReferenceValue;

	public static SerializedProperty FindPropertyRelative_BackingField(this SerializedProperty self, string fieldName)
		=> self.FindPropertyRelative($"<{fieldName}>k__BackingField");

	// -------------------------------------------------------------------------------
	// Draw Texture
	// -------------------------------------------------------------------------------


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


	// -------------------------------------------------------------------------------
	// Scopes
	// -------------------------------------------------------------------------------

	public static EditorGUI.IndentLevelScope      IndentLevel(int value)   => new EditorGUI.IndentLevelScope(value);
	public static EditorGUI.DisabledScope         EnabledIf(bool  enabled) => new EditorGUI.DisabledScope(enabled == false);
	public static EditorGUILayout.HorizontalScope Horizontal               => new EditorGUILayout.HorizontalScope();
	public static EditorGUILayout.VerticalScope   Vertical                 => new EditorGUILayout.VerticalScope();



	public static GroupScope Group(string label = null, GUIStyle stl = null) => new GroupScope(label, stl);
	public struct GroupScope : IDisposable {
		private static void BeginGroup() => GUILayout.BeginVertical(EditorStyles.helpBox);
		private static void EndGroup()   => GUILayout.EndVertical();
		public GroupScope(string label, GUIStyle stl = null) {
			BeginGroup();
			GUILayout.Label(label, stl ?? EditorStyles.boldLabel);
		}
		public void Dispose() => EndGroup();
	}

	public static CollapsableScope CollapsableGroup(ref bool isExpanded, string label = null) => new CollapsableScope(ref isExpanded, label);
	public struct CollapsableScope : IDisposable {
		private static void BeginGroup() => GUILayout.BeginVertical(EditorStyles.helpBox);
		private static void EndGroup()   => GUILayout.EndVertical();
		public CollapsableScope(ref bool isExpanded, string label) {
			BeginGroup();
			if (Button($"{(isExpanded ? "▲" : "▼")} {label}",
			           new GUIStyle("Button") { alignment = TextAnchor.MiddleLeft, },
			           CreateOptions().ExpandWidth(true)))
				isExpanded = !isExpanded;
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


	// -------------------------------------------------------------------------------
	// GUI Objects
	// -------------------------------------------------------------------------------

	public static void DrawGraffitiVersion() {
		string version = "Version: "           +
		                 GraffitiInfo.Version  +
		                 "  |  Version Date: " +
		                 GraffitiInfo.VersionDate.ToShortDateString();
		using (Group(version, EditorStyles.miniLabel)) {}
	}

	public static bool CenteredButton(string label, GUIStyle style = null, List<GUILayoutOption> options = null) {
		bool pressed;
		using (Horizontal) {
			GUILayout.FlexibleSpace();
			pressed = Button(label, style, options);
			GUILayout.FlexibleSpace();
		}
		return pressed;
	}

	public static bool Button(string label, List<GUILayoutOption> options = null)
		=> options == null ? GUILayout.Button(label) : GUILayout.Button(label, options.ToArray());

	public static bool Button(string label, GUIStyle style, List<GUILayoutOption> options = null)
		=> options == null
			   ? style == null ? GUILayout.Button(label) : GUILayout.Button(label, style)
			   : style == null
				   ? GUILayout.Button(label, options.ToArray())
				   : GUILayout.Button(label, style, options.ToArray());




	public static void DrawProperty([CanBeNull] SerializedProperty prop, int indentLevel = 0) {
		if (prop != null && prop.propertyType == SerializedPropertyType.Generic)
			using (new EditorGUI.IndentLevelScope(indentLevel + 1))
				EditorGUILayout.PropertyField(prop);
		else
			EditorGUILayout.PropertyField(prop);
	}


	/// <remarks> Idea: https://gist.github.com/tomkail/ba4136e6aa990f4dc94e0d39ec6a058c </remarks>
	public static void DrawScriptableObject([CanBeNull] ScriptableObject obj, bool drawScriptName = false) {
		if (obj == null)
			return;

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



	// -------------------------------------------------------------------------------
	// Extensions (GUILayoutOptions)
	// -------------------------------------------------------------------------------

	private static List<GUILayoutOption> Add_Return(this List<GUILayoutOption> self, GUILayoutOption item) {
		self.Add(item);
		return self;
	}

	public static List<GUILayoutOption> CreateOptions() => new List<GUILayoutOption>();

	public static List<GUILayoutOption> Width(this        List<GUILayoutOption> self, float width)     => self.Add_Return(GUILayout.Width(width));
	public static List<GUILayoutOption> MinWidth(this     List<GUILayoutOption> self, float minWidth)  => self.Add_Return(GUILayout.MinWidth(minWidth));
	public static List<GUILayoutOption> MaxWidth(this     List<GUILayoutOption> self, float maxWidth)  => self.Add_Return(GUILayout.MaxWidth(maxWidth));
	public static List<GUILayoutOption> Height(this       List<GUILayoutOption> self, float height)    => self.Add_Return(GUILayout.Height(height));
	public static List<GUILayoutOption> MinHeight(this    List<GUILayoutOption> self, float minHeight) => self.Add_Return(GUILayout.MinHeight(minHeight));
	public static List<GUILayoutOption> MaxHeight(this    List<GUILayoutOption> self, float maxHeight) => self.Add_Return(GUILayout.MaxHeight(maxHeight));
	public static List<GUILayoutOption> ExpandWidth(this  List<GUILayoutOption> self, bool  expand)    => self.Add_Return(GUILayout.ExpandWidth(expand));
	public static List<GUILayoutOption> ExpandHeight(this List<GUILayoutOption> self, bool  expand)    => self.Add_Return(GUILayout.ExpandHeight(expand));


	// -------------------------------------------------------------------------------
	// Extensions (Rect)
	// -------------------------------------------------------------------------------

	public class ChainableRect {
		public  float X      => Rect.x;
		public  float Y      => Rect.y;
		public  float Width  => Rect.width;
		public  float Height => Rect.height;
		public  Rect  Rect;
		private float startY;
		public  float AccumulatedHeight => Rect.y - startY + Rect.height;
		public void Initialize(Rect rect, float height) {
			startY      = rect.y;
			Rect.x      = rect.x;
			Rect.y      = rect.y;
			Rect.width  = rect.width;
			Rect.height = height;
		}
		public static implicit operator Rect(ChainableRect rect) => rect.Rect;
	}

	public static ChainableRect SetX(this         ChainableRect self, float value) { self.Rect.x = value; return self; }
	public static ChainableRect SetY(this         ChainableRect self, float value) { self.Rect.y = value; return self; }
	public static ChainableRect SetWidth(this     ChainableRect self, float value) { self.Rect.width = value; return self; }
	public static ChainableRect SetHeight(this    ChainableRect self, float value) { self.Rect.height = value; return self; }

	public static ChainableRect OffsetX(this      ChainableRect self, float value) { self.Rect.x += value; return self; }
	public static ChainableRect OffsetY(this      ChainableRect self, float value) { self.Rect.y += value; return self; }

	public static ChainableRect OffsetXByWidth(this  ChainableRect self, float additionalOffset = 0) { self.Rect.x += self.Rect.width + additionalOffset; return self; }
	public static ChainableRect OffsetYByHeight(this ChainableRect self, float additionalOffset = 0) { self.Rect.y += self.Rect.height + additionalOffset; return self; }
}
}
