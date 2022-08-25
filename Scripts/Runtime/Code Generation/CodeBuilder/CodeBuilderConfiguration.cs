namespace Graffiti.CodeGeneration {
public class CodeBuilderConfiguration : CodeBuilderConfiguration.IRules{

    public interface IRules {

        public IRules CommentAll();

    }

    public IRules SetRule => this;

    internal bool DoCommentAll { get; private set; }

    public IRules CommentAll() => (DoCommentAll = true).Return(this);

}
}
