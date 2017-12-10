using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace CodingTheoryConvolutionalCode.InputOutput
{
    public static class FileController
    {
        private static readonly string FolderPath;

        //Inicializuoja kelią iki direktorijos, kurioje bus saugomi paveiksliukai
        static FileController()
        {
            FolderPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        }

        //Priimami parametrai - failo pavadinimas
        //Grąžinamas rezultatas - failo turinys baitais
        //
        //Funkcija perskaito failą kaip baitus ir grąžina baitų masyvą
        public static byte[] ReadFileBytes(string fileName)
        {
            return File.ReadAllBytes(FolderPath + "/" + fileName);
        }

        //Priimami parametrai - baitų masyvas, paveiksliuko pavadinimas
        //Nieko negrąžina
        //
        //Funkcija išsaugo baitų masyvą kaip paveiksliuką su nurodytu vardu
        public static void SaveImageFile(byte[] bytes, string imageName)
        {
            File.WriteAllBytes(FolderPath + "/" + imageName + ".jpg", bytes);
        }
    }
}
