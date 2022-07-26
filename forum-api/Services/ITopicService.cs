using forum_api.DataAccess.DataObjects;

namespace forum_api.Services
{
    public interface ITopicService
    {
        Topic FindById(int id);
        IEnumerable<Topic> FindAllTopics();
        Topic DeleteById(int id);
        Topic UpdateTopic(Topic topic);
        Topic CreateTopic(Topic topic);
    }
}
