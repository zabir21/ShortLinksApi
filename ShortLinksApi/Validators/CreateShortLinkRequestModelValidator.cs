using FluentValidation;
using ShortLinksApi.Contracts.Request;

namespace ShortLinksApi.Validators
{
    public class CreateShortLinkRequestModelValidator : AbstractValidator<CreateShortLinkRequestModel>
    {
        public CreateShortLinkRequestModelValidator() 
        {
            RuleFor(x => x.FullUrl)
                .NotEmpty();
        }
    }
}
