using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Animale
{
    public class EditModel : PageModel
    {
        public AnimalInfo animalInfo = new AnimalInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Animale WHERE IDAnimal=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                animalInfo.id = "" + reader.GetInt32(0);
                                animalInfo.specie = reader.GetString(1);
                                animalInfo.rasa = reader.GetString(2);
                                animalInfo.varsta = "" + reader.GetInt32(3);
                                animalInfo.sex = reader.GetString(4);
                                animalInfo.pret = "" + reader.GetInt32(5);
                                animalInfo.stoc = "" + reader.GetInt32(6);
                            }
                        }
                    }
                }
            }
        
    
            catch (Exception ex)
            {
                errorMessage=ex.Message;
            }
        }

        public void OnPost()
        {
            animalInfo.id = Request.Form["id"];
            animalInfo.specie = Request.Form["specie"];
            animalInfo.rasa = Request.Form["rasa"];
            animalInfo.varsta = Request.Form["varsta"];
            animalInfo.sex = Request.Form["sex"];
            animalInfo.pret = Request.Form["pret"];
            animalInfo.stoc = Request.Form["stoc"];

            if (animalInfo.id.Length == 0 || animalInfo.specie.Length == 0 || animalInfo.rasa.Length == 0 ||
                animalInfo.varsta.Length == 0 || animalInfo.sex.Length == 0 || animalInfo.pret.Length == 0 ||
                animalInfo.stoc.Length == 0)
            {
                errorMessage = "Sunt necesare toate datele!";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Animale SET Specie=@specie, rasa=@rasa, varsta=@varsta," +
                                "sex=@sex, pret=@pret, StocDisponibil=@stoc " +
                                "WHERE IDAnimal=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    { 
                        command.Parameters.AddWithValue("@Specie", animalInfo.specie);
                        command.Parameters.AddWithValue("@Rasa", animalInfo.rasa);
                        command.Parameters.AddWithValue("@Varsta", animalInfo.varsta);
                        command.Parameters.AddWithValue("@Sex", animalInfo.sex);
                        command.Parameters.AddWithValue("@Pret", animalInfo.pret);
                        command.Parameters.AddWithValue("@Stoc", animalInfo.stoc);
                        command.Parameters.AddWithValue("@id", animalInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Animale/Animale");
        }
    }
}
