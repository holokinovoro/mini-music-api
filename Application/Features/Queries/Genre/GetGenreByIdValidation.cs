using FluentValidation;

namespace Application.Features.Queries.Genre;

public class GetGenreByIdValidation : AbstractValidator<GetGenreById>
{
    public GetGenreByIdValidation()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}
