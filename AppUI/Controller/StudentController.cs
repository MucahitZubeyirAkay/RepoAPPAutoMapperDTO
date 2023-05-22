using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Operation.Interface;
using System;
using System.Threading.Tasks;
using DTOs;

namespace AppUI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
           
                var student = await _studentService.GetAllStudents();
                return Ok(student);
           
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            
                var student = await _studentService.GetStudentById(id);
                if (student == null)
                {
                    return NotFound();
                }
                return Ok(student);
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO studentDTO)
        {
                await _studentService.CreateStudent(studentDTO);
                return CreatedAtAction(nameof(GetStudentById), new { id = studentDTO.Id }, studentDTO);
            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, StudentDTO studentDTO)
        {
           
                if (id != studentDTO.Id)
                {
                    return BadRequest();
                }

                await _studentService.UpdateStudent(studentDTO);
                return NoContent();
           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
                await _studentService.DeleteStudent(id);
                return NoContent();
           
        }
    }
}
