using Lab;
namespace Lab.LabTests
{
    [TestClass]
    public class ProgramTests
    {
        private const string TestFilePath = "data_test.txt";

        [TestInitialize]
        public void Setup()
        {
            File.WriteAllText(TestFilePath,
                "1 = testValue1\n" +
                "2 = testValue2\n" +
                "3 = testValue3\n" +
                "4 = testValue4\n" +
                "5 = testValue5");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(TestFilePath))
            {
                File.Delete(TestFilePath);
            }
        }

        [TestMethod]
        public void LoadData_ShouldLoadCorrectly()
        {
           
            var expectedData = new Dictionary<string, string>
            {
                { "1", "testValue1" },
                { "2", "testValue2" },
                { "3", "testValue3" },
                { "4", "testValue4" },
                { "5", "testValue5" }
            };

            var actualData = Program.LoadData(TestFilePath);

            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [TestMethod]
        public void SaveData_ShouldSaveCorrectly()
        {
            var dataToSave = new Dictionary<string, string>
            {
                { "6", "newValue1" },
                { "7", "newValue2" }
            };

            Program.SaveData(TestFilePath, dataToSave);
            var loadedData = Program.LoadData(TestFilePath);

            Assert.AreEqual("newValue1", loadedData["6"]);
            Assert.AreEqual("newValue2", loadedData["7"]);
        }

        [TestMethod]
        public void DisplayData_ShouldOutputCorrectly()
        {
            // Arrange
            var data = new Dictionary<string, string>
            {
                { "1", "testValue1" },
                { "2", "testValue2" },
                { "3", "testValue3" },
                { "4", "testValue4" },
                { "5", "testValue5" }
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            Program.DisplayData(data);

            string expectedOutput = "1 = testValue1\n2 = testValue2\n3 = testValue3\n4 = testValue4\n5 = testValue5\n";
            string actualOutput = sw.ToString();

            expectedOutput = expectedOutput.Replace("\r\n", "\n").Trim();
            actualOutput = actualOutput.Replace("\r\n", "\n").Trim();

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}
