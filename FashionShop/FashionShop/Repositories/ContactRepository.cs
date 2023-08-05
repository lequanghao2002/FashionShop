using FashionShop.Models.DTO;
using FashionShop.Models.Domain;
using FashionShop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace FashionShop.Repositories
{
    public interface IContactRepository
    {
        List<ContactDTO> GetAllContact();
        AddContactDTO AddContact(AddContactDTO addContactDTO);
        UpdateContactDTO UpdateContactById(int id, UpdateContactDTO updatecontactDTO);
        Contact DeleteContact(int id);
        FindByPhoneDTO FindContact(string Phonenumber);
    }
    public class ContactRepository : IContactRepository
    {
        private readonly FashionShopDBContext _identityDbContext;
        public ContactRepository(FashionShopDBContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public List<ContactDTO> GetAllContact()
        {
            var allContact = _identityDbContext.Contacts.ToList();
            var allContactDTO = new List<ContactDTO>();
            foreach (var contact in allContact)
            {
                allContactDTO.Add(new ContactDTO()
                {
                    ID = contact.ID,
                    FullName = contact.FullName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber,
                });
            }
            return allContactDTO;
        }
        public AddContactDTO AddContact(AddContactDTO addContactDTO)
        {
            var ContactDM = new Contact
            {
                FullName = addContactDTO.FullName,
                Email = addContactDTO.Email,
                PhoneNumber = addContactDTO.PhoneNumber,
                Content= addContactDTO.Content,
                Status= addContactDTO.Status,
            };
            _identityDbContext.Contacts.Add(ContactDM);
            _identityDbContext.SaveChanges();
            return addContactDTO;
        }
        public UpdateContactDTO UpdateContactById(int id, UpdateContactDTO updatecontactDTO)
        {
            var ContactDm = _identityDbContext.Contacts.FirstOrDefault(n => n.ID == id);
            if (ContactDm != null)
            {
                ContactDm.FullName = updatecontactDTO.FullName;
                ContactDm.PhoneNumber = updatecontactDTO.PhoneNumber;
                ContactDm.Email = updatecontactDTO.Email;
                _identityDbContext.SaveChanges();
            }
            return updatecontactDTO;
        }
        public Contact DeleteContact(int id)
        {
            var DeletedM = _identityDbContext.Contacts.FirstOrDefault(n => n.ID == id);
            if (DeletedM != null)
            {
                _identityDbContext.Contacts.Remove(DeletedM);
                _identityDbContext.SaveChanges();
            }
            return DeletedM;
        }
        public FindByPhoneDTO FindContact(string Phonenumber)
        {
            var findcontacWDM = _identityDbContext.Contacts.FirstOrDefault(x => x.PhoneNumber == Phonenumber);
            if (findcontacWDM == null)
            {
                return null;
            }
            var findbyphoneDTO = new FindByPhoneDTO
            {
                ID = findcontacWDM.ID,
                FullName = findcontacWDM.FullName,
                Email = findcontacWDM.Email,
            };
            return findbyphoneDTO;
        }
    
    }
}