using Microsoft.AspNetCore.Mvc;
using NetworkDomain.Dto;
using NetworkDomain.Mapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDomain.IBusinessData
{
    public interface IGatewayBusinessData
    {
        IEnumerable<GatewayMapper> GetAllGateways();
        Task<ActionResult<IEnumerable<GatewayMapper>>> GetAllGatewaysAsync();
        GatewayMapper GetGetwayById(int id);
        GatewayMapper AddNewGateway(GatewayMapper gatewayDto);
        void UpdateGateway(int id, GatewayMapper gatewayDto);
        void RemoveGateway(int id);
        bool CheckValidGatewayId(int Id);
        bool CheckValidIP4Address(string Ip4Address);
        string GetGatewayNameByID(int id);
    }
}
