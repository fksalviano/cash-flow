CREATE DATABASE [CashFlow]
go

USE [CashFlow]
go

CREATE TABLE [dbo].[Transaction]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Date] DATETIME NOT NULL,
    [Description] VARCHAR(50) NOT NULL,
    [Type] CHAR(1) NOT NULL,
    [Value] DECIMAL NOT NULL,
    CONSTRAINT PK_Transaction PRIMARY KEY (Id),
    CONSTRAINT CHK_Transaction_Type CHECK ([Type] in ('C', 'D'))
)
go

INSERT INTO [dbo].[Transaction] ([Id], [Date], [Description], [Type], [Value]) VALUES (NEWID(), GETDATE(), 'Test Credit 100', 'C', 100)
go
INSERT INTO [dbo].[Transaction] ([Id], [Date], [Description], [Type], [Value]) VALUES (NEWID(), GETDATE(), 'Test Credit 50', 'C', 100)
go
INSERT INTO [dbo].[Transaction] ([Id], [Date], [Description], [Type], [Value]) VALUES (NEWID(), GETDATE(), 'Test Debit 100', 'D', 100)
go