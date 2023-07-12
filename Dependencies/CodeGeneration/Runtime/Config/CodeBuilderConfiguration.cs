using CodeGeneration.Internal.Helpers;
using JetBrains.Annotations;

namespace CodeGeneration {
[PublicAPI]
public class CodeBuilderConfiguration : CodeBuilderConfiguration.IRules{

    [PublicAPI]
    public interface IRules {

        public IRules CommentAll();

    }

    public IRules SetRules => this;

    public IRules CommentAll() => (DoCommentAll = true).Return(this);

    internal bool DoCommentAll { get; private set; }

}
}
