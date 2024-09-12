using ContactManager.Model;

namespace ContactManager.Interfaces
{
    public interface IContactService
    {
        public bool ValidateContact(Contact contact);
    }
}
