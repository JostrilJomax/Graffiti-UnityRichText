using System;
using JetBrains.Annotations;

namespace Graffiti.CodeGeneration.Internal.Helpers {
internal static class GenericExtensions {

    public static TReturn Return<T, TReturn>(this T self, TReturn return_) => return_;

}
}
