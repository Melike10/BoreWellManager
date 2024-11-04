using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoreWellManager.Data.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        // savechanges kaç kayda etki ettiğini döner o yüzden int kulllandık
        Task<int> SaveChangesAsync();
        //Task asekron metotların voidi gibidir.
        Task BeginTransaction();
        Task CommitTransaction();
        Task RollBackTransaction();

    }
}
