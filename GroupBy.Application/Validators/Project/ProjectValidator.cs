using FluentValidation;
using GroupBy.Application.DTO.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Validators.Project
{
    public class ProjectValidator : AbstractValidator<ProjectDTO>
    {
        public ProjectValidator()
        {

        }
    }
}
