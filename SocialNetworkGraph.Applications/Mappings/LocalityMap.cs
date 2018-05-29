using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;

namespace SocialNetworkGraph.Mappings
{
    public class LocalityMap : ClassMap<Locality>
    {
        public LocalityMap()
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
