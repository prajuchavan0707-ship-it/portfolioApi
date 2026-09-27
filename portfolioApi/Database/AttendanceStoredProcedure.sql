-- 1. Get All Attendance Stored Procedure
CREATE PROCEDURE sp_GetAttendance
AS 
BEGIN
    SELECT AttendanceId, StudentPrn, SubjectId, AttendanceDate, Status, ModifyBy, ModifyOn FROM Attendances;
END
GO

-- 2. Get Attendance By Id Stored Procedure
CREATE PROCEDURE sp_GetAttendanceById
    @attendanceId INT
AS
BEGIN 
    SELECT AttendanceId, StudentPrn, SubjectId, AttendanceDate, Status, ModifyBy, ModifyOn FROM Attendances WHERE AttendanceId = @attendanceId;
END
GO

-- 3. Add Attendance Stored Procedure
CREATE PROCEDURE sp_AddAttendance
    @StudentPrn  INT,
    @SubjectId INT,
    @AttendanceDate DATE,
    @Status NVARCHAR(50),
    @CreatedBy INT
AS
BEGIN
    INSERT INTO Attendances (StudentPrn, SubjectId, AttendanceDate, Status,CreatedBy, CreatedOn)
    VALUES (@StudentPrn, @SubjectId, @AttendanceDate, @Status, @CreatedBy, GETDATE());
    
    SELECT CAST(SCOPE_IDENTITY() AS INT);
END
GO

-- 4. Update Attendance Stored Procedure
CREATE PROCEDURE sp_UpdateAttendance
    @attendanceId INT,
    @StudentPrn  INT,
    @SubjectId INT,
    @AttendanceDate DATE,
    @Status NVARCHAR(50),
    @ModifyBy INT
AS
BEGIN
    UPDATE Attendances 
    SET StudentPrn = @StudentPrn,
        SubjectId = @SubjectId,
        AttendanceDate = @AttendanceDate,
        Status = @Status,
        ModifyBy = @ModifyBy,
        ModifyOn = GETDATE()
    WHERE AttendanceId = @attendanceId;
END
GO

-- 5. Delete Attendance Stored Procedure
CREATE PROCEDURE sp_DeleteAttendance
    @attendanceId INT 
AS
BEGIN
    DELETE FROM Attendances WHERE AttendanceId = @attendanceId;
END
GO
