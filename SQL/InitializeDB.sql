ALTER TABLE Coursework
	DROP CONSTRAINT FK_Coursework_Student,
		 CONSTRAINT FK_Coursework_Department
GO
DROP TABLE Student
GO
DROP TABLE Department
GO
DROP TABLE Coursework
GO
CREATE TABLE Student (
	Id int PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(90) NOT NULL,
	Course int NOT NULL,
	BirthDate date NOT NULL
)
GO
CREATE TABLE Department (
	Id int PRIMARY KEY IDENTITY(1,1),
	Name nvarchar(90) NOT NULL
)
GO
CREATE TABLE Coursework (
	Id int PRIMARY KEY IDENTITY(1,1),
	StudentId int NOT NULL,
	DepartmentId int NOT NULL,
	DeliveryDate date DEFAULT (GETUTCDATE())
)
GO
ALTER TABLE Coursework
	ADD CONSTRAINT FK_Coursework_Student FOREIGN KEY (StudentId) REFERENCES Student(Id),
	    CONSTRAINT FK_Coursework_Department FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
GO
INSERT INTO Student VALUES ('Vladzimir Liashko', 1, '20020504'),
						   ('Ivanov Ivan', 2, '19990404'),
						   ('Petrov Vladislav', 2, '20010607'),
						   ('Nikitina Darya', 3, '20000302')
GO
INSERT INTO Department VALUES ('MPU'),
							  ('TYPK')
GO
INSERT INTO Coursework VALUES (1, 1, '20220123'),
							  (1, 1, '20220605'),
							  (2, 2, '20220105')