using System;
using System.IO;
using System.Linq;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Tensorflow.Models;

namespace TFRecordNetTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunDemo1();
            RunDemo2();
        }

        static void RunDemo2()
        {
            var ddr = new DataDirectoryResolver();
            var trainImagesDirectory = ddr.GetDataDirectory("marble");
            Console.WriteLine("TrainImagesDirectory : {0}", trainImagesDirectory);

            var path = Path.Combine(ddr.DataBaseDirectory, "marble.tfrecord");
            using var outFile = File.Create(path);
            var writer = new TFRecordWriter(outFile);
            var imageFilePaths = Directory.EnumerateFiles(trainImagesDirectory, "*.*", SearchOption.AllDirectories);;
            foreach (var imageFilePath in imageFilePaths)
            {
                Console.WriteLine($"Importing {imageFilePath}...");
                var dirName = new DirectoryInfo(Path.GetDirectoryName(imageFilePath)).Name;
                var example = new Example()
                {
                    Features = new Features
                    {
                        Feature =
                        {
                            ["name"] = Feature.FromString(imageFilePath),
                            ["imageFile"] = Feature.FromFile(imageFilePath),
                            ["label"] = Feature.FromString(dirName)
                        }
                    }
                };
                writer.Write(example.ToByteArray());
            }
        }
        
        static void RunDemo1()
        {
            var ddr = new DataDirectoryResolver();
            var trainImagesDirectory = ddr.GetDataDirectory("two-digit-mnist");
            Console.WriteLine("TrainImagesDirectory : {0}", trainImagesDirectory);

            var imageFiles = Directory.EnumerateFiles(trainImagesDirectory, "*.png", SearchOption.AllDirectories).Take(5).ToList();;
            foreach (var imageFile in imageFiles)
            {
                Console.WriteLine(imageFile);
            }

            var feature1 = Feature.FromValues(new[] {1.0f, 2.0f, 3.0f, 4.0f});
            var feature2 = Feature.FromFile(imageFiles[0]);
            var feature3 = Feature.FromString("Hello World");
            var feature4 = Feature.FromValues(new[] {4L, 5L, 6L});
            
            // Write the example to a .tfrecord file.
            var path = Path.Combine(ddr.DataBaseDirectory, "demo.tfrecord");
            using var outFile = File.Create(path);
            var writer = new TFRecordWriter(outFile);
            var example = new Example()
            {
                Features = new Features
                {
                    Feature =
                    {
                        ["feature1"] = feature1,
                        ["feature2"] = feature2,
                        ["feature3"] = feature3,
                        ["feature4"] = feature4
                    }
                }
            };
            writer.Write(example.ToByteArray());
        }
    }

}