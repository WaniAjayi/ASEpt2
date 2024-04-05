using System;
using System.Drawing;

namespace WinFormsApp2
{
    /// <summary>
    /// Represents a drawing surface that allows for various drawing operations.
    /// </summary>
    public class Canvas
    {
        private Bitmap bitmap;
        private Graphics graphics;

        /// <summary>
        /// Initializes a new instance of the <see cref="Canvas"/> class with a specified width and height.
        /// </summary>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        public Canvas(int width, int height)
        {
            bitmap = new Bitmap(width, height);
            graphics = Graphics.FromImage(bitmap);
        }

        /// <summary>
        /// Draws a line on the canvas.
        /// </summary>
        /// <param name="startX">The starting X coordinate of the line.</param>
        /// <param name="startY">The starting Y coordinate of the line.</param>
        /// <param name="endX">The ending X coordinate of the line.</param>
        /// <param name="endY">The ending Y coordinate of the line.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="lineWidth">The width of the line.</param>
        public void DrawLine(int startX, int startY, int endX, int endY, Color color, int lineWidth)
        {
            using (Pen pen = new Pen(color, lineWidth))
            {
                graphics.DrawLine(pen, startX, startY, endX, endY);
            }
        }

        /// <summary>
        /// Retrieves the <see cref="Graphics"/> object associated with the canvas.
        /// </summary>
        /// <returns>A <see cref="Graphics"/> object that can be used to perform drawing operations on the canvas.</returns>
        public Graphics GetCanvasBitmap()
        {
            return graphics;
        }
    }
}