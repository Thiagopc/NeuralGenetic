using System.Linq;
using NeuralGenetic.Genetic;
using NeuralGenetic.Learn;


namespace rexgame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Três inputs
            // O primeiro valor do input é referente a operação de aprendizado
            // 0 = OR , 1 = AND
            LearnAndOr learn = new LearnAndOr(3,3,4,4,5);

            GeneticAlgorithm c = new GeneticAlgorithm(100,learn.Size,40,20,learn,500);            

            c.Run();                
            var top =  c._genome.OrderByDescending(x => x.Point).FirstOrDefault();
                      
            
            learn.Function(top.Genomes);


        }
    }
}
