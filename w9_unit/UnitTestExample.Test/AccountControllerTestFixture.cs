using Moq;
using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;

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

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567")
        ]
         public void TestRegisterHappyPath(string email, string password)
                {
                    // Arrange
                    var accountController = new AccountController();

                    // Act
                    var actualResult = accountController.Register(email, password);

                    // Assert
                    Assert.AreEqual(email, actualResult.Email);
                    Assert.AreEqual(password, actualResult.Password);
                    Assert.AreNotEqual(Guid.Empty, actualResult.ID);
         }

        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234"),
            TestCase("irf.uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "abcd1234"),
            TestCase("irf@uni-corvinus.hu", "ABCD1234"),
            TestCase("irf@uni-corvinus.hu", "abcdABCD"),
            TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            // Assert
        }



        //nem kötelező
        [
           Test,
           TestCase("irf@uni-corvinus.hu", "Abcd1234"),
           TestCase("irf@uni-corvinus.hu", "Abcd1234567")
       ]
        public void TestRegisterHappyPath2(string email, string password)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Returns<Account>(a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
            accountServiceMock.Verify(m => m.CreateAccount(actualResult), Times.Once);
        }


        //nem kötelező
        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234")
        ]
        public void TestRegisterApplicationException(string newEmail, string newPassword)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Throws<ApplicationException>();
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;

            // Act
            try
            {
                var actualResult = accountController.Register(newEmail, newPassword);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ApplicationException>(ex);
            }

            // Assert
        }
    }
}
