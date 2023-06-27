namespace CatalogTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //    private int _userId;
            //private string _name;
            //private string _userName;
            //private string _password;
            //private string _phone;
            //private string _email;
            //private bool _admin;
            User Test = new User(0, "test", "Tester", "123141245", "1321414124", "Test@Test.dk", true);
            UserCatalog ul = new UserCatalog("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=VagtDB23;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            int before = ul.GetAllUsers().Result.Count;
            ul.CreateUser(Test);
            int after = ul.GetAllUsers().Result.Count;

            Assert.IsTrue(after == before + 1);
        }
    }
}