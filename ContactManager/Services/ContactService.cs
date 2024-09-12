using ContactManager.Interfaces;
using ContactManager.Model;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        public bool ValidateContact(Contact contact)
        {

            if (contact.Salutation.Length < 3)
            { 
                return false;
            }

            if (contact.Firstname.Length < 3)
            {
                return false;
            }

            if (contact.Lastname.Length < 3)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(contact.DisplayName))
            {
                contact.DisplayName = $"{contact.Salutation} {contact.Firstname} {contact.Lastname}";
            }

            return true;
        }

    }
}
