using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;  // Include the System.Drawing namespace for the Color and Graphics classes
//using WinFormsApp2.WinFormsApp2;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents a class for drawing lines on a canvas from the current cursor position to a specified target position.
    /// </summary>
    public class DrawTo
    {
        private readonly Canvas canvas;
        private readonly CursorManager _cursorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawTo"/> class.
        /// </summary>
        /// <param name="canvas">The canvas on which to draw lines.</param>
        /// <param name="cursorManager">The cursor manager for managing cursor position.</param>
        public DrawTo(Canvas canvas, CursorManager cursorManager)
        {
            this.canvas = canvas;
            this._cursorManager = cursorManager;
        }

        /// <summary>
        /// Draws a line on the canvas from the current cursor position to the specified target position.
        /// </summary>
        /// <param name="targetX">The X-coordinate of the target position.</param>
        /// <param name="targetY">The Y-coordinate of the target position.</param>
        /// <param name="g">The graphics object used for drawing.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="lineWidth">The width of the line.</param>
        public void DrawLineTo(int targetX, int targetY, Graphics g, Color color, int lineWidth)
        {
            // Get the current cursor position
            int startX = _cursorManager.CurrentX;
            int startY = _cursorManager.CurrentY;

            // Draw the line on the canvas
            using (Pen pen = new Pen(color, lineWidth))
            {
                g.DrawLine(pen, startX, startY, targetX, targetY);
            }

            // Update cursor position
            _cursorManager.SetPosition(targetX, targetY);
        }
    }
}
