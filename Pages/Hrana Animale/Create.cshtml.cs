using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Hrana_Animale
{
    public class CreateModel : PageModel
    {
        public HranaInfo hranaInfo = new HranaInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            hranaInfo.id = Request.Form["id"];
            hranaInfo.denumire = Request.Form["denumire"];
            hranaInfo.categorie = Request.Form["categorie"];
            hranaInfo.specie = Request.Form["specie"];
            hranaInfo.pret = Request.Form["pret"];
            hranaInfo.stoc = Request.Form["stoc"];

            if (hranaInfo.id.Length == 0 || hranaInfo.specie.Length == 0 || hranaInfo.denumire.Length == 0 ||
                hranaInfo.categorie.Length == 0 || hranaInfo.pret.Length == 0 ||
                hranaInfo.stoc.Length == 0)
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
                    String sql = "INSERT INTO HranaAnimale" +
                                "(IDHrana,Denumire,Categorie,SpecieAnimal,[Pret/Kg],StocDisponibil) VALUES" +
                                "(@ID,@Denumire,@Categorie,@Specie,@Pret,@Stoc);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", hranaInfo.id);
                        command.Parameters.AddWithValue("@Denumire", hranaInfo.denumire);
                        command.Parameters.AddWithValue("@Categorie", hranaInfo.categorie);
                        command.Parameters.AddWithValue("@Specie", hranaInfo.specie);
                        command.Parameters.AddWithValue("@Pret", hranaInfo.pret);
                        command.Parameters.AddWithValue("@Stoc", hranaInfo.stoc);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            hranaInfo.id = ""; hranaInfo.specie = ""; hranaInfo.denumire = "";
            hranaInfo.categorie = ""; hranaInfo.pret = "";
            hranaInfo.stoc = "";
            successMessage = "Hrana animal noua adaugata cu succes";
        }
    }
}
