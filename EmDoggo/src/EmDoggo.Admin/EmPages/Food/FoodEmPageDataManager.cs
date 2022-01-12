using System;
using System.IO;
using System.Threading.Tasks;
using Emeraude.Application.Admin.EmPages.Data;
using Emeraude.Application.Admin.EmPages.Services;
using Emeraude.Essentials.Helpers;
using Emeraude.Infrastructure.FileStorage.Common;
using Emeraude.Infrastructure.FileStorage.Services;
using MediatR;

namespace EmDoggo.Admin.EmPages.Food;

public class FoodEmPageDataManager : EmPageDataManager<FoodEmPageModel>
{
    private readonly ISystemFilesService systemFilesService;
    private readonly IRootsService rootsService;

    public FoodEmPageDataManager(
        FoodEmPageDataStrategy dataStrategy,
        IMediator mediator,
        IEmPageService emPageService,
        ISystemFilesService systemFilesService,
        IRootsService rootsService)
        : base(dataStrategy, mediator, emPageService)
    {
        this.systemFilesService = systemFilesService;
        this.rootsService = rootsService;
    }

    protected override async Task BeforeCreateAsync(FoodEmPageModel model)
    {
        this.HandleFormFile(model);
        await Task.CompletedTask;
    }

    protected override async Task BeforeEditAsync(string modelId, FoodEmPageModel model)
    {
        this.HandleFormFile(model);
        await Task.CompletedTask;
    }

    private void HandleFormFile(FoodEmPageModel model)
    {
        var targetDirectory = Path.Combine("uploads", "images");
        var fileId = model.ImageUrl;
        if (!string.IsNullOrWhiteSpace(fileId) && File.Exists(this.rootsService.GetPathFromPublicRoot(fileId.Substring(1))))
        {
            return;
        }
        else if (Guid.TryParse(fileId, out var parsedFileId) && this.systemFilesService.MoveTemporaryFileToPublicDirectory(parsedFileId, targetDirectory))
        {
            var fileName = this.systemFilesService.GetTemporaryFile(parsedFileId);
            model.ImageUrl = UrlHelpers.BuildRelativeUrl("uploads", "images", fileName);
        }
        else
        {
            model.ImageUrl = null;
        }
    }
}