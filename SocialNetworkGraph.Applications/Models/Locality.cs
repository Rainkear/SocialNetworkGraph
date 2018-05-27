using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkGraph.Models
{
    public class Locality
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ISet<Person> Persons { get; protected set; }
    }
}
