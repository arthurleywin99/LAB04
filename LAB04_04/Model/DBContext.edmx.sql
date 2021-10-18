
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/17/2021 22:49:00
-- Generated from EDMX file: C:\Users\truon\Desktop\LẬP TRÌNH TRÊN MÔI TRƯỜNG WINDOWS\CSHARP\LAB04\LAB04_04\Model\DBContext.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProductOrder];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProductOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_ProductOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_InvoiceOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_InvoiceOrder];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Invoices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Invoices];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [ProductID] nvarchar(20)  NOT NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [Unit] nvarchar(20)  NOT NULL,
    [BuyPrice] decimal(18,0)  NULL,
    [SellPrice] decimal(18,0)  NULL
);
GO

-- Creating table 'Invoices'
CREATE TABLE [dbo].[Invoices] (
    [InvoiceNo] nvarchar(20)  NOT NULL,
    [OrderDate] datetime  NOT NULL,
    [DeliveryDate] datetime  NOT NULL,
    [Note] nvarchar(255)  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [InvoiceNo] nvarchar(20)  NOT NULL,
    [No] int  NOT NULL,
    [ProductID] nvarchar(20)  NOT NULL,
    [ProductName] nvarchar(100)  NULL,
    [Unit] nvarchar(20)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ProductID] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([ProductID] ASC);
GO

-- Creating primary key on [InvoiceNo] in table 'Invoices'
ALTER TABLE [dbo].[Invoices]
ADD CONSTRAINT [PK_Invoices]
    PRIMARY KEY CLUSTERED ([InvoiceNo] ASC);
GO

-- Creating primary key on [No], [InvoiceNo] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([No], [InvoiceNo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [InvoiceNo] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_InvoiceOrder]
    FOREIGN KEY ([InvoiceNo])
    REFERENCES [dbo].[Invoices]
        ([InvoiceNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InvoiceOrder'
CREATE INDEX [IX_FK_InvoiceOrder]
ON [dbo].[Orders]
    ([InvoiceNo]);
GO

-- Creating foreign key on [ProductID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_ProductOrder]
    FOREIGN KEY ([ProductID])
    REFERENCES [dbo].[Products]
        ([ProductID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductOrder'
CREATE INDEX [IX_FK_ProductOrder]
ON [dbo].[Orders]
    ([ProductID]);
GO

INSERT INTO [dbo].[Invoices]
    ([InvoiceNo], [OrderDate], [DeliveryDate], [Note])
VALUES
    (N'HDX001', CAST(0x0000AAD900000000 AS DateTime), CAST(0x0000AADA00000000 AS DateTime), N'Giao hàng trước 9h'),
    (N'HDX002', CAST(0x0000AADA00000000 AS DateTime), CAST(0x0000AADA00000000 AS DateTime), N'Gọi điện trước khi giao'),
    (N'HDX003', CAST(0x0000AADA00000000 AS DateTime), CAST(0x0000AADC00000000 AS DateTime), N'Giao từ 1-3h')

-- Add Data to Product --
INSERT INTO [dbo].[Products]
    ([ProductID], [ProductName], [Unit], [BuyPrice], [SellPrice]) 
VALUES
    (N'Product1', N'Sản phẩm 1', N'Cái', CAST(100000 AS Decimal(18, 0)), CAST(120000 AS Decimal(18, 0))),
    (N'Product2', N'Sản phẩm 2', N'Cái', CAST(90000 AS Decimal(18, 0)), CAST(120000 AS Decimal(18, 0))),
    (N'Product3', N'Sản phẩm 3', N'Cái', CAST(40000 AS Decimal(18, 0)), CAST(70000 AS Decimal(18, 0))),
    (N'Product4', N'Sản phẩm 4', N'Hộp', CAST(200000 AS Decimal(18, 0)), CAST(300000 AS Decimal(18, 0)))

INSERT INTO [dbo].[Orders] 
    ([InvoiceNo], [No], [ProductID], [ProductName], [Unit], [Price], [Quantity]) 
VALUES
    (N'HDX001', 1, N'Product1', N'Sản phẩm 1', N'Cái', CAST(120000 AS Decimal(18, 0)), 20),
    (N'HDX001', 2, N'Product2', N'Sản phẩm 2', N'Cái', CAST(120000 AS Decimal(18, 0)), 4),
    (N'HDX001', 3, N'Product4', N'Sản phẩm 4', N'Hộp', CAST(300000 AS Decimal(18, 0)), 10),
    (N'HDX002', 1, N'Product4', N'Sản phẩm 1', N'Hộp', CAST(300000 AS Decimal(18, 0)), 10),
    (N'HDX002', 2, N'Product2', N'Sản phẩm 3', N'Cái', CAST(300000 AS Decimal(18, 0)), 12),
    (N'HDX003', 1, N'Product1', N'Sản phẩm 1', N'Cái', CAST(120000 AS Decimal(18, 0)), 40),
    (N'HDX003', 4, N'Product2', N'Sản phẩm 2', N'Cái', CAST(120000 AS Decimal(18, 0)), 60)

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------