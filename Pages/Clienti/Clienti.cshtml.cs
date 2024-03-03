using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Clienti
{
    public class ClientiModel : PageModel
    {
        public List<ClientiInfo> listClienti = new List<ClientiInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Clienti";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientiInfo clientiInfo = new ClientiInfo();
                                clientiInfo.id = "" + reader.GetInt32(0);
                                clientiInfo.nume = reader.GetString(1);
                                clientiInfo.prenume = reader.GetString(2);
                                clientiInfo.nrTelefon = reader.GetString(3);
                                clientiInfo.email = reader.GetString(4);

                                listClienti.Add(clientiInfo);
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

    public class ClientiInfo
    {
        public String id;
        public String nume;
        public String prenume;
        public String nrTelefon;
        public String email;
    }
}


