-- 1. Get All Faculty Stored Procedure
CREATE PROCEDURE sp_GetFaculty
AS 
BEGIN
    SELECT FacultyId, FirstName, LastName, Gender, Subjects, Email, Phone, Salary, ModifyBy, ModifyOn FROM Faculties;
END
GO

-- 2. Get Faculty By Id Stored Procedure
CREATE PROCEDURE sp_GetFacultyById
    @facultyId INT
AS
BEGIN 
    SELECT FacultyId, FirstName, LastName, Gender, Subjects, Email, Phone, Salary, ModifyBy, ModifyOn FROM Faculties WHERE FacultyId = @facultyId;
END
GO

-- 3. Add Faculty Stored Procedure
CREATE PROCEDURE sp_AddFaculty
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Gender NVARCHAR(20),
    @Subjects NVARCHAR(255),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @Salary DECIMAL(18, 2),
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Faculties (FirstName, LastName, Gender, Subjects, Email, Phone, Salary, CreatedBy, CreatedOn)
    VALUES (@FirstName, @LastName, @Gender, @Subjects, @Email, @Phone, @Salary, @CreatedBy, GETDATE());
    
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- 4. Update Faculty Stored Procedure
CREATE PROCEDURE sp_UpdateFaculty
    @facultyId INT,
    @FirstName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @Gender NVARCHAR(20),
    @Subjects NVARCHAR(255),
    @Email NVARCHAR(150),
    @Phone NVARCHAR(50),
    @Salary DECIMAL(18, 2),
    @ModifyBy INT,
    @ModifyOn DATETIME
AS
BEGIN
    UPDATE Faculties 
    SET FirstName = @FirstName,
        LastName = @LastName,
        Gender = @Gender,
        Subjects = @Subjects,
        Email = @Email,
        Phone = @Phone,
        Salary = @Salary,
        ModifyBy = @ModifyBy,
        ModifyOn = GETDATE() 
    WHERE FacultyId = @facultyId;
END
GO

-- 5. Delete Faculty Stored Procedure
CREATE PROCEDURE sp_DeleteFaculty
    @facultyId INT 
AS
BEGIN
    DELETE FROM Faculties WHERE FacultyId = @facultyId;
END
GO
