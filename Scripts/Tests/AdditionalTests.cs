using UnityEngine;

namespace Graffiti.Tests {
public static class AdditionalTests {
    public const string TEST_TEXT_100 = "L";
    public const string TEST_TEXT_101 = "L ";
    public const string TEST_TEXT_102 = " L";
    public const string TEST_TEXT_103 = " L ";
    public const string TEST_TEXT_200 = "Lorem";
    public const string TEST_TEXT_201 = "Lorem ";
    public const string TEST_TEXT_202 = " Lorem";
    public const string TEST_TEXT_203 = " Lorem ";
    public const string TEST_TEXT_300 = "Lorem ipsum";
    public const string TEST_TEXT_301 = "Lorem ipsum ";
    public const string TEST_TEXT_302 = " Lorem ipsum";
    public const string TEST_TEXT_303 = " Lorem ipsum ";
    public const string TEST_TEXT_400 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

    public const string TEST_TEXT_404 = "My name is ";
    public const string TEST_TEXT_040 = "Giovanni Giorgio";

    //[Shortcut("Testing Graffiti 001", KeyCode.Alpha0, ShortcutModifiers.Control | ShortcutModifiers.Alt | ShortcutModifiers.Shift)]
    public static void Run_OnlyInteresting()
    {
        Debug.Log(
            "Oh, my beautiful, gorgeous text... It's perfect. Simply delightful..."
                   .Stylize(..4).Purple.Yellow
                   .And(5..6).Bold.White
                   .And(7..).Grey.Italic);

        Debug.Log(
            "Graffiti: Doing some tests... Succeeded!"
                   .Stylize(0).Blue.Bold.Size(10)
                   .And(1..3).Italic
                   .And(^0).Green.Bold);

        Debug.Log(
            "Orange color gets lighter, theeeeeeeeeeeeeeeeen darker, then back to normal."
                   .Stylize().Orange.Light.Orange.Dark.Orange.Orange);

        Debug.Log(
            "Going really craaaazyyyy now normal again"
                   .Stylize(.5f).Red.Blue.Yellow.Purple.Green.Blue.Orange.Red.Bold.Strikethrough.SmokingHot.Underline);

        // Debug.Log("Some Cool Text".Stylize().Blue);
        // Debug.Log("Some Cool Text".Stylize().Blue.Underline);
        // Debug.Log("Some Cool Text".Stylize().Blue.Underline[Style.Yellow]);
        // Debug.Log("Some Cool Text".Stylize().Underline.Blue[Style.Yellow]);
    }

    //[Shortcut("Testing Graffiti 002", KeyCode.Alpha5, ShortcutModifiers.Control | ShortcutModifiers.Alt | ShortcutModifiers.Shift)]
    public static void Run_All()
    {
        Run_DifferentStringInputs();
        Run_DifferentScopes();
        Run_SeveralStyledStrings();
    }

    private static void Run_DifferentStringInputs()
    {
        Debug.Log("Different string inputs test: ");
        Debug.Log("["      + "   ".Stylize().Green                              + "]");
        Debug.Log("["      + TEST_TEXT_100.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_101.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_102.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_103.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_200.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_201.Stylize().Green                      + "]");
        string aaaa1 = "[" + TEST_TEXT_202.Stylize().Green.Yellow               + "]";
        Debug.Log("["      + TEST_TEXT_202.Stylize().Green.Yellow               + "]");
        Debug.Log("["      + TEST_TEXT_203.Stylize().Underline[Style.Green.Red] + "]");
        Debug.Log("["      + TEST_TEXT_300.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_301.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_302.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_303.Stylize().Green                      + "]");
        Debug.Log("["      + TEST_TEXT_400.Stylize().Green                      + "]");
    }

    private static void Run_DifferentScopes()
    {
        Debug.Log("Different string inputs test: ");
        Debug.Log(" a b c d e ".Stylize().Green.And(1).Red.And(2).Blue);
        Debug.Log(TEST_TEXT_100.Stylize().Green.And(1).Red); // Should be red
        Debug.Log(TEST_TEXT_200.Stylize().Green.And(1).Red.And(2).Blue.And(3..).Yellow.And().Bold);
        Debug.Log(TEST_TEXT_300.Stylize().Green.And(1).Red);
        Debug.Log(TEST_TEXT_400.Stylize().Green.And(1).Red.And(2).Blue.And(3..).Yellow.Purple);
    }

    private static void Run_SeveralStyledStrings()
        => Debug.Log(TEST_TEXT_404.Stylize().Red.Yellow + TEST_TEXT_040.Stylize().Bold.Blue.Size(16));
}
}
