using System;
using System.Text;

namespace Extensions
{
    public static class StringArrayExtensions
    {
        public static int CountHorizontalOcurrences(this string[] haystack, string needle)
        {
            int result = 0;
            foreach (var haystackItem in haystack)
            {
                result += haystackItem.Contains(needle) ? 1 : 0;
            }

            return result;
        }

        public static int CountVerticalOcurrences(this string[] haystack, string needle)
        {
            int result = 0;

            for (int i = 0; i < haystack.Length; i++)
            {
                StringBuilder sequenceToAnalyzeSB = new StringBuilder();
                for (int j = 0; j < haystack.Length; j++)
                {
                    sequenceToAnalyzeSB.Append(haystack[j][i]);
                }

                if (sequenceToAnalyzeSB.ToString().Contains(needle))
                {
                    result++;
                }
            }

            return result;
        }

        public static int CountDiagonalOcurrences(this string[] haystack, string needle)
        {
            int result = 0;
            ///La diferencia entre la dimension de la matriz y el largo de la secuencia
            ///me sirve para el rango en x  e y que tengo que recorrer de las diagonales
            ///Utilizo este rango calculado para evitar algunos bucles del for que serian innecesarios
            int lengthDifference = haystack.Length - needle.Length;

            ///Busco las ocurrencias en la diagonal inferior y diagonal central, de arriba para abajo
            for (int i = lengthDifference; i >= 0; i--)
            {
                StringBuilder sequenceToAnalyzeSB = new StringBuilder();
                for (int j = 0; j < haystack.Length - i; j++)
                {
                    sequenceToAnalyzeSB.Append(haystack[i + j][j]);
                }

                if (sequenceToAnalyzeSB.ToString().Contains(needle))
                {
                    result++;
                }
            }

            ///Busco las ocurrencias en la diagonal superior, de arriba para abajo
            for (int i = 1; i <= lengthDifference; i++)
            {
                StringBuilder sequenceToAnalyzeSB = new StringBuilder();
                for (int j = 0; j < haystack.Length - i; j++)
                {
                    sequenceToAnalyzeSB.Append(haystack[j][i + j]);
                }

                if (sequenceToAnalyzeSB.ToString().Contains(needle))
                {
                    result++;
                }
            }

            ///Busco las ocurrencias en la diagonal inferior y diagonal central, de abajo para arriba
            for (int i = lengthDifference + 1; i < haystack.Length; i++)
            {
                StringBuilder sequenceToAnalyzeSB = new StringBuilder();
                for (int j = 0; j <= i; j++)
                {
                    sequenceToAnalyzeSB.Append(haystack[i - j][j]);
                }

                if (sequenceToAnalyzeSB.ToString().Contains(needle))
                {
                    result++;
                }
            }

            ///Busco las ocurrencias en la diagonal superior, de abajo para arriba
            for (int i = 0; i < lengthDifference; i++)
            {
                StringBuilder sequenceToAnalyzeSB = new StringBuilder();

                for (int j = i + 1; j < haystack.Length; j++)
                {
                    sequenceToAnalyzeSB.Append(haystack[haystack.Length - j + i][j]);
                }

                if (sequenceToAnalyzeSB.ToString().Contains(needle))
                {
                    result++;
                }
            }

            return result;
        }

        public static bool IsSquare(this string[] haystack)
        {
            if (haystack == null || haystack.Length == 0)
            {
                return false;
            }
                
            foreach (var element in haystack)
            {
                if (haystack.Length != element.Length)
                {
                    return false;
                }   
            }

            return true;
        }
    }
}
