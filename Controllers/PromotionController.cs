using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private PromotionService _promotionService;
        public PromotionController(PromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpPost]
        public ActionResult<Promotion> CreatePromotion(Promotion promotion){
            _promotionService.CreatePromotion(promotion);
            return promotion;
        }

        [HttpGet]
        public ActionResult<List<Promotion>> GetPromotionForManage() => _promotionService.GetPromotionForManage();
        [HttpGet]
        public ActionResult<List<Promotion>> GetPromotionForShow() => _promotionService.GetPromotionForShow();

        [HttpGet("{id}")]
        public ActionResult<Promotion> GetPromotionById(string id) => _promotionService.GetPromotionById(id);

        [HttpGet("{id}")]
        public bool ChangeStatusPromotion(string id) {
            var data = _promotionService.GetPromotionById(id);
            if(data.Status == true){
                data.Status = false;
            } else {
                data.Status = true;
            }
            _promotionService.UpdatePromotion(id, data);
            return data.Status;
        }

        [HttpPut("{id}")]
        public IActionResult EditPromotion(string id, [FromBody] Promotion promotion)
        {
            var data = _promotionService.GetPromotionById(id);
            if (data == null){
                return NotFound();
            }
            promotion.Promotion_id = id;
            _promotionService.UpdatePromotion(id, promotion);
            return NoContent();
        }
    }
}