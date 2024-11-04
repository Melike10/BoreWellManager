using BoreWellManager.Business.Operations.Document;
using BoreWellManager.Business.Operations.Document.Dtos;
using BoreWellManager.WebApi.Filters;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddDocument(AddDocumentRequest request)
        {
            var documentdto = new AddDocumentDto
            {
                WellId= request.WellId,
                InstitutionId = request.InstitutionId,
                Type = request.Type,
                CustomerSubmissionDate = request.CustomerSubmissionDate,
                SignaturesReceived = request.SignaturesReceived,
                DeliveredToInstitution= request.DeliveredToInstitution,
                IsLienCertificate= request.IsLienCertificate,
                DocumentFee= request.DocumentFee,
                FeeReceived= request.FeeReceived,
                CreatedBy= request.CreatedBy,
                CustomerFullName= request.CustomerFullName,
                ReceivedFeeAmount= request.ReceivedFeeAmount

            };
            var res = await _documentService.AddDocument(documentdto);
            if (!res.IsSucceed)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }
        [HttpGet("/GetUserDocuments/{tc}")]
        public async Task<IActionResult> GetAllDocumetByTc(string tc)
        {
            var res = await _documentService.GetAllDocumetByTc(tc);
            if (res is null)
                return NotFound();
            return Ok(res);
        }
        [HttpGet("/GetWellDocuments/{wellId}")]
        [IsResponsibleFilter]
        public async Task<IActionResult> GetDocumentByWellId(int wellId)
        {
            var res = await _documentService.GetDocumentByWellId(wellId);
            if (res is null)
                return NotFound();
            return Ok(res);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentById(int id)
        {
            var res = await _documentService.GetDocumentById(id);
            if (res is null)
                return NotFound();
            return Ok(res);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangeDocumentInfo(int id, ChangeDocumentRequest request)
        {
            var documentDto = new UpdateDocumentDto
            {
                DeliveredToInstitution = request.DeliveredToInstitution,
                InstitutionSubmissionDate = request.InstitutionSubmissionDate,
                ModifiedBy = request.ModifiedBy
            };
            var res = await _documentService.ChangeDocumentInfo(id, documentDto);
            if(res.IsSucceed== false)
                return BadRequest(res.Message);
            return Ok(res.Data);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var res = await _documentService.DeleteDocument(id);
            if (res.IsSucceed == false)
                return BadRequest(res.Message);
            else
                return Ok();
        }
    }
}
