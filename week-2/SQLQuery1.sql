-- ============================================
-- EXERCISE 1: Create Database and Tables
-- ============================================

-- Create a new database called RetailStore
CREATE DATABASE RetailStore;
GO

-- Switch to using that database
USE RetailStore;
GO

-- ============================================
-- Create Customers table
-- ============================================
CREATE TABLE Customers (
    CustomerID   INT PRIMARY KEY IDENTITY(1,1),
    FirstName    NVARCHAR(50) NOT NULL,
    LastName     NVARCHAR(50) NOT NULL,
    Email        NVARCHAR(100) UNIQUE NOT NULL,
    Phone        NVARCHAR(15),
    City         NVARCHAR(50),
    CreatedDate  DATETIME DEFAULT GETDATE()
);
GO

-- ============================================
-- Create Products table
-- ============================================
CREATE TABLE Products (
    ProductID    INT PRIMARY KEY IDENTITY(1,1),
    ProductName  NVARCHAR(100) NOT NULL,
    Category     NVARCHAR(50),
    Price        DECIMAL(10,2) NOT NULL,
    StockQty     INT DEFAULT 0
);
GO

-- ============================================
-- Create Orders table
-- ============================================
CREATE TABLE Orders (
    OrderID      INT PRIMARY KEY IDENTITY(1,1),
    CustomerID   INT FOREIGN KEY REFERENCES Customers(CustomerID),
    OrderDate    DATETIME DEFAULT GETDATE(),
    TotalAmount  DECIMAL(10,2),
    Status       NVARCHAR(20) DEFAULT 'Pending'
);
GO

-- ============================================
-- Insert sample data into Customers
-- ============================================
INSERT INTO Customers (FirstName, LastName, Email, Phone, City)
VALUES
('Atasi',   'Priya',   'atasi@email.com',   '9876543210', 'Bhubaneswar'),
('Rahul',   'Sharma',  'rahul@email.com',   '9123456789', 'Delhi'),
('Priya',   'Singh',   'priya@email.com',   '9234567890', 'Mumbai'),
('Amit',    'Kumar',   'amit@email.com',    '9345678901', 'Bangalore'),
('Sneha',   'Das',     'sneha@email.com',   '9456789012', 'Kolkata');
GO

-- ============================================
-- Insert sample data into Products
-- ============================================
INSERT INTO Products (ProductName, Category, Price, StockQty)
VALUES
('Laptop',      'Electronics',  55000.00, 50),
('Phone',       'Electronics',  25000.00, 100),
('Headphones',  'Electronics',   3000.00, 200),
('Desk Chair',  'Furniture',    12000.00, 30),
('Notebook',    'Stationery',     150.00, 500);
GO

-- ============================================
-- Insert sample data into Orders
-- ============================================
INSERT INTO Orders (CustomerID, TotalAmount, Status)
VALUES
(1, 55000.00, 'Completed'),
(2, 28000.00, 'Completed'),
(3,  3000.00, 'Pending'),
(1, 12000.00, 'Completed'),
(4, 25000.00, 'Cancelled'),
(5,   150.00, 'Pending');
GO

-- ============================================
-- Verify everything was created correctly
-- ============================================
SELECT * FROM Customers;
SELECT * FROM Products;
SELECT * FROM Orders;