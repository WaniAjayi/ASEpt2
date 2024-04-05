using System.Drawing;

namespace WinFormsApp2
{

    
    public class PenReset
    {
        private readonly CursorManager cursorManager;

        // You might need additional parameters or properties based on your requirements

        public PenReset(CursorManager cursorManager)
        {
            this.cursorManager = cursorManager;
        }

        public void Execute(Graphics g, Color color, int lineWidth, int x, int y)
        {
            cursorManager.MoveTo(x, y);
            // Perform actions to reset the pen, such as resetting cursor coordinates
        //    cursorManager.ResetToDefault(); // Add a ResetToDefault method in CursorManager if needed
        }
    }
}
