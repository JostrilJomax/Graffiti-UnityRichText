using System;
using Graffiti.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti {
/// <summary>
///     This class is a style, that <b> contains modifiers </b> (Color/Gradient/Size/etc.) for text + it
///     <b> contains nested styles </b> for modifier characters (Underline/Strikethrough/etc.).
/// </summary>
/// <remarks>
///     In other words, this class allows you to change style of your text + change style of modifier characters
///     (Underline/Strikethrough/etc.). <br/> So, for example,
///     <b> you can make Blue text that is underlined by Bold Yellow line. </b>
/// </remarks>
[Serializable]
public partial class StringStyle : StringStyleCore {

    public override bool IsEmpty
        => IsScopeEmpty || IsEmpty_WithoutScope && (ModifierCharSet == null || !ModifierCharSet.HasAnyModifierCharacter);

    /// <summary>
    ///     Modifier character is a special character that modifies the character that precedes it. For example, there
    ///     are such modifier characters as: <b> Underline, Strikethrough </b>, etc.
    /// </summary>
    /// <remarks>
    ///     Modifier characters can be combined, so you can have underline <b> and </b> strikethrough modification for
    ///     the same character.
    /// </remarks>
    internal bool HasAnyModifierCharacter => ModifierCharSet != null && ModifierCharSet.HasAnyModifierCharacter;

    internal bool HasGradientOnAnyModifierCharacter => HasAnyModifierCharacter && ModifierCharSet!.HasGradientOnAny();

    internal bool CanApplyStyleToModifierCharacter
        => ModifierCharSet != null && GraffitiProperties.Config.AllowModifierCharacterStyle;

    [SerializeField] [CanBeNull] internal ModifierCharacterSet ModifierCharSet;



    internal static StringStyle Create() => new StringStyle();

    internal new StringStyle PrepareSize(int size)
    {
        if (CanApplyStyleToModifierCharacter) {
            ModifierCharSet!.GetStyleOfLastModifier()?.PrepareSize(size);
            return this;
        }

        if (!GraffitiProperties.Config.ApplySize) return this;
        base.PrepareSize(size);
        return this;
    }

    internal new StringStyle PrepareFontStyle(UnityBuildInFontStyleType fontStyle)
    {
        if (CanApplyStyleToModifierCharacter) {
            ModifierCharSet!.GetStyleOfLastModifier()?.PrepareFontStyle(fontStyle);
            return this;
        }

        if (!GraffitiProperties.Config.ApplyFontStyle) return this;
        base.PrepareFontStyle(fontStyle);
        return this;
    }

    internal new StringStyle PrepareColor(ColorType gffColor) => __PrepareColor(gffColor, null);
    internal new StringStyle PrepareColor(string strColor)    => __PrepareColor(null, strColor);

    internal new StringStyle __PrepareColor(ColorType? gffColor, string strColor)
    {
        if (CanApplyStyleToModifierCharacter) {
            ModifierCharSet!.GetStyleOfLastModifier()?.__PrepareColor(gffColor, strColor);
            return this;
        }

        if (!GraffitiProperties.Config.ApplyColor) return this;
        if (HasNeverAppliedColor)
            base.__PrepareColor(gffColor, strColor);
        else
            PrepareColorAdditionally(gffColor, strColor);
        return this;
    }

    internal new StringStyle PrepareColorAdditionally(ColorType? gffColor, string strColor)
    {
        if (!GraffitiProperties.Config.ApplyGradient) return this;
        base.PrepareColorAdditionally(gffColor, strColor);
        return this;
    }

    internal new StringStyle PrepareColorModification(GffColor.Modifier modifier)
    {
        base.PrepareColorModification(modifier);
        return this;
    }

    internal new StringStyle PrepareGradient(Gradient gradient)
    {
        if (!GraffitiProperties.Config.ApplyGradient) return this;
        base.PrepareGradient(gradient);
        return this;
    }

    internal StringStyle PrepareModifierCharacter(ModifierCharacterType type)
    {
        if (!GraffitiProperties.Config.AllowModifierCharacters) return this;
        if (type == ModifierCharacterType.None) return this;
        ModifierCharSet ??= new ModifierCharacterSet();
        ModifierCharSet.SetModifierCharacter(type);
        if (CanApplyStyleToModifierCharacter)
            ModifierCharSet.GetStyleOfLastModifier()?.PrepareScope(Scope);
        return this;
    }

    internal void SetStyleTodModifierCharacter(StringStyleCore style)
    {
        ModifierCharSet ??= new ModifierCharacterSet();
        ModifierCharSet.SetStyleToLastAddedModifierCharacter(style);
    }
}
}
