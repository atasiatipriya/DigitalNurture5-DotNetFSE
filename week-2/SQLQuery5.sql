-- ============================================
-- EXERCISE 5: Triggers
-- ============================================

USE RetailStore;
GO

-- First add an OrderItems table to track what
-- products are in each order
CREATE TABLE OrderItems (
    OrderItemID  INT PRIMARY KEY IDENTITY(1,1),
    OrderID      INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductID    INT FOREIGN KEY REFERENCES Products(ProductID),
    Quantity     INT NOT NULL,
    UnitPrice    DECIMAL(10,2) NOT NULL
);
GO

-- ============================================
-- TRIGGER 1: After INSERT on OrderItems
-- Automatically reduces product stock
-- when a new order item is added
-- ============================================
CREATE TRIGGER trg_ReduceStock
ON OrderItems
AFTER INSERT
AS
BEGIN
    -- 'inserted' is a special table that holds
    -- the newly inserted rows
    UPDATE Products
    SET StockQty = StockQty - inserted.Quantity
    FROM Products
    INNER JOIN inserted ON Products.ProductID = inserted.ProductID;

    PRINT 'Stock updated automatically by trigger!';
END;
GO

-- ============================================
-- TRIGGER 2: After DELETE on Orders
-- Logs deleted orders (audit trail)
-- ============================================

-- First create an audit log table
CREATE TABLE OrderAuditLog (
    LogID       INT PRIMARY KEY IDENTITY(1,1),
    OrderID     INT,
    CustomerID  INT,
    TotalAmount DECIMAL(10,2),
    DeletedAt   DATETIME DEFAULT GETDATE()
);
GO

CREATE TRIGGER trg_LogDeletedOrder
ON Orders
AFTER DELETE
AS
BEGIN
    INSERT INTO OrderAuditLog (OrderID, CustomerID, TotalAmount)
    SELECT OrderID, CustomerID, TotalAmount
    FROM deleted;

    PRINT 'Deleted order logged to audit table!';
END;
GO

-- ============================================
-- TEST TRIGGER 1: Add order item, watch stock reduce
-- ============================================

-- Check stock BEFORE
SELECT ProductID, ProductName, StockQty 
FROM Products 
WHERE ProductID = 2; -- Phone has 100 stock

-- Add an order item (buying 5 Phones)
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice)
VALUES (1, 2, 5, 25000.00);

-- Check stock AFTER - should be reduced by 5
SELECT ProductID, ProductName, StockQty 
FROM Products 
WHERE ProductID = 2;
GO

-- ============================================
-- TEST TRIGGER 2: Delete an order, check audit log
-- ============================================

-- Delete order 6
DELETE FROM Orders WHERE OrderID = 6;

-- Check audit log - should show deleted order
SELECT * FROM OrderAuditLog;
GO