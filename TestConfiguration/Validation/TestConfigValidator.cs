using FluentValidation;
using TestConfiguration.Models;

namespace TestConfiguration.Validation
{
    public class TestConfigValidator : AbstractValidator<TestConfig>
    {
        public TestConfigValidator()
        {
            RuleFor(tc => tc.AdmissionReferenceId)
                .NotNull().WithMessage("Admission Reference must be need")
                .NotEmpty().WithMessage("Admission Reference can't be empty");

            RuleFor(tc => tc.BatchId)
               .NotNull().WithMessage("Batch must be need")
               .NotEmpty().WithMessage("Batch can't be empty");

            RuleFor(tc => tc.SesionId)
               .NotNull().WithMessage("Sesion must be need")
               .NotEmpty().WithMessage("Sesion can't be empty");
        }
    }
}
