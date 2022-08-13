using System.Text;
using JetBrains.Annotations;

namespace Graffiti.Internal {
internal static partial class GraffitiStylist {

	/// <summary> Class that contains methods that add a certain tag to a char/char[]/string/StringBuilder </summary>
	internal static class AddTag {

		private static readonly char[] START_BOLD       = {'<', 'b', '>'};
		private static readonly char[] CLOSE_BOLD       = {'<', '/', 'b', '>'};
		private static readonly char[] START_ITALIC     = {'<', 'i', '>'};
		private static readonly char[] CLOSE_ITALIC     = {'<', '/', 'i', '>'};
		private static readonly char[] START_BOLDITALIC = {'<', 'b', '>', '<', 'i', '>'};
		private static readonly char[] CLOSE_BOLDITALIC = {'<', '/', 'i', '>', '<', '/', 'b', '>'};
		private static readonly char[] CLOSE_SIZE       = {'<', '/', 's', 'i', 'z', 'e', '>'};
		private static readonly char[] CLOSE_COLOR      = {'<', '/', 'c', 'o', 'l', 'o', 'r', '>'};

		private static readonly char[] OPEN_SIZE_INCOMPLETE  = {'<', 's', 'i', 'z', 'e', '=',};
		private static readonly char[] OPEN_COLOR_INCOMPLETE = {'<', 'c', 'o', 'l', 'o', 'r', '=',};


		public static void FontStyle(StringBuilder sb, UnityBuildInFontStyleType fontStyle, int startIndex, int endIndex) {
			switch (fontStyle) {
				case UnityBuildInFontStyleType.Bold:       GraffitiStylist.AddTag.Bold(sb, startIndex, endIndex);       return;
				case UnityBuildInFontStyleType.Italic:     GraffitiStylist.AddTag.Italic(sb, startIndex, endIndex);     return;
				case UnityBuildInFontStyleType.BoldItalic: GraffitiStylist.AddTag.BoldItalic(sb, startIndex, endIndex); return;
			}
		}

		// \/ string \/

		/// <summary> Makes your text <b>Bold</b> </summary>
		[Pure] public static string Bold(string self)
			=> $"<b>{self}</b>";

		/// <summary> Makes your text <i>Italic</i> </summary>
		[Pure] public static string Italic(string self)
			=> $"<i>{self}</i>";

		/// <summary> Makes your text <b><i>Bold and Italic</i></b> </summary>
		[Pure] public static string BoldItalic(string self)
			=> $"<b><i>{self}</i></b>";

		/// <summary> Set text size </summary>
		/// <param name="value"> default value is 12 (in unity console) </param>
		[Pure] public static string Size(string self, int value)
			=> $"<size={value}>{self}</size>";

		/// <summary> Adds color to your text </summary>
		/// <param name="color"> Must starts with "#" (if hexadecimal) or be a built-in color name <see cref="UnityColors"/> </param>
		[Pure] public static string Color(string self, string color)
			=> $"<color={color}>{self}</color>";

		/// <summary>
		///    <para>Creates code link to <b>line</b> of code in <b>file</b> with <b>header</b>.</para>
		/// </summary>
		/// <param name="header">A string that will be displayed in console as an underlined blue link (color can be changed). On click will open defined <b>file</b> in defined <b>line</b> in your default text editor.</param>
		/// <param name="file">Path to file. Must starts with 'Assets\' (or 'Assets/' if Linux, but I haven't tested this).</param>
		/// <param name="line">Number of line.</param>
		[Pure] public static string CodeLink(string header, string file, string line)
			=> $"<a href=\"{file}\" line=\"{line}\">{header}</a>";



		// \/ StringBuilder \/

		/// <summary> Makes your text <b>Bold</b> </summary>
		public static void Bold([NotNull] StringBuilder self)
			=> self.Insert(0, START_BOLD)
			       .Append(CLOSE_BOLD);

		/// <summary> Makes your text <i>Italic</i> </summary>
		public static void Italic([NotNull] StringBuilder self)
			=> self.Insert(0, START_ITALIC)
			       .Append(CLOSE_ITALIC);

		/// <summary> Makes your text <b><i>Bold and Italic</i></b> </summary>
		public static void BoldItalic([NotNull] StringBuilder self)
			=> self.Insert(0, START_BOLDITALIC)
			       .Append(CLOSE_BOLDITALIC);

		/// <summary> Set text size </summary>
		/// <param name="value"> default value is 12 (in unity console) </param>
		public static void Size([NotNull] StringBuilder self, int value)
			=> self.Insert(0, $"<size={value}>")
			       .Append(CLOSE_SIZE);

		/// <summary> Adds color to your text </summary>
		/// <param name="color"> Must starts with "#" (if hexadecimal) or be a built-in color name see <see cref="UnityColors"/> for built-in colors </param>
		public static void Color([NotNull] StringBuilder self, string color)
			=> self.Insert(0, $"<color={color}>")
			       .Append(CLOSE_COLOR);



		// \/ String builder with range \/

		/// <summary> Makes your text <b>Bold</b> </summary>
		public static void Bold([NotNull] StringBuilder self, int startIndex, int endIndex)
			=> self.Insert(endIndex+1, CLOSE_BOLD)
			       .Insert(startIndex, START_BOLD);

		/// <summary> Makes your text <i>Italic</i> </summary>
		public static void Italic([NotNull] StringBuilder self, int startIndex, int endIndex)
			=> self.Insert(endIndex+1, CLOSE_ITALIC)
			       .Insert(startIndex, START_ITALIC);

		/// <summary> Makes your text <b><i>Bold and Italic</i></b> </summary>
		public static void BoldItalic([NotNull] StringBuilder self, int startIndex, int endIndex)
			=> self.Insert(endIndex+1, CLOSE_BOLDITALIC)
			       .Insert(startIndex, START_BOLDITALIC);

		/// <summary> Set text size </summary>
		/// <param name="value"> default value is 12 (in unity console) </param>
		public static void Size([NotNull] StringBuilder self, int value, int startIndex, int endIndex)
			=> self.Insert(endIndex+1, CLOSE_SIZE)
			       .Insert(startIndex, $"<size={value}>");


		/// <summary> Adds color to your text </summary>
		/// <param name="color"> Must starts with "#" (if hexadecimal) or be a built-in color name see <see cref="UnityColors"/> for built-in colors </param>
		public static void Color([NotNull] StringBuilder self, string color, int startIndex, int endIndex)
			=> self.Insert(endIndex+1, CLOSE_COLOR)
			       .Insert(startIndex, $"<color={color}>");



		// \/ Char \/

		/// <summary> Makes your text <b>Bold</b> </summary>
		[Pure] public static char[] Bold(char self)
			=> Bold(new[] {self});

		/// <summary> Makes your text <i>Italic</i> </summary>
		[Pure] public static char[] Italic(char self)
			=> Italic(new[] {self});

		/// <summary> Makes your text <b><i>Bold and Italic</i></b> </summary>
		[Pure] public static char[] BoldItalic(char self)
			=> BoldItalic(new[] {self});

		/// <summary> Set text size </summary>
		/// <param name="value"> default value is 12 (in unity console) </param>
		[Pure] public static char[] Size(char self, int value)
			=> Size(new[] {self}, value);

		/// <summary> Adds color to your text </summary>
		/// <param name="color"> Must starts with "#" (if hexadecimal) or be a built-in color name <see cref="UnityColors"/> </param>
		[Pure] public static char[] Color(char self, string color)
			=> Color(new[] {self}, color);



		// \/ Char[] \/

		/// <summary> Makes your text <b>Bold</b> </summary>
		[Pure] public static char[] Bold([NotNull] char[] self)
			=> FillSelfWith(self, START_BOLD, CLOSE_BOLD);

		/// <summary> Makes your text <i>Italic</i> </summary>
		[Pure] public static char[] Italic([NotNull] char[] self)
			=> FillSelfWith(self, START_ITALIC, CLOSE_ITALIC);

		/// <summary> Makes your text <b><i>Bold and Italic</i></b> </summary>
		[Pure] public static char[] BoldItalic([NotNull] char[] self)
			=> FillSelfWith(self, START_BOLDITALIC, CLOSE_BOLDITALIC);

		/// <summary> Set text size </summary>
		/// <param name="value"> default value is 12 (in unity console) </param>
		[Pure] public static char[] Size([NotNull] char[] self, int value)
			=> FillSelfWith(self, OPEN_SIZE_INCOMPLETE, value.ToString(), CLOSE_SIZE);

		/// <summary> Adds color to your text </summary>
		/// <param name="color"> Must starts with "#" (if hexadecimal) or be a built-in color name <see cref="UnityColors"/> </param>
		[Pure] public static char[] Color([NotNull] char[] self, string color)
			=> FillSelfWith(self, OPEN_COLOR_INCOMPLETE, color, CLOSE_COLOR);


		private static char[] FillSelfWith(char[] self, char[] open, char[] close) {
			char[] result =
				new char[open.Length + self.Length + close.Length];
			int i = 0;
			FillResult(open);
			FillResult(self);
			FillResult(close);
			return result;

			void FillResult(char[] array2) {
				foreach (var t in array2) {
					result[i] = t;
					i++;
				}
			}
		}

		private static char[] FillSelfWith(char[] self, char[] openIncomplete, string openWith, char[] close) {
			char[] result =
				new char[openIncomplete.Length + openWith.Length + 1 + self.Length + close.Length];
			int i = 0;
			FillChr(openIncomplete);
			FillStr(openWith);
			result[openIncomplete.Length + openWith.Length] = '>';
			i++;
			FillChr(self);
			FillChr(close);
			return result;

			void FillChr(char[] array2) {
				foreach (var t in array2) {
					result[i] = t;
					i++;
				}
			}

			void FillStr(string array2) {
				foreach (var t in array2) {
					result[i] = t;
					i++;
				}
			}
		}
	}
}
}
