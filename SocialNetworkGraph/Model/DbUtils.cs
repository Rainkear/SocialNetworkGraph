using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetworkGraph
{
    class DbUtils
    {
        public DbUtils()
        {
            CreateSessionFactory();
        }

        /// <summary>
        /// Get ids of all persons and their friends
        /// </summary>
        /// <returns>Dictionary of (Person id, List of friends ids)</returns>
        public Dictionary<int, List<int>> GetAllPersonsIds()
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.Query<Person>()
                    .ToDictionary(x => x.Id, x => x.LFriends
                          .Select(y => y.Id)
                          .ToList()
                     );
            }
        }

        /// <summary>
        /// Get person object by id. Eager fetch hobby, friends, 
        /// places, sex
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>Person object</returns>
        public Person GetPersonById(int id)
        {
            using (var session = sessionFactory.OpenSession())
            {
                return session.QueryOver<Person>()
                   .Fetch(p => p.Hobbies).Eager
                   .Fetch(p => p.LFriends).Eager
                   .Fetch(p=> p.BirthPlace).Eager
                   .Fetch(p => p.LivePlace).Eager
                   .Fetch(p => p.Sex).Eager
                   .Where(p => p.Id == id)
                   .List().First();
            }
        }

        private ISessionFactory sessionFactory;

        /// <summary>
        /// Creates new session from connection string
        /// </summary>
        private void CreateSessionFactory()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["localSql"].ConnectionString;
            sessionFactory = Fluently.Configure()
              .Database(
                MsSqlConfiguration.MsSql2012
                  .ConnectionString(connectionString)
              )
              .Mappings(m =>
                  m.FluentMappings.AddFromAssemblyOf<MainWindowViewModel>())
              .BuildSessionFactory();
        }
    }
}
