using System;
using UnityEngine;

namespace Graffiti.Internal {
[Serializable]
internal struct StringStyleColor {

	[SerializeField] private string    _strColor;
	[SerializeField] private ColorType _graffitiColorType;

	[SerializeField] private GffColor.Modifier _modifier;

	internal bool HasColor { get; private set; }


	/// <remarks> Only one color parameter will be applied (+modificator) </remarks>
	internal StringStyleColor __SetColor(ColorType?         colorType,
	                                     string             strColor,
	                                     GffColor.Modifier modifier)
	{
		_modifier = modifier;
		if (colorType != null || strColor != null) {
			_graffitiColorType = colorType ?? _graffitiColorType;
			_strColor          = strColor;
			HasColor           = true;
		}
		return this;
	}

	internal string GetColorHexValue() => _strColor ?? ModifyColor(_graffitiColorType).GetHexValue();

	internal Color GetUnityColor() {
		if (_strColor != null)
			return ColorConvertor.ToUnityColor(_strColor);
		return ColorConvertor.ToUnityColorFromShortHexColor(ModifyColor(_graffitiColorType).ShortHex);
	}

	private GffColor ModifyColor(ColorType color) {
		switch (_modifier) {
			default:
			case GffColor.Modifier.None:  return GraffitiProperties.Palette.FindColor(color);
			case GffColor.Modifier.Dark:  return GraffitiProperties.Palette.FindColor(color).Clone().MakeDarker();
			case GffColor.Modifier.Light: return GraffitiProperties.Palette.FindColor(color).Clone().MakeLighter();
		}
	}
}
}
