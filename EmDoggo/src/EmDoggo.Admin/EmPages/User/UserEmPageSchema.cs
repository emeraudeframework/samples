using System.Threading.Tasks;
using EmDoggo.Admin.EmPages.Owner;
using Emeraude.Application.Admin.EmPages.Components.Renderers;
using Emeraude.Application.Admin.EmPages.Schema;
using Emeraude.Application.Admin.EmPages.Shared;
using Emeraude.Application.Admin.EmPages.Utilities;
using Emeraude.Defaults.Extensions;
using Emeraude.Defaults.ValuePipes;

namespace EmDoggo.Admin.EmPages.User;

public class UserEmPageSchema : IEmPageSchema<UserEmPageModel>
{
    public async Task<EmPageSchemaSettings<UserEmPageModel>> SetupAsync()
    {
        var settings = new EmPageSchemaSettings<UserEmPageModel>
        {
            Route = "users",
            Title = "User",
            Description = @"List of all users.",
        };

        settings
            .ConfigureIndexView(indexView =>
            {
                indexView.PageActions.Clear();

                indexView
                    .Use(x => x.Email, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    })
                    .Use(x => x.Name, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    })
                    .Use(x => x.RegistrationDate, item =>
                    {
                        item.Title = "Registration";
                        item.SetComponent<EmPageTextRenderer>();
                        item.AddValuePipe<DateTimeFormatValuePipe>();
                    })
                    .Use(x => x.TwoFactorEnabled, item =>
                    {
                        item.Title = "2FA";
                        item.SetComponent<EmPageFlagRenderer>();
                    })
                    .Use(x => x.IsLockedOut, item =>
                    {
                        item.Title = "Locked Out";
                        item.SetComponent<EmPageFlagRenderer>();
                    })
                    .Use(x => x.EmailConfirmed, item =>
                    {
                        item.SetComponent<EmPageFlagRenderer>();
                    });
            })
            .ConfigureDetailsView(detailsView =>
            {
                detailsView
                    .Use(x => x.Id, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    })
                    .Use(x => x.Email, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    })
                    .Use(x => x.Name, item =>
                    {
                        item.SetComponent<EmPageTextRenderer>();
                    })
                    .Use(x => x.RegistrationDate, item =>
                    {
                        item.Title = "Registration";
                        item.SetComponent<EmPageTextRenderer>();
                        item.AddValuePipe<DateTimeFormatValuePipe>();
                    })
                    .Use(x => x.TwoFactorEnabled, item =>
                    {
                        item.Title = "2FA";
                        item.SetComponent<EmPageFlagRenderer>();
                    })
                    .Use(x => x.IsLockedOut, item =>
                    {
                        item.Title = "Locked Out";
                        item.SetComponent<EmPageFlagRenderer>();
                    })
                    .Use(x => x.EmailConfirmed, item =>
                    {
                        item.SetComponent<EmPageFlagRenderer>();
                    });

                detailsView.IncludeFeature(feature =>
                {
                    feature.Route = "owner";
                    feature.Title = "Owner";
                    feature.MapEmPageModel<OwnerEmPageModel>(
                        x => x.UserId,
                        x => x.Id,
                        EmPageFeatureReferenceDirection.FromSourceToParent);
                });
                
                detailsView.PageActions.Add(new EmPageAction
                {
                    Name = "Owner",
                    Order = 2,
                    RelativeUrlFormat = $"/{EmPagesPlaceholders.GetModelPlaceholder<UserEmPageModel>("users", x => x.Id)}/owner",
                });
            })
            .ApplyDefaultEmPageActions();

        settings.ModelActions.RemoveAt(1);
        settings.ModelActions.Add(new EmPageAction
        {
            Order = 1,
            Name = "Owner",
            RelativeUrlFormat = $"/{EmPagesPlaceholders.GetModelPlaceholder<UserEmPageModel>("users", x => x.Id)}/owner",
        });

        return await Task.FromResult(settings);
    }
}