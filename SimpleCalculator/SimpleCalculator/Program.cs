/*
 * Jordan Medeiros 
 * Chosen language: C# 
 * Title: Encircle coding test
 * For: Mike Kirkup
 * 
 *  - Command line program that will take inputs until the phrase "exit" is entered.
 *  - Supports simple numbers.
 *  - Supports simple add expressions.
 *  - Supports simply multiplication expressions.
 *  - Supports nested expressions to an abritrary depth.
 *  - Supports arbitrary number of arguments.
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SimpleCalculator;
namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Simple Calculator: ");
            bool exitProgram = false;
            while (!exitProgram)
            {
                    string input = "";             
                    input = Console.ReadLine();
                    Console.WriteLine(Calculator(input));
                    if (input == "exit")
                    {
                        exitProgram = true;
                    } 
            }
        }

        public static string Calculator(string input)
        {
            char[] array = input.ToCharArray();           
            List<string> inputList = new List<string>();
            foreach (char item in array)
            {
                inputList.Add(item.ToString());
            }
            //simple integers
            //if (input.Contains(Regex.Match(input, @"\d+").Value) && !input.Contains("add") || !input.Contains("multiply"))
            //{
            //    return input;
            //}
            string tempStringInput = string.Join("", inputList.ToArray());
            if (inputList.Contains("a") && inputList.Contains("m"))
            {
                do
                {
                    //retrieves inner most nested expression. 
                    for (int i = 0; i < inputList.Count; i++)
                    {
                        string tempString = "";
                        int rightParenthesisPos = 0;
                        //find right parenthesis to obtain boundary position.
                        if (inputList[i].Equals(")"))
                        {
                            rightParenthesisPos = i;
                        }
                        for (int j = rightParenthesisPos; j > 0; j--)
                        {
                            //starting from the right parenthesis boundary. Search for left parenthesis.
                            if (inputList[j].Equals("("))
                            {
                                //tempString is refreshed here to obtain a new expression.
                                tempString = "";
                                if (rightParenthesisPos == 0)
                                {
                                    for (int n = 0; n < inputList.Count; n++)
                                    {
                                        if (inputList[n].Equals(")"))
                                        {
                                            rightParenthesisPos = n;
                                            break;
                                        }
                                    }
                                }
                                for (int z = j + 1; z < rightParenthesisPos; z++)
                                {
                                    //tempString being populated with expression.
                                    tempString += inputList[z].ToString();
                                }
                                //"a" represents the start of function "add".
                                if (inputList[j + 1].Equals("a"))
                                {
                                    //inserts the new value obtained from function "Addition" into the list where the nested expression was.
                                    inputList.Insert(j, Addition(tempString));
                                }
                                //"m" represents the start of function "multiply"
                                else if (inputList[j + 1].Equals("m"))
                                {
                                    //inserts the new value obtained from function "Multiply" into the list where the nested expression was.
                                    inputList.Insert(j, Multiply(tempString));
                                }
                                //removes the nested expression from list
                                inputList.RemoveRange(j + 1, rightParenthesisPos - j + 1);                                
                                rightParenthesisPos = 0;
                            }
                        }
                    }                                                         
                        string combinedString = string.Join("", inputList.ToArray());
                        if (combinedString.Contains("multiply"))
                        {                            
                            return Multiply(combinedString);
                        }
                        else if (combinedString.Contains("add"))
                        {
                            return Addition(combinedString);
                        }
                        else
                        {
                            return combinedString;
                        }
                } while (!inputList[1].Equals("a") || !inputList[1].Equals("m"));
            }

            // if the input made it this far it either only contains "multiply" or "add" or jiberish.      
            if (tempStringInput.Contains("multiply"))
            {
                return Multiply(tempStringInput);
            }
            else if (tempStringInput.Contains("add"))
            {
                return Addition(tempStringInput);
            }
            else
            {
                //returns simple integers
                return tempStringInput;
            }
        }

        //<summary> 
        // Addition: This function takes an input of string type 
        // finds any integers and returns the calculated number 
        // in the form of a string.        
        //</summary>
        public static string Addition(string input)
        {
            List<double> numberContainer = new List<double>();
            string findInt = "";
            string[] numbers = input.Split(new char[] { ' ' });
            double addedNumber = 0;
            
            if (input.Contains("add"))
            {
                for (int i = 0; i < numbers.Length; i++)
                {                   
                    findInt = Regex.Match(numbers[i], @"\d+").Value;
                    if (findInt != "")
                    {
                        //finds digits and adds to list
                        numberContainer.Add(double.Parse(findInt));
                    }
                }
                //supporting arbitrary number arguments.
                for (int i = 0; i < numberContainer.Count; i++)
                {
                    addedNumber += numberContainer[i];
                }
                return addedNumber.ToString();
            }
            else
            {
                return "Error: Addition method";
            }

        }

        //<summary> 
        // Multiply: This function takes an input of string type 
        // finds any integers and returns the calculated number 
        // in the form of a string.        
        //</summary>
        public static string Multiply(string input)
        {
            List<double> numberContainer = new List<double>();
            string findInt = "";
            string[] numbers = input.Split(new char[] { ' ' });
            double multipliedNumber = 1;
            if (input.Contains("multiply"))
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    findInt = Regex.Match(numbers[i], @"\d+").Value;
                    if (findInt != "")
                    {
                        numberContainer.Add(double.Parse(findInt));
                    }
                }
                //supporting arbitrary number arguments.
                for (int i = 0; i < numberContainer.Count; i++)
                {
                    multipliedNumber *= numberContainer[i];
                }
                return multipliedNumber.ToString();
            }
            else
            {
                return "Error: Multiplication method";
            }
        }



    }
}
