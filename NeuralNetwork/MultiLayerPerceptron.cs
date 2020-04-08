using System.Collections.Generic;
using System;

namespace rexgame.NeuralNetwork
{
    public class MultiLayerPerceptron
    {
        private decimal _result;
        private decimal _bias;
        public List<List<Neuron>> NeuronLayer { get; set; }
        private static Random _random = new Random();

        private decimal getWeight()
        {
            var number = _random.Next(100) / 100.0m;
            number *= _random.Next(2) == 1 ? 1 : -1;
            return number;
        }


        public List<decimal> GetAllValues()
        {
            List<decimal> values = new List<decimal>();

            for (int layer = 0; layer < this.NeuronLayer.Count; layer++)
            {

                for (int neuron = 0; neuron < this.NeuronLayer[layer].Count; neuron++)
                {

                    for (int weigth = 0; weigth < this.NeuronLayer[layer][neuron].Weight.Length; weigth++)
                    {

                        values.Add(this.NeuronLayer[layer][neuron].Weight[weigth]);

                    }


                }

            }

            values.Add(_bias);
            return values;
        }


        public void SetWeights(List<decimal> weights)
        {

            int sentinela = 0;
            for (int layer = 0; layer < this.NeuronLayer.Count; layer++)
            {

                for (int neuron = 0; neuron < this.NeuronLayer[layer].Count; neuron++)
                {

                    for (int weigth = 0; weigth < this.NeuronLayer[layer][neuron].Weight.Length; weigth++)
                    {

                        this.NeuronLayer[layer][neuron].Weight[weigth] = weights[sentinela];
                        sentinela++;

                    }


                }

            }
            this._bias = weights[sentinela];
        }

        public decimal Calc(params decimal[] inputs)
        {

            if (inputs.Length != this.NeuronLayer[0].Count)
                throw new Exception("Valor de inputs deve ser consistente");

            //Camada input
            for (int i = 0; i < this.NeuronLayer[0].Count; i++)
            {
                this.NeuronLayer[0][i].SetValue(inputs[i]);
            }

            for (int i = 1; i < this.NeuronLayer.Count; i++)
            {

                //Se nao for a última camada
                //if (i != (this.NeuronLayer.Count - 1))


                //Navegando pelos neuronios
                for (int n = 0; n < this.NeuronLayer[i].Count; n++)
                {

                    //Nvegango pelos neuronios da rede anterior
                    for (int nw = 0; nw < this.NeuronLayer[i - 1].Count; nw++)
                    {

                        //Navegando pelos pesos neuronio anterior
                        for (int w = 0; w < this.NeuronLayer[i - 1][nw].Weight.Length; w++)
                        {
                            var resultW = this.NeuronLayer[i - 1][nw].GetValue(w);
                            this.NeuronLayer[i][n].SetValue(resultW);
                        }

                    }

                }

                if (i != (this.NeuronLayer.Count - 1))
                {
                    for (int n = 0; n < this.NeuronLayer[i].Count; n++)
                    {
                        this._result += this.NeuronLayer[i][n].GetValue(0);
                    }
                }

            }

            var result = (_result + _bias);
            this._result = 0;
            return result;
        }




        public MultiLayerPerceptron(int inputSize, int layerSize, params int[] neuronPerLayer)
        {
            this._bias = this.getWeight();
            if (neuronPerLayer.Length != layerSize)
                throw new Exception("Number of neuronioms should be equal layer size");

            this.NeuronLayer = new List<List<Neuron>>();

            for (int i = 0; i < (layerSize + 1); i++)
            {
                this.NeuronLayer.Add(new List<Neuron>());
                //Incializa camada de input's
                if (i == 0)
                {
                    for (int j = 0; j < inputSize; j++)
                    {
                        var n = new Neuron();
                        n.Weight = new decimal[neuronPerLayer[0]];

                        for (int y = 0; y < n.Weight.Length; y++)
                        {
                            n.Weight[y] = this.getWeight();
                        }

                        this.NeuronLayer[i].Add(n);
                    }

                }
                //Inicializa camadas da rede neural
                else
                {
                    //Se for diferente do final
                    if (i != layerSize)
                    {
                        for (int quantityNeuron = 0; quantityNeuron < neuronPerLayer[i - 1]; quantityNeuron++)
                        {
                            var n = new Neuron();
                            n.Weight = new decimal[neuronPerLayer[i]];
                            for (int y = 0; y < n.Weight.Length; y++)
                            {
                                n.Weight[y] = this.getWeight();
                            }
                            this.NeuronLayer[i].Add(n);
                        }

                        //se for a última camda
                    }
                    else
                    {
                        for (int quantityNeuron = 0; quantityNeuron < neuronPerLayer[i - 1]; quantityNeuron++)
                        {

                            var n = new Neuron();
                            n.Weight = new decimal[1];
                            n.Weight[0] = this.getWeight();
                            this.NeuronLayer[i].Add(n);
                        }
                    }



                }

            }

        }

    }


    public class Neuron
    {
        public decimal[] Weight { get; set; }

        private decimal _value;

        public decimal GetValue(int indice)
        {
            var result = this._value * Weight[indice];
            if (indice == (Weight.Length - 1))
                this._value = 0;
            return result;
        }

        public void SetValue(decimal value)
        {
            this._value += value;
        }



    }

}