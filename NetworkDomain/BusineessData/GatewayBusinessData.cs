using NetworkData.Entities;
using NetworkData.Interfaces;
using NetworkData.UnitOfWork;
using NetworkDomain.Dto;
using NetworkDomain.IBusinessData;
using NetworkDomain.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetworkDomain.BusineessData
{
    public class GatewayBusinessData : IGatewayBusinessData
    {
        private readonly IUnitOfWorkData<Gateway> _unitOfWorkGatewayData;
        public GatewayBusinessData(IUnitOfWorkData<Gateway> unitOfWorkGatewayData)
        {
            this._unitOfWorkGatewayData = unitOfWorkGatewayData;
        }
        public IEnumerable<GatewayMapper> GetAllGateways()
        {
            List<GatewayMapper> gatewayDtos = new List<GatewayMapper>();
            var gateways = _unitOfWorkGatewayData.repository.GetAll();
            foreach (var gateway in gateways)
            {
                GatewayMapper gatewayDto = new GatewayMapper();
                gatewayDto.Id = gateway.Id;
                gatewayDto.Name = gateway.Name;
                gatewayDto.SerialNumer = gateway.SerialNumer;
                gatewayDto.IP4Address = gateway.IP4Address;
                gatewayDtos.Add(gatewayDto);
            }
            return gatewayDtos;
        }
        public IEnumerable<GatewayMapper> GetAll()
        {
            List<GatewayMapper> gatewayResponse = new List<GatewayMapper>();
            var gateways = _unitOfWorkGatewayData.repository.GetAll();
            if (gateways != null)
            {
                foreach (var gateway in gateways)
                {
                    GatewayMapper gatewayMapper = new GatewayMapper();
                    gatewayMapper.Id = gateway.Id;
                    gatewayMapper.SerialNumer = gateway.SerialNumer;
                    gatewayMapper.Name = gateway.Name;
                    gatewayMapper.IP4Address = gateway.IP4Address;
                    gatewayResponse.Add(gatewayMapper);
                }
            }
            return gatewayResponse;
        }
        public GatewayMapper GetGetwayById(int id)
        {
            GatewayMapper gatewayResponse = new GatewayMapper();
            var gateway =  _unitOfWorkGatewayData.repository.GetById(id);
            if (gateway != null)
            {
                gatewayResponse.Id = gateway.Id;
                gatewayResponse.SerialNumer = gateway.SerialNumer;
                gatewayResponse.Name = gateway.Name;
                gatewayResponse.IP4Address = gateway.IP4Address;
            }
            return gatewayResponse;
        }
        public bool CheckValidGatewayId(int id)
        {
            var gateway = _unitOfWorkGatewayData.repository.GetById(id);
            if (gateway == null) return false;
            return true;
        }
        public GatewayMapper AddNewGateway(GatewayMapper gatewayMapper)
        {
            Gateway gateway = new Gateway();
            gateway.Name = gatewayMapper.Name;
            gateway.SerialNumer = gatewayMapper.SerialNumer;
            gateway.IP4Address = gatewayMapper.IP4Address;
            _unitOfWorkGatewayData.repository.Add(gateway);
            _unitOfWorkGatewayData.Save();
            return gatewayMapper;
        }
        public void UpdateGateway(int id, GatewayMapper gatewayMapper)
        {
            Gateway gateway = new Gateway();
            gateway.Id = gatewayMapper.Id;
            gateway.Name = gatewayMapper.Name;
            gateway.SerialNumer = gatewayMapper.SerialNumer;
            gateway.IP4Address = gatewayMapper.IP4Address;
            _unitOfWorkGatewayData.repository.Update(id, gateway);
            _unitOfWorkGatewayData.Save();
        }
        public void RemoveGateway(int id)
        {
            var gateway = _unitOfWorkGatewayData.repository.GetById(id);
            _unitOfWorkGatewayData.repository.Delete(gateway);
            _unitOfWorkGatewayData.Save();
        }
        public string GetGatewayNameByID(int id)
        {
            throw new NotImplementedException();
        }
        public bool CheckValidIP4Address(string IP4Address)
        {
            const string regexPattern = @"^([\d]{1,3}\.){3}[\d]{1,3}$";
            var regex = new Regex(regexPattern);
            if (string.IsNullOrEmpty(IP4Address))
            {
                return false;//new ValidationResult("IP address is null");
            }
            if (!regex.IsMatch(IP4Address) || IP4Address.Split('.').SingleOrDefault(s => int.Parse(s) > 255) != null)
                return false;//new ValidationResult("Invalid IP Address");
            return true;
        }
        public async Task<Microsoft.AspNetCore.Mvc.ActionResult<IEnumerable<GatewayMapper>>> GetAllGatewaysAsync()
        {
            List<GatewayMapper> gatewayDtos = new List<GatewayMapper>();
            var gateways = await _unitOfWorkGatewayData.repository.GetAllAsync();
            foreach (var gateway in gateways)
            {
                GatewayMapper gatewayDto = new GatewayMapper();
                gatewayDto.Id = gateway.Id;
                gatewayDto.Name = gateway.Name;
                gatewayDto.SerialNumer = gateway.SerialNumer;
                gatewayDto.IP4Address = gateway.IP4Address;
                gatewayDtos.Add(gatewayDto);
            }
            return gatewayDtos;
        }

    }
}
