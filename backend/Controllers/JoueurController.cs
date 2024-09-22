using api_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoueurController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public JoueurController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Joueur
        [HttpGet]
        public ActionResult<IEnumerable<Joueur>> Get()
        {
            var joueurs = new List<Joueur>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Joueur", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var joueur = new Joueur
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Prenom = reader["Prenom"].ToString(),
                            Nom = reader["Nom"].ToString(),
                            Image = reader["Image"].ToString()
                        };
                        joueurs.Add(joueur);
                    }
                }
            }

            return joueurs;
        }

        // GET api/Joueur/5
        [HttpGet("{id}")]
        public Joueur Get(int id)
        {
            var joueur = new Joueur();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Joueur where id=" + id, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var joueurTarget = new Joueur
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Prenom = reader["Prenom"].ToString(),
                            Nom = reader["Type"].ToString(),
                            Image = reader["Image"].ToString()
                        };
                        joueur = joueurTarget;
                    }
                }
            }

            return joueur;
        }

        // POST api/Joueur
        [HttpPost]
        public IActionResult Post([FromBody] Joueur joueur)
        {
            if (joueur == null)
            {
                return BadRequest("Le joueur entré est nul.");
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("INSERT INTO Joueur (Prenom, Nom, Image) VALUES (@Prenom, @Nom, @Image)", connection);
                    command.Parameters.AddWithValue("@Prenom", joueur.Prenom);
                    command.Parameters.AddWithValue("@Nom", joueur.Nom);
                    command.Parameters.AddWithValue("@Image", joueur.Image);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(joueur);
        }

        // PUT api/Joueur/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Joueur joueur)
        {
            if (joueur == null)
            {
                return BadRequest("Les valeurs entrées sont nulles.");
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("UPDATE Joueur set Prenom=@Prenom, Nom=@Nom, Image=@Image where id=" + id, connection);
                    command.Parameters.AddWithValue("@Prenom", joueur.Prenom);
                    command.Parameters.AddWithValue("@Nom", joueur.Nom);
                    command.Parameters.AddWithValue("@Image", joueur.Image);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(joueur);
        }

        // DELETE api/Joueur/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var joueur = new Joueur();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("Delete FROM Joueur where id=" + id, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var joueurTarget = new Joueur
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Prenom = reader["Prenom"].ToString(),
                            Nom = reader["Nom"].ToString(),
                            Image = reader["Image"].ToString()
                        };
                        joueur = joueurTarget;
                    }
                }
            }

            return Ok("Le joueur ayant pour id : " + id + " à été supprimé avec succès.");
        }
    }
}
