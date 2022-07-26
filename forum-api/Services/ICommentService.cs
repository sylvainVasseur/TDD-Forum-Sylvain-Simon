using forum_api.DataAccess.DataObjects;

namespace forum_api.Services
{
    public interface ICommentService
    {
        Comment FindById(int id);
        List<Comment> FindByTopicId(int TpId);
        Comment CreateComment(Comment comment);
        Comment UpdateComment(Comment comment);
        Comment DeleteComment(int id);
    }
}
