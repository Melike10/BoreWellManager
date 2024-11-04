using BoreWellManager.Business.Operations.Land;
using BoreWellManager.Business.Operations.Land.Dtos;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BoreWellManager.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using BoreWellManager.WebApi.Filters;
namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandsController : ControllerBase
    {
        private readonly ILandService _landService;
        public LandsController(ILandService landService)
        {
           _landService=landService;
               
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLandById(int id)
        {
            var hasLand = await _landService.GetLand(id);

            if (hasLand == null)
                return NotFound();
            else
                return Ok(hasLand);



        }
        [HttpGet("userTc/{userTc}")]
        public async Task<IActionResult> GetLandByUserId(string userTc)
        {
            var hasLand = await _landService.GetUserLands(userTc);

            if (hasLand == null)
                return NotFound();
            else
                return Ok(hasLand);

        }

        [HttpPatch("{id}/hasLien")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangedLienStatus(int id,LienType lienType)
        {
            var result = await _landService.ChangeLienStatus(id,lienType);

            if(!result.IsSucceed)
                return NotFound();

            return Ok();

        }

        [HttpPost]
        [Authorize(Roles ="Employee")]
        public async Task<IActionResult> AddLand(LandRequest request)
        {
            var addLandDto = new AddLandDto
            {
                City = request.City,
                Town = request.Town,
                Block = request.Block,
                Plot = request.Plot,
                Street = request.Street,
                Location = request.Location,
                LandType = request.LandType,
                HasLien = request.HasLien,
                IsCksRequired = request.IsCksRequired,
                LienType = (LienType)request.LienType,
                UserIds = request.UserIds
            };
           var res = await _landService.AddLand(addLandDto);
            if (res.IsSucceed) {
                return Ok("Arazi başarıyla eklenmiştir.");
            }
            else { 
                return BadRequest(res.Message);
            }

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _landService.DeleteLand(id);
            if (!result.IsSucceed)
                return NotFound();

            return Ok();
        }

        [HttpPut("{id}")]
        [TimeControlFilter]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> UpdateLand(int id, UpdateLandRequest request)
        {
            var updateLandDto = new UpdateLandDto
            {
                Id = id,
                City = request.City,
                Town = request.Town,
                Street = request.Street,
                Location = request.Location,
                LandType = request.LandType,
                Block= request.Block,
                Plot = request.Plot,
                LienType = (LienType)(request.LienType),
                HasLien = request.HasLien,
                IsCksRequired = request.IsCksRequired,
                UserIds = request.UserIds
            };
            var res = await _landService.UpdateLand(updateLandDto);
            if (!res.IsSucceed) { return BadRequest(res.Message); }
            return await GetLandById(id);
        }
        }
    }
