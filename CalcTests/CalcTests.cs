using CalcCore;

namespace CalcTests

{
    [TestClass]
    public class CalcTests : PageTest
    {
        [TestMethod]
        public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
        {
            await Page.GotoAsync("https://playwright.dev");
        
            // Expect a title "to contain" a substring.
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        
            // create a locator
            var getStarted = Page.Locator("text=Get Started");
        
            // Expect an attribute "to be strictly equal" to the value.
            await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");
        
            // Click the get started link.
            await getStarted.ClickAsync();
        
            // Expects the URL to contain intro.
            await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
        }

        [TestMethod]
        public void Display_ShouldBeEqualZero_Start()
        {
            var calc = new Calc();

            Assert.AreEqual(0, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldBeEqualOne_WhenInputOne()
        {
            var calc = new Calc();

            calc.Input('1');

            Assert.AreEqual(1, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldBeEqualTwelv_WhenInputOneTwo()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');

            Assert.AreEqual(12, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldShowNewOperand_AfterInputOperation()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');

            Assert.AreEqual(34, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldCalculate_AfterEqual()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');
            calc.Input('=');

            Assert.AreEqual(46, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldRepeatLastCalculation_AfterSecondEqual()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');
            calc.Input('=');
            calc.Input('=');

            Assert.AreEqual(80, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldDeleteLastKey_AfterInputArrow()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');
            calc.Input('=');
            calc.Input('←');

            Assert.AreEqual(4, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldZero_WhenNothingToCancel()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input('3');
            calc.Input('4');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');

            Assert.AreEqual(0, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldZero_WhenOnlyMinusExist()
        {
            var calc = new Calc();

            calc.Input('-');
            calc.Input('2');
            calc.Input(',');
            calc.Input('3');
            calc.Input('4');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');
            calc.Input('←');

            Assert.AreEqual(0, calc.Display);
        }



        [TestMethod]
        public void Operand1_ShouldBeEqualCalculation_AfterInputSecondOperation()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');
            calc.Input('-');

            Assert.AreEqual(46, calc.getOperand1());
        }
        
        [TestMethod]
        public void Display_ShouldBeEqualInputWithComma_AfterInputComma()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input('3');
            calc.Input('4');


            Assert.AreEqual(12.34, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldBeEqualInputWithOneComma_AfterInputSomeComma()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input(',');
            calc.Input('3');
            calc.Input(',');
            calc.Input('4');
            calc.Input(',');


            Assert.AreEqual(12.34, calc.Display);
        }

        [TestMethod]
        public void ResetComma_CommaShouldBeReset_AfterInputOperation()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input(',');
            calc.Input('3');
            calc.Input(',');
            calc.Input('4');
            calc.Input(',');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');


            Assert.AreEqual(false, calc.getComma());
        }

        [TestMethod]
        public void Display_ShouldBeEqualInputWithSecondComma_AfterInputSecondOperand()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input(',');
            calc.Input('3');
            calc.Input(',');
            calc.Input('4');
            calc.Input(',');
            calc.Input('+');
            calc.Input('3');
            calc.Input(',');
            calc.Input(',');
            calc.Input('4');


            Assert.AreEqual(3.4, calc.Display);
        }
        [TestMethod]
        public void All_ShouldBeReset_AfterInputC()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input(',');
            calc.Input('3');
            calc.Input('4');
            calc.Input('+');
            calc.Input('3');
            calc.Input('4');
            calc.Input('C');

            Assert.AreEqual(null, calc.getOperand1());
            Assert.AreEqual(null, calc.getOperand2());
            Assert.AreEqual(null, calc.getOperation());
            Assert.AreEqual(0, calc.Display);
        }

        [TestMethod]
        public void Display_ShouldBeZero_WhenDevideByZero()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('2');
            calc.Input('/');
            calc.Input('0');
            
            Assert.AreEqual(0, calc.Display);
        }

        [TestMethod]
        public void PreDisplay_ShouldBeNothing_WhenInputOnlyEqual()
        {
            var calc = new Calc();

            calc.Input('=');
            calc.Input('=');
            

            Assert.AreEqual("", calc.preDisplay);
        }

        [TestMethod]
        public void PreDisplay_ShouldAddUnarMinus_WhenInputUnarMinus()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('8');
            calc.Input('+');
            calc.Input('1');
            calc.Input('±');


            Assert.AreEqual("18+-1", calc.preDisplay);
        }

        [TestMethod]
        public void Display_ShouldAddUnarMinus_WhenInputUnarMinus()
        {
            var calc = new Calc();

            calc.Input('1');
            calc.Input('8');
            calc.Input('+');
            calc.Input('1');
            calc.Input('±');


            Assert.AreEqual(-1, calc.Display);
        }

    }
}
