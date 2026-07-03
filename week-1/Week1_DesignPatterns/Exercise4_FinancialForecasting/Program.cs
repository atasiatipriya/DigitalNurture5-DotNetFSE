using System;

// ============================================
// MAIN PROGRAM - Financial Forecasting
// ============================================

Console.WriteLine("=== Financial Forecasting Tool ===");
Console.WriteLine();

// Test Case 1
double presentValue1 = 10000;  // Starting amount in rupees
double growthRate1 = 0.08;     // 8% annual growth
int years1 = 5;                // Number of years

double futureValue1 = CalculateFutureValue(presentValue1, growthRate1, years1);
Console.WriteLine($"Present Value  : Rs. {presentValue1}");
Console.WriteLine($"Growth Rate    : {growthRate1 * 100}% per year");
Console.WriteLine($"Years          : {years1}");
Console.WriteLine($"Future Value   : Rs. {futureValue1:F2}");
Console.WriteLine();

// Test Case 2
double presentValue2 = 50000;
double growthRate2 = 0.12;     // 12% annual growth
int years2 = 10;

double futureValue2 = CalculateFutureValue(presentValue2, growthRate2, years2);
Console.WriteLine($"Present Value  : Rs. {presentValue2}");
Console.WriteLine($"Growth Rate    : {growthRate2 * 100}% per year");
Console.WriteLine($"Years          : {years2}");
Console.WriteLine($"Future Value   : Rs. {futureValue2:F2}");
Console.WriteLine();

// Test Case 3 - Base case test (0 years)
double presentValue3 = 10000;
double growthRate3 = 0.08;
int years3 = 0;

double futureValue3 = CalculateFutureValue(presentValue3, growthRate3, years3);
Console.WriteLine($"Present Value  : Rs. {presentValue3}");
Console.WriteLine($"Growth Rate    : {growthRate3 * 100}% per year");
Console.WriteLine($"Years          : {years3}");
Console.WriteLine($"Future Value   : Rs. {futureValue3:F2}");

// ============================================
// RECURSIVE METHOD - Calculate Future Value
// ============================================

static double CalculateFutureValue(double presentValue, double growthRate, int years)
{
    // BASE CASE - this is what stops the recursion
    // If years = 0, the future value IS the present value
    // No more growth to calculate
    if (years == 0)
    {
        return presentValue;
    }

    // RECURSIVE CASE
    // Grow the value by one year, then call the same function
    // but with one less year remaining
    // Each call handles ONE year of growth
    return CalculateFutureValue(presentValue * (1 + growthRate), growthRate, years - 1);
}