using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Animale
{
    public class CreateModel : PageModel
    {
        public AnimalInfo animalInfo = new AnimalInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
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

            if(animalInfo.id.Length == 0|| animalInfo.specie.Length == 0 || animalInfo.rasa.Length == 0 ||
                animalInfo.varsta.Length == 0 || animalInfo.sex.Length == 0 || animalInfo.pret.Length == 0 ||
                animalInfo.stoc.Length == 0)
            {
                errorMessage = "Sunt necesare toate datele!";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Animale" +
                                "(IDAnimal,specie,rasa,varsta,sex,pret,StocDisponibil) VALUES" +
                                "(@ID,@Specie,@Rasa,@Varsta,@Sex,@Pret,@Stoc);";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@ID",animalInfo.id);
                        command.Parameters.AddWithValue("@Specie", animalInfo.specie);
                        command.Parameters.AddWithValue("@Rasa", animalInfo.rasa);
                        command.Parameters.AddWithValue("@Varsta", animalInfo.varsta);
                        command.Parameters.AddWithValue("@Sex", animalInfo.sex);
                        command.Parameters.AddWithValue("@Pret", animalInfo.pret);
                        command.Parameters.AddWithValue("@Stoc", animalInfo.stoc);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            animalInfo.id = ""; animalInfo.specie = ""; animalInfo.rasa = "";
            animalInfo.varsta = ""; animalInfo.sex = ""; animalInfo.pret = "";
            animalInfo.stoc = "";
            successMessage = "Animal nou adaugat cu succes";
        }
    }
}
