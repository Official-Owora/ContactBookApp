using ContactBook.Shared.RequestParameter.ModelParameters;
using ContactBookApp.Application.Services.Interfaces;
using ContactBookApp.Domain.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactBookApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UserController>
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserAsync([FromQuery] UserRequestInputParameter parameter)
        {
            var result = await _userService.GetAllUserAsync(parameter);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Data.pagingData));
            return Ok(result.Data.pagingData);
        }

        // GET api/<UserController>/5
        //[Authorize]
        [HttpGet("byId/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id) 
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }

        // GET api/<UserController>/5
        //[Authorize]
        [HttpGet("byemail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            return Ok(result);
        }

        // PUT api/<UserController>/5
        //[Authorize]
        [HttpPut("user/put/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UserRequestDto userRequestDto)
        {
            var result = await _userService.UpdateUserAsync(id, userRequestDto);
            return Ok(result);
        }

        // DELETE api/<UserController>/5
        //[Authorize(Roles = "Admin")]
        [HttpDelete("user/delet/{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return Ok(result);
        }
    }
}
