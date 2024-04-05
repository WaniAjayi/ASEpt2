namespace WinFormsApp2
{
    public class ClearCanvas
    {
        private readonly PictureBox pictureBox1;

        public ClearCanvas(PictureBox pictureBox)
        {
            this.pictureBox1 = pictureBox;
        }


        public void Execute(Color backgroundColor)
        {
            pictureBox1.Image = null; 
            pictureBox1.BackColor = backgroundColor;
        }
    }
}
