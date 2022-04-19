using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using ProjectCafeApi.Models;
using ProjectCafeApi.Services;

namespace ProjectCafeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            var data = _orderService.GetAllForApi();
            var num = data.Count();
            var id = "Order0" + num.ToString();
            order.Order_id = id;
            _orderService.CreateOrder(order);
            return order;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrder() => _orderService.GetOrderAll();

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrderById(string id) => _orderService.GetById(id);

        [HttpGet("{id}/{status}")]
        public IActionResult ChangeStatusOrder(string id, string status)
        {
            var data = _orderService.GetById(id);
            if (data.Status == "waitingApprove")
            {
                if (status == "approve")
                {
                    data.Status = "waitingFood";
                }
                else if (status == "disapprove")
                {
                    data.Status = "canceled";
                }
            }
            else if (data.Status == "waitingFood")
            {
                if (status == "sentFood")
                {
                    data.Status = "waitingPayment";
                }
            }
            else if (data.Status == "waitingPayment")
            {
                if (status == "paid")
                {
                    data.Status = "success";
                }
            }
            _orderService.UpdateOrder(data.Order_id, data);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult EditOrder(string id, [FromBody] Order order)
        {
            var data = _orderService.GetById(id);
            if (data == null)
            {
                return NotFound();
            }
            order.Order_id = data.Order_id;
            _orderService.UpdateOrder(id, order);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<BestType>> GetBestType()
        {
            var data = _orderService.GetOrderSuccess();
            var totalAmount = countItemInList(data);
            var list = new List<BestType>();
            foreach (var itemOrder in data)
            {
                foreach (var item in itemOrder.FoodList)
                {
                    if (!checkListBestType(list, item.Typeid))
                    {
                        BestType dataType = new BestType();
                        dataType.Id = item.Typeid;
                        dataType.Name = item.Type;
                        dataType.TotalAmount = item.Amount;
                        dataType.TotalPrice = item.Price;
                        dataType.Rate = ((float)item.Amount / totalAmount) * 100;
                        list.Add(dataType);
                    }
                    else
                    {
                        for (var i = 0; i < list.Count; i++)
                        {
                            if (list[i].Id == item.Typeid)
                            {
                                list[i].TotalAmount += item.Amount;
                                list[i].TotalPrice += item.Price;
                                list[i].Rate = ((float)list[i].TotalAmount / totalAmount) * 100;
                            }
                        }
                    }
                }
            }
            return list.OrderByDescending(or => or.TotalPrice).ToList();
        }

        [HttpGet]
        public ActionResult<List<Order>> IncomeMonth()
        {
            var data = _orderService.GetOrderSuccess();
            var income = new List<Order>();
            DateTime? date1 = new DateTime(2010, 2, 18);
            if(data != null){
                income = _orderService.GetOrderSuccessByMonth(date1.Value.Month);
            }
            return income;
        }

        [HttpGet]
        public ActionResult<List<BestFood>> GetBestFood()
        {
            var data = _orderService.GetOrderSuccess();
            var totalAmount = countItemInList(data);
            var list = new List<BestFood>();
            foreach (var itemOrder in data)
            {
                foreach (var item in itemOrder.FoodList)
                {
                    if (!checkListBestFood(list, item.Food_id))
                    {
                        BestFood dataFood = new BestFood();
                        dataFood.Id = item.Food_id;
                        dataFood.Name = item.Name;
                        dataFood.TotalAmount = item.Amount;
                        dataFood.TotalPrice = item.Price;
                        dataFood.Rate = ((float)item.Amount / totalAmount) * 100;
                        list.Add(dataFood);
                    }
                    else
                    {
                        for (var i = 0; i < list.Count; i++)
                        {
                            if (list[i].Id == item.Food_id)
                            {
                                list[i].TotalAmount += item.Amount;
                                list[i].TotalPrice += item.Price;
                                list[i].Rate = ((float)list[i].TotalAmount / totalAmount) * 100;
                            }
                        }
                    }
                }
            }
            return list.OrderByDescending(or => or.TotalPrice).ToList();
        }

        bool checkListBestType(List<BestType> myList, string myString)
        {
            foreach (var item in myList)
            {
                if (item.Id == myString)
                {
                    return true;
                }
            }
            return false;
        }
        bool checkListBestFood(List<BestFood> myList, string myString)
        {
            foreach (var item in myList)
            {
                if (item.Id == myString)
                {
                    return true;
                }
            }
            return false;
        }

        int countItemInList(List<Order> myList)
        {
            int count = 0;
            foreach (var item in myList)
            {
                foreach (var inItem in item.FoodList)
                {
                    count += inItem.Amount;
                }
            }
            return count;
        }
    }
}