using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti {
/// <summary> ScriptableObject that contains one ColorPalette. </summary>
[CreateAssetMenu(fileName = ScriptableObjectName, menuName = GraffitiInfo.AssetMenu.Prefix + ScriptableObjectName)]
public class ColorPaletteSo : ScriptableObject {

	private const string ScriptableObjectName = "Graffiti Color Palette";

	[NotNull]
	[field: SerializeField] public ColorPalette Palette { get; private set; } = new ColorPalette();
}
}
