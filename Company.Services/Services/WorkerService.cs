using AutoMapper;
using Company.DB.Model;
using Company.DTO;
using Company.Repository.Repository;
using Company.UnitOfWork.UOW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Company.Services.Services
{
    public class WorkerService : IWorkerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task Delete(int Id)
        {
            var worker = _unitOfWork.WorkerRepository.Find(w => w.Id == Id).FirstOrDefault();
            if(worker is null)
            {
                throw new KeyNotFoundException($"No existe trabajador con id {Id}");
            }
            await _unitOfWork.WorkerRepository.Delete(worker);
            await _unitOfWork.SaveAsync();
        }

        public IQueryable<WorkerDTO> FindByName(string Name)
        {
            var query = _unitOfWork.WorkerRepository.Find(w=>w.Name.ToLower().Contains(Name.ToLower())); 
            return _mapper.Map<IQueryable<WorkerDTO>>(query.AsQueryable());
        }

        public async Task<WorkerDTO> Get(int Id)
        {
            var query = _unitOfWork.WorkerRepository.Find(p => p.Id == Id);
            var find = await query.FirstOrDefaultAsync();
            if (find is null)
            {
                throw new KeyNotFoundException($"No existe el Id {Id}");
            }

            return _mapper.Map<WorkerDTO>(find);
        }

        public async Task<IEnumerable<WorkerDTO>> GetAll()
        {
            var workers = await _unitOfWork.WorkerRepository.GetAll();
            return _mapper.Map<IEnumerable<WorkerDTO>>(workers.AsEnumerable());
        }

        public async Task New(WorkerDTO worker)
        {
            var findDepartment = await _unitOfWork.DepartmentRepository.Find(d => d.Id == worker.DepartmentId).FirstOrDefaultAsync();
            if (findDepartment is null)
            {
                throw new KeyNotFoundException($"No existe departamento con id {worker.DepartmentId}");
            }
            await _unitOfWork.WorkerRepository.Create(_mapper.Map<Worker>(worker));
            await _unitOfWork.SaveAsync();
        }

        public async Task Update(WorkerDTO worker, int Id)
        {
            var findById = await _unitOfWork.WorkerRepository.Find(w=>w.Id == Id).FirstOrDefaultAsync();
            if (findById is null)
            {
                throw new KeyNotFoundException($"No existe trabajador con Id {Id}");
            }
            var findDepartment = await _unitOfWork.DepartmentRepository.Find(d=>d.Id == worker.DepartmentId).FirstOrDefaultAsync();
            if(findDepartment is null)
            {
                throw new KeyNotFoundException($"No existe departamento con id {worker.DepartmentId}");
            }
            findById.Name = worker.Name;
            findById.LastName = worker.LastName;
            findById.DepartmentId = worker.DepartmentId;
            await _unitOfWork.WorkerRepository.Update(findById);
            await _unitOfWork.SaveAsync();
        }
    }
}
