using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly FashionShopDBContext _identityDbContext;
        private readonly IContactRepository _icontactRepository;
        public ContactController(FashionShopDBContext identityDbContext, IContactRepository icontactRepository)
        {
            _identityDbContext = identityDbContext;
            _icontactRepository = icontactRepository;
        }

        [HttpGet("get-all-contact")]
        public IActionResult GetAll(string? searchByPhoneNumber)
        {
            var allcontacts = _icontactRepository.GetAllContact(searchByPhoneNumber);
            return Ok(allcontacts);
        }

        //[HttpPut("update-contact-by-id/{id}")]
        //public IActionResult UpdateContactById(int id, [FromBody] UpdateContactDTO updatecontactDTO)
        //{
        //    var updatContact = _icontactRepository.UpdateContactById(id, updatecontactDTO);
        //    return Ok(updatContact);
        //}

        //[HttpPost("add-contact")]
        //public IActionResult AddContact([FromBody] AddContactDTO addContactDTO)
        //{
        //    var addcontact = _icontactRepository.AddContact(addContactDTO);
        //    return Ok(addcontact);
        //}

        //[HttpDelete("delete-category-by-id/{id}")]
        //public IActionResult DeleteContact(int id)
        //{
        //    var deletecontact = _icontactRepository.DeleteContact(id);
        //    return Ok(deletecontact);
        //}
    }
}