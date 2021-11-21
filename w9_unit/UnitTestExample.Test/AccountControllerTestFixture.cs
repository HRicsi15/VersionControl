using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    class AccountControllerTestFixture
    {

        [Test, TestCase("abcd1234", false),
          TestCase("irf@uni-corvinus", false),
          TestCase("irf.uni-corvinus.hu", false),
          TestCase("irf@uni-corvinus.hu", true)]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // Arrange
            var accountController = new AccountController();
            // Act
            var actualResult = accountController.ValidateEmail(email);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);
            
        }

        [Test, TestCase("ABCD1234", false),//nincs kisbetű
          TestCase("Ab1234", false), //túl rövid jelszó
          TestCase("Abcd1234", true), //megfelelő jelszó
          TestCase("abcd1234", false), //nincs nagybetű
          TestCase("abcdABCD", false)]//nincs szám
        public void TestValidatePassword(string password, bool expectedResult)
        {
            /*
            // Arrange
            var accountController = new AccountController();
            // Act
            var actualResult = accountController.ValidatePassword(password);
            // Assert
            Assert.AreEqual(expectedResult, actualResult);*/

            Regex regex = new Regex("(^[a-zA-Z0-9]{8,}$)");
            if (regex.IsMatch(password))
            {
                Regex regex2 = new Regex("(^[a-z]+$)");
                if (regex2.IsMatch(password))
                {
                    Regex regex3 = new Regex("(^[A-Z]+$)");
                    if (regex3.IsMatch(password))
                    {
                        Regex regex4 = new Regex("(^[0-9]+$)");
                        if (regex4.IsMatch(password))
                        {
                            expectedResult = true;
                        }
                        else { expectedResult = false; }
                    }
                    else { expectedResult = false; }
                }
                else { expectedResult = false; }
                   
            }
            else { expectedResult = false; }
        }
    }
}
