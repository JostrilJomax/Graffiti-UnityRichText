using System;
using Graffiti.CodeGeneration;
using UnityEngine;

namespace Graffiti {
public static class StylizationOptionsGenerator {

    public static string GenerateCode()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        b.Config
         .SetRule
         .CommentAll();

        b.Header(nameof(StylizationOptionsGenerator)).Br();

        b.Using("JetBrains.Annotations");
        b.Using("UnityEngine");
        b.Using(NameOf.Graffiti_Internal).Br();

        b.Namespace.Name(NameOf.Graffiti_Internal).Body(() => {
            b.Internal.Enum.Name(NameOf.ColorType).Body(Enum => {
                Enum.DefaultMember();
                foreach (var field in StylizationOptionsData.ColorFields)
                    Enum.Member(field.name);
            });
        }).Br();

        b.Namespace.Name(NameOf.Graffiti).Body(() => {
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
                          Switch.Case(NameOf.ColorType.Dot("Default"))
                                .Return(nameof(ColorPalette.DefaultConsoleColor));
                          foreach (var field in StylizationOptionsData.ColorFields)
                              Switch.Case(NameOf.ColorType.Dot(field.trimmedName))
                                    .Return(field.trimmedName);
                      });
                  });
            }).Br();

            b.PublicAPI.Br();
            b.Public.Partial.Class.Nameof<StringStyle>().Inherit(nameof(StringStyle).Dot(NameOf.IOnlyColor)).Body(() => {
                b.Br();
                WriteCommonInterfaces(b.Glue.CurrentCodeBlock.Name);
                // public StringStyle White => PrepareColor(ColorType.White);
                // public StringStyle SmokingHot => PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
                WriteCommonProperties();
            }).Br();

            b.PublicAPI.Br();
            b.Public.Partial.Class.Nameof<StyledString>().Inherit(nameof(StyledString).Dot(NameOf.IOnlyColor)).Body(() => {
                b.Br();
                WriteCommonInterfaces(b.Glue.CurrentCodeBlock.Name);
                // public StyledString White { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
                // public StyledString SmokingHot { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot); return this; } }
                WriteCommonProperties();
            }).Br();

            b.PublicAPI.Br();
            b.Public.Static.Partial.Class.Name(nameof(Style)).Body(() => {
                b.Br();
                // public static StringStyle White => StringStyle.Create().PrepareColor(ColorType.White);
                // public static StringStyle SmokingHot => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
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

        void WriteCommonInterfaces(string returnType)
        {
            b.PublicAPI.Br();
            b.Public.Interface.Name(NameOf.IOnlyColor).Body(() => {
                foreach (var field in StylizationOptionsData.ColorFields)
                    b.Public.Writeln($"{returnType} {field.trimmedName} {{ get; }}");
            }).Br();
        }

        void WriteCommonPropertyAuto(string propertyName, Func<string, string> getBody, bool isDefaultItem = false)
        {
            string itemName = isDefaultItem ? "Default" : propertyName;
            switch (b.Glue.CurrentCodeBlock.Name) {
                case NameOf.StringStyle:
                    b.Public.Property.Returns(NameOf.StringStyle).Name(propertyName).Expression(getBody(itemName));
                    break;
                case NameOf.StyledString:
                    b.Public.Property.Returns(NameOf.StyledString).Name(propertyName)
                     .GetReturnThis("LastStyle." + getBody(itemName));
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

        public const string Graffiti              = "Graffiti";
        public const string Graffiti_Internal     = "Graffiti.Internal";
        public const string ColorType             = "ColorType";
        public const string ModifierCharacterType = nameof(Internal.ModifierCharacterType);
        public const string GffColor              = nameof(global::Graffiti.GffColor);
        public const string ColorPalette          = nameof(global::Graffiti.ColorPalette);
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

}
}
