--FUNCTIONS 
use HIVE
GO
--1
CREATE VIEW RandomVal
AS
SELECT CAST (ROUND(RAND() * 10000, 0) AS int) AS RandValue

GO
CREATE FUNCTION genID()
RETURNS varchar(6)
AS
BEGIN
	DECLARE @tempID varchar(6)
		, @nextN int
		, @flag bit =0, @ch1 char, @ch2 char
	WHILE @flag = 0
		BEGIN
		SET @nextN = (SELECT RandValue FROM RandomVal)
		SET @ch1 = CHAR(65 + (SELECT RandValue FROM RandomVal) % 26)
		SET @ch2 = CHAR(65 + (SELECT RandValue FROM RandomVal) % 26)
		SET @tempID = CONCAT( @ch1, @ch2
				, CASE 
					WHEN @nextN < 10 THEN '000'
					WHEN @nextN < 100 THEN '00'
					WHEN @nextN < 1000 THEN '0'
				END
				, CAST(@nextN as char(4)) )
		IF EXISTS (SELECT * FROM HiveUser.Customer WHERE ID = @tempID)
			SET @flag = 0
		else
			SET @flag = 1
		END
	RETURN @tempID
END
GO

--2
CREATE FUNCTION getFirstName(@Name varchar(50))
RETURNS varchar(24)
AS
BEGIN
	RETURN LEFT(@Name, (CHARINDEX(' ', @Name)-1))
END
GO

--3
CREATE FUNCTION getLastName(@Name varchar(50))
RETURNS varchar(24)
AS
BEGIN
	RETURN RIGHT(@Name, (CHARINDEX(' ', REVERSE(@Name))-1))
END
GO

--4
CREATE FUNCTION [Check User Password](@Username varchar(20), @passw varchar(50))
RETURNS bit
AS
BEGIN
	IF @passw = (SELECT Pass FROM HiveUser.Customer
					WHERE Username = @Username)
		RETURN 1
	RETURN 0
END
GO

--5
CREATE FUNCTION [Calculate Total] (@price money, @qty int)
RETURNS money
AS
BEGIN
	RETURN @price*@qty
END
GO

--6
CREATE FUNCTION [Items Purchased] (@userN varchar(20))
RETURNS @items TABLE(
	itemID int,
	itemName varchar(50),
	OrderQty int
)
AS
BEGIN
	DECLARE @OrderID int
	SELECT @OrderID = OrderID
	FROM ItemOrder O JOIN HiveUser.Customer C ON O.CustID =C.Id
	WHERE C.Username = @userN

	INSERT @items(itemID, itemName, OrderQty)
	SELECT DISTINCT I.ID, I.ItemName, sum(O.Quantity)
	FROM ItemOrder O
	JOIN Item I ON I.ID = O.ItemID
	JOIN HiveUser.Customer C ON C.Id = O.CustID
	WHERE C.Username = @userN
	GROUP BY I.ID, I.ItemName
	RETURN
END
GO

--7
CREATE FUNCTION ProperName(@name varchar(24))
RETURNS varchar(24)
AS
BEGIN
	RETURN CONCAT(UPPER(LEFT(@name,1)), LOWER(RIGHT(@name, LEN(@name)-1)))
END
GO

--8
CREATE FUNCTION GetAge(@dob date)
RETURNS int
AS
BEGIN
	RETURN  DATEDIFF(YEAR, @dob, GETDATE()) -
		CASE
			WHEN ( MONTH(@dob) > MONTH(GETDATE()) ) OR
				(MONTH(@dob) = MONTH(GETDATE()) AND
					DAY(@dob) > DAY(GETDATE()))
			THEN 1
		ELSE 0
		END
END
GO

--9
CREATE FUNCTION CheckExpiration(@in date)
RETURNS bit
AS
BEGIN
	IF DATEDIFF(DAY, GETDATE(), @in) > 0
		RETURN 0
	RETURN 1
END
GO