namespace KatEventPlan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int Event_Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Event_Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Event_Date { get; set; }

        public string Description { get; set; }



       

        // Foreign key for Venue
        public int? Venue_Id { get; set; }

        // Navigation property - enables eager loading with Include()
        public virtual Venue Venue { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}

