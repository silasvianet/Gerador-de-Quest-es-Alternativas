using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gerador_de_Questões_Alternativas
{
    public partial class principal : Form
    {
        public principal()
        {
            InitializeComponent();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Novo();
        }

        private void Novo()
        {
            cboAssunto.Items.Clear();
            cboAssunto.Text = string.Empty;

            cboDisciplina.Items.Clear();
            cboDisciplina.Text          = string.Empty;

            numQuestao.Value            = 1;
            numQuestao.Enabled          = true;
            txtResultado.Text           = string.Empty;

            Disciplina();

            pnlPrincipal.Visible         = false;

            chkFraseRelacionada.Checked  = false;
            chkFormatoNovo.Checked       = false;
            chkGeracaoRadomica.Checked   = false;

            grBoxTipoQuestao.Enabled = false;

            numTpQuestaoAnalise3.Value  = 0;
            numTpQuestaoAnalise4.Value  = 0;
            numTpQuestaoCorreta.Value   = 0;
            numTpQuestaoIncorreta.Value = 0;


        }

        private void Disciplina()
        {
            List<string> lstResultado = new List<string>();

            admDisciplina oAdmDisciplina = new admDisciplina();

            cboDisciplina.Items.Clear();

            List<modelDisciplina> oCool = oAdmDisciplina.admSelectRows(ref lstResultado,"", 0 , 0);

            if (oCool == null)
            {
                return;
            }

            foreach (modelDisciplina oMc in oCool)
            {
                cboDisciplina.Items.Add(new MyComboBoxItem(oMc.Codigo.ToString(), oMc.Disciplina));
            }

            cboDisciplina.ValueMember   = "Value";
            cboDisciplina.DisplayMember = "Text";
        }

        private void Assunto() 
        {
            List<string> lstResultado = new List<string>();

            admAssunto oAdmAssunto       = new admAssunto();
            admDisciplina oAdmDisciplina = new admDisciplina();

            cboAssunto.Items.Clear();

            int sCodigoDisciplina =Convert.ToInt32(((MyComboBoxItem)cboDisciplina.SelectedItem).Value);

            List<modelAssunto> oCool = oAdmAssunto.admSelectRows(ref lstResultado, 0, sCodigoDisciplina, 0, "");

            if (oCool == null)
            {
                return;
            }

            foreach (modelAssunto oMc in oCool)
            {
                cboAssunto.Items.Add(new MyComboBoxItem(oMc.Id.ToString(), oMc.Descricao));
            }

            cboAssunto.ValueMember   = "Value";
            cboAssunto.DisplayMember = "Text";
        }
        
        private void btnManutencao_Click(object sender, EventArgs e)
        {
            Consulta Formulario = new Consulta();
            Formulario.ShowDialog();
        }





        private void btnAtualiza_Click(object sender, EventArgs e)
        {
            List<string> lstResultado = new List<string>();

            int nQuantidadeAssunto              = cboAssunto.Items.Count;
            int nQuantidadeQuestao              = 0; //Quantidade de tipo de questões.  
            int nQuantidadeQuestaoDisplay       = 0;

            if (chkGeracaoRadomica.Checked)
            {
                nQuantidadeQuestao = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            else
            {
                nQuantidadeQuestao              = Convert.ToInt32(numQuestao.Value);
            }

            Random rDisciplinaAlertorio  = new Random();
            Random rAssuntoAlertorio     = new Random();
            Random rTipoQuestaoAlertorio = new Random();

            Random rSorteiaQuestao = new Random();

            List<string> lstRespostaQuestao1 = new List<string>();
            List<string> lstRespostaQuestao2 = new List<string>();
            List<string> lstRespostaQuestao3 = new List<string>();
            List<string> lstRespostaQuestao4 = new List<string>();
            List<string> lstRespostaQuestao5 = new List<string>();

            List<string> lstGabarito = new List<string>();

            int nAssuntoEscolhido        = 0;
            int nTipoQuestaoEscolhida    = 0;            

            int nGuardaUltimoAssunto     = 0;
            int nGuardaUltimoTipoQuestao = 0;
            
            int nQuantQuestaoSelecionada = 0;
            int nCodigoDisciplina        = 0;
            int nContadorRegistro        = 0;


            int nContadorQuestaoCorreta       = Convert.ToInt32(numTpQuestaoCorreta.Value);
            int nContadorQuestaoIncorreta     = Convert.ToInt32(numTpQuestaoIncorreta.Value);
            int nContadorQuestaoAnalise3      = 0;
            int nContadorQuestaoAnalise4      = 0; 
            int nContadorQuestaoAnalise5      = Convert.ToInt32(numTpQuestaoAnalise4.Value);            

            admAssunto    oAdmAssunto    = new admAssunto();
            admDisciplina oAdmDisciplina = new admDisciplina();

            if (chkFormatoNovo.Checked)
            {
                nContadorQuestaoAnalise4 = Convert.ToInt32(numTpQuestaoAnalise3.Value);
            }
            else
            {
                nContadorQuestaoAnalise3 = Convert.ToInt32(numTpQuestaoAnalise3.Value);
            }

            #region BLOCO 0

            txtResultado.Text = string.Empty;

            if (cboDisciplina.Text == string.Empty)
            {
                MessageBox.Show("Selecione: Disciplina.");
                return;
            }

            if (nQuantidadeAssunto == 0)
            {
                MessageBox.Show("Não existe assunto, tente novamente.");
                return;
            }

            pnlPrincipal.Visible = true;

            #endregion
          
            //-----------------------------------------------------------------------------------------------------------------------
            pBar.Refresh();
            pBar.Value       = 0;
            pBar.Minimum     = 0;
            pBar.Maximum     = nQuantidadeQuestao;
            //-----------------------------------------------------------------------------------------------------------------------

            //-----------------------------------------------------------------------------------------------------------------------
            #region BLOCO 1 - GERAÇÃO RANDÔMICA DE DISCIPLINA

            if (cboDisciplina.Text != string.Empty)
            {
                nCodigoDisciplina = Convert.ToInt32(((MyComboBoxItem)cboDisciplina.SelectedItem).Value);
            }
            else
            { 
                //Será selecionado aleatóriamente.
                
                List<modelDisciplina> oCoolDisciplina = oAdmDisciplina.admSelectRows(ref lstResultado, "", 0, 0);

                nCodigoDisciplina = rDisciplinaAlertorio.Next(1, oCoolDisciplina.Count + 1);

                nCodigoDisciplina = oCoolDisciplina[nCodigoDisciplina].Codigo;
            }

            #endregion
            //-----------------------------------------------------------------------------------------------------------------------


            //-----------------------------------------------------------------------------------------------------------------------
            List<modelAssunto> oCool = oAdmAssunto.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, 0, "");

            if (chkGeracaoRadomica.Checked) //Vira quantidade de tipos de questões
            {
                nQuantQuestaoSelecionada  = nQuantidadeQuestao;
                nQuantidadeQuestao        = 1;
            }

            var i = 0;

            while (true)  
            {
                //-----------------------------------------------------------------------------------------------------------------------
                if (chkGeracaoRadomica.Checked == false) //Vira quantidade de tipos de questões
                {
                    nContadorQuestaoCorreta      = 1;
                    nContadorQuestaoIncorreta    = 1;
                    //
                    if (chkFormatoNovo.Checked)
                    {
                        nContadorQuestaoAnalise4 = 1;  
                    }
                    else
                    {
                        nContadorQuestaoAnalise3 = 1;
                    }
                    //
                    nContadorQuestaoAnalise5     = 1;
                    nQuantQuestaoSelecionada     = 1;
                }
                //-----------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------
                for (var z = 1; z <= nQuantQuestaoSelecionada; z++)
                {
                    //*****************************************************************************************************************************
                    #region BLOCO 2 GERAÇÃO RANDÔMICO DE ASSUNTO

                    nCodigoDisciplina = Convert.ToInt32(((MyComboBoxItem)cboDisciplina.SelectedItem).Value);

                    //Escolhe assunto aleatóriamente.
                    if (cboAssunto.Text == string.Empty)
                    {
                        nAssuntoEscolhido = rAssuntoAlertorio.Next(1, oCool.Count + 1);

                        //Evita escolha do mesmo assunto, quanto assunto somatória de assunto é maior que 1

                        if (oCool.Count > 1)
                        {
                            while (true)
                            {
                                nAssuntoEscolhido = rAssuntoAlertorio.Next(1, oCool.Count + 1);

                                if (nAssuntoEscolhido != nGuardaUltimoAssunto)
                                {
                                    break;
                                }
                            }
                        }

                        //**********************************************************************************************************************************
                        //Retorna id do assunto escolhido, conforme disciplina selecionada.
                        List<modelAssunto> oCoolAssunto = oAdmAssunto.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nAssuntoEscolhido, "");

                        foreach (modelAssunto oMc in oCoolAssunto)
                        {
                            nAssuntoEscolhido = oMc.Id;
                        }
                        //**********************************************************************************************************************************


                    }
                    else
                    {
                        //Obtem id do assunto escolhido
                        nAssuntoEscolhido = Convert.ToInt32(((MyComboBoxItem)cboAssunto.SelectedItem).Value);
                    }
                    #endregion
                    //*****************************************************************************************************************************


                    //*****************************************************************************************************************************
                    #region BLOCO 3 GERAÇÃO RANDÔMICO DE TIPO DE QUESTÃO
                    //*****************************************************************************************************************************
                    nTipoQuestaoEscolhida = 0;
                    while (true)
                    {
                        nTipoQuestaoEscolhida = rTipoQuestaoAlertorio.Next(1, 6); //Existe três tipos de questão que pode ser retornada

                        if (nTipoQuestaoEscolhida != nGuardaUltimoTipoQuestao)
                        {
                            break;
                        }
                    }
                    //*****************************************************************************************************************************
                    #endregion
                    //*****************************************************************************************************************************


                    //*****************************************************************************************************************************
                    //Converte Disciplina código para id.
                    List<modelDisciplina> oCoolDisciplina2 = oAdmDisciplina.admSelectRows(ref lstResultado, "", nCodigoDisciplina, 0);

                    foreach (modelDisciplina oMc in oCoolDisciplina2)
                    {
                        nCodigoDisciplina = oMc.Id;
                    }
                    //*****************************************************************************************************************************

                    //Questão 1: assinale alternativa correta
                    //Questão 2: assinale alternativa incorreta
                    //Questão 3: assinale as assertativas abaixo
                    //Questão 4: assinale as assertativas abaixo Novo formato com 3 questões
                    //Questão 5: assinale as assertativas abaixo Novo formato com 4 questões

                    #region Tipo de questão
                    switch (nTipoQuestaoEscolhida)
                    {
                        case 1:

                            if (nContadorQuestaoCorreta >= 1)
                            {
                                Application.DoEvents();
                                List<string> lstResultadoQuestoes1 = GeraQuestao1(ref lstRespostaQuestao1, nCodigoDisciplina, nAssuntoEscolhido);

                                nQuantidadeQuestaoDisplay++;
                                txtResultado.Text = txtResultado.Text + "(" + nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + ") " + "Assinale a alternativa correta." + Environment.NewLine;

                                if (lstResultadoQuestoes1 != null)
                                {
                                    if (lstResultadoQuestoes1.Count == 6)
                                    {
                                        for (var ix = 0; ix < 5; ix++)
                                        {
                                            txtResultado.Text = txtResultado.Text + lstResultadoQuestoes1[ix] + Environment.NewLine;
                                        }

                                        lstGabarito.Add(nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + " - " + lstResultadoQuestoes1[5].Replace("(", "").Replace(")", ""));

                                        txtResultado.Text = txtResultado.Text + Environment.NewLine;
                                    }
                                }


                                nContadorRegistro++;
                                AvisoDoProgresso(nContadorRegistro, pBar.Maximum);

                                nContadorQuestaoCorreta--;
                            }
                            break;

                        case 2:

                            if (nContadorQuestaoIncorreta >= 1)
                            {
                                Application.DoEvents();
                                List<string> lstResultadoQuestoes2 = GeraQuestao2(ref lstRespostaQuestao2, nCodigoDisciplina, nAssuntoEscolhido);

                                nQuantidadeQuestaoDisplay++;
                                txtResultado.Text = txtResultado.Text + "(" + nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + ") " + "Assinale a alternativa INCORRETA." + Environment.NewLine;


                                if (lstResultadoQuestoes2 != null)
                                {
                                    if (lstResultadoQuestoes2.Count == 6)
                                    {
                                        for (var ix = 0; ix < 5; ix++)
                                        {
                                            txtResultado.Text = txtResultado.Text + lstResultadoQuestoes2[ix] + Environment.NewLine;
                                        }

                                        lstGabarito.Add(nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + " - " + lstResultadoQuestoes2[5].Replace("(", "").Replace(")", ""));

                                        txtResultado.Text = txtResultado.Text + Environment.NewLine;
                                    }
                                }

                                nContadorRegistro++;
                                AvisoDoProgresso(nContadorRegistro, pBar.Maximum);
                                nContadorQuestaoIncorreta--;
                            }
                            break;

                        case 3:


                            if (nContadorQuestaoAnalise3 >= 1)
                            {
                                List<string> lstResultadoQuestoes3 = null;

                                Application.DoEvents();

                                lstResultadoQuestoes3 = GeraQuestao3(ref lstRespostaQuestao3, nCodigoDisciplina, nAssuntoEscolhido);

                                nQuantidadeQuestaoDisplay++;
                                txtResultado.Text = txtResultado.Text + "(" + nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + ") " + "Analise as assertivas abaixo." + Environment.NewLine;

                                if (lstResultadoQuestoes3 != null)
                                {
                                    if (lstResultadoQuestoes3.Count == 10)
                                    {
                                        for (var ix = 0; ix < 9; ix++)
                                        {
                                            txtResultado.Text = txtResultado.Text + lstResultadoQuestoes3[ix] + Environment.NewLine;
                                        }

                                        lstGabarito.Add(nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + " - " + lstResultadoQuestoes3[9].Replace("(", "").Replace(")", ""));

                                        txtResultado.Text = txtResultado.Text + Environment.NewLine;
                                    }
                                }

                                nContadorRegistro++;
                                AvisoDoProgresso(nContadorRegistro, pBar.Maximum);
                                nContadorQuestaoAnalise3--;
                            }
                            break;

                        case 4:  //Novo formato de analise 3 questões
                            

                            if (nContadorQuestaoAnalise4 >= 1)
                            {
                                Application.DoEvents();

                                List<string> lstResultadoQuestoes4 = GeraQuestao4(ref lstRespostaQuestao3, nCodigoDisciplina, nAssuntoEscolhido);

                                nQuantidadeQuestaoDisplay++;
                                txtResultado.Text = txtResultado.Text + "(" + nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + ") " + "Analise as assertivas abaixo." + Environment.NewLine;

                                if (lstResultadoQuestoes4 != null)
                                {
                                    if (lstResultadoQuestoes4.Count == 9)
                                    {
                                        for (var ix = 0; ix < 8; ix++)
                                        {
                                            txtResultado.Text = txtResultado.Text + lstResultadoQuestoes4[ix] + Environment.NewLine;
                                        }

                                        lstGabarito.Add(nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + " - " + lstResultadoQuestoes4[8].Replace("(", "").Replace(")", ""));

                                        txtResultado.Text = txtResultado.Text + Environment.NewLine;
                                    }
                                }

                                nContadorRegistro++;
                                AvisoDoProgresso(nContadorRegistro, pBar.Maximum);
                                nContadorQuestaoAnalise4--;
                            }
                            break;

                        case 5:  //Novo formato de analise 4 questões
                            
                            if (nContadorQuestaoAnalise5 >= 1)
                            {
                                Application.DoEvents();

                                List<string> lstResultadoQuestoes5 = GeraQuestao5(ref lstRespostaQuestao5, nCodigoDisciplina, nAssuntoEscolhido);

                                nQuantidadeQuestaoDisplay++;
                                txtResultado.Text = txtResultado.Text + "(" + nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + ") " + "Analise as assertivas abaixo." + Environment.NewLine;

                                if (lstResultadoQuestoes5 != null)
                                {
                                    if (lstResultadoQuestoes5.Count == 10)
                                    {
                                        for (var ix = 0; ix < 9; ix++)
                                        {
                                            txtResultado.Text = txtResultado.Text + lstResultadoQuestoes5[ix] + Environment.NewLine;
                                        }

                                        lstGabarito.Add(nQuantidadeQuestaoDisplay.ToString().PadLeft(3, '0') + " - " + lstResultadoQuestoes5[9].Replace("(", "").Replace(")", ""));

                                        txtResultado.Text = txtResultado.Text + Environment.NewLine;
                                    }
                                }

                                nContadorRegistro++;
                                AvisoDoProgresso(nContadorRegistro, pBar.Maximum);
                                nContadorQuestaoAnalise5--;
                            }
                            break;

                        default:
                            break;
                    }
                    #endregion

                    nGuardaUltimoAssunto     = nAssuntoEscolhido;
                    nGuardaUltimoTipoQuestao = nTipoQuestaoEscolhida;
                }
                //-----------------------------------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------------------------------
                if (chkGeracaoRadomica.Checked) //Vira quantidade de tipos de questões
                {
                    if ((nContadorQuestaoCorreta + nContadorQuestaoIncorreta + nContadorQuestaoAnalise3 + nContadorQuestaoAnalise4 + nContadorQuestaoAnalise5) == 0)
                    {
                        break;
                    }
                }
                else
                {
                    i++;

                    if (i == nQuantidadeQuestao)
                    { 
                        break;
                    }

                }
                //-----------------------------------------------------------------------------------------------------------------------


            }
            //-----------------------------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------------------------
            txtResultado.Text = txtResultado.Text + Environment.NewLine + "GABARITO:" + Environment.NewLine + Environment.NewLine;

            foreach (string omc in lstGabarito)
            {
                txtResultado.Text = txtResultado.Text + omc + Environment.NewLine;
            }

            System.Threading.Thread.Sleep(5);
            pnlPrincipal.Visible = false;
            
            MessageBox.Show("Consulta Concluída, segue resultado.");
            //-----------------------------------------------------------------------------------------------------------------------

        }

        private List<string> GeraQuestao1(ref List<string> lstRespostaQuestao1, int nCodigoDisciplina, int nAssuntoEscolhido)
        {
            List<string> lstResultadoQuestoes           = new List<string>();
            List<string> lstResultadoQuestoesFrase      = new List<string>();
            List<string> lstResultadoQuestoesFraseOrdem = new List<string>();
            List<string> lstSimilaridade                = new List<string>();

            string strResposta = "";

            List<string> lstResultado = new List<string>();

            int nIdAssuntoEscolhido = nAssuntoEscolhido;

            int nIndex = 0;

            Random rQuestaoVerdadeira = new Random();
            Random rQuestaoFalsa      = new Random();
            Random rQuestao           = new Random();
             
            int nQuestaoVerdadeiraEscolhida = 0;
            int nQuestaoFalsaEscolhida      = 0;
            
            List<string> lstRespostaFalsa = new List<string>();
                       

            admDados oAdmDados = new admDados();
            
            admAssunto oAdmAssunto = new admAssunto();


            //********************************************************************************************************************************************
            //Retorne questões verdadeiras, que irei fazer um sorteio, e fazer escolha de uma difeferente da anterior.
            List<modelDados> oCollDadosVerdadeiro = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 1, "", 1,0);

            if (oCollDadosVerdadeiro == null)
            {
                return null;
            }
            else
            {
                if (oCollDadosVerdadeiro.Count == 0)
                {
                    return null;
                }
            }

            nIndex = 0;
    
            if (oCollDadosVerdadeiro.Count > 1)
            {
                var nTentativas = 0;

                while (true)
                {
                    nIndex = 0;

                    nTentativas++;

                    if (chkFraseRelacionada.Checked || nTentativas > 10)
                    {
                        lstSimilaridade.Clear();
                    }

                    nIndex                      = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                    nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;
                                        
                    var sSimilar                = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                    if (!lstRespostaQuestao1.Contains(nQuestaoVerdadeiraEscolhida.ToString()))
                    {
                        lstRespostaQuestao1.Add(nQuestaoVerdadeiraEscolhida.ToString());

                        lstSimilaridade.Add(sSimilar);

                        while (true)
                        {
                            var n = rQuestaoFalsa.Next(0, 5);

                            var re = Alfabeto(n);

                            if (!lstResultadoQuestoes.Contains(re))
                            {
                                lstResultadoQuestoes.Add(re);
                                lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);                                
                                strResposta = re;
                                break;
                            }
                        }

                        break;
                    }

                    //Tentativas de escolher questão diferente, da já escolhida.
                    if ((nTentativas > oCollDadosVerdadeiro.Count) && (lstRespostaQuestao1.Contains(nQuestaoVerdadeiraEscolhida.ToString())))
                    {
                        lstRespostaQuestao1.Add(nQuestaoVerdadeiraEscolhida.ToString());

                        lstSimilaridade.Add(sSimilar);

                        while (true)
                        {
                            var n = rQuestaoFalsa.Next(0, 5);

                            var re = Alfabeto(n);

                            if (!lstResultadoQuestoes.Contains(re))
                            {
                                lstResultadoQuestoes.Add(re);
                                lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);

                                strResposta = re;
                                break;
                            }
                        }

                        break;
                    }
                }

            }
            else
            {
                nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                lstRespostaQuestao1.Add(nQuestaoVerdadeiraEscolhida.ToString());

                lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                while (true)
                {
                    var n = rQuestaoFalsa.Next(0, 5);

                    var re = Alfabeto(n);

                    if (!lstResultadoQuestoes.Contains(re))
                    {
                        lstResultadoQuestoes.Add(re);
                        lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);

                        strResposta = re;
                        break;
                    }
                }

            }
            //********************************************************************************************************************************************


            //Retorne questões falsas, que irei fazer um sorteio, e fazer escolhas de  quatro opções.
            List<modelDados> oCollDadosFalse = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 0, "", 1, 0);

            //********************************************************************************************************************************************

            for (var q = 1; q <= 4; q++)
            {
                if (oCollDadosFalse.Count > 1)
                {
                    var nTentativas = 0;

                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                        nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        var sSimilar = oCollDadosFalse[nIndex - 1].Similar.ToString();

                        if (!lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {                            
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 5);

                                var re = Alfabeto(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosFalse.Count) && (lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString())))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 5);

                                var re = Alfabeto(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }                    
                }
                else
                {
                    nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                    nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                   lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                   lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                   while (true)
                   {
                       var n = rQuestaoFalsa.Next(0, 5);

                       var re = Alfabeto(n);

                       if (!lstResultadoQuestoes.Contains(re))
                       {
                           lstResultadoQuestoes.Add(re);
                           lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                           break;
                       }
                   }
                }
            }
            //********************************************************************************************************************************************


            //********************************************************************************************************************************************
            lstResultadoQuestoesFrase.Add(strResposta);          
            //********************************************************************************************************************************************

            for (var o = 0; o < 5; o++)
            {
                for (var t = 0; t < 5; t++)
                {
                    if (lstResultadoQuestoes[t] == Alfabeto(o))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFrase[t]);
                    }
                }
            }

            lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFrase[5]);

            return lstResultadoQuestoesFraseOrdem;
        }

        private List<string> GeraQuestao2(ref List<string> lstRespostaQuestao2, int nCodigoDisciplina, int nAssuntoEscolhido)
        {
            List<string> lstResultadoQuestoes = new List<string>();
            List<string> lstResultadoQuestoesFrase = new List<string>();
            List<string> lstResultadoQuestoesFraseOrdem = new List<string>();
            List<string> lstSimilaridade = new List<string>();

            string strResposta = "";

            List<string> lstResultado = new List<string>();

            int nIdAssuntoEscolhido = nAssuntoEscolhido;

            int nIndex = 0;

            Random rQuestaoVerdadeira = new Random();
            Random rQuestaoFalsa = new Random();
            Random rQuestao = new Random();

            int nQuestaoVerdadeiraEscolhida = 0;
            int nQuestaoFalsaEscolhida = 0;

            List<string> lstRespostaFalsa = new List<string>();

            admDados oAdmDados = new admDados();

            admAssunto oAdmAssunto = new admAssunto();


            //********************************************************************************************************************************************
            //Retorne questões falsas, que irei fazer um sorteio, e fazer escolha de uma difeferente da anterior.
            List<modelDados> oCollDadosVerdadeiro = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 0, "", 1, 0);

            if (oCollDadosVerdadeiro == null)
            {
                return null;
            }
            else
            {
                if (oCollDadosVerdadeiro.Count == 0)
                {
                    return null;
                }
            }

            nIndex = 0;

            if (oCollDadosVerdadeiro.Count > 1)
            {
                var nTentativas = 0;

                while (true)
                {
                    nIndex = 0;

                    nTentativas++;

                    if (chkFraseRelacionada.Checked || nTentativas > 10)
                    {
                        lstSimilaridade.Clear();
                    }

                    nIndex                      = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                    nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                    var sSimilar                = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                    if (!lstRespostaQuestao2.Contains(nQuestaoVerdadeiraEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                    {
                        lstRespostaQuestao2.Add(nQuestaoVerdadeiraEscolhida.ToString());

                        lstSimilaridade.Add(sSimilar);

                        while (true)
                        {
                            var n = rQuestaoFalsa.Next(0, 5);

                            var re = Alfabeto(n);

                            if (!lstResultadoQuestoes.Contains(re))
                            {
                                lstResultadoQuestoes.Add(re);
                                lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);

                                strResposta = re;
                                break;
                            }
                        }

                        break;
                    }

                    //Tentativas de escolher questão diferente, da já escolhida.
                    if ((nTentativas > oCollDadosVerdadeiro.Count) && (lstRespostaQuestao2.Contains(nQuestaoVerdadeiraEscolhida.ToString())))
                    {
                        lstRespostaQuestao2.Add(nQuestaoVerdadeiraEscolhida.ToString());

                        lstSimilaridade.Add(sSimilar);

                        while (true)
                        {
                            var n = rQuestaoFalsa.Next(0, 5);

                            var re = Alfabeto(n);

                            if (!lstResultadoQuestoes.Contains(re))
                            {
                                lstResultadoQuestoes.Add(re);
                                lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);

                                strResposta = re;
                                break;
                            }
                        }

                        break;
                    }
                }
            }
            else
            {
                nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                lstRespostaQuestao2.Add(nQuestaoVerdadeiraEscolhida.ToString());

                lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                while (true)
                {
                    var n = rQuestaoFalsa.Next(0, 5);

                    var re = Alfabeto(n);

                    if (!lstResultadoQuestoes.Contains(re))
                    {
                        lstResultadoQuestoes.Add(re);
                        lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);

                        strResposta = re;
                        break;
                    }
                }

            }
            //********************************************************************************************************************************************


            //Retorne questões verdadeiras, que irei fazer um sorteio, e fazer escolhas de  quatro opções.
            List<modelDados> oCollDadosFalse = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 1, "", 1, 0);

            //********************************************************************************************************************************************

            for (var q = 1; q <= 4; q++)
            {
                if (oCollDadosFalse.Count > 1)
                {
                    var nTentativas = 0;

                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                        nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosFalse[nIndex - 1].Similar.ToString();

                        if (!lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 5);

                                var re = Alfabeto(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosFalse.Count) && (lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString())))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 5);

                                var re = Alfabeto(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                }
                else
                {
                    nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                    nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                    lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 5);

                        var re = Alfabeto(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }
            //********************************************************************************************************************************************


            //********************************************************************************************************************************************
            lstResultadoQuestoesFrase.Add(strResposta);
            //********************************************************************************************************************************************

            for (var o = 0; o < 5; o++)
            {
                for (var t = 0; t < 5; t++)
                {
                    if (lstResultadoQuestoes[t] == Alfabeto(o))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFrase[t]);
                    }
                }
            }

            lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFrase[5]);

            return lstResultadoQuestoesFraseOrdem;
        }

        private List<string> GeraQuestao3(ref List<string> lstRespostaQuestao3, int nCodigoDisciplina, int nAssuntoEscolhido)
        {
            List<string> lstResultadoQuestoes = new List<string>();
            List<string> lstResultadoQuestoesFrase = new List<string>();
            List<string> lstResultadoQuestoesFraseOrdem = new List<string>();

            List<string> lstSimilaridade = new List<string>();

            List<string> lstResultado = new List<string>();

            int nQuantRespostaCorreta = 0;
            int nQuantRespostaInCorreta = 0;

            int nIdAssuntoEscolhido = nAssuntoEscolhido;

            int nIndex = 0;

            Random rQuestaoVerdadeira = new Random();
            Random rQuestaoFalsa = new Random();
            Random rQuestao = new Random();

            int nQuestaoVerdadeiraEscolhida = 0;
            int nQuestaoFalsaEscolhida = 0;

            List<string> lstRespostaFalsa = new List<string>();

            admDados oAdmDados = new admDados();

            admAssunto oAdmAssunto = new admAssunto();

            string sQuestaoVerdadeiraApresentada = "";

            //********************************************************************************************************************************************

            int qtdQuestao = rQuestaoVerdadeira.Next(0, 5);

            nQuantRespostaCorreta   = 4 - qtdQuestao;

            nQuantRespostaInCorreta = 4 - nQuantRespostaCorreta;


            //********************************************************************************************************************************************
            //Retorne questões falsas, que irei fazer um sorteio, e fazer escolha de uma difeferente da anterior.
            List<modelDados> oCollDadosVerdadeiro = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 0, "", 1, 0);

            if (oCollDadosVerdadeiro == null)
            {
                return null;
            }
            else
            {
                if (oCollDadosVerdadeiro.Count == 0)
                {
                    return null;
                }
            }

            nIndex = 0;

            for (var q = 1; q <= nQuantRespostaInCorreta; q++)
            {
                if (oCollDadosVerdadeiro.Count > 1)
                {
                    var nTentativas = 0;

                    #region BLOCO QUESTÃO FALSA
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                        nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaQuestao3.Contains(nQuestaoVerdadeiraEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaQuestao3.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosVerdadeiro.Count) && (lstRespostaQuestao3.Contains(nQuestaoVerdadeiraEscolhida.ToString())))
                        {
                            lstRespostaQuestao3.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                    nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                    lstRespostaQuestao3.Add(nQuestaoVerdadeiraEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 4);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFrase.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }
            //********************************************************************************************************************************************

            //Variavel está como nome de falta porém é verdadeira as questões que  serão escolhidas
            //Retorne questões verdadeiras, que irei fazer um sorteio, e fazer escolhas de  quatro opções.
            List<modelDados> oCollDadosFalse = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 1, "", 1, 0);

            //********************************************************************************************************************************************

            sQuestaoVerdadeiraApresentada = string.Empty;

            for (var q = 1; q <= nQuantRespostaCorreta; q++)
            {
                if (oCollDadosFalse.Count > 1)
                {
                    var nTentativas = 0;

                    if (chkFraseRelacionada.Checked || nTentativas > 10)
                    {
                        lstSimilaridade.Clear();
                    }

                    #region BLOCO QUESTÃO verdadeira
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;
                        
                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                        nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    sQuestaoVerdadeiraApresentada += re + " ";
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosFalse.Count) && (lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString())))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                    nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                    lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 4);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            sQuestaoVerdadeiraApresentada += re + " ";
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFrase.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                            break;
                        }
                    }


                }
            }

            for (var o = 0; o < 4; o++)
            {
                for (var t = 0; t < 4; t++)
                {
                    if (lstResultadoQuestoes[t] == AlfabetoRomano(o))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFrase[t]);
                    }
                }
            }

            lstResultadoQuestoesFraseOrdem.Add("(A) Nenhuma está correta.");
            lstResultadoQuestoesFraseOrdem.Add("(B) Apenas uma está correta.");
            lstResultadoQuestoesFraseOrdem.Add("(C) Apenas duas estão corretas.");
            lstResultadoQuestoesFraseOrdem.Add("(D) Apenas três estão corretas.");
            lstResultadoQuestoesFraseOrdem.Add("(E) Todas estão corretas.");

            lstResultadoQuestoesFraseOrdem.Add(AlfabetoGabarito(nQuantRespostaCorreta) + " (" + sQuestaoVerdadeiraApresentada + ")");

            return lstResultadoQuestoesFraseOrdem;
        }

        //Novo formato de analise com 3 questões
        private List<string> GeraQuestao4(ref List<string> lstRespostaQuestao4, int nCodigoDisciplina, int nAssuntoEscolhido)
        {
            List<string> lstResultadoQuestoes = new List<string>();
            List<string> lstResultadoQuestoesFinal = new List<string>();
            List<string> lstResultadoQuestoesFraseOrdem = new List<string>();
            List<string> lstResultadoQuestoesInseridas = new List<string>();

            List<string> lstSimilaridade = new List<string>();

            List<string> lstGabarito = new List<string>();
                        
            List<string> lstResultado = new List<string>();

            int nQuantRespostaCorreta = 0;
            int nQuantRespostaInCorreta = 0;

            int nIdAssuntoEscolhido = nAssuntoEscolhido;

            int nIndex = 0;

            Random rQuestaoVerdadeira = new Random();
            Random rQuestaoFalsa = new Random();
            Random rQuestao = new Random();

            int nQuestaoVerdadeiraEscolhida = 0;
            int nQuestaoFalsaEscolhida = 0;

            List<string> lstRespostaFalsa = new List<string>();

            List<string> lstQuestaoVerdadeira = new List<string>();
            List<string> lstQuestaoFalsa = new List<string>();

            string sResposta = string.Empty;

            admDados oAdmDados = new admDados();

            admAssunto oAdmAssunto = new admAssunto();

            //********************************************************************************************************************************************

            int qtdQuestao = rQuestaoVerdadeira.Next(0, 4);

            nQuantRespostaCorreta   = 3 - qtdQuestao;

            nQuantRespostaInCorreta = 3 - nQuantRespostaCorreta;


            int nRespostaEscolhida = 0;

            //********************************************************************************************************************************************
            //Retorne questões falsas, que irei fazer um sorteio, e fazer escolha de uma difeferente da anterior.
            List<modelDados> oCollDadosVerdadeiro = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 0, "", 1, 0);

            if (oCollDadosVerdadeiro == null)
            {
                return null;
            }
            else
            {
                if (oCollDadosVerdadeiro.Count == 0)
                {
                    return null;
                }
            }

            nIndex = 0;

            for (var q = 1; q <= nQuantRespostaInCorreta; q++)
            {
                if (oCollDadosVerdadeiro.Count > 1)
                {

                    var nTentativas = 0;

                    #region QUESTÃO FALSA
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                        nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;
                        
                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaQuestao4.Contains(nQuestaoVerdadeiraEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaQuestao4.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 3);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoFalsa.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosVerdadeiro.Count) && (lstRespostaQuestao4.Contains(nQuestaoVerdadeiraEscolhida.ToString())))
                        {
                            lstRespostaQuestao4.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 3);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoFalsa.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                    nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                    lstRespostaQuestao4.Add(nQuestaoVerdadeiraEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 3);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstQuestaoFalsa.Add(re);
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }
            //********************************************************************************************************************************************
            //********************************************************************************************************************************************
            //Variavel está como nome de falsa porém é verdadeira as questões que  serão escolhidas
            //Retorne questões verdadeiras, que irei fazer um sorteio, e fazer escolhas de  quatro opções.
            List<modelDados> oCollDadosFalse = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 1, "", 1, 0);
            //********************************************************************************************************************************************

            for (var q = 1; q <= nQuantRespostaCorreta; q++)
            {
                if (oCollDadosFalse.Count > 1)
                {
                    var nTentativas = 0;

                    #region QUESTÃO VERDADEIRA
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                        nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 3);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoVerdadeira.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosFalse.Count) && (lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString())))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 3);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoVerdadeira.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                    nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                    lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 3);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstQuestaoVerdadeira.Add(re);
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }










            //MONTAGEM DAS PERGUNTAS, CONFORME ALTERNATIVA CORRETA OU INCORRETA.
            //********************************************************************************************************************************************
            //Seleção de 5 possíveis alternativas de 8


            lstResultadoQuestoesFraseOrdem.Clear();
            lstResultadoQuestoesInseridas.Clear();

            //********************************************************************************************************************************************
            for (var o = 0; o < 3; o++)
            {
                for (var t = 0; t < 3; t++)
                {
                    if (lstResultadoQuestoes[t] == AlfabetoRomano(o))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFinal[t]);
                    }
                }
            }
            //********************************************************************************************************************************************

            //********************************************************************************************************************************************

            nRespostaEscolhida = 0;

            while (true)
            {
                var n = 0;

                #region ESCOLHE NENHUMA ESTÁ CORRETA

                if (lstQuestaoFalsa.Count == 3)
                {
                    if (!lstResultadoQuestoesInseridas.Contains("Nenhuma está correta."))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Nenhuma está correta.");
                        lstResultadoQuestoesInseridas.Add("Nenhuma está correta.");

                        sResposta = "A";

                        nRespostaEscolhida++;
                    }
                }

                #endregion

                n = rQuestao.Next(0, 2); //Verifica-se vai escolher pergunta individual ou grupo.

                #region QUESTÕES EM GRUPO
                if (n == 0) //grupo
                {
                    n = rQuestao.Next(0, 2); //Verifica-se vai escolher pergunta verdadeira ou falsa.

                    #region VERDADEIRA
                    if (n == 0 && lstQuestaoVerdadeira.Count == 2) //Monta grupo de questão verdadeira,contendo 2 questão verdadeira.
                    {
                        if (sResposta == string.Empty)
                        {
                            var sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region FALSA
                    if (n == 1) //Monta grupo de questão falsa, contendo questão verdadeiras ou falsas.
                    {
                        n = rQuestao.Next(0, 6);//Escolhe grupo

                        #region GRUPO 1
                        if (n ==0 && lstQuestaoFalsa.Count == 2) //Questão falsa em grupo
                        {
                            var sTemp = "";

                            n = rQuestao.Next(0, 2); //Ordem da questão falsas.

                            if (n == 0)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }
                            else
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II  estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 2
                        if (n == 1 && lstQuestaoFalsa.Count == 3) //Questão falsa em grupo
                        {
                            var sTemp = "";

                            n = rQuestao.Next(0, 5); //Ordem da questão falsas.

                            if (n == 0)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 1)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 2)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 3)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 4)
                            {
                                sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II  estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 3
                        if (n == 2 && lstQuestaoVerdadeira.Count == 3) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 5); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 2)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 3)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 4)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 4
                        if (n == 3 &&  lstQuestaoVerdadeira.Count == 2) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 2); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 5
                        if (n == 4 && lstQuestaoVerdadeira.Count > 0 && lstQuestaoFalsa.Count > 0) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var nTentativaGrupo5 = 0;
                                var nAcho = 0;

                                while (true)
                                {
                                    var sTemp = "";

                                    var sTempVerdadeira = "";
                                    var sTempFalsa = "";

                                    var nVerdadeira = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.
                                    var nFalsa = rQuestao.Next(0, lstQuestaoFalsa.Count);      //Ordem da questão falsas.

                                    var nOrdem = rQuestao.Next(0, 2);

                                    sTempVerdadeira = lstQuestaoVerdadeira[nVerdadeira].Trim().Replace("(", "").Replace(")", "");
                                    
                                    sTempFalsa = lstQuestaoFalsa[nFalsa].Trim().Replace("(", "").Replace(")", "");

                                    if (nOrdem == 0)
                                    {
                                        sTemp = sTempVerdadeira + " e " + sTempFalsa;                                      
                                    }
                                    else
                                    {
                                        sTemp = sTempFalsa + " e " + sTempVerdadeira;
                                    }

                                    if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                    {
                                        switch (sTemp)
                                        {
                                            //Grupo B OPÇÃO 1
                                            case "I e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;
                                        }
                                    }

                                    nTentativaGrupo5++;

                                    if (nTentativaGrupo5 > 10 || nAcho  > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 6
                        if (n == 5  && lstQuestaoVerdadeira.Count == 3 && lstQuestaoFalsa.Count == 0) //Questão falsa com grupo de questão verdadeira
                        {
                            var nTentativaGrupo5 = 0;
                            var nAcho = 0;

                            while (true)
                            {
                                var sTemp = "";

                                var sTempVerdadeira = "";
                                var sTempFalsa = "";

                                var nVerdadeira      = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.
                                var nVerdadeiraFalsa = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.

                                var nOrdem = rQuestao.Next(0, 2);

                                sTempVerdadeira = lstQuestaoVerdadeira[nVerdadeira].Trim().Replace("(", "").Replace(")", "");

                                sTempFalsa      = lstQuestaoVerdadeira[nVerdadeiraFalsa].Trim().Replace("(", "").Replace(")", "");

                                if (nOrdem == 0)
                                {
                                    sTemp = sTempVerdadeira + " e " + sTempFalsa;
                                }
                                else
                                {
                                    sTemp = sTempFalsa + " e " + sTempVerdadeira;
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;
                                    }
                                }

                                nTentativaGrupo5++;

                                if (nTentativaGrupo5 > 10 || nAcho > 0)
                                {
                                    break;
                                }
                            }
                        }
                        #endregion

                    }
                    #endregion
                }
                #endregion

                #region QUESTÕES INDIVIDUAL
                if (n == 1) //Individual
                {
                    n = rQuestao.Next(0, 4); //Verifica-se vai escolher pergunta verdadeira ou falsa.

                    #region QUESTÃO VERDADEIRA.
                    if (n == 0 && lstQuestaoVerdadeira.Count == 1) //Monta questão individual, contendo 1 questão verdadeira.
                    {
                        foreach (string sQuestaoVerdadeira in lstQuestaoVerdadeira)
                        {
                            if (sResposta == string.Empty)
                            {
                                var sTemp = sQuestaoVerdadeira.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    //Grupo A
                                    switch (sTemp)
                                    {
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region QUESTÃO FALSA.
                    if (n == 1) //Monta questão individual, contendo questões falsas.
                    {
                        var nAcho = 0;

                        foreach (string sQuestaoFalsa in lstQuestaoFalsa)
                        {
                            var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo A
                                    case "I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;

                                    case "II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;

                                    case "III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;
                                }
                            }

                            if (nAcho > 0)
                            {
                                break;
                            }

                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion
                    
                    #region QUESTÃO FALSA COM QUESTÃO VERDADEIRA.
                    if (n == 2 && lstQuestaoVerdadeira.Count ==3) //Monta questão individual, contendo questões falsas.
                    {
                        var nAcho = 0;

                        foreach (string sQuestaoFalsa in lstQuestaoVerdadeira)
                        {
                            var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo A
                                    case "I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;

                                    case "II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;

                                    case "III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        break;
                                }
                            }

                            if (nAcho > 0)
                            {
                                break;
                            }
                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion

                    #region QUESTÃO FALSA COM QUESTÃO VERDADEIRA DESDE QUE RESPOSTA JÁ EXISTA.
                    if (n == 3 && lstQuestaoVerdadeira.Count == 2) //Monta questão individual, contendo questões falsas.
                    {
                        if (sResposta != string.Empty)
                        {
                            var nAcho = 0;

                            foreach (string sQuestaoFalsa in lstQuestaoVerdadeira)
                            {
                                var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo A
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;
                                    }
                                }

                                if (nAcho > 0)
                                {
                                    break;
                                }
                            }
                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion
                }
                #endregion
                
                #region ESCOLHE TODAS TODAS CORRETAS

                if (nRespostaEscolhida == 4)
                {
                    if (lstQuestaoVerdadeira.Count == 3)
                    {
                        if (!lstResultadoQuestoesInseridas.Contains("Todas estão corretas."))
                        {
                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Todas estão corretas.");
                            sResposta = "E - I II III";
                            nRespostaEscolhida++;
                            lstResultadoQuestoesInseridas.Add("Todas estão corretas.");
                        }
                    }
                }

                #endregion

                if (lstResultadoQuestoesFraseOrdem.Count > 7 && (sResposta != string.Empty))
                {
                    lstResultadoQuestoesFraseOrdem.Add("" + sResposta);
                    break;
                }
            }
            //********************************************************************************************************************************************


            return lstResultadoQuestoesFraseOrdem;
        }

        //Novo formato de analise com 4 questões
        private List<string> GeraQuestao5(ref List<string> lstRespostaQuestao5, int nCodigoDisciplina, int nAssuntoEscolhido)
        {
            List<string> lstResultadoQuestoes = new List<string>();
            List<string> lstResultadoQuestoesFinal = new List<string>();
            List<string> lstResultadoQuestoesFraseOrdem = new List<string>();
            List<string> lstResultadoQuestoesInseridas = new List<string>();

            List<string> lstSimilaridade = new List<string>();

            List<string> lstGabarito = new List<string>();

            List<string> lstResultado = new List<string>();

            int nQuantRespostaCorreta = 0;
            int nQuantRespostaInCorreta = 0;

            int nIdAssuntoEscolhido = nAssuntoEscolhido;

            int nIndex = 0;

            int nQuestaoIndividual = 0;

            Random rQuestaoVerdadeira = new Random();
            Random rQuestaoFalsa      = new Random();
            Random rQuestao           = new Random();

            int nQuestaoVerdadeiraEscolhida = 0;
            int nQuestaoFalsaEscolhida = 0;

            List<string> lstRespostaFalsa = new List<string>();

            List<string> lstQuestaoVerdadeira = new List<string>();
            List<string> lstQuestaoFalsa = new List<string>();

            string sResposta = string.Empty;

            admDados oAdmDados = new admDados();

            admAssunto oAdmAssunto = new admAssunto();

            //********************************************************************************************************************************************

            int qtdQuestao = rQuestaoVerdadeira.Next(0, 5);

            nQuantRespostaCorreta   = 4 - qtdQuestao;

            nQuantRespostaInCorreta = 4 - nQuantRespostaCorreta;
            
            int nRespostaEscolhida = 0;

            //********************************************************************************************************************************************
            //Retorne questões falsas, que irei fazer um sorteio, e fazer escolha de uma difeferente da anterior.
            List<modelDados> oCollDadosVerdadeiro = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 0, "", 1, 0);

            if (oCollDadosVerdadeiro == null)
            {
                return null;
            }
            else
            {
                if (oCollDadosVerdadeiro.Count == 0)
                {
                    return null;
                }
            }

            nIndex = 0;

            for (var q = 1; q <= nQuantRespostaInCorreta; q++)
            {
                if (oCollDadosVerdadeiro.Count > 1)
                {

                    var nTentativas = 0;

                    #region QUESTÃO FALSA
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                        nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaQuestao5.Contains(nQuestaoVerdadeiraEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaQuestao5.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoFalsa.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosVerdadeiro.Count) && (lstRespostaQuestao5.Contains(nQuestaoVerdadeiraEscolhida.ToString())))
                        {
                            lstRespostaQuestao5.Add(nQuestaoVerdadeiraEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoFalsa.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoVerdadeira.Next(1, oCollDadosVerdadeiro.Count + 1);

                    nQuestaoVerdadeiraEscolhida = oCollDadosVerdadeiro[nIndex - 1].Codigo;

                    lstRespostaQuestao5.Add(nQuestaoVerdadeiraEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 4);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstQuestaoFalsa.Add(re);
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFinal.Add(re + " " + oCollDadosVerdadeiro[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }
            //********************************************************************************************************************************************
            //********************************************************************************************************************************************
            //Variavel está como nome de falsa porém é verdadeira as questões que  serão escolhidas
            //Retorne questões verdadeiras, que irei fazer um sorteio, e fazer escolhas de  quatro opções.
            List<modelDados> oCollDadosFalse = oAdmDados.admSelectRows(ref lstResultado, 0, nCodigoDisciplina, nIdAssuntoEscolhido, 1, "", 1, 0);
            //********************************************************************************************************************************************

            for (var q = 1; q <= nQuantRespostaCorreta; q++)
            {
                if (oCollDadosFalse.Count > 1)
                {
                    var nTentativas = 0;

                    #region QUESTÃO VERDADEIRA
                    while (true)
                    {
                        nIndex = 0;

                        nTentativas++;

                        if (chkFraseRelacionada.Checked || nTentativas > 10)
                        {
                            lstSimilaridade.Clear();
                        }

                        nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                        nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                        var sSimilar = oCollDadosVerdadeiro[nIndex - 1].Similar.ToString();

                        if (!lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString()) && !lstSimilaridade.Contains(sSimilar))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoVerdadeira.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }

                        //Tentativas de escolher questão diferente, da já escolhida.
                        if ((nTentativas > oCollDadosFalse.Count) && (lstRespostaFalsa.Contains(nQuestaoFalsaEscolhida.ToString())))
                        {
                            lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                            lstSimilaridade.Add(sSimilar);

                            while (true)
                            {
                                var n = rQuestaoFalsa.Next(0, 4);

                                var re = AlfabetoRomano(n);

                                if (!lstResultadoQuestoes.Contains(re))
                                {
                                    lstQuestaoVerdadeira.Add(re);
                                    lstResultadoQuestoes.Add(re);
                                    lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    #endregion

                }
                else
                {
                    nIndex = rQuestaoFalsa.Next(1, oCollDadosFalse.Count + 1);

                    nQuestaoFalsaEscolhida = oCollDadosFalse[nIndex - 1].Codigo;

                    lstRespostaFalsa.Add(nQuestaoFalsaEscolhida.ToString());

                    lstSimilaridade.Add(oCollDadosVerdadeiro[nIndex - 1].Similar.ToString());

                    while (true)
                    {
                        var n = rQuestaoFalsa.Next(0, 4);

                        var re = AlfabetoRomano(n);

                        if (!lstResultadoQuestoes.Contains(re))
                        {
                            lstQuestaoVerdadeira.Add(re);
                            lstResultadoQuestoes.Add(re);
                            lstResultadoQuestoesFinal.Add(re + " " + oCollDadosFalse[nIndex - 1].Frase);
                            break;
                        }
                    }
                }
            }










            //MONTAGEM DAS PERGUNTAS, CONFORME ALTERNATIVA CORRETA OU INCORRETA.
            //********************************************************************************************************************************************
            //Seleção de 5 possíveis alternativas de 8


            lstResultadoQuestoesFraseOrdem.Clear();
            lstResultadoQuestoesInseridas.Clear();

            //********************************************************************************************************************************************
            for (var o = 0; o < 4; o++)
            {
                for (var t = 0; t < 4; t++)
                {
                    if (lstResultadoQuestoes[t] == AlfabetoRomano(o))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(lstResultadoQuestoesFinal[t]);
                    }
                }
            }
            //********************************************************************************************************************************************

            //********************************************************************************************************************************************

            nRespostaEscolhida = 0;

            while (true)
            {
                var n = 0;

                #region ESCOLHE NENHUMA ESTÁ CORRETA

                if (lstQuestaoFalsa.Count == 4)
                {
                    if (!lstResultadoQuestoesInseridas.Contains("Nenhuma está correta."))
                    {
                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Nenhuma está correta.");
                        lstResultadoQuestoesInseridas.Add("Nenhuma está correta.");

                        sResposta = "A";

                        nRespostaEscolhida++;
                    }
                }

                #endregion

                n = rQuestao.Next(0, 2); //Verifica-se vai escolher pergunta individual ou grupo.

                #region QUESTÕES EM GRUPO
                if (n == 0) //grupo
                {
                    n = rQuestao.Next(0, 2); //Verifica-se vai escolher pergunta verdadeira ou falsa.

                    #region VERDADEIRA
                    if (n == 0 && lstQuestaoVerdadeira.Count > 1) //Monta grupo de questão verdadeira,contendo 2 questão verdadeira.
                    {
                        if (sResposta == string.Empty)
                        {
                             var sTemp = "";

                             if (lstQuestaoVerdadeira.Count == 2)
                             {
                                 sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                             }

                             if (lstQuestaoVerdadeira.Count == 3)
                             {
                                 sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                             }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                        
                                    case "III, I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region FALSA
                    if (n == 1) //Monta grupo de questão falsa, contendo questão verdadeiras ou falsas.
                    {
                        n = rQuestao.Next(0, 10);//Escolhe grupo

                        #region GRUPO 1
                        if (n == 0 && lstQuestaoFalsa.Count == 2) //Questão falsa em grupo
                        {
                            var sTemp = "";

                            n = rQuestao.Next(0, 2); //Ordem da questão falsas.

                            if (n == 0)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }
                            else
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 2
                        if (n == 1 && lstQuestaoFalsa.Count == 3) //Questão falsa em grupo
                        {
                            var sTemp = "";

                            n = rQuestao.Next(0, 5); //Ordem da questão falsas.

                            if (n == 0)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 1)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 2)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 3)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 4)
                            {
                                sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 2B
                        if (n == 2 && lstQuestaoFalsa.Count == 4) //Questão falsa em grupo
                        {
                            var sTemp = "";

                            n = rQuestao.Next(0, 11); //Ordem da questão falsas.

                            if (n == 0)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 1)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 2)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 3)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 4)
                            {
                                sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }
                            
                            if (n == 5)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 6)
                            {
                                sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 7)
                            {
                                sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 8)
                            {
                                sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 9)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (n == 10)
                            {
                                sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "");
                            }

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 1
                                    case "I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }

                                switch (sTemp)
                                {
                                    //Grupo B OPÇÃO 2
                                    case "II e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, III e I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "IV, II e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "II, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, II e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, IV e III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "I, III e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;

                                    case "III, I e IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 3
                        if (n == 3 && lstQuestaoVerdadeira.Count == 3) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 5); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 2)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 3)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 4)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, III e I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, II e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, III e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, III e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 3B
                        if (n == 4 && lstQuestaoVerdadeira.Count == 4) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 11); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 2)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 3)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 4)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }
                                
                                if (n == 5)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 6)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 7)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 8)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 9)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 10)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }
                                
                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, III e I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, II e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, III e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, III e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 4
                        if (n == 5 && lstQuestaoVerdadeira.Count == 2) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 2); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, III e I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, II e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, III e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, III e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 4B
                        if (n == 6 && lstQuestaoVerdadeira.Count == 8) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var sTemp = "";

                                n = rQuestao.Next(0, 3); //Ordem da questão falsas.

                                if (n == 0)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 1)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 2)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 3)
                                {
                                    sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 4)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 5)
                                {
                                    sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 6)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (n == 7)
                                {
                                    sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, III e I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "II, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, II e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "I, III e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "III, I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;

                                        case "IV, III e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 5
                        if (n == 7 && lstQuestaoVerdadeira.Count > 0 && lstQuestaoFalsa.Count > 0) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var nTentativaGrupo5 = 0;
                                var nAcho = 0;

                                while (true)
                                {
                                    var sTemp = "";

                                    var sTempVerdadeira = "";
                                    var sTempFalsa = "";

                                    var nVerdadeira = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.
                                    var nFalsa      = rQuestao.Next(0, lstQuestaoFalsa.Count);      //Ordem da questão falsas.

                                    var nOrdem = rQuestao.Next(0, 2);

                                    sTempVerdadeira = lstQuestaoVerdadeira[nVerdadeira].Trim().Replace("(", "").Replace(")", "");

                                    sTempFalsa      = lstQuestaoFalsa[nFalsa].Trim().Replace("(", "").Replace(")", "");

                                    if (nOrdem == 0)
                                    {
                                        sTemp = sTempVerdadeira + " e " + sTempFalsa;
                                    }
                                    else
                                    {
                                        sTemp = sTempFalsa + " e " + sTempVerdadeira;
                                    }

                                    if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                    {
                                        switch (sTemp)
                                        {
                                            //Grupo B OPÇÃO 1
                                            case "I e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "III, I e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II, III e I":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV, II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II, IV e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "III, II e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, IV e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, III e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "III, I e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV, III e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                break;
                                        }
                                    }

                                    nTentativaGrupo5++;

                                    if (nTentativaGrupo5 > 10 || nAcho > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 5B
                        if (n == 8 && lstQuestaoVerdadeira.Count > 0 && lstQuestaoFalsa.Count > 0) //Questão falsa com grupo de questão verdadeira
                        {
                            if (sResposta != string.Empty)
                            {
                                var nTentativaGrupo5 = 0;
                                var nAcho = 0;

                                while (true)
                                {
                                    var sTemp = "";

                                    var nVerdadeira = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.
                                    var nFalsa = rQuestao.Next(0, lstQuestaoFalsa.Count);      //Ordem da questão falsas.

                                    var nOrdem = 0;
                                    
                                    if (lstQuestaoVerdadeira.Count == 2 && lstQuestaoFalsa.Count == 2)
                                    {
                                        nOrdem = rQuestao.Next(0, 3);

                                        if (nOrdem == 0)
                                        {
                                            sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 1)
                                        {
                                            sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 2)
                                        {
                                            sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                        }
                                    }

                                    if (lstQuestaoVerdadeira.Count == 3 && lstQuestaoFalsa.Count == 1)
                                    {
                                        nOrdem = rQuestao.Next(0, 4);

                                        if (nOrdem == 0)
                                        {
                                            sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 1)
                                        {
                                            sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 2)
                                        {
                                            sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 3)
                                        {
                                            sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 4)
                                        {
                                            sTemp = lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 5)
                                        {
                                            sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 5)
                                        {
                                            sTemp = lstQuestaoVerdadeira[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[1].Trim().Replace("(", "").Replace(")", "");
                                        }
                                    }

                                    if (lstQuestaoVerdadeira.Count == 1 && lstQuestaoFalsa.Count == 3)
                                    {
                                        nOrdem = rQuestao.Next(0, 4);

                                        if (nOrdem == 0)
                                        {
                                            sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 1)
                                        {
                                            sTemp = lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 2)
                                        {
                                            sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 3)
                                        {
                                            sTemp = lstQuestaoVerdadeira[0].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 4)
                                        {
                                            sTemp = lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 5)
                                        {
                                            sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                                        }

                                        if (nOrdem == 5)
                                        {
                                            sTemp = lstQuestaoFalsa[2].Trim().Replace("(", "").Replace(")", "") + ", " + lstQuestaoFalsa[0].Trim().Replace("(", "").Replace(")", "") + " e " + lstQuestaoFalsa[1].Trim().Replace("(", "").Replace(")", "");
                                        }
                                    }

                                    if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                    {
                                        switch (sTemp)
                                        {
                                            case "III, I e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II, III e I":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV, II e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "II, IV e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "III, II e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, IV e III":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "I, III e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "III, I e IV":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                nAcho++;
                                                break;

                                            case "IV, III e II":
                                                lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                                sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                                nRespostaEscolhida++;
                                                lstResultadoQuestoesInseridas.Add(sTemp);
                                                break;
                                        }
                                    }

                                    nTentativaGrupo5++;

                                    if (nTentativaGrupo5 > 10 || nAcho > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region GRUPO 6
                        if (n == 9 && lstQuestaoVerdadeira.Count == 3 && lstQuestaoFalsa.Count == 0) //Questão falsa com grupo de questão verdadeira
                        {
                            var nTentativaGrupo5 = 0;
                            var nAcho = 0;

                            while (true)
                            {
                                var sTemp = "";

                                var sTempVerdadeira = "";
                                var sTempFalsa = "";

                                var nVerdadeira = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.
                                var nVerdadeiraFalsa = rQuestao.Next(0, lstQuestaoVerdadeira.Count); //Ordem da questão falsas.

                                var nOrdem = rQuestao.Next(0, 2);

                                sTempVerdadeira = lstQuestaoVerdadeira[nVerdadeira].Trim().Replace("(", "").Replace(")", "");

                                sTempFalsa = lstQuestaoVerdadeira[nVerdadeiraFalsa].Trim().Replace("(", "").Replace(")", "");

                                if (nOrdem == 0)
                                {
                                    sTemp = sTempVerdadeira + " e " + sTempFalsa;
                                }
                                else
                                {
                                    sTemp = sTempFalsa + " e " + sTempVerdadeira;
                                }

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo B OPÇÃO 1
                                        case "I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II e III estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "IV e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e II estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV e III estão corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I e IV estáo corretas.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "III, I e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "II, III e I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "IV, II e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "II, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "III, II e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I, IV e III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "I, III e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "III, I e IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I, IV e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            break;

                                        case "IV, III e II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV, II e III estáo corretas.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sTemp;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            break;
                                    }
                                }

                                nTentativaGrupo5++;

                                if (nTentativaGrupo5 > 10 || nAcho > 0)
                                {
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion

                #region QUESTÕES INDIVIDUAL
                if (n == 1) //Individual
                {
                    n = rQuestao.Next(0, 6); //Verifica-se vai escolher pergunta verdadeira ou falsa.

                    #region QUESTÃO VERDADEIRA.
                    if (n == 0 && lstQuestaoVerdadeira.Count == 1 && nQuestaoIndividual < 3) //Monta questão individual, contendo 1 questão verdadeira.
                    {
                        foreach (string sQuestaoVerdadeira in lstQuestaoVerdadeira)
                        {
                            if (sResposta == string.Empty)
                            {
                                var sTemp = sQuestaoVerdadeira.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    //Grupo A
                                    switch (sTemp)
                                    {
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region QUESTÃO VERDADEIRA.
                    if (n == 1 && lstQuestaoVerdadeira.Count > 1 && nQuestaoIndividual < 3) //Monta questão individual, contendo 1 questão verdadeira.
                    {
                        foreach (string sQuestaoVerdadeira in lstQuestaoVerdadeira)
                        {
                            if (sResposta == string.Empty)
                            {
                                var sTemp = sQuestaoVerdadeira.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    //Grupo A
                                    switch (sTemp)
                                    {
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;

                                        case "IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                            sResposta = Alfabeto(nRespostaEscolhida) + " " + sQuestaoVerdadeira;
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nQuestaoIndividual++;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region QUESTÃO FALSA.
                    if (n == 2 &&  nQuestaoIndividual < 3) //Monta questão individual, contendo questões falsas.
                    {
                        var nAcho = 0;

                        foreach (string sQuestaoFalsa in lstQuestaoFalsa)
                        {
                            var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo A
                                    case "I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;
                                }
                            }

                            if (nAcho > 0)
                            {
                                break;
                            }

                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion

                    #region QUESTÃO FALSA COM QUESTÃO VERDADEIRA.
                    if (n == 3 && lstQuestaoVerdadeira.Count == 3 && nQuestaoIndividual < 3) //Monta questão individual, contendo questões falsas.
                    {
                        var nAcho = 0;

                        foreach (string sQuestaoFalsa in lstQuestaoVerdadeira)
                        {
                            var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                            if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                            {
                                switch (sTemp)
                                {
                                    //Grupo A
                                    case "I":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "II":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "III":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;

                                    case "IV":
                                        lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                        nRespostaEscolhida++;
                                        lstResultadoQuestoesInseridas.Add(sTemp);
                                        nAcho++;
                                        nQuestaoIndividual++;
                                        break;
                                }
                            }

                            if (nAcho > 0)
                            {
                                break;
                            }
                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion

                    #region QUESTÃO FALSA COM QUESTÃO VERDADEIRA DESDE QUE RESPOSTA JÁ EXISTA.
                    if (n == 4 && lstQuestaoVerdadeira.Count == 2 &&  nQuestaoIndividual < 3) //Monta questão individual, contendo questões falsas.
                    {
                        if (sResposta != string.Empty)
                        {
                            var nAcho = 0;

                            foreach (string sQuestaoFalsa in lstQuestaoVerdadeira)
                            {
                                var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo A
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;
                                    }
                                }

                                if (nAcho > 0)
                                {
                                    break;
                                }
                            }
                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion

                    #region QUESTÃO FALSA COM QUESTÃO VERDADEIRA DESDE QUE RESPOSTA JÁ EXISTA.
                    if (n == 5 && lstQuestaoVerdadeira.Count > 2 &&  nQuestaoIndividual < 3) //Monta questão individual, contendo questões falsas.
                    {
                        if (sResposta != string.Empty)
                        {
                            var nAcho = 0;

                            foreach (string sQuestaoFalsa in lstQuestaoVerdadeira)
                            {
                                var sTemp = sQuestaoFalsa.Replace("(", "").Replace(")", "");

                                if (!lstResultadoQuestoesInseridas.Contains(sTemp))
                                {
                                    switch (sTemp)
                                    {
                                        //Grupo A
                                        case "I":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas I está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "II":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas II está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "III":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas III está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;

                                        case "IV":
                                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Apenas IV está correta.");
                                            nRespostaEscolhida++;
                                            lstResultadoQuestoesInseridas.Add(sTemp);
                                            nAcho++;
                                            nQuestaoIndividual++;
                                            break;
                                    }
                                }

                                if (nAcho > 0)
                                {
                                    break;
                                }
                            }
                        }

                        //Monta questão falsa com questão verdadeira no caso de todas serem verdadeiras
                    }
                    #endregion
                }
                #endregion

                #region ESCOLHE TODAS TODAS CORRETAS

                if (nRespostaEscolhida == 4)
                {
                    if (lstQuestaoVerdadeira.Count == 4)
                    {
                        if (!lstResultadoQuestoesInseridas.Contains("Todas estão corretas."))
                        {
                            lstResultadoQuestoesFraseOrdem.Add(Alfabeto(nRespostaEscolhida) + " Todas estão corretas.");
                            sResposta = "E - I II III IV";
                            nRespostaEscolhida++;
                            lstResultadoQuestoesInseridas.Add("Todas estão corretas.");
                        }
                    }
                }

                #endregion

                if (lstResultadoQuestoesFraseOrdem.Count > 8 && (sResposta != string.Empty))
                {
                    lstResultadoQuestoesFraseOrdem.Add("" + sResposta);
                    break;
                }
            }
            //********************************************************************************************************************************************


            return lstResultadoQuestoesFraseOrdem;
        }




        private string Alfabeto(int nNumero)
        {
            string strResultado = "";

            switch (nNumero)
            {
                case 0:
                    strResultado = "(A)";
                    break;

                case 1:
                    strResultado = "(B)";
                    break;

                case 2:
                    strResultado = "(C)";
                    break;

                case 3:
                    strResultado = "(D)";
                    break;

                case 4:
                    strResultado = "(E)";
                    break;            
            }

            return strResultado;
        }

        private string AlfabetoGabarito(int nNumero)
        {
            string strResultado = "";

            switch (nNumero)
            {
                case 0:
                    strResultado = "(A)"; //Nenhuma está correta.
                    break;
                case 1:
                    strResultado = "(B)"; // Apenas uma está correta.
                    break;
                case 2:
                    strResultado = "(C)"; //Apenas duas estão corretas.
                    break;
                case 3:
                    strResultado = "(D)"; // Apenas três estão corretas.
                    break;
                case 4:
                    strResultado = "(E)"; //Todas estão corretas.
                    break;
            }

            return strResultado;
        }

        private string AlfabetoGabaritoFormatoNovo(string nValor)
        {
            string strResultado = "";

            switch (nValor.Trim())
            {
                case "":
                    strResultado = "(A)"; //Nenhuma está correta.
                    break;
                case "(I)":
                    strResultado = "(B)"; // Apenas uma está correta.
                    break;
                case "(I)-(III)":
                    strResultado = "(C)"; //Apenas duas estão corretas.
                    break;
                case "(II)-(III)":
                    strResultado = "(D)"; // Apenas três estão corretas.
                    break;
                case "(I)-(II)-(III)":
                    strResultado = "(E)"; //Todas estão corretas.
                    break;
                default:
                    strResultado = nValor;
                    break;
            }

            return strResultado;
        }

        private string AlfabetoRomano(int nNumero)
        {
            string strResultado = "";

            switch (nNumero)
            {
                case 0:
                    strResultado = "(I)";
                    break;

                case 1:
                    strResultado = "(II)";
                    break;

                case 2:
                    strResultado = "(III)";
                    break;

                case 3:
                    strResultado = "(IV)";
                    break;
            }

            return strResultado;
        }





        private void principal_Load(object sender, EventArgs e)
        {
            Novo();
        }

        private void cboAssunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cboDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAssunto.Text = string.Empty;
            Assunto();
        }

        private void cboDisciplina_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }

        private void cboAssunto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)0;
        }

        private void txtResultado_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (txtResultado.Text.Length > 0)
            {
                Clipboard.SetText(txtResultado.Text);
            }
            else
            {
                MessageBox.Show("Não existe resposta para copiar.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            admExportarExcel oAdmExcel = new admExportarExcel();

            string[] lstTitulo = new string[5000];

            string[] lstDados  = new string[5000];

            string[] lstNulo   = new string[0];

            lstTitulo[0] = "TEXTO";
            
            lstDados[0] = "1";
            lstDados[1] = "2";
            lstDados[2] = "3";
            lstDados[3] = "4XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";

            oAdmExcel.gerarExcelDllIntenrop("",1, lstDados.Length, lstTitulo, lstDados, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo, lstNulo);
        }

        public void AvisoDoProgresso(int nRegistro, int nRegistroTotal)
        {
            if (nRegistro <= pBar.Maximum)
            {
                pBar.Value = nRegistro;
                pBar.Maximum = nRegistroTotal;
            }
        }

        private void chkGeracaoRadomica_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeracaoRadomica.Checked)
            {
                grBoxTipoQuestao.Enabled = true;
                numQuestao.Enabled = false;
            }
            else 
            {
                grBoxTipoQuestao.Enabled = false;
                numQuestao.Enabled = true;
            }
        }

        private void numTpQuestaoCorreta_Click(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch{}
        }

        private void numTpQuestaoIncorreta_Click(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoAnalise3_Click(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoAnalise4_Click(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoCorreta_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoIncorreta_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoAnalise3_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void numTpQuestaoAnalise4_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                numQuestao.Value = Convert.ToInt32(numTpQuestaoCorreta.Value + numTpQuestaoIncorreta.Value + numTpQuestaoAnalise3.Value + numTpQuestaoAnalise4.Value);
            }
            catch { }
        }

        private void chkFormatoNovo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFormatoNovo.Checked)
            {
                numTpQuestaoAnalise4.Enabled = true;
            }
            else
            {
                numTpQuestaoAnalise4.Enabled = false;
            }
        }
    }
}
