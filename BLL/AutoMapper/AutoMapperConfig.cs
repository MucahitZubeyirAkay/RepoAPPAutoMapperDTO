using AutoMapper;
using DTOs;
using MODEL;

namespace AppUI.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Student, StudentSmallDTO>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Teacher, TeacherDTO>().ReverseMap();
        }
    }
}
