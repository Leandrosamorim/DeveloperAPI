using DeveloperAPI.Controllers;
using Domain.DeveloperNS;
using Domain.DeveloperNS.Command;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Query;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit
{
    public class DeveloperControllerTests
    {
        private DeveloperController _developerController;

        public Mock<IDeveloperService> mockIDeveloperService;

        [Fact]
        public async void AddDeveloper_ReturnsOkObjectResult()
        {
            // Arrange
            var developer = new RegisterDeveloperCmd() { Name = "teste", StackId = 1, Contact = new Domain.ContactNS.Contact(), Login = "abc", Password="123" };
            mockIDeveloperService = new Mock<IDeveloperService>();
            mockIDeveloperService.Setup(s => s.Create(developer)).ReturnsAsync(new Developer { Contact = developer.Contact, Name = developer.Name, StackId = developer.StackId});
            _developerController = new DeveloperController(mockIDeveloperService.Object);

            // Act
            var result = await _developerController.Create(developer);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockIDeveloperService.Verify(x => x.Create(developer), Times.Once);
        }

        [Fact]
        public async void GetDeveloper_ReturnsOkObjectResult()
        {
            // Arrange
            var developerQuery = new DeveloperQuery() { StackId = 1 };
            mockIDeveloperService = new Mock<IDeveloperService>();
            mockIDeveloperService.Setup(s => s.Get(developerQuery)).ReturnsAsync(new List<DeveloperDTO>());
            _developerController = new DeveloperController(mockIDeveloperService.Object);

            // Act
            var result = await _developerController.Get(developerQuery);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockIDeveloperService.Verify(x => x.Get(developerQuery), Times.Once);
        }

        [Fact]
        public async void UpdateDeveloper_ReturnsOkObjectResult()
        {
            // Arrange
            var developer = new UpdateDeveloperCmd() { UId = Guid.NewGuid(), StackId = 0 };
            mockIDeveloperService = new Mock<IDeveloperService>();
            mockIDeveloperService.Setup(s => s.Update(developer)).ReturnsAsync(new Developer());
            _developerController = new DeveloperController(mockIDeveloperService.Object);

            // Act
            var result = await _developerController.Update(developer);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            mockIDeveloperService.Verify(x => x.Update(developer), Times.Once);
        }

        [Fact]
        public async void DeleteDeveloper_ReturnsOkObjectResult()
        {
            // Arrange
            var uid = Guid.NewGuid();
            mockIDeveloperService = new Mock<IDeveloperService>();
            mockIDeveloperService.Setup(s => s.Delete(uid));
            _developerController = new DeveloperController(mockIDeveloperService.Object);

            // Act
            var result = await _developerController.Delete(uid);

            // Assert
            Assert.IsType<OkResult>(result);
            mockIDeveloperService.Verify(x => x.Delete(uid), Times.Once);
        }


    }
}
