using System;
using System.Linq;
using Newtonsoft.Json;
using rexgame.Genetic;
using rexgame.NeuralNetwork;

namespace rexgame
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new GeneticAlgorithm(100,false);
            x.Calculate();

            var zz = x._point.OrderByDescending(c => c.Value).FirstOrDefault();
            var gene = x._gene[zz.Indice];

            
        }
    }
}
