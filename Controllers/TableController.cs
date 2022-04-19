using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private TableService _tableService;
        public TableController(TableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        public ActionResult<Table> CreateTable(Table table){
            var data = _tableService.GetTableAllForApi();
            var dataBynumber = _tableService.GetTableByNo(table.Table_NO);
            if(dataBynumber != null){
                return NotFound();
            }
            int number = data.Count();
            var id = "table00"+number.ToString();
            table.Table_id = id;
            _tableService.CreatedTable(table);
            return table;
        }

        [HttpGet]
        public ActionResult<List<Table>> GetTable() => _tableService.GetTableAll();

        [HttpGet("{id}")]
        public ActionResult<Table> GetTableById(string id) => _tableService.GetTableById(id);

        [HttpGet("{id}")]
        public ActionResult<Table> DeleteTable(string id) {
            var data = _tableService.GetTableById(id);
            data.Status = "deleted";
            _tableService.UpdateTable(id, data);
            return data;
        } 
    }
}