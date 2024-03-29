using System.Text;
using UnityEngine;

namespace Graffiti.Tests {
public class Tests {
    public static void RunAllTests()
    {
        Run_Main001();
        Run_Main004();
        Run_Main005();
    }


    //[MenuItem("Tools/Sugar/RunTest_1 &#%7")]
    public static void RunTest_1()
    {
        Debug.Log("Test is Running...");

        Debug.Log("Some sort of a text".Stylize(Style.Green.Yellow[Style.Red].Underline[Style.Red]));

        // Debug.Log(",,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,, Apply... ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,\n");
        // Debug.Log("'''''''''''''''''''''''''''''''''''''' Apply... '''''''''''''''''''''''''''''''''\n");
        // Debug.Log("',',',',',',',',',',',',',',',',',',', Apply... ',',',',',',',',',',',',',',',','\n");

        // Style style = Style.Create().Black.Blue.Red.Bold;
        // StyledString sstyle = StyledString.Create().Blue.Red.Yellow.Bold;

        Run_Main001();
    }

    //[MenuItem("Tools/Sugar/RunTest_2 &#%6")]
    public static void RunTest_2()
    {
        Debug.Log("My Message To You Is Simple: S1mple".Stylize().Red.Violet);
        // var b = new StringStyleBase();
        // b.PrepareColor(GraffitiColorType.Yellow);
        // b.PrepareColor(GraffitiColorType.Purple);
        // Debug.Log(b.HasGradient);
        // Debug.Log("My Message To You Is Simple: S1mple".To().Red[b]);
        // Debug.Log("My Message To You Is Simple: S1mple".To().Red.Violet.Underline);
        // Debug.Log("My Message To You Is Simple: S1mple".To().Red.Violet.Underline[b]);

        // Debug.Log("Opus".To().Size(1));
        // Debug.Log("Opus".To().Size(10));
        // Debug.Log("Opus".To().Size(100));
        // Debug.Log("Opus".To().Size(128));
        // Debug.Log("Opus".To().Size(256));
        // Debug.Log("Opus".To().Size(400));
        // Debug.Log("Opus".To().Size(480));
        // Debug.Log("Opus".To().Size(499));
        // Debug.Log("Opus".To().Size(500));
        // Debug.Log("Opus".To().Size(501));

        Run_Main004();
    }

    //[MenuItem("Tools/Sugar/RunTest_3 &#%8")]
    public static void RunTest_3()
    {
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..3).Red.Yellow.Bold.And(2..).Blue.Orange.Bold);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize(..3).Underline.Red.Yellow.Bold.And(2..).Strikethrough.Blue.Orange.Bold);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more"
                   .Stylize(..3).Underline[Style.Blue].Red.Yellow.Bold.And(2..).Strikethrough[Style.Red.White].Blue
                   .Orange.Bold);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more"
                   .Stylize(..3).Strikethrough[Style.Blue].Red.Yellow.Bold.And(2..).Strikethrough[Style.Red.White]
                   .Blue.Orange.Bold);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize(..3).DoubleUnderline.Blue.Orange.Bold.And(2..)
                                                           .Dotted[Style.DefaultColor]);
        Debug.Log("Helicopter helicopter, TWO ".Stylize().Underline[Style.Blue].Red.Yellow.Bold);
        Debug.Log("[" + " Babardar".Stylize().Blue.Yellow);

        Debug.Log("Artabra".Stylize().Violet.Red + " Babardar".Stylize().Blue.Yellow);
        Debug.Log(
            "Helicopter helicopter, TWO".Stylize().Underline[Style.Blue].Red.Yellow.Bold
          + " Tree Four Five Six".Stylize().Strikethrough[Style.Red.White].Blue.Orange.Bold);


        string result = "Incredibly interesting, simply delightful and even more".Stylize(1..3).Underline[Style.Blue].Red.Yellow.Bold;
        Debug.Log(result);
        //Run_Main005();
    }

    public static void Run_Main005()
    {
        Debug.Log("---- 8 ----");
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..3).Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..3).Red.Underline);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..3).Underline.Red.Bold);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..3).Underline.Red.Yellow.Bold);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize(1..3).Underline.Red.Yellow.Bold.And(2..5).Strikethrough.Blue.Orange
                                                           .Bold);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..3).Underline[Style.Blue.Size(20)].Red.Yellow.Bold);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Yellow.Underline[Style.Blue]);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize().Red.Blue.Yellow.Violet.Bold.And(2..5).Underline[Style.Blue].And(..1)
                                                           .Black);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize().Red.Blue.Yellow.Violet.Bold.And(2..5).Red.Underline[Style.Blue]
                                                           .And(..1).Black);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Red.Violet.Bold.And(1..5).Underline[Style.Blue.Yellow]);
    }

    public static void Run_Main004()
    {
        Debug.Log("Test is Running...");

        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize());
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Dark);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Light);

        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Red.Underline.Green);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Red.Yellow.Underline.Green);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Red.Yellow.Underline.Purple.Green);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Red.Yellow.Underline.Purple.Green);
        Debug.Log(
            "Going really craaaazyyyy now normal again".Stylize(.5f).Red.Blue.Yellow.Underline.Purple.Green.Bold.Strikethrough.Blue.Orange
                                                       .SmokingHot.Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(1..4).Red.Blue.Yellow.Underline.Purple.Green.Bold);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(9090).Red.Blue.Yellow.Underline.Purple.Green.Bold);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..).Red.Blue.Underline.Purple.Green.Wheel);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..).Red.Blue.Wheel);

        Debug.Log("Hardcore...");

        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..3).Red.And(2..).Violet);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize(..2).Blue.Red.Blue.Wheel.Purple.Orange.And(2..).Violet.Orange
                                                           .Strikethrough.Black.Blue);

        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..2).Blue.Red.Blue);
        Debug.Log(
            "Incredibly interesting, simply delightful and even more".Stylize(..1).Blue.Red.Yellow.And(4..).Purple.Green.And().Underline.Violet);


        //Debug.Log("AAAA".Stylize().Blue.Stylize());
    }

    // public static void Run_Main003() {
    //
    // 	var sb = new StringBuilder("Example Shmopsil");
    // 	var gr1 = StyleRenderer.CreateGradient(new[] {UnityEngine.Color.red, UnityEngine.Color.blue});
    // 	var gr2 = StyleRenderer.CreateGradient(new[] {UnityEngine.Color.green, UnityEngine.Color.yellow});
    // 	var gr3 = StyleRenderer.CreateGradient(new[] {UnityEngine.Color.cyan, UnityEngine.Color.magenta});
    //
    // 	(char[], Gradient)[] pairs = {
    // 		(Size(GetSymbol(XSymbol.Underline), 24), gr2),
    // 		(Size(GetSymbol(XSymbol.Strikethrough), 24), gr3),
    // 	};
    //
    // 	Insert.Symbol(sb, gr1, pairs, 0, sb.Length - 1);
    // 	Debug.Log(sb);
    // }
    //
    // public static void Run_Main002() {
    // 	StringBuilder sb;
    //
    // 	sb = new StringBuilder("Example Shmopsil");Insert.Symbol(sb, XSymbol.Underline);
    // 	Debug.Log(sb);
    // 	sb = new StringBuilder("Example Shmopsil");Insert.Symbol(sb, new XSymbol[] {XSymbol.Underline, XSymbol.Strikethrough});
    // 	Debug.Log(sb);
    // 	sb = new StringBuilder("Example Shmopsil");Insert.Symbol(sb, XSymbol.Underline, StyleRenderer.CreateGradient(new []{UnityEngine.Color.red, UnityEngine.Color.blue}));
    // 	Debug.Log(sb);
    // 	sb = new StringBuilder("Example Shmopsil");Insert.Symbol(sb, new XSymbol[] {XSymbol.Underline, XSymbol.Strikethrough}, StyleRenderer.CreateGradient(new []{UnityEngine.Color.red, UnityEngine.Color.blue}));
    // 	Debug.Log(sb);
    // 	sb = new StringBuilder("Example Shmopsil");
    // 	Insert.Symbol(sb, XSymbol.Underline, StyleRenderer.CreateGradient(new[] {UnityEngine.Color.yellow, UnityEngine.Color.cyan}));
    // 	//Insert.Symbol(sb, XSymbol.Strikethrough, StyleRenderer.CreateGradient(new[] {UnityEngine.Color.blue, UnityEngine.Color.red}));
    // 	Debug.Log(sb);
    // }

    public static void Run_TestAddColor()
    {
        char[] a = new[] { 'H', 'e', 'l', 'l', 'o', ' ', 't', 'h', 'e', 'r', 'e' };
        var sb = new StringBuilder();
        //sb.Append(Color(a, UnityColors.GetColor(UnityColors.UnityColorType.Cyan)));
        Debug.Log(sb);
    }

    public static void Run_Main001()
    {
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize().Green.Dark.Green.Light.Green);

        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(-.7f).Size(24).Bold);
        //Debug.Log("Incredibly interesting, simply delightful and even more".To(.3f).Yellow.Red.And(-.7f).Red.Yellow);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(-.7f).Yellow.Red.Blue);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(.999f).Blue.Orange);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(-.999f).Blue.Orange);
        // Debug.Log("Incredibly interesting, simply delightful and even more".To(.00001f).Blue.Orange);
        // Debug.Log("Incredibly interesting, simply delightful and even more".To(-.00001f).Blue.Orange);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..3).Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(3..).Red.Blue);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..^3).Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(^5..).Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(..).Red);
        Debug.Log("Incredibly interesting, simply delightful and even more".Stylize(^5).Red);
    }

    public static void Run_TestUnicode()
    {
        Debug.Log("M̶y̶ ̶t̶e̶x̶t̶ ̶i̶s̶ ̶B̶O̶L̶D̶");
        Debug.Log("T̾e̾s̾t̾ i̾s̾ R̾u̾n̾n̾i̾n̾g̾...");
        Debug.Log("M̾y̾ ̾t̾e̾x̾t̾ ̾i̾s̾ ̾B̾O̾L̾D̾".Stylize().Size(24));
        Debug.Log("M̾y̾ ̾t̾e̾x̾t̾ ̾i̾s̾ ̾B̾O̾L̾D̾".Stylize().Size(24));
        Debug.Log("M͓̽y͓̽ ͓̽t͓̽e͓̽x͓̽t͓̽ ͓̽i͓̽s͓̽ ͓̽B͓̽O͓̽L͓̽D͓̽".Stylize().Size(24));

        Debug.Log("Example       \n   ^Default".Stylize().Size(24));
        Debug.Log("E̥x̥ḁm̥p̥l̥e̥       \n   ^Wheels".Stylize().Size(24));           // ok
        Debug.Log("E̴x̴a̴m̴p̴l̴e̴\n   ^Strikethrough wavy".Stylize().Size(24));      // ok+
        Debug.Log("E̶x̶a̶m̶p̶l̶e̶       \n   ^Strikethrough".Stylize().Size(24));    // ok+
        Debug.Log("E̲x̲a̲m̲p̲l̲e̲       \n   ^Underline".Stylize().Size(24));        // ok
        Debug.Log("E̠x̠a̠m̠p̠l̠e̠       \n   ^Single Underline".Stylize().Size(24)); // bad
        Debug.Log("E̳x̳a̳m̳p̳l̳e̳       \n   ^Double Underline".Stylize().Size(24)); // ok
        Debug.Log("E̤x̤a̤m̤p̤l̤e̤       \n   ^Dotted".Stylize().Size(24));           // ok
        Debug.Log("E̷x̷a̷m̷p̷l̷e̷\n   ^Shadow".Stylize().Size(24));                  // ok+
        Debug.Log("E̸x̸a̸m̸p̸l̸e̸\n   ^Big Shadow".Stylize().Size(24));              // ok+
    }

    // public static void Run_GetWordTest() {
    // 	var lst = StyledStringRenderer.GetWords(" 1 3 5 ");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Red);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("1 3 5");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Blue);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("    1    3   5");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Green);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("1     3 5     ");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Yellow);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords(" asdas ASDA ASDASD5 ");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Red);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("ASD ASD AVH ");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Blue);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("    jrrtd    fnd   sfg");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Green);
    // 	}
    //
    // 	lst = StyledStringRenderer.GetWords("sfgh     sgf fdg     ");
    // 	foreach (var a in lst) {
    // 		Debug.Log(a.ToString().To().Orange);
    // 	}
    // }
}
}
