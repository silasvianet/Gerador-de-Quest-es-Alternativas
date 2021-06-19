using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;              //Conexão sql.
using System.Data.Odbc;                   //Conexão odbc.
using System.Data.OleDb;                  //Conexão access.

using Gerador_de_Questões_Alternativas;

namespace database  
{
    class database 
    {

        OleDbConnection conAccessGlobal;  //Conexão sql.
        SqlConnection   conSqlGlobal;     //Conexão odbc.
        OdbcConnection  conOdbcGlobal;    //Conexão access.


        //Conexão geral. ok tem variável global  e tipo de conexão.
        #region CONEXÃO_BANCO

        //(Sql Server)
        //Efetua conexão e abre banco de dados retorna null em caso de erro.
        public SqlConnection conexaoBancoSql(string dbConexao, string dbUsuario, string dbSenha, string dbBanco, ref string dbErro, int timeout, string strEncrypt)
        {
            //montagem da string de conexão
            string ConnectionString = "Data Source="     + dbConexao  + ";";
            ConnectionString       += "User ID="         + dbUsuario  + ";";
            ConnectionString       += "Password="        + dbSenha    + ";";
            ConnectionString       += "Initial Catalog=" + dbBanco    + ";Connection Timeout=" + timeout.ToString() + ";";
            ConnectionString       += "Encrypt ="        + strEncrypt + ";";


            

            try
            {
                conSqlGlobal = new SqlConnection(ConnectionString);
                
                conSqlGlobal.Open();
            }
            catch (Exception e)
            {
                dbErro       = e.Message;
                conSqlGlobal = null;            
            }

            return conSqlGlobal;
        }
        
        //(Access)
        //Efetua conexão e abre banco de dados retorna null em caso de erro.
        public OleDbConnection conexaoBancoAccess(string dbCaminho, string dbUsuario, string dbSenha, ref string dbErro)
        {
            //montagem da string de conexão
            //em banco web ,recomendo retirar:  Mode=Share Deny None;
            string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbCaminho + ";Mode=Share Deny None; User ID= " + dbUsuario + ";Password=" + dbSenha;
            
            //string ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbCaminho + ";User ID= " + dbUsuario + ";Password=" + dbSenha;

            conAccessGlobal = new OleDbConnection(ConnectionString);

            try
            {
                conAccessGlobal.Open();
            }
            catch (Exception e)
            {
                dbErro = e.Message;
                conAccessGlobal = null;
            }

            return conAccessGlobal;
        }
        
        //(Odbc)
        //Efetua conexão e abre banco de dados retorna null em caso de erro.
        public OdbcConnection  conexaoBancoOdbc(string dbSource, string dbUsuario, string dbSenha, string dbBanco, ref string dbErro)
        {
            //montagem da string de conexão
            string ConnectionString = "dsn=" + dbSource + ";UID=" + dbUsuario + ";PWD=" + dbSenha + ";";

            conOdbcGlobal = new OdbcConnection(ConnectionString);

            try
            {
                conOdbcGlobal.Open();
            }
            catch (Exception e)
            {
                dbErro = e.Message;
                conOdbcGlobal = null;
            }

            return conOdbcGlobal;
        }




        #endregion


        //Executa geral. ok retorna true/false e tipo de conexão.
        //-----------------------------
        #region EXECUTA SQL

        //(Sql Server)
        //Retorna resultado do processamento.
        public bool            conexaoExecutaSql(string stringSql, SqlConnection con, ref string dbErro) 
        {

           bool _status = false;  //Começa verdadeiro
           int  _Proc   = 0;      //_Proc = Quantidade retornada pelo executeNonQuery.

           if (con == null)
           {
               return _status;
           }


            try
            {
                SqlCommand sqlc = new SqlCommand(stringSql,con);

                _Proc = sqlc.ExecuteNonQuery();
                
            }
            catch (Exception e)
            {
                dbErro = e.Message;
                _status = false;
            }

               //Retorna a quantidade de processamento, caso seja maior que 0, processamento foi efetuado.
               if (_Proc > 0) 
               {
                  _status = true;      //Caso o processamento foi efetuado com sucesso.
               }
               else
               {
                    dbErro = "Informação não inserida na fonte de dados, detalhe do erro: " + dbErro;
                   _status = false;    //Caso o processamento não foi concluido.
               }

            return _status;

        }
        
        //(Access)
        //retorna OleDbDataReader
        public bool            conexaoExecutaAccess(string stringSql, OleDbConnection con, ref string dbErro)
        {


            bool _status = false;  //Começa verdadeiro
            int _Proc = 0;      //_Proc = Quantidade retornada pelo executeNonQuery.

            if (con == null)
            {
                return _status;
            }

            try
            {
                OleDbCommand sqlc = new OleDbCommand(stringSql, con);

                _Proc = sqlc.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                dbErro = e.Message;
                _status = false;
            }


            //Retorna a quantidade de processamento, caso seja maior que 0, processamento foi efetuado.
            if (_Proc > 0)
            {
                _status = true;      //Caso o processamento foi efetuado com sucesso.
            }
            else
            {
                dbErro = "Informação não inserida na fonte de dados." + dbErro;
                _status = false;    //Caso o processamento não foi concluido.
            }


            //Fecha Conexão
            con.Close();
            con.Dispose();

            //Fecha conexão geral.
            fechaAccess();

            return _status;

        }


        public bool conexaoExecutaAccess(string stringSql, OleDbConnection con, ref string dbErro,List<modelDados> lstDados,List<modelDadosParametro> lstParametro)
        {
            bool _status = false;  //Começa verdadeiro
            int _Proc = 0;         //_Proc = Quantidade retornada pelo executeNonQuery.

            if (con == null)
            {
                return _status;
            }

            //try
            //{
                OleDbCommand sqlc = new OleDbCommand(stringSql, con);
                
                 sqlc.Parameters.Add(lstParametro[0].Campo.ToString(), lstParametro[0].TipoDados, lstParametro[0].Valor).Value = lstDados[0].Disciplina;
                 sqlc.Parameters.Add(lstParametro[1].Campo.ToString(), lstParametro[1].TipoDados, lstParametro[1].Valor).Value = lstDados[0].Assunto;
                 sqlc.Parameters.Add(lstParametro[2].Campo.ToString(), lstParametro[2].TipoDados, lstParametro[2].Valor).Value = lstDados[0].Verdade;
                 sqlc.Parameters.Add(lstParametro[3].Campo.ToString(), lstParametro[3].TipoDados, lstParametro[3].Valor).Value = lstDados[0].Frase;
                 sqlc.Parameters.Add(lstParametro[4].Campo.ToString(), lstParametro[4].TipoDados, lstParametro[4].Valor).Value = lstDados[0].Similar;

                _Proc = sqlc.ExecuteNonQuery();

            //}
            //catch (Exception e)
            //{
            //    dbErro = e.Message;
            //    _status = false;
            //}


            //Retorna a quantidade de processamento, caso seja maior que 0, processamento foi efetuado.
            if (_Proc > 0)
            {
                _status = true;      //Caso o processamento foi efetuado com sucesso.
            }
            else
            {
                dbErro = "Informação não inserida na fonte de dados." + dbErro;
                _status = false;    //Caso o processamento não foi concluido.
            }


            //Fecha Conexão
            con.Close();
            con.Dispose();

            //Fecha conexão geral.
            fechaAccess();

            return _status;

        }
        


        //(Odbc)
        //Retorna resultado do processamento
        public OdbcDataReader  conexaoExecutaOdbc(string stringSql, OdbcConnection con, ref string dbErro)
        {

            OdbcDataReader sqlr = null;

            try
            {
                OdbcCommand sqlc = new OdbcCommand(stringSql, con);

                sqlr = sqlc.ExecuteReader();

            }
            catch (Exception e)
            {
                dbErro = e.Message;
                sqlr = null;
            }

            return sqlr;

        }
        
        #endregion


        //Recordset geral. problema esta aqui , pelo fato do retorno ser diferente.
        #region RECORDSET

        //(Sql Server)
        //Retorna resultado do processamento.
        public SqlDataReader   conexaoRecordSql(string stringSql, SqlConnection con, ref string dbErro)
        {

            SqlDataReader record;

            SqlCommand command = new SqlCommand(stringSql, con);

            try
            {
                record = command.ExecuteReader();
            }
            catch (Exception e)
            {
                record = null;
                dbErro = e.Message;
            }

            return record;

        }
       
        //(Access)
        //Retorna resultado do processamento.
        public OleDbDataReader conexaoRecordAccess(string SelectSql, OleDbConnection con, ref Int64 nRegistro, ref string dbErro)
        {
            OleDbDataReader record;

            OleDbCommand command = new OleDbCommand(SelectSql, con);

            try
            {
                nRegistro = 0;

                record    = command.ExecuteReader();

                if (record.HasRows)
                {
                    while (record.Read())
                    {
                        nRegistro++;
                    }
                }

                if (record != null)
                {
                    record.Close();
                }

                record = command.ExecuteReader();

            }
            catch (Exception e)
            {
                record = null;
                dbErro = e.Message;
            }

            return record;
        }


        //Retorna resultado do processamento.
        public OleDbDataReader conexaoRecordAccessParametro(string SelectSql, OleDbConnection con,string strFrase, int nAssunto, int nDisciplina, ref Int64 nRegistro, ref string dbErro)
        {
            OleDbDataReader record;

            OleDbCommand command = new OleDbCommand(SelectSql, con);

            try
            {
                nRegistro = 0;

                command.Parameters.Add("Frase", OleDbType.VarChar, 500).Value     = strFrase;
                command.Parameters.Add("Assunto", OleDbType.Integer, 20).Value    = nAssunto;
                command.Parameters.Add("Disciplina", OleDbType.Integer, 20).Value = nDisciplina;

                record = command.ExecuteReader();

                if (record.HasRows)
                {
                    while (record.Read())
                    {
                        nRegistro++;
                    }
                }

                if (record != null)
                {
                    record.Close();
                }

                record = command.ExecuteReader();

            }
            catch (Exception e)
            {
                record = null;
                dbErro = e.Message;
            }

            return record;
        }


        #endregion
       

        //Fecha Conexão geral. ok tem variável geral e tipo de conexão.
        #region FECHAR_CONEXÃO

        //(Sql Server)
        //Fecha Conexão sql
        public void fechaSql()
        {
            if (conSqlGlobal != null)
            {
                if (conSqlGlobal.State == System.Data.ConnectionState.Open)
                {
                    conSqlGlobal.Close();
                    conSqlGlobal.Dispose();
                }
            }
        }        
        
        //(Access)
        //Fecha conexão acess.
        public void fechaAccess() 
       {
           if (conAccessGlobal != null)
           {
               if (conAccessGlobal.State == System.Data.ConnectionState.Open)
               {
                   conAccessGlobal.Close();
                   conAccessGlobal.Dispose();
               }
           }
       }
       
        //(Odbc)
        //Fecha Conexão sql
        public void fechaOdbc()
       {
           if (conOdbcGlobal != null)
           {
               if (conOdbcGlobal.State == System.Data.ConnectionState.Open)
               {
                   conOdbcGlobal.Close();
                   conOdbcGlobal.Dispose();
               }
           }
       }

        #endregion
       
 


        //*****************************************************************************************
        //Aplicativos.

        #region POP3

        //Montagem de Insert pop3.
        //
        public string montSqlInsertPop3(string[] strCampo, string[] strDados)  
        {
            //montagem da string principal, para inserir informação
            string _strSqlMontado = "insert into [" + strCampo[13] + "].[dbo].[" + strCampo[15] + "] (";

            //montagem da string com nome dos dados.
            string _strSqlCampo = "";

            //montagem da string contendo dados.
            string _strSqlDados = "  values (";

            //percorre campo e dados, para montagem da INSERT INTO
            for (var i = 16; i < 26; i++)
            {
                if (strCampo[i] != null && strCampo[i].Trim().Length > 0)
                {
                    _strSqlCampo += " [" + strCampo[i] + "],";

                    if (i == 16) { _strSqlDados += "'" + strDados[3].Trim()  + "',"; }
                    if (i == 17) { _strSqlDados += "'" + strDados[17].Trim() + "',"; }
                    if (i == 18) { _strSqlDados += "'" + strDados[18].Trim() + "',"; }
                    if (i == 19) { _strSqlDados += "'" + strDados[19].Trim() + "',"; }
                    if (i == 20) { _strSqlDados += "'" + strDados[1].Trim()  + "',"; }
                    if (i == 21) { _strSqlDados += "'" + strDados[7].Trim()  + "',"; }
                    if (i == 22) { _strSqlDados += "'" + strDados[13].Trim() + "',"; }
                    if (i == 23) { _strSqlDados += "'" + strDados[16].Trim() + "',"; }

                    if (i == 24) { _strSqlDados += "'" + "0" + "',"; }
                    if (i == 25) { _strSqlDados += "'" + "0" + "',"; }

                }
            }

            return _strSqlMontado += _strSqlCampo.Substring(1, _strSqlCampo.Trim().Length - 1) + ")" + _strSqlDados.Substring(1, _strSqlDados.Trim().Length) + ")";
        }
        
        //montagem de Select para procurar id semelhante.
        //--------------------------------
        public string montSqlSelectPop3(string[] strCampo, string[] strDados)
        {

            string _strSqlMontado = "";

            if (strCampo[21] != null && strDados[7] != null)
            {
                //montagem da string principal, para selecionar id
                _strSqlMontado = "select  " + strCampo[21] + " from  [" + strCampo[13] + "].[dbo].[" + strCampo[15] + "]  with(nolock)   where " + strCampo[21] + " = '" + strDados[7] + "'";
            }
            
            return _strSqlMontado;  //retorna em branco quando não é possível montar select, por falta de informações.

        }
        
        //Salva dados pop3.
        //--------------------------------
        public bool salvaDadosPop3(int conexao, string[] strConfig, string[] strDados, ref string dbErro) 
        {
          bool _status = false;


          string _dbErroConexao = "";

          SqlDataReader record_1;

          //conexão Sql Server
          if (conexao == 1)
          {
              //strEntrada[9]  conexão
              //strEntrada[11] usuário
              //strEntrada[12] senha
              //strEntrada[13] banco

              //strEntrada[16] campo veículo
              //.
              //.

              conSqlGlobal = conexaoBancoSql(strConfig[9], strConfig[11], strConfig[12], strConfig[13], ref _dbErroConexao, int.Parse(strConfig[27]), strConfig[8]);

              //Verifica se todos os campos estão preenchidos.
              if (strConfig[16] != null || strConfig[17] != null || strConfig[18] != null || strConfig[19] != null || strConfig[20] != null || strConfig[21] != null || strConfig[22] != null || strConfig[23] != null)
              {

                  //Carrega dados.
                  record_1 = conexaoRecordSql(montSqlSelectPop3(strConfig, strDados), conSqlGlobal, ref dbErro);

                  if (dbErro.Trim().Length > 0)
                  {
                      //Acumula erro.
                      dbErro = dbErro + " " + _dbErroConexao;

                      return false;
                  }


                  //Função verifica se já existe id repetido no banco de dados, evita duplicidade de dados.
                  if (record_1 != null)
                  {

                      if (record_1.HasRows)
                      {
                          //Já existe esse ID, não precisa salvar.
                          _status = true;
                      }
                      else
                      {
                          //Fecha record_1 porque esta associado a essa conexão e não permite o uso por outro objeto.
                          record_1.Close();

                          //Não existe esse ID. 
                          _status = conexaoExecutaSql(montSqlInsertPop3(strConfig, strDados), conSqlGlobal, ref dbErro);

                          dbErro = dbErro + " " + _dbErroConexao;

                          //Fecha banco de dados.
                          fechaSql();

                      }
                  }
                  else
                  {

                      //Já existe esse ID, não precisa salvar.
                      _status = true;

                  }



              }
              else
              {
                  _status = false;
              }



          }

            return _status;
        }
        
        #endregion 

        #region Converte

        //salva dados processados.
        //--------------------------------
        public bool   salvaDados(ref string[] strInformacao,ref string[] strParametro,ref string dbErroConexao,ref string dbErroRecord)
        {
            bool   _status        = true;

            string _strRetorno    = "";
            

            //Verifica-se informação é panico ou teclado, evita salvar no banco de dados.
            if (strInformacao[10].IndexOf("PANICO", 0) >= 0)
            {

                //Salva panico como mensagem.

            }
            else
            {

                
                //Não Salva posição -0.00 -0.00 , gerar um log de erro.
                if (strInformacao[8] != "-0.00" && strInformacao[9] != "-0.00")
                {



                    //execProcedureSqlDescricao     ver retorno **********************************

                    dbErroConexao = "";

                    _strRetorno = execProcedureSqlDescricao(ref strInformacao, ref strParametro, ref dbErroConexao);
                    if (dbErroConexao.Trim().Length > 0)
                    {

                        dbErroConexao = dbErroConexao + " PROCEDURE: sp_S_Rastreamento_RetDesc ";
                        _status = false;
                    }




                    //execProcedureSqlRastreamento  ver retorno **********************************

                    dbErroConexao = "";

                    _strRetorno = execProcedureSqlRastreamento(ref strInformacao, ref strParametro, ref dbErroConexao, _strRetorno);
                    if (dbErroConexao.Trim().Length > 0)
                    {

                        dbErroConexao = dbErroConexao + " PROCEDURE: sp_I_PosSat_Rastreamento ";
                        _status = false;
                    }



                    //execProcedureSqlOnline         ver retorno **********************************  

                    dbErroConexao = "";

                    _strRetorno = execProcedureSqlOnline(ref strInformacao, ref strParametro, ref dbErroConexao);
                    if (dbErroConexao.Trim().Length > 0)
                    {

                        dbErroConexao = dbErroConexao + " PROCEDURE: sp_I_PosSat_Online ";
                        _status = false;
                    }

                    
                    //execProcedureSqlP_cerca       ver retorno **********************************  

                    //dbErroConexao = "";

                    //_strRetorno = execProcedureSqlCerca(ref strInformacao, ref strParametro, ref dbErroConexao);
                    //if (dbErroConexao.Trim().Length > 0)
                    //{
                    //
                    //    dbErroConexao = dbErroConexao + " PROCEDURE: p_cerca ";
                    //    _status = false;
                    //}


                    //execProcedureSqlVelocidade    ver retorno **********************************  

                    dbErroConexao = "";

                    _strRetorno = execProcedureSqlVelocidade(ref strInformacao, ref strParametro, ref dbErroConexao);
                    if (dbErroConexao.Trim().Length > 0)
                    {

                        dbErroConexao = dbErroConexao + " PROCEDURE: p_vel_limite ";
                        _status = false;
                    }

                    
                }


                fechaSql();

            }

            return _status;
        }

        //atualiza dados processados.
        //--------------------------------
        public bool   atualizaDadosBrutos(ref string[] strParametro, ref string[] strInformacao, ref string dbErroConexao, ref string dbErroRecord,int tipoProcessamento)
        {
            
            bool _status = true;

            string sqlUpdate;

            string _dbErroRecord = "";
            string _dbErroConexao = "";

            

            //update na tabela bruta.


            //tratamento a ser removido futuramente: encoding - sem parametro, sua função é salvar alerta do xml do globalstar.
            if (strParametro[11].Length > 0)
            {
                sqlUpdate = "update [" + strInformacao[3] + "" + "].[dbo].[" + strInformacao[4] + "" + "] set " + strInformacao[12] + " = " + tipoProcessamento.ToString() + "," + strInformacao[6] + " = '" + strParametro[11] + "'  where " + strInformacao[13] + " = " + strParametro[7];
            }
            else
            {
                sqlUpdate = "update [" + strInformacao[3] + "" + "].[dbo].[" + strInformacao[4] + "" + "] set " + strInformacao[12] + " = " + tipoProcessamento.ToString() + " where " + strInformacao[13] + " = " + strParametro[7];
            }
            

            if (conexaoExecutaSql(sqlUpdate, conexaoBancoSql(strInformacao[0], strInformacao[1], strInformacao[2], strInformacao[3], ref _dbErroConexao, int.Parse(strParametro[27]), strParametro[8]), ref _dbErroRecord) == false)
            {
                _status = false;
            }

            fechaSql();

            return _status;
        }

        //Executa procedimento online.
        //--------------------------------
        public string execProcedureSqlOnline(ref string[] strInformacao, ref string[] strParametro, ref string dbErroConexao)
        {

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[37].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@codigovei",        SqlDbType.Text).Value = strInformacao[4];
                comm.Parameters.Add("@longitude",        SqlDbType.Text).Value = strInformacao[8];
                comm.Parameters.Add("@latitude",         SqlDbType.Text).Value = strInformacao[9];
                comm.Parameters.Add("@velo",             SqlDbType.Text).Value = "0";
                comm.Parameters.Add("@datahora",         SqlDbType.Text).Value = strInformacao[10];

                comm.ExecuteScalar();

            }
            catch (Exception e)
            {
                dbErroConexao = "ERRO " + e.Message;
            }

            return "";
        }

        //Executa procedimento rastreamento.
        //--------------------------------
        public string execProcedureSqlRastreamento(ref string[] strInformacao, ref string[] strParametro, ref string dbErroConexao, string _strDescricao)
        {
            string _strRetorno = "";

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[36].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@CdVeiculoID",      SqlDbType.Text).Value     = strInformacao[4];
                comm.Parameters.Add("@DtHrRastreamento", SqlDbType.DateTime).Value = strInformacao[10];
                comm.Parameters.Add("@NrLong",           SqlDbType.Text).Value     = strInformacao[8];
                comm.Parameters.Add("@NrLat",            SqlDbType.Text).Value     = strInformacao[9];
                comm.Parameters.Add("@Desc",             SqlDbType.Text).Value     = _strDescricao;
                comm.Parameters.Add("@Velo",             SqlDbType.Int).Value      = "0";

                _strRetorno = comm.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                dbErroConexao = "ERRO " + e.Message;
            }

            return _strRetorno;
        }

        //Executa procedimento descricao.
        //--------------------------------
        public string execProcedureSqlDescricao(ref string[] strInformacao, ref string[] strParametro, ref string dbErroConexao)
        {
            string _strRetorno    = "";

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[35].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@NrLong", SqlDbType.Text).Value    = strInformacao[8];
                comm.Parameters.Add("@NrLat",  SqlDbType.Text).Value    = strInformacao[9];

                _strRetorno = comm.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                dbErroConexao = "ERRO " + e.Message;
            }

            return _strRetorno;
        }

        //Executa procedimento panico.
        //--------------------------------
        public bool   execProcedureSqlPanico(ref string[] strParametro, string strVeiculo, DateTime datDataHora, int intTipo, int intStatus,string strAviso) 
        {
            bool _status = true;

            string _dbErroRecord = "";
            string _dbErroConexao = "";

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[34].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref _dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@CdVei", SqlDbType.Text).Value     = strVeiculo;
                comm.Parameters.Add("@DtHr",  SqlDbType.DateTime).Value = datDataHora;
                comm.Parameters.Add("@tip",   SqlDbType.Int).Value      = intTipo;
                comm.Parameters.Add("@st",    SqlDbType.Int).Value      = intStatus;
                comm.Parameters.Add("@desc",  SqlDbType.Text).Value     = strAviso;

                comm.ExecuteScalar();
            }
            catch (Exception e)
            {
                _status = false;
                _dbErroRecord = "ERRO " + e.Message;
            }

            return _status;
        }

        //Executa procedimento cerca.
        //--------------------------------
        public string execProcedureSqlCerca(ref string[] strInformacao, ref string[] strParametro, ref string _dbErroConexao)
        {
            string _strRetorno = "";

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[38].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref _dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@CODIGO_VEI", SqlDbType.Text).Value     = strInformacao[4];
                comm.Parameters.Add("@LONGITUDE",  SqlDbType.Float).Value    = strInformacao[8];
                comm.Parameters.Add("@LATITUDE",   SqlDbType.Float).Value    = strInformacao[9];
                comm.Parameters.Add("@DATA",       SqlDbType.DateTime).Value = strInformacao[10];

                _strRetorno = comm.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                _dbErroConexao = "ERRO " + e.Message;
            }

            return _strRetorno;
        }

        //Executa procedimento velocidade.
        //--------------------------------
        public string execProcedureSqlVelocidade(ref string[] strInformacao, ref string[] strParametro, ref  string _dbErroConexao)
        {
            string _strRetorno = "";

            try
            {

                SqlCommand comm = new SqlCommand(strParametro[39].Trim(), conexaoBancoSql(strParametro[0], strParametro[1], strParametro[2], strParametro[3], ref _dbErroConexao, int.Parse(strParametro[27]), strParametro[8]));

                comm.CommandType = CommandType.StoredProcedure;

                comm.Parameters.Add("@CODIGO_VEI", SqlDbType.Text).Value    = strInformacao[4]; ;
                comm.Parameters.Add("@DATA_HORA", SqlDbType.DateTime).Value = strInformacao[10]; ;

                comm.ExecuteScalar();
            }
            catch (Exception e)
            {
                _dbErroConexao = "ERRO " + e.Message;
            }

            return _strRetorno;
        }

        #endregion

        #region TELESEED

        //Atualiza dados.
        //--------------------------------
        public bool atualizaTeleseed(ref string[] strparametro, int intValor, int IntRegistro, ref string dbErro)
        {

            bool _status = true;

            string _dbErroConexao = "";

            string strSql = "update [" + strparametro[4] + "].[dbo].[" + strparametro[5] + "] set " + strparametro[10] + " = " + intValor + "  where " + strparametro[8] + " = " + IntRegistro + "";

            //Carrega conexão.
            conSqlGlobal = conexaoBancoSql(strparametro[1], strparametro[2], strparametro[3], strparametro[4], ref _dbErroConexao, int.Parse(strparametro[23]), strparametro[24]);


            if (conexaoExecutaSql(strSql, conSqlGlobal, ref dbErro) == false)
            {
                dbErro  = dbErro + " " + _dbErroConexao;
                _status = false;
            }


            //Fecha Conexão sql.
            fechaSql();

            return _status;

        }

        #endregion

        //*****************************************************************************************



        public void CreateMyOleDbCommand(OleDbConnection connection,
    string queryString, OleDbParameter[] parameters)
        {
            OleDbCommand command = new OleDbCommand(queryString, connection);
            command.CommandText =
                "SELECT CustomerID, CompanyName FROM Customers WHERE Country = ? AND City = ?";
            command.Parameters.Add(parameters);

            for (int j = 0; j < parameters.Length; j++)
            {
                command.Parameters.Add(parameters[j]);
            }

            string message = "";
            for (int i = 0; i < command.Parameters.Count; i++)
            {
                message += command.Parameters[i].ToString() + "\n";
            }
            Console.WriteLine(message);
        }

    }
}
