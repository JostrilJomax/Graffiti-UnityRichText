using UnityEngine;

namespace Graffiti {
/// <summary> ScriptableObject that contains one ColorPalette. </summary>
[CreateAssetMenu(fileName = FILE_NAME, menuName = "ScriptableObjects/" + FILE_NAME)]
public class ColorPaletteSo : ScriptableObject {

	private const string FILE_NAME = "Graffiti Color Palette";

	[field: SerializeField] public ColorPalette Palette { get; private set; } = new ColorPalette();
}
}
