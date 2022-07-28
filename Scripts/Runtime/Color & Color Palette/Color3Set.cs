using System;
using Graffiti.Internal;
using UnityEngine;

namespace Graffiti {
/// <summary> Contains one color and it's darker/lighter variations </summary>
[Serializable]
public struct Color3Set {

	public enum Modifier {
		None,
		Dark,
		Light,
	}

	[field: SerializeField] public Color3 Value        { get; private set; }
	[field: SerializeField] public Color3 ValueDarker  { get; private set; }
	[field: SerializeField] public Color3 ValueLighter { get; private set; }

	public Color3Set(Color3 color) : this(color.UnityColor, color.Hex, color.ShortHex) { }

	public Color3Set(Color unityColor, string hex, string shortHex) {
		Value = new Color3 {
			UnityColor = unityColor,
			Hex = hex,
			ShortHex = shortHex,
		};

		ValueDarker = new Color3().MakeDarker(Value.UnityColor);
		ValueLighter = new Color3().MakeLighter(Value.UnityColor);
	}
}
}
