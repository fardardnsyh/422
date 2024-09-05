using Blogedium_api.Data;
using Blogedium_api.Interfaces.Services;
using Blogedium_api.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using System.Net;

namespace Blogedium_api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModal>> CreateUser( UserModal newuser)
        {
            try
            {
                var user = await _userService.CreateUserAsync(newuser);
                return CreatedAtAction(nameof(GetUserByID), new {id = user.Id}, user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch ( Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModal>> LoginUser (UserModal userModal)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.FindUserByEmailAddressAsync(userModal.EmailAddress); // null

                if (user == null)
                {
                    return BadRequest("User Does Not Exist, Please Register to Continue");
                }

                var verifiedPassword = BCrypt.Net.BCrypt.Verify(userModal.Password, user.Password);

                if (verifiedPassword != true)
                {
                    return BadRequest("Incorrect Password");
                }
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
 
        [HttpGet("profile/{id}")]
        public async Task<ActionResult<UserModal>> GetUserByID (int id)
        {
            try
            {
                var user = await _userService.FindUserAsync(id); // not null
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound("User Not Found");
            }
            catch ( Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModal>> DeleteUser (int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound("User Not Found");
            }
            return NoContent();
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserModal>>> GetAllUsers ()
        {
            try 
            {
                var users = await _userService.GetAllUsersAsync(); // 
                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch ( Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        private string GenerateJwtToken(UserModal userModal)
        {
            // Without claims, the token wouldn't carry user-specific
            // information required for authentication and authorization.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModal.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userModal.Role.ToString())
            };

            // Without a signing key and credentials, the token cannot be securely signed, making it invalid.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            // This step generates the actual token that includes 
            // all the necessary information and is signed for security.
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWT:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token); // Converts the token into a string format that can be sent to the client.
        }
    }
}