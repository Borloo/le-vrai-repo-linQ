using api_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipeController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public EquipeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: api/Joueur
        [HttpGet]
        public ActionResult<IEnumerable<Equipe>> Get()
        {
            var equipes = new List<Equipe>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Equipe", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var equipe = new Equipe
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nom = reader["Nom"].ToString(),
                            Championnat = reader["Championnat"].ToString()
                        };
                        equipes.Add(equipe);
                    }
                }
            }

            return equipes;
        }

        // GET api/Equipe/5
        [HttpGet("{id}")]
        public Equipe Get(int id)
        {
            var equipe = new Equipe();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Equipe where id=" + id, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var equipeTarget = new Equipe
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nom = reader["Nom"].ToString(),
                            Championnat = reader["Championnat"].ToString()
                        };
                        equipe = equipeTarget;
                    }
                }
            }

            return equipe;
        }

        // POST api/Equipe
        [HttpPost]
        public IActionResult Post([FromBody] Equipe equipe)
        {
            if (equipe == null)
            {
                return BadRequest("L\'équipe entrée est nulle.");
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("INSERT INTO Equipe (Nom, Championnat) VALUES (@Nom, @Championnat)", connection);
                    command.Parameters.AddWithValue("@Nom", equipe.Nom);
                    command.Parameters.AddWithValue("@Championnat", equipe.Championnat);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(equipe);
        }

        // PUT api/Equipe/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Equipe equipe)
        {
            if (equipe == null)
            {
                return BadRequest("Les valeurs entrées sont nulles.");
            }

            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var command = new MySqlCommand("UPDATE Equipe set Nom=@Nom, Championnat=@Championnat where id=" + id, connection);
                    command.Parameters.AddWithValue("@Nom", equipe.Nom);
                    command.Parameters.AddWithValue("@Championnat", equipe.Championnat);
                 

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            return Ok(equipe);
        }

        // DELETE api/Equipe/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var equipe = new Equipe();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("Delete FROM Equipe where id=" + id, connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var equipeTarget = new Equipe
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nom = reader["Nom"].ToString(),
                            Championnat = reader["Championnat"].ToString()
                        };
                        equipe = equipeTarget;
                    }
                }
            }

            return Ok("L\'équipe ayant pour id : " + id + " à été supprimée avec succès.");
        }
    }
}
