# DiagnosticBilling

*DiagnosticBilling* is a simple billing software for diagnostic centers, designed to streamline the billing process by allowing users to generate bills, view previous records, and manage them efficiently. The application enables the selection of doctors, patients, and diagnostic tests, followed by automatic bill generation with a detailed breakdown.

## Features
- **Doctor and Patient Selection**: The app allows users to select registered doctors and patients from the database. This avoids re-entry of information, making the process faster.
- **Select Tests**: Users can choose a list of tests to be performed, with the total cost automatically calculated upon pressing the "Generate Bill" button.
- **Generate Bill**: Once the tests are selected, the bill is generated with a breakdown of the tests and their respective costs.
- **View Bill History**: A "View Bill History" button allows users to navigate to a page displaying past bills, which includes a chart showing Bill Id, Patient Name, Doctor Name, Total Cost, Date, and Actions.
- **Select and Delete Multiple Records**: Users can select multiple records from the Bill History table using checkboxes and delete them with the "Delete" button.
- **View Bill Details**: Clicking the "View Details" button for each bill shows detailed information, including patient info, doctor info, test list, and total cost.
- **Printable Bill**: A printable version of the bill can be accessed on the bill details page.
- **Navigation**: A back button is provided to return to the main page.

## Technologies Used
- **ASP.NET Core MVC**: Used for backend development following the Model-View-Controller architecture.
- **SQL Server**: Used to store patient, doctor, and test data, as well as bill records.
- **HTML/CSS**: Used for structuring and styling the user interface.
- **JavaScript**: For handling dynamic actions such as bill generation and record selection.
- **Bootstrap**: For responsive and modern UI design.

## Database Schema

The application uses the following database schema to store patient, doctor, test, and bill data:

- **Patient**: Stores information about patients.
  - `patientId` (Primary Key)
  - `patientName`
  - `patientAge`
  - `patientGender`

- **Doctor**: Stores information about doctors.
  - `doctorId` (Primary Key)
  - `doctorName`
  - `specialty`

- **Test**: Stores information about the available tests in the diagnostic center.
  - `testId` (Primary Key)
  - `testName`
  - `price`

- **Bill**: Stores generated bills with references to the selected doctor and patient.
  - `billId` (Primary Key)
  - `patientId` (Foreign Key to `Patient`)
  - `doctorId` (Foreign Key to `Doctor`)
  - `totalCost`
  - `todayDate` (Default: current date)

- **BillTest**: Stores the mapping between tests and bills.
  - `BillId` (Foreign Key to `Bill`)
  - `TestId` (Foreign Key to `Test`)

