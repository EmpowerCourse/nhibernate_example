using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nhibernate_example.Common
{
    public class Actor
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime LastUpdate { get; set; }
        public virtual ICollection<Film> Films { get; set; }
        public virtual string FullName {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
