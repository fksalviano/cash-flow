CREATE DATABASE [CashFlow]
go

USE [CashFlow]
go

CREATE TABLE [Transaction]
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