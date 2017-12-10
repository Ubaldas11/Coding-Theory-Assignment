using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTheoryConvolutionalCode.Coding
{
    public class Channel
    {
        private readonly List<int> _headerBits;
        private readonly List<int> _footerBits;

        public Channel()
        {
            _headerBits = new List<int>();
            _footerBits = new List<int>();
        }

        //Priimami parametrai - vektorius, siunčiamas kanalu ir klaidos tikimybė
        //Grąžinamas rezultatas - kanalu persiųstas vektorius

        //Funkcija imituoja vektoriaus siuntimą kanalu. Kiekvienam vektoriaus bitui yra generuojama
        //atsitiktinė reikšmė iš [0;1] intervalo, ir jei ji yra mažesnė už vartotojo įvestą klaidos tikimybę,
        //įvykdoma klaida (bitas pakeičiamas), jei ne, siunčiamas tas pats bitas
        public Queue<int> SendThroughChannel(Queue<int> vector, double errorChance)
        {
            var bitList = vector.ToList();
            var sentBits = new Queue<int>();
            var random = new Random();
            bitList.ForEach(x =>
            {
                if (random.NextDouble() < errorChance)
                {
                    sentBits.Enqueue(x ^ 1);
                }
                else
                {
                    sentBits.Enqueue(x);
                }
            });
            return sentBits;
        }

        //Priimami parametrai - originalus vektorius, kanalu persiųstas vektorius
        //Grąžinamas rezultatas - padarytų klaidų siunčiant kanalu pozicijos
        //
        //Funkcija palygina du vektorius ir radus nesutampančius bitus toje pačioje pozicijoje,
        //rastą poziciją įdeda į klaidų pozicijų sąrašą
        public List<int> CalculateErrors(Queue<int> vector, Queue<int> receivedVector)
        {
            var vectorAsList = vector.ToList();
            var receivedVectorAsList = receivedVector.ToList();
            var errorPositionList = new List<int>();
            for (var i = 0; i < receivedVector.Count; i++)
            {
                if (receivedVectorAsList[i] != vectorAsList[i])
                {
                    errorPositionList.Add(i + 1);
                }
            }
            return errorPositionList;
        }

        //Priimami parametrai - originalus paveiksliuko vektorius
        //Grąžinamas rezultatas - paveiksliuko vektorius be techninės informacijos
        //
        //Funkcija imituoja techninės paveiksliuko informacijos siuntimą saugesniu kanalu.
        //Nuo paduoto originalaus paveiksliuko vektoriaus yra nurėžiami antraštės ir paraštės bitai,
        //kurie yra išsaugomi
        public Queue<int> SendJPGTechnicalDataSafely(Queue<int> vector)
        {
            var vectorList = vector.ToList();
            for (int i = 0; i < 10 * 8; i++)
            {
                _headerBits.Add(vectorList[i]);
            }
            var counter = vectorList.Count - 1;
            for (int i = 0; i < 2 * 8; i++)
            {
                _footerBits.Add(vectorList[counter]);
                counter--;
            }
            vectorList.RemoveRange(0, 10 * 8);
            vectorList.RemoveRange(vectorList.Count - 1 - 2 * 8, 2 * 8);
            vector = new Queue<int>(vectorList);
            return vector;
        }

        //Priimami parametrai - paveiksliuko vektorius be techninės informacijos
        //Grąžinamas rezultatas - paveiksliuko vektorius, tinkamas atvaizdavimui
        //
        //Funkcija prie gauto vektoriaus prideda paveiksliuko antraštės ir paraštės bitus
        //ir grąžina paruoštą išsaugojimui į failą vektorių
        public Queue<int> AddJPGTechnicalData(Queue<int> vector)
        {
            var decodedVectorList = vector.ToList();
            for (var i = 10 * 8 - 1; i >= 0; i--)
            {
                decodedVectorList.Insert(0, _headerBits[i]);
            }
            for (var i = 2 * 8 - 1; i >= 0; i--)
            {
                decodedVectorList.Add(_footerBits[i]);
            }
            vector = new Queue<int>(decodedVectorList);
            return vector;
        }
    }
}
