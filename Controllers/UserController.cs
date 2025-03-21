using CoreBusiness.LoginModel;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using UserCase.IBusiness;
using UserCase.IRepository;

namespace DockerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        IUserService _userService;
        ILogger<UserController> _logger;
        public UserController(IUserRepository userRepository, IUserService userService, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLogin loginRequest)
        {
            var user = _userRepository.GetUserByLoginRequest(loginRequest);
            if (user != null)
            {
                var token = _userService.GenerateToken(user);
                return Ok(token);
            }
            return BadRequest();
        }
    }
}
