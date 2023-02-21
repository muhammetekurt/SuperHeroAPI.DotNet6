using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero> {

                new SuperHero{
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York"
                },
                new SuperHero{
                    Id = 2,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"
                }
        };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context) 
        {
            _context= context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
            //return Ok(heroes); without db
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            //var hero = heroes.Find(x => x.Id == id);

            if (hero == null) return BadRequest("Hero not found");

            else return Ok(hero);
            
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

            //heroes.Add(hero);
            //return Ok(heroes);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null) return BadRequest("Hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());

            //var hero = heroes.Find(x => x.Id == request.Id);
            //if (hero == null) return BadRequest("Hero not found");

            //hero.Name= request.Name;
            //hero.FirstName= request.FirstName;
            //hero.LastName= request.LastName;
            //hero.Place= request.Place;

            //return Ok(heroes);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);

            if (dbHero == null) return BadRequest("Hero not found");

            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());

            //var hero = heroes.Find(x => x.Id == id);
            //if (hero == null) return BadRequest("Hero not found");

            //heroes.Remove(hero);

            //return Ok(heroes);
        }
    }
}
