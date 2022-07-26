using Microsoft.VisualStudio.TestTools.UnitTesting;
using forum_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using forum_api.Repositories;
using forum_api.DataAccess.DataObjects;

namespace forum_api.Services.Tests
{

    [TestClass()]
    public class TopicServiceTests
    {
        private IWordFilterService _wordfilterService;
        private ICommentService _commentService;
        private Mock<ICommentRepository> _commentRepository;
        private ITopicService _topicService;
        private Mock<ITopicRepository> _topicRepository;
        private Topic expectedTopic;
        private Comment expectedComment;
        private List<Topic> ListTopic { get; set; }
        private DateTime BaseDateTime;

        [TestInitialize]
        public void Initialize()
        {
            _wordfilterService = new WordFilterService();
            _topicRepository = new Mock<ITopicRepository>(MockBehavior.Strict);
            _topicService = new TopicService(_topicRepository.Object, _wordfilterService);

            _commentRepository = new Mock<ICommentRepository>(MockBehavior.Strict);
            _commentService = new CommentService(_commentRepository.Object, _topicService, _wordfilterService);
            BaseDateTime = new DateTime(2022, 07, 07);
            expectedTopic = new Topic()
            {
                Idtopic = 1,
                CreationDate = BaseDateTime,
                ModificationDate = BaseDateTime,
                Title = "Test new topic",
                CreatorName = "simon",
                Comments = new List<Comment>() { expectedComment }
            };
            expectedComment = new Comment()
            {
                IdComment = 1,
                Username = "simon",
                CreationDate = BaseDateTime,
                ModificationDate = BaseDateTime,
                Content = "Salut, commment ça va ?",
                TopicIdtopic = 1
            };
            ListTopic = new List<Topic>() { expectedTopic, expectedTopic };
        }

        [TestMethod()]
        public void CreateTopicTest()
        {
            //GIVE
            _topicRepository.Setup(s => s.CreateTopic(It.IsAny<Topic>())).Returns(expectedTopic);
            //WHEN
            Topic topic = _topicService.CreateTopic(expectedTopic);
            //THEN
            Assert.IsInstanceOfType(topic, typeof(Topic));
            Assert.IsNotNull(topic.CreationDate);
            Assert.IsNotNull(topic.ModificationDate);
            Assert.AreEqual(expectedTopic, topic);
            Assert.AreEqual(expectedTopic.Idtopic, topic.Idtopic);
        }

        [TestMethod()]
        [DataRow((Topic)null)]
        public void CreateCommentTest_IdNok_ThrowException(Topic topic)
        {
            //GIVE
            _topicRepository.Setup(s => s.CreateTopic(It.IsAny<Topic>())).Returns((Topic)null);
            //Then
            Assert.ThrowsException<Exception>(() => _topicService.CreateTopic(topic));
        }

        [TestMethod()]
        public void FindAllTopicsTest_retournList()
        {
            //Given
            _topicRepository.Setup(s => s.FindAllTopics()).Returns(ListTopic);
            //WHEN
            var topicList = _topicService.FindAllTopics();
            //THEN
            Assert.IsInstanceOfType(topicList, typeof(List<Topic>));
            Assert.AreEqual(ListTopic, topicList);
        }

        [TestMethod()]
        [DataRow(1)]
        public void FindByIdTest_IdOK_RetournUnTopic(int id)
        {
            //GIVEN
            _topicRepository.Setup(repo => repo.FindById(It.IsAny<int>())).Returns(expectedTopic);
            //WHEN
            Topic topic = _topicService.FindById(id);
            //THEN
            Assert.IsNotNull(topic);
            Assert.AreEqual(expectedTopic.Idtopic, topic.Idtopic);
        }

        [TestMethod()]
        [DataRow(1)]
        public void FindByIdTest_IdNOK_ThrowsException(int id)
        {
            //GIVEN
            _topicRepository.Setup(repo => repo.FindById(It.IsAny<int>())).Returns((Topic)null);

            //THEN
            Assert.ThrowsException<Exception>(() => _topicService.FindById(id));
        }

        [TestMethod()]
        public void UpdateTopicTest_TopicOK_UpdateDeModificationDate()
        {
            //Given
            _topicRepository.Setup(r => r.UpdateTopic(It.IsAny<Topic>())).Returns(expectedTopic);
            //WHEN
            Topic topic = _topicService.UpdateTopic(expectedTopic);
            //THEN
            Assert.IsInstanceOfType(topic, typeof(Topic));
            Assert.AreEqual(expectedTopic, topic);
            Assert.IsNotNull(topic.ModificationDate);
            Assert.AreNotEqual(BaseDateTime, topic.ModificationDate);
        }

        [TestMethod()]
        public void UpdateTopicTest_TopicNOK_ThrowsException()
        {
            //Given
            _topicRepository.Setup(r => r.UpdateTopic(It.IsAny<Topic>())).Returns((Topic)null);
            //WHEN
            Assert.ThrowsException<Exception>(() => _topicService.UpdateTopic((Topic)null));
        }
    }
}