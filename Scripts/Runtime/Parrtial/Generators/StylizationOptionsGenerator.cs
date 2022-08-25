using System;
using Graffiti.CodeGeneration;

namespace Graffiti {
public static class StylizationOptionsGenerator {

    public static string GenerateCode()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        // b.Config
        //  .SetRule
        //  .CommentAll();

        b.Header(nameof(StylizationOptionsGenerator)).Br();

        b.Using("JetBrains.Annotations");
        b.Using("UnityEngine");
        b.Using("Graffiti.Internal");
        b.Using("Graffiti.Internal.Helpers").Br();

        b.TitledSeparator("Graffiti Colors");

        b.Namespace.Name("Graffiti.Internal").Body(() => {
            b.Internal.Enum.Name(NameOf.ColorType).Body(Enum => {
                Enum.DefaultMember();
                foreach (var field in StylizationOptionsData.ColorFields)
                    Enum.Member(field.name);
            });
        }).Br();

        b.Namespace.Name("Graffiti").Body(() => {
            b.PublicAPI.Br();
            b.Public.Partial.Class.Nameof<ColorPalette>().Body(() => {
                b.Br();
                // [field: SerializeField] public GffColor White { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
                foreach (var field in StylizationOptionsData.ColorFields)
                    b.field_SerializeField.Public.Property.Returns(NameOf.GffColor).Name(field.trimmedName)
                     .GetPrivateSet(MethodBodyOf.New.GffColor(field.UnityColor, field.ShortHexColor));
                b.Br();

                b.Internal.Method.Returns(NameOf.GffColor).Name(NameOf.FindColor).Params((NameOf.ColorType, "color"))
                 .Body(Method => {
                      b.Switch.Value(Method.GetParam(0).name).Body(Switch => {
                          Switch.DefaultCase().Br();
                          Switch.Case(NameOf.ColorType + ".Default")
                                .Return(nameof(ColorPalette.DefaultConsoleColor));
                          foreach (var field in StylizationOptionsData.ColorFields)
                              Switch.Case(NameOf.ColorType + "." + (field.trimmedName))
                                    .Return(field.trimmedName);
                      });
                  });
            }).Br();

            b.TitledSeparator("Generic Interface");

            b.PublicAPI.Br();
            b.Public.Interface.Name(NameOf.IOnlyColor).Write("<out T>").Body(() => {
                foreach (var field in StylizationOptionsData.ColorFields)
                    b.Public.Write("T ").Write(field.trimmedName).Write(" { get; }").Br();
            }).Br();

            b.TitledSeparator("Fluent API");

            b.PublicAPI.Br();
            b.Public.Partial.Class.Nameof<StyledString>().Inherit(INameOf.IOnlyColor(b.Self)).Body(() => {
                b.Br();
                WriteCommonProperties();
            }).Br();

            b.PublicAPI.Br();
            b.Public.Partial.Class.Nameof<StringStyle>().Inherit(INameOf.IOnlyColor(b.Self)).Body(() => {
                b.Br();
                WriteCommonProperties();
            }).Br();

            b.PublicAPI.Br();
            b.Public.Static.Partial.Class.Name(nameof(Style)).Body(() => {
                b.Br();
                WriteCommonProperties();
            });
        });

        return b.ToString();


        void WriteCommonProperties()
        {
            WriteCommonPropertyAuto("DefaultColor", MethodBodyOf.PrepareColor, true);
            foreach (var field in StylizationOptionsData.ColorFields) {
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareColor);
            }

            b.Br();

            foreach (var field in StylizationOptionsData.ModifierCharacterFields) {
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareModifierCharacter);
            }
        }

        void WriteCommonPropertyAuto(string propertyName, Func<string, string> getBody, bool isDefaultItem = false)
        {
            string itemName = isDefaultItem ? "Default" : propertyName;
            switch (b.Self) {
                case NameOf.StyledString:
                    b.Public.Property.Returns(NameOf.StyledString).Name(propertyName)
                     .Expression("LastStyle." + getBody(itemName) + ".Return(this)");
                    break;
                case NameOf.StringStyle:
                    b.Public.Property.Returns(NameOf.StringStyle).Name(propertyName).Expression(getBody(itemName));
                    break;
                case NameOf.Style:
                    b.Public.Static.Property.Returns(NameOf.StringStyle).Name(propertyName)
                     .Expression(MethodBodyOf.StringStyle_Create() + "." + getBody(itemName));
                    break;
            }
        }
    }

    // Constants
    private static class NameOf {

        public const string ColorType             = "ColorType";
        public const string ModifierCharacterType = nameof(Internal.ModifierCharacterType);
        public const string GffColor              = nameof(global::Graffiti.GffColor);
        public const string StyledString          = nameof(global::Graffiti.StyledString);
        public const string StringStyle           = nameof(global::Graffiti.StringStyle);
        public const string Style                 = nameof(global::Graffiti.Style);
        public const string IOnlyColor            = "IOnlyColor";
        public const string FindColor             = "FindColor";

    }

    // Constants
    private static class MethodBodyOf {

        public static string StringStyle_Create() => $"{NameOf.StringStyle}.{nameof(StringStyle.Create)}()";

        public static string PrepareColor(string itemType) => $"{nameof(StringStyle.PrepareColor)}({NameOf.ColorType}.{itemType})";

        public static string PrepareModifierCharacter(string itemType)
            => $"{nameof(StringStyle.PrepareModifierCharacter)}({NameOf.ModifierCharacterType}.{itemType})";

        public static class New {

            public static string GffColor(string unityColor, string shortHexColor)
                => $"new {NameOf.GffColor}({unityColor}, \"{shortHexColor}\")";

        }

    }

    private static class INameOf {

        public static string IOnlyColor(string T) => $"{NameOf.IOnlyColor}<{T}>";

    }

}
}
