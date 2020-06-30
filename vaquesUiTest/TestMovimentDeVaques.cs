using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace VaquesTest
{
    public class TestMovimentDeVaques : IDisposable
    {
        private string _appUrl = "http://localhost:3000";
        private const int MillisecondsTimeout = 800;
        private const int SecondsWait = 20;

        private readonly IWebDriver _driver;
        private DefaultWait<IWebDriver> _fluentWait;

        public TestMovimentDeVaques()
        {
            // Crea el navegador
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--lang=ca --window-size=1920,1080");
            options.AddArguments("--disable-dev-shm-usage");
            options.AddArgument("--whitelisted-ips");
            options.AddArgument("--no-sandbox");
            options.SetLoggingPreference(LogType.Browser, LogLevel.All);
            var headLess = false;

            if (headLess)
            {
                options.AddArguments("headless");
            }

            _driver = new ChromeDriver(".", options);

            // Defineix el temps d'espera predeterminat.
            _fluentWait = new DefaultWait<IWebDriver>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(SecondsWait),
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            _fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Size = new Size(1920, 1080);
        }

        public void Dispose()
        {
            _driver?.Quit();
        }

        #region esperes
        /// <summary>
        /// Espera fins que aparegui algun component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void WaitUntil<T>(Func<IWebDriver, T> action)
        {
            _fluentWait.Until(action);
            System.Threading.Thread.Sleep(MillisecondsTimeout);
        }

        public void WaitUntilReady()
        {
            _fluentWait.Until(x => x.FindElement(By.XPath("//li[@id='vaca-Pepa']")));
        }

        public void WaitUntilVacaPresent(string vaca, string columna)
        {
            try
            {
                var expressio = $"//ul[@id='{columna}']/li[@id='{vaca}']";
                System.Console.WriteLine(expressio);
                WaitUntil(x => x.FindElement(By.XPath(expressio)));
            }
            catch (WebDriverTimeoutException)
            {
                Assert.True(false, $"S'ha acabat el temps d'espera i no ha aparegut cap '{vaca}' a '{columna}'");
            }
        }

        #endregion

        [Theory]
        [InlineData("Toñi")]
        [InlineData("Conxi")]
        [InlineData("Pepa")]
        public void ComprovaQueEsPotPosarUnaVacaAlCamio(string nom)
        {
            _driver.Navigate().GoToUrl(_appUrl);
            WaitUntilReady();

            var vaca = _driver.FindElement(By.XPath($"//ul[@id='vaques-camp']/li[@id='vaca-{nom}']"));
            vaca.Click();

            WaitUntilVacaPresent($"vaca-{nom}", "vaques-camio");

            Assert.True(true, "Correcte!");

        }

        [Theory]
        [InlineData("Toñi")]
        [InlineData("Conxi")]
        [InlineData("Pepa")]
        [InlineData("Flor")]
        public void ComprovaQueEsPotPassarUnaVacaDelCampALaCiutat(string nom)
        {
            _driver.Navigate().GoToUrl(_appUrl);
            WaitUntilReady();

            var vaca = _driver.FindElement(By.XPath($"//ul[@id='vaques-camp']/li[@id='vaca-{nom}']"));
            vaca.Click();

            WaitUntilVacaPresent($"vaca-{nom}", "vaques-camio");

            var boto = _driver.FindElement(By.XPath($"//button[@id='tocity']"));
            boto.Click();

            WaitUntilVacaPresent($"vaca-{nom}", "vaques-ciutat");

            var viatges = _driver.FindElement(By.XPath($"//span[@id='viatges']")).Text;
            Assert.Equal("1", viatges);

            var llet = _driver.FindElement(By.XPath($"//span[@id='litres']")).Text;
            Assert.NotEqual("0", llet);

            var euros = _driver.FindElement(By.XPath($"//span[@id='Money']")).Text;
            Assert.NotEqual("0", euros);

        }
    }
}
