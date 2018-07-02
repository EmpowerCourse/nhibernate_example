using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using nhibernate_example.Common;
using nhibernate_example.Services;
using NHibernate;
using System.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

namespace nhibernate_example
{
    class Program
    {
        private static ISessionFactory _sessionFactory;

        static void Main(string[] args)
        {
            initializeNH();
            // get a list of films
            FilmService fs = new FilmService(_sessionFactory);
            var filmList = fs.GetFilms(50);
            foreach(var f in filmList)
            {
                Console.WriteLine(f);
            }
            // randomly select a film and return a list of actors in that film
            int randomItemSequence = new Random().Next(0, filmList.Count);
            Film randomFilm = filmList[randomItemSequence];
            List<Actor> actorList = fs.GetActorsInFilm(randomFilm.Id);
            Console.WriteLine("These actors appeared in the " + randomFilm.ReleaseYear + " rendition of " + 
                randomFilm.Title + ", originally in "+ 
                (randomFilm.OriginalLanguage == null ? "an unknown language" : randomFilm.OriginalLanguage.Name) + 
                ".");
            foreach (var a in actorList)
            {
                Console.WriteLine(a.FullName);
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            // querying isn't the only power of NHibernate.
            // here is how to add an actor:
            int muditActorId;
            using (ISession s = _sessionFactory.OpenSession())
            {
                var newActor = new Actor()
                {
                    FirstName = "Mudit",
                    LastName = "Singhania",
                    LastUpdate = DateTime.UtcNow
                };
                s.Save(newActor);
                Console.WriteLine("We just saved Mudit as an actor - his key in the database is " + newActor.Id);
                muditActorId = newActor.Id;
            }
            // ...and here's how to later update him
            using (ISession s = _sessionFactory.OpenSession())
            {
                var mudit = s.Get<Actor>(muditActorId);
                mudit.LastUpdate = new DateTime(2018, 1, 1, 13, 33, 0);
                s.Update(mudit);
                s.Flush();
                Console.WriteLine("We just updated Mudit's record");
            }
            // ...and here's how to remove him
            using (ISession s = _sessionFactory.OpenSession())
            {
                var mudit = s.Get<Actor>(muditActorId);
                s.Delete(mudit);
                s.Flush();
                Console.WriteLine("We just removed Mudit with a delete statement");
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void initializeNH()
        {
            var config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              // uncomment the 2 lines below to see the sql generated and executed for you
                              // .ShowSql()
                              // .FormatSql()
                              .ConnectionString(p => p.Is(ConfigurationManager.AppSettings["ConnectionString"]))
                              .DefaultSchema("dbo"))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<ActorMap>();
                }).BuildConfiguration();
            _sessionFactory = config.BuildSessionFactory();
        }
    }
}
