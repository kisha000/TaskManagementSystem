CREATE TABLE Clients_Surya (
	ClientId INT PRIMARY KEY IDENTITY(1,1),
	ClientName NVARCHAR(100) NOT NULL,
	ContactPerson NVARCHAR(100),
	Email NVARCHAR(100),
	Phone NVARCHAR(20),
	[Address] NVARCHAR(255),
	IsDeleted BIT DEFAULT 0
);

CREATE TABLE Projects_Surya (
	ProjectId INT PRIMARY KEY IDENTITY(1,1),
	ProjectName NVARCHAR(100) NOT NULL,
	[Description] NVARCHAR(MAX),
	StartDate DATE,
	EndDate DATE,
	ClientId INT,
	Status NVARCHAR(50) DEFAULT 'Open',
	PriorityLevel NVARCHAR(50),
	CreatedDate DATETIME DEFAULT GETDATE(),
	ModifiedDate DATETIME,
	IsDeleted BIT DEFAULT 0,
	CONSTRAINT FK_Projects_Surya_Clients FOREIGN KEY (ClientId)
		REFERENCES Clients_Surya(ClientId)
);

CREATE TABLE UserRoles_Surya (
	RoleId INT PRIMARY KEY IDENTITY(1,1),
	RoleName NVARCHAR(50) NOT NULL,
	Description NVARCHAR(255),
	CreatedDate DATETIME DEFAULT GETDATE(),
	IsDeleted BIT DEFAULT 0
);

CREATE TABLE Employees_Surya (
	EmployeeId INT PRIMARY KEY IDENTITY(1,1),
	EmployeeName NVARCHAR(100),
	[Address] NVARCHAR(200) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	PhoneNumber NVARCHAR(20),
	[Password] NVARCHAR(100) NOT NULL,
	RoleId INT,
	ProjectId INT,
	IsDeleted BIT DEFAULT 0,
	IsPasswordReset BIT DEFAULT 0,
	ResetLinkGeneratedAt DATETIME,
	CONSTRAINT UC_Email UNIQUE (Email),
	CONSTRAINT FK_Employees_Surya_UserRoles FOREIGN KEY (RoleId)
		REFERENCES UserRoles_Surya(RoleId)
);



CREATE TABLE Tasks_Surya (
	TaskId INT PRIMARY KEY IDENTITY(1,1),
	TaskName NVARCHAR(100) NOT NULL,
	ProjectId INT,
	TaskDescription NVARCHAR(MAX),
	StartDate DATETIME,
	EstimateDate DATETIME,
	PriorityLevel NVARCHAR(50),
	AttachmentFile NVARCHAR(255),
	Status NVARCHAR(50) DEFAULT 'Open',
	CreatedDate DATETIME DEFAULT GETDATE(),
	ModifiedDate DATETIME,
	EmployeeId INT NULL,  
	IsDeleted BIT DEFAULT 0,
	CONSTRAINT FK_Tasks_Surya_Projects FOREIGN KEY (ProjectId)
		REFERENCES Projects_Surya(ProjectId),
	CONSTRAINT FK_Tasks_Surya_Employees FOREIGN KEY (EmployeeId)
		REFERENCES Employees_Surya(EmployeeId)
);

CREATE TABLE ProjectEmployees_Surya (
	ProjectEmployeeId INT PRIMARY KEY IDENTITY(1,1),
	ProjectId INT,
	EmployeeId INT,
	CONSTRAINT FK_ProjectEmployees_Surya_Projects FOREIGN KEY (ProjectId)
		REFERENCES Projects_Surya(ProjectId),
	CONSTRAINT FK_ProjectEmployees_Surya_Employees FOREIGN KEY (EmployeeId)
		REFERENCES Employees_Surya(EmployeeId)
);

-- Insert admin role
INSERT INTO UserRoles_Surya (RoleName, Description)
VALUES ('Admin', 'Administrator role');

-- Insert user role
INSERT INTO UserRoles_Surya (RoleName, Description)
VALUES ('User', 'Regular user role');

-- Drop foreign key constraints first
--ALTER TABLE ProjectEmployees_Surya DROP CONSTRAINT FK_ProjectEmployees_Surya_Employees;
--ALTER TABLE ProjectEmployees_Surya DROP CONSTRAINT FK_ProjectEmployees_Surya_Projects;
--ALTER TABLE Tasks_Surya DROP CONSTRAINT FK_Tasks_Surya_Employees;
--ALTER TABLE Tasks_Surya DROP CONSTRAINT FK_Tasks_Surya_Projects;
--ALTER TABLE Employees_Surya DROP CONSTRAINT FK_Employees_Surya_UserRoles;
--ALTER TABLE Projects_Surya DROP CONSTRAINT FK_Projects_Surya_Clients;

-- Drop tables
--DROP TABLE ProjectEmployees_Surya;
--DROP TABLE Tasks_Surya;
--DROP TABLE Employees_Surya;
--DROP TABLE Projects_Surya;
--DROP TABLE UserRoles_Surya;
--DROP TABLE Clients_Surya;