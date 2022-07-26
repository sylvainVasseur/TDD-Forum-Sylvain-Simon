using forum_api.DataAccess.DataObjects;
using forum_api.Repositories;

namespace forum_api.Services
{
    public class TopicService : ITopicService
    {
        private ITopicRepository _repository;
        private IWordFilterService _wordFilterService;

        public TopicService(ITopicRepository repository, IWordFilterService wordFilterService)
        {
            _repository = repository;
            _wordFilterService = wordFilterService;
        }

        public Topic CreateTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new Exception($"Le topic est null.");
            }
            else
            {
                topic.CreationDate = DateTime.Now;
                topic.Title = _wordFilterService.WordFilterSentence(topic.Title);
                topic.Comments = new List<Comment>();
                return _repository.CreateTopic(topic);
            }
        }

        public Topic DeleteById(int id)
        {
            if (id == null)
            {
                throw new Exception($"Aucun topic avec l'id {id}, n'a été trouvé.");
            }
            else
            {
                _ = this.FindById(id);
                return _repository.DeleteById(id);
            }
            
        }

        public IEnumerable<Topic> FindAllTopics()
        {
            return _repository.FindAllTopics();
        }

        public Topic FindById(int id)
        {
            var topic = _repository.FindById(id);
            if (topic == null)
            {
                throw new Exception($"Aucun topic avec l'id {id}, n'a été trouvé.");
            }
            return topic;
        }

        public Topic UpdateTopic(Topic topic)
        {
            if (topic == null)
            {
                throw new Exception($"Mauvais topic, null.");
            }
            topic.ModificationDate = DateTime.Now;
            topic.Title = _wordFilterService.WordFilterSentence(topic.Title);

            return _repository.UpdateTopic(topic);
        }

    }
}
