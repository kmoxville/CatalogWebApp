namespace CatalogWebApp.DAL
{
    public static class Seed
    {
        public static List<ProductModel> Data =>
            new List<ProductModel>()
            {
                new ProductModel()
                {
                    Id = 1,
                    Name = "IPhone"
                },
                new ProductModel()
                {
                    Id = 2,
                    Name = "Samsung"
                },
                new ProductModel()
                {
                    Id = 3,
                    Name = "LG"
                }
            };
    }
}
