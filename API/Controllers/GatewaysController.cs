using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
    public class GatewaysController : ControllerBase
    {
        private readonly IGatewayBusinessData _gatewayBusinessData;
        private readonly IGatewayDevicesBusinessData _gatewayDevicesBusinessData;
        private readonly IDeviceBusinessData _deviceBusinessData;
        public GatewaysController(IGatewayBusinessData gatewayBusinessData,
            IGatewayDevicesBusinessData gatewayDevicesBusinessData, IDeviceBusinessData deviceBusinessData)
        {
            this._gatewayBusinessData = gatewayBusinessData;
            this._gatewayDevicesBusinessData = gatewayDevicesBusinessData;
            this._deviceBusinessData = deviceBusinessData;
        }
        
        [HttpGet]
        [Route("GetListOfGateways")]
        public IEnumerable<GatewayMapper> GetListOfGateways()
        {
            return  _gatewayBusinessData.GetAllGateways();
        }
        [HttpGet]
        [Route("GetDevicesByGatewayId")]
        public IEnumerable<DeviceMapper> GetDevicesByGatewayId(int id)
        {
            var result = _deviceBusinessData.GetDevicesByGatewayId(id);
            return result;
        }
        [HttpGet]
        [Route("GetListOfGatewaysDevices")]
        public IEnumerable<GatewayDeviceDto> GetListOfGatewaysDevices()
        {
            var gateways = _gatewayDevicesBusinessData.GetAllData();
            return gateways;
        }
        [HttpGet]
        [Route("GetGatewayDevices")]
        public ActionResult<GatewayDeviceDto> GetGatewayDevices(int id)
        {
            if (id <= 0) return NotFound("Not Valid id!");
            return _gatewayDevicesBusinessData.GetAllDevicesDataByGatewayId(id);
        }
        // GET: api/Gateways/5
        [HttpGet]
        [Route("GetGateway")]
        public ActionResult<GatewayMapper> GetGateway(int id)
        {
            if (id <= 0) return NotFound("Not Valid id!");
            var gateway= _gatewayBusinessData.GetGetwayById(id);
            if (gateway.Id == 0)
            {
                return NotFound("Not Valid Id!");
            }

            return gateway;
        }

        // POST: api/Gateways
        [HttpPost]
        [Route("PostGateway")]
        public IActionResult Post([FromBody] GatewayMapper gateway)
        {
            if (CheckValidIP4Address(gateway.IP4Address) == false)
                return BadRequest("Not Valid IP4Address!");
            var x = _gatewayBusinessData.AddNewGateway(gateway);
            return CreatedAtAction("GetGateway", new { id = gateway.Id }, gateway);
        }

        // PUT: api/Gateways/5
        [HttpPost]
        [Route("EditGateway")]
        public ActionResult Put(int id, GatewayMapper gateway)
        {
            if (id != gateway.Id)
            {
                return BadRequest();
            }
            if (CheckValidIP4Address(gateway.IP4Address) == false)
                return BadRequest("Not Valid IP4Address!");

            try
            {
                _gatewayBusinessData.UpdateGateway(id, gateway);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_gatewayBusinessData.CheckValidGatewayId(id))
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

        private bool CheckIfGatewayHasDevice(int id)
        {
            return _deviceBusinessData.CheckIfGetwayHasDevice(id);
        }
        // DELETE: api/ApiWithActions/5
        [HttpPost()]
        [Route("DeleteGateway")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            if (CheckIfGatewayHasDevice(id) == true)
                return BadRequest("Not valid to delete gateway and has devices on it!");
            _gatewayBusinessData.RemoveGateway(id);
            return NoContent();
        }

        [HttpGet()]
        [Route("CheckValidIP4Address")]
        public bool CheckValidIP4Address(string IP4Address)
        {
            const string regexPattern = @"^([\d]{1,3}\.){3}[\d]{1,3}$";
            var regex = new Regex(regexPattern);
            if (string.IsNullOrEmpty(IP4Address))
            {
                return false;
            }
            if (!regex.IsMatch(IP4Address) || IP4Address.Split('.').SingleOrDefault(s => int.Parse(s) > 255) != null)
                return false;
            return true;
        }
    }
}
