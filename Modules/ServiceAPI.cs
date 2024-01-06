using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObuvashkaWebAPI.Models;
using System.Reflection;

namespace ObuvashkaWebAPI.Modules
{
    public class ServiceAPI 
    {

        public IEnumerable<T> GetData<T>(Cx07681BillingContext db)
        {
            try
            {
                System.Type type = typeof(T);
                var setMethod = db.GetType().GetMethod(nameof(DbContext.Set), new System.Type[] { });
                var genericSetMethod = setMethod.MakeGenericMethod(type);
                var dbSet1 = genericSetMethod.Invoke(db, null);

                var dbSet = dbSet1 as IQueryable<object>;
                IQueryable<object> result = dbSet.AsQueryable();

                IEnumerable<T> data = (IEnumerable<T>)result.AsEnumerable();

                return data;

            }
            catch
            {
                return null;
            }
        }
        public IEnumerable<T> SortList<T>(IEnumerable<T> list, string fieldName, bool sortDesc, int limit, int offset)
        {
            PropertyInfo property = typeof(T).GetProperty(fieldName);

            if (property != null)
            {
                list = list.Skip(offset).Take(limit);
                if (sortDesc)
                    return list.OrderByDescending(x => property.GetValue(x, null)).ToList();
                else
                    return list.OrderBy(x => property.GetValue(x, null)).ToList();
            }

            return list;
        }

        
        
    }
}
