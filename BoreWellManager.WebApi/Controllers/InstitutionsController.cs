using BoreWellManager.Business.Operations.Institution;
using BoreWellManager.Business.Operations.Institution.Dtos;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionsController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;
        public InstitutionsController(IInstitutionService institutionService)
        {
            _institutionService = institutionService;

        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddInstitution(InstitutesRequest request)
        {
            var insDto = new InstitutionDto {
             City = request.City,
             Name = request.Name,
             Town = request.Town
            };
            var res = await _institutionService.AddInstitution(insDto);
            if(!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstitutionById(int id)
        {
            var institution = await _institutionService.GetByIdInstitution(id);
            if(institution is null)
                return NotFound();
            return Ok(institution);
        }

        [HttpGet("/GetAll")]

        public async Task<IActionResult> GetAllInstitutions()
        {
            var ins = await _institutionService.GetAllInstitutions();
            return Ok(ins);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangeAdressOfInstitution(int id, UpdateInstitutionRequest request)
        {
            var instDto = new UpdateInsDto
            {
                City = request.City,
                Town = request.Town
            };
            var res = await _institutionService.ChangeAdressInstitution(id, instDto);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteInstitution(int id)
        {
            var res = await _institutionService.DeleteInstitution(id);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok();
        }

    }
}
