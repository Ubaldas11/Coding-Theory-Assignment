using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingTheoryConvolutionalCode.Helpers
{
    public static class ConvertingHelper
    {
        //Priimami parametrai - vektorius
        //Grąžinamas rezultatas - paduoto vektoriaus kopija
        //
        //Funkcija grąžina paduoto vektoriaus kopiją
        public static Queue<int> CopyVector(Queue<int> vector)
        {
            var copiedVector = new Queue<int>();
            vector.ToList().ForEach(x =>
            {
                copiedVector.Enqueue(x);
            });
            return copiedVector;
        }

        //Priimami parametrai - vektorius (sąrašas)
        //Grąžinamas rezultatas - vektorius (eilė)
        //
        //Funkcija paverčia vektorių iš sąrašo į eilę
        public static Queue<int> ConvertListToQueue(List<int> vector)
        {
            var queue = new Queue<int>();
            foreach (var bit in vector)
            {
                queue.Enqueue(bit);
            }
            return queue;
        }

        //Priimami parametrai - vektorius (simbolių(char) masyvas)
        //Grąžinamas rezultatas - vektorius (eilė)
        //
        //Funkcija paverčia vektorių iš simbolių masyvo į eilę
        public static Queue<int> ConvertListToQueue(char[] vector)
        {
            var queue = new Queue<int>();
            foreach (var charBit in vector)
            {
                var numericValue = (int) char.GetNumericValue(charBit);
                if (numericValue == 0 || numericValue == 1)
                {
                    queue.Enqueue((int) char.GetNumericValue(charBit));
                }
                else
                {
                    throw new Exception();
                }
            }
            return queue;
        }

        //Priimami parametrai - vektorius, versiamas į tekstą
        //Grąžinamas rezultatas - tekstas, išgautas iš vektoriaus
        //
        //Funkcija paverčia paduotą vektorių į tekstą
        public static string ConvertVectorToText(Queue<int> vector)
        {
            var sb = new StringBuilder();
            foreach (var bit in vector)
            {
                sb.Append(bit);
            }
            var text = GetTextFromBinaryString(sb.ToString());
            return text;
        }

        //Priimami parametrai - baitų masyvas
        //Grąžinamas rezultatas - vektorius (eilė)
        //
        //Funkcija paverčia baitų masyvą į vektorių (bitų eilę)
        public static Queue<int> ConvertBytesToIntQueue(byte[] bytes)
        {
            var queue = new Queue<int>();
            foreach (var b in bytes)
            {
                for (var i = 0; i < 8; i++)
                {
                    queue.Enqueue((b & (128 >> i)) == 0 ? 0 : 1);
                }
            }
            
            return queue;
        }

        //Priimami parametrai - vektorius (bitų eilė)
        //Grąžinamas rezultatas - baitų masyvas
        //
        //Funkciją paverčia bitų eilę į bitų masyvą
        public static byte[] ConvertIntQueueToByteArray(Queue<int> vector)
        {
            var byteArray = new byte[vector.Count / 8];
            var position = 0;
            while (vector.Any())
            {
                var numberToByte = 0;
                for (var i = 7; i >= 0; i--)
                {
                    if (!vector.Any())
                    {
                        break;
                    }
                    var bit = vector.Dequeue();
                    numberToByte += (int)(bit * Math.Pow(2, i));
                }
                var b = (byte)numberToByte;
                byteArray[position] = b;
                position++;
            }
            return byteArray;
        }

        //Priimami parametrai - eilutė iš binarinių simbolių
        //Grąžinamas rezultatas - dekoduotas tekstas
        //
        //Funkcija paverčia eilutę iš binarinių simbolių į ASCII koduotės tekstą
        private static string GetTextFromBinaryString(string binaryString)
        {
            return Encoding.ASCII.GetString(GetBytesFromBinaryString(binaryString));
        }

        //Priimami parametrai - eilutė iš binarinių simbolių
        //Grąžinamas rezultatas - baitų masyvas
        //
        //Funkcija paverčia eilutę iš binarinių simbolių į baitų masyvą
        private static byte[] GetBytesFromBinaryString(string binaryString)
        {
            var list = new List<byte>();

            for (var i = 0; i < binaryString.Length; i += 8)
            {
                var t = binaryString.Substring(i, 8);
                list.Add(Convert.ToByte(t, 2));
            }
            return list.ToArray();
        }
    }
}
