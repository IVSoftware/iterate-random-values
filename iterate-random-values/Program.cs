using System;
using System.Linq;
using System.Xml.Linq;

namespace iterate_random_values
{
    class Program
    {
        static void Main(string[] args)
        {
            var testset = generateTestSet();
            var ints =
                testset
                .Descendants()
                .Select(xel => (int)xel)
                .ToArray();
            int sumOfAttributes =
                testset
                .Descendants()
                .Sum(xel=>Convert.ToInt32(xel.Attribute("rand").Value));
            { }
            var xtexts =
                testset
                .Descendants()
                .OfType<XText>()
                .ToArray();
            { }
            // https://stackoverflow.com/a/4251360/5438626
            int sumOfElements =
                testset
                .Descendants()
                .OfType<XText>()
                .Sum(xtext => Convert.ToInt32(xtext.Value));
            { }
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
            return new XElement(
                "xnode",
                new XAttribute("rand", rand),
                new XText(rand.ToString())
            ); ;
        }
        static Random _rando = new Random(Seed: 100);
    }

    static class Extensions
    {

    }
}
