using Company.DTO;
using Company.Services.Services;
using Company.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async Task<IActionResult> GetAllDepartments() {
        
            ResponseDTO<IEnumerable<DepartmentDTO>> response= new ResponseDTO<IEnumerable<DepartmentDTO>>();
            try
            {
                response.Data = await _departmentService.GetAll();
                response.IsSuccess = true;
                response.Message = "Listado!!!";
                return Ok(response);
            }
            catch (ExceptionManager ex)
            {
                return NotFound(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (TaskCanceledException ex)
            {
                return Conflict(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool> { IsSuccess = false, Message = "Ocurrió un error interno" });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO department)
        {
            try
            {
                await _departmentService.New(department);
                return Ok(new ResponseDTO<bool> { IsSuccess = true, Data = true, Message = "Creado!!!" });
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool> { IsSuccess = false, Message = "Ocurrió un error interno" });
            }
        }



        [HttpPut("Id:int")]
        public async Task<IActionResult> Edit(DepartmentDTO department, int Id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {
                await _departmentService.Update(department, Id);
                response.IsSuccess= true;
                response.Message = "Actualizado!!!";

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (DuplicateNameException ex)
            {
                return Conflict(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool> { IsSuccess = false, Message = "Ocurrió un error interno" });
            }
        }

        [HttpGet("Id:int")]
        public async Task<IActionResult> Get(int Id)
        {
            ResponseDTO<DepartmentDTO> response = new ResponseDTO<DepartmentDTO>();
            try
            {

                response.Data = await _departmentService.Get(Id);
                response.IsSuccess = true;
                response.Message = "Elemento encontrado!!!";
                return Ok(response);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool> { IsSuccess = false, Message = "Ocurrió un error interno" });
            }
        }


        [HttpDelete("Id:int")]
        public async Task<IActionResult> Delete(int Id)
        {
            ResponseDTO<bool> response = new ResponseDTO<bool>();
            try
            {

                await _departmentService.Delete(Id);
                response.IsSuccess = true;
                response.Message = "Elemento borrado!!!";
                return Ok(response);

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ResponseDTO<bool> { IsSuccess = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseDTO<bool> { IsSuccess = false, Message = "Ocurrió un error interno" });
            }
        }


    }
}
