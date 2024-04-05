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
    public class CircleTests
    {
        private Mock<CursorManager> cursorManagerMock;
        private Circle circle;
        private Canvas canvas;

        [TestInitialize]
        public void Initialize()
        {
            // Mock the CursorManager
            cursorManagerMock = new Mock<CursorManager>();
            
            canvas = new Canvas(500, 500);
            circle = new Circle(canvas, cursorManagerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for null parameters.")]
        public void Execute_WithNullParameters_ThrowsArgumentException()
        {
            circle.Execute(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for incorrect parameters count.")]
        public void Execute_WithIncorrectParametersCount_ThrowsArgumentException()
        {
            // Circle.Execute expects Graphics, Color, lineWidth, radius, and fillEnabled
            var parameters = new object[] { null, Color.Black, 1, 100 }; 
            circle.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected ArgumentException for invalid parameter types.")]
        public void Execute_WithInvalidParameterTypes_ThrowsArgumentException()
        {
           
            var parameters = new object[] { null, "not a color", "not an int", "not an int", "not a bool" };
            circle.Execute(parameters);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "Expected ArgumentOutOfRangeException for negative radius.")]
        public void Execute_WithNegativeRadius_ThrowsArgumentOutOfRangeException()
        {
            // Negative radius is not logical for a circle
            var parameters = new object[] { null, Color.Black, 1, -100, true };
            circle.Execute(parameters);
        }
    }
}
