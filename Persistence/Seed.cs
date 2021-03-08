using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(
            UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager, ApplicationDbContext context)
        {
            await AddAppUserRoles(roleManager);

            await AddAppUsers(userManager, context);

        }

        private static async Task AddAppUsers(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var users = new List<AppUser>
                {
                    new Owner
                    {
                        FirstName = "Abdulazeez",
                        UserName= "shrewdmensch",
                        LastName= "Bolarinwa"
                    },

                    new Customer
                    {
                        FirstName = "Adeola",
                        UserName = "adeola",
                        LastName= "Omolola",
                        Address = new Address
                        {
                            AddressLine= "10, Morakinyo Street",
                            City = "Alimosho",
                            State = "Lagos State",
                            Country = "Nigeria",
                            ZipCode = "100361"
                        }
                    },
                    new Customer
                    {
                        FirstName = "Grace",
                        UserName= "grace",
                        LastName= "Ikujuni",
                        Address = new Address
                        {
                            AddressLine= "Eket",
                            City = "Eket",
                            State = "Akwa Ibom State",
                            Country = "Nigeria",
                            ZipCode = "53001"
                        }
                    }
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "admin");

                    if (user.GetType() == typeof(Owner))
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Owner);
                    }

                    else
                    {
                        await userManager.AddToRoleAsync(user, UserRoles.Customer);
                    }
                }
                await AddPreloadedData(context);
            }
        }

        private static async Task AddAppUserRoles(RoleManager<AppUserRole> roleManager)
        {
            if (!await roleManager.Roles.AnyAsync())
            {
                List<AppUserRole> roles = new List<AppUserRole>
                {
                    new AppUserRole(UserRoles.Owner),
                    new AppUserRole(UserRoles.Customer)
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }

        private static async Task AddPreloadedData(ApplicationDbContext context)
        {
            await AddStandardSizes(context);

            await AddOperatingExpenses(context);

            DailyRate dailyRate = new DailyRate { Cost = 5000 };
            await context.AddAsync(dailyRate);

            await AddTypeOfClothes(context);

            await context.SaveChangesAsync();
        }

        private static void AddAccessoriesToTypeOfClothes(List<Accessory> accessories, List<TypeOfCloth> typeOfCloths)
        {
            typeOfCloths[0].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[1].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[2].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[3].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[4].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[5].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[6].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[7].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[8].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 8},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[9].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 8},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[5], Quantity= 3}
                };
            typeOfCloths[10].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
            typeOfCloths[11].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 5},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3}
                };
            typeOfCloths[12].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[13].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 6},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[3], Quantity= 3},
                };

            typeOfCloths[14].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[15].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 2},
                };

            typeOfCloths[16].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 1},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 5},
                    new TypeOfClothAccessory{ Accessory = accessories[2], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[17].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 3},
                    new TypeOfClothAccessory{ Accessory = accessories[6], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };

            typeOfCloths[18].Accessories = new List<TypeOfClothAccessory>
                {
                    new TypeOfClothAccessory{ Accessory = accessories[0], Quantity= 2},
                    new TypeOfClothAccessory{ Accessory = accessories[1], Quantity= 4},
                    new TypeOfClothAccessory{ Accessory = accessories[4], Quantity= 3},
                };
        }

        private static void AddMeasurementHeadersToTypeOfClothes(List<MeasurementHeader> measurementHeaders, List<TypeOfCloth> typeOfCloths)
        {
            foreach (var typeOfCloth in typeOfCloths)
            {
                typeOfCloth.MeasurementHeaders = new List<TypeOfClothMeasurementHeader>();

                foreach (var measurementHeader in measurementHeaders)
                {
                    typeOfCloth.MeasurementHeaders
                        .Add(new TypeOfClothMeasurementHeader { MeasurementHeader = measurementHeader });
                }
            }
        }

        private static void AddIncurredExpensesToTypeOfClothes(List<IncurredExpense> incurredExpenses, List<TypeOfCloth> typeOfCloths)
        {
            typeOfCloths[0].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[1].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[2].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                   new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[3].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[4].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[5].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[6].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[7].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[8].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };
            typeOfCloths[9].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[1]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[3]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]}
                };
            typeOfCloths[10].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                   new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]},
                   new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[2]},
                   new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]}
                };
            typeOfCloths[11].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]}
                };
            typeOfCloths[12].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[3]}
                };

            typeOfCloths[13].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[1]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[3]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[14].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[15].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[16].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]}
                };

            typeOfCloths[17].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]}
                };

            typeOfCloths[18].IncurredExpenses = new List<TypeOfClothIncurredExpense>
                {
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[0]},
                    new TypeOfClothIncurredExpense{ IncurredExpense = incurredExpenses[4]}
                };
        }


        private static async Task AddTypeOfClothes(ApplicationDbContext context)
        {
            List<TypeOfCloth> typeOfCloths = new List<TypeOfCloth>
                {
                    new TypeOfCloth{ Name = "Simple Top",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Peplum Top",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Jacket",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Simple Skirt",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Mermaid Skirt",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Simple Dress With Lining",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Simple Dress",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Semi-Party Dress",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Party Dress",ProductionDays= 3.00 },
                    new TypeOfCloth{ Name = "Party Dress With Assessories",ProductionDays= 3.00 },
                    new TypeOfCloth{ Name = "Trouser and Top",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Jumpsuit",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Skirt and Blouse",ProductionDays= 3.00 },
                    new TypeOfCloth{ Name = "Skirt & Blouse With Assessories",ProductionDays= 3.00 },
                    new TypeOfCloth{ Name = "Trouser",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Shorts",ProductionDays= 1.00 },
                    new TypeOfCloth{ Name = "Blouse and Wrapper",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Shirt Dress",ProductionDays= 2.00 },
                    new TypeOfCloth{ Name = "Shorts and Top Set",ProductionDays= 2.00 },
                };

            await context.AddRangeAsync(typeOfCloths);

            AddMeasurementHeadersToTypeOfClothes(await CreateMeasurementHeaders(context), typeOfCloths);

            AddAccessoriesToTypeOfClothes(await AddAccessories(context), typeOfCloths);
            AddIncurredExpensesToTypeOfClothes(await AddIncurredExpenses(context), typeOfCloths);
        }

        private static async Task AddOperatingExpenses(ApplicationDbContext context)
        {
            List<OperatingExpense> operatingExpensePerDays = new List<OperatingExpense>
                {
                    new OperatingExpense{  Name = "Fuel Cost", TotalCost= 5000, Type= OperatingExpenseType.Daily },
                    new OperatingExpense{  Name = "Internet/Data", TotalCost= 20000, Type= OperatingExpenseType.Monthly },
                    new OperatingExpense{  Name = "Phone Bill Allocation", TotalCost= 10000, Type= OperatingExpenseType.Monthly },
                    new OperatingExpense{  Name = "DSTV/Netflix", TotalCost= 2000, Type= OperatingExpenseType.Monthly },
                    new OperatingExpense{  Name = "Instagram and Adverts", TotalCost= 24000, Type= OperatingExpenseType.Monthly }
                };

            await context.AddRangeAsync(operatingExpensePerDays);
        }

        private static async Task<List<IncurredExpense>> AddIncurredExpenses(ApplicationDbContext context)
        {
            List<IncurredExpense> incurredExpenses = new List<IncurredExpense>
                {
                    new IncurredExpense{  Name = "Dispatch Rider", Cost= 2000 },
                    new IncurredExpense{  Name = "Beading", Cost= 3000 },
                    new IncurredExpense{  Name = "Dry Cleaning", Cost= 1000 },
                    new IncurredExpense{  Name = "Transportation to client's", Cost= 3000 },
                    new IncurredExpense{  Name = "Token Entertainment", Cost= 1300 },
                    new IncurredExpense{  Name = "Inconvenient Cost 6", Cost= 10000 },
                    new IncurredExpense{  Name = "Inconvenient Cost 7", Cost= 1500 },
                };

            await context.AddRangeAsync(incurredExpenses);

            return incurredExpenses;
        }

        private static async Task<List<Accessory>> AddAccessories(ApplicationDbContext context)
        {
            List<Accessory> accessories = new List<Accessory>
                {
                    new Accessory{Name = "Zip", Cost=200},
                    new Accessory{Name = "Fabric", Cost=1500},
                    new Accessory{Name = "Linning", Cost=150},
                    new Accessory{Name = "Trimming", Cost=2000},
                    new Accessory{Name = "Thread", Cost=100},
                    new Accessory{Name = "Silk Linning", Cost=400},
                    new Accessory{Name = "Button", Cost=700},
                };

            await context.AddRangeAsync(accessories);

            return accessories;
        }

        private static async Task AddStandardSizes(ApplicationDbContext context)
        {
            List<StandardSize> standardSizes = new List<StandardSize>
                {
                    new StandardSize{Name = "UK 10"},
                    new StandardSize{Name = "USA 10"},
                    new StandardSize{Name = "UK 11"},
                    new StandardSize{Name = "USA 11"},
                    new StandardSize{Name = "UK 12"},
                    new StandardSize{Name = "USA 12"},
                    new StandardSize{Name = "UK 13"},
                    new StandardSize{Name = "USA 13"},
                    new StandardSize{Name = "UK 14"},
                    new StandardSize{Name = "USA 14"},
                    new StandardSize{Name = "UK 15"},
                    new StandardSize{Name = "USA 15"},
                    new StandardSize{Name = "UK 16"},
                    new StandardSize{Name = "USA 16"},
                    new StandardSize{Name = "UK 17"},
                    new StandardSize{Name = "USA 17"},
                    new StandardSize{Name = "UK 18"},
                    new StandardSize{Name = "USA 18"},
                    new StandardSize{Name = "UK 19"},
                    new StandardSize{Name = "USA 19"},
                    new StandardSize{Name = "UK 20"},
                    new StandardSize{Name = "USA 20"},
                    new StandardSize{Name = "UK 21"},
                    new StandardSize{Name = "USA 21"},
                    new StandardSize{Name = "UK 22"},
                    new StandardSize{Name = "USA 22"}
                };

            await context.AddRangeAsync(standardSizes);
        }

        private static async Task<List<MeasurementHeader>> CreateMeasurementHeaders(ApplicationDbContext context)
        {
            List<MeasurementHeader> measurementHeaders = new List<MeasurementHeader>
                {
                    new MeasurementHeader
                    {
                        Name = "Waist"
                    },
                    new MeasurementHeader
                    {
                        Name = "Neck"
                    },
                    new MeasurementHeader
                    {
                        Name = "Bust"
                    },
                    new MeasurementHeader
                    {
                        Name = "Back"
                    },
                    new MeasurementHeader
                    {
                        Name = "Side"
                    },new MeasurementHeader
                    {
                        Name = "Head room"
                    },
                };

            await context.AddRangeAsync(measurementHeaders);
            return measurementHeaders;
        }
    }
}