using BoreWellManager.Business.Operations.Payment.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using BoreWellManager.Data.Repository;
using BoreWellManager.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Payment
{
    public class PaymentManager : IPaymetService
    {
        private readonly IRepository<PaymentEntity> _paymentRepository;
        private readonly IRepository<DocumentEntity> _documentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentManager(IUnitOfWork unitOfWork, IRepository<PaymentEntity> paymentRepository, IRepository<DocumentEntity> documentRepository)
        {
            _documentRepository = documentRepository;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }
        public async Task<ServiceMessage<PaymentEntity>> AddPayment(AddPaymentDto dto)
        {
            var payment = await _paymentRepository.GetAll(p => p.DocumentId == dto.DocumentId && p.DepositorFullName.ToLower() == dto.DepositorFullName.ToLower()).FirstOrDefaultAsync();
            if (payment is not null)
            {
                return new ServiceMessage<PaymentEntity> { IsSucceed = false, Message = "Zaten sistem de böyle bir kayıt mevcut" };
            }
            var doc = await _documentRepository.GetAll(d => d.Id == dto.DocumentId).FirstOrDefaultAsync();
            if (doc is null)
            {
                return new ServiceMessage<PaymentEntity> { IsSucceed = false, Message = "Sistem de girdiğiniz belge id'ye sahip bir kayıt bulunamadı" };
            }
            await _unitOfWork.BeginTransaction();
            var paymentEntitiy = new PaymentEntity
            {
                DocumentId = dto.DocumentId,
                DepositorFullName = dto.DepositorFullName,
                PaymentDate = dto.PaymentDate,
                TotalAmount = dto.TotalAmount,
                RemaningAmount = dto.RemaningAmount,
                EmployeeWhoReceivedPayment = dto.EmployeeWhoReceivedPayment,
                IsInstallmentPayment = dto.IsInstallmentPayment,
                InstallmentAmount = dto.InstallmentAmount,
                LastPaymentDate = dto.LastPaymentDate
            };

            _paymentRepository.Add(payment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ödeme bilgileri kaydedilirken hata oluştu");
            }

            doc.PaymentId = payment.Id;
            doc.FeeReceived = payment.RemaningAmount == 0 ? true : false;
            doc.ModifiedBy = payment.EmployeeWhoReceivedPayment;
            _documentRepository.Update(doc);
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();

            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ödeme bilgilerinin belgeleri güncellenirken hata oluştu");
            }

            return new ServiceMessage<PaymentEntity> { IsSucceed = true, Data = payment };
        }

        public async Task<ServiceMessage<PaymentEntity>> ChangePaymentFee(ChangePaymentDto changePaymentDto)
        {
            var payment = await _paymentRepository.GetAll(x => x.DocumentId == changePaymentDto.DocumentId && x.DepositorFullName.ToLower() == changePaymentDto.CustomerFullName.ToLower()).FirstOrDefaultAsync();
            if (payment is null)
                return new ServiceMessage<PaymentEntity> { IsSucceed = false, Message = "İlgili kişiye ait belge bulunamadı" };
            await _unitOfWork.BeginTransaction();
            payment.IsInstallmentPayment = changePaymentDto.IsInstallmentPayment;
            payment.InstallmentAmount = changePaymentDto.InstallmentAmount;
            payment.LastPaymentDate = changePaymentDto.LastPaymentDate;
            payment.RemaningAmount = payment.RemaningAmount - changePaymentDto.InstallmentAmount;
            _paymentRepository.Update(payment);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belgenin taksit bilgileri güncellenemedi");
            }

            var doc = await _documentRepository.GetAll(x => x.Id == changePaymentDto.DocumentId).FirstOrDefaultAsync();
            if (payment.RemaningAmount == 0)
            {
                doc.FeeReceived = true;

            }
            doc.ModifiedBy = payment.EmployeeWhoReceivedPayment;
            _documentRepository.Update(doc);
            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belgenin taksit bilgileri güncellenemedi");

            }
            return new ServiceMessage<PaymentEntity> { IsSucceed = true, Data = payment };
        }

        public async Task<ServiceMessage> Delete(int id)
        {
            var payment = _paymentRepository.GetById(id);
            var doc = await _documentRepository.GetAll(x => x.PaymentId == payment.Id).FirstOrDefaultAsync();
            if (payment is null)
            {
                return new ServiceMessage { IsSucceed = false, Message = "Silinecek kayıt bulunamadı" };
            }

            await _unitOfWork.BeginTransaction();
            _paymentRepository.Delete(payment);
            try
            {
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belge silinemedi");
            }
            doc.PaymentId = null;
            _documentRepository.Update(doc);
            try
            {
                _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belge silinemedi");
            }
            return new ServiceMessage
            {
                IsSucceed = true
            };

        }

        public async Task<List<PaymentEntity>> GetAllPayments()
        {
            var payments = await _paymentRepository.GetAll().ToListAsync();
            return payments;
        }

        public async Task<PaymentEntity> GetById(int id)
        {
            var payment =  _paymentRepository.GetById(id);
            return payment;
        }

        public async Task<List<PaymentEntity>> GetByName(string name)
        {
            var payments = await _paymentRepository.GetAll(x=>x.DepositorFullName.ToLower() == name.ToLower()).ToListAsync();
            return payments;
        }
    }
}
