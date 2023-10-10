using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweetsWindowsForms.Models
{
    class TOVARS
    {
        public int ID;
        public byte[] Image;
        public int fabrikId;
        public string nazva;
        public int cina;
        public int tehId;
        public string opis;

        public TOVARS()
        {
            this.ID = 0;
            this.Image = null;
            this.fabrikId = 0;
            this.nazva = "";
            this.cina = 0;
            this.tehId = 0;
            this.opis = "";
        }
        public TOVARS(int ID, byte[] Image, int fabrikId, string nazva, int cina, int tehId, string opis)
        {
            this.ID = ID;
            this.Image = Image;
            this.fabrikId = fabrikId;
            this.nazva = nazva;
            this.cina = cina;
            this.tehId = tehId;
            this.opis = opis;
        }
        static public TOVARS GetTovar(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Proektor WHERE ID = " + ID.ToString() + ";");
            TOVARS tovars = new TOVARS(0, null, 0, "", 0, 0, "");
            while (reader.Read())
            {
                tovars = new TOVARS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["foto"],
                        System.Convert.ToInt32(reader["fabrikId"]),
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToInt32(reader["cina"]),
                        System.Convert.ToInt32(reader["tehId"]),
                        System.Convert.ToString(reader["opis"])
                    );
            }
            reader.Close();
            DB.Close();
            return tovars;
        }
        static public List<TOVARS> GetTovarWhere(String where)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Proektor WHERE " + where + ";");
            List<TOVARS> tovars = new List<TOVARS>();
            while (reader.Read())
            {
                tovars.Add(new TOVARS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["foto"],
                        System.Convert.ToInt32(reader["fabrikId"]),
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToInt32(reader["cina"]),
                         System.Convert.ToInt32(reader["tehId"]),
                        System.Convert.ToString(reader["opis"])
                    ));
            }
            reader.Close();
            DB.Close();
            return tovars;
        }
        static public List<TOVARS> GetTovars()
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var reader = DB.GetReader("SELECT * FROM Proektor;");
            List<TOVARS> tovars = new List<TOVARS>();
            while (reader.Read())
            {
                tovars.Add(new TOVARS(
                        System.Convert.ToInt32(reader["ID"]),
                        (byte[])reader["foto"],
                        System.Convert.ToInt32(reader["fabrikId"]),
                        System.Convert.ToString(reader["nazva"]),
                        System.Convert.ToInt32(reader["cina"]),
                        System.Convert.ToInt32(reader["tehId"]),
                        System.Convert.ToString(reader["opis"])
                    ));
            }
            reader.Close();
            DB.Close();
            return tovars;
        }
        static public bool InsertTovar(byte[] Image, int fabrikId, string nazva, int cina, int tehId,  string opis)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var cmd = DB.Command("INSERT INTO Proektor(foto, fabrikId, nazva, cina, tehId, opis) VALUES(@img, " + fabrikId.ToString() + ", N'" + nazva + "', " + cina.ToString() + ",  " + tehId.ToString() + ",  N'" + opis + "');");
            cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = Image;
            bool result = cmd.ExecuteNonQuery() > 0;
            DB.Close();
            return result;
        }
        static public bool DeleteTovar(int ID)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            bool result = DB.RunCommand("DELETE FROM Proektor WHERE ID = " + ID.ToString() + ";");
            DB.Close();
            return result;
        }
        static public bool UpdateTovar(int ID, byte[] Image, int fabrikId, string nazva, int cina, int tehId, string opis)
        {
            Additional.DBConnection DB = new Additional.DBConnection();
            DB.Open();
            var cmd = DB.Command("UPDATE Proektor SET foto=@img, fabrikId = " + fabrikId.ToString() + ", nazva = N'" + nazva + "', cina = N'" + cina.ToString() + "',  tehId = " + tehId.ToString() + ",  opis = N'" + opis + "'  WHERE ID =" + ID.ToString() + ";");
            cmd.Parameters.Add("@img", System.Data.SqlDbType.Image).Value = Image;
            bool result = cmd.ExecuteNonQuery() > 0;
            DB.Close();
            return result;
        }
    }
}
