using System;
using System.Collections.Generic;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary> This class allows you to add a <b> red underline </b> to your text, for example. </summary>
[Serializable]
internal class ModifierCharacterSet {
    [field: SerializeField] public bool                    HasAnyModifierCharacter { get; private set; }
    [field: SerializeField] public List<ModifierCharacter> ModifierCharacters      { get; private set; } = new List<ModifierCharacter>();


    public void SetModifierCharacter(ModifierCharacterType type)
    {
        if (type == ModifierCharacterType.None) {
            return;
        }

        if (!GraffitiProperties.Config.AllowMultipleModifierCharacters && HasAnyModifierCharacter) {
            return;
        }

        ModifierCharacters.Add(new ModifierCharacter { Type = type });
        HasAnyModifierCharacter = true;
    }

    public void SetStyleToLastAddedModifierCharacter(StringStyleCore style)
    {
        if (ModifierCharacters.Count == 0) {
            return;
        }

        ModifierCharacters[^1].Style = style;
    }

    public (char[], Gradient)[] Unpack()
    {
        var pairs = new (char[], Gradient)[ModifierCharacters.Count];

        for (int i = 0; i < ModifierCharacters.Count; i++)
            pairs[i] =
                    (ModifierCharacters[i].Render(),
                        ModifierCharacters[i].Style?.Gradient);
        return pairs;
    }
}
}
