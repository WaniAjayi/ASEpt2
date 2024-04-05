using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WinFormsApp2
{
    /// <summary>
    /// Parses and validates a set of commands for a drawing application.
    /// </summary>
    public class CommandFactory
    {
        private readonly CommandExecutor commandExecutor;

        public CommandFactory(CommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }


        private string[] regexPatterns =
          {
            @"drawto\s(\d+|[a-zA-Z]+),\s?(\d+|[a-zA-Z]+)",
            @"moveto\s(\d+|[a-zA-Z]+),\s?(\d+|[a-zA-Z]+)",
            @"rect\s(\d+|[a-zA-Z]+),\s?(\d+|[a-zA-Z]+)",
            @"trig\s(\d+|[a-zA-Z]+),\s?(\d+|[a-zA-Z]+)",
            @"^circle\s(\d+|[a-zA-Z]+)",
            @"clear",
            @"reset",
            @"run",
            @"\bfill (on|off)\s(color:(red|green|blue|yellow))",
            @"\bpen (red|green|blue|yellow)\b",
            @"while\s*(.+)",
            @"endwhile",
            @"if\s*(.+)",
            @"endif"
        };


        private VariableManager variableManager = new VariableManager();
        private List<string> delayedCommands = new List<string>();



        // Parses multi-line commands from the input
        public bool ParseCommands(string[] lines)
        {
            bool allCommandsValid = true;
            List<string> whileLoopCommands = new List<string>();
            List<string> ifBlockCommands = new List<string>();

            bool inWhileLoop = false;
            bool inIfBlock = false;

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();

                // Check for variable assignment outside of loop
                if (!inWhileLoop && TryParseVariableAssignment(trimmedLine))
                {
                    continue;
                }

                // Handles the start of an if block
                if (!inWhileLoop && trimmedLine.StartsWith("if"))
                {
                    inIfBlock = true;
                    ifBlockCommands.Add(trimmedLine); // Store the if condition for later evaluation
                    continue;
                }

                // Handles the end of an if block
                if (inIfBlock && trimmedLine.Equals("endif"))
                {
                    inIfBlock = false;
                    commandExecutor.ProcessIfBlock(ifBlockCommands); // Process the collected if block commands
                    ifBlockCommands.Clear(); // Clear the commands for the next possible block
                    continue;
                }

                // Handles while loop start
                if (trimmedLine.StartsWith("while"))
                {
                    inWhileLoop = true;
                    whileLoopCommands.Add(trimmedLine); // Add the condition for later evaluation
                    continue;
                }

                // Handles while loop end
                if (inWhileLoop && trimmedLine == "endwhile")
                {
                    inWhileLoop = false;
                    commandExecutor.ProcessWhileLoop(whileLoopCommands);
                    //delayedCommands.AddRange(loopCommands); // Add the loop commands to delayedCommands for sequential execution
                    whileLoopCommands.Clear();

                    continue;

                }
                if (inIfBlock)
                {
                    ifBlockCommands.Add(trimmedLine);
                    continue;
                }

                if (inWhileLoop)
                {
                    // Collect commands within the loop
                    whileLoopCommands.Add(trimmedLine);
                }
                else
                {
                    // Process commands outside of loops
                    bool matched = false;
                    foreach (var pattern in regexPatterns)
                    {
                        if (Regex.IsMatch(trimmedLine, pattern))
                        {
                            delayedCommands.Add(trimmedLine);
                            matched = true;
                            break;
                        }
                    }
                    if (!matched)
                    {
                        allCommandsValid = false;
                    }
                }
            }

            return allCommandsValid;
        }

        private bool TryParseVariableAssignment(string line)
        {
            var match = Regex.Match(line, @"^([a-zA-Z]+)\s*=\s*(\d+)$");
            if (match.Success)
            {
                string variableName = match.Groups[1].Value;
                int value = int.Parse(match.Groups[2].Value);

                VariableManager.AssignVariable(variableName, value);
                return true;
            }
            return false;
        }




        public List<string> GetDelayedCommands()
        {
            // This returns the list of verified commands for onward execution
            return delayedCommands;
        }
    }

}
