using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomerService.Tests
{
    [TestClass]
    public class AddingTests
    {
        [TestMethod]
        public void Create_Single_Customer_In_Memory()
        {
            SqliteConnection connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<CustomerContext>()
                //.UseInMemoryDatabase(databaseName: "TT100")
                .UseSqlite(connection)
                .Options;

            using (var context = new CustomerContext(options))
            {
                context.Database.EnsureCreated();
                var service = new AddingService(context);
                var nadal = new Customer
                {
                    Firstname = "Dimitrij",
                    Lastname = "OVTCHAROV",
                    Title = "Mr"
                };
                service.CreateCustomer(nadal);
            }

            using (var context = new CustomerContext(options))
            {
                Assert.AreEqual(1, context.Customers.Count());
                var added = context.Customers.Single();
                Assert.AreEqual("Dimitrij", added.Firstname);
                Assert.AreEqual("OVTCHAROV", added.Lastname);
                Assert.AreEqual("Mr", added.Title);
            }
        }

        [TestMethod]
        public void Find_Customers_By_Lastname()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "TT50")
                .Options;

            using (var context = new CustomerContext(options))
            {
                context.Customers.Add(new Customer { Firstname = "Kim Hing", Lastname = "Yong", Title = "Mr" });
                context.Customers.Add(new Customer { Firstname = "Burak Selim", Lastname = "Yong", Title = "Mr" });
                context.Customers.Add(new Customer { Firstname = "Su Han", Lastname = "Yong", Title = "Ms" });
                context.Customers.Add(new Customer { Firstname = "Kim Hing", Lastname = "Yang", Title = "Mr" });
                context.Customers.Add(new Customer { Firstname = "Koki", Lastname = "Niwa", Title = "Ms" });
                context.Customers.Add(new Customer { Firstname = "Fun Sun", Lastname = "Kim", Title = "Ms" });
                context.SaveChanges();
            }

            using (var context = new CustomerContext(options))
            {
                var service = new AddingService(context);
                var result = service.FindByLastname("Yong");
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void Update_Single_Customer()
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(databaseName: "TT50")
                .Options;
            var id = 0;
            using (var context = new CustomerContext(options))
            {
                var service = new AddingService(context);

                var kimHing = service.CreateCustomer(new Customer { Firstname = "Kim Hing", Lastname = "Yong", Title = "Mr" });
                context.SaveChanges();
                id = kimHing.CustomerID;

                service.UpdateCustomer(new Customer
                {
                    CustomerID = id,
                    Firstname = "Kim Kim",
                    Lastname = "Yong",
                    Title = "Mr"
                });
            }

            using (var context = new CustomerContext(options))
            {
                var service = new AddingService(context);
                var founded = service.FindById(id);
                Assert.AreEqual("Kim Kim", founded.Firstname);
            }
        }
    }
}