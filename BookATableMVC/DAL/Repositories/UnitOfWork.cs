using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IDisposable
    {

        private BookATableContext context;
        private DbContextTransaction transaction;

        public UnitOfWork()
        {
            this.context = new BookATableContext();
            this.transaction = this.context.Database.BeginTransaction();
        }

        public void Commit()
        {
            this.transaction.Commit();
            this.transaction = null;
        }

        public void RollBack()
        {
            this.transaction.Rollback();
            this.transaction = null;
        }

        public void Dispose()
        {
            Commit();
            this.transaction.Dispose();
            this.transaction = null;

        }
    }
}
