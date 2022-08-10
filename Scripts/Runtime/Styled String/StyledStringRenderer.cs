using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
internal static class StyledStringRenderer {

	private static readonly char[] WORD_SEPARATORS = {' ', '\n'};

	// TODO: remove StringBuilder from parameters or take into account StringBuilder's length
	internal static StringBuilder Render([NotNull] StringStyle style, [NotNull] string str) {

		var sb = new StringBuilder(str);

		Scope[] elementsScopes = GetWords(str, WORD_SEPARATORS);

		(int start, int end) styleScope = style.Scope.CreateScope(elementsScopes.Length);
		int start = elementsScopes[styleScope.start].Start;
		int end = elementsScopes[styleScope.end].End;

		Render(sb, style, start, end, start, end, WORD_SEPARATORS);

		return sb;
	}

	internal static StringBuilder Render([NotNull] StringStyle[] styles, [NotNull] string str) {

		var sb = new StringBuilder(str);

		//  Prefix 'element' means:
		// 1) Letter (if string has only one word)
		// 2) World (is string has 2 or more words)

		Scope[] elementsScopes = GetWords(str, WORD_SEPARATORS);
		if (elementsScopes.Length == 1)
			elementsScopes = GetLetters(elementsScopes[0]);

		(int start, int end)[] cachedStyleScopes = new (int, int)[styles.Length];
		for (int i = 0; i < cachedStyleScopes.Length; i++)
			cachedStyleScopes[i] = styles[i].Scope.CreateScope(elementsScopes.Length);

		//  This 'for' loop is reversed to get rid of the need
		// to take into account changes in the length of the string
		// (as we are changing string from the end everything on the left will
		// stay on the same place).
		for (int elementIndex = elementsScopes.Length - 1; elementIndex >= 0; elementIndex--) {
			var tempStyle = new StringStyle();
			(int start, int end) elementScope = (elementsScopes[elementIndex].Start, elementsScopes[elementIndex].End);
			(int start, int end) gradientGlobalScope = elementScope;

			//  This 'for' loop is reversed so that the last style applied
			// will be the main one in his scope.
			for (int j = cachedStyleScopes.Length - 1; j >= 0; j--) {
				if (styles[j].IsEmpty || !IsInsideScope(cachedStyleScopes[j], elementIndex))
					continue;

				if (styles[j].HasGradient && !tempStyle.HasGradient)
					gradientGlobalScope = (elementsScopes[cachedStyleScopes[j].start].Start,
										   elementsScopes[cachedStyleScopes[j].end].End);

				tempStyle.MergeStyles(styles[j]);
			}

			Render(sb, tempStyle,
				elementScope.start, elementScope.end,
				gradientGlobalScope.start, gradientGlobalScope.end,
				WORD_SEPARATORS);
		}
		return sb;

		bool IsInsideScope((int start, int end) scope, int index) => index >= scope.start && index <= scope.end;
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

	private struct Scope {
		internal int Start;
		internal int End;
	}


	private static void Render(StringBuilder sb, StringStyle style, int start, int end, int gradientSimulationStart, int gradientSimulationEnd, char[] ignoredSymbols) {
		int prevLength = sb.Length;

		if (style.HasModifierCharacterSet) GraffitiStylist.ModifierCharacter.InsertChar(sb, style.Gradient, style.ModifierCharacterSet.Unpack(), start, end, gradientSimulationStart, gradientSimulationEnd, null);
		else if (style.HasGradient)        GraffitiStylist.AddGradient(                 sb, style.Gradient, start, end, gradientSimulationStart, gradientSimulationEnd);
		if (style.HasOnlyOneColor)         GraffitiStylist.AddTag.Color(                sb, style.Color.GetColorHexValue(), start, AdjustedEndIndex());
		if (style.HasSize)                 GraffitiStylist.AddTag.Size(                 sb, style.SizeValue, start, AdjustedEndIndex());
		if (style.HasFont)                 GraffitiStylist.AddTag.FontStyle(                 sb, style.FontStyle, start, AdjustedEndIndex());

		int AdjustedEndIndex() => end + (sb.Length - prevLength);
	}
}
}
