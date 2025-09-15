namespace BarberBookingApp.Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int BarberId { get; set; }
        public Barber Barber { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
