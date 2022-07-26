using Microsoft.VisualStudio.TestTools.UnitTesting;
using forum_api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using forum_api.Repositories;
using Moq;
using forum_api.DataAccess.DataObjects;

namespace forum_api.Services.Tests
{
    [TestClass()]
    public class CommentServiceTests
    {
        private IWordFilterService _wordfilterService;
        private ICommentService _commentService;
        private Mock<ICommentRepository> _commentRepository;
        private ITopicService _topicService;
        private Mock<ITopicRepository> _topicRepository;
        private Topic expectedTopic;
        private Comment expectedComment;
        private List<Comment> ListComment { get; set; }
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
                Comments = new List<Comment>()
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
            ListComment = new List<Comment>() { expectedComment, expectedComment };
        }

        [TestMethod()]
        public void CreateCommentTest_IdOk_Comment()
        {
            //GIVE
            _commentRepository.Setup(s => s.CreateComment(It.IsAny<Comment>())).Returns(expectedComment);
            //WHEN
            Comment comment = _commentService.CreateComment(expectedComment);
            //THEN
            Assert.IsInstanceOfType(comment, typeof(Comment));
            Assert.IsNotNull(comment.CreationDate);
            Assert.IsNotNull(comment.ModificationDate);
            Assert.AreEqual(expectedComment, comment);
            Assert.AreEqual(expectedComment.IdComment, comment.IdComment);
        }

        [TestMethod()]
        [DataRow((Comment)null)]
        public void CreateCommentTest_IdNok_ThrowException(Comment comment)
        {
            //GIVE
            _commentRepository.Setup(s => s.CreateComment(It.IsAny<Comment>())).Returns((Comment)null);
            //Then
            Assert.ThrowsException<Exception>(() => _commentService.CreateComment(comment));
        }

        [TestMethod()]
        [DataRow(1)]
        public void DeleteCommentTest_IdOK_DeleteComment(int id)
        {
            //Given
            _commentRepository.Setup(s => s.DeleteById(It.IsAny<int>())).Returns(expectedComment);
            //WHEN
            Comment comment = _commentService.DeleteComment(id);
            Assert.AreEqual(expectedComment, comment);
        }

        [TestMethod()]
        [DataRow(1)]
        public void FindByTopicIdTest_TopicIdOk_ReturnComment(int id)
        {
            //Given
            _commentRepository.Setup(s => s.FindByTopicId(It.IsAny<int>())).Returns(ListComment);
            //When
            List<Comment> comment = _commentService.FindByTopicId(id);
            //Then
            Assert.IsInstanceOfType(comment, typeof(List<Comment>));
            Assert.AreEqual(ListComment, comment);
        }

        [TestMethod()]
        [DataRow(1)]
        public void FindByIdTest_IdOk_ReturnComment(int id)
        {
            //Given
            _commentRepository.Setup(s => s.FindById(It.IsAny<int>())).Returns(expectedComment);
            //When
            Comment comment = _commentService.FindById(id);
            //Then
            Assert.IsInstanceOfType(comment, typeof(Comment));
            Assert.AreEqual(expectedComment, comment);
        }


        [TestMethod()]
        [DataRow(18)]
        public void FindByIdTest_IdNull_ThrowException(int id)
        {
            //Given
            _commentRepository.Setup(s => s.FindById(It.IsAny<int>())).Returns((Comment)null);
            
            //Then
            Assert.ThrowsException<Exception>(() => _commentService.FindById(id));
        }

        [TestMethod()]
        [DataRow(1)]
        public void FindByTopicIdTest_IdOk_ReturnList(int id)
        {
            //Given
            _commentRepository.Setup(s => s.FindByTopicId(It.IsAny<int>())).Returns(new List<Comment>() { expectedComment});
            //WHEN
             List<Comment> comment = _commentService.FindByTopicId(id);
            //THEN
            Assert.IsInstanceOfType(comment, typeof(List<Comment>));
            Assert.AreEqual(1, comment.Count());
        }

        [TestMethod()]
        public void UpdateCommentTest_CommentOK_UpdateModificationDate()
        {
            //Given
            _commentRepository.Setup(r => r.UpdateComment(It.IsAny<Comment>())).Returns(expectedComment);
            //WHEN
            Comment comment = _commentService.UpdateComment(expectedComment);
            //THEN
            Assert.IsInstanceOfType(comment, typeof(Comment));
            Assert.AreEqual(expectedComment, comment);
            Assert.IsNotNull( comment.ModificationDate);
            Assert.AreNotEqual(BaseDateTime, comment.ModificationDate);
        }
        [TestMethod()]
        public void UpdateCommentTest_CommentNOK_ThrowException()
        {
            //Given
            _commentRepository.Setup(r => r.UpdateComment(It.IsAny<Comment>())).Returns((Comment)null);
            //WHEN
            Assert.ThrowsException<Exception>(() => _commentService.UpdateComment((Comment)null));
        }
    }
}