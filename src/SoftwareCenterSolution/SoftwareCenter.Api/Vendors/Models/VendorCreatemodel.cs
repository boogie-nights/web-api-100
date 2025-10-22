﻿using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace SoftwareCenter.Api.Vendors.Models;

// This represents what we are expecting from the client on the POST /vendors

public record VendorCreateModel
{
    [Required, MaxLength(30)]
    public string Name { get; set; } = string.Empty;

    public VendorPointOfContact PointOfContact { get; set; } = new();
}

public class VendorCreateModelValidator : AbstractValidator<VendorCreateModel>
{
    public VendorCreateModelValidator() {

        RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleFor(x => x.PointOfContact).NotNull().SetValidator(validator: new VendorPointofContactValidator());
    }
}
