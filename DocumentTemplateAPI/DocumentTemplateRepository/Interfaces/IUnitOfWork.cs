using DocumentTemplateModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DocumentTemplateRepository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Call this to commit the unit of work
        /// </summary>
        void Commit();

        void BulkCommit();

        /// <summary>
        /// Return the database reference for this UOW
        /// </summary>
        CP25Team08Context CP25Team08Context { get; }

        /// <summary>
        /// Starts a transaction on this unit of work
        /// </summary>
        void StartTransaction();

        void Rollback();
    }
}
