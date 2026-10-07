using System;
using System.Linq;
using System.Text.RegularExpressions;


Run();


void Run()
{
    const string headMenuMessage = "Welcome to the Cinema menu";
    const string menuCmdsMessage = "\n0: To exit program.\n" +
                             "1: To see prices for youth or senior.\n" +
                             "2: Get total price for your group.\n" +
                             "3: Repeat input 10 times.\n" +
                             "4: Print third word of a sentence.\n";
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
                    PrintCategoryPrice();
                    WaitForUserKeyPress();
                    break;
                case 2:
                    PrintGroupPrice(GetGroupPrices(ReadPositiveInt("How many people are in your group?")));
                    WaitForUserKeyPress();

                    break;
                case 3:
                    RepeatUserInput();
                    WaitForUserKeyPress();

                    break;
                case 4:
                    PrintThirdWordInSentence();
                    WaitForUserKeyPress();

                    break;
                default:
                    System.Console.WriteLine(invalidCmdMessage);
                    break;
            }

        }
        else System.Console.WriteLine(invalidCmdMessage);
    }
}


void PrintThirdWordInSentence()
{
    string input;
    string[] subs;
    while (true)
    {
        System.Console.WriteLine("Enter a sentence with at least 3 words");
        input = Console.ReadLine();
        input = Regex.Replace(input.Trim(), @"\s+", " ");
        subs = input.Split(' ');
        if (subs.Length >= 3) break;
        System.Console.WriteLine("\nDoesnt contain 3 words");
    }

    System.Console.WriteLine($"\nThird word is: {subs[2]}");
}

void RepeatUserInput()
{
    System.Console.WriteLine("What do you want to repeat 10 times?");
    string input = Console.ReadLine();

    for (int i = 0; i < 10; i++)
    {
        Console.Write(input);
    }
}

void PrintCategoryPrice()
{
    int age = ReadPositiveInt(query: "\nEnter an age: ",
                          errorMessage: "\nPlease enter a positive age");
    System.Console.WriteLine($"{GetPriceCategory(age)} price: {GetPrice(age)}.");
}

void PrintGroupPrice(int[] prices)
{
    System.Console.WriteLine($"Number of people: {prices.Length}");
    System.Console.WriteLine($"Total price: {prices.Sum()}");
}

int[] GetGroupPrices(int groupSize)
{
    int[] result = new int[groupSize];

    for (int i = 0; i < result.Length; i++)
    {
        int age = ReadPositiveInt(query: "\nEnter an age: ", errorMessage: "\nPlease enter a positive age");
        result[i] = GetPrice(age);
    }

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
    System.Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
    System.Console.WriteLine();
}


int GetPrice(int age)
{
    int youthPrice = 80;
    int seniorPrice = 90;
    int standardPrice = 120;
    if (age < 5 || age > 100) return 0;
    else if (age < 20) return youthPrice;
    else if (age > 64) return seniorPrice;
    else return standardPrice;
}

string GetPriceCategory(int age)
{
    if (age < 20) return $"Youth";
    else if (age > 64) return $"Senior";
    else return $"Standard";
}