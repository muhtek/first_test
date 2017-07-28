using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Events;
using System.Threading;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace UnitTestProject2
{

    [TestClass]
    public class Selenium_Example_1
    {
        [TestMethod]
        public static void Main(String[] args)
        {
            //            System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", 
            //@"C:\Users\HTEKTAS\Documents\Visual Studio 2012\Projects\UnitTestProject2\chromedriver.exe");

            //    ChromeOptions co = new ChromeOptions();
            //    co.AddArgument("--disable-images");

            IWebDriver driver = new ChromeDriver(@"C:\Users\HTEKTAS\Documents\Visual Studio 2012\Projects\UnitTestProject2");

            driver.Navigate().GoToUrl("http://www.n11.com/");
            driver.FindElement(By.CssSelector("#header > div > div > div.hMedMenu > div.customMenu > div.myAccountHolder > div > div > a.btnSignIn")).Click();

            ReadOnlyCollection<string> handles = driver.WindowHandles;
            List<string> pencereler = new List<string>();
            string anaPencere = driver.CurrentWindowHandle;
            foreach (string pencereId in handles)
            {
                pencereler.Add(pencereId);
            }

            driver.FindElement(By.CssSelector("#loginForm > div.button.inicon.facebook.medium.facebookBtn.btnLogin")).Click();
            Thread.Sleep(1000);

            ReadOnlyCollection<string> handles2 = driver.WindowHandles;
            foreach (string pencereId in handles2)
            {
                if (!pencereler.Contains(pencereId) && !anaPencere.Equals(pencereId))
                {
                    driver.SwitchTo().Window(pencereId);
                }
            }



            driver.FindElement(By.Id("email")).SendKeys("*************");
            driver.FindElement(By.Id("pass")).SendKeys("********");
            IWebElement girisBtn = driver.FindElement(By.Name("login"));

            ReadOnlyCollection<string> handles3 = driver.WindowHandles;

            girisBtn.Click();

            ReadOnlyCollection<string> handles4 = driver.WindowHandles;

            driver.SwitchTo().Window(anaPencere);

            WebDriverWait wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchData")));

            driver.FindElement(By.Id("searchData")).SendKeys("iphone7");
            driver.FindElement(By.XPath("//a[@class='searchBtn']")).Click();

            wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("view")));

            IReadOnlyCollection<IWebElement> urunler = driver.FindElement(By.Id("view")).FindElement(By.TagName("ul")).FindElements(By.TagName("li"));
            IWebElement secilenUrun = null;
            foreach (IWebElement urun in urunler)
            {
                secilenUrun = urun;
                break;
            }
            secilenUrun.FindElement(By.TagName("a"));
            string secilenUrunBaslik = secilenUrun.FindElement(By.TagName("h3")).Text;

            secilenUrun.FindElement(By.TagName("a")).Click();

            wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("instantPay")));

            IWebElement urunBaslik = driver.FindElement(By.XPath("//h1[@class='proName']"));
            string urunBaslikString = urunBaslik.Text;
            Console.WriteLine();
            Assert.AreEqual(urunBaslikString, secilenUrunBaslik);


            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            ss.SaveAsFile(@"C:\Users\HTEKTAS\Desktop\screen\x.jpeg", ImageFormat.Jpeg);


            wait = new WebDriverWait(driver, System.TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("instantPay")));

            IWebElement logout = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='header']/div/div/div[2]/div[2]/div[1]/div[2]/div/a[8]")));
            logout.Click();
            



        }
    }
}