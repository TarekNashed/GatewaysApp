using API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NetworkDomain.IBusinessData;
using NetworkDomain.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GatewayData.Test
{
    public class DeviceTest
    {
        public Mock<IDeviceBusinessData> mock1 = new Mock<IDeviceBusinessData>();
        [Fact]
        public void Test_GetDeviceById_Return_DeviceObj()
        {
            //Arrange
            DeviceMapper device = new DeviceMapper
            {
                Id = 1,
                CreatedDate =DateTime.Now,
                GatewayId = 1,
                Status = false,
                UID=1,
                Vendor="Ven2"
            };
            mock1.Setup(p => p.GetDeviceById(1)).Returns(device);
            DevicesController controller = new DevicesController(mock1.Object);
            //Act
            ActionResult<DeviceMapper> result = controller.GetDevice(1);
            //Assert
            Assert.Equal(device, result.Value);
        }
        [Fact]
        public void Test_SaveDevice_Return_RedirectToAction_GetGatewayById()
        {
            //Arrange
            DeviceMapper device = new DeviceMapper
            {
                CreatedDate = DateTime.Now,
                GatewayId = 1,
                Status = false,
                UID = 1,
                Vendor = "Ven2"
            };
            mock1.Setup(p => p.AddNewDevice(device)).Returns(device);
            DevicesController controller = new DevicesController(mock1.Object);
            //Act
            var result = controller.Post(device);
            //Assert
            Assert.IsType<CreatedAtActionResult>(result);
        }
        [Fact]
        public void Test_PutDevice_Return_NoContentResult()
        {
            //Arrange
            int Id = 1;
            DeviceMapper device = new DeviceMapper
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                GatewayId = 1,
                Status = false,
                UID = 1,
                Vendor = "Ven2"
            };
            mock1.Setup(p => p.UpdateDevice(Id, device));
            DevicesController controller = new DevicesController(mock1.Object);
            //Act
            var result = controller.Put(Id, device);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public void Test_DeleteDevice_Return_NotContentResult()
        {
            //Arrange
            int Id = 1;
            mock1.Setup(p => p.RemoveDevice(Id));
            DevicesController controller = new DevicesController(mock1.Object);
            //Act
            var result = controller.Delete(Id);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
