using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanKassa.Backend.Api.Controllers;
using VanKassa.Backend.Core.Services.Interface;
using VanKassa.Domain.Dtos;

namespace VanKassa.Backend.Api.Tests.Controllers
{
    public class RolesControllerTests
    {
        private readonly IEmployeesRoleService employeesRoleService;

        private readonly RolesController rolesController;

        public RolesControllerTests()
        {
            employeesRoleService = A.Fake<IEmployeesRoleService>();

            rolesController = new RolesController(employeesRoleService);
        }

        [Fact]
        public void RolesController_GetRolesAsync_ReturnOkObject()
        {
            // Arrange
            List<EmployeesRoleDto> roles = A.Fake<List<EmployeesRoleDto>>();
            A.CallTo(() => employeesRoleService.GetAllRolesAsync()).Returns(roles);

            // Act
            var result = rolesController.GetRolesAsync().Result;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

    }
}