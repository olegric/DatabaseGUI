using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweetsWindowsForms.Models
{
    class FABRIKS //оголошення класу
    {  //поля класу
        public int ID;
        public byte[] image;
        public String nazva;
        public String misto;
        public String adres;
        public String phone;
        public String web;

        public FABRIKS() //порожній конструктор
        {
            this.ID = 0;
            this.image = null;
            this.nazva = "";
            this.misto = "";
            this.adres = "";
            this.phone = "";
            this.web = "";
        }
        //конструктор з параметрами
        public FABRIKS(int ID, byte[] image, String nazva, String misto, String adres, String phone, String web)
        {
            this.ID = ID;
            this.image = image;
            this.nazva = nazva;
            this.misto = misto;
            this.adres = adres;
            this.phone = phone;
            this.web = web;
        }
        static public FABRIKS GetFabrik(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Fabrika WHERE ID = " + ID.ToString() + ";");
            FABRIKS fabriks = new FABRIKS(0, null, "", "", "", "", "");
            while (reader.Read())
            {
                fabriks = new FABRIKS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["logo"],
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToString(reader["misto"]),
                        System.Convert.ToString(reader["adres"]),
                        System.Convert.ToString(reader["telefon"]),
                        System.Convert.ToString(reader["web"])
                    );
            }
            reader.Close();
            DB.Close();
            return fabriks;
        }
        static public List<FABRIKS> GetFabrikWhere(String where)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Fabrika WHERE " + where + ";");
            List<FABRIKS> fabriks = new List<FABRIKS>();
            while (reader.Read())
            {
                fabriks.Add(new FABRIKS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["logo"],
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToString(reader["misto"]),
                        System.Convert.ToString(reader["adres"]),
                        System.Convert.ToString(reader["telefon"]),
                        System.Convert.ToString(reader["web"])
                    ));
            }
            reader.Close();
            DB.Close();
            return fabriks;
        }
        static public List<FABRIKS> GetFabriks()
        { // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            var reader = DB.GetReader("SELECT * FROM Fabrika;"); //змінна для читання запиту
            List<FABRIKS> fabriks = new List<FABRIKS>(); //оголошення спису 
            while (reader.Read()) //цикл зчитування даних
            {   //відображення рядків таблиці БД в таблиці на формі
                fabriks.Add(new FABRIKS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["logo"],
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToString(reader["misto"]),
                        System.Convert.ToString(reader["adres"]),
                        System.Convert.ToString(reader["telefon"]),
                        System.Convert.ToString(reader["web"])
                    ));
            }
            reader.Close(); //закрити читання запиту
            DB.Close(); //закрити з'єднання з БД 
            return fabriks;
        }

        static public bool InsertFabrik( byte[] image, String nazva, String misto, String adres, String phone, String web)
        { // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();//відкрити з'єднання
            //змінна для читання запиту
            var cmd = DB.Command("INSERT INTO Fabrika(logo,  nazva,  misto,  adres,  telefon,  web) VALUES(@img, N'" + nazva + "', N'" + misto + "', N'" + adres + "', '" + phone + "', '" + web + "');");
            //параметр для додавання картинки
            cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = image;
            bool result = cmd.ExecuteNonQuery() > 0;
            DB.Close();//закрити з'єднання з БД 
            return result;
        }
        static public bool DeleteFabrik(int ID)
        {   // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            //змінна для читання запиту
            bool result = DB.RunCommand("DELETE FROM Fabrika WHERE ID = " + ID.ToString() + ";");
            DB.Close(); //закрити з'єднання з БД 
            return result;
        }

        static public bool UpdateFabrik(int ID, byte[] image, String nazva, String misto, String adres, String phone, String web)
        {   // оголошення змінної для з'єднання з БД 
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open(); //відкрити з'єднання
            //змінна для читання запиту
            var cmd = DB.Command("UPDATE Fabrika SET logo=@img, nazva=N'" + nazva + "', misto=N'" + misto + "', adres=N'" + adres + "', telefon='" + phone + "', web=N'" + web + "' WHERE ID =" + ID.ToString() + ";");
            //параметр для додавання картинки
            cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = image;
            bool result = cmd.ExecuteNonQuery() > 0;
            DB.Close();//закрити з'єднання з БД 
            return result;
        }
    }
}
