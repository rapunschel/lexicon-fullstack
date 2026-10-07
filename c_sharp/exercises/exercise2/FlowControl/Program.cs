using System;


string headMenuMessage = "Welcome to the Cinema menu";
string menuCmdsMessage = "\n0: To exit program.\n" +
                         "1: To see prices for youth or senior.\n" +
                         "3: Repeat input 10 times.\n" +
                         "4: \n";

string promptAgeMessage = "Enter an age: ";

int youthPrice = 80;
int seniorPrice = 90;
int standardPrice = 120;

string invalidCmdMessage = "\nInvalid command\n";
string invalidNumberMessage = "Not a number.\n";
string nonPositiveAgeMessage = "Please enter a positive age\n";

string youthPriceMessage = $"Youth price: {youthPrice}.";
string seniorPriceMessage = $"Senior price: {seniorPrice}.";
string standardPriceMessage = $"Standard price: {standardPrice}";

bool isRunning = true;



System.Console.WriteLine(headMenuMessage);
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
                PrintAgePrice();
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
void PrintAgePrice()
{
    while (true)
    {
        System.Console.WriteLine(promptAgeMessage);
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            if (age < 0)
            {
                System.Console.WriteLine(nonPositiveAgeMessage);
                continue;
            }
            if (age < 20) System.Console.WriteLine(youthPriceMessage);
            else if (age > 64) System.Console.WriteLine(seniorPriceMessage);
            else System.Console.WriteLine(standardPriceMessage);
            return;
        }
        else System.Console.WriteLine(invalidNumberMessage);
    }
}