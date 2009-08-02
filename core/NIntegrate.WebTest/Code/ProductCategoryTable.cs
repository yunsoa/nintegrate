using NIntegrate.Data;

namespace NIntegrate.WebTest.Code
{
    class ProductCategoryTable : QueryTable
    {
        public ProductCategoryTable()
            : base("SalesLT.ProductCategory", "Test", false)
        {
        }

        public Int32Column ProductCategoryID = new Int32Column("ProductCategoryID");
        public Int32Column ParentProductCategoryID = new Int32Column("ParentProductCategoryID");
        public StringColumn Name = new StringColumn("Name", false);
        public GuidColumn rowguid = new GuidColumn("rowguid");
        public DateTimeColumn ModifiedDate = new DateTimeColumn("ModifiedDate");
    }
}
