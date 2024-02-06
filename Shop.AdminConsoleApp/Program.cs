using Microsoft.EntityFrameworkCore;
using Shop.Business.Services;
using Shop.Business.Utilities.Helper;
using Shop.Core.Entities;
using Shop.DataAccess;



string appStart = "Application started...";
string Welcome = "Welcome!";
Console.SetCursorPosition((Console.WindowWidth - appStart.Length) / 2, Console.CursorTop);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine(appStart);
Console.SetCursorPosition((Console.WindowWidth - Welcome.Length) / 2, Console.CursorTop);
Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine(Welcome);
Console.ResetColor();

bool isContinue = true;
User? user = null;
ShopDbContext context = new ShopDbContext();
UserService userService = new UserService();
ProductService productService = new ProductService();
CategoryService categoryService = new CategoryService();
WalletService walletService = new WalletService();
while (isContinue)
{
    Console.WriteLine("1)Login With Email");
    Console.WriteLine("2)Login With Username");
    Console.WriteLine("Select an option");
    string? loginOption = Console.ReadLine();
    int loginIntOption;
    bool isIntLogin = int.TryParse(loginOption, out loginIntOption);
    if (isIntLogin)
    {
        if (loginIntOption >= 0 && loginIntOption <= 2)
        {
            switch (loginIntOption)
            {
                
                case (int)LoginMethod.LoginWithMail:
                    try
                    {
                        Console.WriteLine("Enter Email");
                        string email = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string password = userService.ReadPasswordFromConsole();
                        string hashedPassword = userService.HashPassword(password);
                        userService.LoginAdminWithEmail(email, password);
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.WriteLine("Please write yes/no");
                            goto YesOrNo;
                        }
                        if (continueOption.ToLower() == "no")
                            return;
                        if (continueOption.ToLower() == "yes")
                        {
                            user = userService.FindUserByEmail(email);
                            isContinue = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)LoginMethod.LoginWithUsername:
                    try
                    {
                        Console.WriteLine("Enter Username");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Password");
                        string password = userService.ReadPasswordFromConsole();
                        string hashedPassword = userService.HashPassword(password);
                        userService.LoginAdminWithUsername(username, password);
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.WriteLine("Please write yes/no");
                            goto YesOrNo;
                        }
                        if (continueOption.ToLower() == "no")
                            return;
                        if (continueOption.ToLower() == "yes")
                        {
                            user = userService.FindUserByUsername(username);
                            isContinue = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
        }
        else Console.WriteLine("Invalid option. Please select again.");
    }
    else Console.WriteLine("Please enter correct format");
}

isContinue = true;
while (isContinue)
{
    Console.WriteLine("\n1)Create Product");
    Console.WriteLine("2)Create Category");
    Console.WriteLine("3)Deactivate User");
    Console.WriteLine("4)Activate User");
    Console.WriteLine("5)Deactivate Product");
    Console.WriteLine("6)Activate Product");
    Console.WriteLine("7)Deactivate Category");
    Console.WriteLine("8)Activate Category");
    Console.WriteLine("9)Update Product");
    Console.WriteLine("10)Update Category");
    Console.WriteLine("11)Get Invoice Report By Time");
    Console.WriteLine("12)Get Canceled Invoice Report By Time");
    Console.WriteLine("13)Get The Most Cart Products");
    Console.WriteLine("14)Show Deactive Users");
    Console.WriteLine("15)Show Active Users");
    Console.WriteLine("16)Show Deactive Products");
    Console.WriteLine("17)Show Active Products");
    Console.WriteLine("18)Show Deactive Categories");
    Console.WriteLine("19)Show Active Categories");
    Console.WriteLine("0)Exit\n");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 19)
        {
            switch (intOption)
            {
                case 0:
                    isContinue = false;
                    break;
                case (int)AdminPanel.ShowActiveCategories:
                    int count6 = 0;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                        count6++;
                    }
                    if (count6 == 0) Console.WriteLine("There is no active category");
                    break;
                case (int)AdminPanel.ShowDeactiveCategories:
                    int count5 = 0;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                        count5++;
                    }
                    if (count5 == 0) Console.WriteLine("There is no deactive category");
                    break;
                case (int)AdminPanel.ShowDeactiveUsers:
                    int count7 = 0;
                    foreach (var deactUser in context.Users.Where(u => u.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{deactUser.Id}\n" +
                            $"Name:{deactUser.Name.ToUpper()}\n");
                        count7++;
                    }
                    if (count7 == 0) Console.WriteLine("There is no deactive user");
                    break;
                case (int)AdminPanel.ShowActiveUsers:
                    int count1 = 0;
                    foreach (var actUser in context.Users.Where(u => u.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{actUser.Id}\n" +
                            $"Name:{actUser.Name.ToUpper()}\n");
                        count1++;
                    }
                    if (count1 == 0) Console.WriteLine("There is no active user");
                    break;
                case (int)AdminPanel.ShowActiveProducts:
                    int count2 = 0;
                    foreach (var actPro in context.Products.Where(u => u.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{actPro.Id}\n" +
                            $"Name:{actPro.Name.ToUpper()}\n");
                        count2++;
                    }
                    if (count2 == 0) Console.WriteLine("There is no active product");
                    break;
                case (int)AdminPanel.ShowDeactiveProducts:
                    int count3 = 0;
                    foreach (var deactPro in context.Products.Where(u => u.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{deactPro.Id}\n" +
                            $"Name:{deactPro.Name.ToUpper()}\n");
                        count3++;
                    }
                    if (count3 == 0) Console.WriteLine("There is no deactive product");
                    break;
                case (int)AdminPanel.CreateProduct:
                    try
                    {
                        Console.WriteLine("Enter name");
                        string name = Console.ReadLine();
                        Console.WriteLine("Enter description");
                        string description = Console.ReadLine();
                        Console.WriteLine("Enter price");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Enter availability count");
                        int count = Convert.ToInt32(Console.ReadLine());
                        foreach (var category in context.Categories)
                        {
                            Console.WriteLine($"\nId:{category.Id}\n" +
                                $"Name:{category.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("Enter Category Id");
                        int categoryIdForPro = Convert.ToInt32(Console.ReadLine());
                        productService.CreateProduct(name, description, price, count, categoryIdForPro);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    break;
                case (int)AdminPanel.CreateCategory:
                    try
                    {
                        Console.WriteLine("Enter Name");
                        string? categoryName = Console.ReadLine();
                        Console.WriteLine("Enter description");
                        string? categoryDescription = Console.ReadLine();
                        categoryService.CreateCategory(categoryName, categoryDescription);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.DeactivateUser:
                    try
                    {
                        foreach (var userForDeactivate in context.Users.Where(u => u.IsDeactive == false && u.Id!=1))
                        {
                            Console.WriteLine($"\nId:{userForDeactivate.Id}/Name:{userForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter user id");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        userService.DeactivateUser(userId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.ActivateUser:
                    try
                    {
                        foreach (var userForActivate in context.Users.Where(u => u.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{userForActivate.Id}/Name:{userForActivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter user id");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        userService.ActivateUser(userId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.DeactivateProduct:
                    try
                    {
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == false))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        productService.DeactivateProduct(productId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.ActivateProduct:
                    try
                    {
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        productService.ActivateProduct(productId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.DeactivateCategory:
                    foreach (var category in context.Categories.Where(c=>c.IsDeactive==false))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                    }
                    Console.WriteLine("Enter Category Id");
                    int categoryId = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        categoryService.DeactivateCategory(categoryId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.ActivateCategory:
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                    }
                    Console.WriteLine("Enter Category Id");
                    int categoryIdForActivate = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        categoryService.ActivateCategory(categoryIdForActivate);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.GetInvoiceReport:
                    Console.WriteLine("Enter Start Time");
                    DateTime startTime = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter End Time");
                    DateTime endTime = Convert.ToDateTime(Console.ReadLine());
                    try
                    {
                        var result = context.GetInvoiceReport(startTime, endTime);
                        Console.WriteLine("TotalPrice\tCreatedDate");
                        decimal total = 0;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.TotalPrice}\t        {item.CreatedDate}");
                            total += item.TotalPrice;
                        }
                        Console.WriteLine($"Total sale:{total}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.GetCanceledInvoiceReport:
                    Console.WriteLine("Enter Start Time");
                    DateTime canceledStartTime = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter End Time");
                    DateTime canceledEndTime = Convert.ToDateTime(Console.ReadLine());
                    try
                    {
                        var result = context.GetCanceledInvoiceReport(canceledStartTime, canceledEndTime);
                        Console.WriteLine("TotalPrice\tCreatedDate");
                        decimal total = 0;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.TotalPrice}\t        {item.CreatedDate}");
                            total += item.TotalPrice;
                        }
                        Console.WriteLine($"Total sale:{total}");

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;


                case (int)AdminPanel.GetTheMostAddedProducts:
                    Console.WriteLine("Enter the count of products");
                    int countOfPro = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        var result = context.GetTheMostAddedProducts(countOfPro);
                        Console.WriteLine("Product Name\t             Count");
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.Name.ToUpper()}\t  {item.ProductCount}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case (int)AdminPanel.UpdateCategory:
                    Console.WriteLine("1)Update Category Name");
                    Console.WriteLine("2)Update Category Description");
                    string? optionUpdateCategory = Console.ReadLine();
                    int intOptionUpdateCategory;
                    bool isOptionUpdateCategory = int.TryParse(optionUpdateCategory, out intOptionUpdateCategory);
                    if (isOptionUpdateCategory)
                    {
                        if (intOptionUpdateCategory > 0 && intOptionUpdateCategory <= 2)
                        {
                            switch (intOptionUpdateCategory)
                            {
                                case (int)UpdateCategory.UpdateName:
                                    try
                                    {
                                    categoryName:
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n");
                                        }
                                        Console.WriteLine("Enter Category Id");
                                        int categoryIdForUpdate = Convert.ToInt32(Console.ReadLine());
                                        if (categoryIdForUpdate < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto categoryName;
                                        }
                                        Category category = context.Categories.Find(categoryIdForUpdate);
                                        Console.WriteLine("Enter new Name:");
                                        string name = Console.ReadLine();
                                        if (name is not null)
                                        {
                                            if (category.Name.ToLower() != name.ToLower())
                                                categoryService.UpdateCategory(category, name, category.Description);
                                            else
                                            {
                                                Console.WriteLine("Name cannot be the same");
                                                goto categoryName;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Name cannot be null");
                                            goto categoryName;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case (int)UpdateCategory.UpdateDescription:
                                    try
                                    {
                                    categoryDesc:
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n" +
                                                $"Description:{existCategory.Description}");
                                        }
                                        Console.WriteLine("Enter Category Id");
                                        int categoryIdForCat = Convert.ToInt32(Console.ReadLine());
                                        if (categoryIdForCat < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto categoryDesc;
                                        }
                                        Category category = context.Categories.Find(categoryIdForCat);
                                        Console.WriteLine("Enter new Description:");
                                        string desc = Console.ReadLine();
                                            if (category.Description.ToLower() != desc.ToLower())
                                                categoryService.UpdateCategory(category, category.Name, desc);
                                            else
                                            {
                                                Console.WriteLine("Description cannot be the same");
                                                goto categoryDesc;
                                            }
                                        
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                            }
                        }
                        else Console.WriteLine("Invalid option. Please select again.");
                    }
                    else Console.WriteLine("Please enter correct format");
                    break;
                case (int)AdminPanel.UpdateProduct:
                    Console.WriteLine("1)Update Product Name");
                    Console.WriteLine("2)Update Product Description");
                    Console.WriteLine("3)Update Product Price");
                    Console.WriteLine("4)Update Product Available count");
                    Console.WriteLine("5)Update Product Category");
                    string? optionUpdateProduct = Console.ReadLine();
                    int intOptionUpdateProduct;
                    bool isOptionUpdateProduct = int.TryParse(optionUpdateProduct, out intOptionUpdateProduct);
                    if (isOptionUpdateProduct)
                    {
                        if (intOptionUpdateProduct > 0 && intOptionUpdateProduct <= 5)
                        {
                            switch (intOptionUpdateProduct)
                            {
                                case (int)UpdateProduct.UpdateName:
                                    try
                                    {
                                    productName:
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                                $"Name:{existProduct.Name.ToUpper()}\n" +
                                                $"Description:{existProduct.Description}\n" +
                                                $"Price:${existProduct.Price}\n" +
                                                $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.WriteLine("Enter product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productName;
                                        }
                                        Product product = context.Products.Find(productId);
                                        Console.WriteLine("Enter new Name:");
                                        string name = Console.ReadLine();
                                        if (name is not null)
                                        {
                                            if (product.Name.ToLower() != name.ToLower())
                                                productService.UpdateProduct(product, name, product.Description,product.Price,product.AvailableCount,product.CategoryId);
                                            else
                                            {
                                                Console.WriteLine("Name cannot be the same");
                                                goto productName;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Name cannot be null");
                                            goto productName;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case (int)UpdateProduct.UpdateDescription:
                                    try
                                    {
                                    productDesc:
                                        foreach (var existProduct in context.Products)
                                        {
                                               Console.WriteLine($"\nId:{existProduct.Id}/" +
                                                $"Name:{existProduct.Name.ToUpper()}\n" +
                                                $"Description:{existProduct.Description}\n" +
                                                $"Price:${existProduct.Price}\n" +
                                                $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productDesc;
                                        }
                                        Product product = context.Products.Find(productId);
                                        Console.WriteLine("Enter new Description:");
                                        string desc = Console.ReadLine();
                                        if (product.Description.ToLower() != desc.ToLower())
                                            productService.UpdateProduct(product, product.Name, desc,product.Price,product.AvailableCount,product.CategoryId);
                                        else
                                        {
                                            Console.WriteLine("Description cannot be the same");
                                            goto productDesc;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case (int)UpdateProduct.UpdatePrice:
                                    try
                                    {
                                    productPrice:
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productPrice;
                                        }
                                        Product product = context.Products.Find(productId);
                                        Console.WriteLine("Enter new Price:");
                                       decimal price=Convert.ToDecimal(Console.ReadLine());
                                        if (product.Price!= price)
                                            productService.UpdateProduct(product, product.Name, product.Description, price, product.AvailableCount, product.CategoryId);
                                        else
                                        {
                                            Console.WriteLine("Price cannot be the same");
                                            goto productPrice;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                                case (int)UpdateProduct.UpdateAvailableCount:
                                    try
                                    {
                                    productAvailable:
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productAvailable;
                                        }
                                        Product product = context.Products.Find(productId);
                                        Console.WriteLine("Enter new Available count:");
                                        int available = Convert.ToInt32(Console.ReadLine());
                                        if (product.AvailableCount != available)
                                            productService.UpdateProduct(product, product.Name, product.Description, product.Price, available, product.CategoryId);
                                        else
                                        {
                                            Console.WriteLine("Available count cannot be the same");
                                            goto productAvailable;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;

                                case (int)UpdateProduct.UpdateCategory:
                                    try
                                    {
                                    productCategory:
                                        var productsWithCategories = context.Products.Where(p=>p.IsDeactive==false).Include(p => p.Category);
                                        foreach (var existProduct in productsWithCategories)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n" +
                                             $"Category:{existProduct.Category.Name.ToUpper()}\n");

                                        }
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productCategory;
                                        }
                                        Product product = context.Products.Find(productId);
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n");
                                        }
                                        Console.WriteLine("Enter new Category Id:");
                                        int categoryIdForUpt = Convert.ToInt32(Console.ReadLine());
                                        if (product.CategoryId != categoryIdForUpt)
                                            productService.UpdateProduct(product, product.Name, product.Description, product.Price, product.AvailableCount, categoryIdForUpt);
                                        else
                                        {
                                            Console.WriteLine("Category cannot be the same");
                                            goto productCategory;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                    break;
                            }
                        }
                        else Console.WriteLine("Invalid option. Please select again.");
                    }
                    else Console.WriteLine("Please enter correct format");
                    break;

                default:
                    isContinue = false;
                    break;
            }
        }
        else Console.WriteLine("Invalid option. Please select again.");
    }
    else Console.WriteLine("Please enter correct format");
}


