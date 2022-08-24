using System;
using Graffiti.CodeGeneration;

namespace Graffiti {
public static class StylizationOptionsGenerator {

    public static string GenerateCode()
    {
        var b = CodeBuilder.CreateDefaultBuilder();

        b.Header(nameof(StylizationOptionsGenerator)).br();

        b.Using("JetBrains.Annotations");
        b.Using("UnityEngine");
        b.Using(NameOf.Graffiti_Internal).br();

        using (b.Namespace(NameOf.Graffiti_Internal)) {
            b.Internal.Enum(NameOf.ColorType, Enum => {
                Enum.WriteDefaultMember();
                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                    Enum.WriteMember(field.name);
            });
        }

        using (b.Namespace(NameOf.Graffiti)) {
            b.PublicAPI.br();
            using (b.Public.Partial.Class(NameOf.ColorPalette).br()) {
                // [field: SerializeField] public GffColor White { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                    b.field_SerializeField.Public.Property(NameOf.GffColor, field.trimmedName)
                     .GetPrivateSet(MethodBodyOf.New.GffColor(field.UnityColor, field.ShortHexColor));
                b.br();

                b.Internal.Method.Returns(NameOf.GffColor).Name(NameOf.FindColor).Params((type: NameOf.ColorType, name: "color")).Body(Method => {
                    b.Switch(Method.GetParam(0).name, Switch => {
                        Switch.WriteDefaultCase().br();
                        Switch.WriteCase($"{NameOf.ColorType}.Default").Return(nameof(ColorPalette.DefaultConsoleColor));
                        foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                            Switch.WriteCase($"{NameOf.ColorType}.{field.trimmedName}").Return(field.trimmedName);
                    });
                });
            }

            b.br();

            b.PublicAPI.br();
            using (b.Public.Partial.Class(NameOf.StringStyle, $"{NameOf.StringStyle}.{NameOf.IOnlyColor}").br()) {
                WriteCommonInterfaces(b.Root.CurrentCodeBlock.Name);
                // public StringStyle White => PrepareColor(ColorType.White);
                // public StringStyle SmokingHot => PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
                WriteCommonProperties();
            }

            b.br();

            b.PublicAPI.br();
            using (b.Public.Partial.Class(NameOf.StyledString, $"{NameOf.StyledString}.{NameOf.IOnlyColor}").br()) {
                WriteCommonInterfaces(b.Root.CurrentCodeBlock.Name);
                // public StyledString White { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
                // public StyledString SmokingHot { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot); return this; } }
                WriteCommonProperties();
            }

            b.br();

            b.PublicAPI.br();
            using (b.Public.Static.Partial.Class(NameOf.Style).br()) {
                // public static StringStyle White => StringStyle.Create().PrepareColor(ColorType.White);
                // public static StringStyle SmokingHot => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
                WriteCommonProperties();
            }
        }

        return b.ToString();


        void WriteCommonProperties()
        {
            WriteCommonPropertyAuto("DefaultColor", MethodBodyOf.PrepareColor, true);
            foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareColor);
            b.br();

            foreach (StylizationOptionsData.ModifierCharacterField field in StylizationOptionsData.ModifierCharacterFields)
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareModifierCharacter);
        }

        void WriteCommonInterfaces(string returnType)
        {
            b.PublicAPI.br();
            using (b.Public.Interface(NameOf.IOnlyColor)) {
                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                    b.Public.Writeln($"{returnType} {field.trimmedName} {{ get; }}");
            }

            b.br();
        }

        void WriteCommonPropertyAuto(string propertyName, Func<string, string> getBody, bool isDefaultItem = false)
        {
            string itemName = isDefaultItem ? "Default" : propertyName;
            switch (b.Root.CurrentCodeBlock.Name) {
                case NameOf.StringStyle:
                    b.Public.Property(NameOf.StringStyle, propertyName).Expression(getBody(itemName));
                    break;
                case NameOf.StyledString:
                    b.Public.Property(NameOf.StyledString, propertyName)
                     .GetReturnThis("LastStyle." + getBody(itemName));
                    break;
                case NameOf.Style:
                    b.Public.Static.Property(NameOf.StringStyle, propertyName)
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
