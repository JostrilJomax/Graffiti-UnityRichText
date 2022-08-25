namespace Graffiti.Internal.Helpers {
public static class GenericExtensions {

    public static TReturn Return<T, TReturn>(this T self, TReturn return_) => return_;

}
}
