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
    public partial class FirmaForm : Form
    {
        private int selectedID = 0;

        public FirmaForm()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (Models.FABRIKS fabrik in Models.FABRIKS.GetFabriks())
                {
                    dataGridView1.Rows.Add(fabrik.ID, Additional.ImageByte.ByteToImage(fabrik.image), fabrik.nazva, fabrik.misto, fabrik.adres, fabrik.phone, fabrik.web);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблеми з базою даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool checkEmpty()
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!Models.FABRIKS.InsertFabrik(Additional.ImageByte.ImageToByte(pictureBox1.BackgroundImage), textBox1.Text, textBox2.Text, textBox3.Text, maskedTextBox1.Text, textBox4.Text))
                {
                    MessageBox.Show("Не вдалося зберегти дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (!Models.FABRIKS.UpdateFabrik(selectedID, Additional.ImageByte.ImageToByte(pictureBox1.BackgroundImage), textBox1.Text, textBox2.Text, textBox3.Text, maskedTextBox1.Text, textBox4.Text))
                {
                    MessageBox.Show("Не вдалося редагувати дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    LoadData();
                }
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!Models.FABRIKS.DeleteFabrik(selectedID))
            {
                MessageBox.Show("Не вдалося видалити дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadData();
            }
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            String searchValue = textBox6.Text.ToLower();
            int[] cols = { 2,3, 4 };
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

        private void updateBtn_Click(object sender, EventArgs e)
        {
            LoadData();
            textBox6.Text = "";
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRow = dataGridView1.CurrentCell.RowIndex;
            selectedID = int.Parse(dataGridView1.Rows[selectedRow].Cells[0].Value.ToString());
            pictureBox1.BackgroundImage = (System.Drawing.Image)dataGridView1.Rows[selectedRow].Cells[1].Value;
            textBox1.Text = dataGridView1.Rows[selectedRow].Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.Rows[selectedRow].Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.Rows[selectedRow].Cells[4].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[selectedRow].Cells[5].Value.ToString();
            textBox4.Text = dataGridView1.Rows[selectedRow].Cells[6].Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {   // оголошення змінної для відкриття файлу зображення
            OpenFileDialog open = new OpenFileDialog();
            //філтр розширення файлу
            open.Filter = "Зображення(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
            if (open.ShowDialog() == DialogResult.OK) //якщо відкрито діалог
            {  //вибираємо картинку і завантажуємо в компонент pictureBox1
                pictureBox1.BackgroundImage = new System.Drawing.Bitmap(open.FileName);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
