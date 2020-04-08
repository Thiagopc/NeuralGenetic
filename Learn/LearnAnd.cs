using System.Collections.Generic;
using NeuralGenetic.Genetic;
using rexgame.NeuralNetwork;

namespace NeuralGenetic.Learn
{
    public class LearnAnd : IFitness
    {

        public int Size{get{return this._neural.GetAllValues().Count;}}
        private MultiLayerPerceptron _neural;

        public LearnAnd(int inputSize, int layer, params int[] neuronParams){
            this._neural = new MultiLayerPerceptron(inputSize, layer, neuronParams);
        }

        public int Function(List<decimal> genome)
        {
            int points =0;
            this._neural.SetWeights(genome);

            if(this._neural.Calc(0,0,0) <= 0.5m){
                points++;
            }

            if(this._neural.Calc(0,1,0) > 0.5m){
                points++;
            }

            if(this._neural.Calc(0,0,1) > 0.5m){
                points++;
            }

            if(this._neural.Calc(0,1,1) > 0.5m){
                points++;
            }

            
            // ================

            if(this._neural.Calc(1,0,0) <= 0.5m){
                points++;
            }

            if(this._neural.Calc(1,1,0) <= 0.5m){
                points++;
            }

            if(this._neural.Calc(1,0,1) <= 0.5m){
                points++;
            }

            if(this._neural.Calc(1,1,1) > 0.5m){
                points++;
            }




            return points;
            
        }
    }
}