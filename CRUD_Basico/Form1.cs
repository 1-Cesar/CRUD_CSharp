using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; 

namespace CRUD_Basico
{
    public partial class frmCadastroCliente : Form
    {
        //preparacao da conexao com banco de dados
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataReader dr;
        string strSQL;

        //variável para saber se o que será salvo é um novo registro ou apenas uma atualização
        bool novo;

        public frmCadastroCliente()
        {
            InitializeComponent();
        }        

        private void frmCadastroCliente_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUf.Enabled = false;
            mskTelefone.Enabled = false;
        }        

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = false;
            tstId.Enabled = false;
            tsbBuscar.Enabled = false;
            txtNome.Enabled = true;
            txtEndereco.Enabled = true;
            mskCEP.Enabled = true;
            txtBairro.Enabled = true;
            txtCidade.Enabled = true;
            txtUf.Enabled = true;
            mskTelefone.Enabled = true;
            txtNome.Focus();
            novo = true;
        }        

        private void tsbSalvar_Click(object sender, EventArgs e)
        {            



            if (novo)
            {
                if (txtNome.Text != "" && txtEndereco.Text != "" && mskCEP.Text != "" && txtBairro.Text != "" && txtCidade.Text != "" && txtUf.Text != "" && mskTelefone.Text != "")
                {

                    //conexao com mysql
                    try
                    {



                        conexao = new MySqlConnection("Server=localhost;Database=cad_cliente;Uid=root;Pwd=root;");
                        strSQL = "INSERT INTO CAD_CLIENTE (NOME,ENDERECO,CEP,BAIRRO,CIDADE,UF,TELEFONE) VALUES (@NOME, @ENDERECO, @CEP, @BAIRRO, @CIDADE, @UF, @TELEFONE)";

                        //capolando base de dados
                        comando = new MySqlCommand(strSQL, conexao);
                        comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                        comando.Parameters.AddWithValue("@ENDERECO", txtEndereco.Text);
                        comando.Parameters.AddWithValue("@CEP", mskCEP.Text);
                        comando.Parameters.AddWithValue("@BAIRRO", txtBairro.Text);
                        comando.Parameters.AddWithValue("@CIDADE", txtCidade.Text);
                        comando.Parameters.AddWithValue("@UF", txtUf.Text);
                        comando.Parameters.AddWithValue("@TELEFONE", mskTelefone.Text);

                        //abrindo a conexao
                        conexao.Open();

                        //execução de comandos
                        comando.ExecuteNonQuery();

                        //mensagem de insert no banco
                        MessageBox.Show("Cadastro realizado com sucesso");

                    }
                    catch (Exception ex)
                    {
                        //mensagem de erro
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        //fechando a conexao
                        conexao.Close();
                        conexao = null;
                        comando = null;
                    }
                }
                                
            }
            else
            {
                try
                {
                    conexao = new MySqlConnection("Server=localhost;Database=cad_cliente;Uid=root;Pwd=root;");
                    
                    strSQL = "UPDATE CAD_CLIENTE SET NOME = @NOME, ENDERECO = @ENDERECO, CEP = @CEP, BAIRRO = @BAIRRO, CIDADE = @CIDADE, UF = @UF, TELEFONE = @TELEFONE WHERE ID =" + tstId.Text;

                    //capolando base de dados
                    comando = new MySqlCommand(strSQL, conexao);

                    comando.Parameters.AddWithValue("@ID", txtId.Text);
                    comando.Parameters.AddWithValue("@NOME", txtNome.Text);
                    comando.Parameters.AddWithValue("@ENDERECO", txtEndereco.Text);
                    comando.Parameters.AddWithValue("@CEP", mskCEP.Text);
                    comando.Parameters.AddWithValue("@BAIRRO", txtBairro.Text);
                    comando.Parameters.AddWithValue("@CIDADE", txtCidade.Text);
                    comando.Parameters.AddWithValue("@UF", txtUf.Text);
                    comando.Parameters.AddWithValue("@TELEFONE", mskTelefone.Text);

                    //abrindo a conexao
                    conexao.Open();

                    //execução de comandos
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Cadastro atualizado com sucesso");
                }
                catch (Exception ex)
                {
                    //mensagem de erro
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //fechando a conexao
                    conexao.Close();
                    conexao = null;
                    comando = null;
                }
            }

            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUf.Enabled = false;
            mskTelefone.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUf.Text = "";
            mskTelefone.Text = "";
            tstId.Text = "";
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            //limpar todos os campos
            tsbNovo.Enabled = true;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tstId.Enabled = true;
            tsbBuscar.Enabled = true;
            txtNome.Enabled = false;
            txtEndereco.Enabled = false;
            mskCEP.Enabled = false;
            txtBairro.Enabled = false;
            txtCidade.Enabled = false;
            txtUf.Enabled = false;
            mskTelefone.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtEndereco.Text = "";
            mskCEP.Text = "";
            txtBairro.Text = "";
            txtCidade.Text = "";
            txtUf.Text = "";
            mskTelefone.Text = "";
            tstId.Text = "";
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cad_cliente;Uid=root;Pwd=root;");

                strSQL = "DELETE FROM CAD_CLIENTE WHERE ID=" + tstId.Text;

                //capolando base de dados
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", txtId.Text);                

                //abrindo a conexao
                conexao.Open();

                //execução de comandos
                comando.ExecuteNonQuery();

                MessageBox.Show("Cadastro excluído com sucesso");

                tsbNovo.Enabled = true;
                tsbSalvar.Enabled = false;
                tsbCancelar.Enabled = false;
                tsbExcluir.Enabled = false;
                tstId.Enabled = true;
                tsbBuscar.Enabled = true;
                txtNome.Enabled = false;
                txtEndereco.Enabled = false;
                mskCEP.Enabled = false;
                txtBairro.Enabled = false;
                txtCidade.Enabled = false;
                txtUf.Enabled = false;
                mskTelefone.Enabled = false;
                txtId.Text = "";
                txtNome.Text = "";
                txtEndereco.Text = "";
                mskCEP.Text = "";
                txtBairro.Text = "";
                txtCidade.Text = "";
                txtUf.Text = "";
                mskTelefone.Text = "";
                tstId.Text = "";
            }
            catch (Exception ex)
            {
                //mensagem de erro
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //fechando a conexao
                conexao.Close();
                conexao = null;
                comando = null;
            }

        }

        private void tsbBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                conexao = new MySqlConnection("Server=localhost;Database=cad_cliente;Uid=root;Pwd=root;");

                strSQL = "SELECT * FROM CAD_CLIENTE WHERE ID =" + tstId.Text;
                                
                comando = new MySqlCommand(strSQL, conexao);

                comando.Parameters.AddWithValue("@ID", txtId.Text);

                //abrindo a conexao
                conexao.Open();

                //execução de comandos
                dr = comando.ExecuteReader();

                tsbNovo.Enabled = false;
                tsbSalvar.Enabled = true;
                tsbCancelar.Enabled = true;
                tsbExcluir.Enabled = true;
                tstId.Enabled = false;
                tsbBuscar.Enabled = false;
                txtNome.Enabled = true;
                txtEndereco.Enabled = true;
                mskCEP.Enabled = true;
                txtBairro.Enabled = true;
                txtCidade.Enabled = true;
                txtUf.Enabled = true;
                mskTelefone.Enabled = true;
                txtNome.Focus();

                while (dr.Read())
                {
                    txtNome.Text = Convert.ToString(dr["nome"]);
                    txtEndereco.Text = Convert.ToString(dr["endereco"]);
                    mskCEP.Text = Convert.ToString(dr["cep"]);
                    txtBairro.Text = Convert.ToString(dr["bairro"]);
                    txtCidade.Text = Convert.ToString(dr["cidade"]);
                    txtUf.Text = Convert.ToString(dr["uf"]);
                    mskTelefone.Text = Convert.ToString(dr["telefone"]);
                    novo = false;
                }
            }
            catch (Exception ex)
            {
                //mensagem de erro
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //fechando a conexao
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }

        /*
         evento bloqueando inserção de caracteres inválidos no campo nome
         e
         manipulação de label correspondente
        */
        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                lblNome.ForeColor = Color.Red;
            }
            else
            {
                lblNome.ForeColor = Color.Black;
            }
            
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo endereco
         e
         manipulação de label correspondente
        */
        private void txtEndereco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                lblEndereco.ForeColor = Color.Red;
            }
            else
            {
                lblEndereco.ForeColor = Color.Black;
            }
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo cep
         e
         manipulação de label correspondente
        */
        private void mskCEP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                lblCep.ForeColor = Color.Red;
            }
            else
            {
                lblCep.ForeColor = Color.Black;
            }
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo bairro
         e
         manipulação de label correspondente
        */
        private void txtBairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
                e.Handled = true;
                lblBairro.ForeColor = Color.Red;
            }
            else
            {
                lblBairro.ForeColor = Color.Black;
            }
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo uf
         e
         manipulação de label correspondente
        */
        private void txtUf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                lblUf.ForeColor = Color.Red;
            }
            else
            {
                lblUf.ForeColor = Color.Black;
            }
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo cidade
         e
         manipulação de label correspondente
        */
        private void txtCidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || char.IsPunctuation(e.KeyChar))
            {
                e.Handled = true;
                lblCidade.ForeColor = Color.Red;
            }
            else
            {
                lblCidade.ForeColor = Color.Black;
            }
        }
        /*
         evento bloqueando inserção de caracteres inválidos no campo telefone
         e
         manipulação de label correspondente
        */
        private void mskTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
                lblTelefone.ForeColor = Color.Red;
            }
            else
            {
                lblTelefone.ForeColor = Color.Black;
            }
        }
    }
}

