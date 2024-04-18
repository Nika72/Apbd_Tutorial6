using Microsoft.AspNetCore.Mvc;
using System;
using Tutorial6.Repositories;
using System.Collections.Generic; 

namespace Tutorial6.Controllers
{
    [ApiController]
    [Route("api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly AnimalRepository _animalRepository;

        public AnimalController(AnimalRepository animalRepository)
        {
            _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        }

        [HttpGet]
        public IActionResult GetAnimals(string orderBy = "Name")
        {
            try
            {
                List<Animal> animals = _animalRepository.GetAnimals(orderBy);
                return Ok(animals);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimalById(int id)
        {
            try
            {
                Animal animal = _animalRepository.GetAnimalById(id);
                if (animal == null)
                {
                    return NotFound($"Animal with ID {id} is not found.");
                }
                return Ok(animal);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            try
            {
                if (animal == null)
                {
                    return BadRequest("Animal data is missing.");
                }

                // Validate animal data
                if (string.IsNullOrWhiteSpace(animal.Name) || string.IsNullOrWhiteSpace(animal.Description) ||
                    string.IsNullOrWhiteSpace(animal.Category) || string.IsNullOrWhiteSpace(animal.Area))
                {
                    return BadRequest("All fields (Name, Description, Category, Area) are required.");
                }

                _animalRepository.AddAnimal(animal);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
        {
            try
            {
                if (animal == null)
                {
                    return BadRequest("Animal data is missing.");
                }

                // Validate animal data
                if (string.IsNullOrWhiteSpace(animal.Name) || string.IsNullOrWhiteSpace(animal.Description) ||
                    string.IsNullOrWhiteSpace(animal.Category) || string.IsNullOrWhiteSpace(animal.Area))
                {
                    return BadRequest("All fields (Name, Description, Category, Area) are required.");
                }

                var existingAnimal = _animalRepository.GetAnimalById(id);
                if (existingAnimal == null)
                {
                    _animalRepository.AddAnimal(animal);
                    return StatusCode(201);
                }
                else
                {
                    animal.Id = id; // Ensure the correct ID is set for the update
                    _animalRepository.UpdateAnimal(animal);
                    return StatusCode(204);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            try
            {
                var existingAnimal = _animalRepository.GetAnimalById(id);
                if (existingAnimal == null)
                {
                    return NotFound($"Animal with ID {id} is not found.");
                }

                _animalRepository.DeleteAnimal(id);
                return StatusCode(204); // No content, successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
