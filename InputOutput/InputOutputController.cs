using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodingTheoryConvolutionalCode.Coding;
using CodingTheoryConvolutionalCode.Helpers;

namespace CodingTheoryConvolutionalCode.InputOutput
{
    public static class InputOutputController
    {
        //Nepriima parametrų
        //Grąžina pasirinktą scenarijų
        //
        //Funkcija paprašo vartotojo pasirinkti norimą scenarijų ir grąžina pasirinktą scenarijų
        public static Scenario ChooseScenario()
        {
            Console.WriteLine("Choose your wanted scenario: " + Environment.NewLine +
                "1 - Single vector" + Environment.NewLine +
                "2 - Text" + Environment.NewLine + 
                "3 - Picture" + Environment.NewLine + 
                "4 - Exit" + Environment.NewLine);
            var input = Console.ReadLine();
            var choice = int.Parse(input);
            return (Scenario) choice;
        }
        //Nieko nepriima
        //Grąžinamas rezultatas - vektorius
        //
        //Funkcija paprašo vartotojo įvesti vektorių ir grąžina jį kaip bitų eilę
        public static Queue<int> InputVector()
        {
            Console.WriteLine("Input the vector (made of 0s and 1s) to encode and press enter:" + Environment.NewLine +
                "Example: 1010");
            var input = Console.ReadLine();
            var charBitArray = input.ToCharArray();
            var vector = ConvertingHelper.ConvertListToQueue(charBitArray);
            return vector;
        }

        //Nieko nepriima
        //Grąžinamas rezultatas - klaidos tikimybė
        //
        //Funkcija paprašo vartotojo įvesti klaidos tikimybę ir grąžina ją
        public static double InputErrorChance()
        {
            Console.WriteLine("Enter the chance of an error (dot as delimiter) and press enter." + Environment.NewLine +
                              "Example of input: 0.123");
            double errorChance;
            if (!double.TryParse(Console.ReadLine(), out errorChance))
            {
                throw new Exception();
            };
            return errorChance;
        }

        //Priimami parametrai - vektorius
        //Grąžinamas rezultatas - vektorius ir vėliava (flag), ar vektorius buvo pakeistas
        //
        //Funkcija paklausia vartotojo, ar jis norės pakeisti vektoriuje klaidas. Jei vartotojas nori,
        //tuomet klaidos pakeičiamos ir grąžinamas pakeistas vektorius su vėliava, kad vektorius buvo keistas.
        //Jei vartotojas nenori, grąžinamas paduotas vektorius ir vėliava, kad vektorius nekeistas.
        public static Queue<int> ChangeErrors(Queue<int> vector, out bool changed)
        {
            Console.WriteLine("Do you want to change made errors?" + Environment.NewLine +
                "1 - Yes" + Environment.NewLine +
                "2 - No");
            var input = Console.ReadLine();
            var choice = int.Parse(input);
            if (choice == 2)
            {
                changed = false;
                return vector;
            }
            else
            {
                changed = true;
                Console.Write("Enter at what positions bits should be changed (separate by semicolon)" + Environment.NewLine + 
                    "Example to change bits at 1, 5 and 6th positions: 1;5;6" + Environment.NewLine);
                input = Console.ReadLine();
                var errorPositionStringList = input.Split(';');
                var errorPositionList = errorPositionStringList.Select(positionString => int.Parse(positionString)).ToList();
                var vectorAsList = ErrorController.ChangeErrors(vector.ToList(), errorPositionList);
                var changedVector = ConvertingHelper.ConvertListToQueue(vectorAsList);
                return changedVector;
            }

        }

        //Nieko nepriima
        //Grąžinamas rezultatas - vektorius
        //
        //Funkcija paprašo vartotojo įvesti tekstą, jį paverčia į bitų eilę ir grąžina
        public static Queue<int> InputText()
        {
            Console.WriteLine("Input the text to encode and press enter.");
            var input = Console.ReadLine();
            var sb = new StringBuilder();
            foreach (var c in input)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            var bitCharArray = sb.ToString().ToCharArray();
            var preparedVector = ConvertingHelper.ConvertListToQueue(bitCharArray);
            return preparedVector;
        }

        //Priimami parametrai - klaidų pozicijų sąrašas
        //Nieko negrąžina
        //
        //Funkcija išspausdina, kiek buvo padaryta klaidų ir kokiose vietose jos buvo padarytos
        public static void PrintInfoAboutErrors(List<int> errorList)
        {
            Console.WriteLine("Errors made: " + errorList.Count);
            Console.Write("At positions: ");
            errorList.ForEach(x => Console.Write(x + "; "));
            Console.WriteLine(Environment.NewLine);
        }

        //Priimami parametrai - vektorius, vektoriaus pavadinimas ir vėliava, nurodanti, ar reikia tarpų tarp vektorius bitų
        //Nieko negrąžina
        //
        //Funkcija išspausdina vektoriaus pavadinimą ir patį vektorių
        public static void PrintVector(Queue<int> vector, string name, bool spacesNeeded)
        {
            Console.WriteLine(name);
            int counter = 0;
            foreach (var bit in vector)
            {
                counter++;
                Console.Write(bit);
                if (counter == 2 && spacesNeeded)
                {
                    counter = 0;
                    Console.Write(" ");
                }
            }
            Console.WriteLine(Environment.NewLine);
        }

        //Priimami parametrai - teksto antraštė, tekstas
        //Nieko negrąžina
        //
        //Funkcija išspausdiną tekstą su jam priskirta antrašte
        public static void PrintText(string header, string text)
        {
            Console.WriteLine(header + Environment.NewLine + text + Environment.NewLine);
        }

        //Nieko nepriima
        //Nieko negrąžina
        //
        //Funkcija informuoja vartotoją apie blogą įvestį ir prašo vartotojo vesti dar kartą
        public static void InformAboutWrongInput()
        {
            Console.WriteLine(Environment.NewLine + "Wrong input. Please follow the given instructions and try wanted scenario again.");
        }

        //Nieko nepriima
        //Grąžinamas rezultatas - vektorius
        //
        //Funkcija paprašo vartotojo įvesti paveiksliuko pavadinimą, nuskaito paveiksliuką kaip baitus
        //paverčia juos į vektorių (bitų eilę) ir grąžina
        public static Queue<int> ReadImageFile()
        {
            Console.WriteLine("Only .jpg files are supported." + Environment.NewLine +
                "Enter name of the image file (with extension):");
            var fileName = Console.ReadLine();
            var bytes = FileController.ReadFileBytes(fileName);
            var queue = ConvertingHelper.ConvertBytesToIntQueue(bytes);
            return queue;
        }

        //Priimami parametrai - vektorius, paveiksliuko pavadinimas
        //Nieko negrąžina
        //
        //Funkcija bando išsaugoti vektorių kaip paveiksliuką ir informuoja vartotoją apie tai, ar saugojimas pavyko.
        //Jei saugojimas pavyko, nurodo vartotojui, kokiu vardu galima rasti išsaugotą paveiksliuko failą.
        //Jei nepavyko, informuoja apie tai.
        public static void SaveImage(Queue<int> vector, string imageName)
        {
            var byteArray = ConvertingHelper.ConvertIntQueueToByteArray(vector);
            Console.WriteLine();
            try
            {
                FileController.SaveImageFile(byteArray, imageName);
                Console.WriteLine("Successfully saved." + Environment.NewLine +
                    "Image can be found at project folder with the name '" + imageName + ".jpg'");
            }
            catch (Exception)
            {
                Console.WriteLine("Saving failed.");
            }
        }
    }
}
