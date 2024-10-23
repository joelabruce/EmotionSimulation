public class NeuralNetwork
{
    int inputSize;
    int hiddenSize;
    int outputSize;

    double[,] weightsInputHidden;
    double[,] weightsHiddenOutput;
    double[] biasHidden;
    double[] biasOutput;

    public NeuralNetwork(int inputSize, int hiddenSize, int outputSize)
    {
        this.inputSize = inputSize;
        this.hiddenSize = hiddenSize;
        this.outputSize = outputSize;

        Random rand = new Random();
        weightsInputHidden = new double[inputSize, hiddenSize];
        weightsHiddenOutput = new double[hiddenSize, outputSize];
        biasHidden = new double[hiddenSize];
        biasOutput = new double[outputSize];

        for (int i = 0; i < inputSize; i++)
            for (int j = 0; j < hiddenSize; j++)
                weightsInputHidden[i, j] = rand.NextDouble() - 0.5;

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
                weightsHiddenOutput[i, j] = rand.NextDouble() - 0.5;
            biasHidden[i] = rand.NextDouble() - 0.5;
        }

        for (int i = 0; i < outputSize; i++)
            biasOutput[i] = rand.NextDouble() - 0.5;
    }

    // Sigmoid activation function
    double Sigmoid(double x)
    {
        return 1.0 / (1.0 + Math.Exp(-x));
    }

    // Softmax activation function for output layer
    double[] Softmax(double[] x)
    {
        double max = x[0];
        for (int i = 1; i < x.Length; i++)
            if (x[i] > max) max = x[i];

        double sum = 0.0;
        double[] result = new double[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            result[i] = Math.Exp(x[i] - max);
            sum += result[i];
        }

        for (int i = 0; i < x.Length; i++)
            result[i] /= sum;

        return result;
    }

    // Forward pass to predict output
    public double[] Predict(double[] inputs)
    {
        double[] hiddenLayer = new double[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            hiddenLayer[i] = 0;
            for (int j = 0; j < inputSize; j++)
                hiddenLayer[i] += inputs[j] * weightsInputHidden[j, i];
            hiddenLayer[i] += biasHidden[i];
            hiddenLayer[i] = Sigmoid(hiddenLayer[i]);
        }

        double[] outputLayer = new double[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputLayer[i] = 0;
            for (int j = 0; j < hiddenSize; j++)
                outputLayer[i] += hiddenLayer[j] * weightsHiddenOutput[j, i];
            outputLayer[i] += biasOutput[i];
        }

        return Softmax(outputLayer); // Softmax for classification
    }

    // Parallelized training method using mini-batches
    public void TrainParallel(double[][] inputs, double[][] expectedOutputs, double learningRate, int batchSize)
    {
        int numBatches = inputs.Length / batchSize;

        for (int epoch = 0; epoch < 10; epoch++) // Example with 10 epochs
        {
            // Loop through batches
            for (int batch = 0; batch < numBatches; batch++)
            {
                // Get the start and end indices for this batch
                int startIdx = batch * batchSize;
                int endIdx = startIdx + batchSize;

                // Accumulate weight and bias updates in parallel
                Parallel.For(startIdx, endIdx, i =>
                {
                    // Get inputs and expected outputs for this example
                    double[] input = inputs[i];
                    double[] expectedOutput = expectedOutputs[i];

                    // Forward pass
                    double[] hiddenLayer = new double[hiddenSize];
                    for (int h = 0; h < hiddenSize; h++)
                    {
                        hiddenLayer[h] = 0;
                        for (int j = 0; j < inputSize; j++)
                            hiddenLayer[h] += input[j] * weightsInputHidden[j, h];
                        hiddenLayer[h] += biasHidden[h];
                        hiddenLayer[h] = Sigmoid(hiddenLayer[h]);
                    }

                    double[] outputLayer = new double[outputSize];
                    for (int o = 0; o < outputSize; o++)
                    {
                        outputLayer[o] = 0;
                        for (int h = 0; h < hiddenSize; h++)
                            outputLayer[o] += hiddenLayer[h] * weightsHiddenOutput[h, o];
                        outputLayer[o] += biasOutput[o];
                    }

                    outputLayer = Softmax(outputLayer);

                    // Calculate output layer error
                    double[] outputLayerError = new double[outputSize];
                    for (int o = 0; o < outputSize; o++)
                        outputLayerError[o] = expectedOutput[o] - outputLayer[o];

                    // Backpropagation
                    double[] hiddenLayerError = new double[hiddenSize];
                    for (int h = 0; h < hiddenSize; h++)
                    {
                        hiddenLayerError[h] = 0;
                        for (int o = 0; o < outputSize; o++)
                            hiddenLayerError[h] += outputLayerError[o] * weightsHiddenOutput[h, o];
                        hiddenLayerError[h] *= Sigmoid(hiddenLayer[h]) * (1 - Sigmoid(hiddenLayer[h]));
                    }

                    // Update weights and biases (gradient descent step)
                    lock (weightsInputHidden) // Use locks to prevent race conditions when updating weights
                    {
                        for (int j = 0; j < inputSize; j++)
                            for (int h = 0; h < hiddenSize; h++)
                                weightsInputHidden[j, h] += learningRate * hiddenLayerError[h] * input[j];
                    }

                    lock (weightsHiddenOutput)
                    {
                        for (int h = 0; h < hiddenSize; h++)
                            for (int o = 0; o < outputSize; o++)
                                weightsHiddenOutput[h, o] += learningRate * outputLayerError[o] * hiddenLayer[h];
                    }

                    lock (biasHidden)
                    {
                        for (int h = 0; h < hiddenSize; h++)
                            biasHidden[h] += learningRate * hiddenLayerError[h];
                    }

                    lock (biasOutput)
                    {
                        for (int o = 0; o < outputSize; o++)
                            biasOutput[o] += learningRate * outputLayerError[o];
                    }
                });
            }

            // Optionally: Print loss or accuracy after each epoch
            Console.WriteLine($"Epoch {epoch + 1} completed.");
        }
    }

    public static void Main()// string[] args)
    {
        // Example: Training on 28x28 images (e.g., MNIST) flattened to 784 input neurons
        NeuralNetwork nn = new NeuralNetwork(784, 128, 10); // 10 classes (0-9 digits)

        // Generate some random training data (replace with actual image data)
        double[][] inputs = new double[10000][];    // Example: 10,000 training samples
        double[][] expectedOutputs = new double[10000][]; // Example: 10,000 expected outputs (one-hot encoded)

        // Fill the arrays with random data (replace with real data)
        Random rand = new Random();
        for (int i = 0; i < 10000; i++)
        {
            inputs[i] = new double[784]; // Flattened 28x28 image
            expectedOutputs[i] = new double[10];  // One-hot label for 10 classes
            expectedOutputs[i][rand.Next(0, 10)] = 1;  // Random label assignment

            for (int j = 0; j < 784; j++)
                inputs[i][j] = rand.NextDouble();
        }

        // Train the network with mini-batch parallelism
        nn.TrainParallel(inputs, expectedOutputs, 0.1, 100); // Batch size of 100
    }
}