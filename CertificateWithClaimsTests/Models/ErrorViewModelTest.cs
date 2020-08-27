using CertificateWithClaims.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace CertificateWithClaimsTests
{
    [TestClass]
    public class ErrorViewModelTest
    {
        [TestMethod]
        public void ShowRequestId_Should_be_false_when_no_RequestId()
        {
            // Arrange
            var model = new ErrorViewModel();

            // Act
            model.RequestId = null;

            // Assert
            model.ShowRequestId.ShouldBeFalse();
        }

        [TestMethod]
        public void ShowRequestId_Should_be_true_when_RequestId()
        {
            // Arrange
            var model = new ErrorViewModel();

            // Act
            model.RequestId = "TEST";

            // Assert
            model.ShowRequestId.ShouldBeTrue();
        }
    }
}
