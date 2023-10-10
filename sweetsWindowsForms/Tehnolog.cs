using sweetsWindowsForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sweetsWindowsForms
{
    public partial class Tehnolog : Form
    {
        private int selectedID = 0;

        public Tehnolog()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (Models.TEHNOLOG teh in Models.TEHNOLOG.GetTehno())
                {
                    dataGridView1.Rows.Add(teh.ID.ToString(), teh.tehnos, teh.opis);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблеми з базою даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkEmpty()
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
     
        private void button2_Click(object sender, EventArgs e)
        {
            String searchValue = textBox3.Text.ToLower();
            int[] cols = { 1 };
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

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!Models.TEHNOLOG.InsertTeh(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Не вдалося зберегти дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadData();
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
                if (!Models.TEHNOLOG.UpdateTeh(selectedID, textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Не вдалося редагувати дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Models.TEHNOLOG.DeleteTeh(selectedID))
            {
                MessageBox.Show("Не вдалося видалити дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
            textBox3.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            selectedID = int.Parse(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[selectedRow].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
