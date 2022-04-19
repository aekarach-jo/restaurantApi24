using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private FoodService _foodService;
        public FoodController(FoodService foodService)
        {
            _foodService = foodService;
        }

    [HttpPost, DisableRequestSizeLimit]
    public IActionResult Upload()
    {
        try
        {
            var file = Request.Form.Files[0];
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if(file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                     file.CopyTo(stream);
                }

                return Ok(new {dbPath});
            } else {
                return BadRequest();
            }
        }
        catch (System.Exception)
        {
            return StatusCode(500, $"Internal Server Error");
        }
    }

    [HttpPost]
    public ActionResult<Food> CreateFood(Food food)
    {
        var data = _foodService.GetAllForApi();
        var num = data.Count();
        var id = "F0" + num.ToString();
        food.Food_id = id;
        food.Status = true;
        food.Amount = 0;
        _foodService.CreateFood(food);
        return food;
    }

    [HttpGet]
    public ActionResult<List<Food>> GetFoodAll() => _foodService.GetFoodAll();

    [HttpGet("{id}")]
    public ActionResult<Food> GetFoodById(string id)
    {
        var data = _foodService.GetFoodById(id);
        if ( data == null ){
            return NotFound();
        }
        return data;
    }

    [HttpPut("{id}")]
    public ActionResult<Food> EditFood(string id, [FromBody] Food food)
    {
        var data = _foodService.GetFoodById(id);
        if (data == null)
        {
            return NotFound();
        }
        food.Food_id = data.Food_id;
        _foodService.UpdateFood(id, food);
        return food;
    }
}
}