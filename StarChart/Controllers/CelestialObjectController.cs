using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [ApiController]
    [Route("")]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id:int}", Name ="GetById")]
        public IActionResult GetById(int id)
        {
            try
            {
                var tmp = _context.CelestialObjects.FirstOrDefault(x => x.Id == id);
                if (tmp == null)
                {
                    return NotFound();
                }
                else
                {
                    tmp.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();
                    return Ok(tmp);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var tmp = _context.CelestialObjects.Where(e => e.Name == name).ToList();

                if (!tmp.Any())
                {
                    return NotFound();
                }
                else
                {
                    

                    foreach (var item in tmp)
                    {
                        item.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == item.Id).ToList();
                    }
                    return Ok(tmp);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                var celestialObjects = _context.CelestialObjects.ToList();

                foreach (var co in celestialObjects)
                {
                    co.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == co.Id).ToList();
                }

                return Ok(celestialObjects);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            try
            {

                    _context.CelestialObjects.Add(celestialObject);
                    _context.SaveChanges();
                    return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            try
            {
                var existing = _context.CelestialObjects.FirstOrDefault(e => e.Id == id);
                if (existing != null)
                {
                    existing.Name = name;
                    _context.Update(existing);
                    _context.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, CelestialObject celestialObject)
        {
            try
            {
                var existing = _context.CelestialObjects.FirstOrDefault(e => e.Id == id);
                if (existing != null)
                {
                    existing.Name = celestialObject.Name;
                    existing.OrbitalPeriod = celestialObject.OrbitalPeriod;
                    existing.OrbitedObjectId = celestialObject.OrbitedObjectId;

                    _context.CelestialObjects.Update(existing);
                    _context.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existing = _context.CelestialObjects.FirstOrDefault(e => e.Id == id);
                if (existing != null)
                {
                    _context.CelestialObjects.RemoveRange(existing);
                    _context.SaveChanges();
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
