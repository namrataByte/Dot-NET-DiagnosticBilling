namespace DiagnosticBilling.Models
{
    public class Bill
    {
        public int billId { get; set; }      // Primary key
        public int patientId { get; set; }  // Foreign key for Patient
        public int doctorId { get; set; }   // Foreign key for Doctor
        public decimal TotalCost { get; set; } // Total cost of the bill
        public DateTime TodayDate { get; set; } // Date of the bill

        // Navigation properties
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }

        // Add navigation property for BillTest (many-to-many relationship)
        public ICollection<BillTest> BillTests { get; set; }
    }
}
