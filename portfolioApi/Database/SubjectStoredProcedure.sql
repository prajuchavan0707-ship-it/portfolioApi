create Procedure sp_GetSubject
as 
begin
	select SubjectId, faculty, CourseTitle, code, credit, modifyby, modifyon from Subjects;
end

create Procedure sp_GetSubjectById
	@subjectId int
as
begin 
	select SubjectId, faculty, CourseTitle, code, credit, modifyby, modifyon from Subjects where SubjectId= @subjectId;
end

create Procedure sp_AddSubject
	@faculty NVARCHAR(50),
	@CourseTitle NVARCHAR(50),
	@Code NVARCHAR(50),
	@Credit int,
	@CreatedBy int
as
begin
	insert into Subjects (faculty,CourseTitle,Code, Credit, CreatedBy, CreatedOn)
	values (@faculty,@CourseTitle,@Code, @Credit, @CreatedBy, GETDATE());
	select cast(SCOPE_IDENTITY() as int);
end

create Procedure sp_UpdateSubject
	@subjectId int,
	@faculty NVARCHAR(50),
	@Code NVARCHAR(50),
	@modifyby int
as
begin
	update Subjects set faculty = @faculty, Code= @Code, ModifyBy = @modifyby, ModifyOn = GetDate() where SubjectId = @subjectId;
end

go
create Procedure sp_DeleteSubject
	@subjectid int 
as
begin
	delete from Subjects where SubjectId= @subjectId;
end
