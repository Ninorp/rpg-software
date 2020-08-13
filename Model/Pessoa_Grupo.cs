using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpG_Software.Model
{
    class Pessoa_Grupo
    {
        [AutoIncrement, PrimaryKey]
        public int id { get; private set; }
        public int grupoID { get; set; }
        public string pessoaID { get; set; }

        public Pessoa_Grupo()
        {

        }
        public Pessoa_Grupo(int grupoid, string pessoaid)
        {
            grupoID = grupoid;
            pessoaID = pessoaid;
        }
    }
}
