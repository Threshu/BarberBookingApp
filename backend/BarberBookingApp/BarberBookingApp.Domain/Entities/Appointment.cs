namespace BarberBookingApp.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public DateTime DateTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public decimal Price { get; set; }
    }

    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled,
        NoShow
    }
}
