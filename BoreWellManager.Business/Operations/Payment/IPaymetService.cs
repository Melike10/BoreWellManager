using BoreWellManager.Business.Operations.Payment.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Payment
{
    public interface IPaymetService
    {
        public Task<ServiceMessage<PaymentEntity>> ChangePaymentFee(ChangePaymentDto changePaymentDto);
        public Task<List<PaymentEntity>> GetAllPayments();
        public Task<List<PaymentEntity>> GetByName(string name);
        public Task<PaymentEntity> GetById(int id);
        public Task<ServiceMessage> Delete(int id);
        public Task<ServiceMessage<PaymentEntity>> AddPayment(AddPaymentDto dto);
    }
}
