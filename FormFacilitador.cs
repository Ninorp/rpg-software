using RpG_Software.Control;
using RpG_Software.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RpG_Software
{
    public partial class FormFacilitador : Form
    {
        private SQLiteAsyncConnection bdconn;
        int[] cod_fac = new int[300];
        public FormFacilitador()
        {
            InitializeComponent();
            bdconn = new SQLiteAsyncConnection("bd_rpg.db");
        }

        private async void btAplicar_Click(object sender, EventArgs e)
        {
            try
            {
                await bdconn.CreateTableAsync<Facilitador>();
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        var f = new Facilitador(txtNome.Text, Convert.ToInt16(numIdade.Value), rbMasculino.Checked ? rbMasculino.Text : rbFeminino.Text);
                        var re = await bdconn.InsertAsync(f);
                        if (re == 1)
                        {
                            txtNome.Text = string.Empty;
                            numIdade.ResetText();
                            MessageBox.Show("Foi cadastrado com sucesso!", "Bem-sucedido o cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Não foi possivel inserir o cadastro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 1:
                        if (cmBoxSearch.SelectedIndex == -1)
                        {
                            MessageBox.Show("Por favor, selecione um nome para alterar ou excluir!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (rbAlterar.Checked)
                        {
                            Facilitador face = null;
                            int id = cod_fac[cmBoxSearch.SelectedIndex];
                            face = await bdconn.FindAsync<Facilitador>(x => x.ID == id);
                            face.Nome = txtNomeEditar.Text;
                            face.Idade = Convert.ToInt16(numIdade.Value);
                            face.Sexo = cmbSexo.Text;
                            var res = await bdconn.UpdateAsync(face);
                            //cmBoxSearch.SelectedText = txtNomeEditar.Text;
                            if (res == 1)
                                MessageBox.Show("Foi editado com sucesso!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else
                                MessageBox.Show("Não foi possivel fazer a edição!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            DialogResult respdel = MessageBox.Show("Tem certeza?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            switch (respdel)
                            {
                                case DialogResult.Yes:
                                    int id = cod_fac[cmBoxSearch.SelectedIndex];
                                    var faced = await bdconn.FindAsync<Facilitador>(x => x.ID == id);
                                    await bdconn.DeleteAsync(faced);
                                    MessageBox.Show("O cadastro foi excluído com sucesso!", "Adeus!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    for (int i = cmBoxSearch.SelectedIndex; i < cod_fac.Length - 1; i++)
                                    {
                                        cod_fac[i] = cod_fac[i + 1];
                                    }
                                    txtNomeEditar.Text = string.Empty;
                                    numIdadeEditar.ResetText();
                                    cmBoxSearch.Items.RemoveAt(cmBoxSearch.SelectedIndex);
                                    break;
                            }
                        }
                        break;
                }
            }
            catch (Exception)
            {
                
            }
            
        }

        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            CriticaDeDados letra = new CriticaDeDados();
            bool receive = letra.verifica_letra(e.KeyChar);

            if (receive == false)
            {
                e.Handled = true;
            }
        }

        private async void btSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var dF = await bdconn.Table<Facilitador>().Where(x => x.Nome.StartsWith(txtNomeSearch.Text) && txtNomeSearch.Text != string.Empty).ToListAsync();
                if (txtNomeSearch.Text == string.Empty)
                {
                    dF = await bdconn.Table<Facilitador>().ToListAsync();
                }
                cmBoxSearch.Items.Clear();
                txtNomeEditar.Text = string.Empty;
                numIdade.Value = 0;
                cmbSexo.SelectedIndex = -1;

                for (int i = 0; i < dF.Count; i++)
                {
                    cmBoxSearch.Items.Add(dF[i].Nome);
                    cod_fac[i] = dF[i].ID;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private async void cmBoxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmBoxSearch.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                Facilitador face = null;
                int id = cod_fac[cmBoxSearch.SelectedIndex];                
                face = await bdconn.FindAsync<Facilitador>(x => x.ID == id);

                txtNomeEditar.Text = face.Nome;
                numIdadeEditar.Value = face.Idade;
                if (face.Sexo == "Masculino")
                {
                    cmbSexo.SelectedIndex = 0;
                }
                else
                    cmbSexo.SelectedIndex = 1;
            }
            catch (Exception)
            {
                
            }
        }

        private async void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                await encheafont();
            }
        }

        public async Task encheafont()
        {
            try
            {
                var dF = await bdconn.Table<Facilitador>().ToListAsync();
                for (int i = 0; i < dF.Count; i++)
                {
                    if (!cmBoxSearch.Items.Contains(dF[i].Nome))
                    {
                        cmBoxSearch.Items.Add(dF[i].Nome);
                        cod_fac[i] = dF[i].ID;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
