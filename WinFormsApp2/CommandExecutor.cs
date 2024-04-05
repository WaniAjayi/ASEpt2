
using System.Text.RegularExpressions;

namespace WinFormsApp2
{
    /// <summary>
    /// Class responsible for executing drawing commands on a canvas.
    /// </summary>
    public class CommandExecutor
    {
        private readonly Canvas canvas;
        private readonly DrawTo drawTo;
        private readonly MoveTo moveTo;
        private readonly CursorManager cursorManager;
        private readonly Graphics graphics;
        private Color color;
        private readonly Rect rect;
        private readonly Triangle triangle;
        private readonly Circle circle;
        private readonly ClearCanvas clearCanvas;
        private readonly PenReset penReset;
        private bool fillEnabled; //Default fill state
        private Color FillColor; // Default fill colour
        private readonly VariableManager variableManager = new VariableManager();
        private readonly PictureBox pictureBox1;
        private LoopConditionManager loopConditionManager = new LoopConditionManager();
        /// <summary>
        /// Constructor initializes the necessary components for drawing on a canvas.
        /// </summary>
        /// <param name="canvasWidth">Width of the canvas.</param>
        /// <param name="canvasHeight">Height of the canvas.</param>
        /// <param name="graphics">Graphics object for drawing on a surface.</param>
        /// <param name="color">Color used for drawing.</param>
        public CommandExecutor(int canvasWidth, int canvasHeight, Graphics graphics, Color color, PictureBox pictureBox)
        {
            this.pictureBox1 = pictureBox;
            this.graphics = graphics;
            this.color = color;


            // Initialize canvas and drawing components
            canvas = new Canvas(canvasWidth, canvasHeight);
            cursorManager = new CursorManager();

            drawTo = new DrawTo(canvas, cursorManager);
            moveTo = new MoveTo(canvas, cursorManager);
            rect = new Rect(cursorManager);
            triangle = new Triangle(canvas, cursorManager);
            circle = new Circle(canvas, cursorManager);
            clearCanvas = new ClearCanvas(pictureBox1);
            penReset = new(cursorManager);

        }

        /// <summary>
        /// Execute a list of drawing commands on the canvas.
        /// </summary>
        /// <param name="commands">List of drawing commands.</param>
      

   

        public void ExecuteCommands(List<String> commands)
        {
            foreach (var command in commands)
            {
                String processedCommand = ResolveVariablesInCommand(command);
                ExecuteCommand(processedCommand);
            }
        }
       


        private string ResolveVariablesInCommand(string command)
        {
            var regex = new Regex(@"\b(?<variableName>[a-zA-Z]+)\b");
            var matches = regex.Matches(command);

            foreach (Match match in matches)
            {
                var variableName = match.Groups["variableName"].Value;

                // CheckVariable method to attempt to get the variable's value
                var value = VariableManager.CheckVariable(variableName);

                // CheckVariable returns the variable name as a string if the variable does not exist
                if (value is not string strValue || !strValue.Equals(variableName))
                {
                    // If the value is not simply the variable name, replace it in the command
                    var replacementValue = value.ToString();
                    command = Regex.Replace(command, $@"\b{Regex.Escape(variableName)}\b", replacementValue);
                }
            }

            return command;
        }

        public void ProcessWhileLoop(List<string> whileLoopCommands)
        {
            // Remove the "while" keyword and condition
            string originalCondition = whileLoopCommands[0].Substring(5).Trim();
            whileLoopCommands.RemoveAt(0); // Removes the while condition from commands list

            while (LoopConditionManager.EvaluateCondition(originalCondition))
            {
                foreach (var command in whileLoopCommands)
                {
                    ExecuteCommand(command);
                }
            }
        }

        public void ProcessIfBlock(List<string> ifBlockCommands)
        {
            // Extract the condition from the first command and parse it
            string condition = ifBlockCommands[0].Substring(2).Trim(); // Remove "if" keyword
            ifBlockCommands.RemoveAt(0); // Remove the condition from the commands list

            // Evaluate the condition - implement EvaluateCondition to return true or false based on condition logic
            if (LoopConditionManager.EvaluateCondition(condition))
            {
                foreach (var command in ifBlockCommands)
                {
                    ExecuteCommand(command); // Executes each command in the if block
                }
            }
        }



        /// <summary>
        /// Execute a single drawing command on the canvas.
        /// </summary>
        /// <param name="command">Drawing command to be executed.</param>
        public void ExecuteCommand(string command)
        {
            // Checks the type of command using regular expressions and execute corresponding action
            if (Regex.IsMatch(command, @"drawto\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)"))
            {
                
                Match match = Regex.Match(command, @"drawto\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)");
                int targetX = VariableManager.TryGetVariableOrLiteralValue(match.Groups[1].Value);
                int targetY = VariableManager.TryGetVariableOrLiteralValue(match.Groups[2].Value);

                // Executes the drawto command
                drawTo.DrawLineTo(targetX, targetY, graphics, color, 2);
            }
            else if (Regex.IsMatch(command, @"moveto\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)"))
            {
                // Extracts offset values from the command
                Match match = Regex.Match(command, @"moveto\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)");
                int xOffsetValue = VariableManager.TryGetVariableOrLiteralValue(match.Groups[1].Value);
                int yOffsetValue = VariableManager.TryGetVariableOrLiteralValue(match.Groups[2].Value);

                // Executes the moveto command
                moveTo.Execute(graphics, color, 2, xOffsetValue, yOffsetValue);
            }
            else if (Regex.IsMatch(command, @"rect\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)"))
            {
                // Extracts width and height from the command
                Match match = Regex.Match(command, @"rect\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)");
                int width = VariableManager.TryGetVariableOrLiteralValue(match.Groups[1].Value);
                int height = VariableManager.TryGetVariableOrLiteralValue(match.Groups[2].Value);


                var parameters = new object[]
                {
                    graphics, color,  2,  width, height, fillEnabled, FillColor
                };

                rect.Execute(parameters);
            }
            else if (Regex.IsMatch(command, @"trig\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)"))
            {
                // Extracts sidelength from the command
                Match match = Regex.Match(command, @"trig\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*),\s?(\d+|[a-zA-Z_][a-zA-Z0-9_]*)");
                int sideLength = VariableManager.TryGetVariableOrLiteralValue(match.Groups[1].Value);

                var parameters = new object[]
                {
                    graphics, color,  2,  sideLength, fillEnabled
                };
                // Executes the triangle command
                triangle.Execute(parameters);
            }
            else if (Regex.IsMatch(command, @"^circle\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*)"))
            {
                // Extract radius as either a number or a variable name
                Match match = Regex.Match(command, @"^circle\s(\d+|[a-zA-Z_][a-zA-Z0-9_]*)");
                int radius = VariableManager.TryGetVariableOrLiteralValue(match.Groups[1].Value); // Resolves both variables and constants

                // Parameters for executing the circle command
                var parameters = new object[]
                {
                    graphics, color, 2, radius, fillEnabled
                };

                // Executes the circle command
                circle.Execute(parameters);
            }

            else if (Regex.IsMatch(command, @"clear"))
            {
                // Execute the clear command
                clearCanvas.Execute(Color.SlateGray);
            }
            else if (Regex.IsMatch(command, @"reset"))
            {
                // Execute the reset command
                penReset.Execute(graphics, color, 2, 5, 5);
            }
            else if (Regex.IsMatch(command, @"run"))
            {
                ExecuteCommand(command);
            }
            else if (Regex.IsMatch(command, @"\bfill (on|off)\s(color:(red|green|blue|yellow))"))
            {
                Match match = Regex.Match(command, @"\bfill (on|off)\s(color:(red|green|blue|yellow))");
                string fillStatus = match.Groups[1].Value;  // "on" or "off"
                string fillColor = match.Groups[2].Value;   //Extract fill colour

                //Update the fill colour based on input
                FillColor = Color.FromName(fillColor);
                fillEnabled = Regex.Match(command, @"\bfill (on|off)\s(color:(red|green|blue|yellow))\b").Groups[1].Value.ToLower() == "on";
            }
            else if (Regex.IsMatch(command, @"\bpen (red|green|blue|yellow)\b"))
            {
                Match match = Regex.Match(command, @"\bpen (red|green|blue|yellow)\b");
                string colorName = match.Groups[1].Value.ToLower();
                switch (colorName)
                {
                    case "red":
                        color = Color.Red;
                        break;
                    case "green":
                        color = Color.Green;
                        break;
                    case "blue":
                        color = Color.Blue;
                        break;
                    case "yellow":
                        color = Color.Yellow;
                        break;
                    default:
                        // Handle unsupported color
                        MessageBox.Show("Unsupported pen color: " + colorName);
                        break;
                }
                ExecuteCommand(command);
            }
            else if (TryParseVariableAssignmentWithOperation(command))
            {

                return;
            }
            else
            {
                MessageBox.Show($"Unrecognized command: {command}");
            }
        }

        private bool TryParseVariableAssignmentWithOperation(string command)
        {
            var match = Regex.Match(command, @"^\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*=\s*([a-zA-Z_][a-zA-Z0-9_]*)\s*(\+|-|\*|\/)\s*(\d+)\s*$");
            if (match.Success)
            {
                string variableName = match.Groups[1].Value;
                string sourceVariableName = match.Groups[2].Value;
                string operation = match.Groups[3].Value;
                int operand = int.Parse(match.Groups[4].Value);

                
                object sourceValueObj = VariableManager.CheckVariable(sourceVariableName);
                int sourceValue = 0;

                // Checks if the sourceValueObj is an integer or needs conversion
                if (sourceValueObj is int)
                {
                    sourceValue = (int)sourceValueObj;
                }
                else if (sourceValueObj is string && int.TryParse((string)sourceValueObj, out int parsedValue))
                {
                    sourceValue = parsedValue;
                }
                else
                {
                    throw new InvalidOperationException($"The variable '{sourceVariableName}' could not be found or is not an integer.");
                }

                // Performs the necessary operation
                int result = sourceValue;
                switch (operation)
                {
                    case "+": result += operand; break;
                    case "-": result -= operand; break;
                    case "*": result *= operand; break;
                    case "/":
                        if (operand == 0)
                        {
                            throw new InvalidOperationException("Division by zero is not allowed.");
                        }
                        result /= operand; break;
                    default: throw new ArgumentException("Invalid operator.");
                }

                VariableManager.AssignVariable(variableName, result);

                return true;
            }
            return false;
        }

        private bool TryParseVariableComparison(string command)
        {
            var regex = new Regex(@"^\s*(\w+)\s*(<|>|==|!=|<=|>=)\s*(\w+)\s*$");
            var match = regex.Match(command);

            if (!match.Success)
            {
                throw new ArgumentException($"Invalid condition format: {command}");
            }

            // Extract and resolve operands
            int leftOperand = (int)VariableManager.CheckVariable(match.Groups[1].Value);
            int rightOperand = (int)VariableManager.CheckVariable(match.Groups[3].Value);
            String operatorSymbol = match.Groups[2].Value;

            // Perform comparison
            return LoopConditionManager.PerformComparison(leftOperand, operatorSymbol, rightOperand);
        }


        /// <summary>
        /// Get the bitmap representation of the canvas.
        /// </summary>
        /// <returns>Graphics object representing the canvas.</returns>
        /// 
        public Graphics GetCanvasBitmap()
        {
            return canvas.GetCanvasBitmap();
        }

        /// <summary>
        /// Save the program commands to a text file.
        /// </summary>
        /// <param name="filePath">The path to the file where the program will be saved.</param>
        /// <param name="commands">The list of commands to save to the file.</param>
        /// <remarks>
        /// Each command in the list will be written to a new line in the text file.
        /// </remarks>
    }
}
