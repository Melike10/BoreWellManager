using BoreWellManager.Business.Operations.Payment;
using BoreWellManager.Business.Operations.Payment.Dtos;
using BoreWellManager.WebApi.Filters;
using BoreWellManager.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoreWellManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymetService _paymetService;
        public PaymentsController(IPaymetService paymetService)
        {
            _paymetService = paymetService;
        }

        [HttpPatch]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangePaymentFee(ChangePaymentRequest request)
        {
            var changePaymentDto = new ChangePaymentDto
            {
             CustomerFullName = request.CustomerFullName,
             DocumentId = request.DocumentId,
             IsInstallmentPayment=request.IsInstallmentPayment,
             InstallmentAmount=request.InstallmentAmount,
             LastPaymentDate = request.LastPaymentDate
            };
            var res = await _paymetService.ChangePaymentFee(changePaymentDto);
            if(res.IsSucceed == false)
            {
                return BadRequest(res.Message);
            }
            return Ok(res.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymetService.GetAllPayments();
            return Ok(payments);
        }

        [HttpGet("/CustomerName/{name}")]
        [IsResponsibleFilter]
        public async Task<IActionResult> GetPaymentsByName(string name)
        {
            var payments = await _paymetService.GetByName(name);
            return Ok(payments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymetService.GetById(id);
            return Ok(payment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var res = await _paymetService.Delete(id);
            if (res.IsSucceed == false) { return BadRequest(res.Message); }
            return Ok();
        }
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> AddPayment(AddPaymentRequest request)
        {
            var paymetDto = new AddPaymentDto
            {
                DocumentId = request.DocumentId,
                DepositorFullName=request.DepositorFullName,
                PaymentDate=request.PaymentDate,
                TotalAmount = request.TotalAmount,
                RemaningAmount=request.RemaningAmount,
                EmployeeWhoReceivedPayment = request.EmployeeWhoReceivedPayment,
                IsInstallmentPayment = request.IsInstallmentPayment,
                InstallmentAmount= request.InstallmentAmount??0,
                LastPaymentDate= request.LastPaymentDate??DateTime.Parse("1975-01-01")
            };
            var res = await _paymetService.AddPayment(paymetDto);
            if (res.IsSucceed == false) { return BadRequest(res.Message); }
            return Ok(res.Data);
        }
    }
}
