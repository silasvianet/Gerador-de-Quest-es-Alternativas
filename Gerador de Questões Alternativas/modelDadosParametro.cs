using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace Gerador_de_Questões_Alternativas
{
    public class modelDadosParametro : infra
    {
        private OleDbType _TipoDados     = OleDbType.Integer;
        private string     _Campo        = "";
        private int        _Valor        = 0;

        public modelDadosParametro()
        {
        }

        public OleDbType TipoDados
        {
            get
            {
                return _TipoDados;
            }
            set
            {
                _TipoDados = value;
            }
        }

        public string     Campo
        {
            get 
            { 
                return _Campo; 
            }
            set 
            { 
                _Campo = value; 
            }
        }

        public int        Valor
        {
            get
            {
                return _Valor;
            }
            set
            {
                _Valor = value;
            }
        }
    }
}
