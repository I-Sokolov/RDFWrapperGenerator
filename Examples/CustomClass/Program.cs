using System;

namespace CustomClass
{

    class Program
    {
        static void Main(string[] args)
        {
            string modelPath = "CustomModel.bin";
            if (!System.IO.File.Exists (modelPath))
            {
                Console.WriteLine("Model {0} does not exist. Creating new and defining custom class and properties...");
                ModelCreator.Run(modelPath);
                Console.WriteLine("Initial model cetaed sucessfully. Generate wrapper and restart this application");
                return;
            }

            UseCustomWrapper.Run(modelPath);               
        }
    }
}
