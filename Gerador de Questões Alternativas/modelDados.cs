using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace Gerador_de_Questões_Alternativas
{
    public class modelDados : infra
    {
        private int        _Codigo       = 0;
        private int        _Disciplina   = 0;
        private int        _Assunto      = 0;
        private int        _Verdade      = 0;
        private string     _Frase        = "";
        private int        _Sequencia    = 0;
        private int        _Similar      = 0;

        public modelDados()
        {
        }


        public int    Codigo
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

        public int    Disciplina
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

        public int    Assunto
        {
            get
            {
                return _Assunto;
            }
            set
            {
                _Assunto = value;
            }
        }

        public int    Verdade
        {
            get
            {
                return _Verdade;
            }
            set
            {
                _Verdade = value;
            }
        }

        public string Frase
        {
            get
            {
                return _Frase;
            }
            set
            {
                _Frase = value;
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

        public int Similar
        {
            get
            {
                return _Similar;
            }
            set
            {
                _Similar = value;
            }
        }
    }
}
