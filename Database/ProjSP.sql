--PROCS
use HIVE
GO
--Customer 
	--1
CREATE PROCEDURE [Insert User]
@Username varchar(20), @Name varchar(50), @passw varchar(50), @Email varchar(max), @dob Date, @gender varchar(10), @phone varchar(11) = NULL
AS
BEGIN
	INSERT INTO HiveUser.Customer Values
	(dbo.genID(), @Username, @passw, dbo.getFirstName(@Name), dbo.getLastName(@Name), @dob, @Email, @phone, @gender)
END
GO
--SP_Help [Insert User]
	--2
CREATE PROCEDURE [Verify User Password]
@Username varchar(20), @passw varchar(50)
AS
	RETURN dbo.[Check User Password] (@Username, @passw)
		
GO
	--3
CREATE PROCEDURE [DELETE User]
@Username varchar(20)
AS
BEGIN
	DELETE FROM HiveUser.Customer
	WHERE Username = @Username
END
GO
	--4
CREATE PROCEDURE [Display All User]
AS
BEGIN
	SELECT * FROM HiveUser.Customer
END
GO
	--5
CREATE PROCEDURE [Search User By Username]
@in varchar(20)
AS
BEGIN
	SELECT * FROM HiveUser.Customer
	WHERE Username LIKE @in+'%'
END
GO
	--6
CREATE PROCEDURE [Search User By Email]
@in varchar(20)
AS
BEGIN
	SELECT * FROM HiveUser.Customer
	WHERE EmailAddress LIKE @in+'%'
END
GO
--7
CREATE PROCEDURE [Update User]
@id varchar(6), @uN varchar(20), @pass varchar(50), @fN varchar(24),
@lN varchar(24), @dob date, @email varchar(50), @gender varchar(10)
AS
BEGIN
	UPDATE HiveUser.Customer
	SET Username = @uN, Pass = @pass, FName = @fN, LName = @lN
		, DOB = @dob, EmailAddress = @email, Gender = @gender
	WHERE Id = @id
END
GO

--Admin
	--1
CREATE PROCEDURE [Insert Admin]
@Username varchar(20), @Name varchar(50), @passw varchar(50), @Email varchar, @dob Date, @gender varchar(10), @phone varchar(11) = NULL
AS
BEGIN
	INSERT INTO HiveAdmin.Adminstrator Values
	(@Username, @passw, dbo.getFirstName(@Name), dbo.getLastName(@Name), @Email, @dob, @gender)
END
GO
	--2
CREATE PROCEDURE [DELETE Admin]
@Username varchar(20)
AS
BEGIN
	DELETE FROM HiveAdmin.Adminstrator
	WHERE Username = @Username
END
GO
	--3
CREATE PROCEDURE [Display All Admin]
AS
BEGIN
	SELECT * FROM HiveAdmin.Adminstrator 
END
GO
	--4
CREATE PROCEDURE [Search Admin By Username]
@in varchar(20)
AS
BEGIN
	SELECT * FROM HiveAdmin.Adminstrator
	WHERE Username LIKE @in+'%'
END
GO
	--5
CREATE PROCEDURE [Search Admin By Email]
@in varchar(20)
AS
BEGIN
	SELECT * FROM HiveAdmin.Adminstrator
	WHERE EmailAddress LIKE @in+'%'
END
GO


--Company
	--1
CREATE PROCEDURE [Insert Company]
@Name varchar(255), @address varchar(30), @phone int = NULL
AS
BEGIN
	INSERT INTO Company Values
	(@Name, @phone, @address)
END
GO
	--2
CREATE PROCEDURE [DELETE Company]
@Cname varchar(255)
AS
BEGIN
	DELETE FROM Company
	WHERE Cname = @Cname
END
GO
	--3
CREATE PROCEDURE [Display Companies]
AS
BEGIN
	SELECT * FROM Company 
END
GO
	--4
CREATE PROCEDURE [Update Company]
@Cname varchar(255), @address varchar(30), @tel varchar(30)
AS
BEGIN
	IF EXISTS (SELECT * FROM Company WHERE Cname = @Cname)
	BEGIN
	UPDATE Company SET Address = @address, Phone = @tel
	WHERE Cname = @Cname
	END
END
GO
	--5
CREATE PROCEDURE [Search Company By Name]
@in varchar(20)
AS
BEGIN
	SELECT * FROM dbo.Company
	WHERE Cname LIKE @in+'%'
END
GO


--Item and Category
	--1
CREATE PROCEDURE [Insert Item]
@itemN varchar(50), @price money, @itemPic varchar(100), @Desc varchar(max) = Null, @qty int = 0, @exp date = Null, @catN varchar(50)
AS
BEGIN
	DECLARE @catID int, @isAvail bit = 0
	IF NOT EXISTS (SELECT * FROM ItemCategory WHERE CategoryName = @catN)
	BEGIN
		INSERT INTO ItemCategory values (@catN)
		SET @catID = @@IDENTITY
	END
	ELSE
		SELECT @catID = ID FROM ItemCategory WHERE CategoryName = @catN
	IF @qty>0
		SET @isAvail = 1
	INSERT INTO Item Values
	(@itemN, @price, @itemPic, @Desc, @qty, @exp, @isAvail, @catID)
END
GO
	--2
CREATE PROCEDURE [DELETE Item]
@itemN varchar(50)
AS
BEGIN
	DELETE FROM Item
	WHERE ItemName = @itemN
END
GO
	--3
CREATE PROCEDURE [Display All Item]
AS
BEGIN
	SELECT * FROM Item 
END
GO
	--4
CREATE PROCEDURE [Remove Item Stock]
@itemN varchar(50), @qty int
AS
BEGIN
	IF EXISTS (SELECT * FROM Item WHERE ItemName = @itemN)
	BEGIN
		UPDATE Item SET Quantity -= @qty
		WHERE ItemName = @itemN
		IF (SELECT Quantity FROM Item WHERE ItemName = @itemN) < 0
			UPDATE Item SET Quantity = 0 WHERE ItemName = @itemN
		IF (SELECT Quantity FROM Item WHERE ItemName = @itemN) = 0
			UPDATE Item SET isAvail = 0 WHERE ItemName = @itemN
	END
END
GO
	--5
CREATE PROCEDURE [Add Item Stock]
@itemN varchar(50), @qty int
AS
BEGIN
	IF EXISTS (SELECT * FROM Item WHERE ItemName = @itemN)
	BEGIN
		UPDATE Item SET Quantity += @qty
		WHERE ItemName = @itemN
		IF (SELECT Quantity FROM Item WHERE ItemName = @itemN) > 0
			UPDATE Item SET isAvail = 1 WHERE ItemName = @itemN
	END
END	
GO
	--6
CREATE PROCEDURE [Search Item By Name]
@in varchar(50)
AS
BEGIN
	SELECT * FROM dbo.Item
	WHERE ItemName LIKE @in+'%'
END
GO
	--7
CREATE PROCEDURE [Search Item By Category]
@in varchar(50)
AS
BEGIN
	SELECT * FROM dbo.Item I JOIN dbo.ItemCategory C
		ON I.CategoryID = C.Id
	WHERE CategoryName LIKE @in+'%'
END
GO
	--8
CREATE PROCEDURE [Update Item]
@id int, @itemN varchar(50), @price money, @itemPic varchar(100), @Desc varchar(max) = Null, @qty int = 0, @exp date = Null, @catN varchar(50)
AS
BEGIN
	DECLARE @catID int, @isAvail bit = 0
	IF NOT EXISTS (SELECT * FROM ItemCategory WHERE CategoryName = @catN)
	BEGIN
		INSERT INTO ItemCategory values (@catN)
		SET @catID = @@IDENTITY
		IF @qty>0
			SET @isAvail = 1
	END
	ELSE
	BEGIN
		SELECT @catID = ID FROM ItemCategory WHERE CategoryName = @catN
		IF ((SELECT Quantity FROM Item WHERE ID = @id) > 0 OR @qty > 0) AND dbo.CheckExpiration(@exp) != 1
			SET @isAvail = 1
		ELSE
			SET @isAvail = 0
	END
	UPDATE Item
	SET  ItemName = @itemN, Price = @price, ItemPic = @itemPic, iDescription = @Desc
		, Quantity = @qty, ExpirationDate = @exp, isAvail = @isAvail, CategoryID = @catID
	WHERE Id = @id
END
GO


--Transaction (Order)
	--1
CREATE PROCEDURE [Order Item]
@qty int, @itemName varchar(50), @userName varchar(20), @CompanyN varchar(255) = NULL
AS
BEGIN
	IF EXISTS (SELECT * FROM Item WHERE ItemName = @itemName) AND EXISTS(SELECT * FROM HiveUser.Customer WHERE Username = @userName) 
	BEGIN
		DECLARE @itemID int, @custID varchar(6),  @price money, @cID int
		IF EXISTS (SELECT * FROM dbo.Company WHERE Cname LIKE @CompanyN+'%' )
			SELECT @cID = Cid FROM dbo.Company WHERE Cname LIKE @CompanyN+'%'
		ELSE
			SET @cID = NULL
		SELECT @itemID = ID FROM Item WHERE ItemName = @itemName
		SELECT @custID = Id FROM HiveUser.Customer WHERE Username = @userName
		SELECT @price = Price FROM Item WHERE ID = @itemID
		BEGIN TRANSACTION
		INSERT into ItemOrder Values
		(GETDATE(), @qty, dbo.[Calculate Total](@price, @qty), @itemID, @custID, @cID)
		UPDATE Item SET Quantity -= @qty
		WHERE ID = @itemID
		COMMIT TRANSACTION
	END
END
GO
	--2
CREATE PROCEDURE [Display All Order]
AS
BEGIN
	SELECT * FROM ItemOrder
END
GO
	--3
CREATE PROCEDURE [Change Order Amount]
@orderID int, @qty int
AS
BEGIN
	IF EXISTS (SELECT * FROM ItemOrder WHERE OrderID = @orderID)
	BEGIN
		DECLARE @price money, @itemID int, @oldQty int
		BEGIN TRANSACTION
		SELECT @itemID = I.ID, @price = I.Price, @oldQty = (I.Quantity+ O.Quantity) FROM Item I JOIN ItemOrder O ON I.ID=O.ItemID
		UPDATE ItemOrder SET Amount = dbo.[Calculate Total](@price, @qty), Quantity = @qty
		WHERE OrderID = @orderID
		UPDATE Item SET Quantity = (@oldQty - @qty)
		WHERE ID = @itemID
		COMMIT TRANSACTION
	END
END
GO
	--4
CREATE PROCEDURE [Check ChangeLog Table]
AS
BEGIN
	IF NOT EXISTS ( SELECT T.name 
					FROM sys.tables T
					WHERE T.name = 'ChangeLog' )
			CREATE TABLE ChangeLog(
				LogID int IDENTITY(1,1) PRIMARY KEY,
				OrderID int FOREIGN KEY REFERENCES ItemOrder(OrderID),
				OldQuantity int NULL,
				NewQuantity int NULL,
				OldAmount money NULL,
				NewAmount money NULL,
				UserName varchar(100),
				changeTime Date
			);
END
GO
	--5
CREATE PROCEDURE [View Customer Transaction]
@user varchar(20)
AS
BEGIN
	SELECT * FROM [Items Purchased] (@user)
END
GO
	--6
CREATE PROCEDURE [Display User Order]
@user varchar(20), @ord int
AS
BEGIN
	SELECT TOP (@ord) OrderID, ItemName, OrderDate, I.Quantity, Amount, ItemID, CustID, CompanyID 
	FROM ItemOrder I JOIN HiveUser.Customer C ON I.CustID = C.Id 
	JOIN Item it ON I.ItemID = it.ID
	WHERE C.Username = @user ORDER BY I.OrderID DESC 
END
GO
	--7
CREATE PROCEDURE [Purchased Items]
AS
BEGIN
	SELECT DISTINCT I.ItemName, I.ID, C.CategoryName, I.isAvail, I.ExpirationDate 
	FROM Item I JOIN ItemOrder O ON I.ID = O.ItemID JOIN ItemCategory C ON C.Id = I.CategoryID
END
GO

--CHANGES LOG
--1
CREATE PROCEDURE [UNDO Change]
@ChangeDate Date
AS
BEGIN
	UPDATE ItemOrder
	SET Quantity = (SELECT OldQuantity FROM ChangeLog
				WHERE changeTime = @ChangeDate)
	WHERE OrderID = (SELECT OrderID FROM ChangeLog
					WHERE changeTime = @ChangeDate)
	DELETE FROM ChangeLog
	WHERE changeTime = @ChangeDate
END
GO
--2
CREATE PROCEDURE [Display Changes]
@ChangeDate Date
AS
BEGIN
SELECT * FROM ChangeLog
WHERE changeTime = @ChangeDate
END

/*
UPDATE ItemOrder
SET Quantity = 7
WHERE OrderID =16*/
-- EXEC [Change Order Amount] 2