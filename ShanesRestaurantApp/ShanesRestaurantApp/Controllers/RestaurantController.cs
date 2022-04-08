using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShanesRestaurantApp.Models;
using Serilog;
using Microsoft.Extensions.Logging;

namespace ShanesRestaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : Controller
    {
        RestaurantModel model = new RestaurantModel();

        [HttpGet]
        [Route("View Meals")]
        public IActionResult MealList()
        {
            return Ok(model.GetMealsList());
        }

        [HttpGet]
        [Route("View Meals Ordered By Name")]
        public IActionResult OrderedMealList(string orderName)
        {
            if (model.MealListByOrderName(orderName).Count == 0)
            {
                _logger.LogWarning("Order Name Doesn't Exist");
                return BadRequest("Order Name Doesn't Exist");
            }
            return Ok(model.MealListByOrderName( orderName));
        }


        [HttpGet]
        [Route("View Meals Ordered By LoginID")]
        public IActionResult MealListByLoginID(int loginID)
        {

            if (loginID <= 0)
            {
                _logger.LogWarning("Login ID Can't Be Negative");
                return BadRequest("Login ID Can't Be Negative");
            }
            if (model.MealListByLoginID(loginID).Count == 0)
            {
                _logger.LogWarning("Meal ID doesn't exist");
                return BadRequest("Meal ID doesn't exist");
            }
            return Ok(model.MealListByLoginID(loginID));
        }


        [HttpPost]
        [Route("Add Meal")]
        public IActionResult Addproduct(RestaurantModel newProduct)
        {
            try
            {
                return Created("", model.OrderMeal(newProduct));
            }
            catch (System.Exception es)
            {
                _logger.LogError(es, es.Message);
                return BadRequest(es.Message);
            }

        }

        [HttpDelete]
        [Route("Delete Meal")]
        public IActionResult DeleteMeal(int mealID)
        {
            if (mealID <= 0)
            {
                _logger.LogWarning("Meal ID Can't Be Negative");
                return BadRequest("Meal ID Can't Be Negative");
            }

            try
            {
                return Accepted(model.DeleteMeal(mealID));
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private readonly ILogger<RestaurantController> _logger;
        public RestaurantController(ILogger<RestaurantController> logger)
        {
            _logger = logger;
        }


    }
}
