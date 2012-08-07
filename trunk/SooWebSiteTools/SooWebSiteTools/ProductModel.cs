using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SooWebSiteTools
{
    class ProductModel
    {
        public int StoreId { get; set; }
        public int ProductId {get; set; }
        public string Model {get; set; }
        public string Sku {get; set; }
        public string Upc {get; set; }
        public int Quantity {get; set; }
        public int StockStatusId {get; set; }
        public string Image {get; set; }
        public string ManufacturerId { get; set; }
        public int shipping {get; set; }
        public decimal Price {get; set; }
        public int points { get; set; }
        public int TaxClassId {get; set; }
        public int WeightClassId { get; set; }
        public int LengthClassId { get; set; }
        public DateTime AvailableDate {get; set; }
        public int Subtract {get; set; }
        public int Length {get; set; }
        public int Weight { get; set; }
        public int Width {get; set; }
        public int Height {get; set; }
        public int Minimum {get; set; }
        public int Statust {get; set; }
        public int SortOrder {get; set; }
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Description2 { get; set; }
        public string MetaKeyword { get; set; }
        public string MetaDescription { get; set; }

        public List<ImageModel> Images { get; set; }
        public List<TagModel> Tags { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<RelatedModel> Relateds { get; set; }
        
    }
}
