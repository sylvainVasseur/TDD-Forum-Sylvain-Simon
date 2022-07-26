using forum_api.DataAccess.DataObjects;

namespace forum_api.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private forumdbContext _context;

        public CommentRepository(forumdbContext context)
        {
            _context = context;
        }
        public List<Comment> FindByTopicId(int TpId)
        {
            List<Comment> comments = _context.Comments.Where(c => c.TopicIdtopic == TpId).ToList();
            return comments;
        }
        public Comment FindById(int id)
        {
            var comment = _context.Comments.SingleOrDefault(c => c.IdComment == id);
            if (comment == null)
            {
                throw new Exception($"Aucun comment avec l'id {id}, n'a été trouvé.");
            }
            else
                return comment;
        }

        public Comment DeleteById(int id)
        {
            var comment = _context.Comments.SingleOrDefault(c => c.IdComment == id);
            if (comment == null)
            {
                throw new Exception($"Aucun comment avec l'id {id}, n'a été trouvé.");
            }
            else
            {
                _context.Comments.Remove(comment);
                _context.SaveChanges();
                return comment;
            }
        }
        public Comment UpdateComment( Comment comment)
        {
            _context.Comments.Update(comment);
            _context.SaveChanges();
            return comment;
        }

        public Comment CreateComment(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
            return comment;
        }
    }
}
