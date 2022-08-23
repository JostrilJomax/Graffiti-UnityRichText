using System;
using Graffiti.CodeGeneration;

namespace Graffiti {
public static class StylizationOptionsGenerator {
    public static string GenerateCode()
    {
        var db = CodeBuilder.CreateDefaultBuilder();

        db.Header(nameof(StylizationOptionsGenerator)).br();

        db.Using("JetBrains.Annotations");
        db.Using("UnityEngine");
        db.Using(NameOf.Graffiti_Internal).br();

        using (db.Namespace(NameOf.Graffiti_Internal)) {
            db.Internal.Enum(
                NameOf.ColorType, Enum => {
                    Enum.WriteDefaultMember();
                    foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                        Enum.WriteMember(field.name);
                });
        }

        using (db.Namespace(NameOf.Graffiti)) {
            using (db.PublicAPI.br().Public.Partial.Class(NameOf.ColorPalette).br()) {
                // [field: SerializeField] public GffColor White { get; private set; } = new GffColor(new Color(0.93f, 0.93f, 0.93f), "#eee");
                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                    db.field_SerializeField.Public.Property(NameOf.GffColor, field.trimmedName)
                      .GetPrivateSet(MethodBodyOf.New.GffColor(field.UnityColor, field.ShortHexColor));
                db.br();

                db.Internal.Method(
                    NameOf.GffColor, NameOf.FindColor,
                    (type: NameOf.ColorType, name: "color"), Method => {
                        db.Switch(
                            Method.Params[0].name, Switch => {
                                Switch.WriteDefaultCase().br();
                                Switch.WriteCase($"{NameOf.ColorType}.Default")
                                      .Return(nameof(ColorPalette.DefaultConsoleColor));
                                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                                    Switch.WriteCase($"{NameOf.ColorType}.{field.trimmedName}")
                                          .Return(field.trimmedName);
                            });
                    });
            }

            db.br();

            using (db.PublicAPI.br().Public.Partial
                     .Class(NameOf.StringStyle, $"{NameOf.StringStyle}.{NameOf.IOnlyColor}").br()) {
                WriteCommonInterfaces(db.Root.CurrentCodeBlock.Name);
                // public StringStyle White => PrepareColor(ColorType.White);
                // public StringStyle SmokingHot => PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
                WriteCommonProperties();
            }

            db.br();

            using (db.PublicAPI.br().Public.Partial
                     .Class(NameOf.StyledString, $"{NameOf.StyledString}.{NameOf.IOnlyColor}").br()) {
                WriteCommonInterfaces(db.Root.CurrentCodeBlock.Name);
                // public StyledString White { get { LastStyle.PrepareColor(ColorType.White ); return this; } }
                // public StyledString SmokingHot { get { LastStyle.PrepareModifierCharacter(ModifierCharacterType.SmokingHot); return this; } }
                WriteCommonProperties();
            }

            db.br();

            using (db.PublicAPI.br().Public.Static.Partial.Class(NameOf.Style).br()) {
                // public static StringStyle White => StringStyle.Create().PrepareColor(ColorType.White);
                // public static StringStyle SmokingHot => StringStyle.Create().PrepareModifierCharacter(ModifierCharacterType.SmokingHot);
                WriteCommonProperties();
            }
        }

        return db.ToString();


        void WriteCommonProperties()
        {
            WriteCommonPropertyAuto("DefaultColor", MethodBodyOf.PrepareColor, true);
            foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareColor);
            db.br();

            foreach (StylizationOptionsData.ModifierCharacterField field in StylizationOptionsData.ModifierCharacterFields)
                WriteCommonPropertyAuto(field.trimmedName, MethodBodyOf.PrepareModifierCharacter);
        }

        void WriteCommonInterfaces(string returnType)
        {
            db.PublicAPI.br();
            using (db.Public.Interface(NameOf.IOnlyColor)) {
                foreach (StylizationOptionsData.ColorField field in StylizationOptionsData.ColorFields)
                    db.Public.Writeln($"{returnType} {field.trimmedName} {{ get; }}");
            }

            db.br();
        }

        void WriteCommonPropertyAuto(string propertyName, Func<string, string> getBody, bool isDefaultItem = false)
        {
            string itemName = isDefaultItem ? "Default" : propertyName;
            switch (db.Root.CurrentCodeBlock.Name) {
                case NameOf.StringStyle:
                    db.Public.Property(NameOf.StringStyle, propertyName).Expression(getBody(itemName));
                    break;
                case NameOf.StyledString:
                    db.Public.Property(NameOf.StyledString, propertyName)
                      .GetReturnThis("LastStyle." + getBody(itemName));
                    break;
                case NameOf.Style:
                    db.Public.Static.Property(NameOf.StringStyle, propertyName)
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
