using Company.DTO;
using Company.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }


        [HttpGet("all")]
        public async Task<ResponseDTO<IEnumerable<DepartmentDTO>>> GetAllDepartments() {
        
            ResponseDTO<IEnumerable<DepartmentDTO>> response= new ResponseDTO<IEnumerable<DepartmentDTO>>();
            try
            {
                response.Data = await _departmentService.GetAll();
                response.IsSuccess = true;
                response.Message = "Listado!!!";
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        
        }


        [HttpPost]
        public async Task<ResponseDTO<bool>> Create(DepartmentDTO department)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {
                await _departmentService.New(department);
                response.IsSuccess = true;
                response.Data = true;
                response.Message = "Creado!!!";
                return response;
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }


        [HttpPut("Id:int")]
        public async Task<ResponseDTO<bool>> Edit(DepartmentDTO department, int Id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {
                await _departmentService.Update(department, Id);
                response.IsSuccess= true;
                response.Message = "Actualizado!!!";

                return response;
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return response;
            }
        }

        [HttpGet("Id:int")]
        public async Task<ResponseDTO<DepartmentDTO>> Get(int Id)
        {
            ResponseDTO<DepartmentDTO> response = new ResponseDTO<DepartmentDTO>();
            try
            {

                response.Data = await _departmentService.Get(Id);
                response.IsSuccess = true;
                response.Message = "Elemento encontrado!!!";
                return response;

            }catch  (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }


        [HttpDelete("Id:int")]
        public async Task<ResponseDTO<bool>> Delete(int Id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {

                await _departmentService.Delete(Id);
                response.IsSuccess = true;
                response.Message = "Elemento borrado!!!";
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }


    }
}
