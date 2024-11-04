using BoreWellManager.Data.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        // DI İLE BAŞLIYORUZ
        private readonly BoreWellManagerDbContext _db;
        private IDbContextTransaction _transaction;
        public UnitOfWork(BoreWellManagerDbContext db)
        {
            _db = db; 
        }
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _db.Database.CommitTransactionAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task RollBackTransaction()
        {
           await _db.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
