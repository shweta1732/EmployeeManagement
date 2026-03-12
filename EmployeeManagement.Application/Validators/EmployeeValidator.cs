using EmployeeManagement.Domain.Entities;
using FluentValidation;

namespace EmployeeManagement.Application.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(e => e.LastName).NotEmpty().MaximumLength(100);
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Salary).GreaterThanOrEqualTo(0);
        }
    }
}