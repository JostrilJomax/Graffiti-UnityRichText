using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary>
/// This class contains methods for converting colors to different formats.
/// </summary>
internal static class ColorConvertor {

	// To Unity Color //////////////////////////////////

	internal static Color ToUnityColor([NotNull] string color) {
		if (color[0] == '#') {
			if (color.Length < 4 || color.Length > 9)
				return default;
			switch (color.Length) {
				case 4: return ToUnityColorFromShortHexColor(color);
				case 5: return ToUnityColorFromShortHexColor(color);
				case 6: return default;
				case 7: return ToUnityColorFromHexColor(color);
				case 8: return default;
				case 9: return ToUnityColorFromHexColor(color);
			}
		} else if (UnityColors.TryGetColor(color, out var res))
			return ToUnityColorFromShortHexColor(res.ShortHex);
		return default;
	}

	internal static Color ToUnityColorFromHexColor([NotNull] string str) {
		if (str[0] != '#') return default;

		bool isRRGGBBAA = str.Length == 9;
		if (!isRRGGBBAA && str.Length != 7) return default;

		float r = Convert.ToInt32(str.Substring(1, 2), 16)/255f;
		float g = Convert.ToInt32(str.Substring(3, 2), 16)/255f;
		float b = Convert.ToInt32(str.Substring(5, 2), 16)/255f;
		float a = 1f;
		if (isRRGGBBAA)
			a = Convert.ToInt32(str.Substring(7, 2), 16)/255f;
		return new Color(r, g, b, a);
	}

	internal static Color ToUnityColorFromShortHexColor([NotNull] string str) {
		if (str[0] != '#') return default;

		bool isRGBA = str.Length == 5;
		if (!isRGBA && str.Length != 4) return default;

		float r = 1f;
		float g = 1f;
		float b = 1f;
		float a = 1f;

		if (str[1] != 'f' && str[1] != 'F')
			r = Convert.ToInt32(str[1].ToString(), 16)*16/255f;
		if (str[2] != 'f' && str[2] != 'F')
			g = Convert.ToInt32(str[2].ToString(), 16)*16/255f;
		if (str[3] != 'f' && str[3] != 'F')
			b = Convert.ToInt32(str[3].ToString(), 16)*16/255f;
		if (isRGBA && str[4] != 'f' && str[4] != 'F')
			a = Convert.ToInt32(str[4].ToString(), 16)*16/255f;

		return new Color(r, g, b, a);
	}


	// To #RRGGBB format //////////////////////////////////

	internal static string ToHexColor(Color color) {
		return ToHexColor(color.r, color.g, color.b);
	}

	internal static string ToHexColor(float r, float g, float b) {
		return ToHexColor(
		       unchecked((byte) (r * 255f)),
		       unchecked((byte) (g * 255f)),
		       unchecked((byte) (b * 255f)));
	}

	internal static string ToHexColor(int r, int g, int b) {
		return ToHexColor(
		       unchecked((byte) r),
		       unchecked((byte) g),
		       unchecked((byte) b));
	}

	internal static string ToHexColor(byte r, byte g, byte b) {
		return $"#{r:X2}{g:X2}{b:X2}";
	}


	// To #RGB format //////////////////////////////////

	internal static string ToShortHexColor([NotNull] string str) {
		if (str[0] == '#' && str.Length == 7) {
			string result = "#";
			//TODO: It would be great to use Span<> here
			result += $"{RoundHex(Convert.ToByte(str.Substring(1, 2), 16)):X}";
			result += $"{RoundHex(Convert.ToByte(str.Substring(3, 2), 16)):X}";
			result += $"{RoundHex(Convert.ToByte(str.Substring(5, 2), 16)):X}";
			return result;
		}
		return str;
	}

	internal static string ToShortHexColor(Color color) {
		return ToShortHexColor(color.r, color.g, color.b);
	}

	internal static string ToShortHexColor(float r, float g, float b) {
		return ToShortHexColor(
			unchecked((byte) (r * 255f)),
			unchecked((byte) (g * 255f)),
			unchecked((byte) (b * 255f)));
	}

	internal static string ToShortHexColor(int r, int g, int b) {
		return ToShortHexColor(
			unchecked((byte) r),
			unchecked((byte) g),
			unchecked((byte) b));
	}

	internal static string ToShortHexColor(byte r, byte g, byte b) {
		return $"#{RoundHex(r):X}"+
				$"{RoundHex(g):X}"+
				$"{RoundHex(b):X}";
	}

	private static byte RoundHex(byte value) {
		if (value % 16 == 0) // 10, A0, F0, etc.
			return (byte)(value/16);
		if (value >= 240) // F0 to FF
			return 15; // F

		float f = (float)value / 16f;
		float decimalPart = f - (float) Math.Truncate(f);
		float addThis = (1 - decimalPart) * 16;
		return (byte) ((value + addThis)/16);
	}
}
}
