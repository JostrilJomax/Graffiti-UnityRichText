using System;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary>
/// • This class and it's subclasses are designed to perform certain actions
/// (such as insertion, replacement) to char/char[]/string/StringBuilder.
/// <para/>
/// • This class also contains special symbols, that I call "modifier characters" that
/// allow to add to a char Underline/Strikethrough/etc.
/// </summary>
/// <remarks>
/// See <see cref="GraffitiStylist.AddTag"/><br/>
/// See <see cref="GraffitiStylist.ReplaceTag"/><br/>
/// See <see cref="ModifierCharacter"/><br/>
/// </remarks>
internal static partial class GraffitiStylist {

	#region ,,',,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,',, Shared Methods Between Stylists ,,',,,,,,,,,,,,,,,,,,,,,,,,,,,',,

	private static readonly char[] IGNORED_SYMBOLS = {' ', '\n'}; // todo: do I need to add symbols like '\r'?

	internal static bool IsOnlySeparators(string str) {
		foreach (var ch in str) {
			if (!IsIgnoredSymbol(ch))
				return false;
		}
		return true;
	}

	private static bool IsIgnoredSymbol(char me, char[] ignoredSymbols = null) {
		ignoredSymbols ??= IGNORED_SYMBOLS;
		foreach (var ignore in ignoredSymbols)
			if (me == ignore)
				return true;
		return false;
	}

	/// <remarks> Including the last index. </remarks>
	private static (char[] before, char[] between, char[] after) GetScope(StringBuilder sb, int startIndex, int endIndex) {
		var before = startIndex - 1 <= 0
			? Array.Empty<char>()
			: CharsFromTo(sb, 0, startIndex - 1);

		var between = CharsFromTo(sb, startIndex, endIndex);

		var after = endIndex + 1 >= sb.Length
			? Array.Empty<char>()
			: CharsFromTo(sb, endIndex + 1, sb.Length - 1);
		return (before, between, after);
	}

	/// <remarks> Including the last index. </remarks>
	private static char[] CharsFromTo(StringBuilder sb, int fromIndex, int toIndex) {
		var array = new char[toIndex - fromIndex  + 1];
		int i = 0, j = fromIndex;
		for (; i < array.Length; i++) {
			array[i] = sb[j];
			j++;
		}
		return array;
	}

	#endregion ''.'''''''''''''''''''''''''''''.'' Shared Methods Between Stylists ''.'''''''''''''''''''''''''''.''



	public static void AddGradient(StringBuilder  self,
                               [NotNull] Gradient gradient,
                               int                startIndex,
                               int                endIndex,
                               int                gradientSimulationStart,
                               int                gradientSimulationEnd) {
		var scope = GetScope(self, startIndex, endIndex);
		int gradientLength = gradientSimulationEnd - gradientSimulationStart + 1;

		self.Clear();
		self.Append(scope.before);
		for (int i = 0; i < scope.between.Length; i++) {
			if (IsIgnoredSymbol(scope.between[i])) {
				self.Append(scope.between[i]);
				continue;
			}
			self.Append(Colorize(scope.between[i].ToString(), gradient, GetGradientTime(i), gradientLength));
		}
		self.Append(scope.after);

		int GetGradientTime(int offset) {
			return startIndex - gradientSimulationStart + offset;
		}
	}

	[Pure] public static string Colorize([NotNull] string   self,
	                                     [NotNull] Gradient gradient,
	                                     int                t,
	                                     int                length,
	                                     bool               toShortHex = true)
		=> AddTag.Color(self, toShortHex
			? ColorConvertor.ToShortHexColor(gradient.Evaluate((float) t/length))
			: ColorConvertor.ToHexColor(gradient.Evaluate((float) t/length)));

	[Pure] public static char[] Colorize([NotNull] char[]   self,
	                                     [NotNull] Gradient gradient,
	                                     int                t,
	                                     int                length,
	                                     bool               toShortHex = true)
		=> AddTag.Color(self, toShortHex
			? ColorConvertor.ToShortHexColor(gradient.Evaluate((float) t/length))
			: ColorConvertor.ToHexColor(gradient.Evaluate((float) t/length)));

	[Pure] public static char[] Colorize(char               self,
	                                     [NotNull] Gradient gradient,
	                                     int                t,
	                                     int                length,
	                                     bool               toShortHex = true)
		=> AddTag.Color(self, toShortHex
			? ColorConvertor.ToShortHexColor(gradient.Evaluate((float) t/length))
			: ColorConvertor.ToHexColor(gradient.Evaluate((float) t/length)));
}
}
