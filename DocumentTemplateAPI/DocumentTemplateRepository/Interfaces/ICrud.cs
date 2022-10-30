using DocumentTemplateModel.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace DocumentTemplateRepository.Interfaces
{
    public interface ICrud<CreateReq, UpdateReq, T, Id>
        where CreateReq : class
        where UpdateReq : class
        where T : class
    {
        bool IsExistById(Id id);
        T CreateNew(CreateReq item);
        List<T> CreateBulk(List<CreateReq> items);
        List<T> GetAll();
        IQueryable<T> GetAllQueryable();
        PaginationResult<T> GetPagination(int page, int pageSize, NameValueCollection queries = null, Func<IQueryable<T>, NameValueCollection, IQueryable<T>> externalFilter = null, IQueryable<T> query = null, Func<IQueryable<T>, NameValueCollection, string, IQueryable<T>> externalSort = null);
        T GetOneById(Id id);
        T UpdateOneById(Id id, UpdateReq item);
        T UpdateOne(T item);
        IEnumerable<T> UpdateBulk(IEnumerable<T> items);
        bool DeleteOneById(Id id);
        bool SoftDeleteById(Id id);
        bool SoftDeleteBulk(List<Id> ids);
    }
}
