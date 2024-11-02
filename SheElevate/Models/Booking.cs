namespace SheElevate.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int MentorsID { get; set; }
        public required Mentors Mentors { get; set; }
        public int MenteeID { get; set; }
        public required Mentee Mentee { get; set; }
        public DateTime SessionDate { get; set; }
        public required string SessionDetails { get; set; }
    }
}
