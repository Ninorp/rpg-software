using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace RpG_Software.Model
{
    class MapPessoa : ClassMap<Pessoa>
    {
        public MapPessoa()
        {
            AutoMap();
            Map(m => m.ID).Name("Carimbo de data/hora");           
        }
    }
}
