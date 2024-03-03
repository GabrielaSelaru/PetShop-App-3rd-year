using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Detalii_Comenzi
{
    public class DetaliiModel : PageModel
    {
        public List<DetaliiInfo> listDetalii = new List<DetaliiInfo>();
        int valNull = 0;
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM DetaliiComanda";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DetaliiInfo detaliiInfo = new DetaliiInfo();
                                detaliiInfo.id = "" + reader.GetInt32(0);
                                detaliiInfo.idComanda = (reader.IsDBNull(1) ? "-" : reader[1].ToString());
                                detaliiInfo.idAnimal = (reader.IsDBNull(2) ? "-" : reader[2].ToString());
                                detaliiInfo.idHrana = (reader.IsDBNull(3) ? "-" : reader[3].ToString());
                                detaliiInfo.cantitateA = (reader.IsDBNull(4) ? "0" : reader[4].ToString());
                                detaliiInfo.cantitateH = (reader.IsDBNull(5) ? "0" : reader[5].ToString());

                                listDetalii.Add(detaliiInfo);
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

    public class DetaliiInfo
    {
        public String id;
        public String idComanda;
        public String idAnimal;
        public String idHrana;
        public String cantitateA;
        public String cantitateH;
    }
}
