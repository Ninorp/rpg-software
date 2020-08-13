using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using RpG_Software.Model;
using System.Diagnostics;
using SQLite;

namespace RpG_Software
{
    public partial class FormImporta : Form
    {
        SQLiteAsyncConnection bdconn;
        public FormImporta()
        {
            InitializeComponent();
            bdconn = new SQLiteAsyncConnection("bd_rpg.db");

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (TextReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    var csv = new CsvReader(sr);
                    csv.Configuration.RegisterClassMap<MapPessoa>();
                    List<Pessoa> pessoa = new List<Pessoa>();

                    csv.Read();
                    csv.ReadHeader();
                    Pessoa p;
                    try
                    {
                        await bdconn.CreateTableAsync<Pessoa>();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        
                    }
                    while (csv.Read())
                    {
                        try
                        {
                            p = new Pessoa(csv.Context.Record[0], csv.Context.Record[1], csv.Context.Record[2], csv.Context.Record[3], csv.Context.Record[4]
                            , Convert.ToInt16(csv.Context.Record[5]), csv.Context.Record[6], csv.Context.Record[7], csv.Context.Record[8], csv.Context.Record[9], 
                            csv.Context.Record[10], csv.Context.Record[11], csv.Context.Record[12]);
                            if (p.Idade > 6)
                            {
                                pessoa.Add(p);
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                        }                    
                        
                        
                    }
                    try
                    {
                        List<Pessoa> pStored = await bdconn.Table<Pessoa>().ToListAsync();
                        progressBar1.Minimum = progressBar1.Value = 0;
                        
                        progressBar1.Maximum = pessoa.Count - 1;
                        for (int i = 0; i < pessoa.Count; i++)
                        {
                            bool add = true;
                            for (int j = 0; j < pStored.Count; j++)
                            {
                                if (pStored[j].ID == pessoa[i].ID)
                                {
                                    add = false;
                                    break;
                                }
                            }
                            if (add)
                            {
                                await bdconn.InsertAsync(pessoa[i]);
                            }
                            progressBar1.Value = i;
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                       
                    }
                    dataGridView1.DataSource = await bdconn.Table<Pessoa>().ToListAsync();
                }
            }
        }

        

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = await bdconn.Table<Pessoa>().ToListAsync();
            }
            catch (Exception)
            {

            }
            
        }
    }
}
