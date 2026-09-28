using System;

namespace DynamicStringTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Interactive C# String Transformation Engine";

            Console.WriteLine("=================================================");
            Console.WriteLine("    INTERACTIVE DYNAMIC STRING TRANSFORMATION    ");
            Console.WriteLine("=================================================");
            Console.Write("Enter your starting text: ");
            string text = Console.ReadLine() ?? string.Empty;

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=================================================");
                Console.WriteLine($"CURRENT TEXT: \"{text}\"");
                Console.WriteLine($"LENGTH:       {text.Length} characters");
                Console.WriteLine("=================================================");
                Console.WriteLine("--- SEARCH & EXTRACTION ---");
                Console.WriteLine(" 1. Substring / Slicing      (.Substring)");
                Console.WriteLine(" 2. Search Substring Index   (.IndexOf / .LastIndexOf)");
                Console.WriteLine(" 3. Prefix/Suffix Checks     (.StartsWith / .EndsWith / .Contains)");
                Console.WriteLine("--- MUTATION & REPLACEMENT ---");
                Console.WriteLine(" 4. Replace Characters/Words (.Replace)");
                Console.WriteLine(" 5. Insert New Text at Index (.Insert)");
                Console.WriteLine(" 6. Remove Character Range   (.Remove)");
                Console.WriteLine("--- TOKENIZATION & FORMATTING ---");
                Console.WriteLine(" 7. Split & Rejoin by Custom Delimiter (.Split / string.Join)");
                Console.WriteLine(" 8. Custom Padding & Align   (.PadLeft / .PadRight)");
                Console.WriteLine(" 9. Trim Whitespace or Chars (.Trim / .TrimStart / .TrimEnd)");
                Console.WriteLine(" A. Case Conversions         (.ToUpper / .ToLower)");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine(" R. Reset to completely new text");
                Console.WriteLine(" Q. Quit");
                Console.WriteLine("=================================================");
                Console.Write("Choose an action: ");

                char choice = char.ToUpper(Console.ReadKey().KeyChar);
                Console.WriteLine("\n");

                switch (choice)
                {
                    case '1':
                        ExecuteSubstring(text, ref text);
                        break;
                    case '2':
                        ExecuteSearch(text);
                        break;
                    case '3':
                        ExecutePrefixSuffixCheck(text);
                        break;
                    case '4':
                        ExecuteReplace(ref text);
                        break;
                    case '5':
                        ExecuteInsert(ref text);
                        break;
                    case '6':
                        ExecuteRemove(ref text);
                        break;
                    case '7':
                        ExecuteSplitAndJoin(ref text);
                        break;
                    case '8':
                        ExecutePadding(ref text);
                        break;
                    case '9':
                        ExecuteTrim(ref text);
                        break;
                    case 'A':
                        ExecuteCaseChange(ref text);
                        break;
                    case 'R':
                        Console.Write("Enter new working text: ");
                        text = Console.ReadLine() ?? string.Empty;
                        break;
                    case 'Q':
                        running = false;
                        Console.WriteLine("Closing session.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press any key to retry...");
                        Console.ReadKey();
                        break;
                }

                if (running && choice != 'R')
                {
                    Console.Write("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        #region Method Handlers

        // 1. DYNAMIC SUBSTRING
        static void ExecuteSubstring(string current, ref string state)
        {
            Console.WriteLine("--- Dynamic Substring ---");
            Console.WriteLine($"Valid start indices: 0 to {Math.Max(0, current.Length - 1)}");
            int start = PromptInt("Enter starting index: ", 0, current.Length);

            Console.WriteLine("1. Extract to end of string");
            Console.WriteLine("2. Extract specific character length");
            Console.Write("Choice (1 or 2): ");
            char subChoice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            string result;
            if (subChoice == '2')
            {
                int maxLen = current.Length - start;
                int len = PromptInt($"Enter length (1 to {maxLen}): ", 1, maxLen);
                result = current.Substring(start, len);
            }
            else
            {
                result = current.Substring(start);
            }

            Console.WriteLine($"\nExtracted Substring: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 2. DYNAMIC INDEX SEARCH
        static void ExecuteSearch(string current)
        {
            Console.WriteLine("--- Dynamic Index Search ---");
            Console.Write("Enter term or character to locate: ");
            string term = Console.ReadLine();

            if (string.IsNullOrEmpty(term))
            {
                Console.WriteLine("Search term cannot be empty.");
                return;
            }

            int firstIdx = current.IndexOf(term, StringComparison.OrdinalIgnoreCase);
            int lastIdx = current.LastIndexOf(term, StringComparison.OrdinalIgnoreCase);

            if (firstIdx == -1)
            {
                Console.WriteLine($"The term \"{term}\" was not found in the string.");
            }
            else
            {
                Console.WriteLine($"First match at index: {firstIdx}");
                Console.WriteLine($"Last match at index:  {lastIdx}");
            }
        }

        // 3. PREFIX / SUFFIX / CONTAINS
        static void ExecutePrefixSuffixCheck(string current)
        {
            Console.WriteLine("--- Prefix, Suffix & Contains Inspection ---");
            Console.Write("Enter check phrase: ");
            string phrase = Console.ReadLine() ?? "";

            Console.WriteLine($".StartsWith(\"{phrase}\"): {current.StartsWith(phrase, StringComparison.OrdinalIgnoreCase)}");
            Console.WriteLine($".EndsWith(\"{phrase}\"):   {current.EndsWith(phrase, StringComparison.OrdinalIgnoreCase)}");
            Console.WriteLine($".Contains(\"{phrase}\"):   {current.Contains(phrase, StringComparison.OrdinalIgnoreCase)}");
        }

        // 4. DYNAMIC REPLACE
        static void ExecuteReplace(ref string state)
        {
            Console.WriteLine("--- Dynamic Replace ---");
            Console.Write("Enter what to find: ");
            string oldValue = Console.ReadLine();

            if (string.IsNullOrEmpty(oldValue))
            {
                Console.WriteLine("Search target cannot be empty.");
                return;
            }

            Console.Write($"Enter replacement for \"{oldValue}\": ");
            string newValue = Console.ReadLine() ?? "";

            string result = state.Replace(oldValue, newValue);
            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 5. DYNAMIC INSERT
        static void ExecuteInsert(ref string state)
        {
            Console.WriteLine("--- Dynamic Insert ---");
            int index = PromptInt($"Enter index to insert at (0 to {state.Length}): ", 0, state.Length);
            Console.Write("Enter text to insert: ");
            string insertion = Console.ReadLine() ?? "";

            string result = state.Insert(index, insertion);
            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 6. DYNAMIC REMOVE
        static void ExecuteRemove(ref string state)
        {
            Console.WriteLine("--- Dynamic Remove ---");
            if (state.Length == 0)
            {
                Console.WriteLine("String is already empty.");
                return;
            }

            int start = PromptInt($"Enter start index to remove (0 to {state.Length - 1}): ", 0, state.Length - 1);
            int maxChars = state.Length - start;
            int count = PromptInt($"How many characters to delete (1 to {maxChars}): ", 1, maxChars);

            string result = state.Remove(start, count);
            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 7. DYNAMIC SPLIT & JOIN
        static void ExecuteSplitAndJoin(ref string state)
        {
            Console.WriteLine("--- Dynamic Split & Rejoin ---");
            Console.Write("Enter delimiter character to split text by (e.g. space, comma, dash): ");
            string delimInput = Console.ReadLine();
            char delimiter = string.IsNullOrEmpty(delimInput) ? ' ' : delimInput[0];

            string[] tokens = state.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine($"\nSplit generated {tokens.Length} token(s):");
            for (int i = 0; i < tokens.Length; i++)
            {
                Console.WriteLine($"  [{i}] {tokens[i]}");
            }

            Console.Write("\nEnter new separator string to rejoin them with: ");
            string glue = Console.ReadLine() ?? "";
            string result = string.Join(glue, tokens);

            Console.WriteLine($"\nRejoined: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 8. DYNAMIC PADDING
        static void ExecutePadding(ref string state)
        {
            Console.WriteLine("--- Dynamic Alignment Padding ---");
            Console.WriteLine("1. Pad Left");
            Console.WriteLine("2. Pad Right");
            Console.Write("Choice (1 or 2): ");
            char padType = Console.ReadKey().KeyChar;
            Console.WriteLine();

            int targetLength = PromptInt($"Enter total padded width (minimum {state.Length}): ", state.Length, 150);
            Console.Write("Enter single character to pad with (leave empty for space): ");
            string padCharInput = Console.ReadLine();
            char padChar = string.IsNullOrEmpty(padCharInput) ? ' ' : padCharInput[0];

            string result = (padType == '1') ? state.PadLeft(targetLength, padChar) : state.PadRight(targetLength, padChar);
            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // 9. DYNAMIC TRIM
        static void ExecuteTrim(ref string state)
        {
            Console.WriteLine("--- Dynamic Trim ---");
            Console.WriteLine("1. Trim both sides");
            Console.WriteLine("2. Trim Start only");
            Console.WriteLine("3. Trim End only");
            Console.Write("Choice (1-3): ");
            char trimType = Console.ReadKey().KeyChar;
            Console.WriteLine();

            Console.Write("Optional: Enter custom characters to trim off (leave empty for standard whitespace): ");
            string customChars = Console.ReadLine();

            string result = state;
            char[] charsToTrim = string.IsNullOrEmpty(customChars) ? null : customChars.ToCharArray();

            if (charsToTrim == null)
            {
                if (trimType == '1') result = state.Trim();
                else if (trimType == '2') result = state.TrimStart();
                else if (trimType == '3') result = state.TrimEnd();
            }
            else
            {
                if (trimType == '1') result = state.Trim(charsToTrim);
                else if (trimType == '2') result = state.TrimStart(charsToTrim);
                else if (trimType == '3') result = state.TrimEnd(charsToTrim);
            }

            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        // A. CASE CONVERSIONS
        static void ExecuteCaseChange(ref string state)
        {
            Console.WriteLine("1. UPPERCASE (.ToUpper())");
            Console.WriteLine("2. lowercase (.ToLower())");
            Console.Write("Choice (1 or 2): ");
            char opt = Console.ReadKey().KeyChar;
            Console.WriteLine();

            string result = (opt == '1') ? state.ToUpper() : state.ToLower();
            Console.WriteLine($"\nResulting string: \"{result}\"");
            PromptApplyChange(result, ref state);
        }

        #endregion

        #region Helper Functions
        static int PromptInt(string message, int min, int max)
        {
            int value;
            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max);
            return value;
        }

        static void PromptApplyChange(string newResult, ref string state)
        {
            Console.Write("\nKeep this result as your current text for future steps? (Y/N): ");
            char key = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (char.ToUpper(key) == 'Y')
            {
                state = newResult;
                Console.WriteLine("Working text updated!");
            }
        }
        #endregion
    }
}