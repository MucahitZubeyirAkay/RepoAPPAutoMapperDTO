using BLL.Operation.Implementations;
using BLL.Operation.Interface;
using BLL.Validators;
using DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="manager,teacher,student,admin")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly StudentValidator _studentValidator;

        public StudentController(IStudentService studentService, StudentValidator studentValidator)
        {
            _studentService = studentService;
            _studentValidator = studentValidator;
        }


        [HttpGet]
        //[Authorize(Roles ="teacher,manager")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var student = await _studentService.GetAll();
            return Ok(student);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("Add")]
        [Authorize]
        public async Task<IActionResult> Add([FromBody] StudentDTO studentDTO)
        {
            var validationResult = await _studentValidator.ValidateAsync(studentDTO);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage);
                return BadRequest(errorMessages);
            }

            await _studentService.Create(studentDTO);
            return CreatedAtAction(nameof(GetById), new { id = studentDTO.Id }, studentDTO);

        }

        [HttpPut("Put")]
        [Authorize(Roles = "teacher,manager")]
        public async Task<IActionResult> Update(int id, StudentDTO studentDTO)
        {
            if (id != studentDTO.Id)
            {
                return BadRequest();
            }

            //User.Claims

            await _studentService.Update(studentDTO);
            return NoContent();
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "teacher,manager")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.Delete(id);
            return NoContent();
        }

        [HttpPost("Upload")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadFile([FromQuery]int id, [FromForm] StudentDTO studentDTO)
        {
            if(id!=studentDTO.Id)
            {
                return BadRequest("Geçersiz öğrenci.");
            }
            if(studentDTO.File == null || studentDTO.File.Length == 0)
            {
                return BadRequest("Dosya yüklenmedi.");
            }

            var fileName = await _studentService.UploadFile(studentDTO.File);
            //var fileName = await _fileRepository.UploadFile(studentDTO.File);
            studentDTO.Image = fileName;

            await _studentService.Update(studentDTO);
            return NoContent();
        }

        

        //[HttpPost("Upload")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Upload(int id, [FromForm] StudentDTO studentDTO)
        //{
        //    if (id != studentDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    if (studentDTO.File == null || studentDTO.File.Length == 0)
        //    {
        //        return BadRequest("Dosya yüklenmedi.");
        //    }

        //    var fileName = $"{Guid.NewGuid().ToString()}_{studentDTO.File.FileName}";
        //    var filePath = Path.Combine("Uploads", fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await studentDTO.File.CopyToAsync(stream);
        //    }

        //    studentDTO.Image = fileName;

        //    await _studentService.Update(studentDTO);
        //    return NoContent();
        //}



        //[HttpPost("UploadFile/{id}")]
        //[AllowAnonymous]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> UploadFile(int id, IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        try
        //        {
        //            // Dosyanın yükleneceği dizin
        //            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        //            if (!Directory.Exists(uploadPath))
        //                Directory.CreateDirectory(uploadPath);

        //            // Dosya adını ve yolu oluşturma
        //            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        //            var filePath = Path.Combine(uploadPath, fileName);

        //            // Dosyayı sunucuya kaydet
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            // Öğrenci nesnesini alma
        //            var student = await _studentService.GetById(id);

        //            if (student == null)
        //            {
        //                return NotFound();
        //            }

        //            // Öğrenciye dosya adını atama
        //            student.Image = fileName;

        //            // Öğrenciyi güncelleme
        //            await _studentService.Update(student);

        //            return Ok($"Dosya yüklendi: {fileName}");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Hata durumunda uygun yanıtı dönme
        //            return StatusCode(StatusCodes.Status500InternalServerError, $"Dosya yükleme hatası: {ex.Message}");
        //        }
        //    }
        //    else
        //    {
        //        // Dosya yüklenmediyse hata yanıtı dönme
        //        return BadRequest("Dosya yüklenmedi.");
        //    }
        //}


        //[HttpPost("UploadStudentImage")]
        //[Authorize]
        //public async Task<IActionResult> UploadStudentImage(int id, IFormFile file)
        //{
        //    try
        //    {
        //        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        //        var fileName = await _fileUploadService.UploadFile(file);

        //        var student = await _studentService.GetById(id);
        //        student.Image = fileName;

        //        await _studentService.Update(student);

        //        return Ok($"Dosya yüklendi: {fileName}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Dosya yükleme hatası: {ex.Message}");
        //    }
        //}



        //[HttpPost("UploadImage")]
        //public async Task<IActionResult> UploadImage(int id, IFormFile file)
        //{
        //    var student = await _studentService.GetById(id);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    if (file == null || file.Length == 0)
        //    {
        //        return BadRequest("Dosya seçilmedi");
        //    }

        //    var dosyaAdi = Guid.NewGuid().ToString() + "_" + file.FileName;
        //    var dosyaUzantisi = Path.Combine("Uploads", dosyaAdi);

        //    using (var stream = new FileStream(dosyaUzantisi, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }

        //    student.Image = dosyaUzantisi;

        //    await _studentService.Update(student);

        //    return Ok("Dosya yükleme işlemi başarılı.");

        //}

        //[HttpPost("UploadFile")]
        //[Authorize]
        //public async Task<IActionResult> UploadFile(int id, IFormFile file)
        //{

        //    if (file != null && file.Length > 0)
        //    {
        //        try
        //        {
        //            // Dosyanın yükleneceği dizin
        //            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        //            if (!Directory.Exists(uploadPath))
        //                Directory.CreateDirectory(uploadPath);

        //            // Dosya adını ve yolu oluşturma
        //            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        //            var filePath = Path.Combine(uploadPath, fileName);

        //            // Dosyayı sunucuya kaydet
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }

        //            // Öğrenci nesnesini alma
        //            var student = await _studentService.GetById(id);

        //            if (student == null)
        //            {
        //                return NotFound();
        //            }

        //            // Öğrenciye dosya adını atama
        //            student.Image = fileName;

        //            // Öğrenciyi güncelleme
        //            await _studentService.Update(student);

        //            return Ok($"Dosya yüklendi: {fileName}");
        //        }
        //        catch (Exception ex)
        //        {
        //            // Hata durumunda uygun yanıtı dönme
        //            return StatusCode(StatusCodes.Status500InternalServerError, $"Dosya yükleme hatası: {ex.Message}");
        //        }
        //    }
        //    else
        //    {
        //        // Dosya yüklenmediyse hata yanıtı dönme
        //        return BadRequest("Dosya yüklenmedi.");
        //    }
        //}



    }

}
