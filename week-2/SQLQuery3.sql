-- ============================================
-- EXERCISE 3: Stored Procedures
-- ============================================

USE RetailStore;
GO

-- ============================================
-- Procedure 1: Get all orders for a customer
-- ============================================
CREATE PROCEDURE GetCustomerOrders
    @CustomerID INT
AS
BEGIN
    SELECT 
        o.OrderID,
        c.FirstName + ' ' + c.LastName AS CustomerName,
        o.OrderDate,
        o.TotalAmount,
        o.Status
    FROM Orders o
    INNER JOIN Customers c ON o.CustomerID = c.CustomerID
    WHERE o.CustomerID = @CustomerID;
END;
GO

-- ============================================
-- Procedure 2: Add a new product
-- ============================================
CREATE PROCEDURE AddProduct
    @ProductName NVARCHAR(100),
    @Category    NVARCHAR(50),
    @Price       DECIMAL(10,2),
    @StockQty    INT
AS
BEGIN
    INSERT INTO Products (ProductName, Category, Price, StockQty)
    VALUES (@ProductName, @Category, @Price, @StockQty);

    PRINT 'Product added successfully!';
END;
GO

-- ============================================
-- Procedure 3: Update order status
-- ============================================
CREATE PROCEDURE UpdateOrderStatus
    @OrderID INT,
    @NewStatus NVARCHAR(20)
AS
BEGIN
    UPDATE Orders
    SET Status = @NewStatus
    WHERE OrderID = @OrderID;

    PRINT 'Order status updated to: ' + @NewStatus;
END;
GO

-- ============================================
-- TEST: Execute the procedures
-- ============================================

-- Get orders for Customer 1 (Atasi)
EXEC GetCustomerOrders @CustomerID = 1;

-- Add a new product
EXEC AddProduct 
    @ProductName = 'Keyboard',
    @Category    = 'Electronics',
    @Price       = 1500.00,
    @StockQty    = 150;

-- Check the new product was added
SELECT * FROM Products;

-- Update Order 3 status from Pending to Completed
EXEC UpdateOrderStatus @OrderID = 3, @NewStatus = 'Completed';

-- Check the order was updated
SELECT * FROM Orders WHERE OrderID = 3;
GO