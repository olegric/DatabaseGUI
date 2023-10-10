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
    public partial class TovarForm : Form
    {
        private int selectedID = 0;
       
        public TovarForm()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dataGridView1.Rows.Clear();//очистити рядки таблиці
                //цикл завантаження даних в таблицю
                foreach (Models.TOVARS tovars in Models.TOVARS.GetTovars())
                {
                    Models.FABRIKS fabrik = Models.FABRIKS.GetFabrik(tovars.fabrikId);
                    Models.TEHNOLOG th = Models.TEHNOLOG.GetTeh(tovars.tehId);
                    //відображення даних в таблиці
                    dataGridView1.Rows.Add(tovars.ID, Additional.ImageByte.ByteToImage(tovars.Image), tovars.fabrikId, fabrik.nazva, tovars.nazva, tovars.cina, tovars.tehId, th.tehnos, tovars.opis);
                }
                comboBox1.Items.Clear();
                //цикл завантаження списку фірм в comboBox1
                foreach (Models.FABRIKS fabrik in Models.FABRIKS.GetFabriks())
                {
                    comboBox1.Items.Add(fabrik.nazva);
                }
                comboBox2.Items.Clear();
                //цикл завантаження списку технологій в comboBox2
                foreach (Models.TEHNOLOG th in Models.TEHNOLOG.GetTehno())
                {
                    comboBox2.Items.Add(th.tehnos);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Проблеми з базою даних", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool checkEmpty()//вибір даних зі списків
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //додати запис
        private void addBtn_Click(object sender, EventArgs e)
        {
            if (checkEmpty())
            {
                MessageBox.Show("Одне з полів не заповнене", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (Models.FABRIKS fabrik in Models.FABRIKS.GetFabriks())
                {
                    if (fabrik.nazva == comboBox1.SelectedItem.ToString())

                        foreach (Models.TEHNOLOG th in Models.TEHNOLOG.GetTehno())
                         {
                            if (th.tehnos == comboBox2.SelectedItem.ToString())

                                if (!Models.TOVARS.InsertTovar(Additional.ImageByte.ImageToByte(pictureBox1.BackgroundImage), fabrik.ID, textBox1.Text, int.Parse(textBox3.Text), th.ID, textBox5.Text))
                        {
                            MessageBox.Show("Не вдалося зберегти дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    }
                    LoadData();
                    break;
                }
                LoadData();
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
                foreach (Models.FABRIKS fabrik in Models.FABRIKS.GetFabriks())
                {
                    if (fabrik.nazva == comboBox1.SelectedItem.ToString())

                        foreach (Models.TEHNOLOG th in Models.TEHNOLOG.GetTehno())
                    {
                            if (th.tehnos == comboBox2.SelectedItem.ToString())

                                if (!Models.TOVARS.UpdateTovar(selectedID, Additional.ImageByte.ImageToByte(pictureBox1.BackgroundImage), fabrik.ID, textBox1.Text, int.Parse(textBox3.Text), th.ID, textBox5.Text))
                        {
                            MessageBox.Show("Не вдалося редагувати дані", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                         
                        //break;
                    }
                    LoadData();
                    //break;
                }
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Зображення(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = new System.Drawing.Bitmap(open.FileName);
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!Models.TOVARS.DeleteTovar(selectedID))
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

            pictureBox1.BackgroundImage = (System.Drawing.Image)dataGridView1.Rows[selectedRow].Cells[1].Value;
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dataGridView1.Rows[selectedRow].Cells[3].Value.ToString());
            textBox1.Text = dataGridView1.Rows[selectedRow].Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.Rows[selectedRow].Cells[5].Value.ToString();
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(dataGridView1.Rows[selectedRow].Cells[7].Value.ToString());
            textBox5.Text = dataGridView1.Rows[selectedRow].Cells[8].Value.ToString();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            String searchValue = textBox6.Text.ToLower();
            int[] cols = { 3, 4, 5, 6 };
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
    }
}