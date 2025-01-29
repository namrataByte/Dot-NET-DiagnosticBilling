namespace DiagnosticBilling.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public decimal Price { get; set; }

        // Add navigation property for BillTest (many-to-many relationship)
        public ICollection<BillTest> BillTests { get; set; }
    }
}
