using System.ComponentModel.DataAnnotations;

namespace ContactManager.Model
{
    public class Contact : ModelBase
    {
        [Required]
        public string Salutation { get; set; }

        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public bool NotifyHasBirthdaySoon
        {
            get
            {
                var today = DateTime.Today;
                var nextBirthday = new DateTime(today.Year, Birthdate.Month, Birthdate.Day);

                if (nextBirthday < today)
                {
                    nextBirthday = nextBirthday.AddYears(1);
                }

                return (nextBirthday - today).Days <= 14;
            }
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phonenumber { get; set; }
    }
}
