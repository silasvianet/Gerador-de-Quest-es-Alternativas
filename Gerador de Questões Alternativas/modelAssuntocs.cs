using System;
using System.Collections.Generic;
using System.Text;

namespace Gerador_de_Questões_Alternativas
{
    public class modelAssunto : infra
    {
        private int      _Id               = 0;
        private int      _CodigoDisciplina = 0;
        private int      _CodigoAssunto    = 0;
        private string   _Descricao        = "";
        private int      _Sequencia        = 0;

        public modelAssunto()
        {
        }


        public int    Id
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

        public int    CodigoDisciplina
        {
            get
            {
                return _CodigoDisciplina;
            }
            set
            {
                _CodigoDisciplina = value;
            }
        }

        public int    CodigoAssunto
        {
            get
            {
                return _CodigoAssunto;
            }
            set
            {
                _CodigoAssunto = value;
            }
        }

        public string Descricao
        {
            get
            {
                return Formatted(_Descricao);
            }
            set
            {
                _Descricao = value;
            }
        }

        public int Sequencia 
        {
            get
            {
                return _Sequencia;
            }
            set
            {
                _Sequencia = value;
            }
        }

    }
}
