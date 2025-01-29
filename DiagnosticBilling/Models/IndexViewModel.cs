namespace DiagnosticBilling.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Bill> Bills { get; set; }
        public IEnumerable<Test> Tests { get; set; }
    }
}
