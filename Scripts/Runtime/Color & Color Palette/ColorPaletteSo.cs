using UnityEngine;
using UnityEngine.Serialization;

namespace Graffiti {
/// <summary> ScriptableObject that contains one ColorPalette. </summary>
[CreateAssetMenu(fileName = RESOURCE_NAME, menuName = "ScriptableObjects/" + RESOURCE_NAME)]
public class ColorPaletteSo : ScriptableObject {

	private const string RESOURCE_NAME = "Graffiti Color Palette";

	public ColorPalette Palette => _colorPalette;

	[SerializeField]
	private ColorPalette _colorPalette = new ColorPalette();
}
}
