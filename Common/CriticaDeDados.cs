using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpG_Software.Control
{
    class CriticaDeDados
    {
        public bool verifica_numero(int tecla)
        {
            bool retorno = false;

            if ((tecla >= 48 && tecla <= 58) || tecla == 8)
            {
                retorno = true;
            }
            return retorno;
        }

        public bool verifica_letra(char tecla)
        {
            bool retorno = false;
            // 8 correponde a backspace 
            // 32 corresponde a space bar
            if (char.IsLetter(tecla) || char.IsWhiteSpace(tecla) || char.IsControl(tecla))
            {
                retorno = true;
            }           
            return retorno;
        }
    }
}
