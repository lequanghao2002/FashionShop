using FashionShop.Models.DTO;
using FashionShop.Models.Domain;
using FashionShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.IdentityModel.Tokens;

namespace FashionShop.Repositories
{
    public interface IContactRepository
    {
        List<ContactDTO> GetAllContact(string? searchByPhoneNumber);
        //AddContactDTO AddContact(AddContactDTO addContactDTO);
        //UpdateContactDTO UpdateContactById(int id, UpdateContactDTO updatecontactDTO);
        //Contact DeleteContact(int id);
    }
    public class ContactRepository : IContactRepository
    {
        private readonly FashionShopDBContext _identityDbContext;
        public ContactRepository(FashionShopDBContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public List<ContactDTO> GetAllContact(string? searchByPhoneNumber)
        {
            var allContact = _identityDbContext.Contacts.AsQueryable();

            if(!searchByPhoneNumber.IsNullOrEmpty())
            {
                allContact = allContact.Where(c => c.PhoneNumber.Contains(searchByPhoneNumber));    
            }

            var allContactDomain = allContact.Select(c => new ContactDTO
            {
                ID = c.ID,
                FullName = c.FullName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Content = c.Content,
                Status = c.Status,

            }).OrderByDescending(c => c.ID).ToList();

            return allContactDomain;
        }
        //public AddContactDTO AddContact(AddContactDTO addContactDTO)
        //{
        //    var ContactDM = new Contact
        //    {
        //        FullName = addContactDTO.FullName,
        //        Email = addContactDTO.Email,
        //        PhoneNumber = addContactDTO.PhoneNumber,
        //        Content = addContactDTO.Content,
        //        Status = true,
        //    };
        //    _identityDbContext.Contacts.Add(ContactDM);
        //    _identityDbContext.SaveChanges();
        //    return addContactDTO;
        //}
        //public UpdateContactDTO UpdateContactById(int id, UpdateContactDTO updatecontactDTO)
        //{
        //    var ContactDm = _identityDbContext.Contacts.FirstOrDefault(n => n.ID == id);
        //    if (ContactDm != null)
        //    {
        //        ContactDm.FullName = updatecontactDTO.FullName;
        //        ContactDm.PhoneNumber = updatecontactDTO.PhoneNumber;
        //        ContactDm.Email = updatecontactDTO.Email;
        //        _identityDbContext.SaveChanges();
        //    }
        //    return updatecontactDTO;
        //}
        //public Contact DeleteContact(int id)
        //{
        //    var DeletedM = _identityDbContext.Contacts.FirstOrDefault(n => n.ID == id);
        //    if (DeletedM != null)
        //    {
        //        _identityDbContext.Contacts.Remove(DeletedM);
        //        _identityDbContext.SaveChanges();
        //    }
        //    return DeletedM;
        //}
    }
}