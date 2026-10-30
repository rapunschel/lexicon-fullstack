using System;
using Garage.Models.Records;
using Garage.ViewModels;
using Garage.Views.Utilities;

namespace Garage.Views.UI;

public static class VehicleDetailsReaderUI
{
    private static VehicleDetailsViewModel _detailsVM = new();


    private static VehicleDetails GetCommonDetails()
    {
        while (true)
        {
            try
            {

                string input = InputReader.PromptUntilValid<string>(
                        tryRead: InputReader.ReadString,
                        showQuery: () => Display.ShowQuery("Enter the format: brand, registration, color, wheel count"),
                        showError: () => Display.ErrorMessage(error: "Must enter something"));
                string[] strings = input.Split(',');

                if (int.TryParse(strings[3].Trim(), out int wheelCount))
                {
                    return new VehicleDetails(Brand: strings[0].Trim(), Registration: strings[1].Trim(), Color: strings[2].Trim(), WheelCount: wheelCount);
                }
                else
                {
                    Display.ErrorMessage("Invalid input. Try again");
                }
            }
            catch (Exception)
            {
                Display.ErrorMessage("Incorrect format. Example: Ford, abc123, red, 5");
            }
        }
    }
}
