using BoreWellManager.Business.Operations.Document.Dtos;
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

namespace BoreWellManager.Business.Operations.Document
{

    public class DocumentManager : IDocumentService
    {
        private readonly IRepository<DocumentEntity> _documentRepository;
        private readonly IRepository<PaymentEntity> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DocumentManager(IRepository<DocumentEntity> documentRepository,IUnitOfWork unitOfWork,IRepository<PaymentEntity> paymentRepository)
        {
            _documentRepository = documentRepository;
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
        }
        public async Task<ServiceMessage<DocumentEntity>> AddDocument(AddDocumentDto documentDto)
        {
            var hasDocument = await _documentRepository.GetAll(x => x.WellId == documentDto.WellId && x.InstitutionId == documentDto.InstitutionId && x.Type == documentDto.Type).FirstOrDefaultAsync();
            if (hasDocument != null)
            {
                return new ServiceMessage<DocumentEntity> { IsSucceed = false, Message = "Bu kayda ait ilgili belge zaten sistemde var" };
            }

            await _unitOfWork.BeginTransaction();

            // Document oluşturma
            var document = new DocumentEntity
            {
                WellId = documentDto.WellId,
                InstitutionId = documentDto.InstitutionId,
                PaymentId = null, // İlk başta null bırak
                Type = documentDto.Type,
                CustomerSubmissionDate = documentDto.CustomerSubmissionDate,
                DeliveredToInstitution = documentDto.DeliveredToInstitution,
                IsLienCertificate = documentDto.IsLienCertificate,
                DocumentFee = documentDto.DocumentFee,
                FeeReceived = documentDto.FeeReceived,
                CreatedBy = documentDto.CreatedBy
            };
            _documentRepository.Add(document);

            try
            {
                await _unitOfWork.SaveChangesAsync();  
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belge kaydı yapılırken bir hata oluştu.");
            }

            // Payment oluşturma
            var payment = new PaymentEntity
            {
                DocumentId = document.Id,  
                DepositorFullName = documentDto.CustomerFullName,
                TotalAmount = documentDto.DocumentFee,
                RemaningAmount = documentDto.DocumentFee - documentDto.ReceivedFeeAmount,
                EmployeeWhoReceivedPayment = documentDto.ReceivedFeeAmount != 0 ? documentDto.CreatedBy : null,
                IsInstallmentPayment = documentDto.ReceivedFeeAmount == documentDto.DocumentFee ? false : true,
                InstallmentAmount = documentDto.ReceivedFeeAmount,
                LastPaymentDate = documentDto.ReceivedFeeAmount != 0 ? DateTime.Now : (DateTime?)null,
            };
            _paymentRepository.Add(payment);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                document.PaymentId = payment.Id; // PaymentId güncelle
                _documentRepository.Update(document);

                await _unitOfWork.SaveChangesAsync(); // Güncellenmiş document kaydet
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Ödeme belgesi oluşturulurken hata oluştu");
            }

            return new ServiceMessage<DocumentEntity>
            {
                IsSucceed = true,
                Data = document
            };
        }


        public async Task<ServiceMessage<DocumentEntity>> ChangeDocumentInfo(int documentId, UpdateDocumentDto dto)
        {
            var document = _documentRepository.GetById(documentId);
            if(document is null)
                return new ServiceMessage<DocumentEntity> { IsSucceed = false, Message = "Girdiğiniz id 'ye ait bir kayıt bulunamadı" };
            document.DeliveredToInstitution = dto.DeliveredToInstitution;
            document.InstitutionSubmissionDate = dto.InstitutionSubmissionDate;
            document.ModifiedBy = dto.ModifiedBy;
            _documentRepository.Update(document);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Belgenin kurum bilgileri güncellenirken hata oluştu");
            }
            return new ServiceMessage<DocumentEntity> { Data = document ,IsSucceed=true};
        }

        public async Task<ServiceMessage> DeleteDocument(int documentId)
        {
            var document = _documentRepository.GetById(documentId);
            if (document is null)
                return new ServiceMessage { IsSucceed = false, Message = "Silinecek bir kayıt bulunamadı." };
            await _unitOfWork.BeginTransaction();
            _documentRepository.Delete(document);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belge silinirken hata oluştu.");
            }
            _paymentRepository.DeleteById((int)document.PaymentId);
            try
            {
               await _unitOfWork.SaveChangesAsync();
               await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {

                await _unitOfWork.RollBackTransaction();
                throw new Exception("Belgenin ödeme bilgileri silinirken hata oluştu.");
            }
            return new ServiceMessage { IsSucceed = true };

        }

        public async Task<List<DocumentEntity>> GetAllDocumetByTc(string tc)
        {
            //var res = await _documentRepository.GetAll(x=> x.Well.UserId== userId).ToListAsync(); 
            var res = await _documentRepository.GetAll(x => x.Well.User.TC == tc).ToListAsync();
            return res;
           
        }

        public async Task<DocumentEntity> GetDocumentById(int id)
        {
            var res= _documentRepository.GetById(id);
          
             return res;
        }

        public async Task<List<DocumentEntity>> GetDocumentByWellId(int wellId)
        {
            var res = _documentRepository.GetAll(x=> x.WellId == wellId).ToList();
            return res;
        }

       /* public async Task<ServiceMessage<DocumentEntity>> UpdateDocumentAllInfo(int documentId, DocumentDto dto)
        {
            var document = _documentRepository.GetById(documentId);
            if (document == null)
            {
                new ServiceMessage<DocumentEntity> { IsSucceed = false, Message = "Güncellenecek belge bulunamadı" };
            }

                await _unitOfWork.BeginTransaction();
                document.WellId = dto.WellId;
                document.InstitutionId = dto.InstitutionId;
                document.PaymentId= dto.PaymentId;
                document.DeliveredToInstitution= dto.DeliveredToInstitution;
                document.CustomerSubmissionDate = dto.CustomerSubmissionDate;
                document.InstitutionSubmissionDate = dto.InstitutionSubmissionDate;
                document.CreatedBy = dto.CreatedBy;
                document.ModifiedBy = dto.ModifiedBy;
                document.FeeReceived = dto.FeeReceived;
                document.IsLienCertificate=dto.IsLienCertificate;
                document.DocumentFee=dto.DocumentFee;
                document.Type = dto.Type;
                document.SignaturesReceived= dto.SignaturesReceived;

                _documentRepository.Update(document);
                try
                {
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (Exception)
                {
                    await _unitOfWork.RollBackTransaction();
                    throw new Exception("Belgenin bilgilerinin değiştirilmesi sırasında bir hata oluştu.");
                }

                var paymentEntitiy = await _paymentRepository.GetAll(x=>x.DocumentId==documentId).FirstOrDefaultAsync();
                  if(paymentEntitiy.Id!= document.PaymentId)
            {
                // eğer böyle bir kayıt sistemde var ise
                var newPayment = _paymentRepository.GetById(dto.PaymentId);
                if(newPayment is not null)
                {
                    _paymentRepository.Delete(paymentEntitiy, false);
                    newPayment.DocumentId = document.Id;
                    if (document.FeeReceived == true && document.DocumentFee == dto.ReceivedFeeAmount)
                    {
                    }
                    _paymentRepository.Update(newPayment);

                }
                else
                {

                }

            }
            else
            {

            }


                return new ServiceMessage<DocumentEntity> { IsSucceed = true,Data = document};

            }
       */
    }
}
