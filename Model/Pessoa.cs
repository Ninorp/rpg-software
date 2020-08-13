using System.Linq;

namespace RpG_Software.Model
{
    public class Pessoa
    {
        public string ID { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Celular { get; private set; }
        public string Telefone { get; private set; }
        public int Idade { get; private set; }
        public string Sexo { get; private set; }
        public string EstadoCivil { get; private set; }
        public string ParenteFara { get; private set; }
        public string Igreja { get; private set; }
        public string Areas { get; private set; }
        public bool ehPastor { get; private set; }
        public bool ehEsposaDePastor { get; private set; }

        public Pessoa(string datah, string nome, string email, string celular, string telefone, 
            int idade, string sexo, string estado, string parente, string igreja, string areas)
        {
            ID = new string(datah.Where(c => char.IsDigit(c)).ToArray());
            Nome = nome;
            Email = email;
            Celular = celular;
            Telefone = telefone;
            Idade = idade;
            Sexo = sexo;
            EstadoCivil = estado;
            ParenteFara = parente;
            Igreja = igreja;
            Areas = areas;
        }
        public Pessoa(string datah, string nome, string email, string celular, string telefone,
            int idade, string sexo, string estado, string parente, string igreja, string areas, string pastor, string esposa)
        {
            ID = new string(datah.Where(c => char.IsDigit(c)).ToArray());
            Nome = nome;
            Email = email;
            Celular = celular;
            Telefone = telefone;
            Idade = idade;
            Sexo = sexo;
            EstadoCivil = estado;
            ParenteFara = parente;
            Igreja = igreja;
            Areas = areas;
            ehPastor = (pastor == "Sim") ? true : false;
            ehEsposaDePastor = (esposa == "Sim") ? true : false;
        }
        public Pessoa()
        {

        }
    }
}
