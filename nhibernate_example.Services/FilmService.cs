using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using nhibernate_example.Common;

namespace nhibernate_example.Services
{
    public class FilmService
    {
        private ISessionFactory _sessionFactory;

        public FilmService(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public List<Film> GetFilms(int firstN = 100)
        {
            using (ISession nhSession = _sessionFactory.OpenSession())
            {
                return nhSession.Query<Film>()
                    .OrderBy(x => x.Title)
                    .Take(firstN)
                    .ToList();
            }
        }

        public List<Actor> GetActorsInFilm(int filmId)
        {
            using (ISession nhSession = _sessionFactory.OpenSession())
            {
                return nhSession.Query<Actor>()
                    .Where(x => x.Films.Any(f => f.Id == filmId))
                    .OrderBy(x => x.LastName)
                    .ToList();
            }
        }
    }
}
