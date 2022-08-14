using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti {
/// <summary> ScriptableObject that contains one ColorPalette. </summary>
[CreateAssetMenu(fileName = ScriptableObjectName, menuName = GraffitiInfo.ScriptableObjectMenuPrefix + ScriptableObjectName)]
public class ColorPaletteSo : ScriptableObject {

	private const string ScriptableObjectName = "Graffiti Color Palette";

	[field: SerializeField] [NotNull] public ColorPalette Palette { get; private set; } = new ColorPalette();
}
}
