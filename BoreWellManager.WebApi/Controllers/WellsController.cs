using BoreWellManager.Business.Operations.Land;
using BoreWellManager.Business.Operations.Well;
using BoreWellManager.Business.Operations.Well.Dtos;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellsController : ControllerBase
    {
        private readonly IWellService _wellService;
        public WellsController(IWellService wellService)
        {
            _wellService = wellService; 
        }
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddWell(WellRequest request)
        {
            var wellDto = new AddWellDto
            {
                XCordinat= request.XCordinat,
                YCordinat= request.YCordinat,
                UserId= request.UserId,
                LandId= request.LandId
            };
            var res = await _wellService.AddWell(wellDto);
            if(!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok("Kuyu eklenmiştir.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdWell(int id)
        {
            var well = await _wellService.GetById(id);
            if (well is null)
                return NotFound();
            return Ok(well);
        }

        [HttpPatch("{id}/WellMeasure")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangeWellMeasure(int id,AddWellMeasureRequest request)
        {
            var wellMeasureDto = new AddWellMeasureDto {
             Debi=request.Debi??0,
             StaticLevel = request.StaticLevel??0,
             DynamicLevel=request.DynamicLevel??0
            };
            var res = await _wellService.ChangeWellMeasureById(id, wellMeasureDto);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> UpdateWell(int id,UpdateWellRequest request)
        {
            var updateWellDto = new UpdateWellDto { 
             LandId=request.LandId,
             UserId=request.UserId,
             XCordinat=request.XCordinat,
             YCordinat=request.YCordinat,
             Debi=request.Debi,
             StaticLevel=request.StaticLevel,
             DynamicLevel=request.DynamicLevel
            };
            var res = await _wellService.UpdateWell(id, updateWellDto);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteWell(int id)
        {
            var result = await _wellService.DeleteWell(id);
            if (!result.IsSucceed)
                return NotFound();

            return Ok();
        }
    }
}
