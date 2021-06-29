    namespace MLAlgoLib
{

namespace ArtificialNeuralNetwork
{

     public enum ActivationFunctionEnum
        {
            LinearFunction = 0,
            SigmoidFunction = 1,
            BipolarSigmoidFunction = 2,
            RectifiedLinearFunction=3

        }

     public enum LearningAlgorithmEnum
        {
            BackPropagationLearning = 0,
            LevenbergMarquardtLearning = 1,
            EvolutionaryLearningGA = 2,
            RGA_Learning = 3,
            GSA_Learning = 4,
            GWO_Learning = 5,
            HPSOGWO_Learning = 6,
            mHPSOGWO_Learning = 7,
                PSOGSA_Learning=8,
            BayesianLevenbergMarquardtLearning = 9
        }

      public enum OptimizationAlogrithmEnum
        {
            GA_Optimizer = 0,
            GSA_Optimizer = 1,
            GWO__Optimizer = 2,
            HPSOGWO_Optimizer = 3
        }
}
}