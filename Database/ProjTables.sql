CREATE DATABASE HIVE
GO
USE HIVE
GO
CREATE TABLE HiveAdmin.Adminstrator(
	AdminId int identity PRIMARY KEY,
	Username varchar(20) UNIQUE NOT NULL,
	Pass varchar(50),
	FName varchar(24) NOT NULL,
	LName varchar(24) NOT NULL,
	EmailAddress varchar(50) NOT NULL UNIQUE,
	DOB date NOT NULL,
	Gender char NOT NULL,
	CONSTRAINT UC_Admin UNIQUE(Pass)
);
GO

CREATE TABLE HiveUser.Customer(
	Id varchar(6) PRIMARY KEY,
	Username varchar(20) UNIQUE NOT NULL,
	Pass varchar(50),
	FName varchar(24) NOT NULL,
	LName varchar(24) NOT NULL,
	DOB date NOT NULL,
	EmailAddress varchar(50) NOT NULL UNIQUE,
	Phone varchar(11),
	Gender varchar(10) NOT NULL
);

GO

CREATE TABLE ItemCategory(
	Id int PRIMARY KEY identity(1,1),
	CategoryName varchar(50) NOT NULL
);
GO

CREATE TABLE Item(
	ID int PRIMARY KEY identity(1,1),
	ItemName varchar(50) NOT NULL UNIQUE,
	Price money NOT NULL,
	ItemPic varchar(100),
	iDescription varchar(max),
	Quantity int,
	ExpirationDate DATE,
	isAvail bit,
	CategoryID int FOREIGN KEY REFERENCES ItemCategory(Id)
);
GO

CREATE TABLE Company(
	Cid int Primary Key IDENTITY,
	Cname varchar(255) Not Null UNIQUE,
	Phone int,
	Address varchar(30) Not Null
);
GO

CREATE TABLE ItemOrder(
	OrderID int PRIMARY KEY IDENTITY(1,1),
	OrderDate date DEFAULT(GETDATE()),
	Quantity int,
	Amount Money NOT NULL,
	ItemID int FOREIGN KEY REFERENCES Item(ID),
	CustID varchar(6) FOREIGN KEY REFERENCES HiveUser.Customer(Id),
	CompanyID int FOREIGN KEY REFERENCES Company(Cid) NULL
);


