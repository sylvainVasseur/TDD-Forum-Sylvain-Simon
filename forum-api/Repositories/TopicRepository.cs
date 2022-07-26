using forum_api.DataAccess.DataObjects;

namespace forum_api.Repositories
{
    public class TopicRepository : ITopicRepository
    {
        private forumdbContext _context;

        public TopicRepository(forumdbContext context)
        {
            _context = context;
        }

        public virtual Topic FindById(int id)
        {
            Topic topic = _context.Topics.Find(id);
            if (topic != null)
            {
                topic.Comments = _context.Comments.Where(commentaire => commentaire.TopicIdtopic == topic.Idtopic).ToList();
            }
            return topic;
        }
        public IEnumerable<Topic> FindAllTopics()
        {
            List<Topic> topics = _context.Topics.ToList();

            if (topics != null)
            {
                foreach (var topic in topics)
                {
                    topic.Comments = _context.Comments.Where(com => com.TopicIdtopic == topic.Idtopic).ToList();
                }
            }
            return topics;
        }

        public Topic DeleteById(int id)
        {
            Topic topic = _context.Topics.Find(id);
            _context.Topics.Remove(topic);
            _context.SaveChanges();
            return topic;


        }
        public Topic UpdateTopic(Topic topic)
        {
            _context.Topics.Update(topic);
            _context.SaveChanges();
            return topic;
        }

        public Topic CreateTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            _context.SaveChanges();
            return topic;
        }
        
    }
}
