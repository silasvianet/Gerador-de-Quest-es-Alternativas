using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.IO;

namespace Gerador_de_Questões_Alternativas
{
    class admDisciplina
    {

        public List<modelDisciplina> admSelectRows(ref List<string> lstResultado, string strDisciplina, int nCodigo, int nId)
        {
            string strErro1Banco  = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1  = null;

            database.database dat1 = new database.database();

            modelDisciplina oMc = new modelDisciplina();

            List<modelDisciplina> oColl = new List<modelDisciplina>();

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

                    strSql = "SELECT * FROM DISCIPLINA WHERE 1 = 1";

                    if (strDisciplina.Length > 0)
                    {
                        strSql = strSql + " AND Disciplina = '" + strDisciplina.Replace("'", "").Replace(((char)34).ToString(), "") + "'";
                    }

                    if (nCodigo > 0)
                    {
                        strSql = strSql + " AND Codigo     = " + nCodigo;
                    }

                    if (nId > 0)
                    {
                        strSql = strSql + " AND iD         = " + nId;
                    }

                    strSql = strSql + " order by disciplina desc";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro disciplina: " + strErro1Record);
                        return oColl;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDisciplina();

                            if (record_1["id"] != DBNull.Value)
                            {
                                oMc.Id = Convert.ToInt32(record_1["id"].ToString());
                            }

                            if (record_1["Codigo"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["Codigo"].ToString());
                            }

                            oMc.Disciplina = record_1["Disciplina"].ToString();

                            oColl.Add(oMc);

                            //lstResultado.Add("Parâmetro disciplina localizado com sucesso: Codigo: " + nCodigo.ToString() + " , Disciplina: " + strDisciplina + " , id:" + nCodigo.ToString());
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro disciplina: Codigo: " + nCodigo.ToString() + " , Disciplina: " + strDisciplina + " , id:" + nCodigo.ToString());
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

        public modelDisciplina admSelectRowBynome(ref List<string> lstResultado, string strDisciplina)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDisciplina oMc = new modelDisciplina();

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


                    if (strDisciplina.Length == 0)
                    {
                        return null;
                    }

                    var strSql = "";

                    //=============================================================================================================================================================================

                    strSql = "SELECT TOP 1 * FROM DISCIPLINA WHERE 1 = 1";

                    if (strDisciplina.Length > 0)
                    {
                        strSql = strSql + " AND Disciplina = '" + strDisciplina.Replace("'", "").Replace(((char)34).ToString(), "") + "'";
                    }

                    strSql = strSql + " order by disciplina ASC";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1,ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro disciplina: " + strErro1Record);
                        return oMc;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oMc = new modelDisciplina();

                            if (record_1["id"] != DBNull.Value)
                            {
                                oMc.Id = Convert.ToInt32(record_1["id"].ToString());
                            }

                            if (record_1["Codigo"] != DBNull.Value)
                            {
                                oMc.Codigo = Convert.ToInt32(record_1["Codigo"].ToString());
                            }

                            oMc.Disciplina = record_1["Disciplina"].ToString();

                            //lstResultado.Add("Parâmetro disciplina localizado com sucesso: Codigo: " + nCodigo.ToString() + " , Disciplina: " + strDisciplina + " , id:" + nCodigo.ToString());
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro disciplina: " + strDisciplina);
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
