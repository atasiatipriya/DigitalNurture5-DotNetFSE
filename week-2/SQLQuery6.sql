-- ============================================
-- EXERCISE 6: Transactions with TRY/CATCH
-- ============================================

USE RetailStore;
GO

-- ============================================
-- TRANSACTION 1: Successful transaction
-- Transfer order from one customer to another
-- Both updates must succeed together
-- ============================================

PRINT '--- TEST 1: Successful Transaction ---';

BEGIN TRANSACTION;

BEGIN TRY
    -- Step 1: Update order status
    UPDATE Orders 
    SET Status = 'Completed'
    WHERE OrderID = 3;

    -- Step 2: Update customer city
    UPDATE Customers
    SET City = 'Chennai'
    WHERE CustomerID = 3;

    -- If both steps succeeded, save the changes
    COMMIT TRANSACTION;
    PRINT 'Transaction committed successfully!';
END TRY
BEGIN CATCH
    -- If anything failed, undo ALL changes
    ROLLBACK TRANSACTION;
    PRINT 'Transaction rolled back due to error!';
    PRINT 'Error: ' + ERROR_MESSAGE();
END CATCH;
GO

-- Verify changes were saved
SELECT OrderID, Status FROM Orders WHERE OrderID = 3;
SELECT CustomerID, FirstName, City FROM Customers WHERE CustomerID = 3;
GO

-- ============================================
-- TRANSACTION 2: Failed transaction (ROLLBACK)
-- We intentionally cause an error to see rollback
-- ============================================

PRINT '--- TEST 2: Failed Transaction (Rollback) ---';

BEGIN TRANSACTION;

BEGIN TRY
    -- Step 1: This will succeed
    UPDATE Orders
    SET Status = 'Processing'
    WHERE OrderID = 1;

    PRINT 'Step 1 done - Order status updated';

    -- Step 2: This will FAIL intentionally
    -- Inserting into a column that doesn't exist
    INSERT INTO Customers (FirstName, LastName, Email, FakeColumn)
    VALUES ('Test', 'User', 'test@email.com', 'invalid');

    -- This line will NEVER run because Step 2 failed
    COMMIT TRANSACTION;
    PRINT 'Transaction committed!';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    PRINT 'Transaction rolled back!';
    PRINT 'Error message: ' + ERROR_MESSAGE();
END CATCH;
GO

-- Verify Order 1 status was NOT changed (rollback worked)
SELECT OrderID, Status FROM Orders WHERE OrderID = 1;
GO

-- ============================================
-- TRANSACTION 3: Savepoints
-- You can partially rollback using savepoints
-- ============================================

PRINT '--- TEST 3: Using Savepoints ---';

BEGIN TRANSACTION;

    -- Insert first customer
    INSERT INTO Customers (FirstName, LastName, Email, Phone, City)
    VALUES ('Ravi', 'Patel', 'ravi@email.com', '9111111111', 'Hyderabad');
    
    PRINT 'First customer inserted';

    -- Create a savepoint here
    SAVE TRANSACTION SavePoint1;

    -- Insert second customer
    INSERT INTO Customers (FirstName, LastName, Email, Phone, City)
    VALUES ('Meena', 'Nair', 'meena@email.com', '9222222222', 'Kochi');
    
    PRINT 'Second customer inserted';

    -- Rollback only to savepoint (undo second insert only)
    ROLLBACK TRANSACTION SavePoint1;
    PRINT 'Rolled back to SavePoint1 - second customer removed';

-- Commit first customer only
COMMIT TRANSACTION;
PRINT 'First customer committed successfully!';
GO

-- Verify: Ravi should exist, Meena should NOT
SELECT * FROM Customers WHERE FirstName IN ('Ravi', 'Meena');
GO