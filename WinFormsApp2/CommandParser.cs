using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GraphicalProgrammingEnvironment
{
    public class Parser
    {
        //private List<IGraphicsCommand> commandList;

        //public Parser()
        //{
        //    commandList = new List<IGraphicsCommand>();
        //}

        //public List<string> SplitCommands(string CommandInput)
        //{
        //    List<string> Commands = new List<string>();
        //    string[] lines = CommandInput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        //    foreach (string line in lines)
        //    {
        //        Commands.Add(line);
        //    }
        //    return Commands;
        //}

        //public void AddCommand(IGraphicsCommand command)
        //{
        //    if (command != null)
        //    {
        //        commandList.Add(command);
        //    }

        //    else
        //    { 
        //        throw new ArgumentNullException(nameof(command), "Command cannot be null."); 
        //    }
        //}

    }

    //public class CommandFactory
    //{
    //    private List<IGraphicsCommand> execCommandList;
    //    string commands { get; set; }
    //    public PictureBox pictureBox2 { get; }
    //    private readonly CursorManager cursorManager;
    //   // public CommandParser CommandParser { get; }
    //    private readonly Bitmap canvas;
    //    private int height;
    //    private int width;
    //    private Graphics g;
    //    private Color color;

    //    public CommandFactory(string Commands, PictureBox pictureBox2, CursorManager stateManager, Bitmap bitmap)
    //    {
    //        this.commands = Commands ?? throw new ArgumentNullException($"'{nameof(commands)}' cannot be null or empty.", nameof(commands));
    //        //CommandParser = new CommandParser();
    //        this.pictureBox2 = pictureBox2;
    //        this.cursorManager = stateManager;
    //        this.canvas = bitmap;
    //        this.width = pictureBox2.Width;
    //        this.height = pictureBox2.Height;
    //        this.g = pictureBox2.CreateGraphics();
    //        execCommandList = new List<IGraphicsCommand>();
    //    }

    //    public IGraphicsCommand CreateCommand(string commands)
    //    {
            

    //            if (string.IsNullOrEmpty(commands))
    //            {
    //                throw new ArgumentNullException($"'{nameof(commands)}' cannot be null or empty.", nameof(commands));
    //            }

    //        try
    //        {
    //            Match drawToMatch = Regex.Match(commands, @"drawto\s(\d+),\s?(\d+)");
    //            Match moveToMatch = Regex.Match(commands, @"moveto\s(\d+),\s?(\d+)");
    //            Match rectMatch = Regex.Match(commands, @"rect\s(\d+),\s?(\d+)");
    //            Match triMatch = Regex.Match(commands, @"trig\s(\d+),\s?(\d+)");
    //            Match circleMatch = Regex.Match(commands, @"^circle\s(\d+)");
    //            Match clearMatch = Regex.Match(commands, @"clear");
    //            Match resetMatch = Regex.Match(commands, @"reset");

    //            IGraphicsCommand? createdCommand = null;

    //            if (drawToMatch.Success)
    //            {
    //                int targetX = int.Parse(drawToMatch.Groups[1].Value);
    //                int targetY = int.Parse(drawToMatch.Groups[2].Value);
    //                createdCommand = new DrawTo(targetX, targetY, pictureBox2, cursorManager, g, color);
    //            }
    //            else if (moveToMatch.Success)
    //            {
    //                int targetX = int.Parse(Regex.Match(commands, @"moveto\s(\d+),\s?(\d+)").Groups[1].Value);
    //                int targetY = int.Parse(Regex.Match(commands, @"moveto\s(\d+),\s?(\d+)").Groups[2].Value);

    //                createdCommand = new MoveTo(targetX, targetY, cursorManager, canvas, pictureBox2);

    //            }
    //            else if (rectMatch.Success)
    //            {
    //                int targetX = int.Parse(Regex.Match(commands, @"rect\s(\d+),\s?(\d+)").Groups[1].Value);
    //                int targetY = int.Parse(Regex.Match(commands, @"rect\s(\d+),\s?(\d+)").Groups[2].Value);

    //                createdCommand = new RectangleCommand(targetX, targetY, width, height);
    //            }
    //            else if (triMatch.Success)
    //            {
    //                int targetX = int.Parse(Regex.Match(commands, @"trig\s(\d+),\s?(\d+)").Groups[1].Value);
    //                int targetY = int.Parse(Regex.Match(commands, @"trig\s(\d+),\s?(\d+)").Groups[2].Value);

    //                createdCommand = new TriangleCommand(targetX, targetY, cursorManager);
    //            }
    //            else if (circleMatch.Success)
    //            {
    //                int radius = int.Parse(Regex.Match(commands, @"circle\s(\d+)").Groups[1].Value);
    //                createdCommand = new CircleCommand(radius, cursorManager);
    //            }
    //            else if (clearMatch.Success)
    //            {
    //                createdCommand = new ClearCommand(pictureBox2);
    //            }
    //            else if (resetMatch.Success)
    //            {
    //                //int targetX = 15, targetY = 15;
    //                createdCommand = new ResetCommand(cursorManager, canvas, pictureBox2);
    //            }

    //            if (createdCommand != null)
    //            {
    //                execCommandList.Add(createdCommand);
    //                return createdCommand;
    //            }
    //            else

    //            { throw new InvalidOperationException("Unknown command type"); }

    //        }
    //        catch (Exception) { throw new InvalidOperationException("Unknown command type"); }
                

                     
                
    //    }
        
    //    public void ExecuteAllCommands()
    //    {
    //      foreach(var execCommand in execCommandList)
    //      execCommand.Execute( g, fill: false, color: Color.Red);
    //    }

    //}

    
}
