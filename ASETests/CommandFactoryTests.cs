using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinFormsApp2;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2Tests
{
    [TestClass]
    public class CommandFactoryTests
    {
        private CommandFactory commandFactory;
        private CommandExecutor commandExecutor;

        [TestInitialize]
        public void Initialize()
        {
      
            Bitmap dummyBitmap = new Bitmap(1, 1);
            Graphics graphics = Graphics.FromImage(dummyBitmap);

            // This is a dummy PictureBox to satisfy the constructor requirement.
            PictureBox pictureBox = new PictureBox();

            commandExecutor = new CommandExecutor(100, 100, graphics, Color.Black, pictureBox);
            commandFactory = new CommandFactory(commandExecutor);
        }

        [TestMethod]
        public void ParseCommands_ValidDrawCommand_ReturnsTrue()
        {
            
            string[] commands = { "drawto 50, 50" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParseCommands_InvalidCommand_ReturnsFalse()
        {
            
            string[] commands = { "invalid 50, 50" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ParseCommands_ValidVariableAssignment_ReturnsTrue()
        {
            
            string[] commands = { "x = 100" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParseCommands_VariableUsedInDrawCommand_ReturnsTrue()
        {
            
            string[] commands = { "x = 100", "drawto x, 50" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), " Value must be greater than 0.")]
        public void ValueLessThanOrEqualToZero_ThrowsArgumentException()
        {
            
            string[] commands = { "x = 0", "while x < 10", "x = x + 1", "endwhile" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }

        public void ParseCommands_ValidWhileLoop_ReturnsTrue()
        {
            
            string[] commands = { "x = 2", "while x < 10", "x = x + 1", "endwhile" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ParseCommands_ValidIfCondition_ReturnsTrue()
        {
            
            string[] commands = { "x = 5", "if x == 5", "drawto 50, 50", "endif" };

            
            bool result = commandFactory.ParseCommands(commands);

            
            Assert.IsTrue(result);
        }
    }
}
