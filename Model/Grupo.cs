using SQLite;
using System;
using System.Collections.Generic;


namespace RpG_Software.Model
{
    public class Grupo
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; private set; }
        public string Sexo { get; private set; }
        public int IdadeMin { get; private set; }
        public int IdadeMax { get; private set; }
        public string Descricao { get; private set; }                
        public int facilitadorid { get; private set; }
        public bool ehEspecial { get; private set; }      
        public int facilitadorauxid { get; private set; }
        [Ignore]
        public List<Pessoa> Membro { get; private set; }

        public Grupo(string sexo, int idademin, int idademax, string facilitador, string auxiliar, bool especial)
        {
            //ID = DateTime.Now.ToString();
            Sexo = sexo;
            IdadeMin = idademin;
            IdadeMax = idademax;
            facilitadorid = Convert.ToInt32(facilitador);
            facilitadorauxid = Convert.ToInt32(auxiliar);
            ehEspecial = especial;
            Descricao = string.Format("{0} especial, {1} e {2}-{3} anos", ehEspecial == true ? "É" : "Não é", Sexo, IdadeMin, IdadeMax);
        }

        public Grupo(string sexo, int idademin, int idademax, string facilitador, string auxiliar)
        {
            //ID = DateTime.Now.ToString();
            Sexo = sexo;
            IdadeMin = idademin;
            IdadeMax = idademax;
            facilitadorid = Convert.ToInt32(facilitador);
            facilitadorauxid = Convert.ToInt32(auxiliar);
            ehEspecial = false;
            Descricao = string.Format("{0} especial, {1} e {2}-{3} anos", ehEspecial == true ? "É" : "Não é", Sexo, IdadeMin, IdadeMax);         
        }

        public Grupo(string sexo, int idademin, int idademax, bool especial)
        {
            //ID = DateTime.Now.ToString();
            Sexo = sexo;
            IdadeMin = idademin;
            IdadeMax = idademax;
            ehEspecial = especial;
            Descricao = string.Format("{0} especial, {1} e {2}-{3} anos", ehEspecial == true ? "É" : "Não é", Sexo, IdadeMin, IdadeMax);
        }

        public Grupo(string sexo, int idademin, int idademax)
        {
            //ID = DateTime.Now.ToString();
            Sexo = sexo;
            IdadeMin = idademin;
            IdadeMax = idademax;
            ehEspecial = false;
            Descricao = string.Format("{0} especial, {1} e {2}-{3} anos", ehEspecial == true ? "É" : "Não é", Sexo, IdadeMin, IdadeMax);
        }

        public Grupo()
        {

        }

        public bool addMembro(Pessoa p)
        {
            int qtdMax = (ehEspecial || IdadeMax < 18) ? 8 : 11;
            if (Membro.Count >= qtdMax)
                return false;
            if (p.Sexo == Sexo && (IdadeMax > p.Idade && p.Idade >= IdadeMin))
            {
                if (ehEspecial && p.ehEsposaDePastor == false && p.ehPastor == false)
                    return false;
                if (ehEspecial == false && (p.ehPastor || p.ehEsposaDePastor))
                    return false;
                if(Sexo == "Masculino" && p.ehEsposaDePastor)
                    return false;
                foreach(string str in p.Nome.Split(' '))
                {
                    if (str == string.Empty)
                        break;
                    if (p.Nome.IndexOf(str[0]) == 0)
                        continue;
                    var pAlea = Membro.Find(x => x.Sexo == p.Sexo && x.Nome.Contains(str) && x.ID != p.ID
                        && x.ParenteFara == "Sim" && p.ParenteFara == "Sim" 
                        && ((x.ehPastor && p.ehPastor) || (x.ehEsposaDePastor && p.ehEsposaDePastor)));
                    if (pAlea != null)
                    {
                        // é parente
                        return false;
                    }

                }
                Membro.Add(p);             
                return true;
            }
            return false;
        }
        public void AddGeral(IEnumerable<Pessoa> ps)
        {
            if (Membro == null)
            {
                Membro = new List<Pessoa>();
            }
            
            Membro.AddRange(ps);
        }
        public void setFacilitador(string p)
        {
            facilitadorid = Convert.ToInt32(p);
        }

        public void setFacilitadorAuxiliar(string p)
        {
            facilitadorauxid = Convert.ToInt32(p);
        }

        public void delMembro(string id)
        {
            Pessoa p;
            for (int i = 0; i < Membro.Count; i++)
            {
                p = Membro[i];
                if(p.ID == id)
                {
                    Membro.Remove(p);
                    return;
                }
            }
        }
    }
}
