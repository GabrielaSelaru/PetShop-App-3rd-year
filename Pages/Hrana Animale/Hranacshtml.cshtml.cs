using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Hrana_Animale
{
    public class HranacshtmlModel : PageModel
    {
        public List<HranaInfo> listHrana = new List<HranaInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM HranaAnimale";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HranaInfo hranaInfo = new HranaInfo();
                                hranaInfo.id = "" + reader.GetInt32(0);
                                hranaInfo.denumire = reader.GetString(1);
                                hranaInfo.categorie = reader.GetString(2);
                                hranaInfo.specie = reader.GetString(3);
                                hranaInfo.pret = "" + reader.GetInt32(4);
                                hranaInfo.stoc = "" + reader.GetInt32(5);

                                listHrana.Add(hranaInfo);
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
    }

    public class HranaInfo
    {
        public String id;
        public String denumire;
        public String categorie;
        public String specie;
        public String pret;
        public String stoc;
    }
}
