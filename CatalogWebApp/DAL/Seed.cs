namespace CatalogWebApp.DAL
{
    public static class Seed
    {
        public static List<CatalogModel> Data =>
            new List<CatalogModel>()
            {
                new CatalogModel()
                {
                    Id = 1,
                    Name = "IPhine"
                },
                new CatalogModel()
                {
                    Id = 2,
                    Name = "Samsung"
                },
                new CatalogModel()
                {
                    Id = 3,
                    Name = "LG"
                }
            };
    }
}
