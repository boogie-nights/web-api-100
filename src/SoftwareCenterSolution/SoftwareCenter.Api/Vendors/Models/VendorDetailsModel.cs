namespace SoftwareCenter.Api.Vendors.Models;


// What we are returning to the caller on the POST and the GET /venders/{ID}
public record VendorDetailsModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public VendorPointOfContact PointOfContact { get; set; } = new();
}
