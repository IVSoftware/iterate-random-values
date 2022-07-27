using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace iterate_random_values
{
    class Program
    {
        static void Main(string[] args)
        {
            var testset = generateTestSet();
            Console.WriteLine(testset.ToString());
            Console.WriteLine();

            int sumOfAttributes =
                testset
                .Descendants("rand")
                .Sum(xrand=>(int)xrand);

            Debug.Assert(
                sumOfAttributes == 99, 
                "Expecting seeded pseudorandom sequence to consistently produce 99");

            int sumOfElements =
                testset
                .Descendants("xnode")
                .Select(xel=>Convert.ToInt32(xel.Attribute("rand").Value))
                .Sum(xtext => (int)xtext);

            Debug.Assert(
                sumOfElements == 99,
                "Expecting seeded pseudorandom sequence to consistently produce 99");

            Console.WriteLine($"Sum of attributes: {sumOfAttributes}");
            Console.WriteLine($"Sum of elements  : {sumOfElements}");
        }
        private static XElement generateTestSet()
        {
            var testSet = new XElement("root");
            for (int i = 0; i < 5; i++)
            {
                var xel = randoElement();
                xel.Add(randoElement());
                xel.Add(randoElement());
                testSet.Add(xel);
            }
            return testSet;
        }
        private static XElement randoElement()
        {
            var rand = _rando.Next(1, 11);
            var xel = new XElement(
                "xnode",
                new XAttribute("rand", rand),    // Attribute
                new XElement("rand", rand)
            );
            return xel;
        }
        static Random _rando = new Random(Seed: 100);
    }
}
