using forum_api.DataAccess.DataObjects;

namespace forum_api.Repositories
{
    public interface ICommentRepository
    {
        Comment FindById(int id);
        List<Comment> FindByTopicId(int TpId);
        Comment UpdateComment(Comment comment);
        Comment CreateComment(Comment comment);
        Comment DeleteById(int id);
    }
}
