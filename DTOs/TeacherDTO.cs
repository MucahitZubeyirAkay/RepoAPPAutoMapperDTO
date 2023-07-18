using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TeacherDTO
    {
        public int Id { get; set; }
        public string TeacherName { get; set; }
        public string TeacherSurname { get; set; }

    }
}
