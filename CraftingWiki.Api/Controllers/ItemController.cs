using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CraftingWiki.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        public class Item
        {
            public int ItemId { get; set; }
            public string Name { get; set; }
            public List<RecipeIngredient> Recipe { get; set; }
        }

        public class RecipeIngredient
        {
            public int ItemId { get; set; }
            public int Quantity { get; set; }
        }

        private static List<Item> _items = new List<Item>
        {
            new Item 
            { 
                ItemId = 1, 
                Name = "A", 
                Recipe = new List<RecipeIngredient>
                {
                    new RecipeIngredient { ItemId = 2, Quantity = 1},
                    new RecipeIngredient { ItemId = 3, Quantity = 2},
                    new RecipeIngredient { ItemId = 4, Quantity = 1}
                }
            },
            new Item
            { 
                ItemId = 2, 
                Name = "B", 
                Recipe = new List<RecipeIngredient>
                {
                    new RecipeIngredient { ItemId = 3, Quantity = 1},
                    new RecipeIngredient { ItemId = 5, Quantity = 1}
                } 
            },
            new Item{ ItemId = 3, Name = "C", Recipe = new List<RecipeIngredient> { } },
            new Item{ ItemId = 4, Name = "D", Recipe = new List<RecipeIngredient>() },
            new Item{ ItemId = 5, Name = "E", Recipe = new List<RecipeIngredient>() },
        };

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return Ok(_items.Select(x => new { x.ItemId, x.Name }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            var result = _items.FirstOrDefault(x => x.ItemId == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { result.ItemId, result.Name, Recipe = result.Recipe.Select(x => new { x.ItemId, x.Quantity, _items.First(y => y.ItemId == x.ItemId).Name }) });
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
