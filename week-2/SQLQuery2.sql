-- ============================================
-- EXERCISE 2: Window Functions
-- ============================================

USE RetailStore;
GO

-- ============================================
-- ROW_NUMBER: Gives unique sequential numbers
-- Even if two rows have same value, they get
-- different numbers (1,2,3,4,5...)
-- ============================================
SELECT 
    OrderID,
    CustomerID,
    TotalAmount,
    ROW_NUMBER() OVER (ORDER BY TotalAmount DESC) AS RowNumber
FROM Orders;
GO

-- ============================================
-- RANK: Gives same rank to equal values
-- But SKIPS the next rank
-- Example: 1, 2, 2, 4 (skips 3)
-- ============================================
SELECT 
    OrderID,
    CustomerID,
    TotalAmount,
    RANK() OVER (ORDER BY TotalAmount DESC) AS RankNumber
FROM Orders;
GO

-- ============================================
-- DENSE_RANK: Gives same rank to equal values
-- But does NOT skip the next rank
-- Example: 1, 2, 2, 3 (no skip)
-- ============================================
SELECT 
    OrderID,
    CustomerID,
    TotalAmount,
    DENSE_RANK() OVER (ORDER BY TotalAmount DESC) AS DenseRankNumber
FROM Orders;
GO

-- ============================================
-- PARTITION BY: Rank WITHIN each group
-- Here we rank orders within each customer
-- ============================================
SELECT 
    OrderID,
    CustomerID,
    TotalAmount,
    ROW_NUMBER() OVER (
        PARTITION BY CustomerID 
        ORDER BY TotalAmount DESC
    ) AS OrderRankPerCustomer
FROM Orders;
GO