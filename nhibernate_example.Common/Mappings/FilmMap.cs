using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace nhibernate_example.Common
{
    public class FilmMap : ClassMap<Film>
    {
        public FilmMap()
        {
            Table("film");
            Id(x => x.Id).Column("film_id");
            Map(x => x.Title);
            Map(x => x.Description).Nullable();
            Map(x => x.ReleaseYear).Column("release_year").Nullable();
            Map(x => x.RentalDurationDays).Column("rental_duration");
            Map(x => x.RentalRatePerDay).Column("rental_rate");
            Map(x => x.DurationInMinutes).Column("length").Nullable();
            Map(x => x.ReplacementCost).Column("replacement_cost");
            Map(x => x.Rating).Nullable();
            References(x => x.OriginalLanguage)
                .Column("original_language_id")
                .ForeignKey("language_id")
                .Nullable();
            HasManyToMany(x => x.Actors)
                .Table("film_actor")
                .ChildKeyColumn("actor_id")
                .ParentKeyColumn("film_id")
                .LazyLoad();
        }
    }
}