using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShanesRestaurantApp.Models;
using Serilog;
using Microsoft.Extensions.Logging;

namespace ShanesRestaurantApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : Controller
    {

        LoginModel model = new LoginModel();

        [HttpPost]
        [Route("Create Account")]
        public IActionResult CreateLogin(LoginModel newLogin)
        {
            try
            {
                return Created("", model.CreateLogin(newLogin));
            }
            catch (System.Exception es)
            {
                _logger.LogError(es, es.Message);
                return BadRequest(es.Message);

            }

        }

        [HttpGet]
        [Route("View Accounts")]
        public IActionResult Index()
        {
            return Ok(model.GetLoginList());
        }

        [HttpGet]
        [Route("View Account By LoginId")]
        public IActionResult AccountById(int loginId)
        {
            _logger.LogError("Getting Login ID: " + loginId);
            try
            {
                return Ok(model.GetLoginUserName(loginId));
            }
            catch (System.Exception ex)
            {
                _logger.LogError("Error Getting Login ID: " + loginId);
                return BadRequest(ex.Message);
            }
        }

        private readonly ILogger<Login> _logger;
        public Login(ILogger<Login> logger)
        {
            _logger = logger;
        }

        
    }
}
