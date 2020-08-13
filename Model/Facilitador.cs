using SQLite;

namespace RpG_Software.Model
{
    class Facilitador
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; private set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Sexo { get; set; }
        public Facilitador()
        {

        }

        public Facilitador(string nome, int idade, string sexo)
        {
            Nome = nome;
            Idade = idade;
            Sexo = sexo;
        }
    }
}
