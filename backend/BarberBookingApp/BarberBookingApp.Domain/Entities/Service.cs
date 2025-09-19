namespace BarberBookingApp.Domain.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int Duration { get; set; } //in minutes
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<BarberService> BarberServices { get; set; }

        public Service()
        {
            Appointments = new List<Appointment>();
            BarberServices = new List<BarberService>();
        }
    }
}
