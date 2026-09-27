-- 1. Get All Users Stored Procedure
CREATE PROCEDURE sp_GetUser
AS 
BEGIN
    SELECT UserId, FirstName, LastName, UserName, Email, Password, Address FROM Users;
END
GO

-- 2. Get User By Id Stored Procedure
CREATE PROCEDURE sp_GetUserById
    @userId INT
AS
BEGIN 
    SELECT UserId, FirstName, LastName, UserName, Email, Password, Address FROM Users WHERE UserId = @userId;
END
GO

-- 3. Add User Stored Procedure
CREATE PROCEDURE sp_AddUser
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @UserName NVARCHAR(100),
    @Email NVARCHAR(150),
    @Password NVARCHAR(100),
    @Address NVARCHAR(255)
AS
BEGIN
    INSERT INTO Users (FirstName, LastName, UserName, Email, Password, Address)
    VALUES (@FirstName, @LastName, @UserName, @Email, @Password, @Address);
    
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- 4. Update User Stored Procedure
CREATE PROCEDURE sp_UpdateUser
    @userId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @UserName NVARCHAR(100),
    @Email NVARCHAR(150),
    @Password NVARCHAR(100),
    @Address NVARCHAR(255)
AS
BEGIN
    UPDATE Users 
    SET FirstName = @FirstName,
        LastName = @LastName,
        UserName = @UserName, 
        Email = @Email, 
        Password = @Password,
        Address = @Address 
    WHERE UserId = @userId;
END
GO

-- 5. Delete User Stored Procedure
CREATE PROCEDURE sp_DeleteUser
    @userId INT 
AS
BEGIN
    DELETE FROM Users WHERE UserId = @userId;
END
GO
