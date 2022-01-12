using System.Threading.Tasks;
using EmDoggo.Admin.EmPages.User;
using Emeraude.Application.Admin.EmPages.Components.Mutators;
using Emeraude.Application.Admin.EmPages.Components.Renderers;
using Emeraude.Application.Admin.EmPages.Schema;
using Emeraude.Application.Admin.EmPages.Utilities;
using Emeraude.Defaults.Extensions;

namespace EmDoggo.Admin.EmPages.Owner;

public class OwnerEmPageSchema : IEmPageSchema<OwnerEmPageModel>
{
    public async Task<EmPageSchemaSettings<OwnerEmPageModel>> SetupAsync()
    {
        var settings = new EmPageSchemaSettings<OwnerEmPageModel>
        {
            Route = "owner",
            Title = "Owner",
            Description = @"Owner description.",
            UseAsFeature = true,
        };

        settings
            .ConfigureIndexView(indexView =>
            {
                indexView.PageActions.Clear();
                indexView.PageActions.Add(new EmPageAction
                {
                    Name = "Create",
                    RelativeUrlFormat = $"/create?userId={EmPagesPlaceholders.GetModelPlaceholder<UserEmPageModel>("users", x => x.Id)}"
                });
                
                indexView
                    .Use(x => x.Id, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    });
            })
            .ConfigureDetailsView(detailsView =>
            {
                detailsView
                    .Use(x => x.Id, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    });
            })
            .ConfigureFormView(formView =>
            {
                formView
                    .Use(x => x.Address, item =>
                    {
                        item.SetComponent<EmPageTextMutator>();
                    })
                    .Use(x => x.UserId, item =>
                    {
                        item.Hidden = true;
                        item.SetComponent<EmPageHiddenQueryMutator>(component =>
                        {
                            component.ReferenceKey = "userId";
                        });
                    });
            })
            .ApplyDefaultEmPageActions();

        return settings;
    }
}