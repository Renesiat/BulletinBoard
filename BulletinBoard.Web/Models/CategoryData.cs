namespace BulletinBoard.Web.Models
{
    public class CategoryData
    {
        public static readonly Dictionary<string, List<string>> Categories = new()
        {
            ["Home Appliances"] = new() { "Fridges", "Washing Machines", "Boilers", "Ovens", "Range Hoods", "Microwaves" },
            ["Computing"] = new() { "PCs", "Laptops", "Monitors", "Printers", "Scanners" },
            ["Smartphones"] = new() { "Android Phones", "iOS/Apple Phones" },
            ["Other"] = new() { "Clothing", "Footwear", "Accessories", "Sports Equipment", "Toys" }
        };
    }
}
