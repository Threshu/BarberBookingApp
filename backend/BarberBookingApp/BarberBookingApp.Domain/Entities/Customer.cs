namespace BarberBookingApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; }
        public Customer()
        {
            Appointments = new List<Appointment>();
        }
    }
}
