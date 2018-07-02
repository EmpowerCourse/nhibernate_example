using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace nhibernate_example.Common
{
    public class LanguageMap : ClassMap<Language>
    {
        public LanguageMap()
        {
            Table("language");
            Id(x => x.Id).Column("language_id");
            Map(x => x.Name);
        }
    }
}
