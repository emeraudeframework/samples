using System;
using Emeraude.Application.Admin.EmPages.Data;

namespace EmDoggo.Admin.EmPages.Owner;

public class OwnerEmPageDataStrategy : EmPageEntityDataStrategy<Domain.Entities.Owner, OwnerEmPageModel>
{
    public OwnerEmPageDataStrategy()
    {
        this.AddCustomFilterExpression(x => x.UserId, (value) => x => x.UserId == new Guid(value.ToString()));
    }
}