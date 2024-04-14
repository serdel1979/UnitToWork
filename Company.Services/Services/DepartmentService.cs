using AutoMapper;
using Company.DB.Model;
using Company.DTO;
using Company.Repository.Repository;
using Company.UnitOfWork.UOW;
using Company.Utils.Exceptions;
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
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IRepository<Department> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this._repository = repository;
            this.mapper = mapper;
            this._unitOfWork = unitOfWork;
        }
        public async Task Delete(int Id)
        {
            var query = _unitOfWork.DepartmentRepository.Find(c=>c.Id == Id);
            var fromModel = await query.FirstOrDefaultAsync();
            if (fromModel is null)
            {
                throw new KeyNotFoundException();
            }
            await _unitOfWork.DepartmentRepository.Delete(fromModel);
            await _unitOfWork.SaveAsync();
        }

        public IQueryable<DepartmentDTO> FindByName(string Name)
        {

            var request = _unitOfWork.DepartmentRepository.Find(p => p.Name.ToLower().Equals(Name.ToLower()));
            var fromModel = request.FirstOrDefaultAsync();
            IQueryable<DepartmentDTO> depstos = mapper.Map<IQueryable<DepartmentDTO>>(fromModel);
            return depstos.AsQueryable();
        }

        public async Task<DepartmentResponseDTO> Get(int Id)
        {
            var query = _unitOfWork.DepartmentRepository.Find(p => p.Id == Id)
                .Include(d=>d.Workers);
            var find = await query.FirstOrDefaultAsync();
            if (find is null)
            {
                throw new KeyNotFoundException($"No existe el Id {Id}");
            }

            return mapper.Map<DepartmentResponseDTO>(find);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            var depts = await _unitOfWork.DepartmentRepository
                           .Find()
                           .ToListAsync();

            return depts.Select(d => mapper.Map<DepartmentDTO>(d));
        }


        public async Task New(DepartmentDTO department)
        {
            var request = _unitOfWork.DepartmentRepository.Find(p => p.Name.ToLower().Equals(department.Name.ToLower()));
            var fromModel = await request.FirstOrDefaultAsync();

            if(fromModel is not null)
            {
                throw new DuplicateNameException($"Ya existe un departamento con el nombre {department.Name}!!!");
            }

            Department newDep = mapper.Map<Department>(department);
            await _unitOfWork.DepartmentRepository.Create(newDep);
            await _unitOfWork.SaveAsync();
        }





        public async Task Update(DepartmentDTO department, int Id)
        {
            var byId = await _unitOfWork.DepartmentRepository.Find(c => c.Id == Id).FirstOrDefaultAsync();
            if (byId == null)
            {
                throw new KeyNotFoundException($"No existe el id {Id}");
            }

            var byName = await _unitOfWork.DepartmentRepository.Find(c => c.Name.ToLower() == department.Name.ToLower()).FirstOrDefaultAsync();
            if (byName != null && byName.Id != Id)
            {
                throw new DuplicateNameException($"Ya existe el nombre {department.Name}");
            }

            byId.Name = department.Name;
            await _unitOfWork.DepartmentRepository.Update(byId);
            await _unitOfWork.SaveAsync();
        }

    }
}
