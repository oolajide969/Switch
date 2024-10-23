using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Switcha.Core.Models;

namespace Switcha.Data
{
    public class SuperEntityDAO<T> where T : SuperEntity, new()
    {
        protected virtual ISession GetSession()
        {
            return SessionManager.Session();
        }

        public void Commit()
        {
            var session = GetSession();
            if (session.Transaction != null && session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
            session.Flush();
        }

        public T RetrieveByID(int Id)
        {
            T entity = new T();

            entity = GetSession().Get<T>(Id);

            return entity;
        }

        public List<T> RetrieveList()
        {
            List<T> list = GetSession().Query<T>().ToList();
            return list;
        }

        public void Insert(T entity)
        {
            GetSession().Save(entity);
        }
        public void Update(T entity)
        {
            GetSession().Update(entity);
        }

        public long GetCount()
        {
            long count = GetSession().Query<T>().LongCount();
            return count;
        }

    }
}
