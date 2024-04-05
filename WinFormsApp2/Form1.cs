using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents the main form of the application, handling user inputs for command execution and file operations.
    /// </summary>
    public partial class Form1 : Form
    {
        private CommandFactory commandFactory;
        private CommandExecutor commandExecutor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class, setting up command parsing and execution.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            // Instantiate CommandExecutor with necessary parameters
            commandExecutor = new CommandExecutor(pictureBox1.Width, pictureBox1.Height, pictureBox1.CreateGraphics(), Color.Black, pictureBox1);

            // Pass the CommandExecutor instance to CommandParser
            commandFactory = new CommandFactory(commandExecutor);
        }

        private string[] commands = Array.Empty<string>();

        /// <summary>
        /// Handles the click event of the button used to execute commands. Parses and executes commands entered in a RichTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains no event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            commands = richTextBox1.Text.ToLower().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (commandFactory.ParseCommands(commands))
            {
                List<string> commandsToExecute = commandFactory.GetDelayedCommands();
                commandExecutor.ExecuteCommands(commandsToExecute);
            }
        }









        /// <summary>
        /// Handles the KeyPress event of the TextBox used for single-line command input. Executes the command when the Enter key is pressed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A <see cref="KeyPressEventArgs"/> that contains the event data.</param>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string singleLineCommand = textBox1.Text.ToLower().Trim();
                if (string.IsNullOrEmpty(singleLineCommand))
                {
                    MessageBox.Show("You haven't entered a command!");
                    e.Handled = true;
                    return;
                }

                textBox1.Clear();
            }
        }

        /// <summary>
        /// Handles the click event of the button used to save commands to a file. Displays a SaveFileDialog and saves commands entered in a RichTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains no event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                string[] lines = richTextBox1.Lines;
                File.WriteAllLines(fileName, lines);
                MessageBox.Show("Program saved!");
                richTextBox1.Clear();
            }
        }

        /// <summary>
        /// Handles the click event of the button used to load commands from a file. Displays an OpenFileDialog and loads commands into a RichTextBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains no event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                try
                {
                    string[] lines = File.ReadAllLines(fileName);
                    richTextBox1.Lines = lines;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
