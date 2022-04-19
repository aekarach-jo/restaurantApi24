using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private IncomeService _incomeService;
        public IncomeController(IncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpPost]
        public ActionResult<Income> CreateIncome(Income income) => _incomeService.CreateIncome(income);

        [HttpGet]
        public ActionResult<List<Income>> GetIncome() => _incomeService.GetIncomeAll();

    }
}