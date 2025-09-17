// Esbjörn Holmerin Nord, NET25
using System;

namespace Labb2_NumbersGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Definier en bool som kollar om användaren vill fortsätta spela och en while loop som körs så länge boolen är true.
            bool userWilling = true;
            while (userWilling)
            {
                int guesses = 5;
                int correctNumberMax = 20;

                bool difficultyChosen = false;
                do
                {
                    // Välkomna användaren och fråga svårighetsgrad
                    Console.WriteLine("Välkommen! Välj en svårighetsgrad (skriv in svårighetsgraden):");
                    Console.WriteLine("\"Lätt\"  (5 gissningar, hitta numret mellan 1 och 20");
                    Console.WriteLine("\"Medel\" (6 gissningar, hitta numret mellan 1 och 30");
                    Console.WriteLine("\"Svår\"  (8 gissningar, hitta numret mellan 1 och 60");

                    string answer = Console.ReadLine();
                    if(answer.ToLower() == "lätt")
                    {
                        guesses = 5;
                        correctNumberMax = 20;
                        difficultyChosen = true;
                    }
                    else if (answer.ToLower() == "medel")
                    {
                        guesses = 6;
                        correctNumberMax = 30;
                        difficultyChosen = true;
                    }
                    else if (answer.ToLower() == "svår")
                    {
                        guesses = 8;
                        correctNumberMax = 60;
                        difficultyChosen = true;
                    }
                    else
                    {
                        Console.WriteLine("Ursäkta jag förstod inte vilken svårighetsgrad du menade, vänligen prova igen.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                }
                while (!difficultyChosen);

                //Bestäm range och antal gissningar baserat på användarens svar

                //Definiera det rätta numret
                Random random = new Random();
                int correctNumber = random.Next(1, (correctNumberMax+1));

                //Tar det korrekta nummret och matar in den i gameplay loopen. Kollar om använaren hittar rätt nummer eller inte och svarar motsvarande
                if (GuessLoop(correctNumber, guesses, correctNumberMax))
                {
                    Console.WriteLine($"Grattis! Du hittade mitt nummer som var {correctNumber}.");
                }
                else
                {
                    Console.WriteLine("Tyvärr, du lyckades inte gissa talet på fem försök!");
                }

                //Fråga om användaren vill fortsätta. Rensar konsol fönstret om svaret är ja, annars så ändrar den kontrol boolen för while loopen vilket gör att loopen bryts när den ska börja om
                Console.WriteLine("Vill du spela igen? Skriv \"ja\" om du vill.");
                if(Console.ReadLine() == "ja")
                {
                    Console.Clear();
                }
                else
                {
                    userWilling = false;
                }
            }
        }

        // Låt användaren försöka ett antal ggr som är bestämmt av svårighetsgraden
        // Ta in input och jämför mot rätta nummret
        // Skriv ut om det är rätt, högre, eller lägre
        static bool GuessLoop(int correctNumber, int guesses, int correctMax)
        {
            for (int i = 0; i < guesses; i++)
            {
                Console.WriteLine($"Du har {guesses - i} gissningar kvar");
                // Kollar att användaren matar in ett heltal och att det är inom gränsen för gissningsspelet.
                if (int.TryParse(Console.ReadLine(), out int inputNumber) && (inputNumber < (correctMax + 1) && inputNumber > 0))
                {
                    if (inputNumber == correctNumber)
                    {
                        return true;
                    }
                    else if (inputNumber > correctNumber)
                    {
                        // Bryter ut ur metoden om det var sista gissningen
                        if(i == guesses - 1)
                        {
                            return false;
                        }
                        // Skriver ut ett random svar från svarsmetoden.
                        Console.WriteLine(TooHighResponse());
                    }
                    else
                    {
                        // Bryter ut ur metoden om det var sista gissningen
                        if (i == guesses - 1)
                        {
                            return false;
                        }
                        // Skriver ut ett random svar från svarsmetoden.
                        Console.WriteLine(TooLowResponse());
                    }
                }
                else
                {
                    // Bryter ut ur metoden om det var sista gissningen
                    if (i == guesses - 1)
                    {
                        return false;
                    }
                    Console.WriteLine($"Skriv in ditt svar som ett heltal mellan 1 och {correctMax}. Du har slösat en gissning");
                }
            }
            return false;
        }

        // Returnerar en av 3 strängar som säger att användaren gissade för högt.
        static string TooHighResponse()
        {
            Random random = new Random();
            int responseChoice = random.Next(0, 3);

            switch (responseChoice) 
            {
                case 0:
                    return "Tyvärr, du gissade för högt!";
                case 1:
                    return "Du var lite ambitiös i din gissning. Prova något lägre.";
                case 2:
                    return "Jösses. Det fär var lite väl högt.";
            }
            return "Tyvärr, du gissade för högt!";
        }

        // Returnerar en av 3 strängar som säger att användaren gissade för lågt.
        static string TooLowResponse()
        {
            Random random = new Random();
            int responseChoice = random.Next(0, 3);

            switch (responseChoice)
            {
                case 0:
                    return "Tyvärr, du gissade för lågt!";
                case 1:
                    return "Du kan gissa lite högre skulle jag säga.";
                case 2:
                    return "Det där var tyvärr lite för lågt.";
            }
            return "Tyvärr, du gissade för högt!";
        }
    }
}
