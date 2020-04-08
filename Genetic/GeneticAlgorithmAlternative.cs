using System;
using System.Collections.Generic;
using System.Linq;
using rexgame.NeuralNetwork;

namespace NeuralGenetic.Genetic
{
    public abstract class GeneticAlgorithmAlternative
    {
        private readonly IFitness _fitness;
        private readonly int _taxcrossover;
        private readonly int _taxMutation;
        private readonly int _epoch;
        public List<Genome> _genome;

        private static Random _random;

        public GeneticAlgorithmAlternative(int population, int sizeGenome,
        int crossover, int percentMutantion, IFitness _fitness, int poch = 100)
        {
            _genome = new List<Genome>();
            this._fitness = _fitness;
            this._taxcrossover = Convert.ToInt32( (crossover / 100.0) * population);
            this._taxMutation = percentMutantion;
            this._epoch = poch;

            if (_random == null)
                _random = new Random();

            for (int populationController = 0; populationController < population; populationController++)
            {
                this._genome.Add(new Genome());
                for (int size = 0; size < sizeGenome; size++)
                {
                    this._genome[populationController].Genomes.Add(this.getRandom());
                }

            }

        }




        private decimal getRandom()
        {
            var number = _random.Next(0,10) / 10.0m;
            number *= _random.Next(2) == 1 ? 1 : -1;
            return number;
        }

        private void RunNetwork()
        {

            for (int population = 0; population < this._genome.Count; population++)
            {

                var genome = this._genome[population];
                genome.Point = this._fitness.Function(genome.Genomes);

            }

            this._genome = this._genome.OrderByDescending(c => c.Point).ToList();
        }



        private void CrosSover()
        {

            int halfGenmoe = this._genome.FirstOrDefault().Genomes.Count() / 2;
            bool mutation = this._taxMutation <= _random.Next(0, this._taxMutation);

            List<decimal> newGenome = new List<decimal>();
            for (int i = 0; i < this._taxcrossover; i++)
            {

                var num = _random.Next(3,this._genome.Count - 1);
                var first = this._genome[num].Genomes.GetRange(0, halfGenmoe);
                var second = this._genome[_random.Next(this._genome.Count - 1)].Genomes.GetRange(halfGenmoe, ( (this._genome[0].Genomes.Count) - halfGenmoe ));

                second.AddRange(first);
                this._genome[num].Genomes = second.ToList();

                 
            }


        }


        public void Run()
        {
            this.RunNetwork();

            for(int i =0; i < _epoch; i++){
                this.CrosSover();
                this.RunNetwork();
            }

        }
    }



    public class Genome
    {
        public List<decimal> Genomes { get; set; } = new List<decimal>();
        public int Point { get; set; }

    }

    public interface IFitness
    {
        int Function(List<decimal> genome);
    }
}