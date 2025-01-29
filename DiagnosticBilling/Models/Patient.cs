
namespace DiagnosticBilling.Models
{
    public class Patient 
    {
        public int patientId { get; set; }
        public string patientName { get; set; }
        public int patientAge { get; set; }
        public string patientGender { get; set; } // M, F, Else
     
    }
}
