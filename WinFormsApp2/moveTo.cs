using System.Drawing;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents a command to move the cursor to a new position on the canvas.
    /// </summary>
    public class MoveTo
    {
        private readonly Canvas canvas;
        private readonly CursorManager cursorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveTo"/> class with a specified canvas and cursor manager.
        /// </summary>
        /// <param name="canvas">The canvas where the movement will be applied.</param>
        /// <param name="cursorManager">The cursor manager responsible for tracking and updating the cursor's position.</param>
        public MoveTo(Canvas canvas, CursorManager cursorManager)
        {
            this.canvas = canvas;
            this.cursorManager = cursorManager;
        }

        /// <summary>
        /// Executes the move command, updating the cursor's position to a new location based on the specified offsets.
        /// </summary>
        /// <param name="g">The graphics context where the drawing occurs. Not used in this method but included for consistency with similar command structures.</param>
        /// <param name="color">The color of the line to draw. Not used in this method but included for consistency with similar command structures.</param>
        /// <param name="lineWidth">The width of the line to draw. Not used in this method but included for consistency with similar command structures.</param>
        /// <param name="xOffset">The horizontal offset to apply to the cursor's current position.</param>
        /// <param name="yOffset">The vertical offset to apply to the cursor's current position.</param>
        public void Execute(Graphics g, Color color, int lineWidth, int xOffset, int yOffset)
        {
            // Calculate the target position based on the current position and the specified offsets.
            int targetX = cursorManager.CurrentX + xOffset;
            int targetY = cursorManager.CurrentY + yOffset;

            // Move the cursor to the new position.
            cursorManager.MoveTo(targetX, targetY);
        }
    }
}
