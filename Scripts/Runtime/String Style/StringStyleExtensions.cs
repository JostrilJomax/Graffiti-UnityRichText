using Graffiti.Internal;

namespace Graffiti {
internal static class StringStyleExtensions {

    /// <inheritdoc cref="StringStyleExtensions.MergeStyleCores"/>
    internal static void MergeStyles(this StringStyle self, StringStyle other)
    {
        MergeStyleCores(self, other);
        if (other.HasModifierCharacterSet) {
            self.ModifierCharacterSet.MergeWith(other.ModifierCharacterSet);
        }
    }

    /// <remarks>
    ///     Style [<see cref="first"/>] is the highest priority. This means that if it has some modifier, that modifier
    ///     won't be replaced by the same modifier of [<see cref="second"/>] style.
    /// </remarks>
    internal static void MergeStyleCores(StringStyleCore first, StringStyleCore second)
    {
        // Note (1):
        // Check for '!self.HasColor' in gradient preparation is required to prevent scenarios
        // in which gradient is applied despite the fact that the style already has a color that
        // should be dominant, since it was applied first

        // Example (1):
        // "Some Printed Text".Stylize().Red.Blue.And(1).White;
        // In the example word 'Printed' is expected to be White
        // (styles are processed from the end, so the last style is dominant)

        if (!first.HasColor && !first.HasGradient && second.HasGradient) // (1)
        {
            first.PrepareGradient(second.Gradient);
        }

        if (!first.HasColor && second.HasOnlyOneColor) {
            first.PrepareColor(second.Color.GetColorHexValue());
        }

        if (!first.HasSize && second.HasSize) {
            first.PrepareSize(second.SizeValue);
        }

        if (!first.HasFont && second.HasFont) {
            first.PrepareFontStyle(second.FontStyle);
        }
    }

    /// <summary> Add each modifier from [<see cref="other"/>] style to [<see cref="self"/>] style. </summary>
    internal static void AddStyleCore(this StringStyleCore self, StringStyleCore other)
    {
        if (!self.HasCachedGradient && other.HasCachedGradient) {
            self.PrepareGradient(other.Gradient);
        }

        if (other.CollectedColors != null) {
            foreach (StringStyleColor color in other.CollectedColors)
                self.PrepareColor(color.GetColorHexValue());
        }

        if (other.HasOnlyOneColor) {
            self.PrepareColor(other.Color.GetColorHexValue());
        }

        if (other.HasSize) {
            self.PrepareSize(other.SizeValue);
        }

        if (other.HasFont) {
            self.PrepareFontStyle(other.FontStyle);
        }
    }

    internal static void MergeWith(this ModifierCharacterSet self, ModifierCharacterSet other)
    {
        if (!GraffitiProperties.Config.AllowMultipleModifierCharacters && self.HasAnyModifierCharacter) {
            return;
        }

        foreach (ModifierCharacter otherChar in other.ModifierCharacters)
            if (!HasModifierCharacterType(otherChar.Type)) {
                self.ModifierCharacters.Add(new ModifierCharacter { Type = otherChar.Type, Style = otherChar.Style });
            }

        bool HasModifierCharacterType(ModifierCharacterType type)
        {
            foreach (ModifierCharacter selfChar in self.ModifierCharacters)
                if (selfChar.Type == type) {
                    return true;
                }

            return false;
        }
    }

}
}
