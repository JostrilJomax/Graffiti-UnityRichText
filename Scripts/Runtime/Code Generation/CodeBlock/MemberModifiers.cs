namespace Graffiti.CodeGeneration {
public class MemberModifiers {
	public bool IsPartial  { get; internal set; }
	public bool IsAbstract { get; internal set; }
	public bool IsInternal { get; internal set; }
	public bool IsPrivate  { get; internal set; }
	public bool IsPublic   { get; internal set; }
	public bool IsStatic   { get; internal set; }
	public void CopyTo(MemberModifiers to) {
		to.IsPartial  = IsPartial;
		to.IsAbstract = IsAbstract;
		to.IsInternal = IsInternal;
		to.IsPrivate  = IsPrivate;
		to.IsPublic   = IsPublic;
		to.IsStatic   = IsStatic;
	}
}
}
