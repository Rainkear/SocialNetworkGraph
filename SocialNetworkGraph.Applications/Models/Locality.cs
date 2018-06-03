using System.Collections.Generic;

namespace SocialNetworkGraph.Models
{
    public class Locality
    {
        public Locality(int id, string name)
        {
            Id = id;
            Name = name;
            Persons = new HashSet<Person>();
        }

        public Locality() { }

        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual ISet<Person> Persons { get; protected set; }
    }
}
