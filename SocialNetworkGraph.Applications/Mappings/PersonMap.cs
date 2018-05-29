using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;

namespace SocialNetworkGraph.Mappings
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.Id);
            Map(x => x.LastName);
            Map(x => x.FirstName);
            Map(x => x.FatherName);
            Map(x => x.BirthDate);
            References(x => x.Sex).Column("Sex");
            References(x => x.BirthPlace).Column("BirthPlace");
            References(x => x.LivePlace).Column("LivePlace");
            Map(x => x.Phone);
            // using two lists for many-to-many on a same table
            // https://stackoverflow.com/questions/25833827/fluent-nhibernate-many-to-many-on-same-table
            HasManyToMany(x => x.LFriends)
                .Table("PersonPerson")
                .ParentKeyColumn("FirstPersonID")
                .ChildKeyColumn("SecondPersonID")
                .Cascade.All().Table("PersonPerson")
                .AsSet();
            HasManyToMany(x => x.RFriends)
                .Table("PersonPerson")
                .ParentKeyColumn("SecondPersonID")
                .ChildKeyColumn("FirstPersonID")
                .Cascade.All()
                .Inverse()
                .AsSet();
            HasManyToMany(x => x.Hobbies)
                .Cascade.All()
                .Table("PersonHobby")
                .AsSet();
        }
    }
}
