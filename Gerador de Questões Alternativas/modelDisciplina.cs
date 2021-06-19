using System;
using System.Collections.Generic;
using System.Text;

namespace Gerador_de_Questões_Alternativas
{
    public class modelDisciplina : infra
    {
        private int      _Id         = 0;
        private int      _Codigo     = 0;
        private string   _Disciplina = "";


        public modelDisciplina()
        {
        }
        


        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        public int Codigo
        {
            get
            {
                return _Codigo;
            }
            set
            {
                _Codigo = value;
            }
        }

        public string Disciplina
        {
            get
            {
                return _Disciplina;
            }
            set
            {
                _Disciplina = value;
            }
        }

    }
}
