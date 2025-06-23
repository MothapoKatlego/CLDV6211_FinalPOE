namespace KatEventPlan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [Key]
        public int Booking_Id { get; set; }

        public int Event_Id { get; set; }

        public int Venue_Id { get; set; }

        public DateTime Booking_Date { get; set; }

        public virtual Event Event { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
