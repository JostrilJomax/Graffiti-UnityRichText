using System;
using JetBrains.Annotations;

namespace Graffiti.CodeGeneration {
public static class GenericExtensions {

    public static int For(this int self, [NotNull] Action<int> iterator)
    {
        for (int i = 0; i < self; i++)
            iterator.Invoke(i);
        return self;
    }

    public static TReturn Return<T, TReturn>(this T self, TReturn return_) => return_;

}
}
