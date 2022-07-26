using Microsoft.AspNetCore.Mvc;
using forum_api.Services;
using forum_api.DataAccess.DataObjects;

namespace forum_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _service;

        public TopicController(ITopicService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Topic> FindAllTopics()
        {
            return _service.FindAllTopics();
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                return Ok(_service.FindById(id));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }


        [HttpPost]
        public IActionResult CreateTopic(Topic topic)
        {
            try
            {
                Topic topicCreation = _service.CreateTopic(topic);
                return CreatedAtAction(nameof(FindById), new { id = topicCreation.Idtopic }, topicCreation);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _service.DeleteById(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPut]
        public IActionResult UpdateTopic(Topic topic)
        {
            try
            {
                _service.UpdateTopic(topic);
                return Ok("Updated");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}