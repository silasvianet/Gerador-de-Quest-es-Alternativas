using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.ComponentModel;

namespace Gerador_de_Questões_Alternativas
{
    public class admDados : EventArgs
    {
        public event EventHandler<ProgressChangedEventArgs> ProgressoMudou;

        public List<modelDados> admSelectRows(ref List<string> lstResultado, int nCodigo, int nDisciplina, int nAssunto, int nVerdade, string strFrase, int nSimilar, int nSimilarNaoCarregar)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            int nSequencia = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDados oMc = new modelDados();

            List<modelDados> oColl = new List<modelDados>();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return oColl;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";

                    //=============================================================================================================================================================================

                    strSql = "SELECT * FROM DADOS WHERE 1 = 1";

                    if (nCodigo > 0)
                    {
                        strSql = strSql + " AND CODIGO     = " + nCodigo.ToString();
                    }

                    if (nDisciplina > 0)
                    {
                        strSql = strSql + " AND Disciplina     = " + nDisciplina.ToString();
                    }

                    if (nAssunto > 0)
                    {
                        strSql = strSql + " AND Assunto        = " + nAssunto.ToString();
                    }

                    if (nVerdade != -1)
                    {
                        strSql = strSql + " AND Verdade        = " + nVerdade.ToString();
                    }

                    if (strFrase.Length > 0)
                    {
                        strSql = strSql + " AND Frase like '%" + strFrase.Replace("'", "").Replace(((char)34).ToString(), "") + "%'";
                    }

                    if (nSimilar == 0)
                    { 
                        strSql = strSql + " AND SIMILAR   <> " + nSimilarNaoCarregar.ToString();
                    }
                    
                    strSql = strSql + " order by codigo ASC";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Frase: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDados();

                            nSequencia++;
                            oMc.Sequencia = nSequencia;

                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }

                            if (record_1["Disciplina"] != DBNull.Value)
                            {
                                oMc.Disciplina = Convert.ToInt32(record_1["Disciplina"].ToString());
                            }

                            if (record_1["Assunto"] != DBNull.Value)
                            {
                                oMc.Assunto = Convert.ToInt32(record_1["Assunto"].ToString());
                            }

                            if (record_1["Verdade"] != DBNull.Value)
                            {
                                oMc.Verdade = Convert.ToInt32(record_1["Verdade"].ToString());
                            }

                            oMc.Frase = record_1["Frase"].ToString();
                            
                            if (record_1["Similar"] != DBNull.Value)
                            {
                                oMc.Similar = Convert.ToInt32(record_1["Similar"].ToString());
                            }

                            oColl.Add(oMc);
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível carregar dados: " + strFrase);
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return oColl;
        }

        

        public List<modelDadosDescritivo> admSelectRowsDescritivo(ref List<string> lstResultado, int nCodigo, int nDisciplina, int nAssunto, int nVerdade, string strFrase)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            int nSequencia = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDadosDescritivo oMc = new modelDadosDescritivo();

            List<modelDadosDescritivo> oColl = new List<modelDadosDescritivo>();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return oColl;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";

                    //=============================================================================================================================================================================

                    strSql = "SELECT A.CODIGO,A.VERDADE,A.FRASE,(SELECT DESCRICAO FROM ASSUNTO WHERE ID = A.Assunto) AS Assunto,(SELECT Disciplina FROM DISCIPLINA WHERE id = A.Disciplina) AS Disciplina,A.SIMILAR FROM DADOS A WHERE 1 = 1";

                    if (nCodigo > 0)
                    {
                        strSql = strSql + " AND A.CODIGO     = " + nCodigo.ToString();
                    }

                    if (nDisciplina > 0)
                    {
                        strSql = strSql + " AND A.Disciplina     = " + nDisciplina.ToString();
                    }

                    if (nAssunto > 0)
                    {
                        strSql = strSql + " AND A.Assunto        = " + nAssunto.ToString();
                    }

                    if (nVerdade != -1)
                    {
                        strSql = strSql + " AND A.Verdade        = " + nVerdade.ToString();
                    }

                    if (strFrase.Length > 0)
                    {
                        strSql = strSql + " AND A.Frase like '%" + strFrase.Replace("'", "").Replace(((char)34).ToString(), "") + "%'";
                    }

                    strSql = strSql + " order by A.Frase ASC";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Frase: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDadosDescritivo();

                            nSequencia++;
                            oMc.Sequencia = nSequencia;

                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }
                            
                            oMc.Disciplina = record_1["Disciplina"].ToString();

                            oMc.Assunto = record_1["Assunto"].ToString();

                            if (record_1["Verdade"] != DBNull.Value)
                            {
                                oMc.Verdade = Convert.ToInt32(record_1["Verdade"].ToString());
                            }

                            oMc.Frase = record_1["Frase"].ToString();

                            if (record_1["Similar"] != DBNull.Value)
                            {
                                oMc.Similar = Convert.ToInt32(record_1["Similar"].ToString());
                            }

                            oColl.Add(oMc);

                            System.Threading.Thread.Sleep(5);
                            this.OnProgressoMudou(nSequencia);
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível carregar dados: " + strFrase);
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return oColl;
        }

        public List<modelDados> admSelectRowsIdMaior(ref List<string> lstResultado, int nId)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            int nSequencia = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDados oMc = new modelDados();

            List<modelDados> oColl = new List<modelDados>();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return oColl;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";
                    //=============================================================================================================================================================================

                    strSql = "SELECT * FROM DADOS WHERE CODIGO >" + nId;

                    strSql = strSql + " order by Frase ASC";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Frase: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDados();

                            nSequencia++;
                            oMc.Sequencia = nSequencia;

                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }

                            if (record_1["Disciplina"] != DBNull.Value)
                            {
                                oMc.Disciplina = Convert.ToInt32(record_1["Disciplina"].ToString());
                            }

                            if (record_1["Assunto"] != DBNull.Value)
                            {
                                oMc.Assunto = Convert.ToInt32(record_1["Assunto"].ToString());
                            }

                            if (record_1["Verdade"] != DBNull.Value)
                            {
                                oMc.Verdade = Convert.ToInt32(record_1["Verdade"].ToString());
                            }

                            oMc.Frase = record_1["Frase"].ToString();

                            if (record_1["Similar"] != DBNull.Value)
                            {
                                oMc.Similar = Convert.ToInt32(record_1["Similar"].ToString());
                            }

                            oColl.Add(oMc);

                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível carregar dados");
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return oColl;
        }

        public List<modelDadosDescritivo> admSelectRowsIdMaiorDescritivo(ref List<string> lstResultado, int nId)
        {
            string strErro1Banco  = "";
            string strErro1Record = "";
            int nSequencia        = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDadosDescritivo oMc = new modelDadosDescritivo();

            List<modelDadosDescritivo> oColl = new List<modelDadosDescritivo>();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return oColl;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";
                    //=============================================================================================================================================================================

                    strErro1Record = "";

                    strSql = "SELECT A.CODIGO,A.VERDADE,A.FRASE,(SELECT DESCRICAO FROM ASSUNTO WHERE ID = A.Assunto) AS Assunto,(SELECT Disciplina FROM DISCIPLINA WHERE id = A.Disciplina) AS Disciplina, A.SIMILAR FROM DADOS A WHERE A.CODIGO > " + nId;

                    strSql = strSql + " order by A.CODIGO";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Frase: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDadosDescritivo();

                            nSequencia++;
                            oMc.Sequencia = nSequencia;

                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }

                            oMc.Disciplina = record_1["Disciplina"].ToString();

                            oMc.Assunto = record_1["Assunto"].ToString();

                            if (record_1["Verdade"] != DBNull.Value)
                            {
                                oMc.Verdade = Convert.ToInt32(record_1["Verdade"].ToString());
                            }

                            oMc.Frase = record_1["Frase"].ToString();
                            
                            if (record_1["Similar"] != DBNull.Value)
                            {
                                oMc.Similar = Convert.ToInt32(record_1["Similar"].ToString());
                            }

                            oColl.Add(oMc);

                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível carregar dados");
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return oColl;
        }

        public int admSelectRowsIdMaiorApoio(ref List<string> lstResultado)
        {
            int nUltId = 0;

            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDados oMc = new modelDados();

            List<modelDados> oColl = new List<modelDados>();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return nUltId;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";

                    strSql = "SELECT max(CODIGO) AS CODIGO FROM DADOS";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1, ref nRegistro,ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar último id: " + strErro1Record);
                        return nUltId;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                nUltId = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }
                        }
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }
                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return nUltId;
        }

        public modelDados admSelectRowBynome(ref List<string> lstResultado, string strDescricao)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDados oMc = new modelDados();

            //***************************************************************************************************************************************************************************************************

            try
            {
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return oMc;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "";

                    //=============================================================================================================================================================================

                    strSql = "SELECT TOP 1 * FROM ASSUNTO WHERE 1 = 1";

                    if (strDescricao.Length > 0)
                    {
                        strSql = strSql + " AND Descricao = '" + strDescricao.Replace("'", "").Replace(((char)34).ToString(), "") + "'";
                    }

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Assunto: " + strErro1Record);
                        return oMc;
                    }


                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDados();

                            if (record_1["CODIGO"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["CODIGO"].ToString());
                            }

                            if (record_1["Disciplina"] != DBNull.Value)
                            {
                                oMc.Disciplina = Convert.ToInt32(record_1["Disciplina"].ToString());
                            }

                            if (record_1["Assunto"] != DBNull.Value)
                            {
                                oMc.Assunto = Convert.ToInt32(record_1["Assunto"].ToString());
                            }

                            if (record_1["Verdade"] != DBNull.Value)
                            {
                                oMc.Verdade = Convert.ToInt32(record_1["Verdade"].ToString());
                            }

                            oMc.Frase = record_1["Frase"].ToString();

                            if (record_1["Similar"] != DBNull.Value)
                            {
                                oMc.Similar = Convert.ToInt32(record_1["Similar"].ToString());
                            }
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível carregar dados:");
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro:  ==> " + ex.Message);
            }

            finally
            {

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return oMc;
        }

        protected virtual void OnProgressoMudou(int porcentagem)
        {
            EventHandler<ProgressChangedEventArgs> evento = this.ProgressoMudou;

            if (evento != null)
            {
                ProgressChangedEventArgs p = new ProgressChangedEventArgs(porcentagem, null);

                evento(this, p);
            }
        }
    }
}
