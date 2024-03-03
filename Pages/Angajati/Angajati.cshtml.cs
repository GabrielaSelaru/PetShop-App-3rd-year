using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Angajati
{
    public class AngajatiModel : PageModel { 
    
        public List<AngajatiInfo> listAngajati = new List<AngajatiInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Angajati";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AngajatiInfo angajatiInfo = new AngajatiInfo();
                                angajatiInfo.id = "" + reader.GetInt32(0);
                                angajatiInfo.nume = reader.GetString(1);
                                angajatiInfo.prenume = reader.GetString(2);
                                angajatiInfo.functie = reader.GetString(3);
                                angajatiInfo.idDepartament = "" + reader.GetInt32(4);
                                angajatiInfo.nrTelefon = reader.GetString(5);
                                angajatiInfo.email = reader.GetString(6);

                                listAngajati.Add(angajatiInfo);
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

    public class AngajatiInfo
    {
        public String id;
        public String nume;
        public String prenume;
        public String functie;
        public String idDepartament;
        public String nrTelefon;
        public String email;
    }
}


