namespace BarberBookingApp.Domain.Entities
{
    public class BarberService
    {
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public decimal Price { get; set; }
    }
}
