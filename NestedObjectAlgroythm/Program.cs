public class Program
{
    public static object Lookup(Dictionary<string, object> obj, string path)
    {
        //find keys
        var keys = path.Split('.');
        object curr = obj;

        //iterate through object, if object is a dictionary and contains key, set the current object to that key, if not - no path
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

        //if the current object at the end is still an object, throw Exception, else return the value
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
