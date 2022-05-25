
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumNUnitTestProject
{
    public class SeleniumCalculatorDataDrivenTests
    {
        public IWebDriver driver;
        public IWebElement textBoxFirstNumber;
        public IWebElement textBoxSecondNumber;
        public IWebElement dropDownOperation;
        public IWebElement calcbutton;
        public IWebElement resetbutton;
        public IWebElement result;


        [OneTimeSetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArguments("--headless", "window-size=1920,1200");

            driver = new ChromeDriver(options);
            driver.Url = "https://number-calculator.nakov.repl.co/";
            driver.Manage().Window.Maximize();

            textBoxFirstNumber = driver.FindElement(By.Id("number1"));
            textBoxSecondNumber = driver.FindElement(By.Id("number2"));
            dropDownOperation = driver.FindElement(By.Id("operation"));
            calcbutton = driver.FindElement(By.Id("calcButton"));
            resetbutton = driver.FindElement(By.Id("resetButton"));
            result = driver.FindElement(By.Id("result"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [TestCase("5", "+", "10", "Result: 15")]
        [TestCase("5", "*", "10", "Result: 50")]
        [TestCase("10", "-", "5", "Result: 5")]
        [TestCase("10", "/", "5", "Result: 2")]
        [TestCase("", "+", "10", "Result: invalid input")]
        [TestCase("5", "", "10", "Result: invalid operation")]
        [TestCase("5", "+", "", "Result: invalid input")]
        [TestCase("", "", "", "Result: invalid input")]
        public void Test_CalculatorWebApp(string firstNumber, string operation, string secondNumber, string expectedResult)
        {
            // Arrange
            resetbutton.Click();

            if (firstNumber != "")
            {
                textBoxFirstNumber.SendKeys(firstNumber);
            }

            if (secondNumber != "")
            {
                textBoxSecondNumber.SendKeys(secondNumber);
            }

            if (operation != "")
            {
                dropDownOperation.SendKeys(operation);
            }

            // Act

            calcbutton.Click();

            //Assert

            Assert.AreEqual(expectedResult, result.Text);
        }
    }
}
