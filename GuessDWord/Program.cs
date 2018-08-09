using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDWord
{
    class Program
    {
        // The Array that contains the list of words.
        //Premade list of the word.
        string[] listOfWords = new string[] { "cat", "egg", "sun", "book", "lion", "nose", "night", "toast",
                                                "kitten", "slipper", "computer", "telescope", "strawberry", "toothpaste", "playground"};

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            InitiateGame();
            //RunGame();
            //ResultStats();
            Console.ReadLine();
            
        }
        public static void InitiateGame()
        {
            string[] listOfInput = new string[] { "1", "easy", "2", "normal", "3", "hard" };
            string userInput;
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("##### Welcome to Guess My Word! #####");
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            do
            {
            Console.WriteLine("Please Select the Difficulty Level by entering the number(1-3):");
            Console.WriteLine("1. Easy (3 to 5 Characters)");
            Console.WriteLine("2. Normal (6 to 9 Characters)");
            Console.WriteLine("3. Hard (10+ Characters)");
            userInput = Console.ReadLine().ToLower();

            
                switch (userInput)
                {
                    case "1":
                    case "easy":
                        Console.WriteLine("Easy Level");
                        //Call Easy Level function in here. 
                        break;

                    case "2":
                    case "normal":
                        Console.WriteLine("Normal Level");
                        //Call Normal Level function in here.
                        break;

                    case "3":
                    case "hard":
                        Console.WriteLine("Hard Level");
                        //Call Hard Level function in here.
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please re-enter your difficulty choice!");
                        //Read the input again from the user here.
                        break;
                }
            } while (!ValidateInput(userInput, listOfInput));

        }


        //The following function is to validate any input whether includes in an array or not.
        public static bool ValidateInput(string input, string[] listOfValidInput)
        {
            for (int i=0; i < listOfValidInput.Length; i++)
            {
                if (input == listOfValidInput[i]) { return true; }
            }

            return false;
        }

        public string RandomWordPicker(string[] listOfWords )
        {
            //Pick a random word.
            Random rand = new Random();
            String selectedWord = listOfWords[rand.Next(listOfWords.Length)];
            return selectedWord;
        }
    }
}
