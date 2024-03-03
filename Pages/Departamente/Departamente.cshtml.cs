using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Departamente
{
    public class DepartamenteModel : PageModel
    {
        public List<DepartamentInfo> listDepartament = new List<DepartamentInfo>();
        public void OnGet()
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Departamente";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DepartamentInfo departamentInfo = new DepartamentInfo();
                                departamentInfo.id = "" + reader.GetInt32(0);
                                departamentInfo.nume = reader.GetString(1);

                                listDepartament.Add(departamentInfo);
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

    public class DepartamentInfo
    {
        public String id;
        public String nume;
      
    }
}



