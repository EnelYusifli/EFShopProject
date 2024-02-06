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
BrandService brandService = new BrandService();
WalletService walletService = new WalletService();
while (isContinue)
{
    Console.WriteLine("\n1)Login With Email \t 2)Login With Username\n");
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        userService.LoginAdminWithEmail(email, password);
                        Console.ResetColor();
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please write yes/no");
                            Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        userService.LoginAdminWithUsername(username, password);
                        Console.ResetColor();
                    YesOrNo:
                        Console.WriteLine("\nDo you want to continue? (yes/no)");
                        string? continueOption = Console.ReadLine();
                        if (continueOption is null || continueOption.ToLower() != "yes" && continueOption.ToLower() != "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Please write yes/no");
                            Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
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
    Console.WriteLine("3)Create Brand");
    Console.WriteLine("-------------------");
    Console.WriteLine("4)Deactivate User");
    Console.WriteLine("5)Activate User");
    Console.WriteLine("6)Deactivate Product");
    Console.WriteLine("7)Activate Product");
    Console.WriteLine("8)Deactivate Category");
    Console.WriteLine("9)Activate Category"); 
    Console.WriteLine("10)Deactivate Brand");
    Console.WriteLine("11)Activate Brand");
    Console.WriteLine("-------------------");
    Console.WriteLine("12)Update Product");
    Console.WriteLine("13)Update Category");
    Console.WriteLine("14)Update Brand");
    Console.WriteLine("-------------------");
    Console.WriteLine("15)Get Invoice Report By Time");
    Console.WriteLine("16)Get Canceled Invoice Report By Time");
    Console.WriteLine("17)Get The Most Cart Products");
    Console.WriteLine("-------------------");
    Console.WriteLine("18)Show Deactive Users");
    Console.WriteLine("19)Show Active Users");
    Console.WriteLine("20)Show Deactive Products");
    Console.WriteLine("21)Show Active Products");
    Console.WriteLine("22)Show Deactive Categories");
    Console.WriteLine("23)Show Active Categories"); 
    Console.WriteLine("24)Show Deactive Brands");
    Console.WriteLine("25)Show Active Brands");
    Console.WriteLine("-------------------");
    Console.WriteLine("0)Exit\n");
    Console.WriteLine("Choose an option");
    string? option = Console.ReadLine();
    int intOption;
    bool isInt = int.TryParse(option, out intOption);
    if (isInt)
    {
        if (intOption >= 0 && intOption <= 25)
        {
            switch (intOption)
            {
                case 0:
                    isContinue = false;
                    break;
                case (int)AdminPanel.ShowActiveCategories:
                    int count6 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                        count6++;
                    }
                    Console.ResetColor();
                    if (count6 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no active category");
                        Console.ResetColor();
                    }

                    break;
                case (int)AdminPanel.ShowDeactiveCategories:
                    int count5 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                        count5++;
                    }
                    Console.ResetColor();
                    if (count5 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no deactive category");
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ShowActiveBrands:
                    int count8 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var brand in context.Brands.Where(b => b.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{brand.Id}\n" +
                            $"Name:{brand.Name.ToUpper()}\n");
                        count8++;
                    }
                    Console.ResetColor();
                    if (count8 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no active brand");
                        Console.ResetColor();
                    }

                    break;
                case (int)AdminPanel.ShowDeactiveBrands:
                    int count9 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var brand in context.Brands.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{brand.Id}\n" +
                            $"Name:{brand.Name.ToUpper()}\n");
                        count9++;
                    }
                    Console.ResetColor();
                    if (count9 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no deactive brand");
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ShowDeactiveUsers:
                    int count7 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var deactUser in context.Users.Where(u => u.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{deactUser.Id}\n" +
                            $"Name:{deactUser.Name.ToUpper()}\n");
                        count7++;
                    }
                    Console.ResetColor();
                    if (count7 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no deactive user");
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ShowActiveUsers:
                    int count1 = 0;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foreach (var actUser in context.Users.Where(u => u.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{actUser.Id}\n" +
                            $"Name:{actUser.Name.ToUpper()}\n");
                        count1++;
                    }
                    Console.ResetColor();
                    if (count1 == 0) Console.WriteLine("There is no active user");
                    break;
                case (int)AdminPanel.ShowActiveProducts:
                    int count2 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var actPro in context.Products.Where(u => u.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{actPro.Id}\n" +
                            $"Name:{actPro.Name.ToUpper()}\n");
                        count2++;
                    }
                    Console.ResetColor();
                    if (count2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no active product");
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ShowDeactiveProducts:
                    int count3 = 0;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var deactPro in context.Products.Where(u => u.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{deactPro.Id}\n" +
                            $"Name:{deactPro.Name.ToUpper()}\n");
                        count3++;
                    }
                    Console.ResetColor();
                    if (count3 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("There is no deactive product");
                        Console.ResetColor();
                    }
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
                        Console.ForegroundColor = ConsoleColor.Blue;
                        foreach (var category in context.Categories)
                        {
                            Console.WriteLine($"\nId:{category.Id}\n" +
                                $"Name:{category.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Category Id");
                        int categoryIdForPro = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        foreach (var brand in context.Brands)
                        {
                            Console.WriteLine($"\nId:{brand.Id}\n" +
                                $"Name:{brand.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Brand Id");
                        int brandIdForPro = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        productService.CreateProduct(name, description, price, count, categoryIdForPro, brandIdForPro);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }

                    break;
                case (int)AdminPanel.CreateCategory:
                    try
                    {
                        Console.WriteLine("Enter Name");
                        string? categoryName = Console.ReadLine();
                        Console.WriteLine("Enter description");
                        string? categoryDescription = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        categoryService.CreateCategory(categoryName, categoryDescription);
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.CreateBrand:
                    try
                    {
                        Console.WriteLine("Enter Name");
                        string? brandName = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        brandService.CreateBrand(brandName);
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.DeactivateUser:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var userForDeactivate in context.Users.Where(u => u.IsDeactive == false && u.Id != 1))
                        {
                            Console.WriteLine($"\nId:{userForDeactivate.Id}/Name:{userForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("\nEnter user id");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        userService.DeactivateUser(userId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ActivateUser:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var userForActivate in context.Users.Where(u => u.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{userForActivate.Id}/Name:{userForActivate.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("\nEnter user id");
                        int userId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        userService.ActivateUser(userId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.DeactivateProduct:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == false))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        productService.DeactivateProduct(productId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ActivateProduct:
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var productForDeactivate in context.Products.Where(p => p.IsDeactive == true))
                        {
                            Console.WriteLine($"\nId:{productForDeactivate.Id}/Name:{productForDeactivate.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("\nEnter product id");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        Console.ForegroundColor = ConsoleColor.Green;
                        productService.ActivateProduct(productId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.DeactivateCategory:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                    }
                    Console.ResetColor();
                    Console.WriteLine("Enter Category Id");
                    int categoryId = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        categoryService.DeactivateCategory(categoryId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ActivateCategory:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var category in context.Categories.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{category.Id}\n" +
                            $"Name:{category.Name.ToUpper()}\n");
                    }
                    Console.ResetColor();
                    Console.WriteLine("Enter Category Id");
                    int categoryIdForActivate = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        categoryService.ActivateCategory(categoryIdForActivate);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.DeactivateBrand:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var brand in context.Brands.Where(c => c.IsDeactive == false))
                    {
                        Console.WriteLine($"\nId:{brand.Id}\n" +
                            $"Name:{brand.Name.ToUpper()}\n");
                    }
                    Console.ResetColor();
                    Console.WriteLine("Enter Brand Id");
                    int brandId = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        brandService.DeactivateBrand(brandId);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.ActivateBrand:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    foreach (var brand in context.Brands.Where(c => c.IsDeactive == true))
                    {
                        Console.WriteLine($"\nId:{brand.Id}\n" +
                            $"Name:{brand.Name.ToUpper()}\n");
                    }
                    Console.ResetColor();
                    Console.WriteLine("Enter Brand Id");
                    int brandIdForActivate = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        brandService.ActivateBrand(brandIdForActivate);
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
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
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.TotalPrice}\t        {item.CreatedDate}");
                            total += item.TotalPrice;
                        }
                        Console.WriteLine($"Total sale:{total}");
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.GetCanceledInvoiceReport:
                    Console.WriteLine("Enter Start Time");
                    DateTime canceledStartTime = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("Enter End Time");
                    DateTime canceledEndTime = Convert.ToDateTime(Console.ReadLine());
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        var result = context.GetCanceledInvoiceReport(canceledStartTime, canceledEndTime);
                        Console.WriteLine("TotalPrice\tCreatedDate");
                        decimal total = 0;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.TotalPrice}\t        {item.CreatedDate}");
                        }
                        Console.ResetColor();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;


                case (int)AdminPanel.GetTheMostAddedProducts:
                    Console.WriteLine("Enter the count of products");
                    int countOfPro = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        var result = context.GetTheMostAddedProducts(countOfPro);
                        Console.WriteLine("Product Name\t             Count");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var item in result)
                        {
                            Console.WriteLine($"{item.Name.ToUpper()}\t  {item.ProductCount}");
                        }
                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.UpdateBrand:
                    try
                    {
                    brandName:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        foreach (var existBrand in context.Brands)
                        {
                            Console.WriteLine($"\nId:{existBrand.Id}/" +
                                $"Name:{existBrand.Name.ToUpper()}\n");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Enter Brand Id");
                        int brandIdForUpdate = Convert.ToInt32(Console.ReadLine());
                        if (brandIdForUpdate < 0)
                        {
                            Console.WriteLine("Id cannot be negative");
                            goto brandName;
                        }
                        Brand brand = context.Brands.Find(brandIdForUpdate);
                        if(brand is null)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Brand cannot be found");
                            Console.ResetColor();
                            goto brandName;
                        }
                        Console.WriteLine("Enter new Name:");
                        string name = Console.ReadLine();
                        if (name is not null)
                        {
                            if (brand.Name.ToLower() != name.ToLower())
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                brandService.UpdateBrand(brand,name);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Name cannot be the same");
                                Console.ResetColor();
                                goto brandName;
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Name cannot be null");
                            Console.ResetColor();
                            goto brandName;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();
                    }
                    break;
                case (int)AdminPanel.UpdateCategory:
                    Console.WriteLine("1)Update Category Name");
                    Console.WriteLine("2)Update Category Description");
                    Console.WriteLine("Choose an option");
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
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Category Id");
                                        int categoryIdForUpdate = Convert.ToInt32(Console.ReadLine());
                                        if (categoryIdForUpdate < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto categoryName;
                                        }
                                        Category category = context.Categories.Find(categoryIdForUpdate);
                                        if (category is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Category cannot be found");
                                            Console.ResetColor();
                                            goto categoryName;
                                        }
                                        Console.WriteLine("Enter new Name:");
                                        string name = Console.ReadLine();
                                        if (name is not null)
                                        {
                                            if (category.Name.ToLower() != name.ToLower())
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                categoryService.UpdateCategory(category, name, category.Description);
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Name cannot be the same");
                                                Console.ResetColor();
                                                goto categoryName;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Name cannot be null");
                                            Console.ResetColor();
                                            goto categoryName;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;
                                case (int)UpdateCategory.UpdateDescription:
                                    try
                                    {
                                    categoryDesc:
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n" +
                                                $"Description:{existCategory.Description}");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Category Id");
                                        int categoryIdForCat = Convert.ToInt32(Console.ReadLine());
                                        if (categoryIdForCat < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto categoryDesc;
                                        }
                                        Category category = context.Categories.Find(categoryIdForCat);
                                        if (category is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Category cannot be found");
                                            Console.ResetColor();
                                            goto categoryDesc;
                                        }
                                        Console.WriteLine("Enter new Description:");
                                        string desc = Console.ReadLine();
                                        if (category.Description.ToLower() != desc.ToLower())
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            categoryService.UpdateCategory(category, category.Name, desc);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Description cannot be the same");
                                            Console.ResetColor();
                                            goto categoryDesc;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;
                            }
                        }
                        else Console.WriteLine("Invalid option. Please select again.");
                    }
                    else Console.WriteLine("Please enter correct format");
                    break;
                case (int)AdminPanel.UpdateProduct:
                    Console.WriteLine("\n1)Update Product Name");
                    Console.WriteLine("2)Update Product Description");
                    Console.WriteLine("3)Update Product Price");
                    Console.WriteLine("4)Update Product Available count");
                    Console.WriteLine("5)Update Product Category");
                    Console.WriteLine("6)Update Product Brand\n");
                    Console.WriteLine("Choose an option");
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
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                                $"Name:{existProduct.Name.ToUpper()}\n" +
                                                $"Description:{existProduct.Description}\n" +
                                                $"Price:${existProduct.Price}\n" +
                                                $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productName;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productName;
                                        }
                                        Console.WriteLine("Enter new Name:");
                                        string name = Console.ReadLine();
                                        if (name is not null)
                                        {
                                            if (product.Name.ToLower() != name.ToLower())
                                            {
                                                Console.ForegroundColor = ConsoleColor.Green;
                                                productService.UpdateProduct(product, name, product.Description, product.Price, product.AvailableCount, product.CategoryId, product.BrandId);
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Yellow;
                                                Console.WriteLine("Name cannot be the same");
                                                Console.ResetColor();
                                                goto productName;
                                            }
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Name cannot be null");
                                            Console.ResetColor();
                                            goto productName;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;

                                case (int)UpdateProduct.UpdateDescription:
                                    try
                                    {
                                    productDesc:
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productDesc;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productDesc;
                                        }
                                        Console.WriteLine("Enter new Description:");
                                        string desc = Console.ReadLine();
                                        if (product.Description.ToLower() != desc.ToLower())
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.UpdateProduct(product, product.Name, desc, product.Price, product.AvailableCount, product.CategoryId, product.BrandId);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Description cannot be the same");
                                            Console.ResetColor();
                                            goto productDesc;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;
                                case (int)UpdateProduct.UpdatePrice:
                                    try
                                    {
                                    productPrice:
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productPrice;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productPrice;
                                        }
                                        Console.WriteLine("Enter new Price:");
                                        decimal price = Convert.ToDecimal(Console.ReadLine());
                                        if (product.Price != price)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.UpdateProduct(product, product.Name, product.Description, price, product.AvailableCount, product.CategoryId, product.BrandId);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Price cannot be the same");
                                            Console.ResetColor();
                                            goto productPrice;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;
                                case (int)UpdateProduct.UpdateAvailableCount:
                                    try
                                    {
                                    productAvailable:
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existProduct in context.Products)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.WriteLine("Id cannot be negative");
                                            goto productAvailable;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productAvailable;
                                        }
                                        Console.WriteLine("Enter new Available count:");
                                        int available = Convert.ToInt32(Console.ReadLine());
                                        if (product.AvailableCount != available)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.UpdateProduct(product, product.Name, product.Description, product.Price, available, product.CategoryId, product.BrandId);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Available count cannot be the same");
                                            Console.ResetColor();
                                            goto productAvailable;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;

                                case (int)UpdateProduct.UpdateCategory:
                                    try
                                    {
                                    productCategory:
                                        var productsWithCategories = context.Products.Where(p => p.IsDeactive == false).Include(p => p.Category);
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        foreach (var existProduct in productsWithCategories)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n" +
                                             $"Category:{existProduct.Category.Name.ToUpper()}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Id cannot be negative");
                                            Console.ResetColor();
                                            goto productCategory;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productCategory;
                                        }
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existCategory in context.Categories)
                                        {
                                            Console.WriteLine($"\nId:{existCategory.Id}/" +
                                                $"Name:{existCategory.Name.ToUpper()}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter new Category Id:");
                                        int categoryIdForUpt = Convert.ToInt32(Console.ReadLine());
                                        if (product.CategoryId != categoryIdForUpt)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.UpdateProduct(product, product.Name, product.Description, product.Price, product.AvailableCount, categoryIdForUpt, product.BrandId);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Category cannot be the same");
                                            Console.ResetColor();
                                            goto productCategory;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
                                    }
                                    break;
                                case (int)UpdateProduct.UpdateBrand:
                                    try
                                    {
                                    productBrand:
                                        var productsWithBrands = context.Products.Where(p => p.IsDeactive == false).Include(p => p.Brand);
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        foreach (var existProduct in productsWithBrands)
                                        {
                                            Console.WriteLine($"\nId:{existProduct.Id}/" +
                                             $"Name:{existProduct.Name.ToUpper()}\n" +
                                             $"Description:{existProduct.Description}\n" +
                                             $"Price:${existProduct.Price}\n" +
                                             $"Available:{existProduct.AvailableCount}\n" +
                                             $"Category:{existProduct.Brand.Name.ToUpper()}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter Product Id");
                                        int productId = Convert.ToInt32(Console.ReadLine());
                                        if (productId < 0)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Id cannot be negative");
                                            Console.ResetColor();
                                            goto productBrand;
                                        }
                                        Product product = context.Products.Find(productId);
                                        if (product is null)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Product cannot be found");
                                            Console.ResetColor();
                                            goto productBrand;
                                        }
                                        Console.ForegroundColor = ConsoleColor.Magenta;
                                        foreach (var existBrand in context.Brands)
                                        {
                                            Console.WriteLine($"\nId:{existBrand.Id}/" +
                                                $"Name:{existBrand.Name.ToUpper()}\n");
                                        }
                                        Console.ResetColor();
                                        Console.WriteLine("Enter new Brand Id:");
                                        int brandIdForUpt = Convert.ToInt32(Console.ReadLine());
                                        if (product.BrandId != brandIdForUpt)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            productService.UpdateProduct(product, product.Name, product.Description, product.Price, product.AvailableCount, product.CategoryId, brandIdForUpt);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine("Brand cannot be the same");
                                            Console.ResetColor();
                                            goto productBrand;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine(ex.Message);
                                        Console.ResetColor();
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


