namespace WindowsFormsApplication1.Extensions
{
    using System;
    using System.Collections.Generic;

    static class Extensions
    {
        //public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        //{
        //    return listToClone.Select(item => (T)item.Clone()).ToList();
        //}

        public static bool Contains(string[] arr, string item)
        {
            foreach (string i in arr)
            {
                if (i.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
