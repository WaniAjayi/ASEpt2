using WinFormsApp2;

namespace ASETests
{

    [TestClass]
        public class LoopConditionManagerTests
        {
            [TestMethod]
            public void EvaluateCondition_ValidEquality_ReturnsTrue()
            {
                
                string condition = "10 == 10";

                
                bool result = LoopConditionManager.EvaluateCondition(condition);

                Assert.IsTrue(result);
            }

            [TestMethod]
            public void EvaluateCondition_ValidInequality_ReturnsFalse()
            {
                
                string condition = "10 != 10";

               
                bool result = LoopConditionManager.EvaluateCondition(condition);

                
                Assert.IsFalse(result);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void EvaluateCondition_InvalidFormat_ThrowsArgumentException()
            {
                
                string condition = "This is not a valid condition";

                
                LoopConditionManager.EvaluateCondition(condition);

            }

            [TestMethod]
            public void EvaluateCondition_LessThanComparison_ReturnsTrue()
            {
               
                string condition = "5 < 10";

               
                bool result = LoopConditionManager.EvaluateCondition(condition);

               
                Assert.IsTrue(result);
            }

            [TestMethod]
            public void EvaluateCondition_GreaterThanComparison_ReturnsFalse()
            {
               
                string condition = "10 > 20";

               
                bool result = LoopConditionManager.EvaluateCondition(condition);

                
                Assert.IsFalse(result);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void EvaluateCondition_InvalidOperator_ThrowsArgumentException()
            {
                
                string condition = "10 ** 10"; 

               
                LoopConditionManager.EvaluateCondition(condition);

            }

        }

}
