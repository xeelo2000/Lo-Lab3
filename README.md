# Lab 3 - Database/ SQL
## Purpose 
This lab helps us learn SQL demands and connecting to bit.io

## `MainPage.xaml`
* Responsible for defining the user interface
* This contains a ListView which is acquiring data from the Database 

## `MainPage.xaml.cs`
* Responsible for establishing the connection between the ListView and the database with:

* Also calls the proper `BusinessLogic` methods
* Displays the alerts for any error messages that may appear

## `BusinessLogic.cs`
* Used as a means of communication between the UI (`MainPage.xaml` / `MainPage.xaml.cs`) and the Database (`Database.cs`) by calling proper methods
* Checks user input via `CheckEntryFields()`

## `RelationalDatabase.cs`
* Connected to the bit.io. This is the database named Entries


## How to Run
1. Open the `Lab3.sln`file in the Visual Studio
2. Ensure that an Android device is able to be simulated ([Android Studio](https://developer.android.com/studio) was used for this specific lab)
3. Run the application via Visual Studio and ensure that the target is an Android device that is able to be emulated
![SCR-20220927-ijs](https://user-images.githubusercontent.com/105162443/192606677-a5195bdc-67a4-4083-b994-27e164b82eac.png)
4. The app should be exist within the tablet and function there!

## Alternative Way to Run (Windows / Mac)
If there are issues that arise with running it via an emulated device, you're able to just target it to be ran just as .NET MAUI Application directly on your computer.
![SCR-20220927-iob](https://user-images.githubusercontent.com/105162443/192607007-0e6e73ca-fd7d-4eb8-bbc7-9e447b41457c.png)

