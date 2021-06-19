using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace Gerador_de_Questões_Alternativas
{
    public class admAssunto
    {
        public List<modelAssunto> admSelectRows(ref List<string> lstResultado, int nId, int ncodigoDisciplina, int nCodigoAssunto, string strDescricao)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            int nSequencia = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelAssunto oMc = new modelAssunto();

            List<modelAssunto> oColl = new List<modelAssunto>();

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

                    strSql = "SELECT * FROM ASSUNTO WHERE 1 = 1";

                    if (nId > 0)
                    {
                        strSql = strSql + " AND id     = " + nId;
                    }

                    if (ncodigoDisciplina > 0)
                    {
                        strSql = strSql + " AND codigoDisciplina     = " + ncodigoDisciplina;
                    }

                    if (nCodigoAssunto > 0)
                    {
                        strSql = strSql + " AND CodigoAssunto        = " + nCodigoAssunto;
                    }

                    if (strDescricao.Length > 0)
                    {
                        strSql = strSql + " AND Descricao = '" + strDescricao.Replace("'", "").Replace(((char)34).ToString(), "") + "'";
                    }

                    strSql = strSql + " order by id ASC";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Assunto: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelAssunto();

                            nSequencia++;
                            oMc.Sequencia = nSequencia;

                            if (record_1["id"] != DBNull.Value)
                            {
                                oMc.Id = Convert.ToInt32(record_1["id"].ToString());
                            }

                            if (record_1["CodigoDisciplina"] != DBNull.Value)
                            {
                                oMc.CodigoDisciplina = Convert.ToInt32(record_1["CodigoDisciplina"].ToString());
                            }

                            if (record_1["CodigoAssunto"] != DBNull.Value)
                            {
                                oMc.CodigoAssunto = Convert.ToInt32(record_1["CodigoAssunto"].ToString());
                            }

                            oMc.Descricao = record_1["Descricao"].ToString();

                            oColl.Add(oMc);

                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro Assunto: Codigo Disciplina: " + ncodigoDisciplina.ToString() + " , Disciplina: " + strDescricao + " , id:" + nId.ToString());
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

        public modelAssunto admSelectRowBynome(ref List<string> lstResultado, string strDescricao)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelAssunto oMc = new modelAssunto();

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

                    if (strDescricao.Length == 0)
                    {
                        return null;
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
                            oMc = new modelAssunto();

                            if (record_1["id"] != DBNull.Value)
                            {
                                oMc.Id = Convert.ToInt32(record_1["id"].ToString());
                            }

                            if (record_1["Codigo"] != DBNull.Value)
                            {
                                oMc.CodigoDisciplina = Convert.ToInt32(record_1["CodigoDisciplina"].ToString());
                            }

                            if (record_1["CodigoAssunto"] != DBNull.Value)
                            {
                                oMc.CodigoAssunto = Convert.ToInt32(record_1["CodigoAssunto"].ToString());
                            }

                            oMc.Descricao = record_1["Descricao"].ToString();

                            //lstResultado.Add("Parâmetro disciplina localizado com sucesso: Codigo: " + nCodigo.ToString() + " , Disciplina: " + strDisciplina + " , id:" + nCodigo.ToString());
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro Assunto: " + strDescricao);
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
    }
}
