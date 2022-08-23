using System;
using System.Collections.Generic;
using Graffiti;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
/// <summary> Editor GUI helper class. </summary>
public static class GraffitiGUI {

    // -------------------------------------------------------------------------------
    #region Unsorted (Small methods without defined group)
    // -------------------------------------------------------------------------------

    public static void Space(int size = 10) => GUILayout.Space(size);

    public static T CastTo<T>([NotNull] this SerializedProperty from) where T : ScriptableObject => (T) from.objectReferenceValue;

    public static SerializedProperty FindPropertyRelative_BackingField(this SerializedProperty self, string fieldName)
        => self.FindPropertyRelative($"<{fieldName}>k__BackingField");


    // -------------------------------------------------------------------------------
    #endregion
    #region Scopes
    // -------------------------------------------------------------------------------

    public static EditorGUI.IndentLevelScope      IndentLevel(int value)  => new EditorGUI.IndentLevelScope(value);
    public static EditorGUI.DisabledScope         EnabledIf(bool enabled) => new EditorGUI.DisabledScope(enabled == false);
    public static EditorGUILayout.HorizontalScope Horizontal()            => new EditorGUILayout.HorizontalScope();
    public static EditorGUILayout.VerticalScope   Vertical()              => new EditorGUILayout.VerticalScope();

    public static GroupScope Group(string label = null, GUIStyle stl = null) => new GroupScope(label, stl);

    public struct GroupScope : IDisposable {

        private static void Begin() => GUILayout.BeginVertical(EditorStyles.helpBox);
        private static void End()   => GUILayout.EndVertical();

        public GroupScope(string label, GUIStyle stl = null)
        {
            Begin();
            GUILayout.Label(label, stl ?? EditorStyles.boldLabel);
        }

        public void Dispose() => End();

    }

    public static CollapsableScope CollapsableGroup(ref bool isExpanded, string label = null)
        => new CollapsableScope(ref isExpanded, label);

    public struct CollapsableScope : IDisposable {

        private static void Begin() => GUILayout.BeginVertical(EditorStyles.helpBox);
        private static void End()   => GUILayout.EndVertical();

        public CollapsableScope(ref bool isExpanded, string label)
        {
            Begin();
            label = $"<b>{label}</b>";
            if (Button(
                isExpanded ? $"▲ {label}" : $"▼ {label} ...",
                new GUIStyle("Button") { alignment = TextAnchor.MiddleLeft, richText = true },
                CreateOptions().ExpandWidth(true))) {
                isExpanded = !isExpanded;
            }
        }

        public void Dispose() => End();

    }

    public static LabelWidthScope LabelWidth(float width) => new LabelWidthScope(width);

    public struct LabelWidthScope : IDisposable {

        private static readonly Stack<float> _temporaryWidths = new Stack<float>();

        public LabelWidthScope(float width)
        {
            _temporaryWidths.Push(EditorGUIUtility.labelWidth);
            EditorGUIUtility.labelWidth = width;
        }

        public void Dispose() => EditorGUIUtility.labelWidth = _temporaryWidths.Pop();

    }

    public static FieldWidthScope FieldWidth(float width) => new FieldWidthScope(width);

    public struct FieldWidthScope : IDisposable {

        private static readonly Stack<float> _temporaryWidths = new Stack<float>();

        public FieldWidthScope(float width)
        {
            _temporaryWidths.Push(EditorGUIUtility.fieldWidth);
            EditorGUIUtility.fieldWidth = width;
        }

        public void Dispose() => EditorGUIUtility.fieldWidth = _temporaryWidths.Pop();

    }


    public static DispenserGroupScope<T> DispenserGroup<T>(params Dispenser<T>[] dispensers) => new DispenserGroupScope<T>(dispensers);

    public struct DispenserGroupScope<T> : IDisposable {

        private readonly Dispenser<T>[] _dispenser;

        public DispenserGroupScope(Dispenser<T>[] dispensers)
        {
            _dispenser = dispensers;
        }

        public void Dispose()
        {
            foreach (Dispenser<T> disp in _dispenser) disp.Reset();
        }

    }

    public class Dispenser<T> {

        private readonly T       _default;
        private readonly List<T> _list = new List<T>();
        private          int     _i    = -1;

        public Dispenser(T defaultValue)
        {
            _default = defaultValue;
        }

        public T RepeatGet => _list[_i];

        public T Get {
            get {
                if (_i >= _list.Count - 1) {
                    _list.Add(_default);
                }

                return _list[++_i];
            }
        }

        public void Set(T value) => _list[_i] = value;

        public void Reset() => _i = 0;

    }


    // -------------------------------------------------------------------------------
    #endregion
    #region Draw GUILayout
    // -------------------------------------------------------------------------------

    public static void DrawGraffitiVersion()
    {
        string version = "Version: " + GraffitiInfo.Version + "  |  Version Date: " + GraffitiInfo.VersionDate.ToShortDateString();
        using (Group(version, EditorStyles.miniLabel)) { }
    }

    public static bool CenteredButton(string label, GUIStyle style = null, List<GUILayoutOption> options = null)
    {
        bool pressed;
        using (Horizontal()) {
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



    public static void DrawProperty([CanBeNull] SerializedProperty prop, int indentLevel = 0)
    {
        if (prop != null && prop.propertyType == SerializedPropertyType.Generic) {
            using (new EditorGUI.IndentLevelScope(indentLevel + 1)) {
                EditorGUILayout.PropertyField(prop);
            }
        }
        else {
            EditorGUILayout.PropertyField(prop);
        }
    }


    /// <remarks> Idea: https://gist.github.com/tomkail/ba4136e6aa990f4dc94e0d39ec6a058c </remarks>
    public static void DrawScriptableObject([CanBeNull] ScriptableObject obj, bool drawScriptName = false)
    {
        if (obj == null) {
            return;
        }

        var serializedObject = new SerializedObject(obj);
        SerializedProperty iterator = serializedObject.GetIterator();

        iterator.NextVisible(true);
        if (drawScriptName) {
            EditorGUILayout.PropertyField(iterator);
        }

        while (iterator.NextVisible(false)) EditorGUILayout.PropertyField(iterator);
        if (GUI.changed) {
            serializedObject.ApplyModifiedProperties();
        }
    }


    /// <remarks>
    ///     Idea:
    ///     https://github.com/WooshiiDev/HierarchyDecorator/blob/master/HierarchyDecorator/Scripts/Editor/Util/SerializedPropertyUtility.cs
    /// </remarks>
    public static void DrawChildrenProperties([NotNull] SerializedProperty property, bool showChildrenRecursive = false)
    {
        SerializedProperty iterator = property.Copy();
        while (iterator.NextVisible(true)) EditorGUILayout.PropertyField(iterator, showChildrenRecursive);
    }


    // -------------------------------------------------------------------------------
    #endregion
    #region Extensions (Fluid API for creating GUILayoutOptions)
    // -------------------------------------------------------------------------------

    private static List<GUILayoutOption> Add_Return(this List<GUILayoutOption> self, GUILayoutOption item)
    {
        self.Add(item);
        return self;
    }

    public static List<GUILayoutOption> CreateOptions() => new List<GUILayoutOption>();

    public static List<GUILayoutOption> Width(this List<GUILayoutOption> self, float width) => self.Add_Return(GUILayout.Width(width));

    public static List<GUILayoutOption> MinWidth(this List<GUILayoutOption> self, float minWidth)
        => self.Add_Return(GUILayout.MinWidth(minWidth));

    public static List<GUILayoutOption> MaxWidth(this List<GUILayoutOption> self, float maxWidth)
        => self.Add_Return(GUILayout.MaxWidth(maxWidth));

    public static List<GUILayoutOption> Height(this List<GUILayoutOption> self, float height) => self.Add_Return(GUILayout.Height(height));

    public static List<GUILayoutOption> MinHeight(this List<GUILayoutOption> self, float minHeight)
        => self.Add_Return(GUILayout.MinHeight(minHeight));

    public static List<GUILayoutOption> MaxHeight(this List<GUILayoutOption> self, float maxHeight)
        => self.Add_Return(GUILayout.MaxHeight(maxHeight));

    public static List<GUILayoutOption> ExpandWidth(this List<GUILayoutOption> self, bool expand)
        => self.Add_Return(GUILayout.ExpandWidth(expand));

    public static List<GUILayoutOption> ExpandHeight(this List<GUILayoutOption> self, bool expand)
        => self.Add_Return(GUILayout.ExpandHeight(expand));


    // -------------------------------------------------------------------------------
    #endregion
    #region Extensions / Helpers / Constants (Rect)
    // -------------------------------------------------------------------------------

    public const float Padding               = 5;
    public const float IndentStepWidth       = 14;
    public const float DefaultPropertyHeight = 18;

    /// <summary> • Wrapper class for Rect struct. Contains helper functions for fluent use. </summary>
    public class FluentRect {

        private Rect  _rect;
        private float _startX;
        private float _startY;

        /// <summary>
        ///     The correct height is applied only after the first GUI render, so you can pass here a predetermined value to
        ///     avoid any single-frame artifacts
        /// </summary>
        public FluentRect(float startingHeight = 0)
        {
            _rect.height = startingHeight;
        }

        public float x      => _rect.x;
        public float y      => _rect.y;
        public float width  => _rect.width;
        public float height => _rect.height;

        public float AccumulatedHeight => _rect.y - _startY + _rect.height;
        public float AccumulatedWidth  => _rect.x           - _startX;

        public FluentRect SetX(float value)
        {
            _rect.x = value;
            return this;
        }

        public FluentRect SetY(float value)
        {
            _rect.y = value;
            return this;
        }

        public FluentRect SetWidth(float value)
        {
            _rect.width = value;
            return this;
        }

        public FluentRect SetHeight(float value)
        {
            _rect.height = value;
            return this;
        }

        public FluentRect OffsetX(float value)
        {
            _rect.x += value;
            return this;
        }

        public FluentRect OffsetY(float value)
        {
            _rect.y += value;
            return this;
        }

        public FluentRect OffsetXByWidth(float additionalOffset = 0)
        {
            _rect.x += _rect.width + additionalOffset;
            return this;
        }

        public FluentRect OffsetYByHeight(float additionalOffset = 0)
        {
            _rect.y += _rect.height + additionalOffset;
            return this;
        }

        public void Initialize(Rect propertyRect, float indentXDelta = 0, float heightUnit = DefaultPropertyHeight)
        {
            _startX = _rect.x = propertyRect.x;
            _startY = _rect.y = propertyRect.y;
            _rect.width = propertyRect.width;
            _rect.height = heightUnit;
            _rect.x += indentXDelta;
            _rect.width -= indentXDelta;
        }

        public static implicit operator Rect(FluentRect rect) => rect._rect;

    }


    // -------------------------------------------------------------------------------
    #endregion
    #region IndentationFixer for GUI
    // -------------------------------------------------------------------------------

    /// <inheritdoc cref="IndentationFixerScope"/>
    public static IndentationFixerScope IndentationFixer(out float indentDelta) => new IndentationFixerScope(out indentDelta);

    /// <summary>
    ///     • This IDisposable struct fixes problems with indentation in GUI / EditorGUI by temporary setting indentLevel
    ///     to 0 and returning distance by which objects should be shifted <br/><br/> • EditorGUI does respond to indentLevel,
    ///     but GUI does not, so adjusting their positions becomes a mess. And I need both, because some components are only
    ///     available in EditorGUI (ColorPicker) and some only in GUI (Button, for some reason there is no EditorGUI.Button
    ///     (•ิ_•ิ)? ).<br/><br/> • The reason for not using built-in EditorGUI.IndentLevelScope: I assume that it is made only
    ///     for GUILayout / EditorGUILayout, because it just does nothing.
    /// </summary>
    public struct IndentationFixerScope : IDisposable {

        private readonly int _previousIndentLevel;

        public IndentationFixerScope(out float indentXDelta)
        {
            indentXDelta = IndentStepWidth * EditorGUI.indentLevel;
            _previousIndentLevel = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
        }

        public void Dispose() => EditorGUI.indentLevel = _previousIndentLevel;

    }


    // -------------------------------------------------------------------------------
    #endregion
    // -------------------------------------------------------------------------------

}
}
