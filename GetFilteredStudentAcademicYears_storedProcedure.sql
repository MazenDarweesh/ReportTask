CREATE OR ALTER PROCEDURE GetFilteredStudentAcademicYears
    @SchoolId NVARCHAR(50) = NULL,
    @YearId NVARCHAR(50) = NULL,
    @GradeId NVARCHAR(50) = NULL,
    @ClassId NVARCHAR(50) = NULL
AS
BEGIN
    SELECT
        say.*,
        s.*,
        sc.*,
        c.*,
        g.*,
        sem.*
    FROM
        StudentAcademicYears say
    INNER JOIN
        Students s ON say.StudentId = s.Id
    INNER JOIN
        Schools sc ON say.SchoolId = sc.Id
    INNER JOIN
        Classrooms c ON say.ClassroomId = c.Id
    INNER JOIN
        Grades g ON say.GradeId = g.Id
    INNER JOIN
        Semesters sem ON say.SemesterId = sem.Id
    WHERE
        (@SchoolId IS NULL OR say.SchoolId = @SchoolId)
        AND (@YearId IS NULL OR sem.AcademicYearId = @YearId)
        AND (@GradeId IS NULL OR say.GradeId = @GradeId)
        AND (@ClassId IS NULL OR say.ClassroomId = @ClassId);
END