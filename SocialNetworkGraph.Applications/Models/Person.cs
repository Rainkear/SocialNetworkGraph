using System;
using System.Collections.Generic;

namespace SocialNetworkGraph.Models
{
    public class Person
    {
        public Person(int id, string lastName, string firstName, string fatherName, 
            DateTime birthDate, Sex sex, Locality birthPlace, Locality livePlace, string phone)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            FatherName = fatherName;
            BirthDate = birthDate;
            Sex = sex;
            BirthPlace = birthPlace;
            LivePlace = livePlace;
            Phone = phone;
            LFriends = new HashSet<Person>();
            RFriends = new HashSet<Person>();
            Hobbies = new HashSet<Hobby>();
        }

        public Person() { }

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
