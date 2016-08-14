namespace Satao
{
    public static class Extensions
    {
        public static T CloneUsingJson<T>(this T obj) where T : class
        {
            if (obj == null) return null;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }

    }
}
