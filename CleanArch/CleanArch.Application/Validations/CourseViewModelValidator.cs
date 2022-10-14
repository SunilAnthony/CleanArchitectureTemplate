using CleanArch.Application.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Validations
{
    public class CourseViewModelValidator : AbstractValidator<CourseViewModel>
    {
        public CourseViewModelValidator()
        {
            RuleFor(course => course.Name).NotEmpty().WithMessage("Please add a name");
            RuleFor(course => course.Description).NotEmpty().WithMessage("Please add a description");
            RuleFor(course => course.ImageUrl).NotEmpty().WithMessage("Please add a Image Url");
        }
    }
}
