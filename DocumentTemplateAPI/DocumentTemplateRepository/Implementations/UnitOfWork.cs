using DocumentTemplateModel.Models;
using DocumentTemplateRepository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DocumentTemplateRepository.Implementations
{
    [ExcludeFromCodeCoverage]
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _dbtransaction;
        private readonly CP25Team08Context _CP25Team08Context;

        public UnitOfWork()
        {
            _CP25Team08Context = new CP25Team08Context();
        }

        public void StartTransaction()
        {
            _dbtransaction = _CP25Team08Context.Database.BeginTransaction();

        }

        public void Commit()
        {
            _CP25Team08Context.SaveChanges();
            _dbtransaction.Commit();
        }


        public void Rollback()
        {
            _dbtransaction.Rollback();
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_CP25Team08Context == null) return;
            _CP25Team08Context.Dispose();
            if (_dbtransaction == null) return;
            _dbtransaction.Dispose();
        }

        public void BulkCommit()
        {
            throw new NotImplementedException();
        }

        public CP25Team08Context CP25Team08Context
        {
            get { return _CP25Team08Context; }
        }
    }
}
