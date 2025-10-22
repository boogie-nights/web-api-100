using FluentValidation;

namespace SoftwareCenter.Api.Vendors.Models;

public record VendorPointOfContact
{
    public string Name { get; set; } = string.Empty;

    public string EMail { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}

public abstract class VendorPointofContactValidator : AbstractValidator<VendorPointOfContact>
{

}
