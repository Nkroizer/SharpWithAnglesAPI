using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpWithAnglesAPI.Data;

namespace SharpWithAnglesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly DataContext _context;

        public AnimalController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Animal>>> GetAnimal()
        {
            return Ok(await _context.Animals.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<Animal>>> PostAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return Ok(await _context.Animals.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Animal>>> UpdateAnimal(Animal animal)
        {
            var dbAnimal = await _context.Animals.FindAsync(animal.id);
            if (dbAnimal == null)
            {
                return BadRequest("animal not found");
            }

            dbAnimal.name = animal.name;
            dbAnimal.scientificName = animal.scientificName;
            dbAnimal.species = animal.species;
            dbAnimal.imageUrl = animal.imageUrl;
            await _context.SaveChangesAsync();
            return Ok(await _context.Animals.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Animal>>> DeleteAnimal(int id)
        {
            var dbAnimal = await _context.Animals.FindAsync(id);
            if (dbAnimal == null)
            {
                return BadRequest("animal not found");
            }
            _context.Animals.Remove(dbAnimal);
            await _context.SaveChangesAsync();
            return Ok(await _context.Animals.ToListAsync());
        }
    }
}
