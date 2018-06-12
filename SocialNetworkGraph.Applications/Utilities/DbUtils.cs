using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using SocialNetworkGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkGraph.Utilities
{
    public class DbUtils
    {
        public DbUtils()
        {
            CreateSessionFactory();
        }

        /// <summary>
        /// Get ids of all persons and their friends
        /// </summary>
        /// <returns>Dictionary of (Person id, List of friends ids)</returns>
        public Dictionary<Vertex, List<Vertex>> GetAllPersonsIds()
        {

            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.Query<Person>()
                        .ToDictionary(x => new Vertex(x.Id, x.LastName, x.FirstName, x.FatherName), x => x.LFriends
                              .Select(y => new Vertex(y.Id, y.LastName, y.FirstName, y.FatherName))
                              .ToList()
                         );
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Instance.LogFile(ex.ToString());
                throw new Exception("Get persons list error: " + ex.Message, ex);
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
            try
            {
                using (var session = sessionFactory.OpenSession())
                {
                    return session.QueryOver<Person>()
                       .Fetch(p => p.Hobbies).Eager
                       .Fetch(p => p.LFriends).Eager
                       .Fetch(p => p.BirthPlace).Eager
                       .Fetch(p => p.LivePlace).Eager
                       .Fetch(p => p.Sex).Eager
                       .Where(p => p.Id == id)
                       .List().First();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.Instance.LogFile(ex.ToString());
                throw new Exception("Get person by id exception: " + ex.Message, ex);
            }
        }

        private ISessionFactory sessionFactory;

        /// <summary>
        /// Creates new session from connection string
        /// </summary>
        private void CreateSessionFactory()
        {
            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["localSql"].ConnectionString;
                sessionFactory = Fluently.Configure()
                  .Database(
                    MsSqlConfiguration.MsSql2012
                      .ConnectionString(connectionString)
                  )
                  .Mappings(m =>
                      m.FluentMappings.AddFromAssemblyOf<DbUtils>())
                  .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                ExceptionLogger.Instance.LogFile(ex.ToString());
                if (ex.InnerException != null)
                    throw new Exception("Session factory exception: " + ex.Message + "\r\n" + ex.InnerException.Message, ex);
                else
                    throw new Exception("Session factory exception: " + ex.Message, ex);
            }
        }
    }
}
