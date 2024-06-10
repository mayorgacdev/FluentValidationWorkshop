CREATE DATABASE Workshop
GO

USE Workshop
GO

CREATE TABLE Customer (
    CustomerId UNIQUEIDENTIFIER PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    PassportNumber NVARCHAR(20) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    ModifiedDate DATETIME,
    DeletedDate DATETIME,
    IsEdit BIT NOT NULL DEFAULT 0,
    IsDelete BIT NOT NULL DEFAULT 0
);

CREATE PROCEDURE InsertCustomer
    @CustomerId UNIQUEIDENTIFIER,
    @FirstName NVARCHAR(100),
    @PassportNumber NVARCHAR(20),
    @PhoneNumber NVARCHAR(20),
    @Address NVARCHAR(255),
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Customer (CustomerId, FirstName, PassportNumber, PhoneNumber, Address, Email, CreatedDate)
        VALUES (@CustomerId, @FirstName, @PassportNumber, @PhoneNumber, @Address, @Email, GETDATE());

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION; 
        THROW;
    END CATCH
END
