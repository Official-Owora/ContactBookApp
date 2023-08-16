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
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactController>
       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllContactAsync([FromQuery] ContactRequestInputParameter parameter)
        {
            var result = await _contactService.GetAllContactAsync(parameter);
            Response.Headers.Add("X-Pagination",JsonSerializer.Serialize(result.Data.pagingData));
            return Ok(result.Data.pagingData);
        }

        // GET api/<ContactController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactByIdAsync(string id) 
        {
            var result = await _contactService.GetContactByIdAsync(id);
            return Ok(result);
        }

        // GET api/<ContactController>/5
        [Authorize]
        [HttpGet("{email}")]
        public async Task<IActionResult> GetContactByEmailAsync(string email)
        {
            var result = await _contactService.GetContactByEmailAsync(email);
            return Ok(result);
        }

        // POST api/<ContactController>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateContactAsync([FromBody] ContactRequestDto contactRequestDto)
        {
            var result = await _contactService.CreateContactAsync(contactRequestDto);
            return Ok(result);
        }

        // PUT api/<ContactController>/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContactAsync(string id, [FromBody] ContactRequestDto contactRequestDto)
        {
            var result = await _contactService.UpdateContactAsync(id, contactRequestDto);
            return Ok(result);
        }

        // DELETE api/<ContactController>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactAsync(string id)
        {
            var result = await _contactService.DeleteContactAsync(id);
            return Ok(result);
        }
    }
}
