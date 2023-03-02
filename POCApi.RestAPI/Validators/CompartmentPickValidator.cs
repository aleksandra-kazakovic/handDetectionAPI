using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using POCApi.RestAPI.Requests;

namespace POCApi.RestAPI.Validators
{
    public class CompartmentPickValidator : AbstractValidator<ExamineCompartmentPickingRequest>
    {
        public CompartmentPickValidator()
        {

            RuleFor(compartmentPick => compartmentPick.portId).NotNull().WithMessage("Port id must have a value");
            RuleFor(compartmentPick => compartmentPick.compartmentId).NotNull().WithMessage("Compartment id must have a value");
        }
    }
}
