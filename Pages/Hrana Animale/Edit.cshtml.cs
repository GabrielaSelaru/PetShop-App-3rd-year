using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectBD.Pages.Animale;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace ProiectBD.Pages.Hrana_Animale
{
    public class EditModel : PageModel
    {
        public HranaInfo hranaInfo = new HranaInfo();
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
                    String sql = "SELECT * FROM HranaAnimale WHERE IDHrana=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hranaInfo.id = "" + reader.GetInt32(0);
                                hranaInfo.denumire = reader.GetString(1);
                                hranaInfo.categorie = reader.GetString(2);
                                hranaInfo.specie = reader.GetString(3);
                                hranaInfo.pret = "" + reader.GetInt32(4);
                                hranaInfo.stoc = "" + reader.GetInt32(5);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                errorMessage = ex.Message;

            }
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
                        String sql = "UPDATE HranaAnimale SET SpecieAnimal=@specie, denumire=@denumire, categorie=@categorie," +
                                    "[Pret/Kg]=@pret, StocDisponibil=@stoc " +
                                    "WHERE IDHrana=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@Specie", hranaInfo.specie);
                            command.Parameters.AddWithValue("@Denumire", hranaInfo.denumire);
                            command.Parameters.AddWithValue("@Categorie", hranaInfo.categorie);
                            command.Parameters.AddWithValue("@Pret", hranaInfo.pret);
                            command.Parameters.AddWithValue("@Stoc", hranaInfo.stoc);
                            command.Parameters.AddWithValue("@id", hranaInfo.id);

                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }

                Response.Redirect("/Hrana Animale/Hranacshtml");
            }
        }
    
}

