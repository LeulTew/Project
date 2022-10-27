--TRIGGERS
--Item Order
use HIVE
GO
CREATE TRIGGER [Order Change]
ON ItemOrder
AFTER UPDATE
AS
BEGIN
	IF UPDATE(Amount) OR UPDATE(Quantity)
	BEGIN
		EXEC [Check ChangeLog Table]
		INSERT INTO ChangeLog(OrderID)
		Select OrderID FROM inserted
		UPDATE ChangeLog
		SET OldQuantity = (SELECT Quantity FROM deleted)
		, NewQuantity = (SELECT Quantity FROM inserted)
		, OldAmount = (SELECT Amount FROM deleted)
		, NewAmount = (SELECT Amount FROM inserted)
		, UserName = SUSER_NAME()
		, changeTime = GETDATE()
		WHERE OrderID = (SELECT OrderID FROM inserted)
	END
END
GO

--Admin
CREATE TRIGGER [Add Admin]
ON HiveAdmin.Adminstrator
AFTER INSERT, UPDATE
AS
BEGIN
	DECLARE @dob date = (Select DOB FROM inserted) 
	IF ( dbo.GetAge(@dob) < 18
		AND EXISTS (SELECT * FROM INSERTED 
					WHERE EmailAddress NOT LIKE '%_@__%.__%') )
		ROLLBACK TRANSACTION
	ELSE
	BEGIN
		UPDATE Adminstrator
		SET FName = dbo.ProperName(FName), LName = dbo.ProperName(LName)
		WHERE AdminId = (SELECT AdminId FROM inserted)
	END
END
GO

--Customer
CREATE TRIGGER [Add User]
ON HiveUser.Customer
AFTER INSERT, UPDATE
AS
BEGIN
	DECLARE @dob date = (Select DOB FROM inserted) 
	IF ( dbo.GetAge(@dob) < 18
		OR EXISTS (SELECT * FROM INSERTED 
					WHERE EmailAddress NOT LIKE '%_@__%.__%') )
		ROLLBACK TRANSACTION
	ELSE
	BEGIN
		UPDATE Customer
		SET FName = dbo.ProperName(FName), LName = dbo.ProperName(LName)
		WHERE Id = (SELECT Id FROM inserted)
	END
END
GO

--Item
--1
CREATE TRIGGER [DELETE Item Category]
ON Item
AFTER DELETE
AS
BEGIN
	DECLARE @catID int
	SELECT @catID = C.ID FROM ItemCategory C
			JOIN deleted I ON C.Id = I.CategoryID
	IF (SELECT count(I.ID) FROM Item I
			JOIN ItemCategory C ON I.CategoryID = C.Id WHERE C.Id != 1) <= 1  
		DELETE FROM ItemCategory
		WHERE Id = @catID
END
GO

--2
CREATE TRIGGER [Item Availability]
ON Item
AFTER INSERT
AS
BEGIN
	IF NOT NULL = (SELECT Item.ID FROM Item JOIN INSERTED ON Item.ID = inserted.ID)
	BEGIN
		ROLLBACK TRANSACTION
		UPDATE Item SET Quantity += (SELECT Quantity FROM INSERTED)
		WHERE ID = (SELECT ID From INSERTED)
	END
	DECLARE @exp date, @id int
	DECLARE checkAvail Cursor
	FOR
	SELECT ID, ExpirationDate FROM inserted
	OPEN checkAvail
	FETCH NEXT FROM checkAvail
	INTO @id, @exp
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF dbo.CheckExpiration(@exp) = 1
			OR (SELECT Quantity FROM Item 
				WHERE ID = @id) = 0
			UPDATE Item SET isAvail = 0
			WHERE ID = @id
		FETCH NEXT FROM checkAvail INTO @id, @exp
	END
	CLOSE checkAvail
	DEALLOCATE checkAvail
END
GO
