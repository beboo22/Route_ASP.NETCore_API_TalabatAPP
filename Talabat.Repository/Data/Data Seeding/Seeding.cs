using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entity;
using Talabat.Core.Entity.Identity;
using Talabat.Core.Entity.Order_Aggregrate;

namespace Talabat.Repository.Data.Data_Seeding
{
    public class Seeding
    {
        public static async Task DataSeeding(StoreDbContext storeDb)
        {
            if(storeDb.Set<DeliveryMethod>().Count()==0)
            {
                var connction = string.Empty;
                var filepath = Path.Combine(/*Directory.GetCurrentDirectory()*/"D:\\.net\\.net\\Ahmed Nassr\\Api\\Talabat solution\\Talabat.Repository\\"
                                            , "Data\\Data Seeding",
                                            "delivery.json");
                if (File.Exists(filepath))
                {
                    connction = File.ReadAllText(filepath);
                }
                else
                {
                    Console.WriteLine("file products.json doen't exists");
                }
                var desiralization = new List<DeliveryMethod>();

                try
                {
                    desiralization = JsonSerializer.Deserialize<List<DeliveryMethod>>(connction);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing products.json: {ex.Message}");
                    return;
                }

                if (desiralization is not null)
                {
                    foreach (var item in desiralization)
                        storeDb.Set<DeliveryMethod>().Add(item);

                    try
                    {
                        await storeDb.SaveChangesAsync();
                        Console.WriteLine("Data seeding completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving changes to the database: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("file is empty.");
                }
            }

            if (storeDb.Set<Product>().Count() == 0)
            {
                var connction = string.Empty;
                var filepath = Path.Combine("D:\\.net\\.net\\Ahmed Nassr\\Api\\Talabat solution\\Talabat.Repository\\", "Data\\Data Seeding", "products.json");
                if (File.Exists(filepath))
                {
                    connction = File.ReadAllText(filepath);
                }
                else
                {
                    Console.WriteLine("file products.json doen't exists");
                }
                var desiralization = new List<Product>();

                try
                {
                    desiralization = JsonSerializer.Deserialize<List<Product>>(connction).ToList();

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing products.json: {ex.Message}");
                    return;
                }

                if (desiralization is not null)
                {
                    foreach (var item in desiralization)
                        storeDb.Set<Product>().Add(item);
                    //SqlException: String or binary data would be truncated in table 'TalabatApi.dbo.Products',
                    //column 'Description'.Truncated value: 'L'.
                    try
                    {
                        await storeDb.SaveChangesAsync();
                        Console.WriteLine("Data seeding completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving changes to the database: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("file is empty.");
                }
            }

            if (storeDb.Set<ProductBrand>().Count() == 0)
            {
                var connction = string.Empty;
                var filepath = Path.Combine(/*Directory.GetCurrentDirectory()*/"D:\\.net\\.net\\Ahmed Nassr\\Api\\Talabat solution\\Talabat.Repository\\"
                                            , "Data\\Data Seeding",
                                            "brands.json");
                if (File.Exists(filepath))
                {
                    connction = File.ReadAllText(filepath);
                }
                else
                {
                    Console.WriteLine("file products.json doen't exists");
                }
                var desiralization = new List<ProductBrand>();

                try
                {
                    desiralization = JsonSerializer.Deserialize<List<ProductBrand>>(connction);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing products.json: {ex.Message}");
                    return;
                }

                if (desiralization is not null)
                {
                    foreach (var item in desiralization)
                        storeDb.Set<ProductBrand>().Add(item);

                    try
                    {
                        await storeDb.SaveChangesAsync();
                        Console.WriteLine("Data seeding completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving changes to the database: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("file is empty.");
                }
            }
            if (storeDb.Set<ProductCategory>().Count() == 0)
            {
                var connction = string.Empty;
                var filepath = Path.Combine(/*Directory.GetCurrentDirectory()*/"D:\\.net\\.net\\Ahmed Nassr\\Api\\Talabat solution\\Talabat.Repository\\"
                                            , "Data\\Data Seeding",
                                            "categories.json");
                if (File.Exists(filepath))
                {
                    connction = File.ReadAllText(filepath);
                }
                else
                {
                    Console.WriteLine("file products.json doen't exists");
                }
                var desiralization = new List<ProductCategory>();

                try
                {
                    desiralization = JsonSerializer.Deserialize<List<ProductCategory>>(connction);

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deserializing products.json: {ex.Message}");
                    return;
                }

                if (desiralization is not null)
                {
                    foreach (var item in desiralization)
                        storeDb.Set<ProductCategory>().Add(item);

                    try
                    {
                        await storeDb.SaveChangesAsync();
                        Console.WriteLine("Data seeding completed successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error saving changes to the database: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("file is empty.");
                }
            }
        }

        public static async Task IdentityDataSeeding(UserManager<AppUser> _UserManger)
        {
            if(_UserManger.Users.Count() ==0)
            {
                var user = new AppUser()
                {
                    DisplayName = "BebooTarek",
                    Email = "Beboo555@yahoo.com",
                    UserName ="beboo555",
                    PhoneNumber = "1234567890",

                };


                await _UserManger.CreateAsync(user,"P@ssw0rd555");
            }




        }



    }
}
