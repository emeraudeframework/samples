using System;
using Emeraude.Application.Admin.EmPages.Schema;
using Emeraude.Application.Mapping;

namespace EmDoggo.Admin.EmPages.Owner;

public class OwnerEmPageModel : IEmPageModel, IMapFrom<Domain.Entities.Owner>
{
    public string Id { get; set; }
    
    public string Address { get; set; }
    
    public Guid UserId { get; set; }
}