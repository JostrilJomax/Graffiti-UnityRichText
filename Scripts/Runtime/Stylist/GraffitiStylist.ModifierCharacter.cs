using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Graffiti.Internal {
internal static partial class GraffitiStylist {

	/// <summary>
	/// Class that contains methods to insert modifier character(s) to StringBuilder
	/// with/without additional conditions (like gradient).
	/// </summary>
	// TODO: I am not sure that it is a good practice to name class like a method. But otherwise I would have to add this prefix to all methods.
	internal static class ModifierCharacter {
		private const char Brick             = '█';
		private const char SmokingHot        = '̾';
		private const char Strikethrough     = '̶';
		private const char WavyStrikethrough = '̴';
		private const char Slash             = '̷';
		private const char HighSlash         = '̸';
		private const char Underline         = '̲';
		private const char DoubleUnderline   = '̳';
		private const char Dotted            = '̤';
		private const char Wheel             = '̥';

		internal static char GetModifierCharacter(ModifierCharacterType modifierType) => modifierType switch {
			ModifierCharacterType.SmokingHot        => SmokingHot,
			ModifierCharacterType.Strikethrough     => Strikethrough,
			ModifierCharacterType.WavyStrikethrough => WavyStrikethrough,
			ModifierCharacterType.Slash             => Slash,
			ModifierCharacterType.HighSlash         => HighSlash,
			ModifierCharacterType.Underline         => Underline,
			ModifierCharacterType.DoubleUnderline   => DoubleUnderline,
			ModifierCharacterType.Dotted            => Dotted,
			ModifierCharacterType.Wheel             => Wheel,
			_                              => Brick,
		};


		internal static void InsertChar(StringBuilder                     self,
		                              Gradient                            gradient,
		                              (char[] chars, Gradient gradient)[] modifierPair,
		                              int                                 startIndex,
		                              int                                 endIndex,
		                              int                                 gradientSimulationStart,
		                              int                                 gradientSimulationEnd,
		                              char[]                              ignoredSymbols) {
			var scope = GetScope(self, startIndex, endIndex);
			self.Clear();
			self.Append(scope.before);
			AppendGradient(self, scope.between, gradient, modifierPair,
				gradientSimulationStart, gradientSimulationEnd,
				ignoredSymbols);
			self.Append(scope.after);
		}

		private static void AppendGradient(StringBuilder                       sb,
		                                   char[]                              chars,
		                                   Gradient                            charsGradient,
		                                   (char[] chars, Gradient gradient)[] modifierPairs,
		                                   int                                 gradientSimulationStart,
		                                   int                                 gradientSimulationEnd,
		                                   char[]                              ignoredSymbols) {
			int gradientLength = gradientSimulationEnd - gradientSimulationStart + 1;
			int startIndex = sb.Length;

			for (int i = 0; i < chars.Length; i++) {
				if (IsIgnoredSymbol(chars[i], ignoredSymbols)) {
					sb.Append(chars[i]);
					continue;
				}

				var tempChars = new List<char> { chars[i] };

				foreach (var pair in modifierPairs) {
					Add(tempChars,
						pair.gradient != null
							? Colorize(pair.chars, pair.gradient, GetGradientTime(i), gradientLength)
							: pair.chars);
				}

				sb.Append(charsGradient != null
					? Colorize(tempChars.ToArray(), charsGradient, GetGradientTime(i), gradientLength)
					: tempChars.ToArray());

				int GetGradientTime(int offset) => startIndex - gradientSimulationStart + offset;
				static void Add(List<char> list, char[] chars) {
					foreach (char ch in chars) list.Add(ch);
				}
			}
		}
	}
}
}
