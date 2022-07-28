#define GRAFFITI_USE_GENERIC_EXTENSIONS
//#undef  GRAFFITI_USE_GENERIC_EXTENSIONS

using System;

namespace Graffiti {
public static class StyledStringExtensions {
    
	// TODO: add link to documentation
	/// <summary>
	/// • Converts string to StyledString, allowing you to effortlessly add various modifiers to your string.
	/// <para/>
	/// <i> Please, read more clear and relevant documentation here: ... </i>
	/// <para/>
	/// • List of modifiers you can use:<br/>
	/// ○ <b>Color</b> ( .Blue, .Yellow, etc. )<br/>
	/// ○ <b>Gradient</b> ( Just write two colors, like this: .Yellow.Blue )<br/>
	/// ○ <b>Size</b> ( .Size(12) )<br/>
	/// ○ <b>Font Style</b> ( .Bold, .Italic )<br/>
	/// ○ <b>Modifier characters</b> ( .Underline, .Strikethrough, etc. )
	/// <para/>
	/// • You can even stylize modifier characters. For example, you can create <b>Blue text with Yellow Underline</b>:
	/// <code> "Cool Text".Stylize().Blue.Underline[Style.Yellow]; </code>
	///
	/// • You can also <b>change color brightness</b> using <b>.Dark</b>/<b>.Light</b> and color afterwards:
	/// <code> "Cool Text".Stylize().Light.Yellow; </code>
	///
	/// • If you only want to add a modifier(s) to a specific part of a string, here is a list of ways to do so:<br/>
	/// ○ <b>To One Word</b>: <b>.Style(0)</b>, where 0 - index of the word in the string you are modifying (starting from 0)<br/>
	/// ○ <b>To a Range of Words</b>: <b>.Style(0, 1)</b>, where 0 - index of the word to start with, 1 - index of the word to end on<br/>
	/// (negative values are counted as 0, values bigger than the index of last word in a string are clamped)<br/>
	/// ○ <b>To a Range of words v2</b>: <b>.Style(0..1)</b>, where 0..1 - Range, read more here: https://docs.microsoft.com/en-us/dotnet/api/system.range?view=net-6.0<br/>
	/// ○ <b>To a Percentage</b>: <b>.Stylize(.5f)</b>, where .5f - means 50% of all words in the string<br/>
	/// (if you use negative value, words will be modified from the end)
	/// </summary>
	public static StyledString Stylize(this string self,                               StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(0, false, 0, true);    return new StyledString(self, stl);} /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize(this string self, int index,                    StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(index);                return new StyledString(self, stl);} /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize(this string self, float percentage,             StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(percentage);           return new StyledString(self, stl);} /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize(this string self, int startIndex, int endIndex, StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(startIndex, endIndex); return new StyledString(self, stl);} /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize(this string self, Index index,                  StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(index);                return new StyledString(self, stl);} /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize(this string self, Range range,                  StringStyle stl = null) {stl ??= new StringStyle(); stl.PrepareScope(range);                return new StyledString(self, stl);}

#if GRAFFITI_USE_GENERIC_EXTENSIONS

	/// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self,                               StringStyle stl = null) => self.ToString().Stylize(                      stl); /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self, int   index,                  StringStyle stl = null) => self.ToString().Stylize(index,                stl); /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self, float percentage,             StringStyle stl = null) => self.ToString().Stylize(percentage,           stl); /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self, int startIndex, int endIndex, StringStyle stl = null) => self.ToString().Stylize(startIndex, endIndex, stl); /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self, Index index,                  StringStyle stl = null) => self.ToString().Stylize(index,                stl); /// <inheritdoc cref="Stylize(string,Graffiti.StringStyle)"/>
	public static StyledString Stylize<T>(this T self, Range range,                  StringStyle stl = null) => self.ToString().Stylize(range,                stl);


	private const string WARNING_001 = "you probably want to use .And() method in current context.";

	/// <summary>
	/// • You probably want to use <b>.And()</b> method in this context.
	/// <para/>
	/// Explanation:<br/>
	/// • <b>.Stylize()</b> method always converts calling object to a string (if it isn't already) and then creates from this string new <see cref="StyledString"/>.<br/>
	/// • <b>.And()</b> method does not convert calling object to a string, it modifies the current <see cref="StyledString"/>.
	/// </summary>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self,                               StringStyle stl = null) => self.ToString().Stylize(                       stl); /// <inheritdoc cref="Stylize(StyledString,Graffiti.StringStyle)"/>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self, int index,                    StringStyle stl = null) => self.ToString().Stylize(index,                 stl); /// <inheritdoc cref="Stylize(StyledString,Graffiti.StringStyle)"/>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self, float percentage,             StringStyle stl = null) => self.ToString().Stylize(percentage,            stl); /// <inheritdoc cref="Stylize(StyledString,Graffiti.StringStyle)"/>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self, int startIndex, int endIndex, StringStyle stl = null) => self.ToString().Stylize(startIndex, endIndex,  stl); /// <inheritdoc cref="Stylize(StyledString,Graffiti.StringStyle)"/>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self, Index index,                  StringStyle stl = null) => self.ToString().Stylize(index,                 stl); /// <inheritdoc cref="Stylize(StyledString,Graffiti.StringStyle)"/>
	[Obsolete(WARNING_001)]	public static StyledString Stylize(this StyledString self, Range range,                  StringStyle stl = null) => self.ToString().Stylize(range,                 stl);

#endif //GRAFFITI_USE_GENERIC_EXTENSIONS


	/// <summary>
	/// • This method allows you to create additional layer of stylization to your string.
	/// <para/>
	/// • Example ("Cool" will be Blue, "Text" will be Yellow):<br/>
	/// <code>"Cool Text".Stylize(0).Blue.And(1).Yellow;</code>
	/// </summary>
	public static StyledString And(this StyledString self,                                StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(0, false, 0, true);    self.Styles.Add(stl); return self; } /// <inheritdoc cref="And(StyledString,StringStyle)"/>
	public static StyledString And(this StyledString self, int index,                     StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(index);                self.Styles.Add(stl); return self; } /// <inheritdoc cref="And(StyledString,StringStyle)"/>
	public static StyledString And(this StyledString self, int startIndex, int endIndex,  StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(startIndex, endIndex); self.Styles.Add(stl); return self; } /// <inheritdoc cref="And(StyledString,StringStyle)"/>
	public static StyledString And(this StyledString self, float percentage,              StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(percentage);           self.Styles.Add(stl); return self; } /// <inheritdoc cref="And(StyledString,StringStyle)"/>
	public static StyledString And(this StyledString self, Index index,                   StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(index);                self.Styles.Add(stl); return self; } /// <inheritdoc cref="And(StyledString,StringStyle)"/>
	public static StyledString And(this StyledString self, Range range,                   StringStyle stl = null) { stl ??= new StringStyle(); stl.PrepareScope(range);                self.Styles.Add(stl); return self; }
}
}
