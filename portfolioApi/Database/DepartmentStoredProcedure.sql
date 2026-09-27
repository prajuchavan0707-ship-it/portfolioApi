-- 1. Get All Departments Stored Procedure
CREATE PROCEDURE sp_GetDepartment
AS 
BEGIN
    SELECT DepartmentId, DepartmentName, DepartmentCode, CreatedBy, CreatedOn FROM Department;
END
GO

-- 2. Get Department By Id Stored Procedure
CREATE PROCEDURE sp_GetDepartmentById
    @departmentId INT
AS
BEGIN 
    SELECT DepartmentId, DepartmentName, DepartmentCode, CreatedBy, CreatedOn FROM Department WHERE DepartmentId = @departmentId;
END
GO

-- 3. Add Department Stored Procedure
CREATE PROCEDURE sp_AddDepartment
    @DepartmentName NVARCHAR(100),
    @DepartmentCode NVARCHAR(50),
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Department (DepartmentName, DepartmentCode, CreatedBy, CreatedOn)
    VALUES (@DepartmentName, @DepartmentCode, @CreatedBy, GETDATE());
    
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- 4. Update Department Stored Procedure
CREATE PROCEDURE sp_UpdateDepartment
    @departmentId INT,
    @DepartmentName NVARCHAR(100),
    @DepartmentCode NVARCHAR(50),
    @ModifyBy Int
AS
BEGIN
    UPDATE Department 
    SET DepartmentName = @DepartmentName,
        DepartmentCode = @DepartmentCode,
        ModifyBy =@Modifyby,
        ModifyOn = GETDATE()
    WHERE DepartmentId = @departmentId;
END
GO

-- 5. Delete Department Stored Procedure
CREATE PROCEDURE sp_DeleteDepartment
    @departmentId INT 
AS
BEGIN
    DELETE FROM Department WHERE DepartmentId = @departmentId;
END
GO
