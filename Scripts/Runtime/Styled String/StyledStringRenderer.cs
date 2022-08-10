using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
internal static class StyledStringRenderer {

	private struct Scope {
		public Scope((int, int) tuple) { Start = tuple.Item1; End = tuple.Item2; }
		public Scope(int item1, int item2) { Start = item1; End = item2; }
		public int Start;
		public int End;
	}

	private static readonly char[] SeparatorChars = {' ', '\n'};

	internal static StringBuilder Render([NotNull] StringStyle style, [NotNull] string str) {

		var sb = new StringBuilder(str);

		Scope[] elementBorders = GetWords(str, SeparatorChars);

		Scope styleScope = new Scope(style.Scope.CreateScope(elementBorders.Length));
		int start = elementBorders[styleScope.Start].Start;
		int end = elementBorders[styleScope.End].End;

		Render(sb, style, start, end, start, end, SeparatorChars);

		return sb;
	}

	internal static StringBuilder Render([NotNull] StringStyle[] styles, [NotNull] string str) {

		var sb = new StringBuilder(str);

		//  Prefix 'element' means:
		// 1) Letter (if string has only one word)
		// 2) Word (is string has 2 or more words)
		//  'Word' - any distinct character set that does not include delimiter characters (' ', '\n')

		Scope[] elementBorders = GetWords(str, SeparatorChars);
		if (elementBorders.Length == 1)
			elementBorders = GetLetters(elementBorders[0]);

		Scope[] cachedStyleScopes = new Scope[styles.Length];
		for (int i = 0; i < cachedStyleScopes.Length; i++)
			cachedStyleScopes[i] = new Scope(styles[i].Scope.CreateScope(elementBorders.Length));

		//  This 'for' loop is reversed to get rid of the need
		// to take into account changes in the length of the string
		// (as we are changing string from the end everything on the left will
		// stay on the same place).
		for (int elementIndex = elementBorders.Length - 1; elementIndex >= 0; elementIndex--) {
			var   tempStyle           = new StringStyle();
			var   elementScope        = new Scope(elementBorders[elementIndex].Start, elementBorders[elementIndex].End);
			Scope gradientGlobalScope = elementScope;

			//  This 'for' loop is reversed so that the last style applied
			// will be the main one in his scope.
			for (int j = cachedStyleScopes.Length - 1; j >= 0; j--) {
				if (styles[j].IsEmpty || !IsInsideScope(cachedStyleScopes[j], elementIndex))
					continue;

				if (styles[j].HasGradient && !tempStyle.HasGradient)
					gradientGlobalScope = new Scope(elementBorders[cachedStyleScopes[j].Start].Start,
					                                elementBorders[cachedStyleScopes[j].End].End);

				tempStyle.MergeStyles(styles[j]);
			}

			Render(sb, tempStyle,
				elementScope.Start, elementScope.End,
				gradientGlobalScope.Start, gradientGlobalScope.End,
				SeparatorChars);
		}
		return sb;

		bool IsInsideScope(Scope scope, int index) => index >= scope.Start && index <= scope.End;
	}


	private static Scope[] GetWords(string str, char[] separators) {
		var words = new List<Scope>();

		int i = 0, separatorStartIndex = 0;
		for (; i < str.Length; i++) {
			// Waiting to find something that is not a separator
			if (EqualToAnySeparator(str[i]))
				continue;

			// Found a word
			var tempScope = new Scope {
				Start = separatorStartIndex,
			};

			// Looping through word to find it's end
			for (; i < str.Length; i++) {
				bool isLastChar = i == str.Length - 1;
				bool separatorCharacter = EqualToAnySeparator(str[i]);
				bool wordEnds = separatorCharacter || isLastChar;
				if (!wordEnds) continue;

				if (isLastChar)
					tempScope.End = i;
				else
					tempScope.End = i - 1;

				words.Add(tempScope);
				separatorStartIndex = i;
				break;
			}
		}

		return words.ToArray();

		bool EqualToAnySeparator(char ch) {
			foreach (var separ in separators)
				if (ch == separ) return true;
			return false;
		}
	}

	private static Scope[] GetLetters(Scope word) {
		int length = word.End - word.Start + 1;
		var letters = new Scope[length];
		for (int i = 0; i < letters.Length; i++) {
			letters[i].Start = word.Start + i;
			letters[i].End = letters[i].Start;
		}
		return letters;
	}

	private static void Render(StringBuilder sb, StringStyle style, int start, int end, int gradientSimulationStart, int gradientSimulationEnd, char[] ignoredSymbols) {
		int prevLength = sb.Length;

		if (style.HasModifierCharacterSet) GraffitiStylist.ModifierCharacter.InsertChar(sb, style.Gradient, style.ModifierCharacterSet.Unpack(), start, end, gradientSimulationStart, gradientSimulationEnd, null);
		else if (style.HasGradient)        GraffitiStylist.AddGradient(                 sb, style.Gradient, start, end, gradientSimulationStart, gradientSimulationEnd);
		if (style.HasOnlyOneColor)         GraffitiStylist.AddTag.Color(                sb, style.Color.GetColorHexValue(), start, AdjustedEndIndex());
		if (style.HasSize)                 GraffitiStylist.AddTag.Size(                 sb, style.SizeValue, start, AdjustedEndIndex());
		if (style.HasFont)                 GraffitiStylist.AddTag.FontStyle(            sb, style.FontStyle, start, AdjustedEndIndex());

		int AdjustedEndIndex() => end + (sb.Length - prevLength);
	}
}
}
