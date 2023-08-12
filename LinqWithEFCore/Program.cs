using LinqWithEFCore.Entities;
using Microsoft.EntityFrameworkCore;
using static System.Console;

//FilterAndSort();
//JoinCategoriesAndProducts();
//GroupJoin();
AggregateProducts();
static void FilterAndSort() {
    using NwContext db = new();
    DbSet<Product> products = db.Products;

    var sortedAndFilteredProducts = products
        .Where(p => p.UnitPrice < 10M)
        .OrderByDescending(p => p.UnitPrice);

    // more efficient query , with anonymouth obj , and gettin only 3 columns of db
    var projectedProducts = products.Select(p => new {
        p.ProductId,
        p.UnitPrice,
        p.ProductName
    });

    WriteLine("Products that cost less than $10:");
    foreach (var p in projectedProducts) {
        WriteLine("{0}: {1} costs {2:$#,##0.00}",
        p.ProductId, p.ProductName, p.UnitPrice);
    }
    WriteLine();
}

static void JoinCategoriesAndProducts() {
    using NwContext db = new();

    var queryJoin = db.Categories.Join(
        inner: db.Products,
        outerKeySelector: c => c.CategoryId,
        innerKeySelector: p => p.ProductId,
        resultSelector: (c, p) => new {
            c.CategoryName,
            p.ProductName,
            p.ProductId
        }).OrderBy(cp => cp.CategoryName);

    foreach (var item in queryJoin) {
        WriteLine("{0}: {1} is in {2}.",
        arg0: item.ProductId,
        arg1: item.ProductName,
        arg2: item.CategoryName);
    }
}

static void GroupJoin() {
    using NwContext db = new();

    var queryGroup = db.Categories.AsEnumerable().GroupJoin(
        inner: db.Products,
        outerKeySelector: category => category.CategoryId,
        innerKeySelector: product => product.CategoryId,
        resultSelector: (c, matchingProducts) => new {
            c.CategoryName,
            Products = matchingProducts.OrderBy(p => p.ProductName)
        });

    foreach (var category in queryGroup) {
        WriteLine("{0} has {1} products.",
        arg0: category.CategoryName,
        arg1: category.Products.Count());

        foreach (var product in category.Products) {
            WriteLine($" {product.ProductName}");
        }
    }
}

static void AggregateProducts() {
    using NwContext db = new();
    WriteLine($"Count :   " + db.Products.Count());
    WriteLine("highest price : "+ db.Products.Max(p => p.UnitPrice));
    WriteLine("sum of units in stock : " + db.Products.Sum(p => p.UnitsInStock));
    WriteLine("sum of units on order : " + db.Products.Sum(p => p.UnitsOnOrder));
    WriteLine("avg of prices : " + db.Products.Average(p => p.UnitPrice));
    WriteLine("value of units in stock : " + db.Products.Sum(p => p.UnitPrice * p.UnitsInStock));
}