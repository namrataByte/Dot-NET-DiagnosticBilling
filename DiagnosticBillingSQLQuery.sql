CREATE TABLE Patient (
    patientId INT PRIMARY KEY IDENTITY(1,1),
    patientName VARCHAR(100) NOT NULL,
    patientAge INT NOT NULL,
    patientGender CHAR(1) CHECK (patientGender IN ('M', 'F', 'E')) 
);
CREATE TABLE Doctor (
    doctorId INT PRIMARY KEY IDENTITY(1,1),
    doctorName VARCHAR(100) NOT NULL,
    specialty VARCHAR(100) NOT NULL
);
CREATE TABLE Test (
    testId INT PRIMARY KEY IDENTITY(1,1),
    testName VARCHAR(100) NOT NULL,
    price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE Bill (
    billId INT PRIMARY KEY IDENTITY(1,1),
    patientId INT,
    doctorId INT,
    totalCost DECIMAL(10, 2),
    todayDate DATE DEFAULT GETDATE(), 
    FOREIGN KEY (patientId) REFERENCES Patient(patientId),
    FOREIGN KEY (doctorId) REFERENCES Doctor(doctorId)
);

CREATE TABLE BillTest (
    BillId INT,
    TestId INT,
    PRIMARY KEY (BillId, TestId),
    FOREIGN KEY (BillId) REFERENCES Bill(billId),
    FOREIGN KEY (TestId) REFERENCES Test(testId)
);







DROP TABLE IF EXISTS Test;
DROP TABLE IF EXISTS Patient;
DROP TABLE IF EXISTS Doctor;
DROP TABLE IF EXISTS Bill;

SELECT * FROM [Test];
SELECT * FROM Patient;
SELECT * FROM Doctor;
SELECT * FROM [Bill];
SELECT * FROM [BillTest];


SET IDENTITY_INSERT Test ON;

SELECT COLUMN_NAME, 
       IS_NULLABLE, 
       COLUMN_DEFAULT, 
       DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Test';

SELECT * FROM Test;



INSERT INTO Test (testName, price) VALUES('Complete Blood Count (CBC)', 350);
INSERT INTO Test (testName, price) VALUES('Lipid Profile', 700);
INSERT INTO Test (testName, price) VALUES('Liver Function Test (LFT)', 600);
INSERT INTO Test (testName, price) VALUES('Kidney Function Test (KFT)', 600);
INSERT INTO Test (testName, price) VALUES('Thyroid Profile (T3, T4, TSH)', 600);
INSERT INTO Test (testName, price) VALUES('HbA1c (Glycosylated Hemoglobin)', 425);
INSERT INTO Test (testName, price) VALUES('Vitamin D Test', 1075);
INSERT INTO Test (testName, price) VALUES('Vitamin B12 Test', 950);
INSERT INTO Test (testName, price) VALUES('Prostate-Specific Antigen (PSA) Test', 950);
INSERT INTO Test (testName, price) VALUES('Anti-Mullerian Hormone (AMH) Test', 1925);
INSERT INTO Test (testName, price) VALUES('Ferritin Test', 705);
INSERT INTO Test (testName, price) VALUES('Homocysteine Test', 1250);
INSERT INTO Test (testName, price) VALUES('C-Reactive Protein (CRP) Test', 500);
INSERT INTO Test (testName, price) VALUES('Anti-CCP Antibody Test', 1450);
INSERT INTO Test (testName, price) VALUES('HLA-B27 Test', 2325);


INSERT INTO Doctor (doctorName, specialty) VALUES ('Dr. Aditi Sharma', 'Cardiologist');
INSERT INTO Doctor (doctorName, specialty) VALUES('Dr. Rajesh Iyer', 'Orthopedic Surgeon');
INSERT INTO Doctor (doctorName, specialty) VALUES('Dr. Kavita Menon', 'Dermatologist');

INSERT INTO Patient (patientName, patientAge, patientGender) VALUES ('Rahul Kumar', 30, 'M');
INSERT INTO Patient (patientName, patientAge, patientGender) VALUES ('Sita Devi', 45, 'F');
INSERT INTO Patient (patientName, patientAge, patientGender) VALUES ('Alex Thomas', 28, 'E');



        

