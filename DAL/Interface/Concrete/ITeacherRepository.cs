using DAL.Interface.Abstract;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Concrete
{
    public interface ITeacherRepository : IGenericRepository<TeacherDTO>
    {
    }
}
