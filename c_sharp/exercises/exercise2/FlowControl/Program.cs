using System;


const string headMenuMessage = "Welcome to the Cinema menu";
const string menuCmdsMessage = "\n0: To exit program.\n" +
                         "1: To see prices for youth or senior.\n" +
                         "3: Repeat input 10 times.\n" +
                         "4: \n";
const string invalidCmdMessage = "Invalid command";



System.Console.WriteLine(headMenuMessage);
bool isRunning = true;
while (isRunning)
{

    System.Console.WriteLine(menuCmdsMessage);
    if (int.TryParse(Console.ReadLine(), out int command))
    {
        switch (command)
        {
            case 0:
                isRunning = false;
                break;
            case 1:
                int price = GetAgeAndPrice(out int age);
                PrintAgePrice(age: age, price: price);
                WaitForUserKeyPress();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            default:
                System.Console.WriteLine(invalidCmdMessage);
                break;
        }

    }
    else System.Console.WriteLine(invalidCmdMessage);
}


int[] GetGroupPrices()
{
    int[] result = new int[ReadPositiveInt("How many people are in your group?")];


    return result;
}

int ReadPositiveInt(string query, string errorMessage = "Not a valid number.")
{

    while (true)
    {
        System.Console.WriteLine(query);
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
        {
            return number;
        }
        else System.Console.WriteLine(errorMessage);
    }
}

void WaitForUserKeyPress()
{
    System.Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    System.Console.WriteLine();

}

int GetAgeAndPrice(out int age)
{
    const string nonPositiveAgeMessage = "\nPlease enter a positive age";
    const string promptAgeMessage = "\nEnter an age: ";
    int youthPrice = 80;
    int seniorPrice = 90;
    int standardPrice = 120;
    age = ReadPositiveInt(query: promptAgeMessage, errorMessage: nonPositiveAgeMessage);

    if (age < 20) return youthPrice;
    else if (age > 64) return seniorPrice;
    else return standardPrice;
}

void PrintAgePrice(int age, int price)
{
    if (age < 20) System.Console.WriteLine($"Youth price: {price}.");
    else if (age > 64) System.Console.WriteLine($"Senior price: {price}.");
    else System.Console.WriteLine($"Standard price: {price}");
}