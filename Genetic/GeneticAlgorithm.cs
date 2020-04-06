using System.Collections.Generic;
using rexgame.NeuralNetwork;

namespace rexgame.Genetic
{

    public struct Point{
        public int Indice{get;set;}
        public int Value {get;set;}
    }
    public class GeneticAlgorithm
    {
        public List<MultiLayerPerceptron> _gene;
        public List<Point> _point = new List<Point>();
        public GeneticAlgorithm(int sizeGene, bool whileNotFind, int epoch = 1000){
            _gene = new List<MultiLayerPerceptron>();

            for(int i =0; i < sizeGene; i++){

                this._gene.Add(new MultiLayerPerceptron(2,3,3,4,5));
            }


        }


        public void Calculate(){

            int pontos =0;
            for(int i =0; i < this._gene.Count; i++){

                var result = this._gene[i].Calc(1,1);
                
                if(result <= 0.5m)
                    pontos++;

                result = this._gene[i].Calc(1,0);
                if(result >= 0.5m)
                    pontos++;

                result = this._gene[i].Calc(1,0);
                if(result >= 0.5m)
                    pontos++;

                result = this._gene[i].Calc(0,0);
                if(result <= 0.5m)
                    pontos++;
            
            
            this._point.Add(new Point(){
                Indice = i,
                Value = pontos

            });
            pontos =0;
            }

        }



    }
}