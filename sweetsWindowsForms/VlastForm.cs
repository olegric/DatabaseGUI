using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sweetsWindowsForms
{
    public partial class VlastForm : Form
    {
        private int selectedID = 0;

        public VlastForm()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (Models.HARAKTERIST har in Models.HARAKTERIST.GetHarakteristiks())
                {
                    Models.TOVARS tovars = Models.TOVARS.GetTovar(har.proek_id);

                    dataGridView1.Rows.Add(har.ID, har.proek_id, tovars.nazva, har.rozdil, har.lamp, har.resurs, har.iaskravist, har.kontras, har.dodatkovo);
                }
                comboBox1.Items.Clear();
                foreach (Models.TOVARS tovars in Models.TOVARS.GetTovars())
                {
                    comboBox1.Items.Add(tovars.nazva);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Проблеми з базою даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool checkEmpty()
        {
            if (comboBox1.SelectedItem == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (Models.TOVARS tovars in Models.TOVARS.GetTovars())
                {
                    if (tovars.nazva == comboBox1.SelectedItem.ToString())
                    {
                        if (!Models.HARAKTERIST.InsertHarakteristik(tovars.ID, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text))
                        {
                            MessageBox.Show("Не вдалося зберегти дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            LoadData();
                        }

                    break;
                    }
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (Models.TOVARS tovars in Models.TOVARS.GetTovars())
                {
                    if
                        (!Models.HARAKTERIST.UpdateHarakteristik(selectedID, tovars.ID, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text))

                    {
                        MessageBox.Show("Не вдалося редагувати дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                LoadData();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Models.HARAKTERIST.DeleteHarakteristik(selectedID))
            {
                MessageBox.Show("Не вдалося видалити дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadData();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            selectedID = int.Parse(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString());
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[selectedRow].Cells[2].Value.ToString());
            textBox1.Text = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.Rows[selectedRow].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[selectedRow].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[selectedRow].Cells[6].Value.ToString();
            textBox5.Text = dataGridView1.Rows[selectedRow].Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.Rows[selectedRow].Cells[8].Value.ToString();
            // textBox6.Text = dataGridView1.Rows[selectedRow].Cells[8].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String searchValue = textBox8.Text.ToLower();
            int[] cols = { 2 };
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < cols.Length; i++)
                {
                    if (row.Cells[cols[i]].Value != null && row.Cells[cols[i]].Value.ToString().ToLower().Contains(searchValue))
                    {
                        int rowIndex = row.Index;
                        dataGridView1.Rows[rowIndex].Selected = true;
                        break;
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
            textBox8.Text = "";
        }
    }
}


