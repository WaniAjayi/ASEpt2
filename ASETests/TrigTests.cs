using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp2;

namespace ASETests
{
    [TestClass]
    public class TrigTests
    {
        private Mock<CursorManager> cursorManagerMock;
        private Triangle triangle;

        [TestInitialize]
        public void Initialize()
        {
            cursorManagerMock = new Mock<CursorManager>();
            Canvas canvas = new Canvas(100, 100); 

            triangle = new Triangle(canvas, cursorManagerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for null parameters.")]
        public void Execute_WithNullParameters_ThrowsArgumentException()
        {
            triangle.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for incorrect parameters count.")]
        public void Execute_WithIncorrectParametersCount_ThrowsArgumentException()
        {
            var parameters = new object[] { null, Color.Black, 1, 100 }; // Insufficient parameters
            triangle.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for invalid parameter types.")]
        public void Execute_WithInvalidParameterTypes_ThrowsArgumentException()
        {
            var parameters = new object[] { null, "not a color", "not an int", "not an int", "not a bool" };
            triangle.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Expected ArgumentOutOfRangeException for negative side length.")]
        public void Execute_WithNegativeSideLength_ThrowsArgumentOutOfRangeException()
        {
            var parameters = new object[] { null, Color.Black, 1, -100, true };
            triangle.Execute(parameters);
        }

    }
}
