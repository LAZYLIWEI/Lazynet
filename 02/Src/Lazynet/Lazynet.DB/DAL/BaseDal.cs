/*
* ==============================================================================
*
* Filename: BaseDal
* Description: 
*
* Version: 1.0
* Created: 2020/5/27 0:42:03
* Compiler: Visual Studio 2010
*
* Author: Your name
* Company: Your company name
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;
using Lazynet.Core.DB;
using SqlSugar;

namespace Lazynet.DB.DAL
{
    public class BaseDal<T> where T : class, new()
    {
        protected SqlSugarClient DB = DBContextFactory.CreateDbContext();
        
        public List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            var queryable = DB.Queryable<T>();
            return queryable.Where(whereLambda).ToList();
        }
        
        public List<T> GetPageList(int pageIndex, int pageSize, out int totalCount,
            System.Linq.Expressions.Expression<Func<T, bool>> whereLambda,
            System.Linq.Expressions.Expression<Func<T, object>> orderbyLambda, bool isAsc)
        {
            var queryable = DB.Queryable<T>();
            var temp = queryable.Where(whereLambda);
            totalCount = temp.Count();
            if (isAsc) //升序
            {
                temp = temp.OrderBy(orderbyLambda, OrderByType.Asc).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                temp = temp.OrderBy(orderbyLambda, OrderByType.Desc).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return temp.ToList();
        }

        public bool Delete(T entity)
        {
            var deleteable = DB.Deleteable(entity);
            return deleteable.ExecuteCommand() > 0;
        }
       
        public bool Edit(T entity)
        {
            var updateable = DB.Updateable(entity);
            return updateable.ExecuteCommand() > 0;
        }
       
        public T Add(T entity)
        {
            var insertable = DB.Insertable(entity);
            return insertable.ExecuteCommand() > 0 ? entity : null;
        }
      
        public void BeginTran()
        {
            DB.BeginTran();
        }
      
        public void CommitTran()
        {
            DB.CommitTran();
        }

        public void RollbackTran()
        {
            DB.RollbackTran();
        }

    }
}
