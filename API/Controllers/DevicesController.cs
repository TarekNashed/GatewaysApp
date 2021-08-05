using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetworkData.Entities;
using NetworkData.Interfaces;
using NetworkDomain.Dto;
using NetworkDomain.IBusinessData;
using NetworkDomain.Mapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceBusinessData _deviceBusinessData;
        public DevicesController(IDeviceBusinessData deviceBusinessData)
        {
            this._deviceBusinessData = deviceBusinessData;
        }
        // GET: api/Devices
        [HttpGet]
        [Route("GetListOfDevices")]
        public IEnumerable<DeviceMapper> GetListOfDevices()
        {
            return _deviceBusinessData.GetAllDevices();
        }
        // GET: api/Devices/5
        [HttpGet()]
        [Route("GetDevice")]
        public ActionResult<DeviceMapper> GetDevice(int id)
        {
            if (id <= 0) NotFound("Not Valid id!");
            var device = _deviceBusinessData.GetDeviceById(id);
            if (device.Id == 0)
            {
                return NotFound("Not Valid Id!");
            }

            return device;
        }
        //[HttpGet()]
        //[Route("GetNameById")]
        //public string GetNameById(int id)
        //{
        //    var device = _deviceBusinessData.GetNameById(id);
        //    return device;
        //}
        // POST: api/Devices
        [HttpPost]
        [Route("PostDevice")]
        public ActionResult Post([FromBody] DeviceMapper device)
        {
            var x = _deviceBusinessData.CheckValidGatewayId(device.GatewayId);
            if (x == false)
            {
                return BadRequest("Not Valid Getway Id!");
            }
            if (_deviceBusinessData.CheckIfExceedNumOnGateway(device.GatewayId) == false)
            {
                return BadRequest("Not valid to add more than 10 gateway on the current devices!");
            }
            _deviceBusinessData.AddNewDevice(device);
            return CreatedAtAction("GetDevice", new { id = device.Id }, device);
        }
        // PUT: api/Devices/5
        [HttpPost]
        [Route("EditDevice")]
        public IActionResult Put(int id, DeviceMapper device)
        {
            if (id != device.Id)
            {
                return BadRequest();
            }
            if (_deviceBusinessData.CheckValidGatewayId(device.GatewayId) == false)
                return BadRequest("Not Valid Getway Id!");

            if (_deviceBusinessData.CheckIfExceedNumOnGateway(device.GatewayId) == false)
                return BadRequest("Not valid to add more than 10 gateway on the current devices!");
            try
            {
                _deviceBusinessData.UpdateDevice(id, device);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_deviceBusinessData.CheckValidDeviceId(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpPost]
        [Route("DeleteDevice")]
        public ActionResult Delete(int id)
        {
            //var gateway = _deviceBusinessData.GetDeviceById(id);
            //if (gateway == null)
            //{
            //    return NotFound();
            //}
            if (id == 0)
            {
                return NotFound();
            }
            _deviceBusinessData.RemoveDevice(id);
            return NoContent();
        }
    }
}
