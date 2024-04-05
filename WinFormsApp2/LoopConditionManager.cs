using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    /// <summary>
    /// Manages and evaluates loop conditions to control the flow in an application.
    /// </summary>
    public class LoopConditionManager
    {
        /// <summary>
        /// Evaluates a loop condition based on a string representation.
        /// </summary>
        /// <param name="condition">The condition to be evaluated, formatted as a string.</param>
        /// <returns>A boolean value indicating whether the condition evaluates to true or false.</returns>
        /// <exception cref="ArgumentException">Thrown when the condition is in an invalid format.</exception>
        public static bool EvaluateCondition(string condition)
        {
            var match = Regex.Match(condition, @"^\s*((?:[a-zA-Z_][a-zA-Z0-9_]*)|\d+)\s*(==|!=|<|>|<=|>=)\s*((?:[a-zA-Z_][a-zA-Z0-9_]*)|\d+)\s*$");

            if (match.Success)
            {
                string operand1 = match.Groups[1].Value;
                string operatorVar = match.Groups[2].Value;
                string operand2 = match.Groups[3].Value;

                int value1 = VariableManager.TryGetVariableOrLiteralValue(operand1);
                int value2 = VariableManager.TryGetVariableOrLiteralValue(operand2);


              

                return PerformComparison(value1, operatorVar, value2);
            }
            else
            {
                throw new ArgumentException("Invalid condition format.");
            }
        }

        /// <summary>
        /// Performs a comparison operation between two integer operands.
        /// </summary>
        /// <param name="opVar1">The first operand in the comparison.</param>
        /// <param name="operatorVar">The operator used for comparison. Supported operators include ==, !=, <, >, <=, >=.</param>
        /// <param name="opVar2">The second operand in the comparison.</param>
        /// <returns>A boolean value indicating the result of the comparison operation.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid operator is specified.</exception>
        public static bool PerformComparison(int opVar1, string operatorVar, int opVar2)
        {
            switch (operatorVar)
            {
                case "==": return opVar1 == opVar2;
                case "!=": return opVar1 != opVar2;
                case "<": return opVar1 < opVar2;
                case ">": return opVar1 > opVar2;
                case "<=": return opVar1 <= opVar2;
                case ">=": return opVar1 >= opVar2;
                default: throw new ArgumentException("Invalid operator.");
            }
        }
    }
}
