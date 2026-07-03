using System;

// ============================================
// MAIN PROGRAM - Testing our Singleton
// ============================================

// Try to get Logger instance - first time, it gets created
Logger logger1 = Logger.GetInstance();
logger1.Log("Application started.");

// Try to get Logger instance again
Logger logger2 = Logger.GetInstance();
logger2.Log("User logged in.");

// Prove both variables point to the SAME object
if (logger1 == logger2)
{
    Console.WriteLine("Both logger1 and logger2 are the SAME instance. Singleton works!");
}
else
{
    Console.WriteLine("Different instances. Singleton failed!");
}

// ============================================
// SINGLETON CLASS - Logger
// ============================================

public class Logger
{
    // Holds the one and only instance
    private static Logger? _instance = null;

    // Private constructor - blocks "new Logger()" from outside
    private Logger()
    {
        Console.WriteLine("Logger created for the first time.");
    }

    // Only way to get the instance
    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    // Method to log messages
    public void Log(string message)
    {
        Console.WriteLine("LOG: " + message);
    }
}