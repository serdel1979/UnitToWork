using AutoMapper;
using Company.DB.Model;
using Company.DTO;

namespace Company.Utils
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Worker,WorkerDTO>().ReverseMap();
            CreateMap<Department, DepartmentDTO>().ReverseMap();
        }
    }
}
