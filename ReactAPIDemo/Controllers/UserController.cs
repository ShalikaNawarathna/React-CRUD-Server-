using Microsoft.AspNetCore.Mvc;
using ReactAPIDemo.Models;
using ReactAPIDemo.Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactAPIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IusersService userService;
        public UserController(IusersService userService)
        {
            this.userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Users> users = await userService.Get();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<UserController>/5
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string? email)
        {
            var decodedEmail = WebUtility.UrlDecode(email);
            var user =  await userService.Get(decodedEmail);
            if(user == null)
            {
                return NotFound($"User with Id = {email} not found ");
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost("createUser")]
        /*public ActionResult<Users> Post([FromBody] Users user)
        {
            
            userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
        */
        public ActionResult<Users> Post([FromBody] Users user)
        {
            try
            {
              var newUser =   userService.Create(user);
                return Ok(newUser);
                //return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return an error response to the client
                return BadRequest(ex.Message);
            }
        }
        // PUT api/<UserController>/5
        [HttpPut("updateUser/{email}")]
        public async  Task<ActionResult> Put(Users user, string email)
        {
            try
            {
                var updateUser =  await userService.Update(user);
                return Ok(updateUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }

        // DELETE api/<UserController>/5
        [HttpDelete("deleteUser/{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            try
            {
                await userService.Remove(email);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] Login user)
        {
            try
            {
                Users loginUser = await userService.Login(user.Email, user.Password);
                return Ok(loginUser);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
