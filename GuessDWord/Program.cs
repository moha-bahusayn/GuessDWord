using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDWord
{
    class Program
    {
        // The Array that contains the premade list of words.
        static string[] listOfWords = new string[] { "cat", "egg", "sun", "book", "lion", "nose", "night", "toast",
                                                "kitten", "slipper", "computer", "telescope", "strawberry", "toothpaste", "playground"};

        //This is the main function to start the program.
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            InitiateGame();
            //do
            //ChooseDiff();
            //RunGame();
            //ResultStats();
            //while the answer to start the game is true.
            Console.ReadLine();

        }

        //This is the function to initiate the process of the game.
        public static void InitiateGame()
        {
            string[] listOfInput = new string[] { "1", "easy", "2", "normal", "3", "hard" };
            string userInput = "";
            string selectedLevel = "";
            string[] listOfLevels = new string[] { "Easy", "Normal", "Hard" };
            int failedAttempt = 0;
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("##### Welcome to Guess My Word! #####");
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            do
            {
                Console.WriteLine("Please Select the Difficulty Level:");
                Console.WriteLine("1. Easy");//(10 Attempts)
                Console.WriteLine("2. Normal");//(5 Attempts)
                Console.WriteLine("3. Hard"); //(3 Attempts)
                userInput = Console.ReadLine().ToLower();
                
                switch (userInput)
                {
                    case "1":
                    case "easy":
                        Console.WriteLine("Easy Level");
                        //Call Easy Level function in here.
                        selectedLevel = listOfLevels[0];
                        failedAttempt = 10;
                        break;

                    case "2":
                    case "normal":
                        Console.WriteLine("Normal Level");
                        //Call Normal Level function in here.
                        selectedLevel = listOfLevels[1];
                        failedAttempt = 5;
                        break;

                    case "3":
                    case "hard":
                        Console.WriteLine("Hard Level");
                        //Call Hard Level function in here.
                        selectedLevel = listOfLevels[2];
                        failedAttempt = 3;
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please re-enter your difficulty choice!");
                        //Read the input again from the user.
                        break;
                }
            } while (!ValidateInput(userInput, listOfInput));

            string randomWord = RandomWordPicker(listOfWords);
            Console.WriteLine("level is: " + selectedLevel + ". Attempts are: " + failedAttempt +"and the random word is: "+randomWord);
        }
        
        //The following function is to validate any input whether includes in an array or not.
        public static bool ValidateInput(string input, string[] listOfValidInput)
        {
            for (int i = 0; i < listOfValidInput.Length; i++)
            {
                if (input == listOfValidInput[i]) { return true; }
            }
            return false;
        }
        
        //The following function is to pick a random word based on the level selection.
        public static string RandomWordPicker(string[] listOfWords)
        {
            //Pick a random word.
            Random rand = new Random();
            String selectedWord = listOfWords[rand.Next(listOfWords.Length)];
            return selectedWord;
        }
        
    }
}
