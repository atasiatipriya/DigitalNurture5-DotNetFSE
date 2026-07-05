-- ============================================
-- EXERCISE 4: Views and Indexes
-- ============================================

USE RetailStore;
GO

-- ============================================
-- VIEW 1: Customer Order Summary
-- Shows each customer with their total orders
-- ============================================
CREATE VIEW CustomerOrderSummary AS
SELECT 
    c.CustomerID,
    c.FirstName + ' ' + c.LastName AS CustomerName,
    c.City,
    COUNT(o.OrderID)       AS TotalOrders,
    SUM(o.TotalAmount)     AS TotalSpent,
    MAX(o.TotalAmount)     AS HighestOrder
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.FirstName, c.LastName, c.City;
GO

-- ============================================
-- VIEW 2: Pending Orders View
-- Shows only orders that are still pending
-- ============================================
CREATE VIEW PendingOrdersView AS
SELECT 
    o.OrderID,
    c.FirstName + ' ' + c.LastName AS CustomerName,
    c.Phone,
    o.TotalAmount,
    o.OrderDate
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE o.Status = 'Pending';
GO

-- ============================================
-- VIEW 3: Product Stock Alert View
-- Shows products with low stock (less than 100)
-- ============================================
CREATE VIEW LowStockProducts AS
SELECT 
    ProductID,
    ProductName,
    Category,
    Price,
    StockQty
FROM Products
WHERE StockQty < 100;
GO

-- ============================================
-- TEST: Use the views like normal tables
-- ============================================

-- See customer order summary
SELECT * FROM CustomerOrderSummary
ORDER BY TotalSpent DESC;

-- See pending orders
SELECT * FROM PendingOrdersView;

-- See low stock products
SELECT * FROM LowStockProducts;
GO

-- ============================================
-- INDEXES: Speed up searches
-- ============================================

-- Index on Email column for faster customer lookup
CREATE INDEX IX_Customers_Email
ON Customers(Email);

-- Index on Category for faster product filtering
CREATE INDEX IX_Products_Category
ON Products(Category);

-- Index on Status for faster order filtering
CREATE INDEX IX_Orders_Status
ON Orders(Status);

PRINT 'All indexes created successfully!';
GO

-- Test index usage - search by email
SELECT * FROM Customers 
WHERE Email = 'atasi@email.com';

-- Test index usage - search by category
SELECT * FROM Products 
WHERE Category = 'Electronics';
GO