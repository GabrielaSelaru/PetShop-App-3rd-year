using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProiectBD.Pages.Comenzi
{
    public class InformatiiModel : PageModel
    {
        public List<Interogare1> interogare1 = new List<Interogare1>();
        public List<Interogare2> interogare2 = new List<Interogare2>();
        public List<Interogare3> interogare3 = new List<Interogare3>();
        public List<Interogare4> interogare4 = new List<Interogare4>();
        public string hrana { get; set; }
        public string animal { get; set; }
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


                            String sql1 = "SELECT a.IDComanda, B.Nume, B.Prenume " +
                            "FROM Comanda a, Clienti B " +
                            "WHERE a.IDClient=b.IDClient " +
                            "AND a.IDAngajat=2";
                            using (SqlCommand command = new SqlCommand(sql1, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare1 interogare11 = new Interogare1();
                                        interogare11.id = "" + reader.GetInt32(0);
                                        interogare11.nume = reader["nume"].ToString();
                                        interogare11.prenume = reader["prenume"].ToString();
                                        interogare1.Add(interogare11);

                                    }
                                }
                            }
                            break;

                        case "actiune2":
                            String sql2 = "select a.IDComanda, c.Nume as NumeClient,c.Prenume as PrenumeClient " +
                                          "from DetaliiComanda a, Comanda b, Clienti c " +
                                          "WHERE A.IDComanda=B.IDComanda AND B.IDClient=C.IDClient " +
                                          "group by a.IDComanda, c.Nume, c.Prenume " +
                                          "HAVING SUM(a.CantitateA)>0 AND SUM(A.CantitateH) IS NULL";
                            using (SqlCommand command = new SqlCommand(sql2, connection1))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare2 interogare22 = new Interogare2();
                                        interogare22.id = "" + reader.GetInt32(0);
                                        interogare22.Nume = reader.GetString(1);
                                        interogare22.prenume = reader.GetString(2);
                                        interogare2.Add(interogare22);

                                    }
                                }
                            }
                            break;

                        case "actiune3":
                            hrana = Request.Form["hrana"];
                            String sql3;
                            if (hrana == "umeda")
                                sql3 = "select count(a.IDHrana)as 'Numar comenzi' " +
                                       "from DetaliiComanda a, HranaAnimale b " +
                                       "where a.IDHrana=b.IDHrana and b.Categorie='Hrana umeda'";
                            else sql3 = "select count(a.IDHrana)as 'Numar comenzi' " +
                                        "from DetaliiComanda a, HranaAnimale b " +
                                        "where a.IDHrana=b.IDHrana and b.Categorie='Hrana uscata'";
                             using (SqlCommand command = new SqlCommand(sql3, connection1))
                            {
                                command.Parameters.AddWithValue("@hrana", hrana);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare3 interogare33 = new Interogare3();
                                        interogare33.nr = "" + reader.GetInt32(0);
                                        interogare3.Add(interogare33);

                                    }
                                }
                            }
                            break;

                        case "actiune4":
                            animal = Request.Form["animal"]; 
                            String sql4 = "SELECT DISTINCT A.IDComanda, B.Nume as NumeClient, B.Prenume AS PrenumeClient " +
                                " FROM Comanda A, Clienti B, DetaliiComanda C " +
                                "WHERE A.IDClient=B.IDClient AND A.IDComanda=C.IDComanda" +
                                " AND C.IDAnimal in ( SELECT IDAnimal FROM Animale " +
                                "WHERE Specie=@animal)";
                            using (SqlCommand command = new SqlCommand(sql4, connection1))
                            {
                                command.Parameters.AddWithValue("@animal", animal);
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Interogare4 interogare44 = new Interogare4();
                                        interogare44.id = "" + reader.GetInt32(0);
                                        interogare44.Nume = reader.GetString(1);
                                        interogare44.prenume = reader.GetString(2);
                                        interogare4.Add(interogare44);

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
        }

        public class Interogare2
        {
            public String id;
            public String Nume;
            public String prenume;
        }

        public class Interogare3
        {
            public String nr;
        }

        public class Interogare4
        {
            public String id;
            public String Nume;
            public String prenume;
        }
    }
  }
   