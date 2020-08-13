using RpG_Software.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RpG_Software
{
    public partial class FormGrupos : Form
    {
        //aqui que começa a mágica
        public FormGrupos()
        {
            InitializeComponent();
           // listVGrupos.SelectedIndexChanged += teste;
        }
        private SQLiteAsyncConnection bdconn;
        private string[] id_pessoa = new string[500];
        private int[] id_grupo = new int[500];
        int[] id_fac = new int[300];
        int[] id_facaux = new int[300];

        private async void Form2_Load(object sender, EventArgs e)
        {
            bdconn = new SQLiteAsyncConnection("bd_rpg.db");
            await AlocarAutomaticamente();
        }

        private async Task AlocarAutomaticamente()
        {
            try
            {
                await bdconn.CreateTableAsync<Grupo>();
                await bdconn.CreateTableAsync<Pessoa_Grupo>();
                //await bdconn.ExecuteAsync("CREATE TABLE pessoa_grupo(pessoa_grupo_id INTEGER PRIMARY KEY,grupoID varchar(255), pessoaID varchar(255))");
                int countP = await bdconn.Table<Pessoa>().CountAsync();
                int countPG = await bdconn.Table<Pessoa_Grupo>().CountAsync();
                int countG = await bdconn.Table<Grupo>().CountAsync();
                var listP = await bdconn.Table<Pessoa>().ToListAsync();
                Random rnd = new Random();
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Maximum = countP - countPG;
                while(countP != countPG)
                {
                    int index = rnd.Next(listP.Count() - 1);
                    var p = listP[index];
                    Debug.WriteLine(listP.Count());
                    if (await bdconn.Table<Pessoa_Grupo>().Where(x => x.pessoaID == p.ID).FirstOrDefaultAsync() == null)
                    {
                        //vou alocar a pessoa em algum grupo automaticamente
                        if (countG == 0)
                        {
                            await CriaGruposAuto();
                            countG = await bdconn.Table<Grupo>().CountAsync();
                        }
                        
                        if (await AlocarPessoa(p))
                        {
                            listP.RemoveAt(index);                            
                        } else
                        {
                            bool temResto = true;
                            foreach (var item in listP)
                            {
                                if (await AlocarPessoa(item))
                                    temResto = false;
                                if (temResto == false)
                                {
                                    listP.Remove(item);
                                    break;
                                }
                                    
                            }
                            if (temResto)
                            {
                                Grupo g;
                                foreach (var item in listP)
                                {
                                    int idademin = 18, idademax = 100;
                                    if (item.Idade < 18)
                                    {
                                        idademin = 7;
                                        idademax = 18;
                                    }
                                    g = new Grupo(item.Sexo, idademin, idademax, (item.ehPastor || item.ehEsposaDePastor) ? true : false);
                                    await bdconn.InsertAsync(g);
                                    Pessoa_Grupo pessoa_Grupo = new Pessoa_Grupo(g.ID, item.ID);
                                    await bdconn.InsertAsync(pessoa_Grupo);
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        listP.RemoveAt(index);
                        
                    }
                    if (toolStripProgressBar1.Value < toolStripProgressBar1.Maximum)
                    {
                        toolStripProgressBar1.Value = countP - listP.Count;
                    }
                    countPG = await bdconn.Table<Pessoa_Grupo>().CountAsync();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                await AtualizarListadeGrupos(false);
            }
        }

        public async Task CriaGruposAuto()
        {
            int countPMasculinoCrianca = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Masculino" && x.Idade < 9).CountAsync();
            int countPMasculinoJuvenil = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Masculino" && x.Idade > 8 && x.Idade < 13).CountAsync();
            int countPMasculinoAdoles = await bdconn.Table<Pessoa>().Where(x=> x.Sexo=="Masculino" && x.Idade > 12 && x.Idade < 18).CountAsync();
            int countPFemininoCrianca = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Feminino" && x.Idade < 9).CountAsync();
            int countPFemininoJuvenil = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Feminino" && x.Idade > 8 && x.Idade < 13).CountAsync();
            int countPFemininoAdoles = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Feminino" && x.Idade > 8 && x.Idade < 18).CountAsync();
            int countPMasculinoMaior = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Masculino" && x.Idade >= 18 && x.ehPastor != true).CountAsync();
            int countPFemininoMaior = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Feminino" && x.Idade >= 18 && x.ehPastor != true && x.ehEsposaDePastor != true).CountAsync();
            int countPastores = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Masculino" && x.Idade >= 18 && x.ehPastor == true).CountAsync();
            int countPastoras = await bdconn.Table<Pessoa>().Where(x => x.Sexo == "Feminino" && x.Idade >= 18 && (x.ehPastor == true || x.ehEsposaDePastor == true)).CountAsync();
            
            int qtdGruposMasculinosCrianca = countPMasculinoCrianca < 8 ? 1 : Convert.ToInt16(Math.Round(countPMasculinoCrianca / 8.0) + 0.5);
            int qtdGruposMasculinosJuvenil = countPMasculinoJuvenil < 8 ? 1 : Convert.ToInt16(Math.Round(countPMasculinoJuvenil / 8.0) + 0.5);
            int qtdGruposMasculinosAdoles= countPMasculinoAdoles < 8 ? 1 : Convert.ToInt16(Math.Round(countPMasculinoAdoles / 8.0) + 0.5);
            int qtdGruposFemininosCrianca = countPFemininoCrianca < 8 ? 1 : Convert.ToInt16(Math.Round(countPFemininoCrianca / 8.0) + 0.5);
            int qtdGruposFemininosJuvenil = countPFemininoJuvenil < 8 ? 1 : Convert.ToInt16(Math.Round(countPMasculinoJuvenil / 8.0) + 0.5);
            int qtdGruposFemininosAdoles = countPFemininoAdoles < 8 ? 1 : Convert.ToInt16(Math.Round(countPFemininoAdoles / 8.0) + 0.5);
            int qtdGruposMasculinosMaior = Convert.ToInt16(Math.Round(countPMasculinoMaior / 11.0) + 0.5);
            int qtdGruposFemininosMaior = Convert.ToInt16(Math.Round(countPFemininoMaior / 11.0) + 0.5);
            int qtdGruposPastores = countPastores < 9 ? 1 : Convert.ToInt16(Math.Round(countPastores / 8.0) + 0.5);
            int qtdGruposPastoras = countPastoras < 9 ? 1 : Convert.ToInt16(Math.Round(countPastoras / 8.0) + 0.5);

            Grupo g = new Grupo();

            for (int i = 0; i < qtdGruposMasculinosCrianca; i++)
            {               
                g = new Grupo("Masculino", 7, 9);                
                await bdconn.InsertAsync(g);                                     
            }

            for (int i = 0; i < qtdGruposMasculinosJuvenil; i++)
            {
                g = new Grupo("Masculino", 9, 13);
                await bdconn.InsertAsync(g);
            }
            
            for (int i = 0; i < qtdGruposMasculinosAdoles; i++)
            {
                g = new Grupo("Masculino", 13, 18);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposMasculinosMaior; i++)
            {
                g = new Grupo("Masculino", 18, 100);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposFemininosCrianca; i++)
            {
                g = new Grupo("Feminino", 7, 9);
                await bdconn.InsertAsync(g);
            }

            for (int i = 0; i < qtdGruposFemininosJuvenil; i++)
            {
                g = new Grupo("Feminino", 9, 13);
                await bdconn.InsertAsync(g);
            }

            for (int i = 0; i < qtdGruposFemininosAdoles; i++)
            {
                g = new Grupo("Feminino", 13, 18);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposFemininosMaior; i++)
            {
                g = new Grupo("Feminino", 18, 100);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposFemininosMaior; i++)
            {
                g = new Grupo("Feminino", 18, 100);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposPastores; i++)
            {
                g = new Grupo("Masculino", 18, 100, true);
                await bdconn.InsertAsync(g);
            }
            for (int i = 0; i < qtdGruposPastoras; i++)
            {
                g = new Grupo("Feminino", 18, 100, true);
                await bdconn.InsertAsync(g);
            }
        }

        public async Task<bool> AlocarPessoa(Pessoa p)
        {
            List<Grupo> grupo = await bdconn.Table<Grupo>().ToListAsync();
            List<Pessoa_Grupo> PGs = await bdconn.Table<Pessoa_Grupo>().ToListAsync();
            //int length = await bdconn.Table<Pessoa_Grupo>().CountAsync();
            //var query = "SELECT P.ID, P.Nome FROM (Pessoa AS P INNER JOIN Pessoa_Grupo AS PG ON P.ID = PG.pessoaID) WHERE PG.grupoID =";
            //Random rnd = new Random();
            for (int i = 0; i < grupo.Count; i++)
            {
                //int index = rnd.Next(grupo.Count() - 1);
                //var g = grupo[index];
                //var ps = await bdconn.QueryAsync<Pessoa>(query + grupo[i].ID);
                var filtrado = PGs.Where(pg => pg.grupoID == grupo[i].ID);
                List<Pessoa> pf = new List<Pessoa>();
                foreach (var item in filtrado)
                {
                    pf.Add(await bdconn.Table<Pessoa>().Where(pp => pp.ID == item.pessoaID).FirstOrDefaultAsync());
                }
                grupo[i].AddGeral(pf);                                                               
                if (grupo[i].addMembro(p))
                {
                    Pessoa_Grupo pg = new Pessoa_Grupo(grupo[i].ID, p.ID);
                    await bdconn.InsertAsync(pg);
                    return true;
                }
                        
                //grupo.RemoveAt(index);

            }
            return false;
        }

        public async Task AtualizarListadeGrupos(bool filtrar)
        {
            try
            {
                List<Pessoa_Grupo> PGs = await bdconn.Table<Pessoa_Grupo>().ToListAsync();
                List<Grupo> grupo = await bdconn.Table<Grupo>().ToListAsync();
                grupo = grupo.Where(x => PGs.Any(c => c.grupoID == x.ID)).ToList();
                listVGrupos.Items.Clear();
                listVPessoas.Items.Clear();
                //int length = await bdconn.Table<Pessoa_Grupo>().CountAsync();
                //var query = "SELECT P.ID, P.Nome FROM (Pessoa AS P INNER JOIN Pessoa_Grupo AS PG ON P.ID = PG.pessoaID) WHERE PG.grupoID =";
                if (filtrar)
                {                    
                    grupo = grupo.Where(x => x.Sexo == comboBox1.SelectedItem as string).ToList();
                }
                toolStripProgressBar1.Value = 0;
                toolStripProgressBar1.Minimum = 0;
                toolStripProgressBar1.Maximum = grupo.Count - 1;
                for (int i = 0; i < grupo.Count; i++)
                {
                    // var ps = await bdconn.QueryAsync<Pessoa>(query + grupo[i].ID);
                    toolStripProgressBar1.Value = i;
                    List<Pessoa> pf = await PegarPessoasDeTalGrupo(grupo[i].ID);

                    if (pf.Count == 0)
                    {
                        //continue;
                    }
                    id_grupo[i] = grupo[i].ID;
                    grupo[i].AddGeral(pf);
                    string pessoas = grupo[i].Descricao + " - ";
                    for (int j = 0; j < grupo[i].Membro.Count; j++)
                    {
                        pessoas += grupo[i].Membro[j].Nome.Split(' ')[0] + ", ";
                    }
                    if (!listVGrupos.Items.Contains(new ListViewItem(pessoas)))
                    {
                        listVGrupos.Items.Add(pessoas);
                    }

                }
            }
            catch (Exception)
            {
                
            }
            
            
        }
        public async Task<List<Pessoa>> PegarPessoasDeTalGrupo(int id)
        {
            List<Pessoa_Grupo> PGs = await bdconn.Table<Pessoa_Grupo>().ToListAsync();
            var filtrado = PGs.Where(pg => pg.grupoID == id);
            List<Pessoa> pf = new List<Pessoa>();
            foreach (var item in filtrado)
            {
                pf.Add(await bdconn.Table<Pessoa>().Where(p => p.ID == item.pessoaID).FirstOrDefaultAsync());
            }

            return pf;
        } 

        
        private async void listVGrupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listVGrupos.InvokeRequired)
                {

                    listVGrupos.Invoke((MethodInvoker)async delegate ()
                    {
                        await PreencheListVPessoas(sender);
                        await PreencheCmbsFaci(sender);
                    });
                }
                else
                {
                    await PreencheListVPessoas(sender);
                    await PreencheCmbsFaci(sender);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                                   
        }

        private async Task PreencheListVPessoas(object sender)
        {
            var lista = sender as ListView;
            listVPessoas.Items.Clear();
            if (lista.SelectedIndices.Count == 0)
            {
                return;
            }
            // var grupo = await bdconn.Table<Grupo>().Where(x => x.ID == id_grupo[lista.SelectedIndices[0]]).FirstOrDefaultAsync();
            List<Pessoa> pf = await PegarPessoasDeTalGrupo(id_grupo[lista.SelectedIndices[0]]);
            int i = 0;
            
            foreach (var p in pf)
            {
                id_pessoa[i] = p.ID;
                var item = new ListViewItem(p.Nome);
                item.SubItems.Add(p.Idade.ToString());
                listVPessoas.Items.Add(item);
                i++;              
            }

            lblqtdP.Text = "Quantidade de pessoas: " + i;
        }
        
        private async Task PreencheCmbsFaci(object sender)
        {
            try
            {
                var lista = sender as ListView;
                cmbFac.Items.Clear(); cmbFacAux.Items.Clear();
                if (lista.SelectedIndices.Count == 0)
                {
                    return;
                }
                int id = id_grupo[lista.SelectedIndices[0]];
                string sexo =  bdconn.FindAsync<Grupo>(x => x.ID == id).Result.Sexo;
                int fac = bdconn.FindAsync<Grupo>(x => x.ID == id).Result.facilitadorid;
                int faca = bdconn.FindAsync<Grupo>(x => x.ID == id).Result.facilitadorauxid;
                var facs = await bdconn.Table<Facilitador>().Where(x => x.Sexo == sexo).ToListAsync();
                int i = 0;
                foreach (var item in facs)
                {
                    cmbFac.Items.Add(item.Nome);
                    cmbFacAux.Items.Add(item.Nome);
                    id_fac[i] = item.ID;
                    id_facaux[i] = item.ID;
                    if (item.ID == faca)
                    {
                        cmbFacAux.SelectedIndex = i;
                    }
                    if (item.ID == fac)
                    {
                        cmbFac.SelectedIndex = i;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 2)
            {
                await AtualizarListadeGrupos(true);
            }
            else
            {
                await AtualizarListadeGrupos(false);
            }
        }

        private async void btExportar_Click(object sender, EventArgs e)
        {
            List<Pessoa_Grupo> PGs = await bdconn.Table<Pessoa_Grupo>().ToListAsync();
            List<Grupo> grupo = await bdconn.Table<Grupo>().ToListAsync();
            grupo = grupo.Where(x => PGs.Any(c => c.grupoID == x.ID)).ToList();
            PGs = null;
            string path = Application.StartupPath + "\\grupos\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (int i = 0; i < grupo.Count; i++)
            {
                try
                {
                    var pf = await PegarPessoasDeTalGrupo(grupo[i].ID);
                    if (pf.Count == 0)
                    {
                        continue;
                    }
                    var pfnova = pf.Select(x => new {x.Nome}).ToList();
                    int idfac = grupo[i].facilitadorid;
                    Facilitador facilitador = null;
                    Facilitador facilitadoraux = null;
                    try
                    {
                        facilitador = await bdconn.FindAsync<Facilitador>(x => x.ID == idfac);
                        idfac = grupo[i].facilitadorauxid;
                        facilitadoraux = await bdconn.FindAsync<Facilitador>(x => x.ID == idfac);
                    }
                    catch (Exception)
                    {

                    }
                    
                    string file = path + (i + 1) + " - " + grupo[i].Descricao + ".csv";
                    if (!File.Exists(file))
                    {
                        File.Create(file).Close();
                    }
                    
                    using (var st = new StreamWriter(file))
                    {
                        st.WriteLine(string.Format("Facilitador(a): {0}", facilitador == null ? "" : facilitador.Nome));
                        st.WriteLine(string.Format("Facilitador(a) auxiliar: {0}", facilitadoraux == null ? "" : facilitadoraux.Nome));
                        var csvW = new CsvHelper.CsvWriter(st);
                        csvW.WriteRecords(pfnova);                        
                        st.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
            var resposta = MessageBox.Show("Deseja visitar a pasta de onde estão todos os arquivos?", "Exportação bem-sucedida!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resposta == DialogResult.Yes)
            {
                Process.Start(path);
            }
        }

        private async void listVPessoas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listVPessoas.InvokeRequired)
            {
                listVPessoas.Invoke((MethodInvoker)async delegate ()
                {
                    if (listVPessoas.SelectedIndices.Count == 0)
                    {
                        groupBox1.Enabled = false;
                    }
                    else
                    {
                        await PreenchecmmGrupos();
                        groupBox1.Enabled = true;

                    }

                });

            }
            else
            {
                if (listVPessoas.SelectedIndices.Count == 0)
                {
                    groupBox1.Enabled = false;
                }
                else
                {
                    await PreenchecmmGrupos();
                    groupBox1.Enabled = true;
                }
            }
        }
        private int[] id_cmbG = new int[100];
        public async Task PreenchecmmGrupos()
        {
            try
            {
                var index = id_grupo[GetIndexlvG()];
                var g = await bdconn.Table<Grupo>().Where(x => x.ID == index).FirstOrDefaultAsync();
                var ip = id_pessoa[listVPessoas.SelectedIndices[0]];
                var p = await bdconn.Table<Pessoa>().Where(x => x.ID == ip).FirstOrDefaultAsync();
                var lista = await bdconn.Table<Grupo>().Where(x => x.ID != g.ID && x.Sexo == g.Sexo && x.ehEspecial == g.ehEspecial).ToListAsync();
                if (p.Idade < 18)
                {
                    lista = lista.Where(x => x.IdadeMax <= 18).ToList();
                }
                else
                {
                    lista = lista.Where(x => x.IdadeMax == 100).ToList();
                }
                cmBGrupos.Items.Clear();
                int i = 0;
                foreach (var item in lista)
                {
                    List<Pessoa> pf = await PegarPessoasDeTalGrupo(item.ID);

                    if (pf.Count == 0)
                    {
                        continue;
                    }

                    item.AddGeral(pf);
                    string pessoas = "";
                    for (int j = 0; j < item.Membro.Count; j++)
                    {
                        pessoas += item.Membro[j].Nome.Split(' ')[0] + ", ";
                    }
                    id_cmbG[i] = item.ID;
                    i++;
                    cmBGrupos.Items.Add(pessoas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private int GetIndexlvG()
        {
            int index = 0;
            if (listVGrupos.InvokeRequired)
            {
               listVGrupos.Invoke((MethodInvoker)delegate ()
               {
                   index = listVGrupos.SelectedIndices[0];
               });
            } else
            {
                index = listVGrupos.SelectedIndices[0];
            }
            return index;
        }

        private async void btAplicarTroca_Click(object sender, EventArgs e)
        {
            if (cmBGrupos.SelectedIndex != -1)
            {
                for (int i = 0; i < listVPessoas.SelectedIndices.Count; i++)
                {
                    var index = id_pessoa[listVPessoas.SelectedIndices[i]];
                    var pg = await bdconn.FindAsync<Pessoa_Grupo>(x => x.pessoaID == index);                    
                    pg.grupoID = id_cmbG[cmBGrupos.SelectedIndex];
                    int r = await bdconn.UpdateAsync(pg);
                    if (r == 1)
                    {
                        MessageBox.Show("Foi alterado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                await AtualizarListadeGrupos(false);
                cmBGrupos.Items.Clear();
            }
        }

        private async void btAlocarAuto_Click(object sender, EventArgs e)
        {
            var resp = MessageBox.Show("Tem certeza?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resp == DialogResult.Yes)
            {
                listVGrupos.Items.Clear();
                listVPessoas.Items.Clear();
                cmBGrupos.Items.Clear();
                lblqtdP.Text = "Quantidade de pessoas: ";
                await bdconn.DropTableAsync<Grupo>();
                await bdconn.DropTableAsync<Pessoa_Grupo>();
                await AlocarAutomaticamente();
            }
        }

        private async void cmbFac_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                int id = id_grupo[GetIndexlvG()];
                var grupo = await bdconn.FindAsync<Grupo>(x => x.ID == id);
                if(!cmb.Name.Contains("Aux"))
                    grupo.setFacilitador(id_fac[cmb.SelectedIndex].ToString());
                else
                    grupo.setFacilitadorAuxiliar(id_facaux[cmb.SelectedIndex].ToString());
                await bdconn.UpdateAsync(grupo);
            }
            catch (Exception)
            {

            }
        }

        
    }
}
