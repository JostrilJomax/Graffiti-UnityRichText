using System;
using UnityEngine;

namespace Graffiti.Internal {
[Serializable]
internal struct StringStyleSize {
    [SerializeField] private int value;

    internal bool HasValue { get; private set; }

    internal int Value {
        get => value;
        set {
            //  Unity Console behaviour (Unity 2020.3.20f1):
            // 1) Console will draw font with negative value size as a tiny line on the bottom.
            // 2) Console will draw font with size of 0 as a void.
            // 3) Max font size in console is 500 (I have tested it, after 500 font does not grow).

            //  This clamp operation is unnecessary (Unity console can handle any value).
            //  I am clamping here to bring clearness, because if you set text size above 500 (say 501) you will see
            // font as it is 500 size, but in log file size won't be 500 (it will be 501)

            this.value = Mathf.Clamp(value, -1, 500);
            HasValue = true;
        }
    }
}
}
