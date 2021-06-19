using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Gerador_de_Questões_Alternativas
{
    class admExportarExcel
    {

        public void gerarExcelDllIntenrop(string strCaminho,int nOpen, int nqtdRegistro, string[] titulo
            , string[] campo1    
            , string[] campo2
            , string[] campo3
            , string[] campo4
            , string[] campo5
            , string[] campo6
            , string[] campo7
            , string[] campo8
            , string[] campo9
            , string[] campo10
            , string[] campo11
            , string[] campo12
            , string[] campo13
            , string[] campo14
            , string[] campo15
            , string[] campo16
            , string[] campo17
            , string[] campo18
            , string[] campo19
            , string[] campo20)
        {

            //---------------------------------------------------------------------------------------------------------------------------

            string strArquivoTime = DateTime.Now.ToString("MMddyyyyHHmmss");

            string sFilePath = "";

            try
            {

                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;

                object misValue = System.Reflection.Missing.Value;

                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                //=============================================================================================================================================================================

                #region excel
                //------------------------------------------------------------------------------------------------------

                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                if (titulo.Length >= 1)
                    xlWorkSheet.Cells[1, 1] = titulo[0];

                if (titulo.Length >= 2)
                    xlWorkSheet.Cells[1, 2] = titulo[1];

                if (titulo.Length >= 3)
                    xlWorkSheet.Cells[1, 3] = titulo[2];

                if (titulo.Length >= 4)
                    xlWorkSheet.Cells[1, 4] = titulo[3];

                if (titulo.Length >= 5)
                    xlWorkSheet.Cells[1, 5] = titulo[4];

                if (titulo.Length >= 6)
                    xlWorkSheet.Cells[1, 6] = titulo[5];

                if (titulo.Length >= 7)
                    xlWorkSheet.Cells[1, 7] = titulo[6];

                if (titulo.Length >= 8)
                    xlWorkSheet.Cells[1, 8] = titulo[7];

                if (titulo.Length >= 9)
                    xlWorkSheet.Cells[1, 9] = titulo[8];

                if (titulo.Length >= 10)
                    xlWorkSheet.Cells[1, 10] = titulo[9];

                if (titulo.Length >= 11)
                    xlWorkSheet.Cells[1, 11] = titulo[10];

                if (titulo.Length >= 12)
                    xlWorkSheet.Cells[1, 12] = titulo[11];

                if (titulo.Length >= 13)
                    xlWorkSheet.Cells[1, 13] = titulo[12];

                if (titulo.Length >= 14)
                    xlWorkSheet.Cells[1, 14] = titulo[13];

                if (titulo.Length >= 15)
                    xlWorkSheet.Cells[1, 15] = titulo[14];

                if (titulo.Length >= 16)
                    xlWorkSheet.Cells[1, 16] = titulo[15];

                if (titulo.Length >= 17)
                    xlWorkSheet.Cells[1, 17] = titulo[16];

                if (titulo.Length >= 18)
                    xlWorkSheet.Cells[1, 18] = titulo[17];

                if (titulo.Length >= 19)
                    xlWorkSheet.Cells[1, 19] = titulo[18];

                if (titulo.Length >= 20)
                    xlWorkSheet.Cells[1, 20] = titulo[19];

                //Interop params
                object oMissing = System.Reflection.Missing.Value;

                //------------------------------------------------------------------------------------------------------

                if (nqtdRegistro > 0)
                {
                    var i2 = 2;

                    while (i2 < (nqtdRegistro + 2))
                    {
                        //xlApp.Columns[1].AutoFit();
                        //xlApp.Columns[2].AutoFit();
                        //xlApp.Columns[3].AutoFit();
                        //xlApp.Columns[4].AutoFit();
                        //xlApp.Columns[5].AutoFit();
                        //xlApp.Columns[6].AutoFit();
                        //xlApp.Columns[7].AutoFit();
                        //xlApp.Columns[8].AutoFit();
                        //xlApp.Columns[9].AutoFit();
                        //xlApp.Columns[10].AutoFit();
                        //xlApp.Columns[11].AutoFit();
                        //xlApp.Columns[12].AutoFit();
                        //xlApp.Columns[13].AutoFit();
                        //xlApp.Columns[14].AutoFit();
                        //xlApp.Columns[15].AutoFit();
                        //xlApp.Columns[16].AutoFit();
                        //xlApp.Columns[17].AutoFit();
                        //xlApp.Columns[18].AutoFit();
                        //xlApp.Columns[19].AutoFit();
                        //xlApp.Columns[20].AutoFit();

                        if (campo1.Length > 0)
                            xlWorkSheet.Cells[i2, 1] = campo1[i2 - 2];

                        if (campo2.Length > 0)
                            xlWorkSheet.Cells[i2, 2] = campo2[i2 - 2];

                        if (campo3.Length > 0)
                            xlWorkSheet.Cells[i2, 3] = campo3[i2 - 2];

                        if (campo4.Length > 0)
                            xlWorkSheet.Cells[i2, 4] = campo4[i2 - 2];

                        if (campo5.Length > 0)
                            xlWorkSheet.Cells[i2, 5] = campo5[i2 - 2];

                        if (campo6.Length > 0)
                            xlWorkSheet.Cells[i2, 6] = campo6[i2 - 2];

                        if (campo7.Length > 0)
                            xlWorkSheet.Cells[i2, 7] = campo7[i2 - 2];

                        if (campo8.Length > 0)
                            xlWorkSheet.Cells[i2, 8] = campo8[i2 - 2];

                        if (campo9.Length > 0)
                            xlWorkSheet.Cells[i2, 9] = campo9[i2 - 2];

                        if (campo10.Length > 0)
                            xlWorkSheet.Cells[i2, 10] = campo10[i2 - 2];

                        if (campo11.Length > 0)
                            xlWorkSheet.Cells[i2, 11] = campo11[i2 - 2];

                        if (campo12.Length > 0)
                            xlWorkSheet.Cells[i2, 12] = campo12[i2 - 2];

                        if (campo13.Length > 0)
                            xlWorkSheet.Cells[i2, 13] = campo13[i2 - 2];

                        if (campo14.Length > 0)
                            xlWorkSheet.Cells[i2, 14] = campo14[i2 - 2];

                        if (campo15.Length > 0)
                            xlWorkSheet.Cells[i2, 15] = campo15[i2 - 2];

                        if (campo16.Length > 0)
                            xlWorkSheet.Cells[i2, 16] = campo16[i2 - 2];

                        if (campo17.Length > 0)
                            xlWorkSheet.Cells[i2, 17] = campo17[i2 - 2];

                        if (campo18.Length > 0)
                            xlWorkSheet.Cells[i2, 18] = campo18[i2 - 2];

                        if (campo19.Length > 0)
                            xlWorkSheet.Cells[i2, 19] = campo19[i2 - 2];

                        if (campo20.Length > 0)
                            xlWorkSheet.Cells[i2, 20] = campo20[i2 - 2];

                        i2++;
                    }

                    //xlWorkSheet.Rows.RowHeight = 20;
                    xlWorkSheet.Rows.AutoFit();

                    if (strCaminho.Length == 0)
                    {
                        sFilePath = AppDomain.CurrentDomain.BaseDirectory + @"PLANILHA-" + strArquivoTime + ".xls";
                    }
                    else
                    {
                        sFilePath = strCaminho;
                    }

                    xlWorkBook.SaveAs(sFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    liberarObjetos(xlWorkSheet);
                    liberarObjetos(xlWorkBook);
                    liberarObjetos(xlApp);
                }

                //***************************************************************************************************************************************
                #endregion

                if (nOpen == 1)
                {
                    System.Diagnostics.Process.Start(sFilePath);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void liberarObjetos(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show("Ocorreu um erro durante a liberação do objeto " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}
