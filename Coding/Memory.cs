using System;

namespace CodingTheoryConvolutionalCode.Coding
{
    public class Memory
    {
        private readonly int[] _memoryBits;

        //Inicializuojama atmintis, kurioje yra 6-i nuliniai bitai.
        public Memory()
        {
            _memoryBits = new int[] {0, 0, 0, 0, 0, 0};
        }

        //Priimami parametrai - bitas, kurį reikia įstumti į atmintį
        //Nieko negrąžina
        //
        //Funkcija perstumia visus atminties bitus dešinėn ir į pirmą atminties registrą įrašo paduotą bitą
        public void PushBit(int bit)
        {
            for (var i = _memoryBits.Length - 1; i > 0; i--)
            {
                _memoryBits[i] = _memoryBits[i - 1];
            }
            _memoryBits[0] = bit;
        }

        //Priimami parametrai - atminties registras
        //Grąžinamas rezultatas - atminties bitas
        //
        //Funkcija grąžina atminties bitą, esantį nurodytame atminties registre
        public int GetBit(int position)
        {
            return _memoryBits[position - 1];
        }
    }
}
