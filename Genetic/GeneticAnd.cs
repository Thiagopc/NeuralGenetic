namespace NeuralGenetic.Genetic
{
    public class GeneticAnd : GeneticAlgorithmAlternative
    {

        public GeneticAnd(int population, int sizeGenome,
        int crossover, int percentMutantion, IFitness _fitness, int poch = 100) :
        base(population, sizeGenome, crossover, percentMutantion, _fitness)
        {

        }

    }
}