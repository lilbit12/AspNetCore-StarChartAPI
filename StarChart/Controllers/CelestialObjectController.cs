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
        [HttpGet("{id:int}")]
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
                    List<CelestialObject> list = new List<CelestialObject>();

                    foreach (var item in _context.CelestialObjects)
                    {
                        if (item.OrbitedObjectId == id)
                        {
                            list.Add(item);
                        }
                    }
                    return Ok(tmp);
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{name:string}")]
        public IActionResult GetByName(string name)
        {
            try
            {
                var tmp = _context.CelestialObjects.FirstOrDefault(x => x.Name.Equals(name));

                if (tmp == null)
                {
                    return NotFound();
                }
                else
                {
                    List<CelestialObject> list = new List<CelestialObject>();

                    foreach (var item in _context.CelestialObjects)
                    {
                        if (item.OrbitedObjectId == item.Id)
                        {
                            list.Add(item);
                        }
                    }
                }

                return Ok(tmp);
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

                List<CelestialObject> list = new List<CelestialObject>();

                foreach (var item in _context.CelestialObjects)
                {
                    if (item.OrbitedObjectId == item.Id)
                    {
                        list.Add(item);
                    }
                }


                return Ok(_context.CelestialObjects);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
