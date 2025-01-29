namespace DiagnosticBilling.Models
{
    public class BillViewModel
    {
        public int billId { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime TodayDate { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string patientGender { get; set; }
        public string doctorName { get; set; }
        public string specialty { get; set; }
        public List<SelectedTestViewModel> SelectedTests { get; set; } // Add this property
    }

    public class SelectedTestViewModel
    {
        public string TestName { get; set; }
        public decimal Price { get; set; }
    }



}
