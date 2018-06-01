using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Extentions
{
    public static class SquareArrayExtentions
    {
        #region Getters
        public static int Height<T>(this T[,] array)
        {
            return array.GetLength(0);
        }
        public static int Width<T>(this T[,] array)
        {
            return array.GetLength(1);
        }

        public static bool CanGetLeft<T>(this T[,] array, int x)
        {
            return x > 0;
        }
        public static bool CanGetRight<T>(this T[,] array, int x)
        {
            return x < array.Width() - 1;
        }
        public static bool CanGetAbove<T>(this T[,] array, int y)
        {
            return y > 0;
        }
        public static bool CanGetBelow<T>(this T[,] array, int y)
        {
            return y < array.Height() - 1;
        }

        public static T GetAt<T>(this T[,] array, int x, int y)
        {
            return array[y, x];
        }
        public static T GetLeftOf<T>(this T[,] array, int x, int y)
        {
            return array[y, x - 1];
        }
        public static T GetRightOf<T>(this T[,] array, int x, int y)
        {
            return array[y, x + 1];
        }
        public static T GetAboveOf<T>(this T[,] array, int x, int y)
        {
            return array[y - 1, x];
        }
        public static T GetBelowOf<T>(this T[,] array, int x, int y)
        {
            return array[y + 1, x];
        }

        #endregion

        #region Setters

        public static void SetAt<T>(this T[,] array, int x, int y, T t)
        {
            array[y, x] = t;
        }
        public static void SetLeftOf<T>(this T[,] array, int x, int y, T t)
        {
            array[y, x - 1] = t;
        }
        public static void SetRightOf<T>(this T[,] array, int x, int y, T t)
        {
            array[y, x + 1] = t;
        }
        public static void SetAboveOf<T>(this T[,] array, int x, int y, T t)
        {
            array[y - 1, x] = t;
        }
        public static void SetBelowOf<T>(this T[,] array, int x, int y, T t)
        {
            array[y + 1, x] = t;
        }
        #endregion

        public static string ToASCIIString<T>(this T[,] array)
        {
            return array.ToASCIIString<T>(t => t.ToString());
        }

        public static string ToASCIIString<T>(this T[,] array, Func<T, string> transform)
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < array.Height(); y++)
            {
                sb.Append('[');
                for (int x = 0; x < array.Width(); x++)
                {
                    string transformed = transform(array.GetAt(x, y));
                    sb.Append(transformed);
                    sb.Append(' ');
                }
                sb.Append(']');
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
