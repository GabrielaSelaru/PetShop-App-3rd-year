using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ProiectBD.Pages.Animale
{
    public class InformatiiModel : PageModel
    {
        public List<Interogare1>interogare1 = new List<Interogare1>();
        public List<Interogare2>interogare2 = new List<Interogare2>();
        public List<Interogare3>interogare3 = new List<Interogare3>();
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


                            String sql1 = "SELECT A.IDComanda, B.Nume AS NumeClient, B.Prenume AS PrenumeClient," +
                                "(SELECT COUNT(CantitateA) FROM DetaliiComanda " +
                                "WHERE IDComanda=A.IDComanda) AS NumarAnimale " +
                                "FROM Comanda A, Clienti B " +
                                "WHERE A.IDClient=B.IDClient";
                            using (SqlCommand command = new SqlCommand(sql1, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare1 interogare11 = new Interogare1();
                                        interogare11.id = "" + reader.GetInt32(0);
                                        interogare11.nume = reader.GetString(1);
                                        interogare11.prenume = reader.GetString(2);
                                        interogare11.nr = "" + reader.GetInt32(3);
                                        interogare1.Add(interogare11);
                                    }
                                }
                            }
                            break;

                        case "actiune2":

                            String sql2 = "SELECT a.IDAnimal, a.Specie, A.Rasa " +
                                "FROM Animale a, DetaliiComanda b " +
                                "WHERE a.IDAnimal=b.IDAnimal " +
                                "GROUP BY A.Specie, A.Rasa, a.IDAnimal " +
                                "HAVING COUNT(b.IDAnimal) >= ALL " +
                                "(select COUNT(IDAnimal) as NumarAnimale " +
                                "FROM DetaliiComanda " +
                                "GROUP BY IDAnimal)";

                            using (SqlCommand command = new SqlCommand(sql2, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare2 interogare22 = new Interogare2();
                                        interogare22.id = "" + reader.GetInt32(0);
                                        interogare22.specie = reader.GetString(1);
                                        interogare22.rasa = reader.GetString(2);
                                        
                                        interogare2.Add(interogare22);
                                    }
                                }
                            }
                            break;

                        case "actiune3":

                            String sql3 = "select a.IDAnimal, B.Specie, B.Rasa, COUNT(a.IDAnimal) " +
                                "FROM DetaliiComanda a, Animale b " +
                                "WHERE A.IDAnimal=B.IDAnimal " +
                                "GROUP BY a.IDAnimal,b.Specie,b.Rasa " +
                                "order by COUNT(a.IDAnimal) DESC";

                            using (SqlCommand command = new SqlCommand(sql3, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare3 interogare33 = new Interogare3();
                                        interogare33.id = "" + reader.GetInt32(0);
                                        interogare33.specie = reader.GetString(1);
                                        interogare33.rasa = reader.GetString(2);
                                        interogare33.nr="" + reader.GetInt32(3);
                                        interogare3.Add(interogare33);
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
            public String id;
            public String nume;
            public String prenume;
            public String nr;
        }
    }
        
        public class Interogare2
    {
        public String id;
        public String specie;
        public String rasa;
    }
}
        public class Interogare3
    {
         public String id;
         public String specie;
         public String rasa;
         public String nr;
    }