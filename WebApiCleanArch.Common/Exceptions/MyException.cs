using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiCleanArch.Common.Exceptions
{
    public  class MyException
    {

        public static void NotNull<T>(T obj, string name, string message = null) where T : class
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }
        public static void NotNull<T>(T? obj, string name, string message = null) where T : struct
        {
            if (obj is null)
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }

        public static void NotNull<T>(IEnumerable<T> obj, string name, string message = null) where T : class
        {
            if (obj is null || !obj.Any())
                throw new ArgumentNullException($"{name} : {typeof(T)}", message);
        }
    }
}
