using DocumentTemplateModel.Entities;
using DocumentTemplateRepository.Interfaces;
using DocumentTemplateUtilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DocumentTemplateRepository.Implementations
{
    public abstract class BaseCRUDRepository<
        CreateReq, UpdateReq, EDMXType, Id
    > : ICrud<CreateReq, UpdateReq, EDMXType, Id>
        where CreateReq : class
        where UpdateReq : class
        where EDMXType : class
    {
        protected readonly IUnitOfWork _UnitOfWork;
        protected readonly IDBRepositoryBase<EDMXType> _repo;

        public BaseCRUDRepository(
            IUnitOfWork _UnitOfWork,
            IDBRepositoryBase<EDMXType> _repo
        )
        {
            this._UnitOfWork = _UnitOfWork;
            this._repo = _repo;
        }

        public List<EDMXType> CreateBulk(List<CreateReq> items)
        {
            throw new NotImplementedException();
        }

        protected bool SetPropValue<DT>(EDMXType obj, string property, DT value)
        {
            var properties = typeof(EDMXType).GetProperties();
            var propertyInfo = properties.FirstOrDefault(p => p.Name == property);

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(obj, value);
                return true;
            }

            return false;
        }

        public EDMXType CreateNew(CreateReq item)
        {
            try
            {
                _UnitOfWork.StartTransaction();
                EDMXType newItem = CreationTypeToEdmx(item);

                DateTime now = DateTime.Now;
                SetPropValue<DateTime>(newItem, "createdAt", now);
                SetPropValue<DateTime>(newItem, "updatedAt", now);

                var result = (EDMXType)_repo.Insert(newItem);
                _UnitOfWork.Commit();

                return result;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                throw;
            }
        }

        public bool DeleteOneById(Id id)
        {
            try
            {
                _UnitOfWork.StartTransaction();
                var item = _repo.SingleOrDefault(id);
                _repo.Delete(item);

                _UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                throw;
            }
        }

        public List<EDMXType> GetAll()
        {
            try
            {
                return _repo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public IQueryable<EDMXType> GetAllQueryable()
        {
            try
            {
                return _repo.GetAll();
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public EDMXType GetOneById(Id id)
        {
            try
            {
                return _repo.SingleOrDefault(id);
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public PaginationResult<EDMXType> GetPagination(int page, int pageSize, NameValueCollection queries = null, Func<IQueryable<EDMXType>, NameValueCollection, IQueryable<EDMXType>> externalFilter = null, IQueryable<EDMXType> query = null, Func<IQueryable<EDMXType>, NameValueCollection, string, IQueryable<EDMXType>> externalSort = null)
        {
            try
            {
                if (query == null)
                {
                    query = _repo.GetAll();
                }

                var edmxType = typeof(EDMXType);
                var param = Expression.Parameter(edmxType, "x");

                var invalidParameters = new List<string>();
                var allowQueries = new List<string>() { "_size", "_page", "_sort" };
                var queriesOfExternal = new NameValueCollection();
                foreach (string key in queries)
                {
                    var val = queries[key];
                    if (!allowQueries.Contains(key))
                    {
                        queriesOfExternal.Add(key, val);
                    }
                }

                foreach (string key in queries)
                {
                    if (key.Contains("_") && !key.StartsWith("_") && !key.EndsWith("_"))
                    {
                        var val = queries[key];
                        var pair = key.Split('_');
                        if (pair.Length != 2)
                        {
                            throw new Exception("Invalid parameter");
                        }

                        var columName = pair[0];
                        var op = pair[1];

                        var isNotExistColumn = true;
                        Type currentType = null;

                        var foreignEDMXs = new List<Type>();
                        foreach (var property in typeof(EDMXType).GetProperties())
                        {
                            // Check column is exist in current EDMX type
                            if (property.Name.ToLower() == columName.ToLower())
                            {
                                queriesOfExternal.Remove(key);
                                isNotExistColumn = false;
                                break;
                            }
                        }

                        if (isNotExistColumn && foreignEDMXs.Count > 0)
                        {
                            foreach (var foreignEDMX in foreignEDMXs)
                            {
                                // Check exist in current foreign EDMX
                                var isExist = false;
                                foreach (var propertyForeignEDMX in foreignEDMX.GetProperties())
                                {
                                    if (propertyForeignEDMX.Name.ToLower() == columName.ToLower())
                                    {
                                        queriesOfExternal.Remove(key);
                                        isExist = true;
                                    }
                                }
                                if (isExist)
                                {
                                    currentType = foreignEDMX;
                                    break;
                                }
                            }
                        }

                        Expression exprProp = null;
                        Type typeOfProp = null;

                        if (isNotExistColumn && currentType == null)
                        {
                            continue;
                        }
                        else if (isNotExistColumn && currentType != null)
                        {
                            var foreignTableExpression = Expression.PropertyOrField(param, currentType.Name);
                            exprProp = Expression.PropertyOrField(foreignTableExpression, columName);
                            typeOfProp = currentType;
                        }
                        else
                        {
                            exprProp = Expression.PropertyOrField(param, columName);
                            typeOfProp = typeof(EDMXType);
                        }

                        PropertyInfo prop = typeOfProp.GetProperty(columName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase); ;
                        var propType = prop.PropertyType;
                        var isNullable = propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(Nullable<>);

                        Expression constVal;
                        // To check column EDMX nullable
                        if (isNullable)
                        {
                            if (String.IsNullOrEmpty(val) || val.Equals("null"))
                            {
                                constVal = Expression.Constant(null, propType);
                            }
                            else
                            {
                                constVal = Expression.Convert(Expression.Constant(Convert.ChangeType(val, propType.GetGenericArguments()[0])), propType);
                            }
                        }
                        else
                        {
                            if (propType == typeof(DateTime))
                            {
                                var requestedDt = DateTime.Parse(val);
                                val = requestedDt.ToString("o");
                            }

                            constVal = Expression.Constant(Convert.ChangeType(val, propType));
                        }

                        switch (op.ToLower())
                        {
                            case "contains":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                    Expression.Call(exprProp, "Contains", null, constVal),
                                    param));
                                break;

                            case "startswith":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                    Expression.Call(exprProp, "StartsWith", null, constVal),
                                    param));
                                break;

                            case "endswith":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                    Expression.Call(exprProp, "EndsWith", null, constVal),
                                    param));
                                break;

                            case "eq":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                        Expression.Equal(exprProp, constVal),
                                        param));
                                break;

                            case "ne":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                        Expression.NotEqual(exprProp, constVal),
                                        param));
                                break;

                            case "lte":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                     Expression.LessThanOrEqual(exprProp, constVal),
                                     param));
                                break;

                            case "lt":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                     Expression.LessThan(exprProp, constVal),
                                     param));
                                break;

                            case "gte":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                     Expression.GreaterThanOrEqual(exprProp, constVal),
                                     param));
                                break;

                            case "gt":
                                query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                     Expression.GreaterThan(exprProp, constVal),
                                     param));
                                break;

                            case "isnull":
                                if (val == "true" || val == "1")
                                {
                                    query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                    Expression.Equal(exprProp, Expression.Constant(null, exprProp.Type)),
                                    param));
                                }
                                else
                                {
                                    query = query.Where(Expression.Lambda<Func<EDMXType, bool>>(
                                    Expression.NotEqual(exprProp, Expression.Constant(null, exprProp.Type)),
                                    param));
                                }
                                break;

                            default:
                                throw new Exception("Not supported operator");
                        }
                    }
                    else if (!allowQueries.Contains(key))
                    {
                        invalidParameters.Add(key);
                    }
                }

                if (invalidParameters != null && invalidParameters.Count > 0)
                {
                    throw new ParameterNotAllowException($"Not allowed parameter: { string.Join(", ", invalidParameters) }");
                }

                // Filter by external fields
                if (externalFilter != null && queriesOfExternal.Count > 0)
                {
                    query = externalFilter(query, queriesOfExternal);
                }
                var temp = query;

                // Sort by external fields
                if (externalSort != null)
                {
                    query = externalSort(query, queries, queries["_sort"]);
                }
                if (IsOrdered(query) != true)
                {
                    var _sort = "id:desc";
                    if (queries["_sort"] != null)
                    {
                        _sort = queries["_sort"];
                    }

                    var sortQueries = new List<string>();
                    var sortArr = _sort.Split(';');
                    // Multiple sort
                    if (sortArr.Length > 0)
                    {
                        foreach (var sortItem in sortArr)
                        {
                            sortQueries.Add(sortItem);
                        }
                    }
                    // Single sort
                    else
                    {
                        sortQueries.Add(_sort);
                    }

                    if (sortQueries.Count > 0)
                    {
                        string sortOrder = "";
                        foreach (var sort in sortQueries)
                        {
                            var sortParts = sort.Split(':');
                            var sortColumn = sortParts[0];
                            sortOrder = sortParts.Length > 1 ? sortParts[1] : "desc";

                            if (sortOrder == "desc")
                            {
                                query = query.OrderByDESCProperty(sortColumn);
                            }
                            else
                            {
                                query = query.OrderByASCProperty(sortColumn);
                            }
                        }
                        if (sortOrder == "desc")
                        {
                            query = query.OrderByDESCProperty("Id");
                        }
                        else
                        {
                            query = query.OrderByASCProperty("Id");
                        }
                    }

                }

                return new PaginationResult<EDMXType>
                {
                    Items = query.Skip(page * pageSize).Take(pageSize).ToList(),
                    Page = page,
                    Size = pageSize,
                    Total = query.Count()
                };
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public bool IsExistById(Id id)
        {
            try
            {
                var user = _repo.SingleOrDefault(id);
                return user != null;
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public bool SoftDeleteBulk(List<Id> ids)
        {
            throw new NotImplementedException();

        }

        public bool SoftDeleteById(Id id)
        {
            try
            {
                _UnitOfWork.StartTransaction();
                var item = _repo.SingleOrDefault(id);
                SetPropValue<int>(item, "status_account", (int)Enums.Status.DISABLE);

                var now = DateTime.Now;
                SetPropValue<DateTime>(item, "updatedAt", now);

                _UnitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                throw;
            }
        }

        public IEnumerable<EDMXType> UpdateBulk(IEnumerable<EDMXType> items)
        {
            throw new NotImplementedException();
        }

        public EDMXType UpdateOne(EDMXType item)
        {
            try
            {
                _UnitOfWork.StartTransaction();

                DateTime now = DateTime.Now;
                SetPropValue<DateTime>(item, "updatedAt", now);

                _repo.Update(item);
                _UnitOfWork.Commit();

                return item;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        public EDMXType UpdateOneById(Id id, UpdateReq item)
        {
            try
            {
                _UnitOfWork.StartTransaction();
                var obj = _repo.SingleOrDefault(id);
                if (obj == null)
                {
                    return null;
                }

                this.UpdateEDMXFromUpdateReq(obj, item);

                DateTime now = DateTime.Now;
                SetPropValue<DateTime>(obj, "updatedAt", now);

                _repo.Update(obj);
                _UnitOfWork.Commit();

                return obj;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }

        

        private bool IsOrdered(IQueryable Data)
        {
            string query = Data.ToString();

            int pIndex = query.LastIndexOf(')');

            if (pIndex == -1)
                pIndex = 0;

            if (query.IndexOf("ORDER BY", pIndex) != -1)
            {
                return true;
            }

            return false;
        }

        protected abstract EDMXType CreationTypeToEdmx(CreateReq inp);
        protected abstract void UpdateEDMXFromUpdateReq(EDMXType edmx, UpdateReq inp);
    }
    public static class ReflectionQueryable
    {
        private static readonly MethodInfo OrderByASCMethod =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "OrderBy")
                .Where(method => method.GetParameters().Length == 2)
                .Single();

        private static readonly MethodInfo OrderByDESCMethod =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "OrderByDescending")
                .Where(method => method.GetParameters().Length == 2)
                .Single();

        private static readonly MethodInfo ThenByMethod =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "ThenBy")
                .Where(method => method.GetParameters().Length == 2)
                .Single();

        private static readonly MethodInfo ThenByDescendingMethod =
            typeof(Queryable).GetMethods()
                .Where(method => method.Name == "ThenByDescending")
                .Where(method => method.GetParameters().Length == 2)
                .Single();

        public static IQueryable<TSource> OrderByASCProperty<TSource>
            (this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), $"order_{ source}");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            if (source.Expression.Type == typeof(IOrderedQueryable<TSource>))
            {
                MethodInfo genericMethod = ThenByMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });
                object ret = genericMethod.Invoke(null, new object[] { source, lambda });
                return (IQueryable<TSource>)ret;
            }
            else
            {
                MethodInfo genericMethod = OrderByASCMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });
                object ret = genericMethod.Invoke(null, new object[] { source, lambda });
                return (IQueryable<TSource>)ret;
            }
        }

        public static IQueryable<TSource> OrderByDESCProperty<TSource>
            (this IQueryable<TSource> source, string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TSource), $"order_{ source}");
            Expression orderByProperty = Expression.Property(parameter, propertyName);

            LambdaExpression lambda = Expression.Lambda(orderByProperty, new[] { parameter });

            if (source.Expression.Type == typeof(IOrderedQueryable<TSource>))
            {
                MethodInfo genericMethod = ThenByDescendingMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });
                object ret = genericMethod.Invoke(null, new object[] { source, lambda });
                return (IQueryable<TSource>)ret;
            }
            else
            {
                MethodInfo genericMethod = OrderByDESCMethod.MakeGenericMethod(new[] { typeof(TSource), orderByProperty.Type });
                object ret = genericMethod.Invoke(null, new object[] { source, lambda });
                return (IQueryable<TSource>)ret;
            }
        }
    }
}
