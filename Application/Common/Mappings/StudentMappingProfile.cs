
using Application.Common.Contracts;
using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, StudentResponse>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId)).ReverseMap();

            CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();

            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
