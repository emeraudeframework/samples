using System;
using System.Collections.Generic;
using Emeraude.Contracts;

namespace EmDoggo.Domain.Entities;

public class Owner : Entity
{
    public Owner()
    {
        Dogs = new HashSet<Dog>();
    }

    public Guid UserId { get; set; }

    public string Address { get; set; }

    public ICollection<Dog> Dogs { get; set; }
}