using BLL.ToolServices;
using Common.Dtos;
using DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolPark.Models;

namespace ToolPark.Validators
{
    public class ToolValidator : AbstractValidator<ToolDto>
    {
        public ToolValidator(IToolService _toolService)
        {
            RuleFor(t => t.SerialNumber)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(30);

            RuleFor(t => t.Label)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(50);

            RuleFor(t => t.SerialNumber)
                .Must(s => !_toolService.SerialNumberExist(s))
                .When(t => t.CurrentSerialNumber != t.SerialNumber);
        }
    }
}
