using DiagnosticBilling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BillingController : Controller
{
    private readonly DiagnosticBillingDbContext _context;

    public BillingController(DiagnosticBillingDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var doctors = _context.Doctor.ToList();
        var patients = _context.Patient.ToList();
        var tests = _context.Test.ToList();
        var bills = _context.Bill.ToList();

        // Pass Doctors, Patients, Tests to the View
        ViewBag.Doctors = doctors;
        ViewBag.Patients = patients;

        // Passing the data to the view through the ViewModel
        return View(new IndexViewModel
        {
            Bills = bills,
            Tests = tests
        });
    }

    public IActionResult BillHistory()
    {
        var bills = _context.Bill
            .Include(b => b.Patient) // Include the related Patient object
            .Include(b => b.Doctor)  // Include the related Doctor object
            .ToList();

        return View(bills);
    }


    [Route("Billing/Bill/{billId}")]
    public async Task<IActionResult> Bill(int billId)
    {
        // SQL join to fetch Bill details along with related Patient and Doctor data
        var billDetails = await (from bill in _context.Bill
                                 join patient in _context.Patient on bill.patientId equals patient.patientId
                                 join doctor in _context.Doctor on bill.doctorId equals doctor.doctorId
                                 where bill.billId == billId
                                 select new
                                 {
                                     BillId = bill.billId,
                                     TotalCost = bill.TotalCost,
                                     TodayDate = bill.TodayDate,
                                     PatientName = patient.patientName,
                                     PatientAge = patient.patientAge,
                                     PatientGender = patient.patientGender,
                                     DoctorName = doctor.doctorName,
                                     Specialty = doctor.specialty
                                 }).FirstOrDefaultAsync();

        if (billDetails == null)
        {
            return NotFound(); // Return 404 if the bill is not found
        }

        // Fetch selected tests related to the bill using the BillTest junction table
        var selectedTests = await _context.BillTest
                                          .Where(bt => bt.BillId == billId)
                                          .Select(bt => new SelectedTestViewModel
                                          {
                                              TestName = bt.Test.TestName,
                                              Price = bt.Test.Price
                                          }).ToListAsync();

        // Handle cases where no tests are found (though it won't break your application)
        if (selectedTests == null || !selectedTests.Any())
        {
            Console.WriteLine("No tests found for this bill.");
        }

        // Return the view with the data
        return View(new BillViewModel
        {
            billId = billDetails.BillId,
            TotalCost = billDetails.TotalCost,
            TodayDate = billDetails.TodayDate,
            patientName = billDetails.PatientName,
            patientAge = billDetails.PatientAge,
            patientGender = billDetails.PatientGender,
            doctorName = billDetails.DoctorName,
            specialty = billDetails.Specialty,
            SelectedTests = selectedTests // Pass the list directly
        });
    }

    [HttpPost]
    public IActionResult DeleteSelected(int[] selectedBills)
    {
        if (selectedBills != null && selectedBills.Any())
        {
            // Find related BillTest records
            var relatedBillTests = _context.BillTest.Where(bt => selectedBills.Contains(bt.BillId));

            // Remove related BillTest records
            _context.BillTest.RemoveRange(relatedBillTests);

            // Find and remove the selected Bills
            var billsToDelete = _context.Bill.Where(b => selectedBills.Contains(b.billId));
            _context.Bill.RemoveRange(billsToDelete);

            // Save changes to the database
            _context.SaveChanges();
        }

        return RedirectToAction("BillHistory");
    }

    public IActionResult GenerateBill(int patientId, int doctorId, int[] selectedTestIds)
    {
        if (patientId == 0 || doctorId == 0 || selectedTestIds == null || !selectedTestIds.Any())
        {
            // Handle error when data is invalid or missing
            ModelState.AddModelError("", "Please select valid doctor, patient, and tests.");
            return RedirectToAction("Index");
        }

        // Fetch the selected tests from the database
        var selectedTests = _context.Test.Where(t => selectedTestIds.Contains(t.TestId)).ToList();

        // Calculate the total cost
        var totalCost = selectedTests.Sum(t => t.Price);

        // Create a new Bill object
        var bill = new Bill
        {
            patientId = patientId,
            doctorId = doctorId,
            TotalCost = totalCost,
            TodayDate = DateTime.Now
        };

        // Add the Bill to the database
        _context.Bill.Add(bill);
        _context.SaveChanges(); // Save to generate the BillId

        // Save the selected tests into the BillTest table
        var billTests = selectedTests.Select(test => new BillTest
        {
            BillId = bill.billId, // Use the generated BillId
            TestId = test.TestId  // Link the test
        }).ToList();

        _context.BillTest.AddRange(billTests);
        _context.SaveChanges(); // Save the BillTest records

        // Repopulate ViewBag after generating the bill
        ViewBag.Doctors = _context.Doctor.ToList();
        ViewBag.Patients = _context.Patient.ToList();

        // Create the ViewModel with Bills, Tests, and selected tests data
        var model = new IndexViewModel
        {
            Bills = _context.Bill.ToList(),
            Tests = _context.Test.ToList()
        };

        // Passing the selected tests and total cost to the view through ViewData or ViewBag
        ViewData["SelectedTests"] = selectedTests;
        ViewData["TotalCost"] = totalCost;
        ViewBag.Patient = _context.Patient.Find(patientId);
        ViewBag.Doctor = _context.Doctor.Find(doctorId);

        return View("Index", model); // Return the view with the updated model
    }

}
