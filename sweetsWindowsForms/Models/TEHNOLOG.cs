using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweetsWindowsForms.Models
{
    class TEHNOLOG
    {
        public int ID;
        public String tehnos;
        public String opis;

        public TEHNOLOG()
        {
            this.ID = 0;
            this.tehnos = "";
            this.opis = "";
        }
        public TEHNOLOG(int ID, String tehnos, String opis)
        {
            this.ID = ID;
            this.tehnos = tehnos;
            this.opis = opis;

        }
        static public TEHNOLOG GetTeh(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Teh WHERE ID = " + ID.ToString() + ";");
            TEHNOLOG tehnos = new TEHNOLOG(0, "", "");
            while (reader.Read())
            {
                tehnos = new TEHNOLOG(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToString(reader["tehnologia"]),
                        System.Convert.ToString(reader["opus"])
                    );
            }
            reader.Close();
            DB.Close();
            return tehnos;
        }
        static public List<TEHNOLOG> GetTehWhere(String where)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Teh WHERE " + where + ";");
            List<TEHNOLOG> tehnos = new List<TEHNOLOG>();
            while (reader.Read())
            {
                tehnos.Add(new TEHNOLOG(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToString(reader["tehnologia"]),
                        System.Convert.ToString(reader["opus"])
                    ));
            }
            reader.Close();
            DB.Close();
            return tehnos;
        }
        static public List<TEHNOLOG> GetTehno()
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Teh;");
            List<TEHNOLOG> tehnos = new List<TEHNOLOG>();
            while (reader.Read())
            {
                tehnos.Add(new TEHNOLOG(
                        System.Convert.ToInt32(reader["ID"]),
                        System.Convert.ToString(reader["tehnologia"]),
                        System.Convert.ToString(reader["opus"])
                    ));
            }
            reader.Close();
            DB.Close();
            return tehnos;
        }
        static public bool InsertTeh(String tehnos, String opis)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            bool result = DB.RunCommand("INSERT INTO Teh(tehnologia, opus) VALUES(N'" + tehnos + "', N'" + opis + "');");
            DB.Close();
            return result;
        }
        static public bool DeleteTeh(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            bool result = DB.RunCommand("DELETE FROM Teh WHERE ID = " + ID.ToString() + ";");
            DB.Close();
            return result;
        }
        static public bool UpdateTeh(int ID, String tehnos, String opis)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            bool result = DB.RunCommand("UPDATE Teh SET tehnologia= N'" + tehnos + "',  opus= N'" + opis + "' WHERE ID =" + ID.ToString() + ";");
            DB.Close();
            return result;
        }
    }
}
