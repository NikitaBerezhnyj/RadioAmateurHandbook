using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Exceptions;
using RadioAmateurHandbook.Radios;
using RadioAmateurHandbook.UI;
using RadioAmateurHandbook.Users;
using RadioAmateurHandbook.Utils;
using System;
using System.Reflection;

namespace RadioAmateurHandbook
{
    internal class ConsoleApp
    {
        private RadioFM fmRadio;
        private RadioAM amRadio;
        private string activeUserRole;
        private User activeUser;
        private Client client;
        private Admin admin;
        private Manager manager;
        private Director director;

        public void Run()
        {
            fmRadio = new RadioFM();
            amRadio = new RadioAM();

            client = new Client(fmRadio, amRadio);
            admin = new Admin(fmRadio, amRadio);
            manager = new Manager(fmRadio, amRadio);
            director = new Director(fmRadio, amRadio);

            activeUser = client;
            activeUserRole = "Client";

            bool isDataLoaded = false;

            while (!isDataLoaded)
            {
                string filename = DataManager.GetFilename();

                if (File.Exists(filename))
                {
                    bool isLoadChoiceMade = false;

                    while (!isLoadChoiceMade)
                    {
                        char wantLoad = InputUtils.GetChar(
                            "Do you want to load radio state from saves? [y - Yes; n - No]: "
                        );

                        if (wantLoad == 'y')
                        {
                            if (!DataManager.LoadData(fmRadio, amRadio))
                            {
                                MessageUtils.WarningMessage("Failed to load data from file.");
                            }

                            isLoadChoiceMade = true;
                            isDataLoaded = true;
                        }
                        else if (wantLoad == 'n')
                        {
                            isLoadChoiceMade = true;
                            isDataLoaded = true;
                        }
                        else
                        {
                            MessageUtils.WarningMessage(
                                "Error. Please enter correct character [y - Yes; n - No]"
                            );
                            ConsoleUtils.WaitForEnter();
                        }
                    }
                }
                else
                {
                    DataManager.CreateData();
                    isDataLoaded = true;
                }
            }

            bool isStarterDataChoosen = false;
            while (!isStarterDataChoosen)
            {
                bool isRadioChoosen = false;
                while (!isRadioChoosen)
                {
                    try
                    {
                        int radioNum = InputUtils.GetNumber($"Select radio [0] {fmRadio.GetName()} [1] {amRadio.GetName()}: ");
                        if (radioNum == -1 || (radioNum != 0 && radioNum != 1))
                        {
                            throw new UserInputException("Invalid input. Please enter a valid code for radio.");
                        }
                        if (radioNum == 0) { activeUser.SelectFM(); }
                        else { activeUser.SelectAM(); }
                        isRadioChoosen = true;
                    }
                    catch (UserInputException ex)
                    {
                        MessageUtils.PanicMessage("Error: " + ex.Message);
                        ConsoleUtils.WaitForEnter();
                        continue;
                    }
                }

                bool isUserChoosen = false;
                while (!isUserChoosen)
                {
                    try
                    {
                        int roleNum = InputUtils.GetNumber("Select user role: [0] Client [1] Admin [2] Manager [3] Director: ");
                        if (roleNum == -1 || (roleNum < 0 && roleNum > 3))
                        {
                            throw new UserInputException("Invalid input. Please enter a valid user type.");
                        }

                        string currentRadioType = activeUser.GetActiveRadioType();

                        activeUser = roleNum switch
                        {
                            0 => client,
                            1 => admin,
                            2 => manager,
                            3 => director,
                            _ => activeUser
                        };

                        activeUserRole = roleNum switch
                        {
                            0 => "Client",
                            1 => "Admin",
                            2 => "Manager",
                            3 => "Director",
                            _ => activeUserRole
                        };

                        if (currentRadioType == "AM")
                        {
                            activeUser.SelectAM();
                        }
                        else
                        {
                            activeUser.SelectFM();
                        }
                        isUserChoosen = true;
                    }
                    catch (UserInputException ex)
                    {
                        MessageUtils.PanicMessage("Error: " + ex.Message);
                        ConsoleUtils.WaitForEnter();
                        continue;
                    }
                }
                isStarterDataChoosen = true;
            }

            bool isRunning = true;
            while (isRunning)
            {
                Radio currentRadio = activeUser.GetActiveRadio();

                ConsoleUtils.ClearScreen();
                MenuRenderer.Render(activeUserRole, currentRadio.GetName());
                RadioInfoRenderer.Print(currentRadio);

                int choice;

                try
                {
                    choice = InputUtils.GetNumber("Enter task code: ");
                    if (choice == -1)
                    {
                        throw new UserInputException("Invalid input. Please enter a valid task number.");
                    }
                }
                catch (UserInputException ex)
                {
                    MessageUtils.PanicMessage("Error: " + ex.Message);
                    ConsoleUtils.WaitForEnter();
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        if (!activeUser.CanTurnOff())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to turn off radio.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        activeUser.TurnOff();
                        break;

                    case 1:
                        if (!activeUser.CanTurnOn())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to turn off radio.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        activeUser.TurnOn();
                        break;

                    case 2:
                        if (!activeUser.CanSetVolume())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to change volume.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        try
                        {
                            int volume = InputUtils.GetNumber("Enter volume (db): ");
                            if (volume == -1)
                            {
                                throw new UserInputException("Invalid input. Please enter a valid volume.");
                            }
                            activeUser.SetVolume(volume);
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 3:
                        if (!activeUser.CanSetFrequency())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to change frequency.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        try
                        {
                            double frequency = InputUtils.GetDouble("Enter frequency (MHz): ");
                            if (frequency == -1)
                            {
                                throw new UserInputException("Invalid input. Please enter a valid frequency.");
                            }
                            activeUser.SetFrequency(frequency);
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 4:
                        if (!activeUser.CanSaveFrequency())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to save frequency.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        try
                        {
                            double frequencyToSave = InputUtils.GetDouble("Enter frequency to save: ");
                            if (frequencyToSave == -1)
                            {
                                throw new UserInputException("Invalid input. Please enter a valid frequency.");
                            }
                            int saveIndex = InputUtils.GetNumber("Enter index [1-5]: ");
                            if (saveIndex != -1 && saveIndex <= 1 && saveIndex >= 5)
                            {
                                throw new UserInputException("Invalid input. Please enter a valid index frequency.");
                            }
                            activeUser.SaveFrequency(saveIndex - 1, frequencyToSave);
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 5:
                        if (!activeUser.CanLoadFrequency())
                        {
                            MessageUtils.WarningMessage($"{activeUserRole} has no permission to load frequency.");
                            ConsoleUtils.WaitForEnter();
                            break;
                        }

                        try
                        {
                            int loadIndex = InputUtils.GetNumber("Enter index [1-5]: ");
                            if (loadIndex == -1)
                            {
                                throw new UserInputException("Invalid input. Please enter a valid frequency.");
                            }
                            activeUser.LoadFrequency(loadIndex);
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 6:
                        try
                        {
                            int radioNum = InputUtils.GetNumber($"Select radio [0] {fmRadio.GetName()} [1] {amRadio.GetName()}: ");
                            if (radioNum == -1 || (radioNum != 0 && radioNum != 1))
                            {
                                throw new UserInputException("Invalid input. Please enter a valid code for radio.");
                            }
                            if (radioNum == 0) { activeUser.SelectFM(); }
                            else { activeUser.SelectAM(); }
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 7:
                        try
                        {
                            int roleNum = InputUtils.GetNumber("Select user role: [0] Client [1] Admin [2] Manager [3] Director: ");
                            if (roleNum == -1 || (roleNum < 0 && roleNum > 3))
                            {
                                throw new UserInputException("Invalid input. Please enter a valid user type.");
                            }

                            string currentRadioType = activeUser.GetActiveRadioType();

                            activeUser = roleNum switch
                            {
                                0 => client,
                                1 => admin,
                                2 => manager,
                                3 => director,
                                _ => activeUser
                            };

                            activeUserRole = roleNum switch
                            {
                                0 => "Client",
                                1 => "Admin",
                                2 => "Manager",
                                3 => "Director",
                                _ => activeUserRole
                            };

                            if (currentRadioType == "AM")
                            {
                                activeUser.SelectAM();
                            }
                            else
                            {
                                activeUser.SelectFM();
                            }
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 8:
                        try
                        {
                            char confirmReset = InputUtils.GetChar("Are you sure you want to reset radios? [y/n]: ");
                            if (confirmReset == ' ')
                            {
                                throw new UserInputException("Please enter a correct character");
                            }

                            if (confirmReset == 'y')
                            {
                                fmRadio.Reset();
                                amRadio.Reset();
                                MessageUtils.SuccessMessage("All objects reset.");
                            }
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 9:
                        try
                        {
                            char confirmExit = InputUtils.GetChar("Are you sure you want to exit? [y/n]: ");
                            if (confirmExit == ' ')
                            {
                                throw new UserInputException("Please enter correct character");
                            }

                            if (confirmExit == 'y')
                            {
                                MessageUtils.SuccessMessage("Thanks for work");
                                isRunning = false;
                            }
                        }
                        catch (UserInputException ex)
                        {
                            MessageUtils.PanicMessage("Error: " + ex.Message);
                            ConsoleUtils.WaitForEnter();
                            continue;
                        }
                        break;

                    case 10:
                        HelpRenderer.Render();
                        break;

                    default:
                        MessageUtils.WarningMessage("Invalid choice.");
                        ConsoleUtils.WaitForEnter();
                        break;
                }

                DataManager.SaveData(fmRadio, amRadio);
            }
        }
    }
}