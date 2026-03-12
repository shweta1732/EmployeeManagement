using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, Employee>();

            // DTO mappings
            CreateMap<Employee, EmployeeReadDto>();
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
        }
    }
}