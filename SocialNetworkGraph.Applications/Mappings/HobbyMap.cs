using FluentNHibernate.Mapping;
using SocialNetworkGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
