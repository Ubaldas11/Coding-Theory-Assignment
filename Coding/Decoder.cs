using System.Collections.Generic;
using System.Linq;

namespace CodingTheoryConvolutionalCode.Coding
{
    public class Decoder
    {
        private readonly Memory _upMemory;
        private readonly Memory _downMemory;

        public Decoder()
        {
            _upMemory = new Memory();
            _downMemory = new Memory();
        }

        //Priimami parametrai - neužkoduotas vektorius
        //Grąžinamas rezultatas - užkoduotas vektorius
        //
        //Funkcija dekoduoja gautą vektorių tiesioginio dekodavimo algoritmu. Pirmi du atsiųsto vektoriaus bitai
        //išimami, apskaičiuojama pirmoji moduliu du suma, apskaičiuojamas daugumos sprendimo bitas, apskaičiuojamas
        //dekoduotas bitas ir jis išeina iš dekodavimo schemos. Tuomet į dekoduotojo apatinę ir viršutinę atmintį yra įstumiami
        //atitinkami bitai.
        //Gale dekodavimo yra nukerpami pirmi 6 dekoduoti bitai, nes jie yra atminties būsena, o ne dekoduoto vektoriaus dalis
        public Queue<int> Decode(Queue<int> vector)
        {
            var decodedBits = new Queue<int>();
            while (vector.Any())
            {
                var firstBit = vector.Dequeue();
                var secondBit = vector.Dequeue();

                var bitAfterFirstSum = GetFirstSumResult(firstBit, secondBit);
                var majorityDecisionElement = GetMajorityDecisionElement(bitAfterFirstSum);
                var decodedBit = GetDecodedBit(majorityDecisionElement);
                decodedBits.Enqueue(decodedBit);

                _upMemory.PushBit(firstBit);
                _downMemory.PushBit(bitAfterFirstSum);
            }
            for (var i = 0; i < 6; i++)
            {
                decodedBits.Dequeue();
            }
            return decodedBits;
        }

        //Priimami parametrai - du bitai, ateinantys iš užkoduoto vektoriaus pradžios
        //Grąžinamas rezultatas - pirmos sumos moduliu 2 rezultatas
        //
        //Funkcija sudeda pirmus du iš užkoduoto vektoriaus atėjusius bitus bei viršutinės atminties 2-ą, 5-ą ir 6-ą bitus
        //ir grąžina sumos moduliu 2 rezultatą
        private int GetFirstSumResult(int firstBit, int secondBit)
        {
            var sum = firstBit + secondBit + _upMemory.GetBit(2) + _upMemory.GetBit(5) + _upMemory.GetBit(6);
            return sum % 2;
        }

        //Priimami parametrai - bitas, atėjęs po pirmos sumos
        //Grąžinamas rezultatas - bitas, gautas daugumos sprendimu
        //
        //Funkcija paima bitą po pirmos sumos ir apatinės atminties 1-ą, 4-ą ir 6-ą bitą. Tuomet, jei vienetų yra daugiau arba lygu nei 3
        //(bitų suma didesnė arba lygi už 3), grąžina vieną, jei ne, nulį
        private int GetMajorityDecisionElement(int bitToProcess)
        {
            var sum = bitToProcess + _downMemory.GetBit(1) + _downMemory.GetBit(4) + _downMemory.GetBit(6);
            return sum >= 3 ? 1 : 0;
        }

        //Priimami parametrai - daugumos sprendimo bitas
        //Grąžinamas rezultatas - dekoduotas bitas
        //
        //Funkcija sudeda daugumos sprendimo bitą ir viršutinės atminties 6-ąjį bitą bei grąžina sumos moduliu 2 rezultatą,
        //kuris yra dekoduotas bitas, einantis į dekoduotą vektorių
        private int GetDecodedBit(int majorityDecisionElement)
        {
            var sum = _upMemory.GetBit(6) + majorityDecisionElement;
            return sum % 2;
        }
    }
}
