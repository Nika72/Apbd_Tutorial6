using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Tutorial6.Repositories 
{
    public class AnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Animal> GetAnimals(string orderBy)
        {
            List<Animal> animals = new List<Animal>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT * FROM Animal ORDER BY ";

                switch (orderBy.ToLower())
                {
                    case "description":
                        sqlQuery += "Description";
                        break;
                    case "category":
                        sqlQuery += "Category";
                        break;
                    case "area":
                        sqlQuery += "Area";
                        break;
                    default:
                        sqlQuery += "Name"; // Default sorting by Name
                        break;
                }

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Animal animal = new Animal
                            {
                                Id = (int)reader["IdAnimal"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Category = reader["Category"].ToString(),
                                Area = reader["Area"].ToString()
                            };

                            animals.Add(animal);
                        }
                    }
                }
            }

            return animals;
        }


        public Animal GetAnimalById(int id)
        {
            Animal animal = null;

            string sqlQuery = "SELECT * FROM Animal WHERE IdAnimal = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animal = new Animal
                            {
                                Id = (int)reader["IdAnimal"],
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                Category = reader["Category"].ToString(),
                                Area = reader["Area"].ToString()
                            };
                        }
                    }
                }
            }

            return animal;
        }
    

    public void AddAnimal(Animal animal)
        {
            // SQL query to insert a new animal into the database
            string sqlQuery = @"INSERT INTO Animal (Name, Description, Category, Area)
                        VALUES (@Name, @Description, @Category, @Area)";

            // Establishing a connection to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create a SqlCommand object with the SQL query and connection
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Adding parameters for the animal's properties to the command
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description);
                    command.Parameters.AddWithValue("@Category", animal.Category);
                    command.Parameters.AddWithValue("@Area", animal.Area);

                    // Executing the SQL query to insert the animal into the database
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateAnimal(Animal animal)
        {
            // SQL query to update an existing animal in the database
            string sqlQuery = @"UPDATE Animal 
                        SET Name = @Name, Description = @Description, 
                            Category = @Category, Area = @Area 
                        WHERE IdAnimal = @Id";

            // Establishing a connection to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Create a SqlCommand object with the SQL query and connection
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Add parameters for the animal's properties to the command
                    command.Parameters.AddWithValue("@Name", animal.Name);
                    command.Parameters.AddWithValue("@Description", animal.Description);
                    command.Parameters.AddWithValue("@Category", animal.Category);
                    command.Parameters.AddWithValue("@Area", animal.Area);
                    command.Parameters.AddWithValue("@Id", animal.Id); // Specify the ID of the animal to update

                    // Execute the SQL query to update the animal in the database
                    command.ExecuteNonQuery();
                }
            }
        }


        public void DeleteAnimal(int id)
        {
            string sqlQuery = "DELETE FROM Animal WHERE IdAnimal = @Id";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}