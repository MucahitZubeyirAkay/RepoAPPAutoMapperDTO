using DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Operation.Interface
{
    public interface IStudentService : IGenericService<StudentDTO>
    {
        Task<string> UploadFile(IFormFile file);
    }
}
