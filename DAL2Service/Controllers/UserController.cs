using Base.Models;
using DAL2Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DAL2Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _UserService;
        public UserController(IUserService userService) 
        {
         _UserService = userService;
        
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            var res = await _UserService.Login(model);
            if (res == null)
                return "Login Failed";

            return Ok(res);
        }
    }
}
