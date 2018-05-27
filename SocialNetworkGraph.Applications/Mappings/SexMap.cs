using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
