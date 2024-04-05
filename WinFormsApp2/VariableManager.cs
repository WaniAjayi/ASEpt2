using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    /// <summary>
    /// Manages variables within the application, allowing for the declaration, assignment, and retrieval of variable values.
    /// </summary>
    public class VariableManager
    {
        // Private dictionary to store variable names and values.
        private static readonly Dictionary<string, object> variableState = new();

        /// <summary>
        /// Checks if the variable with the specified name is already declared.
        /// </summary>
        /// <param name="variableName">The name of the variable to be checked.</param>
        /// <returns>The value of the variable if found; otherwise, returns the name of the variable itself.</returns>
        public static object CheckVariable(string variableName)
        {
            if (variableState.ContainsKey(variableName))
            {
                return variableState[variableName];
            }

            return variableName;
        }

        /// <summary>
        /// Sets the value of a variable.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="value">The value to set. Must be greater than 0 for integers and not null or empty for strings.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when an integer value is less than or equal to 0.</exception>
        /// <exception cref="ArgumentException">Thrown when a string value is null or empty.</exception>
        public static void AssignVariable(string variableName, object value)
        {
            if (value is int intValue && intValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Value must be greater than 0.");
            }

            if (value is string stringValue && string.IsNullOrEmpty(stringValue))
            {
                throw new ArgumentException("Value cannot be null or empty.", nameof(value));
            }

            variableState[variableName] = value;
        }

        /// <summary>
        /// Tries to parse the operand as an integer or retrieves the value of a variable.
        /// </summary>
        /// <param name="operand">The operand to evaluate, which can be a literal value or a variable name.</param>
        /// <returns>The integer value of the operand or variable.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the operand is not a valid integer and not a known variable name.</exception>
        public static int TryGetVariableOrLiteralValue(string operand)
        {
            if (int.TryParse(operand, out int operandValue))
            {
                // returns when Operand is a constant
                return operandValue;
            }
            else if (CheckVariable(operand) is int variableValue)
            {
                // returns when Operand is a variable
                return variableValue;
            }
            else
            {
                throw new InvalidOperationException($"Invalid operand in while loop condition: {operand}");
            }
        }
    }
}
