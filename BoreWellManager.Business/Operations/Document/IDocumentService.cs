using BoreWellManager.Business.Operations.Document.Dtos;
using BoreWellManager.Business.Types;
using BoreWellManager.Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Business.Operations.Document
{
    public interface IDocumentService
    {
        public Task<ServiceMessage<DocumentEntity>> AddDocument(AddDocumentDto documentDto);
        public Task<List<DocumentEntity>> GetAllDocumetByTc(string tc);
        public Task<List<DocumentEntity>> GetDocumentByWellId(int wellId);
        public Task<DocumentEntity> GetDocumentById(int id);
        public Task<ServiceMessage<DocumentEntity>> ChangeDocumentInfo(int documentId, UpdateDocumentDto dto);
        /*public Task<ServiceMessage<DocumentEntity>> UpdateDocumentAllInfo(int documentId, DocumentDto dto);*/
        public Task<ServiceMessage> DeleteDocument(int documentId);

    }
}
