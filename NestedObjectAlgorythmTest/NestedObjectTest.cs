namespace NestedObjectAlgorythmTest
{
    public class NestedObjectAlgorythmTests
    {
        private Dictionary<string, object> _objectData;

        public NestedObjectAlgorythmTests()
        {
             _objectData = new Dictionary<string, object>
        {
            {
                "prop1", new Dictionary<string, object>
                {
                    { "prop2", new Dictionary<string, object>
                {
                    { "prop3", "Apple" },
                    { "prop4", "Orange" }
                }},
                    { "prop5", "Pear" }
                }
            }
        };
        }

        [Fact]
        public void Lookup_Valid_ReturnsValue()
        {
            string path = "prop1.prop2.prop3";
            var result = Program.Lookup(_objectData, path);
            Assert.Equal("Apple", result);

            string path2 = "prop1.prop5";
            var result2 = Program.Lookup(_objectData, path2);
            Assert.Equal("Pear", result2);
        }

        [Fact]
        public void Lookup_InvalidPath_ThrowsException()
        {
            string path = "prop1.prop4";
            var exception = Assert.Throws<Exception>(() => Program.Lookup(_objectData, path));
            Assert.Equal("Path not found.", exception.Message);

            string path2 = "prop1.notfound";
            var exception2 = Assert.Throws<Exception>(() => Program.Lookup(_objectData, path2));
            Assert.Equal("Path not found.", exception2.Message);

            string path3 = "prop1.";
            var exception3 = Assert.Throws<Exception>(() => Program.Lookup(_objectData, path3));
            Assert.Equal("Path not found.", exception3.Message);
        }

        [Fact]
        public void Lookup_PathIsObject_ThrowsException()
        {
            string path = "prop1.prop2";
            var exception = Assert.Throws<Exception>(() => Program.Lookup(_objectData, path));
            Assert.Equal("This path returns an object, please go one level deeper.", exception.Message);
        }

        [Fact]
        public void Lookup_Empty_ThrowsException()
        {
            string path = "";
            var exception = Assert.Throws<Exception>(() => Program.Lookup(_objectData, path));
            Assert.Equal("Path not found.", exception.Message);
        }
    }
}