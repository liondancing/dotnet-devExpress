using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DXWebApplication1.Models {
    public partial class OutlookDataContext : DbContext {
        public OutlookDataContext()
            : base(@"data source=(localdb)\mssqllocaldb;attachdbfilename=|DataDirectory|\Data.mdf;integrated security=True;connect timeout=120") {
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Resources> Resources { get; set; }
    }

    public partial class Appointment {
        public int ID { get; set; }

        public int? EventType { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? AllDay { get; set; }

        public string Subject { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public int? Status { get; set; }

        public int? Label { get; set; }

        public int? ResourceID { get; set; }

        public string RecurrenceInfo { get; set; }

        public string ReminderInfo { get; set; }

        public string ContactInfo { get; set; }
    }

    public partial class Messages {
        public int ID { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [StringLength(32)]
        public string From { get; set; }

        [Required]
        [StringLength(128)]
        public string Subject { get; set; }

        [Required]
        public string Text { get; set; }

        public bool HasAttachment { get; set; }

        [StringLength(16)]
        public string Folder { get; set; }

        public bool IsReply { get; set; }
    }

    public partial class Resources {
        public int ID { get; set; }

        public string Name { get; set; }
    }
}