public class Program
{
    public static object Lookup(Dictionary<string, object> obj, string path)
    {
        var keys = path.Split('.');
        object curr = obj;

        foreach (var key in keys)
        {
            if (curr is Dictionary<string, object> dict && dict.ContainsKey(key))
            {
                curr = dict[key];
            }
            else
            {
                throw new Exception("Path not found.");
            }
        }
        if (curr is Dictionary<string, object>)
        {
            throw new Exception("This path returns an object, please go one level deeper.");
        }
        else
        {
            return curr;
        }
    }

    public static void Main()
    {
        var objectData = new Dictionary<string, object>
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

        string path = "prop1.prop2.prop3";
        var result = Lookup(objectData, path);
        Console.WriteLine(result);
    }
}
