using System;

// ============================================
// MAIN PROGRAM - E-commerce Product Search
// ============================================

// Our product inventory - an array of product names
// Note: For Binary Search to work, the array MUST be sorted
string[] products = {
    "Camera",
    "Headphones",
    "Keyboard",
    "Laptop",
    "Monitor",
    "Mouse",
    "Phone",
    "Printer",
    "Tablet",
    "Watch"
};

Console.WriteLine("=== E-Commerce Product Search System ===");
Console.WriteLine("Available products: " + string.Join(", ", products));
Console.WriteLine();

// ---- TEST LINEAR SEARCH ----
Console.WriteLine("--- Linear Search ---");

string searchItem1 = "Laptop";
int linearResult1 = LinearSearch(products, searchItem1);
if (linearResult1 != -1)
    Console.WriteLine($"'{searchItem1}' found at index {linearResult1} using Linear Search.");
else
    Console.WriteLine($"'{searchItem1}' not found using Linear Search.");

string searchItem2 = "Charger";
int linearResult2 = LinearSearch(products, searchItem2);
if (linearResult2 != -1)
    Console.WriteLine($"'{searchItem2}' found at index {linearResult2} using Linear Search.");
else
    Console.WriteLine($"'{searchItem2}' not found using Linear Search.");

Console.WriteLine();

// ---- TEST BINARY SEARCH ----
Console.WriteLine("--- Binary Search ---");

string searchItem3 = "Laptop";
int binaryResult1 = BinarySearch(products, searchItem3);
if (binaryResult1 != -1)
    Console.WriteLine($"'{searchItem3}' found at index {binaryResult1} using Binary Search.");
else
    Console.WriteLine($"'{searchItem3}' not found using Binary Search.");

string searchItem4 = "Charger";
int binaryResult2 = BinarySearch(products, searchItem4);
if (binaryResult2 != -1)
    Console.WriteLine($"'{searchItem4}' found at index {binaryResult2} using Binary Search.");
else
    Console.WriteLine($"'{searchItem4}' not found using Binary Search.");

// ============================================
// LINEAR SEARCH METHOD
// Checks every item one by one from start to end
// Returns index if found, -1 if not found
// ============================================

static int LinearSearch(string[] arr, string target)
{
    // Go through every item in the array
    for (int i = 0; i < arr.Length; i++)
    {
        // If current item matches what we're looking for
        if (arr[i].Equals(target, StringComparison.OrdinalIgnoreCase))
        {
            return i; // Return the position where we found it
        }
    }
    return -1; // Reached end without finding it
}

// ============================================
// BINARY SEARCH METHOD
// Only works on SORTED arrays
// Cuts the search area in half each time
// Returns index if found, -1 if not found
// ============================================

static int BinarySearch(string[] arr, string target)
{
    int left = 0;               // Start of search area
    int right = arr.Length - 1; // End of search area

    while (left <= right)
    {
        // Find the middle index
        int mid = (left + right) / 2;

        int comparison = string.Compare(arr[mid], target,
                         StringComparison.OrdinalIgnoreCase);

        if (comparison == 0)
        {
            return mid; // Found it at middle!
        }
        else if (comparison < 0)
        {
            // Middle item comes before target alphabetically
            // So target must be in the RIGHT half
            left = mid + 1;
        }
        else
        {
            // Middle item comes after target alphabetically
            // So target must be in the LEFT half
            right = mid - 1;
        }
    }
    return -1; // Not found
}