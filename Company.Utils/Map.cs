using AutoMapper;
using Company.DB.Model;
using Company.DTO;
using System.Runtime.CompilerServices;

namespace Company.Utils
{
    public class Map : Profile
    {
        public Map()
        {
            CreateMap<Worker, WorkerDTO>()
                .ForMember(x => x.Department, op=> op.MapFrom(WorkerDetail));
            CreateMap<Worker, WorkerResponseDTO>();
            CreateMap<WorkerDTO, Worker>();
            CreateMap<Department, DepartmentDTO>();
            CreateMap<Department, DepartmentResponseDTO>()
                .ForMember(dto=>dto.Workers, op => op.MapFrom(MapDepartmentWorkers));
            CreateMap<DepartmentDTO, Department>();
        }
    
    
        private DepartmentDTO WorkerDetail(Worker worker, WorkerDTO workerDTO)
        {
            var department = new DepartmentDTO()
            {
                ID = worker.Department.Id,
                Name = worker.Department.Name
            };
            return department;
        }

        private List<WorkerResponseDTO> MapDepartmentWorkers(Department department, DepartmentResponseDTO responseDTO)
        {
            var workerList = new List<WorkerResponseDTO>();
            
            if(department.Workers == null) return workerList;

            foreach(var worker in department.Workers)
            {
                workerList.Add(new WorkerResponseDTO()
                {
                    Id = worker.Id,
                    Name = worker.Name,
                    LastName = worker.LastName,
                });
            }

            return workerList;
        }
    }

}
