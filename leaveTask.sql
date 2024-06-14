create database Hrms;
use Hrms;



CREATE TABLE LeaveRequests (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    EmpID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FromDate DATE NOT NULL,
    ToDate DATE NOT NULL,
    Reason NVARCHAR(MAX) NOT NULL,
    TotalDays INT NOT NULL,
    AbsentDays INT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Pending'
);




CREATE TABLE Emp (
    EmpID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Contact NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    Role NVARCHAR(100) NOT NULL,
    DateOfJoining DATE NOT NULL,
    Salary DECIMAL(10,2) DEFAULT 5000,
    LeaveBalance INT DEFAULT 2,
    LastUpdated DATE DEFAULT GETDATE()
	Designation NVARCHAR(100)
);

 select * from Emp;
 
ALTER TABLE Emp
ADD Designation NVARCHAR(100) NOT NULL;
 insert into Emp values('HRVaish','8108136181','vaish00721@gmail.com','vaishnav','admin','1/12/2022','5000',2,GETDATE(),'AdminHR');

 select * from OfferLetters;

 CREATE TABLE OfferLetters (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    GeneratedDate DATETIME,
    FilePath NVARCHAR(200)
);



CREATE TABLE OfferLetters (
    Id INT PRIMARY KEY IDENTITY,
    EmpId NVARCHAR(50),
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    GeneratedDate DATETIME,
    FilePath NVARCHAR(200)
);

CREATE TABLE PaySlips (
    Id INT PRIMARY KEY IDENTITY,
    EmpNo VARCHAR(50),
    Month VARCHAR(50),
    FilePath VARCHAR(255)
);

CREATE TABLE Tickets (
    TicketID INT IDENTITY(1,1) PRIMARY KEY,
    RaisedBy INT NOT NULL FOREIGN KEY REFERENCES Emp(EmpID),
    RaisedByName NVARCHAR(100) NOT NULL,
    RaisedTo INT NOT NULL,
    RaisedToName NVARCHAR(100) NOT NULL,
    Designation NVARCHAR(100) NOT NULL,
    TicketDescription NVARCHAR(MAX) NOT NULL,
    Attachment NVARCHAR(255),
    RaisedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE ClosedTickets (
    TicketID INT PRIMARY KEY,
    RaisedBy INT NOT NULL,
    RaisedByName NVARCHAR(100) NOT NULL,
    RaisedTo INT NOT NULL,
    RaisedToName NVARCHAR(100) NOT NULL,
    Designation NVARCHAR(100) NOT NULL,
    TicketDescription NVARCHAR(MAX) NOT NULL,
    Solution NVARCHAR(MAX) NOT NULL,
    ClosedDate DATETIME DEFAULT GETDATE()
);

alter table ClosedTickets
add RaisedDate DATETIME DEFAULT GETDATE();

drop table ClosedTickets;

select * from Tickets;
select * from ClosedTickets;


select * from PaySlips;
select * from LeaveRequests;
select * from Emp;
select * from OfferLetters;


drop table Emp;

select * from Emp;
select * from OfferLetters;
select * from PaySlips;

truncate table PaySlips;
truncate table OfferLetters;
truncate table LeaveRequests;
truncate table Emp;
truncate table Tickets;
