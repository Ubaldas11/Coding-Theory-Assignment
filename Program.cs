using System;
using CodingTheoryConvolutionalCode.Coding;
using CodingTheoryConvolutionalCode.Helpers;
using CodingTheoryConvolutionalCode.InputOutput;

namespace CodingTheoryConvolutionalCode
{
    class Program
    {
        /**
            Pagrindinė programos funkcija.
            Vykdo vartotojo pasirinktą scenarijų. Jei vartotojas neteisingai įveda informaciją,
            jam apie tai pranešama ir prašoma atlikti nurodytus veiksmus iš naujo.
        */
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    var scenario = InputOutputController.ChooseScenario();
                    switch (scenario)
                    {
                        case Scenario.Vector:
                            VectorScenario(new Channel(), new Encoder(), new Decoder());
                            break;
                        case Scenario.Text:
                            TextScenario(new Channel(), new Encoder(), new Decoder());
                            break;
                        case Scenario.Image:
                            ImageScenario(new Channel(), new Encoder(), new Decoder());
                            break;
                        case Scenario.Exit:
                            Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception)
                {
                    InputOutputController.InformAboutWrongInput();
                }
            }
        }

        //Priimami parametrai - kanalas, užkoduotojas ir dekoduotojas
        //Nieko negrąžina
        //
        //Funkcija, vykdanti paveiksliuko scenarijų. Nuskaito klaidos tikimybę, paverčia nurodytą paveiksliuką į bitus,
        //pasilieka techninę informaciją ir
        //1) likusius bitus užkoduoja, siunčia kanalu, dekoduoja, prideda techninę informaciją ir bando atvaizduoti faile
        //2) likusius bitus siunčia kanalu, prideda techninę informaciją ir bando atvaizduoti file
        private static void ImageScenario(Channel channel, Encoder encoder, Decoder decoder)
        {
            var errorChance = InputOutputController.InputErrorChance();
            var vector = InputOutputController.ReadImageFile();
            vector = channel.SendJPGTechnicalDataSafely(vector);
            var copyOfInputVector = ConvertingHelper.CopyVector(vector);

            var encodedVector = encoder.Encode(vector);
            var receivedEncodedVector = channel.SendThroughChannel(encodedVector, errorChance);
            var decodedVector = decoder.Decode(receivedEncodedVector);
            decodedVector = channel.AddJPGTechnicalData(decodedVector);
            InputOutputController.SaveImage(decodedVector, "decodedImage");

            var receivedInputVector = channel.SendThroughChannel(copyOfInputVector, errorChance);
            receivedInputVector = channel.AddJPGTechnicalData(receivedInputVector);
            InputOutputController.SaveImage(receivedInputVector, "notEncodedImage");
        }

        //Priimami parametrai - kanalas, užkoduotojas ir dekoduotojas
        //Nieko negrąžina
        //
        //Funkcija vykdo 1 vektoriaus scenarijų. Perskaito klaidos tikimybę, perskaito įvestą vektorių,
        //jį užkoduoja, siunčia kanalu, parodo padarytas klaidas, pasiūlo pakeisti padarytas klaidas,
        //jei vartotojas nori - pakeičia jas, dekoduoja vektorių, parodo jį
        private static void VectorScenario(Channel channel, Encoder encoder, Decoder decoder)
        {
            var inputVector = InputOutputController.InputVector();
            var errorChance = InputOutputController.InputErrorChance();

            var encodedVector = encoder.Encode(inputVector);
            InputOutputController.PrintVector(encodedVector, "Encoded vector:", true);

            var receivedVector = channel.SendThroughChannel(encodedVector, errorChance);
            var errorList = channel.CalculateErrors(encodedVector, receivedVector);
            InputOutputController.PrintVector(receivedVector, "Sent vector:", true);
            InputOutputController.PrintInfoAboutErrors(errorList);

            bool changed;
            var changedVector = InputOutputController.ChangeErrors(receivedVector, out changed);
            if (changed)
            {
                InputOutputController.PrintVector(changedVector, "Changed vector:", true);
            }
            var decodedVector = decoder.Decode(changedVector);
            InputOutputController.PrintVector(decodedVector, "Decoded vector:", false);
        }

        //Priimami parametrai - kanalas, užkoduotojas ir dekoduotojas
        //Nieko negrąžina
        //
        //Funkcija, vykdanti teksto scenarijų. Perskaito vartotojo įvestą klaidos tikimybę, tekstą, paverčia jį į vektorių ir
        // 1) užkoduoją jį, siunčia kanalu, dekoduoja ir atvaizduoja ekrane
        // 2) siunčia kanalu ir atvaizduoja ekrane
        private static void TextScenario(Channel channel, Encoder encoder, Decoder decoder)
        {
            var errorChance = InputOutputController.InputErrorChance();
            var inputVector = InputOutputController.InputText();
            var copyOfInputVector = ConvertingHelper.CopyVector(inputVector);

            var encodedVector = encoder.Encode(inputVector);
            var receivedEncodedVector = channel.SendThroughChannel(encodedVector, errorChance);
            var decodedVector = decoder.Decode(receivedEncodedVector);
            var decodedText = ConvertingHelper.ConvertVectorToText(decodedVector);
            InputOutputController.PrintText("Decoded text: ", decodedText);

            var receivedInputVector = channel.SendThroughChannel(copyOfInputVector, errorChance);
            var plainText = ConvertingHelper.ConvertVectorToText(receivedInputVector);
            InputOutputController.PrintText("Plain text, received after sending from channel: ", plainText);
        }
    }
}
