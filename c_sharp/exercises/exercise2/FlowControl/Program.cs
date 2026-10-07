using System;


string headMenuMessage = "Welcome to the Cinema menu";
string menuCmdsMessage = "\n0: To exit program.\n" +
                         "1: To see prices for youth or senior.\n" +
                         "3: Repeat input 10 times.\n" +
                         "4: \n";

string promptAgeMessage = "\nEnter an age: ";

int youthPrice = 80;
int seniorPrice = 90;
int standardPrice = 120;

string invalidCmdMessage = "Invalid command";
string invalidNumberMessage = "Not a number.";
string nonPositiveAgeMessage = "\nPlease enter a positive age\n";


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


void WaitForUserKeyPress()
{
    System.Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    System.Console.WriteLine();

}

int GetAgeAndPrice(out int age)
{
    while (true)
    {
        System.Console.WriteLine(promptAgeMessage);
        if (int.TryParse(Console.ReadLine(), out age))
        {
            if (age < 0)
            {
                System.Console.WriteLine(nonPositiveAgeMessage);
                continue;
            }
            if (age < 20) return youthPrice;
            else if (age > 64) return seniorPrice;
            else return standardPrice;


        }
        else System.Console.WriteLine(invalidNumberMessage);
    }
}

void PrintAgePrice(int age, int price)
{
    if (age < 20) System.Console.WriteLine($"Youth price: {youthPrice}.");
    else if (age > 64) System.Console.WriteLine($"Senior price: {seniorPrice}.");
    else System.Console.WriteLine($"Standard price: {standardPrice}");
}