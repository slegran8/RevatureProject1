using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShanesRestaurantApp.Models;
using Serilog;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace ShanesRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        OrderModel model = new OrderModel();
        private List<OrderModel> tempList;

        [HttpGet]
        [Route("View Orders")]
        public IActionResult Index()
        {
            _logger.LogWarning("Retrieving All Orders");
            try
            {
                return Ok(model.GetOrderList());
            }
            catch (Exception ex)
            {
                
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("View Orders By LoginID")]
        public IActionResult OrderMealList(int loginID)
        {
            if (loginID <= 0)
            {
                _logger.LogWarning("Login Can't Be Negative");
                return BadRequest("Login Can't Be Negative");

            }
           if (model.OrderListByLoginID(loginID).Count == 0)
            {
                _logger.LogWarning("Login ID of:" + loginID + " doesn't exist");
                 return BadRequest("Login ID of:" + loginID + " doesn't exist");
            }

            _logger.LogWarning("Retrieving All Orders by ID");
            try
            {
                
                return Ok(model.OrderListByLoginID(loginID));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [Route("Make Order By LoginID")]
        public IActionResult makeOrder(int loginID)
        {
            if (loginID <= 0)
            {
                _logger.LogWarning("Login Can't Be Negative");
                return BadRequest("Login Can't Be Negative");

            }
            _logger.LogWarning("Making All Orders For Login ID: " + loginID);
            try
            {
                return Ok(model.FinalizeMeal(loginID));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }


    }
}
