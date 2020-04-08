using System;
using System.Linq;
using NeuralGenetic.Genetic;
using NeuralGenetic.Learn;
using Newtonsoft.Json;
using rexgame.Genetic;
using rexgame.NeuralNetwork;

namespace rexgame
{
    class Program
    {
        static void Main(string[] args)
        {
            // var x = new GeneticAlgorithm(5000,false,10);
            // x.Calculate();
            // x.Run();

            // var zz = x._point.OrderByDescending(c => c.Value).FirstOrDefault();
            // var gene = x._gene[zz.Indice];

            LearnAnd learn = new LearnAnd(3,3,4,4,5);

            GeneticAnd c = new GeneticAnd(100,learn.Size,40,20,learn,500);            

            c.Run();                
            var top =  c._genome.OrderByDescending(x => x.Point).FirstOrDefault();
                      
            learn.Function(top.Genomes);


        }
    }
}
