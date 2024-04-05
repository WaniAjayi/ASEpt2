using System.Drawing;
using System;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents a command to draw a circle on a canvas.
    /// </summary>
    public class Circle : ICommand
    {
        private readonly Canvas canvas;
        private readonly CursorManager cursorManager;
        private Color Color = Color.Transparent; // Default fill color

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class with a specified canvas and cursor manager.
        /// </summary>
        /// <param name="canvas">The canvas on which the circle will be drawn.</param>
        /// <param name="cursorManager">The cursor manager to determine the center position of the circle.</param>
        public Circle(Canvas canvas, CursorManager cursorManager)
        {
            this.canvas = canvas;
            this.cursorManager = cursorManager;
        }

        /// <summary>
        /// Executes the circle drawing operation with specified parameters.
        /// </summary>
        /// <param name="parameters">An array of parameters needed for drawing the circle, including Graphics, Color, line width, radius, and fillEnabled flag.</param>
        /// <exception cref="ArgumentException">Thrown when the parameters array is null, has an incorrect length, or contains invalid types.</exception>
        public void Execute(object[] parameters)
        {
            if (parameters == null || parameters.Length != 5)
                throw new ArgumentException("Execute requires exactly 5 parameters: Graphics, Color, int, int, bool.");

            try
            {
                var g = (Graphics)parameters[0];
                var color = (Color)parameters[1];
                var lineWidth = Convert.ToInt32(parameters[2]);
                var radius = Convert.ToInt32(parameters[3]);
                var fillEnabled = Convert.ToBoolean(parameters[4]);


                if (radius < 0)
                    throw new ArgumentOutOfRangeException(nameof(radius), "Radius cannot be negative");
                Draw(g, color, lineWidth, radius, fillEnabled);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Parameter array contains invalid types.");
            }
        }

        /// <summary>
        /// Draws a circle on the provided Graphics object.
        /// </summary>
        /// <param name="g">The Graphics object to draw on.</param>
        /// <param name="color">The color of the circle.</param>
        /// <param name="lineWidth">The width of the circle's outline.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="fillEnabled">Indicates whether the circle should be filled with color.</param>
        public void Draw(Graphics g, Color color, int lineWidth, int radius, bool fillEnabled)
        {
            int centerX = cursorManager.CurrentX;
            int centerY = cursorManager.CurrentY;

            int diameter = radius * 2;
            int x = centerX - radius;
            int y = centerY - radius;

            if (fillEnabled)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    g.FillEllipse(brush, x, y, diameter, diameter);
                }
            }
            else
            {
                using (Pen pen = new Pen(color, lineWidth))
                {
                    g.DrawEllipse(pen, x, y, diameter, diameter);
                }
            }
        }
    }
}
