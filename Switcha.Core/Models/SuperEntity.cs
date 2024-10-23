using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Switcha.Core.Models
{
    public class SuperEntity
    {
        public virtual int ID { get; set; }

        public virtual DateTime dateCreated { get; set; }

        public virtual DateTime dateUpdated { get; set; }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is SuperEntity && (obj as SuperEntity).ID.Equals(this.ID))
            {
                return true;
            }
            return base.Equals(obj);
        }
    }
}
