using AutoMapper;
using Common.Dtos;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ToolServices
{
    public class ToolService : IToolService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Tool> _toolRepository;

        public ToolService(IMapper mapper, IRepository<Tool> toolRepository)
        {
            _mapper = mapper;
            _toolRepository = toolRepository;
        }

        public void AddTool(ToolDto toolDto)
        {
            if (toolDto == null)
                throw new ArgumentNullException(nameof(toolDto));

            var tool = _mapper.Map<Tool>(toolDto);
            _toolRepository.Add(tool);
            _toolRepository.Commit();
        }

        public void DeleteTool(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            _toolRepository.Remove(id);
            _toolRepository.Commit();
        }

        public ToolDto GetToolById(string id)
        {
            return _mapper.Map<ToolDto>(_toolRepository.GetById(id));
        }

        public IEnumerable<ToolDto> GetTools()
        {
            return _mapper.Map<IEnumerable<ToolDto>>(_toolRepository.Get());
        }

        public bool SerialNumberExist(string serialNumber)
        {
            return _toolRepository.GetById(serialNumber) != null;
        }

        public void UpdateTool(string id, ToolDto toolDto)
        {
            if (toolDto == null)
                throw new ArgumentNullException(nameof(toolDto));

            var tool = _toolRepository.GetById(id);

            //to update the primary key entity should be removed to insert a new one
            if (id != toolDto.SerialNumber)
            {
                _toolRepository.Remove(tool);
                AddTool(toolDto);
            }
            else
            {
                _mapper.Map(toolDto, tool);
                _toolRepository.Commit();
            }
            
        }
    }
}
