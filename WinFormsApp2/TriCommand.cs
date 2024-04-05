using System.Drawing;
using System;
namespace WinFormsApp2
{
    /// <summary>
    /// Represents a command to draw a triangle on a canvas.
    /// </summary>
    public class Triangle : ICommand
    {
        private readonly Canvas canvas;
        private readonly CursorManager cursorManager;
        private Color fillColor = Color.Transparent; // Default fill color

        /// <summary>
        /// Initializes a new instance of the <see cref="Triangle"/> class.
        /// </summary>
        /// <param name="canvas">The canvas where the triangle will be drawn.</param>
        /// <param name="cursorManager">The cursor manager to determine the starting point of the triangle.</param>
        public Triangle(Canvas canvas, CursorManager cursorManager)
        {
            this.canvas = canvas;
            this.cursorManager = cursorManager;
        }

        /// <summary>
        /// Executes the triangle drawing operation with specified parameters.
        /// </summary>
        /// <param name="parameters">An array of parameters needed for drawing the triangle, including Graphics, Color, line width, side length, and fillEnabled flag.</param>
        /// <exception cref="ArgumentException">Thrown when the parameters array is null, has an incorrect length, or contains invalid types.</exception>
        public void Execute(object[] parameters)
        {
            if (parameters == null || parameters.Length != 5)
                throw new ArgumentException("Execute requires exactly 5 parameters: Graphics, Color, int, int, bool.");

            try
            {
                var g = (Graphics)parameters[0];
                var fillColor = (Color)parameters[1];
                var lineWidth = Convert.ToInt32(parameters[2]);
                var width = Convert.ToInt32(parameters[3]);
                var fillEnabled = Convert.ToBoolean(parameters[4]);

                Draw(g, fillColor, lineWidth, width, fillEnabled);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Parameter array contains invalid types.");
            }
        }

        /// <summary>
        /// Draws a triangle on the provided Graphics object.
        /// </summary>
        /// <param name="g">The Graphics object to draw on.</param>
        /// <param name="fillColor">The color to fill the triangle.</param>
        /// <param name="lineWidth">The width of the lines of the triangle.</param>
        /// <param name="sideLength">The length of each side of the triangle.</param>
        /// <param name="fillEnabled">Whether the triangle should be filled.</param>
        public void Draw(Graphics g, Color fillColor, int lineWidth, int sideLength, bool fillEnabled)
        {
            if (lineWidth < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(lineWidth), "lineWidth cannot be negative.");
            }
            if (sideLength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sideLength), "sideLength cannot be negative.");
            }

            int startX = cursorManager.CurrentX;
            int startY = cursorManager.CurrentY;

            Point[] points = new Point[]
            {
                new Point(startX, startY),
                new Point(startX + sideLength, startY),
                new Point(startX - sideLength / 2, startY + sideLength)
            };

            if (fillEnabled)
            {
                using (SolidBrush brush = new SolidBrush(fillColor))
                {
                    g.FillPolygon(brush, points);
                }
            }
            else
            {
                using (Pen pen = new Pen(fillColor, lineWidth))
                {
                    g.DrawPolygon(pen, points);
                }
            }
        }
    }
}
