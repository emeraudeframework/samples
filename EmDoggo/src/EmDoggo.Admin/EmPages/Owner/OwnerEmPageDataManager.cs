using Emeraude.Application.Admin.EmPages.Data;
using Emeraude.Application.Admin.EmPages.Services;
using MediatR;

namespace EmDoggo.Admin.EmPages.Owner;

public class OwnerEmPageDataManager : EmPageDataManager<OwnerEmPageModel>
{
    public OwnerEmPageDataManager(
        OwnerEmPageDataStrategy dataStrategy,
        IMediator mediator,
        IEmPageService emPageService)
        : base(dataStrategy, mediator, emPageService)
    {
    }
}