


CREATE PROCEDURE AddAnnouncement
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Status NVARCHAR(50),
    @Category NVARCHAR(100),
    @SubCategory NVARCHAR(100)
AS
BEGIN
    INSERT INTO Announcements (Title, Description, CreatedDate, Status, Category, SubCategory)
    VALUES (@Title, @Description, GETDATE(), @Status, @Category, @SubCategory)
END
GO


CREATE PROCEDURE GetAllAnnouncements
AS
BEGIN
    SELECT * FROM Announcements
END
GO

CREATE PROCEDURE GetAnnouncementById
    @Id INT
AS
BEGIN
    SELECT * FROM Announcements WHERE Id = @Id
END
GO


CREATE PROCEDURE UpdateAnnouncement
    @Id INT,
    @Title NVARCHAR(255),
    @Description NVARCHAR(MAX),
    @Status NVARCHAR(50),
    @Category NVARCHAR(100),
    @SubCategory NVARCHAR(100)
AS
BEGIN
    UPDATE Announcements
    SET
        Title = @Title,
        Description = @Description,
        Status = @Status,
        Category = @Category,
        SubCategory = @SubCategory
    WHERE Id = @Id
END
GO


CREATE PROCEDURE DeleteAnnouncement
    @Id INT
AS
BEGIN
    DELETE FROM Announcements WHERE Id = @Id
END
GO
