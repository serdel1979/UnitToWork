﻿using AutoMapper;
using Company.DB.Model;
using Company.DTO;
using Company.Repository.Repository;
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

        public DepartmentService(IRepository<Department> repository, IMapper mapper)
        {
            this._repository = repository;
            this.mapper = mapper;
        }
        public async Task Delete(int Id)
        {
            var query = _repository.Find(c=>c.Id == Id);
            var fromModel = await query.FirstOrDefaultAsync();
            if (fromModel is null)
            {
                throw new KeyNotFoundException();
            }
            await _repository.Delete(fromModel);
            await _repository.Save();
        }

        public IQueryable<DepartmentDTO> FindByName(string Name)
        {

            var request = _repository.Find(p => p.Name.ToLower().Equals(Name.ToLower()));
            var fromModel = request.FirstOrDefaultAsync();
            IQueryable<DepartmentDTO> depstos = mapper.Map<IQueryable<DepartmentDTO>>(fromModel);
            return depstos.AsQueryable();
        }

        public async Task<DepartmentDTO> Get(int Id)
        {
            var query = _repository.Find(p => p.Id == Id);
            var find = await query.FirstOrDefaultAsync();
            if (find is null)
            {
                throw new KeyNotFoundException($"No existe el Id {Id}");
            }

            return mapper.Map<DepartmentDTO>(find);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetAll()
        {
            var depts = await _repository.GetAll();
            return depts.Select(d => mapper.Map<DepartmentDTO>(d));
        }


        public async Task New(DepartmentDTO department)
        {
            var request = _repository.Find(p => p.Name.ToLower().Equals(department.Name.ToLower()));
            var fromModel = await request.FirstOrDefaultAsync();

            if(fromModel is not null)
            {
                throw new TaskCanceledException($"Ya existe un departamento con el nombre {department.Name}!!!");
            }

            Department newDep = mapper.Map<Department>(department);
            await _repository.Create(newDep);
            await _repository.Save();
        }





        public async Task Update(DepartmentDTO department, int Id)
        {
            var byId = _repository.Find(c => c.Id == Id);
            var fromModel = await byId.FirstOrDefaultAsync();
            if (fromModel is null)
            {
                throw new TaskCanceledException($"No existe el id {Id}");
            }
            var byName = _repository.Find(c => c.Name.ToLower().Equals(department.Name.ToLower()));
            var fromModelbyName = await byName.FirstOrDefaultAsync();
            if (fromModelbyName is not null)
            {
                throw new DuplicateNameException($"Ya existe el nombre {department.Name}");
            }
            fromModel.Name = department.Name;

           // Department newDep = mapper.Map<Department>(department);
            await _repository.Update(fromModel);
            await _repository.Save();
        }
    }
}