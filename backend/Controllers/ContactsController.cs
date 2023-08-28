using backend.Data;
using backend.Data.DTOs.Request;
using backend.Data.DTOs.Response;
using backend.Exceptions;
using backend.Servicies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        ContactsService ContactService { get; }

        public ContactsController(ContactsService contactService) {
            this.ContactService = contactService;
        }
        
        // GET: api/<ContactController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await ContactService.GetAllContactsAsync());
            } 
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                ContacDetailResponseDTO? contactResponse = await ContactService.GetContactByIdAsync(id);
                if (contactResponse == null) return NotFound();


                return Ok(contactResponse);
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactDetailRequestDTO contactRequest)
        {
            try 
            {
                await ContactService.CreateContactAsync(contactRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ContactDetailRequestDTO contact)
        {
            try
            {
                await ContactService.UpdateContactAsync(id, contact);
                return Ok();
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await ContactService.DeleteContactAsync(id);
                return Ok();
            }
            catch (ContactNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
