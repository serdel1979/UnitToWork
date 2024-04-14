using Company.DB.Model;
using Company.DTO;
using Company.Services.Services;
using Company.Utils.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            this._workerService = workerService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseDTO<IEnumerable<WorkerResponseDTO>> response = new ResponseDTO<IEnumerable<WorkerResponseDTO>>
                {
                    Data = await _workerService.GetAll(),
                    Message = "Listado",
                    IsSuccess = true,
                };
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
        public async Task<IActionResult> NewWorker(WorkerDTO worker)
        {
            try
            {
                await _workerService.New(worker);
                ResponseDTO<bool> response = new ResponseDTO<bool>()
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"Creado {worker.Name} {worker.LastName}"
                };
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

        [HttpPut("Id")]
        public async Task<IActionResult> Update(WorkerDTO worker, int Id)
        {
            try
            {
                await _workerService.Update(worker,Id);
                ResponseDTO<bool> response = new ResponseDTO<bool>()
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"Actualizado {worker.Name} {worker.LastName}"
                };
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


        [HttpGet("Id:int")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                
                ResponseDTO<WorkerDTO> response = new ResponseDTO<WorkerDTO>()
                {
                    Data = await _workerService.Get(Id),
                    IsSuccess = true,
                    Message = "Datos del trabajador"
                };
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
            try
            {
                await _workerService.Delete(Id);
                ResponseDTO<bool> response = new ResponseDTO<bool>()
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"Borrado trabajador con id {Id}"
                };
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
