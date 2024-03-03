using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace ProiectBD.Pages.Angajati
{
    public class InformatiiModel : PageModel
    {
        public string SelectedDepartment { get; set; }
        public List<Interogare1> interogare1 = new List<Interogare1>();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            SelectedDepartment = Request.Form["SelectedDepartment"];
            try
            {
                if (!string.IsNullOrWhiteSpace(SelectedDepartment))
                {
                    // Employees = ExecuteQueryForDepartment(SelectedDepartment);
                    interogare1 ??= new List<Interogare1>();
                }

                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection = new SqlConnection(ConnectionStrings))
                {
                    connection.Open();
                    // command.Parameters.AddWithValue("@SelectedDepartment", SelectedDepartment);
                    String sql = "SELECT  a.Nume ,a.Prenume, b.NumeDepartament " +
                        "FROM Angajati a, Departamente b " +
                        "WHERE b.IDDepartament=a.IDDepartament " +
                        "AND b.NumeDepartament=@SelectedDepartment";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@SelectedDepartment", SelectedDepartment);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // List<EmployeeViewModel> employee = new List<EmployeeViewModel>();

                            while (reader.Read())
                            {
                                Interogare1 interogare11 = new Interogare1();
                                interogare11.Nume = reader["Nume"].ToString();
                                interogare11.Prenume = reader["Prenume"].ToString();
                                interogare11.NumeDepartament = reader["NumeDepartament"].ToString();
                                interogare1.Add(interogare11);
                            }

                        }
                    }
                }
            }


            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
public class Interogare1
{
    public string Nume { get; set; }
    public string Prenume { get; set; }
    public string NumeDepartament { get; set; }
}

