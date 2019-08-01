namespace E_Commerce_Site.Migrations
{
    using E_Commerce_Site.Context;
    using E_Commerce_Site.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<E_Commerce_Site.Context.MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "E_Commerce_Site.Context.MainContext";
        }

        protected override void Seed(Context.MainContext context)
        {
            //    Tag tag1 = new Tag()
            //    {
            //        TagName = "Breathing"

            //    };

            //    Tag tag2 = new Tag()
            //    {
            //        TagName = "Fit"

            //    };

            //    Tag tag3 = new Tag()
            //    {
            //        TagName = "Comfort"

            //    };



            //    Size size1 = new Size()
            //    {
            //        SizeName = "XS"

            //    };

            //    Size size2 = new Size()
            //    {
            //        SizeName = "S"

            //    };

            //    Size size3 = new Size()
            //    {
            //        SizeName = "M"

            //    };
            //    Size size4 = new Size()
            //    {
            //        SizeName = "L"

            //    };
            //    Size size5 = new Size()
            //    {
            //        SizeName = "XL"

            //    };
            //    Size size6 = new Size()
            //    {
            //        SizeName = "XXL"

            //    };







            //    Color color1 = new Color()
            //    {
            //        ColorName = "Red"

            //    };

            //    Color color2 = new Color()
            //    {
            //        ColorName = "Blue"

            //    };


            //    Color color3 = new Color()
            //    {
            //        ColorName = "Green"

            //    };



            //    Customer customer1 = new Customer()
            //    {

            //        CustomerName = "Mehmet Erdem",
            //        Address = "70 HumberCollege Blvd",
            //        DateOfBirth = new DateTime(2008, 3, 2),
            //        PhoneNumber = "64776174747",
            //        PostalCode = "m8x d3x",
            //        Country = "Canada",
            //        State = "On",
            //        City = "Toronto",
            //        Email = "mehmet@erdem.com"


            //    };

            //    Category Category1 = new Category()
            //    {
            //        CategoryName = "Men"


            //    };
            //    Category Category2 = new Category()
            //    {
            //        CategoryName = "Women"


            //    };

            //    Category Category3 = new Category()
            //    {
            //        CategoryName = "Kids"


            //    };




            //SubCategory subcategory1 = new SubCategory()
            //{
            //    CategoryId = 1,
            //    SubCategoryName = "Blouses"
            //};






            //context.Categories.Add(Category1);
            //context.Categories.Add(Category2);
            //context.Categories.Add(Category3);

            //context.SubCategories.Add(subcategory1);
            ////context.SubCategories.Add(subcategory2);
            ////context.SubCategories.Add(subcategory3);
            //context.Colors.Add(color1);
            //context.Colors.Add(color2);
            //context.Colors.Add(color3);
            //context.Sizes.Add(size1);
            //context.Sizes.Add(size2);
            //context.Sizes.Add(size3);
            //context.Sizes.Add(size4);
            //context.Sizes.Add(size5);
            //context.Sizes.Add(size6);
            //context.Tags.Add(tag1);
            //context.Tags.Add(tag2);
            //context.Tags.Add(tag3);
            //context.Customers.Add(customer1);


            base.Seed(context);
        }
    }
}
