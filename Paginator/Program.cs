using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WorkingWithEFCore;

Northwind db = new();
WriteLine($"Provider : {db.Database.ProviderName}");

// QueryingCategories();
// FilterIncludes();
// QueryingProducts();
// QueryingWithLike();
// GetRandomProduct();

/*
#region CRUD
ListProducts();
    // Use of Create
    var resultAdd = AddProduct(categoryId: 6, productName: "La Pizza de Don Cangrejo", price: 500M);
    AddProduct(categoryId: 6, productName: "La Pizza de Don Cangrejo", price: 500M);
    AddProduct(categoryId: 6, productName: "La Pizza de Don Cangrejo", price: 500M);
    AddProduct(categoryId: 6, productName: "La Pizza de Don Cangrejo", price: 500M);
    if(resultAdd.affected == 1)
    {
        WriteLine($"Add product succesful with ID: {resultAdd.productId}");
    }
ListProducts(new int[] {resultAdd.productId});

// Use of Update
// var resultUpdate = UpdateProductPrice(productNameStartWith:"La ", amount:40M);
// if(resultUpdate.affected == 1)
// {
//     WriteLine($"Increase price success for ID : {resultUpdate.productId}");
// }
// ListProducts(productsToHiglight: new[] {resultUpdate.productId});

// Use of better Update
var resultUpdateBetter = UpdateProductPriceBetter(productNameStartWith: "La ", amount: 20M);
if(resultUpdateBetter.affected > 0)
{
    WriteLine("Increase product price succesful.");
}
ListProducts(resultUpdateBetter.productsId);

// Use of Delete and Better Delete
WriteLine("About to delete all products that start with La ");
Write("Press Enter to continue : ");
if(ReadKey(intercept: true).Key == ConsoleKey.Enter)
{
    int deleted = DeleteProductsBetter(productNameStartWith: "La ");
    WriteLine($"{deleted} product(s) were deleted");
}
else
{
    WriteLine("Delete was cancelced");
}

ListProducts();


#endregion*/

int res;
    do {
        WriteLine("Chose the amount of products that you want to se per page: \n 1- 1 \n 2- 5\n 3- 10\n 4- 25\n 5- 30");
        res = int.Parse(ReadLine());
        switch(res){
            case(1):{
ListProducts(1);
                break;
            }
            case(2):{
ListProducts(5);
                break;
            }
            case(3):{
ListProducts(10);
                break;
            }
            case(4):{
ListProducts(25);
                break;
            }
            case(5):{
ListProducts(30);
                break;
            }

        }
    }while(res > 5 || res < 1);