using System.Collections.Generic;
using System.Linq;
using CodingTheoryConvolutionalCode.Helpers;

namespace CodingTheoryConvolutionalCode.Coding
{
    public class Encoder
    {
        private readonly Memory _memory;

        public Encoder()
        {
            _memory = new Memory();
        }

        //Priimami parametrai - vektorius, kurį reikia užkoduoti
        //Grąžinamas rezultatas - užkoduotas vektorius
        //
        //Funkcija užkoduoja vektorių pagal sąsūkos kodą. Pradžioje prie gauto vektoriaus prirašomi 6 nuliniai bitai,
        //tokiu būdu kodavimas bus baigtas kai į koduotoją pateks visi gauto vektoriaus bitai ir 6-i prirašyti bitai.
        //Toliau, kol pateiktas vektorius nėra tuščias, iš gauto vektoriaus pradžios imamas bitas. Jis pats įrašomas į
        //užkoduotą vektorių, tuomet jis pateikiamas į kodavimo schemą, iš kurios išėjęs bitas irgi įrašomas į užkoduotą vektorių.
        //Įrašius 2 bitus į užkoduotą vektorių, naudotas bitas yra įstumiamas į koduotojo atmintį.
        public Queue<int> Encode(Queue<int> vector)
        {
            var encodedBits = new Queue<int>();
            AddZeroBitsToSourceBits(vector);
            while (vector.Any())
            {
                var bitToProcess = vector.Dequeue();
                encodedBits.Enqueue(bitToProcess);
                encodedBits.Enqueue(EncodeBit(bitToProcess));

                _memory.PushBit(bitToProcess);
            }
            return encodedBits;
        }

        //Priimami parametrai - iš vektoriaus atėjęs bitas, kurį reikia panaudoti kodavimo schemoje
        //Gražinamas rezultatas - iš kodavimo schemos išėjęs bitas
        //
        //Funkcija gauna bitą į užkoduotą vektorių pagal kodavimo schemą. Gautas bitas yra sudedamas su
        //atminties 2-u, 5-u ir 6-u bitu ir grąžinama suma moduliu 2, kuri ir yra užkoduotas bitas, eisiantis į
        //užkoduotą vektorių.
        private int EncodeBit(int bit)
        {
            var sum = bit + _memory.GetBit(2) + _memory.GetBit(5) + _memory.GetBit(6);
            return sum%2;
        }

        //Priimami parametrai - pradinis, neužkoduotas vektorius
        //Grąžinamas rezultatas - vektorius, paruoštas kodavimui.
        //
        //Funkcija prie pradinio vektoriaus galo prirašo 6-is nulinius bitus.
        private void AddZeroBitsToSourceBits(Queue<int> vector)
        {
            for (var i = 0; i < 6; i++)
            {
                vector.Enqueue(0);
            }
        }
    }
}
