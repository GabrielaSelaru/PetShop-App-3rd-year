using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Clienti
{
    public class InformatiiModel : PageModel
    {
        public List<Interogare1> interogare1 = new List<Interogare1>();
        public List<Interogare2> interogare2 = new List<Interogare2>();
        public void OnGet()
        {
        }

        public void OnPost(string actiune)
        {
            try
            {
                String ConnectionStrings = "Data Source=DESKTOP-UO9QS3B\\SQLEXPRESS;Initial Catalog=PetShop;Integrated Security=True;";

                using (SqlConnection connection1 = new SqlConnection(ConnectionStrings))
                {
                    connection1.Open();

                    switch (actiune)
                    {
                        case "actiune1":


                            String sql1 = "SELECT  a.Nume ,a.Prenume, COUNT(IDComanda) " +
                                "FROM Clienti a, Comanda b " +
                                "WHERE A.IDClient=B.IDClient " +
                                "GROUP BY A.Nume,A.Prenume";
                            using (SqlCommand command = new SqlCommand(sql1, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                       Interogare1 interogare11 = new Interogare1();
                                        interogare11.nume = reader.GetString(0);
                                        interogare11.prenume = reader.GetString(1);
                                        interogare11.nr= "" + reader.GetInt32(2);
                                        interogare1.Add(interogare11);
                                    }
                                }
                            }
                            break;

                        case "actiune2":


                            String sql2 = "SELECT A.Nume,a.Prenume, b.Total " +
                                "FROM Clienti A, Comanda B " +
                                "WHERE A.IDClient=B.IDClient and (SELECT MAX(Total) from Comanda)=b.Total";
                            using (SqlCommand command = new SqlCommand(sql2, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare2 interogare22 = new Interogare2();
                                        interogare22.nume = reader.GetString(0);
                                        interogare22.prenume = reader.GetString(1);
                                        interogare22.total = "" + reader.GetInt32(2);
                                        interogare2.Add(interogare22);
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
                

    public class Interogare1
        {
            public String nume;
            public String prenume;
            public String nr;
        }
    }

    public class Interogare2
    {
        public String nume;
        public String prenume;
        public String total;
    }
}
