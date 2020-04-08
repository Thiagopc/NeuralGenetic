using System;
using System.Collections.Generic;
using System.Linq;
using rexgame.NeuralNetwork;

namespace rexgame.Genetic
{

    public struct Point
    {
        public int Indice { get; set; }
        public int Value { get; set; }
    }
    public class GeneticAlgorithm
    {
        private int _sizeGenes;
        private readonly int _epoch;
        private static Random _random = new Random();

        public List<MultiLayerPerceptron> _gene;
        public List<Point> _point = new List<Point>();
        public GeneticAlgorithm(int sizeGene, bool whileNotFind, int epoch = 1000)
        {
            _gene = new List<MultiLayerPerceptron>();
            this._epoch = epoch;

            for (int i = 0; i < sizeGene; i++)
            {

                this._gene.Add(new MultiLayerPerceptron(2, 3, 3, 4, 5));
            }




            this._sizeGenes = this._gene.FirstOrDefault().GetAllValues().Count;
        }


        public void Calculate()
        {

            int pontos = 0;
            for (int i = 0; i < this._gene.Count; i++)
            {

                var result = this._gene[i].Calc(1, 1);

                if (result <= 0.5m)
                    pontos++;

                result = this._gene[i].Calc(1, 0);
                if (result > 0.5m)
                    pontos++;

                result = this._gene[i].Calc(0, 1);
                if (result > 0.5m)
                    pontos++;

                result = this._gene[i].Calc(0, 0);
                if (result <= 0.5m)
                    pontos++;


                this._point.Add(new Point()
                {
                    Indice = i,
                    Value = pontos

                });
                pontos = 0;
            }

        }



        public void Run()
        {

            for (int i = 0; i < this._epoch; i++)
            {
                this.Mutation();
                this.Calculate();
            }



        }

        private void Mutation()
        {
            int sizeGene = this._gene.FirstOrDefault().GetAllValues().Count;
            int dezporcent =Convert.ToInt32( this._gene.Count * 0.10);
            int metade = Convert.ToInt32(sizeGene /2);
            int target = 5;
            List<decimal> pesos = new List<decimal>();
            for(int i=0; i < dezporcent; i++){

                var um = _random.Next(3,this._gene.Count);
                var dois = _random.Next(3,this._gene.Count);
                target = um;
                var pt1 = this._gene[um].GetAllValues().GetRange(0,metade);
                var pt2 = this._gene[dois].GetAllValues().GetRange(metade,metade);
                

                for(int j =0; j < sizeGene; j++){

                    
                    if(j < metade){                        
                        pesos.Add(pt1[j]);
                    }else
                    {                        
                        pesos.Add(pt2[ j - metade]);
                    }

                }
            }

            this._gene[target].SetWeights(pesos);
            pesos.Clear();
        }

    }
}