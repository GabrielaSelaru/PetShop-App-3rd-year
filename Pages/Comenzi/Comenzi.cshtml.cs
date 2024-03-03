using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Comenzi
{
    public class ComenziModel : PageModel
    {
       //public string interogare1 { get; set; }

        

        public List<ComenziInfo> listComenzi = new List<ComenziInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Comanda";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ComenziInfo comenziInfo = new ComenziInfo();
                                comenziInfo.id = "" + reader.GetInt32(0);
                                comenziInfo.idClient = "" + reader.GetInt32(1);
                                comenziInfo.idAngajat = (reader.IsDBNull(2) ? "-" : reader[2].ToString());
                                comenziInfo.total = "" + reader.GetInt32(3);
                                listComenzi.Add(comenziInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void OnPost()
        {
        }   
    }

    public class ComenziInfo
    {
        public String id;
        public String idClient;
        public String idAngajat;
        public String total;
    }

   
}



