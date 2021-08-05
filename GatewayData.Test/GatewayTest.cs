using Moq;
using System;
using Xunit;
using NetworkDomain.IBusinessData;
using API.Controllers;
using System.Collections.Generic;
using NetworkDomain.Mapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Linq;

namespace GatewayData.Test
{
    public class GatewayTest
    {
        public Mock<IGatewayBusinessData> mock1 = new Mock<IGatewayBusinessData>();
        public Mock<IGatewayDevicesBusinessData> mock2 = new Mock<IGatewayDevicesBusinessData>();
        [Fact]
        public void Test_GetGatewayById_Return_GatewayObj()
        {
            //Arrange
            GatewayMapper gateway = new GatewayMapper
            {
                Id = 1,
                IP4Address = "224.100.210.100",
                SerialNumer = "Seri 2",
                Name= "Gateway1"
            };
            mock1.Setup(p => p.GetGetwayById(1)).Returns(gateway);
            GatewaysController controller = new GatewaysController(mock1.Object, mock2.Object);
            //Act
            ActionResult<GatewayMapper> result = controller.GetGateway(1);
            //Assert
            Assert.Equal(gateway, result.Value);
        }
        [Fact]
        public void Test_SaveGateway_Return_RedirectToAction_GetGatewayById()
        {
            //Arrange
            GatewayMapper gateway = new GatewayMapper
            {
                IP4Address = "225.255.200.100",
                SerialNumer = "Seri 4",
                Name = "Gateway4"
            };
            mock1.Setup(p => p.AddNewGateway(gateway)).Returns(new GatewayMapper());
            GatewaysController controller = new GatewaysController(mock1.Object, mock2.Object);
            //Act
            var result = controller.Post(gateway);
            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        [Fact]
        public void Test_PutGateway_Return_NoContentResult()
        {
            //Arrange
            GatewayMapper gateway = new GatewayMapper
            {
                Id=1,
                IP4Address = "225.255.200.100",
                SerialNumer = "Seri 4",
                Name = "Gateway4"
            };
            mock1.Setup(p => p.UpdateGateway(1, gateway));
            GatewaysController controller = new GatewaysController(mock1.Object, mock2.Object);
            //Act
            var result = controller.Put(1, gateway);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void Test_DeleteGateway_Return_NotContentResult()
        {
            //Arrange
            int Id = 1;
            mock1.Setup(p => p.RemoveGateway(Id));
            GatewaysController controller = new GatewaysController(mock1.Object, mock2.Object);
            //Act
            var result = controller.Delete(Id);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
