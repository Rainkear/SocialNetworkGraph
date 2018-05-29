using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;

namespace SocialNetworkGraph.Mappings
{
    public class HobbyMap : ClassMap<Hobby>
    {
        public HobbyMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasManyToMany(x => x.Persons)
                .Cascade.All()
                .Inverse()
                .Table("PersonHobby")
                .AsSet();
        }
    }
}
