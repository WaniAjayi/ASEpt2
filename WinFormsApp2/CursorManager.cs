using System;

namespace WinFormsApp2
{
    /// <summary>
    /// Manages the position of a cursor in a 2D coordinate system.
    /// </summary>
    public class CursorManager
    {
        /// <summary>
        /// Gets or sets the current X-coordinate of the cursor.
        /// </summary>
        public int CurrentX { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating a While loop is on or off.
        /// </summary>
        public bool whileLoopFlag;

        /// <summary>
        /// Gets or sets a value indicating an IF loop is on or off.
        /// </summary>
        public bool ifBlockFlag;

        /// <summary>
        /// Gets or sets a value indicating a nested While loop.
        /// </summary>
        public int WhileLoopCounter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a loop counter.
        /// </summary>
        public int LineCounter { get; set; }

        public int LoopCounter { get; set; }

        public bool ifMethodFlag { get; set; }

        /// <summary>
        /// Gets or sets the current Y-coordinate of the cursor.
        /// </summary>
        public int CurrentY { get; private set; }

        /// <summary>
        /// Moves the cursor to the specified coordinates.
        /// </summary>
        /// <param name="x">The new X-coordinate.</param>
        /// <param name="y">The new Y-coordinate.</param>
        public void MoveTo(int x, int y)
        {
            CurrentX = x;
            CurrentY = y;
        }

        /// <summary>
        /// Sets the position of the cursor to the specified coordinates.
        /// </summary>
        /// <param name="x">The new X-coordinate.</param>
        /// <param name="y">The new Y-coordinate.</param>
        public void SetPosition(int x, int y)
        {
            CurrentX = x;
            CurrentY = y;
        }

        /// <summary>
        /// Resets the position of the cursor to the origin (0, 0).
        /// </summary>
        public void ResetPosition()
        {
            CurrentX = 0;
            CurrentY = 0;
        }

        public void SetWhileLoopFlag(bool status)
        {
            whileLoopFlag = status;
        }

        public void SetIfBlockFlag(bool status)
        {
            ifBlockFlag = status;
        }

        public void SetMethodFlag(bool status)
        {
            ifMethodFlag = status;
        }

        public void IncrementLineCounter()
        {
            LineCounter++;
        }

        public void DecrementLoopCounter()
        {
            LineCounter--;
        }

        public void IncrementLoopCounter()
        {
            LineCounter++;
        }

        public void IncrementWhileLoopCounter()
        {
            WhileLoopCounter++;
        }

        public void DecrementWhileLoopCounter()
        {
            WhileLoopCounter--;
        }
    }
}
