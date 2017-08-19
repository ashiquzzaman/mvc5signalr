using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using SignalRMVCUnityCRUD.Models;

// By: Ashiquzzaman;

namespace SignalRMVCUnityCRUD.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Members

        protected DbContext Context;
        private bool disposed = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            Context = context;

        }
        public Repository():this(new ApplicationDbContext()){}

        #endregion

        #region PROPERTY
        // Entity corresponding Database Table
        protected DbSet<T> DbSet
        {
            get { return Context.Set<T>(); }
        }

        #endregion

        #region LINQ QUERY

        /// <summary>
        /// add a item in a table. item never be added untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be added into corresponding DB table.</param>
        public virtual void Add(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            DbSet.Add(item); // add new item in this set
        }

        /// <summary>
        /// Remove a item in a table. item never be Removed untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be Removed into corresponding DB table.</param>
        public virtual void Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            Attach(item); //attach item if not exist
            DbSet.Remove(item); //set as "removed"

        }

        /// <summary>
        /// Modify a item in a table. item never be Modified untill call savechanges method.
        /// </summary>
        /// <param name="item">object of a class which will be Modified into corresponding DB table.</param>
        public virtual void Modify(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            var entry = Context.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            return DbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> All()
        {
            return DbSet.AsQueryable();
        }

        public T Create(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            DbSet.Add(item);
            Context.SaveChanges();
            return item;
        }

        public int Update(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            var entry = Context.Entry(item);
            DbSet.Attach(item);
            entry.State = EntityState.Modified;
            return Context.SaveChanges();
        }

        public int Update(Expression<Func<T, bool>> predicate)
        {
            var records = FindAll(predicate);
            if (!records.Any())
            {
                throw new ObjectNotFoundException();
            }
            foreach (var record in records)
            {
                var entry = Context.Entry(record);

                DbSet.Attach(record);

                entry.State = EntityState.Modified;
            }
            return Context.SaveChanges();
        }

        public int Delete(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            DbSet.Remove(item);

            return Context.SaveChanges();
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            var records = FindAll(predicate);
            if (!records.Any())
            {
                throw new ObjectNotFoundException();
            }
            foreach (var record in records)
            {
                DbSet.Remove(record);
            }
            return Context.SaveChanges();
        }

        /// <summary>
        /// Count all item in a DB table.
        /// </summary>
        public int Count
        {
            get { return DbSet.Count(); }
        }
        public long LongCount
        {
            get { return DbSet.LongCount(); }
        }
        public int CountFunc(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public long LongCountFunc(Expression<Func<T, bool>> predicate)
        {
            return DbSet.LongCount(predicate);
        }

        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var count = DbSet.Count(predicate);
            return count > 0;
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return DbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return DbSet.FirstOrDefault(predicate);
            }
            catch (Exception ex)
            {
                    
                throw ex;
            }
            
        }

        public string Max(Expression<Func<T, string>> predicate)
        {
            return DbSet.Max(predicate);
        }

        public string MaxFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).Max(predicate);
        }

        public string Min(Expression<Func<T, string>> predicate)
        {
            return DbSet.Min(predicate);
        }

        public string MinFunc(Expression<Func<T, string>> predicate, Expression<Func<T, bool>> where)
        {
            return DbSet.Where(where).Min(predicate);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        #endregion

        #region SQL RAW QUERY

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery)
        {
            return Context.Database.SqlQuery<TEntity>(sqlQuery);
        }

        public IEnumerable<TEntity> ExecuteQueryWithoutPrameter<TEntity>(string sqlQuery)
        {
            return Context.Database.SqlQuery<TEntity>(sqlQuery);
        }
     

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }





        public string ExecuteScalar(string sqlCommand, params object[] parameters)
        {
            //decimal data = base.Database.SqlQuery<decimal>(sqlCommand, parameters).Single();
            return Context.Database.SqlQuery<decimal>(sqlCommand, parameters).Single().ToString();
        }

        public string ExecuteScalarString(string sqlCommand, params object[] parameters)
        {
            //decimal data = base.Database.SqlQuery<decimal>(sqlCommand, parameters).Single();
            return Context.Database.SqlQuery<string>(sqlCommand, parameters).Single().ToString();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        public TEntity ExecuteScalar<TEntity>(string sqlCommand, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(sqlCommand, parameters).Single();
        }

        #endregion

        #region DATABSE TRANSACTION

        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            Context.Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            Context.Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Context.Entry(original).CurrentValues.SetValues(current);
        }

        public int SaveChanges()
        {
            try
            {
               return Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    Context.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                        .ForEach(entry =>
                        {
                            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                        });

                }
            } while (saveFailed);

        }

        public void RollbackChanges()
        {
            Context.ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        #region IDisposable Members

        ~Repository()
        {
            Dispose(false);
        }

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Context != null)
                    {
                        Context.Dispose();
                        Context = null;
                    }
                }
            }
            disposed = true;
        }


        #endregion

    }
}
