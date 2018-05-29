using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;

namespace SocialNetworkGraph.Mappings
{
    public class SexMap : ClassMap<Sex>
    {
        public SexMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            HasMany(x => x.Persons)
                .Inverse()
                .Cascade.All()
                .AsSet();
        }
    }
}
