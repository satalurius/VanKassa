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
    public class OutletsControllerTests
    {
        private readonly IOutletService outletService;

        private readonly OutletsController outletsController;

        public OutletsControllerTests()
        {
            outletService = A.Fake<IOutletService>();

            outletsController = new OutletsController(outletService);
        }

        [Fact]
        public void Controller_GetOutletsAsync_ReturnOkObject()
        {
            // Arrange 
            List<OutletDto> outlets = A.Fake<List<OutletDto>>();
            A.CallTo(() => outletService.GetOutletsAsync()).Returns(outlets);

            // Act
            var result = outletsController.GetOutletsAsync().Result;

            // Asset
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
    
    }
}
