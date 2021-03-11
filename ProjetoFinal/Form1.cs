using EM.Domain;
using EM.Repository;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoFinal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        private void MostarTabela(object aluno)
        {
            dgvDados.DataSource = bindingSource;
            dgvDados.AutoGenerateColumns = true;
            bindingSource.DataSource = aluno;
        }

        private void Adicionar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno();
                RepositorioAluno repositorio = new RepositorioAluno();

                if (repositorio.GetByMatricula(Convert.ToInt32(txtMatricula.Text)) != null) throw new Exception("Aluno já cadastrado");
                if (string.IsNullOrEmpty(txtNome.Text) || !maskNasc.MaskCompleted || string.IsNullOrEmpty(txtMatricula.Text)) throw new Exception("Campos vazios!\nPor favor preencher!");
                if (Convert.ToDateTime(maskNasc.Text) > DateTime.Now) throw new Exception("Data Invalida!\nBem Vindo viajante do tempo!");

                if (txtCpf.Text != string.Empty && txtCpf.Text != null && txtCpf.Text.Length > 0)
                {
                    if (ValidarCpf(txtCpf.Text))
                    {
                        repositorio.Add(aluno = Dados(aluno));
                        MostarTabela(aluno);
                        Limpar_Click(sender, e);
                    }
                    else throw new Exception("Cpf Invalido!");
                }
                else
                {
                    repositorio.Add(aluno = Dados(aluno));
                    MostarTabela(aluno);
                    Limpar_Click(sender, e);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            try
            {
                Aluno aluno = new Aluno();
                RepositorioAluno repositorio = new RepositorioAluno();

                if (string.IsNullOrEmpty(txtNome.Text) || !maskNasc.MaskCompleted || string.IsNullOrEmpty(txtMatricula.Text)) throw new Exception("Campos vazios!\nPor favor preencher todos!");
                if (Convert.ToDateTime(maskNasc.Text) > DateTime.Now) throw new Exception("Formatação de Data errada!\nBem Vindo viajante do tempo!");
                if (txtCpf.Text != string.Empty && txtCpf.Text != null && txtCpf.Text.Length > 0)
                {
                    if (ValidarCpf(txtCpf.Text))
                    {
                        repositorio.Update(aluno = Dados(aluno));
                        MostarTabela(aluno);
                        Limpar_Click(sender, e);
                    }
                    else throw new Exception("Cpf Invalido!");
                }
                else
                {
                    repositorio.Update(aluno = Dados(aluno));
                    MostarTabela(aluno);
                    Limpar_Click(sender, e);
                }
                Cancelar_click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Hand); }
        }

        private void Cancelar_click(object sender, EventArgs e)
        {
            Limpar_Click(sender, e);
            btnLimpar.Click -= Cancelar_click;
            btnLimpar.Click += new EventHandler(Limpar_Click);
            ButtonAdd.Click -= Alterar_Click;
            ButtonAdd.Click += new EventHandler(Adicionar_Click);
            btnLimpar.Text = "Limpar";
            ButtonAdd.Text = "Adicionar";
            txtMatricula.ReadOnly = false;
            label1.Text = "Novo Aluno";
        }

        private void Deletar_Click(object sender, EventArgs e)
        {
            try
            {
                RepositorioAluno repositorio = new RepositorioAluno();
                if (repositorio.GetByMatricula(Convert.ToInt32(txtMatricula.Text)) == null) { Limpar_Click(sender, e); throw new Exception("Aluno não cadastrado");  }
                if (txtMatricula.Text == null || txtMatricula.Text == string.Empty) throw new Exception("Primeiro selecione a matricula a ser deletada!");
                else
                {
                    Aluno aluno = repositorio.GetByMatricula(Convert.ToInt32(txtMatricula.Text));

                    DialogResult result = MessageBox.Show($"Confimar Deleção:\n\n\tAluno: {aluno.Nome}.\n\tMatricula: {aluno.Matricula}.\n\tNascimento: {aluno.Nascimento.Day + "/" + aluno.Nascimento.Month + "/" + aluno.Nascimento.Year}.\n\tSexo: {aluno.Sexo}.\n\tCpf: {aluno.CPF}", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.OK))
                    {
                        repositorio.Remove(aluno);
                        MessageBox.Show("Aluno deletado!", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpar_Click(sender, e);
                    }
                    MostarTabela(repositorio.GetAll());
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void DgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow linhaSelecionada = dgvDados.SelectedRows[0];
            txtNome.Text = Convert.ToString(linhaSelecionada.Cells["Nome"].Value);
            txtCpf.Text = Convert.ToString(linhaSelecionada.Cells["CPF"].Value);
            txtMatricula.Text = Convert.ToString(linhaSelecionada.Cells["Matricula"].Value);
            cboxSexo.Text = Convert.ToString(linhaSelecionada.Cells["Sexo"].Value);
            maskNasc.Text = Convert.ToString(linhaSelecionada.Cells["Nascimento"].Value);
        }

        private void Editar_Click(object sender, EventArgs e)
        {
            if (txtMatricula.Text == string.Empty || txtMatricula.Text == null) MessageBox.Show("Primeiro selecione o aluno a ser editado!", "Opa!");
            else
            {
                label1.Text = "Editando Aluno";
                txtMatricula.ReadOnly = true;
                btnLimpar.Text = "Cancelar";
                ButtonAdd.Text = "Modificar";
                btnLimpar.Click -= Limpar_Click;
                btnLimpar.Click += new EventHandler(Cancelar_click);
                ButtonAdd.Click -= Adicionar_Click;
                ButtonAdd.Click += new EventHandler(Alterar_Click);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var repositorio = new RepositorioAluno();
            MostarTabela(repositorio.GetAll());
        }

        private void Limpar_Click(object sender, EventArgs e)
        {
            txtMatricula.Clear();
            txtNome.Clear();
            txtCpf.Clear();
            maskNasc.Clear();
        }

        private void Pesquisar_Click(object sender, EventArgs e)
        {
            RepositorioAluno repositorio = new RepositorioAluno();

            if (int.TryParse(txtPesquisa.Text, out _))
            {
                if (repositorio.GetByMatricula(Convert.ToInt32(txtPesquisa.Text)) == null)
                {
                    MessageBox.Show("Matricula nao encontrada", "Busca de aluno!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPesquisa.Clear();
                }
                else
                {
                    MostarTabela(repositorio.GetByMatricula(Convert.ToInt32(txtPesquisa.Text)));
                }
            }
            else
            {
                if (repositorio.GetByContendoNoNome(txtPesquisa.Text).FirstOrDefault() == null)
                {
                    MessageBox.Show("Aluno não encontrado!", "Busca de aluno!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPesquisa.Clear();
                }
                else
                {
                    MostarTabela(repositorio.GetByContendoNoNome(txtPesquisa.Text));
                }
            }
            if (txtPesquisa.Text == string.Empty)
            {
                MostarTabela(repositorio.GetAll());
            }
        }

        private Aluno Dados(Aluno aluno)
        {
            aluno.Matricula = Convert.ToInt32(txtMatricula.Text);
            aluno.Nome = txtNome.Text.ToLower();
            aluno.Sexo = (EnumeradorSexo)Enum.Parse(typeof(EnumeradorSexo), cboxSexo.Text);
            aluno.Nascimento = Convert.ToDateTime(maskNasc.Text);
            if (txtCpf.Text != string.Empty && txtCpf.Text != null && txtCpf.Text.Length > 0) aluno.CPF = txtCpf.Text;
            else aluno.CPF = null;

            return aluno;
        }

        private void TxtCpf_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 86) { e.Handled = true; e.SuppressKeyPress = true; }
        }

        private void TxtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == 13) Adicionar_Click(sender, e);
        }

        private void TxtCpf_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) MessageBox.Show("Botão direito sobre a caixa de texto desabilitada.");
        }

        private void TxtMatricula_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 86) { e.Handled = true; e.SuppressKeyPress = true; }
        }

        private void TxtMatricula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == 13) Adicionar_Click(sender, e);
        }

        private void TxtMatricula_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) MessageBox.Show("Botão direito sobre a caixa de texto desabilitada.");
        }

        private void TxtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyValue == 86) { e.Handled = true; e.SuppressKeyPress = true; }
        }

        private void TxtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == 13) Adicionar_Click(sender, e);
        }

        private void TxtNome_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) MessageBox.Show("Botão direito sobre a caixa de texto desabilitada.");
        }

        private void TxtPesquisa_KeyPress(object sender, KeyPressEventArgs e)
        {
            RepositorioAluno repositorio = new RepositorioAluno();
            if (e.KeyChar == 13 && txtPesquisa.Text == string.Empty) MostarTabela(repositorio.GetAll());
            if (e.KeyChar == 13) Pesquisar_Click(sender, e);
        }

        private void MaskNasc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) Adicionar_Click(sender, e);
        }
    }
}