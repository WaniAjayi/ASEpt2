using System;
using System.Drawing;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents a rectangle drawing command that can be executed within a drawing context.
    /// </summary>
    public class Rect : ICommand
    {
        private readonly CursorManager cursorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rect"/> class with the specified cursor manager.
        /// </summary>
        /// <param name="cursorManager">The cursormanager to be used for determining the starting position of the rectangle.</param>
        public Rect(CursorManager cursorManager)
        {
            this.cursorManager = cursorManager;
        }

        /// <summary>
        /// Executes the drawing operation using an array of parameters.
        /// </summary>
        /// <param name="parameters">Parameters array containing Graphics, color, lineWidth, width, height, fillEnabled, and fillColor.</param>
        /// <exception cref="ArgumentException">Thrown when the parameters array is null, has an incorrect length, or contains invalid types.</exception>
        public void Execute(object[] parameters)
        {
            if (parameters == null || parameters.Length != 7)
                throw new ArgumentException("Execute requires exactly 7 parameters: Graphics, Color, int, int, int, bool, Color.");

            try
            {
                var g = (Graphics)parameters[0];
                var color = (Color)parameters[1];
                var lineWidth = Convert.ToInt32(parameters[2]);
                var width = Convert.ToInt32(parameters[3]);
                var height = Convert.ToInt32(parameters[4]);
                var fillEnabled = Convert.ToBoolean(parameters[5]);
                var fillColor = (Color)parameters[6];

                Draw(g, color, lineWidth, width, height, fillEnabled, fillColor);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Parameter array contains invalid types.");
            }
        }

        /// <summary>
        /// Draws a rectangle using the specified Graphics object and other parameters.
        /// This method determines the rectangle's position based on the current cursor position and then draws or fills the rectangle.
        /// </summary>
        /// <param name="g">The Graphics object used for drawing.</param>
        /// <param name="color">The color of the rectangle's border.</param>
        /// <param name="lineWidth">The width of the rectangle's border.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="fillEnabled">Indicates whether the rectangle should be filled.</param>
        /// <param name="fillColor">The color to fill the rectangle with if fillEnabled is true.</param>
        private void Draw(Graphics g, Color color, int lineWidth, int width, int height, bool fillEnabled, Color fillColor)
        {
            if (lineWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lineWidth), "lineWidth cannot be negative.");
            }
            if (width < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width), "Width cannot be negative.");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height), "Height cannot be negative.");
            }

            int startX = cursorManager.CurrentX;
            int startY = cursorManager.CurrentY;

            if (fillEnabled)
            {
                using (SolidBrush brush = new SolidBrush(fillColor))
                {
                    g.FillRectangle(brush, startX, startY, width, height);
                }
            }
            else
            {
                using (Pen pen = new Pen(color, lineWidth))
                {
                    g.DrawRectangle(pen, startX, startY, width, height);
                }
            }
        }
    }
}
