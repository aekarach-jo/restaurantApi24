using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeFoodController : ControllerBase
    {
        private TypeFoodService _typeFoodService;
        public TypeFoodController(TypeFoodService typeFoodService)
        {
            _typeFoodService = typeFoodService;
        }

        [HttpPost]
        public ActionResult<TypeFood> CreateTypeFood(TypeFood typeFood)
        {
            var data = _typeFoodService.GetTypeFoodForApi();
            int runNumber = data.Count();
            var type_id = "tf0"+runNumber.ToString();
            typeFood.id = type_id;
            foreach(var typeF in data){
                if( typeF.name.ToLower() == typeFood.name.ToLower())
                {
                    return NotFound();
                }
            }
            _typeFoodService.CreateTypeFood(typeFood);
            return typeFood;
        }

        [HttpGet]
        public ActionResult<List<TypeFood>> GetTypeFood()
        {
            var data = _typeFoodService.GetTypeFood();
            return data;
        }
    }
}