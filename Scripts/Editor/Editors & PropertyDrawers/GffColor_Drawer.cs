﻿// TODO: This script is obviously bad and needs refactoring.
// But it does it's work so I will leave it as it is for now.

using System;
using Graffiti;
using Graffiti.Internal;
using UnityEditor;
using UnityEngine;

namespace GraffitiEditor {
[CustomPropertyDrawer(typeof(GffColor))]
public class GffColor_Drawer : PropertyDrawer {

    private const int FirstRowElementWidth     = 140;
    private const int ButtonWidth              = 120;
    private const int BrightnessSelectionWidth = 360;
    private const int WarningLabelWidth        = 460;

    private static bool _isExpanded_showMore;

    private readonly GraffitiGUI.FluentRect _rect = new GraffitiGUI.FluentRect();

    private int _colorModifier_exampleText;

    private string _string_exampleText = GraffitiSettingsSo_Editor.LONG_LOREM_IPSUM_TEXT;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => _rect.AccumulatedHeight + GraffitiGUI.Padding;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _rect.Initialize(position);

        SerializedProperty field_UnityColor = property.FindPropertyRelative_BackingField(GffColor._nameof_UnityColor);
        SerializedProperty field_ShortHex = property.FindPropertyRelative_BackingField(GffColor._nameof_ShortHex);


        // Color Name (Colored Label)
        GUI.TextField(
            _rect.SetWidth(FirstRowElementWidth),
            GraffitiStylist.AddTag.Color(label.text, field_ShortHex.stringValue),
            new GUIStyle("TextField") { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, richText = true });


        // Color Selector
        UpdateColors(EditorGUI.ColorField(_rect.OffsetXByWidth(GraffitiGUI.Padding), field_UnityColor.colorValue));


        // Show More/Less Button
        _rect.OffsetXByWidth(GraffitiGUI.Padding).SetWidth(Mathf.Max(position.width - _rect.AccumulatedWidth, FirstRowElementWidth));
        if (GUI.Button(_rect, _isExpanded_showMore ? "Show Less" : "Show More")) {
            _isExpanded_showMore = !_isExpanded_showMore;
        }

        if (!_isExpanded_showMore) {
            return;
        }


        // Brightness Selection
        _rect.SetX(position.x).OffsetYByHeight(GraffitiGUI.Padding).SetWidth(BrightnessSelectionWidth);
        _colorModifier_exampleText = GUI.SelectionGrid(_rect, _colorModifier_exampleText, new[] { "Normal", "Lighter", "Darker" }, 3);

        string currentShortHexColorValue = _colorModifier_exampleText switch {
            0 => field_ShortHex.stringValue,
            1 => new GffColor(field_UnityColor.colorValue, field_ShortHex.stringValue).Clone().MakeLighter().ShortHex,
            2 => new GffColor(field_UnityColor.colorValue, field_ShortHex.stringValue).Clone().MakeDarker().ShortHex,
            _ => throw new ArgumentOutOfRangeException(),
        };


        // Short Hex Value Text
        _rect.SetX(position.x).OffsetYByHeight(GraffitiGUI.Padding).SetWidth(ButtonWidth);
        GUI.Label(_rect, "Current Short Hex:");
        GUI.TextField(_rect.OffsetXByWidth(GraffitiGUI.Padding), currentShortHexColorValue);


        // Warning
        if (_colorModifier_exampleText != 0 && field_ShortHex.stringValue == currentShortHexColorValue) {
            _rect.SetX(position.x).SetWidth(WarningLabelWidth).OffsetYByHeight();
            GUI.Label(_rect, "Can't change color's luminosity, color is already too bright/dim !");
        }


        // Example Text
        _rect.SetX(position.x).OffsetYByHeight(GraffitiGUI.Padding).SetWidth(position.width)
             .SetHeight(GraffitiGUI.DefaultPropertyHeight * 6);
        _string_exampleText = GraffitiStylist.ReplaceTag.Color(_string_exampleText, currentShortHexColorValue);
        _string_exampleText = EditorGUI.TextArea(
            _rect, _string_exampleText,
            new GUIStyle("TextArea") { richText = true });


        void UpdateColors(Color color)
        {
            if (color == field_UnityColor.colorValue) {
                return;
            }

            field_UnityColor.colorValue = color;
            field_ShortHex.stringValue = ColorConvertor.ToShortHexColor(color);
        }
    }

}
}
