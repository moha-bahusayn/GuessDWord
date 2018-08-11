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
        static string[] listOfInput = new string[] { "1", "easy", "2", "normal", "3", "hard" };
        static string[] listOfLevels = new string[] { "easy", "normal", "hard" };
        static int[] listOfAttempts = new int[] { 10, 5, 3 };
        //This is the main function to start the program.
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
            Console.Clear();

            InitiateGame();
            do
            {
                string level = ChooseDiff();
                int attempt = AttemptsCounter(level);
                RunGame(level, attempt);
            } while (ToReset());
            Console.ReadLine();

        }
        
        public static void InitiateGame()
        {
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("##### Welcome to Guess My Word! #####");
            Console.WriteLine("##### ========================= #####");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(""); 
        }

        public static string ChooseDiff()
        {
            string diffInput;
            string selectedLevel = "";
            do
            {
                Console.WriteLine("Please Select the Difficulty Level:");
                Console.WriteLine("1. Easy");//(10 Attempts)
                Console.WriteLine("2. Normal");//(5 Attempts)
                Console.WriteLine("3. Hard"); //(3 Attempts)
                diffInput = Console.ReadLine().ToLower();
                switch (diffInput)
                {
                    case "1":
                    case "easy":
                        selectedLevel = listOfLevels[0];
                        break;

                    case "2":
                    case "normal":
                        selectedLevel = listOfLevels[1];
                        break;

                    case "3":
                    case "hard":
                        selectedLevel = listOfLevels[2];
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please re-enter your difficulty choice!");
                        break;
                }
                return selectedLevel;
            } while (!ValidateInput(diffInput, listOfInput));
            
        }

        public static int AttemptsCounter(string level)
        {
            int attempt = 0;
            switch (level)
            {
                case "easy":
                    attempt = listOfAttempts[0];
                    break;

                case "normal":
                    attempt = listOfAttempts[1];
                    break;

                case "hard":
                    attempt = listOfAttempts[2];
                    break;
            }
            return attempt;
        }


        public static void RunGame(string lvl, int attempts)
        {
            Console.Clear();
            string randomWord = RandomWordPicker(listOfWords);

            //DEBUG
            Console.WriteLine("The random word is: " + randomWord);
            Console.ReadLine();

            string starRandom = "";
            for (int i = 0; i < randomWord.Length; i++)
            {
                starRandom += "*";
            }

            Console.WriteLine("Start the Game by type in a single letter: ");
            string charInput = Console.ReadLine().ToLower();
            int attemptsCounter = 0;
            do
            {
                if (IsValidChar(charInput))
                {
                    string validInput = charInput;
                    if (IsValidLetter(validInput, randomWord, starRandom))
                    {
                        Console.WriteLine("Bingo!. The letter || " + validInput.ToUpper() + " || is in the random word.");
                        if (randomWord == starRandom)
                        {
                            ResultStats(randomWord, attemptsCounter);
                        }
                        else
                        {

                            Console.ReadLine();
                            Console.WriteLine("Let's Continue. Shall We?");
                            Console.ReadLine();
                            Console.WriteLine("The Status: ");
                            Console.WriteLine("============");
                            Console.WriteLine(starRandom);
                            Console.WriteLine("============");
                            Console.ReadLine();
                            Console.WriteLine("Great! Enter a single letter again to complete the word!");
                            charInput = Console.ReadLine().ToLower();
                        }
                    }
                    else
                    {
                        attemptsCounter++;
                        if (attemptsCounter > attempts)
                        {
                            GameOver(randomWord, attemptsCounter);
                        }
                        Console.WriteLine("Oops! That's a wrong guess. Please try again.");

                        Console.WriteLine("Please Try Again to enter a letter. you have "
                                          + (attempts - attemptsCounter) + " attempts left.");

                        charInput = Console.ReadLine().ToLower();

                    }
                }
                else
                {
                    Console.WriteLine("Please re-enter a single letter ONLY.");
                    charInput = Console.ReadLine().ToLower();
                }
            } while (!IsValidChar(charInput));
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

        public static string RandomWordPicker(string[] listOfWords)
        {
            //Pick a random word.
            Random rand = new Random();
            String selectedWord = listOfWords[rand.Next(listOfWords.Length)];
            return selectedWord;
        }

        public static bool IsValidChar(string a)
        {
            if (a.Length == 1) {return true;}
            //else
            return false;
        }

        public static bool IsValidLetter(string letter, string word, string starWord)
        {
            /*for each letter in randomword, if string a == letter, replace the (*) with string a, and type Good!. if Not, failed attempt is recorded.*/

            for (int i= 0; i<starWord.Length; i++)
            {
                foreach(char y in word)
                {
                    if (y.ToString() == letter) { return true; }
                }
            }
            Console.ReadLine();
            return false;
        }

        private static void ResultStats(string word, int counter)
        {
            Console.WriteLine("Congratulations! you have won!");
            Console.WriteLine("");
            Console.WriteLine("You have guessed the word" + word.ToUpper() + "with " + counter + " mistakes.");
        }

        private static void GameOver(string word, int counter)
        {
            Console.WriteLine("Hard Luck! You lost the round!");
            Console.WriteLine("");
            Console.WriteLine("The word is: " + word.ToUpper() + ". You have tried with " + counter + " mistakes.");
        }

        private static bool ToReset()
        {
            Console.WriteLine("That's the End! Do you want to start Over? Y/N");
            string a = Console.ReadLine().ToLower();
            if (a == "y") { return true; }
            return false;
        }

    }
}
