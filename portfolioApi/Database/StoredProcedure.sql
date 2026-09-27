--For Student Table

create Procedure sp_GetStudent
as
Begin 
	select Prn , FirstName, LastName, Gender,DateOfBirth,Email,ContactNo,CreatedBy , CreatedOn, IsActive From Student;
end

create Procedure sp_GetStudentById
	@Id INT
as
Begin
	select  Prn , FirstName, LastName, Gender,DateOfBirth,Email,ContactNo,CreatedBy , CreatedOn, IsActive from Student where Prn = @Id;
END

create Procedure sp_AddStudent
	@FirstName NVARCHAR(100) ,
	@LastName NVARCHAR(100) ,
	@Gender NVARCHAR(10) ,
	@DateOfBirth DateTime ,
	@Email NVARCHAR(50) ,
	@ContactNo NVARCHAR(20),
	@CreatedBy INT,
	@ISActive bit
as 
Begin 
	INSERT INTO Student (FirstName, LastName, Gender,DateOfBirth,Email,ContactNo,CreatedBy,CreatedOn,IsActive) 
	
	VALUES (@FirstName,@LastName,@Gender,@DateOfBirth,@Email,@ContactNo,@CreatedBy,GETDATE(),@ISActive);
	SELECT CAST(SCOPE_IDENTITY() AS INT);
end

CREATE PROCEDURE sp_UpdateStudent
	@Id INT,
	@DateOfBirth DateTime ,
	@Email NVARCHAR(50) ,
	@ContactNo NVARCHAR(20),
	@ModifyBy INT
AS
BEGIN
UPDATE Student SET  DateOfBirth=@DateOfBirth ,Email=@Email,ContactNo=@ContactNo,ModifyBy=@ModifyBy,ModifyOn=GETDATE() WHERE Prn = @Id;
END


CREATE PROCEDURE sp_DeleteStudent
@Id INT
AS
BEGIN
DELETE FROM Student WHERE Prn = @Id;
END
