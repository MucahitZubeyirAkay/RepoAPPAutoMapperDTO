using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Concrete
{
    public interface IFileRepository // generic yok
    {
        Task<string> UploadFile(IFormFile file);
    }
}
