using Microsoft.AspNetCore.Mvc;
using forum_api.Services;
using forum_api.DataAccess.DataObjects;

namespace forum_api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {

        private readonly ICommentService _service;

        public CommentController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult FindByTopicId(int TpId)
        {
            try
            {
                return Ok(_service.FindByTopicId(TpId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                return Ok(_service.FindById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            try
            {
                _service.CreateComment(comment);
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                _service.DeleteComment(id);
                return Ok("Deleted");
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut]
        public IActionResult Update(Comment comment)
        {
            try
            {
                this._service.UpdateComment(comment);
                return Ok("Updated");
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
    }
}
