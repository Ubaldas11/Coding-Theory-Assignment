using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTheoryConvolutionalCode.Coding
{
    public static class ErrorController
    {
        //Priimami parametrai - norimas keisti vektorius, klaidų pozicijų sąrašas.
        //Grąžinamas rezultatas - vektorius su pakeistom klaidom.
        //
        //Funkcija pakeičia vektoriaus bitus tose vietose, kurios yra nurodytos paduotame klaidų pozicijų sąraše
        //ir grąžina vektorių su norimom klaidom norimose vietose.
        public static List<int> ChangeErrors(List<int> vector, List<int> errorPositionList)
        {
            for (var i = 0; i < vector.Count; i++)
            {
                foreach (var pos in errorPositionList)
                {
                    if (i == pos-1)
                    {
                        vector[i] = vector[i] ^ 1;
                    }
                }
            }
            return vector;
        }
    }
}
