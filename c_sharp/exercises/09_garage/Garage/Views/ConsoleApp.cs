using System;
using Garage.Views.UI;
using Garage.Views.Utilities;
using System.Text;
using System.Linq;

namespace Garage.Views;

public class ConsoleApp
{
    private enum Command
    {
        Exit = 0,
        CreateGarage = 1,
        ViewGarage = 2,
    }

    public void Run()
    {

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("0: Exit");
        sb.AppendLine($"{(int)Command.CreateGarage}: Create a garage");
        sb.Append($"{(int)Command.ViewGarage}: View garage");



        GarageHandlerUI handler = new();

        while (true)
        {
            Display.Clear();
            Display.ShowCommands(sb.ToString());
            if (!int.TryParse(Console.ReadLine(), out int command)
                || command < 0
                || command > Enum.GetValues<Command>().Max(command => (int)command))
            {
                Display.ErrorMessage("Not a valid command");
                Display.WaitThenClear();
                continue;
            }

            switch ((Command)command)
            {
                case Command.Exit:
                    return;
                case Command.CreateGarage:
                    handler.CreateGarageView();
                    break;
                case Command.ViewGarage:
                    handler.ViewGarageMenu();
                    break;
            }


        }

    }
}
