using CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;

namespace CodeGeneration {
/// <summary> Contains all code member info: access modifiers, class modifiers and so on. </summary>
[PublicAPI]
public class MemberInfo {

    internal string WillUsePublic    => (IsPublic     = true).Return("public");
    internal string WillUseInternal  => (IsInternal   = true).Return("internal");
    internal string WillUseProtected => (IsProtected  = true).Return("protected");
    internal string WillUsePrivate   => (IsPrivate    = true).Return("private");
    internal string WillUseStatic    => (IsStatic     = true).Return("static");
    internal string WillUsePartial   => (IsPartial    = true).Return("partial");
    internal string WillUseAbstract  => (IsAbstract   = true).Return("abstract");

    public bool IsPublic    { get; private set; }
    public bool IsInternal  { get; private set; }
    public bool IsProtected { get; private set; }
    public bool IsPrivate   { get; private set; }
    public bool IsStatic    { get; private set; }
    public bool IsPartial   { get; private set; }
    public bool IsAbstract  { get; private set; }

    internal void CopyMemberInfoTo(MemberInfo to)
    {
        to.IsPublic = IsPublic;
        to.IsInternal = IsInternal;
        to.IsProtected = IsProtected;
        to.IsPrivate = IsPrivate;
        to.IsStatic = IsStatic;
        to.IsPartial = IsPartial;
    }

}
}
