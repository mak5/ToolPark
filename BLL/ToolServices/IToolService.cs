using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ToolServices
{
    public interface IToolService
    {
        void AddTool(ToolDto toolDto);
        IEnumerable<ToolDto> GetTools();
        ToolDto GetToolById(string id);
        void UpdateTool(string id, ToolDto toolDto);

        bool SerialNumberExist(string serialNumber);
        void DeleteTool(string id);
    }
}
