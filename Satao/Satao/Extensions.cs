using System.Reflection;

namespace Satao
{
    public static class Extensions
    {
        public static T DeepClone<T>(this T obj)
        {
            if (obj == null) return obj;
            var type = typeof(T);
            if (type.GetTypeInfo().IsPrimitive || type == typeof(string))
                return obj;
            AutoMapper.Mapper.Initialize(cfg => cfg.CreateMap<T, T>());
            return AutoMapper.Mapper.Map<T, T>(obj);
        }
    }
}
