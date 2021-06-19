using System;
using System.Collections.Generic;
using System.Text;

namespace Gerador_de_Questões_Alternativas
{
    public abstract class infra
    {
        public infra()
        {
        }

        public string Formatted(string sValue)
        {
            if (!string.IsNullOrEmpty(sValue))
            {
                return sValue.ToUpper();
            }
            else
            {
                return sValue;
            }
        }
    }
}
