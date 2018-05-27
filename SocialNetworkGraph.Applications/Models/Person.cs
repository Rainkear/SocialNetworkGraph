using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkGraph.Models
{
    public class Person
    {
        public virtual int Id { get; protected set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string FatherName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual Locality BirthPlace { get; set; }
        public virtual Locality LivePlace { get; set; }
        public virtual string Phone { get; set; }
        // https://stackoverflow.com/questions/25833827/fluent-nhibernate-many-to-many-on-same-table
        //
        // Using ISet for multiple eager fetch in DbUtils.GetPersonById
        public virtual ISet<Person> LFriends { get; set; }
        public virtual ISet<Person> RFriends { get; set; }
        public virtual ISet<Hobby> Hobbies { get; set; }
    }
}
