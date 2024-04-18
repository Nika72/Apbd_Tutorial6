using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Tutorial6.Repositories;

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

        // Implement other endpoints for adding and updating animals here
    }
}