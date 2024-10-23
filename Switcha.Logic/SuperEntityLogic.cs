using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Switcha.Core.Models;
using Switcha.Data;

namespace Switcha.Logic
{
    public class SuperEntityLogic<T> where T : SuperEntity, new()
    {
        public void AddNewTimeStamps(T entity)
        {
            entity.dateCreated = DateTime.Now;
            entity.dateUpdated = DateTime.Now;
        }
        public void AddDateModifiedTimeStamp(T entity)
        {
            entity.dateUpdated = DateTime.Now;
        }

        public long Count()
        {
            SuperEntityDAO<T> entity = new SuperEntityDAO<T>();
            return entity.GetCount();
        }
        public List<T> GetAll()
        {
            SuperEntityDAO<T> entity = new SuperEntityDAO<T>();
            return entity.RetrieveList();
        }
        public T GetByID(int id)
        {
            SuperEntityDAO<T> entity = new SuperEntityDAO<T>();
            return entity.RetrieveByID(id);
        }
        public void Insert(T entity)
        {
             AddNewTimeStamps(entity);
             SuperEntityDAO<T> eDAO = new SuperEntityDAO<T>();
             eDAO.Insert(entity);
        }
        public void Update(T entity)
        {
            AddDateModifiedTimeStamp(entity);
            SuperEntityDAO<T> eDAO = new SuperEntityDAO<T>();
            eDAO.Update(entity);
        }

        public void Commit()
        {
            SuperEntityDAO<T> Commit = new SuperEntityDAO<T>();
            Commit.Commit();
        }
        
    }
}
