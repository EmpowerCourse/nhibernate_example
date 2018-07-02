using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nhibernate_example.Common
{
    public class Film
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Description { get; set; }
        public virtual string ReleaseYear { get; set; }
        public virtual short RentalDurationDays { get; set; }
        public virtual decimal RentalRatePerDay { get; set; }
        public virtual short? DurationInMinutes { get; set; }
        public virtual decimal ReplacementCost { get; set; }
        public virtual string Rating { get; set; }
        public virtual Language OriginalLanguage { get; set; }
        public virtual ICollection<Actor> Actors { get; set; }

        public virtual double? DurationInHours
        {
            get
            {
                return DurationInMinutes.HasValue
                    ? (DurationInMinutes.Value / 60d)
                    : (double?)null;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}, {1:C} per night, {2} hours long",
                Title, RentalRatePerDay, DurationInHours.HasValue ? DurationInHours.Value.ToString("#0.00") : "n/a");
        }
    }
}
