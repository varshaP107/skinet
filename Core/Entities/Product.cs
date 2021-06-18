namespace Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price{get;set;}
        public string PictureUrl { get; set; }
        public ProductType ProductType{get;set;}
        public int ProductTypeId { get; set; }  //here we added full type as well as id, so that it help Entity f/w to create relationship and Foreign key
        public ProductBrand ProductBrand{get;set;}
        public int ProductBrandId { get; set; }

    }
}