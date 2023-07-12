// This file is NOT generated.

using Graffiti.Internal;

namespace Graffiti {
public partial class StyledString {

    /// <summary>
    ///     • This indexer allows you to <b> add style directly to modifier character </b> (see
    ///     <see cref="ModifierCharacterSet"/>). <br/>It means you can add <i> Color/Gradient/etc. </i> to
    ///     <i> Underline/Strikethrough/etc. </i>
    ///     <para/>
    ///     • Requirement is that <b> indexer have to be after modifier character </b> <br/><i> (see correct usage below) </i>.
    ///     <br/>Otherwise you will add style to text <br/><i> (see incorrect usage below) </i>.
    /// </summary>
    /// <example>
    ///     <b> Correct usage: </b> (will make your text Blue with Yellow Underline)
    ///     <code> "Some Cool Text".Stylize().Blue.Underline[Style.Yellow]; </code>
    ///     <i>
    ///         <b> Incorrect usage: </b>
    ///     </i>
    ///     (will make your text Gradient from Blue to Yellow and with regular Underline)
    ///     <code> "Some Cool Text".Stylize().Underline.Blue[Style.Yellow]; </code>
    /// </example>
    /// <remarks>
    ///     • Example of indexer usage (text with Yellow Underline): <br/>"Some Cool
    ///     Text".Stylize().Underline[Style.Yellow];
    /// </remarks>
    public StyledString this[StringStyleCore style] {
        get {
            if (LastStyle.HasAnyModifierCharacter) {
                LastStyle.SetStyleTodModifierCharacter(style);
            }
            else {
                LastStyle.AddStyleCore(style);
            }

            return this;
        }
    }

    /// <inheritdoc cref="StyledString.this[StringStyleCore]"/>
    public StyledString this[StringStyle style] => this[(StringStyleCore) style];

}

public partial class StringStyle {

    /// <inheritdoc cref="StyledString.this[StringStyleCore]"/>
    /// <remarks> • Example of indexer usage (Yellow Underline style): <br/>var myStyle = Style.Underline[Style.Yellow]; </remarks>
    public StringStyle this[StringStyleCore style] {
        get {
            if (HasAnyModifierCharacter) {
                SetStyleTodModifierCharacter(style);
            }
            else {
                this.AddStyleCore(style);
            }

            return this;
        }
    }

    /// <inheritdoc cref="StringStyle.this[StringStyleCore]"/>
    public StringStyle this[StringStyle style] => this[(StringStyleCore) style];

}
}
