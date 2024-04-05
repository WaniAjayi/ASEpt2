using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp2;

namespace ASETests
{
    [TestClass]
    public class VariableManagerTests
    {
        [TestMethod]
        public void AssignVariable_ValidInteger_SetsVariable()
        {
            
            string variableName = "a";
            int value = 10;

            
            VariableManager.AssignVariable(variableName, value);

            Assert.AreEqual(value, VariableManager.CheckVariable(variableName));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AssignVariable_NegativeInteger_ThrowsArgumentOutOfRangeException()
        {
            string variableName = "a";
            int value = -1;

            VariableManager.AssignVariable(variableName, value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AssignVariable_EmptyString_ThrowsArgumentException()
        {
            string variableName = "a";
            string value = "";

            VariableManager.AssignVariable(variableName, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TryGetVariableOrLiteralValue_UnknownVariable_ThrowsInvalidOperationException()
        {
            string operand = "d";

            VariableManager.TryGetVariableOrLiteralValue(operand);

        }

        [TestMethod]
        public void TryGetVariableOrLiteralValue_ValidIntegerLiteral_ReturnsValue()
        {
            string operand = "5";

            int result = VariableManager.TryGetVariableOrLiteralValue(operand);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void TryGetVariableOrLiteralValue_ValidVariable_ReturnsValue()
        {
            string variableName = "a";
            int expectedValue = 20;
            VariableManager.AssignVariable(variableName, expectedValue);
            string operand = variableName;

            
            int result = VariableManager.TryGetVariableOrLiteralValue(operand);

            Assert.AreEqual(expectedValue, result);
        }
    }
}
