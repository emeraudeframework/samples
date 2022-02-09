using System.Threading.Tasks;
using Emeraude.Application.Admin.EmPages.Schema;
using Emeraude.Application.Admin.EmPages.Schema.IndexView;

namespace EmDoggo.Admin.EmPages.Insights;

public class InsightsEmPageSchema : IEmPageSchema<NullEmPageModel>
{
    public async Task<EmPageSchemaSettings<NullEmPageModel>> SetupAsync()
    {
        var settings = new EmPageSchemaSettings<NullEmPageModel>
        {
            Route = "insights",
            Title = "Insights",
            Description = @"Description of insights page.",
            PriorityIndex = 100,
        };

        settings
            .ConfigureIndexView(indexView =>
            {
                indexView.CustomViewComponent = new CustomIndexViewComponent
                {
                    Name = "InsightsCustomView",
                };

                indexView.PageActions.Clear();
            });

        return await Task.FromResult(settings);
    }
}