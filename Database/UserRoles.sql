CREATE login userLogin with password = 'user'

CREATE user HiveUser for login userLogin
exec sp_addrolemember 'db_datareader', 'HiveUser'
GO
CREATE SCHEMA HiveUser
GO

CREATE login adminLogin with password = 'admin'

CREATE user HiveAdmin for login adminLogin
exec sp_addrolemember 'db_datareader', 'HiveAdmin'
GO
CREATE SCHEMA HiveAdmin