using Moq;
using System.Drawing;
using WinFormsApp2;

namespace ASETests
{
    [TestClass]
    public class RectTests
    {
        private Mock<CursorManager> cursorManagerMock;
        private Rect rect;

        [TestInitialize]
        public void Initialize()
        {
            // Mock the CursorManager
            cursorManagerMock = new Mock<CursorManager>();
            rect = new Rect(cursorManagerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for null parameters.")]
        public void Execute_WithNullParameters_ThrowsArgumentException()
        {
            rect.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for incorrect parameters count.")]
        public void Execute_WithIncorrectParametersCount_ThrowsArgumentException()
        {
            var parameters = new object[] { null, Color.Black, 1 }; // Insufficient parameters
            rect.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for invalid parameter types.")]
        public void Execute_WithInvalidParameterTypes_ThrowsArgumentException()
        {
            var parameters = new object[] { null, "not a color", "not an int", "also not an int", "still not an int", "not a bool", "definitely not a color" };
            rect.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Expected ArgumentOutOfRangeException for negative dimensions.")]
        public void Execute_WithNegativeDimensions_ThrowsArgumentException()
        {
            var parameters = new object[] { null, Color.Black, 1, -50, -50, false, Color.White };
            rect.Execute(parameters);
        }

    }
}