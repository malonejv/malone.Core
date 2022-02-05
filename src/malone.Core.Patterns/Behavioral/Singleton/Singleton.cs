using System;
using System.Reflection;
using System.Threading;

namespace malone.Core.Patterns.Behavioral.Singleton
{
    public class Singleton<T> where T : class
    {
        private static readonly Lazy<T> instance = new Lazy<T>(() =>
        {
            var ctors =
                typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public |
                               BindingFlags.Static);

            if (!Array.Exists(ctors, ci => ci.GetParameters().Length == 0))
            {
                throw new SingletonConstructorNotFoundException("Non-public ctor() note found.");
            }

            var ctor = Array.Find(ctors, ci => ci.GetParameters().Length == 0);

            return ctor.Invoke(new object[] { }) as T;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        public static T Instance
        {
            get { return instance.Value; }
        }
    }
}
