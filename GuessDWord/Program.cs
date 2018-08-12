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
        }
        
        public static void InitiateGame()
        {
            Console.WriteLine("###### ========================= ######");
            Console.WriteLine("###### Welcome to Guess My Word! ######");
            Console.WriteLine("###### ========================= ######");
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
                Console.WriteLine("#1. Easy");//(10 Attempts)
                Console.WriteLine("#2. Normal");//(5 Attempts)
                Console.WriteLine("#3. Hard"); //(3 Attempts)
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

            /* ===================================================== */
            //DEBUG
            Console.WriteLine("The random word is: '{0}'", randomWord);
            Console.ReadLine();
            /* ===================================================== */

            string starRandom = "";
            int wordLength = 0;
            for (int i = 0; i < randomWord.Length; i++)
            {
                starRandom += "*";
                wordLength++;
            }

            
            Console.WriteLine("The random word has: '{0}' characters.Let's Start!", wordLength);
            int attemptsCounter = 0;
            int attemptsLeft = attempts - attemptsCounter;
            string charInput = "";
            

            /* ===================================================== */
            //DEBUG
            Console.WriteLine("Attempts: '{0}'", attempts);
            Console.ReadLine();
            /* ===================================================== */

            Console.WriteLine("Enter a single character.");
            charInput = Console.ReadLine().ToLower();
            do
            {
                if (IsValidChar(charInput))
                {
                    if (IsValidLetter(charInput, randomWord, starRandom))
                    {
                        Console.WriteLine("Correct!. The letter || '{0}' || is in the random word.", charInput.ToUpper());
                        starRandom = ReplaceLetter(charInput, randomWord, starRandom);
                        if (starRandom == randomWord)
                        {
                            ResultStats(randomWord, attemptsCounter);
                            break;
                        }
                        else
                        {

                            Console.WriteLine("Let's Continue. Shall We?");
                            Console.WriteLine("The Status: ");
                            Console.WriteLine("============");
                            Console.WriteLine(starRandom);
                            Console.WriteLine("============");
                            Console.WriteLine("Level:'{0}'", lvl);
                            Console.WriteLine("'{0}' / '{1}' Lives", attemptsLeft, attempts);
                            Console.WriteLine("Great! Enter a single letter again to complete the word!");
                        }
                    }
                    else
                    {
                        attemptsCounter++;
                        attemptsLeft--;
                        if (attemptsLeft == 0)
                        {
                            GameOver(randomWord, attemptsCounter);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Oops! That's a wrong guess. you have '{0}' attempts left.", attemptsLeft);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("That's an invalid input.Please re-enter a single letter ONLY.");
                    charInput = Console.ReadLine().ToLower();
                }
                //In here, needs to add lines of code to continue adding valid inputs in order to keep the value of 
                //the new word compiled of stars and corrected-guess letters.\
                Console.WriteLine("Enter a single character.");
                charInput = Console.ReadLine().ToLower();
            } while (attemptsLeft > 0);
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

        public static bool IsValidLetter(string letter, string word, string starLetter)
        {
                foreach(char y in word)
                {
                    if (y.ToString() == letter)
                    {
                        return true;
                    }
                }
            return false;
        }

        private static string ReplaceLetter(string letter, string word, string stars)
        {
            string newStar="";
            for (int i = 0; i<word.Length; i++)
            {
                if (word[i].ToString() == letter)
                {

                    newStar += word[i];
                }
                else
                {
                    if (stars[i].ToString() == "*")
                    {
                        newStar += "*";
                    }
                    else
                    {
                        newStar += stars[i];
                    }
                }
            }
            return newStar;
        }

        private static void ResultStats(string word, int counter)
        {
            Console.WriteLine("Congratulations! you have won!");
            Console.WriteLine("");
            Console.WriteLine("You have guessed the word # '{0}' # with '{1}'mistakes.", word.ToUpper(), counter);
        }

        private static void GameOver(string word, int counter)
        {
            Console.WriteLine("Hard Luck! You lost the round!");
            Console.WriteLine("");
            Console.WriteLine("The word is: '{0}'. You have tried with '{1}' mistakes.", word.ToUpper(), counter);
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
