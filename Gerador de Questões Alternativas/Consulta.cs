using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.SqlClient;
using System.IO;

namespace Gerador_de_Questões_Alternativas
{
    public partial class Consulta : Form
    {
        public Consulta()
        {
            InitializeComponent();
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            string strErro = "";

            if (MessageBox.Show("Deseja limpa o banco de dados, essa ação ira remover todos os dados.", "Remover dados", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (LimparBancoDados(ref strErro) == false)
                {
                    MessageBox.Show("Erro, em Limpar banco de dados:" + strErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (LimparBancoAssunto(ref strErro) == false)
                    {
                        MessageBox.Show("Erro, em Limpar banco de dados:" + strErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (LimparBancoDisciplina(ref strErro) == false)
                        {
                            MessageBox.Show("Erro, em Limpar banco de dados:" + strErro, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Banco de dados zerado...");
                        }
                    }
                }

                gdvExcel.DataSource = null;
                gdvExcel.Refresh();

            }

        }

        private void btnImportacao_Click(object sender, EventArgs e)
        {
            int nRegistro = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();

            List<string> lstResultado = new List<string>();

            admDados oAdmDados = new admDados();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                int nultId = oAdmDados.admSelectRowsIdMaiorApoio(ref lstResultado);

                pnlManutencao.Visible = true;
                pBar.Value = 0;
                if (importacaoExcelDisciplina(openFileDialog.FileName, ref lstResultado) == false)
                {
                    //retorno
                }
                System.Threading.Thread.Sleep(10);
                pnlManutencao.Visible = false;

                //***************************************************************************************************************************

                pnlManutencao.Visible = true;
                pBar.Value = 0;
                if (importacaoExcelAssunto(openFileDialog.FileName, ref lstResultado) == false)
                {
                    //retorno
                }
                System.Threading.Thread.Sleep(10);
                pnlManutencao.Visible = false;

                //***************************************************************************************************************************

                pnlManutencao.Visible = true;
                pBar.Value = 0;
                if (importacaoExcelDados(openFileDialog.FileName, ref lstResultado) == false)
                {
                    //retorno
                }
                System.Threading.Thread.Sleep(10);
                pnlManutencao.Visible = false;

                //***************************************************************************************************************************

                lstImportacao.Items.Clear();

                pnlManutencao.Visible = true;
                pBar.Value = 0;
                foreach (string oColl in lstResultado)
                {
                    lstImportacao.Items.Add(oColl);

                    AvisoDoProgresso(nRegistro, lstResultado.Count);
                }

                lstImportacao.Visible = true;
                btnFecharList.Visible = true;

                CarregaImportacao(nultId);

                System.Threading.Thread.Sleep(1);
                pnlManutencao.Visible = false;

                MessageBox.Show("Para concluir importação, reinicialize o sistema.");
            }
        }



        public bool LimparBancoDados(ref string strErro1Banco)
        {
            strErro1Banco = "";

            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            //***************************************************************************************************************************************************************************************************
            try
            {

                //=============================================================================================================================================================================
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);

                    if (strErro1Banco.Length > 0)
                    {
                        return false;
                    }

                    var strSql = "DELETE * FROM DADOS";

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Banco) == false)
                    {
                        return false;
                    }

                }
                //=============================================================================================================================================================================


            }
            catch (Exception ex)
            {
                strErro1Banco = strErro1Banco + ex.Message;
            }

            finally
            {

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return true;
        }

        public bool LimparBancoAssunto(ref string strErro1Banco)
        {
            strErro1Banco = "";

            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            //***************************************************************************************************************************************************************************************************
            try
            {

                //=============================================================================================================================================================================
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);

                    if (strErro1Banco.Length > 0)
                    {
                        return false;
                    }

                    var strSql = "DELETE * FROM ASSUNTO";

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Banco) == false)
                    {
                        return false;
                    }

                }
                //=============================================================================================================================================================================


            }
            catch (Exception ex)
            {
                strErro1Banco = strErro1Banco + ex.Message;
            }

            finally
            {

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return true;
        }

        public bool LimparBancoDisciplina(ref string strErro1Banco)
        {
            strErro1Banco = "";

            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            //***************************************************************************************************************************************************************************************************
            try
            {

                //=============================================================================================================================================================================
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);

                    if (strErro1Banco.Length > 0)
                    {
                        return false;
                    }

                    var strSql = "DELETE * FROM DISCIPLINA";

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Banco) == false)
                    {
                        return false;
                    }

                }
                //=============================================================================================================================================================================


            }
            catch (Exception ex)
            {
                strErro1Banco = strErro1Banco + ex.Message;
            }

            finally
            {

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            }

            //***************************************************************************************************************************************************************************************************

            return true;
        }



        public bool importacaoExcelDados(string strArquivoCompleto, ref List<string> lstResultado)
        {
            string strControleProcessamento = DateTime.Now.ToString();
            bool bStatus = false;
            int iQtdRecords = 0;

            //try
            //{
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application(); // the Excel application.

                // the reference to the workbook,
                // which is the xls document to read from.
                Microsoft.Office.Interop.Excel.Workbook book = null;

                // the reference to the worksheet,
                // we'll assume the first sheet in the book.
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;
                Range range = null;
                // the range object is used to hold the data
                // we'll be reading from and to find the range of data.


                app.Visible = false;
                app.ScreenUpdating = false;
                app.DisplayAlerts = false;

                book = app.Workbooks.Open(strArquivoCompleto,
                       Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value);

                sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Worksheets["Frases"];

                range = sheet.get_Range("A1", Missing.Value);
                range = range.get_End(XlDirection.xlToRight);
                range = range.get_End(XlDirection.xlDown);

                string downAddress = range.get_Address(false, false, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);

                range = sheet.get_Range("A1", downAddress);

                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                string strDiciplina = "";
                string strAssunto = "";
                string strVerdade = "";
                string strFrase   = "";
                string strSimilar = "";

                //1 PASSO
                //Validação dos dados, tamanho, dicionario

                lstResultado.Add("INICIALIZANDO IMPORTAÇÃO DE FRASE");
                lstResultado.Add("TOTAL DE FRASE ENCONTRADA: " + (rowCount - 1).ToString());

                for (int iLinha = 2; iLinha <= rowCount; iLinha++)
                {
                    for (int iCollum = 1; iCollum <= colCount; iCollum++)
                    {
                        #region Switch
                        switch (iCollum)
                        {

                            case 1: //DISCIPLINA
                                strDiciplina = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strDiciplina = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value.ToString() ?? "";
                                }
                                else
                                {
                                    strDiciplina = "";
                                }

                                break;

                            case 2://ASSUNTO
                                strAssunto = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strAssunto = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value.ToString() ?? "";
                                }
                                else
                                {
                                    strAssunto = "";
                                }

                                break;

                            case 3://VERDADE
                                strVerdade = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strVerdade = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value.ToString() ?? "";
                                }
                                else
                                {
                                    strVerdade = "";
                                }

                                break;

                            case 4://FRASE
                                strFrase = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strFrase = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value.ToString() ?? "";
                                }
                                else
                                {
                                    strFrase = "";
                                }
                                break;

                            case 5://SIMILAR
                                strSimilar = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strSimilar = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value.ToString() ?? "";
                                }
                                else
                                {
                                    strSimilar = "";
                                }
                                break;

                            default:
                                break;
                        }
                        #endregion

                        AvisoDoProgresso(iCollum, colCount);
                    }

                    iQtdRecords = iQtdRecords + 1;

                    //Salva dados
                    if (salvaPrincipalDados(strDiciplina, strAssunto, strVerdade, strFrase, strSimilar, ref lstResultado) == false)
                    {
                        bStatus = false;
                    }

                }

                bStatus = true;

                //Fecha os objetos COM
                book.Close(false, null, null);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            //}
            //catch (Exception ex)
            //{
            //    lstResultado.Add(ex.Message);
            //    bStatus = false;
            //}

            //finally
            //{
                lstResultado.Add("FINALIZANDO IMPORTAÇÃO DE FRASE");
                lstResultado.Add(".");
                lstResultado.Add(".");
            //}

            return bStatus;
        }

        public bool importacaoExcelDisciplina(string strArquivoCompleto, ref List<string> lstResultado)
        {
            string strControleProcessamento = DateTime.Now.ToString();
            bool bStatus = false;
            int iQtdRecords = 0;

            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application(); // the Excel application.

                // the reference to the workbook,
                // which is the xls document to read from.
                Microsoft.Office.Interop.Excel.Workbook book = null;

                // the reference to the worksheet,
                // we'll assume the first sheet in the book.
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;
                Range range = null;
                // the range object is used to hold the data
                // we'll be reading from and to find the range of data.


                app.Visible = false;
                app.ScreenUpdating = false;
                app.DisplayAlerts = false;

                book = app.Workbooks.Open(strArquivoCompleto,
                       Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value);

                sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Worksheets["lista_de_disciplinas"];

                range = sheet.get_Range("A1", Missing.Value);
                range = range.get_End(XlDirection.xlToRight);
                range = range.get_End(XlDirection.xlDown);

                string downAddress = range.get_Address(false, false, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);

                range = sheet.get_Range("A1", downAddress);

                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                string strCodigo = "";
                string strDisciplina = "";

                //1 PASSO
                //Validação dos dados, tamanho, dicionario

                pnlManutencao.Visible = true;
                pBar.Value = 0;

                lstResultado.Add("INICIALIZANDO IMPORTAÇÃO DE DISCIPLINA");
                lstResultado.Add("TOTAL DE DISCIPLINA ENCONTRADO: " + (rowCount - 1).ToString());

                for (int iLinha = 2; iLinha <= rowCount; iLinha++)
                {
                    for (int iCollum = 1; iCollum <= colCount; iCollum++)
                    {
                        #region Switch
                        switch (iCollum)
                        {

                            case 1: //CÓDIGO
                                strCodigo = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strCodigo = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() ?? "";
                                }
                                else
                                {
                                    strCodigo = "";
                                }

                                break;

                            case 2://DISCIPLINA
                                strDisciplina = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strDisciplina = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() ?? "";
                                }
                                else
                                {
                                    strDisciplina = "";
                                }

                                break;
                            default:
                                break;
                        }
                        #endregion

                        AvisoDoProgresso(iCollum, colCount);
                    }

                    iQtdRecords = iQtdRecords + 1;

                    //Salva dados
                    if (salvaPrincipalDisciplina(strCodigo, strDisciplina, ref lstResultado) == false)
                    {
                        bStatus = false;
                    }
                }

                pBar.Value = 100;
                System.Threading.Thread.Sleep(10);
                pnlManutencao.Visible = false;

                bStatus = true;

                //Fecha os objetos COM
                book.Close(false, null, null);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            }
            catch (Exception ex)
            {
                lstResultado.Add(ex.Message);
                bStatus = false;
            }

            finally
            {
                lstResultado.Add("FINALIZANDO IMPORTAÇÃO DE DISCIPLINA");
                lstResultado.Add(".");
                lstResultado.Add(".");
            }

            return bStatus;
        }

        public bool importacaoExcelAssunto(string strArquivoCompleto, ref List<string> lstResultado)
        {
            string strControleProcessamento = DateTime.Now.ToString();
            bool bStatus = false;
            int iQtdRecords = 0;

            try
            {
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application(); // the Excel application.

                // the reference to the workbook,
                // which is the xls document to read from.
                Microsoft.Office.Interop.Excel.Workbook book = null;

                // the reference to the worksheet,
                // we'll assume the first sheet in the book.
                Microsoft.Office.Interop.Excel.Worksheet sheet = null;
                Range range = null;
                // the range object is used to hold the data
                // we'll be reading from and to find the range of data.


                app.Visible = false;
                app.ScreenUpdating = false;
                app.DisplayAlerts = false;

                book = app.Workbooks.Open(strArquivoCompleto,
                       Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                       Missing.Value, Missing.Value, Missing.Value);

                sheet = (Microsoft.Office.Interop.Excel.Worksheet)book.Worksheets["lista_de_assuntos"];

                range = sheet.get_Range("A1", Missing.Value);
                range = range.get_End(XlDirection.xlToRight);
                range = range.get_End(XlDirection.xlDown);

                string downAddress = range.get_Address(false, false, XlReferenceStyle.xlA1, Type.Missing, Type.Missing);

                range = sheet.get_Range("A1", downAddress);

                int rowCount = range.Rows.Count;
                int colCount = range.Columns.Count;

                string strDisciplina = "";
                string strAssunto = "";
                string strDescricao = "";

                //1 PASSO
                //Validação dos dados, tamanho, dicionario

                lstResultado.Add("INICIALIZANDO IMPORTAÇÃO DE ASSUNTO");
                lstResultado.Add("TOTAL DE ASSUNTO ENCONTRADO: " + (rowCount - 1).ToString());

                for (int iLinha = 2; iLinha <= rowCount; iLinha++)
                {
                    for (int iCollum = 1; iCollum <= colCount; iCollum++)
                    {
                        #region Switch
                        switch (iCollum)
                        {

                            case 1: //DISCIPLINA
                                strDisciplina = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strDisciplina = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() ?? "";
                                }
                                else
                                {
                                    strDisciplina = "";
                                }

                                break;

                            case 2://ASSUNTO
                                strAssunto = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strAssunto = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() ?? "";
                                }
                                else
                                {
                                    strAssunto = "";
                                }

                                break;

                            case 3://DESCRIÇÃO
                                strDescricao = "";
                                if ((range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2 != null)
                                {
                                    strDescricao = (string)(range.Cells[iLinha, iCollum] as Microsoft.Office.Interop.Excel.Range).Value2.ToString() ?? "";
                                }
                                else
                                {
                                    strDescricao = "";
                                }

                                break;
                            default:
                                break;
                        }
                        #endregion

                        AvisoDoProgresso(iCollum, colCount);
                    }

                    iQtdRecords = iQtdRecords + 1;

                    //Salva dados
                    if (salvaPrincipalAssunto(strDisciplina, strAssunto, strDescricao, ref lstResultado) == false)
                    {
                        bStatus = false;
                    }
                }

                bStatus = true;

                //Fecha os objetos COM
                book.Close(false, null, null);
                app.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            }
            catch (Exception ex)
            {
                lstResultado.Add(ex.Message);
                bStatus = false;
            }

            finally
            {
                lstResultado.Add("FINALIZANDO IMPORTAÇÃO DE ASSUNTO");
                lstResultado.Add(".");
                lstResultado.Add(".");
            }

            return bStatus;
        }





        public bool salvaPrincipalDados(string strDisciplina, string strAssunto, string strVerdade, string strFrase, string strSimilar, ref List<string> lstResultado)
        {
            string strErro1Banco = "";
            string strErro1Record = "";

            int registroExiste = 0;
            Int64 nRegistro = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

            modelDados oCollDados         = new modelDados();
            List<modelDados> lstCollDados = new List<modelDados>();

            modelDadosParametro oCollDadosParametro       = new modelDadosParametro();
            List<modelDadosParametro> lstDadosParametro   = new List<modelDadosParametro>();



            pnlManutencao.Visible = false;

            //***************************************************************************************************************************************************************************************************

            //try
            //{
                if (File.Exists(System.Windows.Forms.Application.StartupPath + @"\dados.mdb"))
                {

                    //=============================================================================================================================================================================
                    Banco_1 = dat1.conexaoBancoAccess(System.Windows.Forms.Application.StartupPath + @"\dados.mdb", "", "", ref strErro1Banco);


                    if (strErro1Banco.Length > 0)
                    {
                        lstResultado.Add(strErro1Banco);
                        return false;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }


                    var strSql = "";

                    //=============================================================================================================================================================================
                    oCollDados.Assunto = 0;

                    strSql = "SELECT * FROM ASSUNTO WHERE DESCRICAO = '" + strAssunto.Trim() + "'";

                    nRegistro = 0;

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1, ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro assunto: " + strErro1Record);
                        return false;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oCollDados.Assunto = Convert.ToInt32(record_1["id"].ToString());

                            lstResultado.Add("Parâmetro assunto localizado com sucesso: " + strAssunto + " , novo Código: " + oCollDados.Assunto.ToString());
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro assunto: " + strAssunto);
                    }

                    //=============================================================================================================================================================================


                    //=============================================================================================================================================================================
                    oCollDados.Disciplina = 0;

                    strSql = "SELECT * FROM DISCIPLINA WHERE DISCIPLINA = '" + strDisciplina.Trim() + "'";

                    nRegistro = 0;

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1, ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add("Erro em localizar parâmetro Disciplina: " + strErro1Record);
                        return false;
                    }

                    if (record_1.HasRows)
                    {
                        while (record_1.Read())
                        {
                            oCollDados.Disciplina = Convert.ToInt32(record_1["id"].ToString());

                            lstResultado.Add("Parâmetro Disciplina localizado com sucesso: " + strAssunto + " , novo Código: " + oCollDados.Assunto.ToString());
                        }
                    }
                    else
                    {
                        lstResultado.Add("Não foi possível localizar parâmetro Disciplina: " + strAssunto);
                    }

                    //=============================================================================================================================================================================

                    if (strVerdade.Contains("false"))
                    {
                        oCollDados.Verdade = 0;
                    }
                    else
                    {
                        oCollDados.Verdade = 1;
                    }


                    strErro1Record = "";

                    strSql = "SELECT * FROM DADOS WHERE FRASE = ? AND ASSUNTO = ? AND DISCIPLINA = ?";

                    nRegistro = 0;

                    record_1 = dat1.conexaoRecordAccessParametro(strSql, Banco_1, strFrase, oCollDados.Assunto, oCollDados.Disciplina, ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add(strErro1Record);
                        return false;
                    }
                    //=============================================================================================================================================================================



                    //=============================================================================================================================================================================
                    if (strErro1Banco.Length == 0 && strErro1Record.Length == 0)
                    {
                        if (record_1.HasRows)
                        {
                            while (record_1.Read())
                            {
                                registroExiste = registroExiste + 1;
                            }
                        }
                    }

                    if (registroExiste > 0)
                    {
                        lstResultado.Add("Essa FRASE já esta cadastrada, Descrição: " + strFrase + "  Assunto: " + strAssunto + " Disciplina: " + strDisciplina + " Similar: " + strSimilar);
                        return true;
                    }

                    //Fechar record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    if (strSimilar.Contains(" "))
                    {
                        oCollDados.Similar = 0;
                    }
                    else
                    {
                        oCollDados.Similar = Convert.ToInt32(strSimilar);
                    }

                    //=============================================================================================================================================================================

                    oCollDadosParametro.Campo     = "Disciplina";
                    oCollDadosParametro.TipoDados = OleDbType.Integer;
                    oCollDadosParametro.Valor     = 10;
                    lstDadosParametro.Add(oCollDadosParametro);
                    oCollDadosParametro = new modelDadosParametro();

                    oCollDadosParametro.Campo     = "Assunto";
                    oCollDadosParametro.TipoDados = OleDbType.Integer;
                    oCollDadosParametro.Valor     = 10;
                    lstDadosParametro.Add(oCollDadosParametro);
                    oCollDadosParametro = new modelDadosParametro();

                    oCollDadosParametro.Campo     = "Verdade";
                    oCollDadosParametro.TipoDados = OleDbType.Integer;
                    oCollDadosParametro.Valor     = 1;
                    lstDadosParametro.Add(oCollDadosParametro);
                    oCollDadosParametro = new modelDadosParametro();

                    oCollDadosParametro.Campo     = "Frase";
                    oCollDadosParametro.TipoDados = OleDbType.VarChar;
                    oCollDadosParametro.Valor     = 500;
                    lstDadosParametro.Add(oCollDadosParametro);
                    oCollDadosParametro = new modelDadosParametro();

                    oCollDadosParametro.Campo     = "similar";
                    oCollDadosParametro.TipoDados = OleDbType.Integer;
                    oCollDadosParametro.Valor     = 1;
                    lstDadosParametro.Add(oCollDadosParametro);
                    oCollDadosParametro = new modelDadosParametro();
                    
                    strSql = "INSERT INTO DADOS (Disciplina,Assunto,Verdade,Frase,similar) values ( ?, ?, ?, ?, ?) ";

                    oCollDados.Frase = strFrase;
                    
                    lstCollDados.Add(oCollDados);
                    
                    //=============================================================================================================================================================================

                    strErro1Record = "";

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Record, lstCollDados,lstDadosParametro) == false)
                    {
                        lstResultado.Add("Erro, em incluir FRASE: " + strFrase + "  Disciplina: " + strDisciplina + " , Assunto: " + strAssunto + " , Similar: " + strSimilar + "   ==> " + strErro1Record);
                    }
                    else
                    {
                        lstResultado.Add("Importação concluída com sucesso: FRASE: " + strFrase + "  Disciplina: " + strDisciplina + " , Assunto: " + strAssunto+ " , Similar: " + strSimilar);
                    }

                }
                //=============================================================================================================================================================================
            //}
            //catch (Exception ex)
            //{
            //    lstResultado.Add("Erro, em incluir FRASE: " + strFrase + "  Disciplina: " + strDisciplina + " , Assunto: " + strAssunto + "   ==> " + ex.Message);
            //}

            //finally
            //{

                //Fecha record.
                if (record_1 != null)
                {
                    record_1.Close();
                    record_1.Dispose();
                }

                //Fecha Banco de dados.
                if (Banco_1 != null) Banco_1.Close();
                dat1.fechaSql();

            //}

            //***************************************************************************************************************************************************************************************************

            return true;
        }

        public bool salvaPrincipalDisciplina(string strCodigo, string strDisciplina, ref List<string> lstResultado)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            int registroExiste = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

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
                        return false;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "SELECT * FROM DISCIPLINA WHERE CODIGO = " + strCodigo.Trim();

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1, ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add(strErro1Record);
                        return false;
                    }
                    //=============================================================================================================================================================================



                    //=============================================================================================================================================================================
                    if (strErro1Banco.Length == 0 && strErro1Record.Length == 0)
                    {
                        if (record_1.HasRows)
                        {
                            while (record_1.Read())
                            {
                                registroExiste = registroExiste + 1;
                            }
                        }
                    }

                    if (registroExiste > 0)
                    {
                        lstResultado.Add("Essa disciplina já esta cadastrada, Código: " + strCodigo + "  Disciplina: " + strDisciplina);
                        return true;
                    }

                    //Fechar record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }
                    //=============================================================================================================================================================================

                    strSql = "INSERT INTO DISCIPLINA (codigo,Disciplina) values ( ";
                    strSql = strSql + "" + strCodigo.Trim().ToUpper() + ",";
                    strSql = strSql + "'" + strDisciplina.Trim().ToUpper() + "')";

                    //=============================================================================================================================================================================

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Record) == false)
                    {
                        lstResultado.Add("Erro, em incluir disciplina, Código: " + strCodigo + "  Disciplina: " + strDisciplina + "   ==> " + strErro1Record);
                    }
                    else
                    {
                        lstResultado.Add("Importação concluída com sucesso: Disciplina, Código: " + strCodigo + "  Disciplina: " + strDisciplina + "   ==> " + strErro1Record);
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro, em incluir disciplina, Código: " + strCodigo + "  Disciplina: " + strDisciplina + "   ==> " + ex.Message);
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

            return true;
        }

        public bool salvaPrincipalAssunto(string strDisciplina, string strAssunto, string strDescricao, ref List<string> lstResultado)
        {
            string strErro1Banco = "";
            string strErro1Record = "";
            Int64 nRegistro = 0;

            int registroExiste = 0;

            OleDbDataReader record_1 = null;
            OleDbConnection Banco_1 = null;

            database.database dat1 = new database.database();

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
                        return false;
                    }

                    //Fecha record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }

                    var strSql = "SELECT * FROM ASSUNTO WHERE codigoDisciplina = " + strDisciplina.Trim() + " AND descricao = '" + strDescricao + "'";

                    record_1 = dat1.conexaoRecordAccess(strSql, Banco_1, ref nRegistro, ref strErro1Record);

                    if (strErro1Record.Length > 0)
                    {
                        lstResultado.Add(strErro1Record);
                        return false;
                    }
                    //=============================================================================================================================================================================



                    //=============================================================================================================================================================================
                    if (strErro1Banco.Length == 0 && strErro1Record.Length == 0)
                    {
                        if (record_1.HasRows)
                        {
                            while (record_1.Read())
                            {
                                registroExiste = registroExiste + 1;
                            }
                        }
                    }

                    if (registroExiste > 0)
                    {
                        lstResultado.Add("Esse assunto já esta cadastrada, Código Disciplina: " + strDisciplina + "  descrição: " + strDescricao);
                        return true;
                    }

                    //Fechar record.
                    if (record_1 != null)
                    {
                        record_1.Close();
                        record_1.Dispose();
                    }
                    //=============================================================================================================================================================================

                    strSql = "INSERT INTO ASSUNTO (codigoDisciplina,CodigoAssunto,descricao) values ( ";
                    strSql = strSql + ""  + strDisciplina.Trim().ToUpper() + ",";
                    strSql = strSql + ""  + strAssunto.Trim().ToUpper()    + ",";
                    strSql = strSql + "'" + strDescricao.Trim().ToUpper()  + "')";

                    //=============================================================================================================================================================================

                    if (dat1.conexaoExecutaAccess(strSql, Banco_1, ref strErro1Record) == false)
                    {
                        lstResultado.Add("Erro, em incluir disciplina, Código Disciplina: " + strDisciplina + "  Descrição: " + strDescricao + "   ==> " + strErro1Record);
                    }
                    else
                    {
                        lstResultado.Add("Importação concluída com sucesso: assunto, Código Disciplina: " + strDisciplina + "  Descrição: " + strDescricao + "   ==> " + strErro1Record);
                    }

                }
                //=============================================================================================================================================================================
            }
            catch (Exception ex)
            {
                lstResultado.Add("Erro, em incluir assunto, Código Disciplina: " + strDisciplina + "  Descrição: " + strDescricao + "   ==> " + ex.Message);
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

            return true;
        }



        private void btnFecharList_Click(object sender, EventArgs e)
        {
            lstImportacao.Visible = false;
            btnFecharList.Visible = false;
        }

        private void Consulta_Load(object sender, EventArgs e)
        {
            lstImportacao.Visible = false;
            btnFecharList.Visible = false;

            Novo();
        }

        private void Novo()
        {
            cboAssunto.Items.Clear();
            cboAssunto.SelectedValue = 0;
            cboDisciplina.Items.Clear();
            cboDisciplina.SelectedValue = 0;
            txtQuestao.Text = string.Empty;

            Disciplina();

            pnlManutencao.Visible = false;

            pBar.Value = 0;
        }

        private void Disciplina()
        {
            List<string> lstResultado = new List<string>();

            admDisciplina oAdmDisciplina = new admDisciplina();

            cboDisciplina.Items.Clear();

            List<modelDisciplina> oCool = oAdmDisciplina.admSelectRows(ref lstResultado, "", 0, 0);

            if (oCool == null)
            {
                return;
            }

            foreach (modelDisciplina oMc in oCool)
            {
                cboDisciplina.Items.Add(new MyComboBoxItem(oMc.Codigo.ToString(), oMc.Disciplina));
            }

            cboDisciplina.ValueMember = "Value";
            cboDisciplina.DisplayMember = "Text";
        }

        private void Assunto()
        {
            List<string> lstResultado = new List<string>();

            admAssunto oAdmAssunto = new admAssunto();
            admDisciplina oAdmDisciplina = new admDisciplina();

            cboAssunto.Items.Clear();

            int sCodigoDisciplina = Convert.ToInt32(((MyComboBoxItem)cboDisciplina.SelectedItem).Value);

            List<modelAssunto> oCool = oAdmAssunto.admSelectRows(ref lstResultado, 0, sCodigoDisciplina, 0, "");

            if (oCool == null)
            {
                return;
            }

            foreach (modelAssunto oMc in oCool)
            {
                cboAssunto.Items.Add(new MyComboBoxItem(oMc.Id.ToString(), oMc.Descricao));
            }

            cboAssunto.ValueMember = "Value";
            cboAssunto.DisplayMember = "Text";

        }

        private void cboDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAssunto.Text = string.Empty;
            Assunto();
        }

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            int nCodigoDisciplina = 0;
            int nCodigoAssunto = 0;

            List<string> lstResultado = new List<string>();

            admDados oAdmDados = new admDados();

            List<modelDadosDescritivo> oColl = new List<modelDadosDescritivo>();

            if (cboDisciplina.Text != string.Empty)
            {
                nCodigoDisciplina = Convert.ToInt32(((MyComboBoxItem)cboDisciplina.SelectedItem).Value);
            }

            if (cboAssunto.Text != string.Empty)
            {
                nCodigoAssunto = Convert.ToInt32(((MyComboBoxItem)cboAssunto.SelectedItem).Value);
            }

            pnlManutencao.Visible = true;
            pBar.Value = 0;
            oAdmDados.ProgressoMudou += new EventHandler<ProgressChangedEventArgs>(AvisoDoProgresso);

            oColl = oAdmDados.admSelectRowsDescritivo(ref lstResultado, 0, nCodigoDisciplina, nCodigoAssunto, -1, txtQuestao.Text);

            if (oColl != null)
            {
                gdvExcel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gdvExcel.DataSource = oColl;
                gdvExcel.Refresh();
                lblRegistro.Text = "Registro(s): " + (gdvExcel.RowCount).ToString();
            }
            else
            {
                gdvExcel.DataSource = null;
                gdvExcel.Refresh();
            }

            pBar.Value = 100;
            System.Threading.Thread.Sleep(10);
            pnlManutencao.Visible = false;
        }

        private void CarregaImportacao(int nultId)
        {
            List<string> lstResultado = new List<string>();

            admDados oAdmDados = new admDados();

            List<modelDadosDescritivo> oColl = new List<modelDadosDescritivo>();

            oColl = oAdmDados.admSelectRowsIdMaiorDescritivo(ref lstResultado, nultId);

            if (oColl != null)
            {
                gdvExcel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gdvExcel.DataSource = oColl;
                gdvExcel.Refresh();
                lblRegistro.Text = "Registro(s): " + (gdvExcel.RowCount).ToString();
            }
            else
            {
                gdvExcel.DataSource = null;
                gdvExcel.Refresh();
            }
        }

        public void AvisoDoProgresso(object sender, ProgressChangedEventArgs e)
        {
            //if (pBar.InvokeRequired)
            //{
            //    this.Invoke(new EventHandler<ProgressChangedEventArgs>(AvisoDoProgresso), sender, e);
                
            //    return;
            //}

            pBar.Value       = e.ProgressPercentage;
            lblRegistro.Text = e.ProgressPercentage.ToString();
        }

        public void AvisoDoProgresso(int nRegistro,int nRegistroTotal)
        {
            //if (pBar.InvokeRequired)
            //{
            //    this.Invoke(new EventHandler<ProgressChangedEventArgs>(AvisoDoProgresso), sender, e);

            //    return;
            //}

            pBar.Value       = nRegistro;
            lblRegistro.Text = nRegistroTotal.ToString();
            pBar.Maximum     = nRegistroTotal;
        }
    }
}
