namespace BarberBookingApp.Domain.Entities
{
    public class Barber
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        public double Rating { get; set; }
        public string ImageUrl {  get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Schedule> Schedules { get; set; }
        public ICollection<BarberService> BarberServices { get; set; }

        public Barber()
        {
            Appointments = new List<Appointment>();
            Schedules = new List<Schedule>();
            BarberServices = new List<BarberService>();
        }
    }
}
