using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Graffiti.Internal {
/// <summary> Helper class to create Unity's Gradient class. </summary>
internal static class GradientBuilder {

    internal static Gradient CreateGradient([NotNull] IList<StringStyleColor> list)
    {
        var colors = new Color[list.Count];
        for (int i = 0; i < colors.Length; i++) colors[i] = list[i].GetUnityColor();
        return CreateGradient(colors);
    }

    internal static Gradient CreateGradient(Color[] list)
    {
        var gr = new Gradient();
        int length = list.Length > 8 ? 8 : list.Length;
        var grCK = new GradientColorKey[length];
        var grAK = new GradientAlphaKey[length];
        float timeStep = 1f / (length - 1);
        for (int i = 0; i < length; i++) {
            grCK[i].color = list[i];
            grCK[i].time = timeStep * i;
            grAK[i].alpha = grCK[i].color.a;
            grAK[i].time = grCK[i].time;
        }

        gr.SetKeys(grCK, grAK);
        return gr;
    }

}
}
