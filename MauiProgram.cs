namespace Lab2Solution;

/**
Name: XEE LO
Date: OCTOBER 6, 2022
Description: CS 341 SOFTWARE ENGINEERING: LAB 3
Bugs: 
Reflection: 
*/
public static class MauiProgram
{
    public static IBusinessLogic ibl = new BusinessLogic();

    public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}

