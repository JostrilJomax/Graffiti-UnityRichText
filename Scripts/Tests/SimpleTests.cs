using UnityEngine;
using static Graffiti.Internal.GraffitiStylist;

namespace Graffiti.Tests {
public static class SimpleTests {
    private static readonly string text                 = "Index_0 Index_1 Index_3 Index_4";
    private static          int    _assertsCount        = 100;
    private static          bool   _addPreceedingSpaces = true;
    private static          string Color1 => ColorPalette.DefaultInstance.Green.ShortHex;
    private static          string Color2 => ColorPalette.DefaultInstance.White.ShortHex;

    public static void RunAllTests() => TestBaseFunctionality();



    //[MenuItem("Tools/Sugar/TestBaseFunctionality1 &#%Y")]
    public static void AnotherMethod()
    {
        bool flag = Random.Range(0, 1) == 1;

        Debug.Log($"{nameof(flag)} is set to {flag}".Stylize(0).Bold.And(^0).Bold);
    }


    //[MenuItem("Tools/Sugar/TestBaseFunctionality &#%T")]
    public static void TestBaseFunctionality()
    {
        // Basic actions on the text =============================================================================================================================

        Log(text.Stylize(), "Just text");

        Log(text.Stylize().Bold, "Bold text");
        Log(text.Stylize().Size(24), "Text with size of 24 (double than default)");
        Log(text.Stylize().Underline, "Underlined text");
        Log(text.Stylize().Red, "Colored text");

        // Note: If you want to make a gradient you can just write 2 or more colors in the same style.
        Log(text.Stylize().Red.Green, "Gradient (Red.Green) text");

        // Note: Does not matter if the colors were interrupted by another modificator.
        Log(
            text.Stylize().Red.Yellow.Size(24).Green.Blue.Purple.Orange,
            "Gradient (Red.Yellow.Green.Blue.Purple.Orange) text with size 24. You can have up to 8 colors");


        // Ways to select and modify particular part of the text =================================================================================================

        _addPreceedingSpaces.Off();

        // Note: As you can see in the example below you don't need to separate the words that you want to modify, that is surely beneficial to readability.
        //       (I have to split the string because it was too long for the IDE, but you get the point).
        // Note: The style is applied to the 9'th word from the end, to the 7'th word from the end (But as in code we use indexes, it is written as ^8..^6).
        Log(
            "If you have a long string and you only need to add style to one or a few words in it, "
          + "you don't have to split the string, you can just define the area where the style will be applied".Stylize(^8..^6).Yellow.Bold);

        _addPreceedingSpaces.On();

        // Note: Index of the third word is 2, because index of the first word is 0
        // - Why so?
        // - The only reason for this is that the default implementation of the Range and Index classes is done the way that
        //   the Range 0..1 (or ..1) means from the first word to the second and the Index of ^0 means the first word from the end (the last word).
        Log(text.Stylize(1).Blue, "The second word is Colored");
        Log(text.Stylize(^1).Purple, "The Second word from the end is Colored");
        Log(text.Stylize(1, true).Purple, $"(Same as {LogNumber(-1)}, but different writing style)");

        Log(text.Stylize(..1).Violet.Bold, "First 2 words are Colored and Bold");
        Log(text.Stylize(^1..).Red.Bold, "Last 2 words are Colored and Bold");
        Log(text.Stylize(0, 1).Violet.Bold, $"(Same as {LogNumber(-2)}, but different writing style)");
        Log(text.Stylize(1, 0, true).Red.Bold, $"(Same as {LogNumber(-2)}, but different writing style)");

        Log(text.Stylize(-.3f).Orange.Size(24), "Last 30% of words are Colored and has size 24");
    }

    private static void Log(object self, string description = null)
    {
        Debug.Log(AddLogNumber() + AddSpaces() + $"|{self}|" + "\n" + AddDescription() + "\n");

        string AddLogNumber() => AddTag.Color($"<b>[{_assertsCount++}]:</b>", Color1);

        string AddSpaces()
            => _addPreceedingSpaces ? "                                                                                    " : "";

        string AddDescription() => AddTag.Color($"└{description ??= "no description"}.", Color2);
    }

    private static string LogNumber(int offset)      => AddTag.Bold((_assertsCount + offset).ToString());
    private static bool   Toggle(ref this bool self) => self = !self;
    private static bool   Off(ref this bool self)    => self = false;
    private static bool   On(ref this bool self)     => self = true;
}
}
