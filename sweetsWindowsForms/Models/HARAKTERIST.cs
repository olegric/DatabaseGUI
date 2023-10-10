using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweetsWindowsForms.Models
{
    class HARAKTERIST
    {
        //поля класу
        public int ID;
        public int proek_id;
        public String rozdil;
        public String lamp;
        public String resurs;
        public String iaskravist;
        public String kontras;
        public String dodatkovo;

        public HARAKTERIST() //порожній конструктор
        {
            this.ID = 0;
            this.proek_id = 0;
            this.rozdil = "";
            this.lamp = "";
            this.resurs = "";
            this.iaskravist = "";
            this.kontras = "";
            this.dodatkovo = "";

        }
        //конструктор з параметрами
        public HARAKTERIST(int ID, int proek_id,  String rozdil, String lamp, String resurs, String iaskravist, String kontras, String dodatkovo)
        {
            this.ID = ID;
            this.proek_id = proek_id;
            this.rozdil = rozdil;
            this.lamp = lamp;
            this.resurs = resurs;
            this.iaskravist = iaskravist;
            this.kontras = kontras;
            this.dodatkovo = dodatkovo;
        }
        static public HARAKTERIST GetHarakteristik(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Harakteristik WHERE ID = " + ID.ToString() + ";");
            HARAKTERIST harak = new HARAKTERIST(0, 0, "", "", "", "", "", "");
            while (reader.Read())
            {
                harak = new HARAKTERIST(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToInt32(reader["proek_id"]),
                        System.Convert.ToString(reader["rozdil"]),
                        System.Convert.ToString(reader["lamp"]),
                        System.Convert.ToString(reader["resurs"]),
                        System.Convert.ToString(reader["iaskravist"]),
                        System.Convert.ToString(reader["kontras"]),
                        System.Convert.ToString(reader["dodatkovo"])
                    );
            }
            reader.Close();
            DB.Close();
            return harak;
        }
        static public List<HARAKTERIST> GetHarakteristikWhere(String where)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Harakteristik WHERE " + where + ";");
            List<HARAKTERIST> harak = new List<HARAKTERIST>();
            while (reader.Read())
            {
                harak.Add(new HARAKTERIST(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToInt32(reader["proek_id"]),
                        System.Convert.ToString(reader["rozdil"]),
                        System.Convert.ToString(reader["lamp"]),
                        System.Convert.ToString(reader["resurs"]),
                        System.Convert.ToString(reader["iaskravist"]),
                        System.Convert.ToString(reader["kontras"]),
                        System.Convert.ToString(reader["dodatkovo"])
                    ));
            }
            reader.Close();
            DB.Close();
            return harak;
        }
        static public List<HARAKTERIST> GetHarakteristiks()
        { // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            var reader = DB.GetReader("SELECT * FROM Harakteristik;"); //змінна для читання запиту
            List<HARAKTERIST> harak = new List<HARAKTERIST>(); //оголошення спису 
            while (reader.Read()) //цикл зчитування даних
            {   //відображення рядків таблиці БД в таблиці на формі
                harak.Add(new HARAKTERIST(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToInt32(reader["proek_id"]),
                        System.Convert.ToString(reader["rozdil"]),
                        System.Convert.ToString(reader["lamp"]),
                        System.Convert.ToString(reader["resurs"]),
                        System.Convert.ToString(reader["iaskravist"]),
                        System.Convert.ToString(reader["kontras"]),
                        System.Convert.ToString(reader["dodatkovo"])
                    ));
            }
            reader.Close(); //закрити читання запиту
            DB.Close(); //закрити з'єднання з БД 
            return harak;
        }

        static public bool InsertHarakteristik(int proek_id, String rozdil, String lamp, String resurs, String iaskravist, String kontras, String dodatkovo)
        { // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();//відкрити з'єднання
            //змінна для читання запиту
            bool result = DB.RunCommand("INSERT INTO Harakteristik( proek_id, rozdil,  lamp,  resurs, iaskravist, kontras, dodatkovo) VALUES( " + proek_id.ToString() + ", N'" + rozdil + "', N'" + lamp + "', N'" + resurs + "', N'" + iaskravist + "', N'" + kontras + "', N'" + dodatkovo + "');");
            
            DB.Close();//закрити з'єднання з БД 
            return result;
        }
        static public bool DeleteHarakteristik(int ID)
        {   // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            //змінна для читання запиту
            bool result = DB.RunCommand("DELETE FROM Harakteristik WHERE ID = " + ID.ToString() + ";");
            DB.Close(); //закрити з'єднання з БД 
            return result;
        }

        static public bool UpdateHarakteristik(int ID, int proek_id, String rozdil, String lamp, String resurs, String iaskravist, String kontras, String dodatkovo)
        {   // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            //змінна для читання запиту
            bool result = DB.RunCommand("UPDATE Harakteristik SET  proek_id=" + proek_id.ToString() + ", rozdil=N'" + rozdil + "', lamp= N'" + lamp + "', resurs=N'" + resurs + "', iaskravist = N'" + iaskravist + "', kontras = N'" + kontras + "', dodatkovo = N'" + dodatkovo + "' WHERE ID =" + ID.ToString() + ";");
            
            DB.Close();//закрити з'єднання з БД 
            return result;
        }
    }
}

   
