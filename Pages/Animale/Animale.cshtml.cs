using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Animale
{
    public class AnimaleModel : PageModel
    {
        public List<AnimalInfo> listAnimal = new List<AnimalInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Animale";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                AnimalInfo animalInfo = new AnimalInfo();
                                animalInfo.id = "" + reader.GetInt32(0);
                                animalInfo.specie = reader.GetString(1);
                                animalInfo.rasa = reader.GetString(2);
                                animalInfo.varsta = "" + reader.GetInt32(3);
                                animalInfo.sex = reader.GetString(4);
                                animalInfo.pret = "" + reader.GetInt32(5);
                                animalInfo.stoc = "" + reader.GetInt32(6);

                                listAnimal.Add(animalInfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class AnimalInfo 
    {
        public String id;
        public String specie;
        public String rasa;
        public String varsta;
        public String sex;
        public String pret;
        public String stoc;
    }
}
