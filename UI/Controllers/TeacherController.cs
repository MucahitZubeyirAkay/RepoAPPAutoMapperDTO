using BLL.Operation.Interface;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="manager,teacher,admin")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> GetAll()
        {
            var teacher = await _teacherService.GetAll();
            return Ok(teacher);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetById(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        [Authorize(Roles = "manager,admin")]

        public async Task<IActionResult> Add([FromBody] TeacherDTO teacherDTO)
        {
            await _teacherService.Create(teacherDTO);
            return CreatedAtAction(nameof(GetById), new { id = teacherDTO.Id }, teacherDTO);

        }

        [HttpPut]
        [Authorize(Roles = "manager,admin")]

        public async Task<IActionResult> Update(int id, TeacherDTO teacherDTO)
        {
            if (id != teacherDTO.Id)
            {
                return BadRequest();
            }

            await _teacherService.Update(teacherDTO);
            return NoContent();
        }

        [HttpDelete]
        [Authorize(Roles = "manager,admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.Delete(id);
            return NoContent();
        }
    }
}
