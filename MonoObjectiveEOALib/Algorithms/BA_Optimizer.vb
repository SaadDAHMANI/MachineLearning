''____________________________________________________________________________________________
''Bat optimization algorithm (BA)
''
'' This code Is based On :
'' Yang, X. S. (2010). “A New Metaheuristic Bat-Inspired Algorithm, 
'' in: Nature Inspired Cooperative Strategies for Optimization (NISCO 2010)”. 
'' Studies in Computational Intelligence. 284: 65–74
'' 
'' Matlab code :  Abhishek Gupta (2020). BAT optimization Algorithm 
'' (https//www.mathworks.com/matlabcentral/fileexchange/68981-bat-optimization-algorithm), 
'' MATLAB Central File Exchange. Retrieved December 8, 2020. 
''
'' Writen in VB.NET by S.DAHMANI (sd.dahmani2000@gmail.com)
''___________________________________________________________________________________________
Public Class BA_Optimizer
    Inherits EvolutionaryAlgoBase

    Public Sub New()
    End Sub
    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceIntervals As List(Of Interval))
        PopulationSize_N = populationSize
        Dimensions_D = searchSpaceDimension
        SearchIntervals = searchSpaceIntervals
        'InitializePopulation()
    End Sub

    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Return "BA"
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Return "Bat Algorithm"
        End Get
    End Property

    Private _BestSolution As Double()
    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Return _BestSolution
        End Get
    End Property

    Private _BestChart As List(Of Double)
    Public Overrides ReadOnly Property BestChart As List(Of Double)
        Get
            Return _BestChart
        End Get
    End Property

    Private _WorstChart As List(Of Double)
    Public Overrides ReadOnly Property WorstChart As List(Of Double)
        Get
            Return _WorstChart
        End Get
    End Property

    Private _MeanChart As List(Of Double)
    Public Overrides ReadOnly Property MeanChart As List(Of Double)
        Get
            Return _MeanChart
        End Get
    End Property

    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Private _CurrentBestFitness As Double
    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Return _CurrentBestFitness
        End Get
    End Property

#Region "BA_params"
    Public Property R0_Param As Double
        Get
            Return R0
        End Get
        Set(value As Double)
            If (value >= 0) AndAlso (value <= 1) Then
                R0 = value
            Else
                R0 = 0.1
                Throw New Exception("R0 must in [0, 1].")
            End If
        End Set
    End Property

    Public Property A0_Param As Double
        Get
            Return A0
        End Get
        Set(value As Double)
            If (value >= 0) AndAlso (value <= 2) Then
                A0 = value
            Else
                A0 = 0.9
                Throw New Exception("A0 must in [0, 2].")
            End If
        End Set
    End Property

    Public Property Alpha_Param As Double
        Get
            Return Alpha
        End Get
        Set(value As Double)
            If (value > 0) AndAlso (value < 1) Then
                Alpha = value
            Else
                Alpha = 0.9
                Throw New Exception("Alpha must in ]0, 1[.")
            End If
        End Set
    End Property

    Public Property Gamma_Param As Double
        Get
            Return Gamma
        End Get
        Set(value As Double)
            If value > 0 Then
                Gamma = value
            Else
                Gamma = 0.1R
                Throw New Exception("Gamma must be >0.")
            End If
        End Set
    End Property

    Public Property Fmin_Param As Double
        Get
            Return Fmin
        End Get
        Set(value As Double)
            Fmin = value
        End Set
    End Property

    Public Property Fmax_Param As Double
        Get
            Return Fmax
        End Get
        Set(value As Double)
            Fmax = value
        End Set
    End Property
#End Region

#Region "BA variables"
    Private N, D, Iindex As Integer
    Private fitnessValue, fitnessNew As Double
    Private Fmax As Double = 2 'maximum frequency
    Private Fmin As Double = 0 'minimum frequency
    Private Alpha As Double = 0.9 'constant for loudness update
    Private Gamma As Double = 0.9 'onstant for emission rate update
    Private R0 As Double = 0.1 'initial pulse emission rate
    Private A0 As Double = 0.9 'initial loudness
    Private eps As Double
    Private fitness_min As Double

    Private A As Double() 'loudness for each BAT
    Private R As Double() 'pulse emission rate for each BAT
    Private F As Double() 'Frequency
    Private V As Double(,) 'Velocities
    Private Fitness As Double()
    Private BestSol As Double()


#End Region
    Public Overrides Sub RunEpoch()

        For i = 0 To N
            'Update emission rate
            R(i) = R0 * (1 - Math.Exp((-1 * Gamma * CurrentIteration)))

            'Update loundness
            A(i) = Alpha * A(i)

            'For j = 0 To D
            F(i) = Fmin + ((Fmax - Fmin) * RandomGenerator.NextDouble())  'randomly chose the frequency
            'Next

            For j = 0 To D
                V(i, j) = V(i, j) + ((Population(i)(j) - BestSol(j)) * F(i)) 'update the velocity
                Population(i)(j) = Population(i)(j) + V(i, j) 'update the BAT position
            Next

            '' Check the condition with R
            If RandomGenerator.NextDouble() < R(i) Then
                eps = 0.1 '-1 + (2 * RandomGenerator.NextDouble())
                For j = 0 To D
                    Population(i)(j) = BestSol(j) + (eps * RandomGenerator.NextDouble() * A.Average())
                Next
            End If

            'Apply simple bounds/limits
            For j = 0 To D
                If Population(i)(j) < SearchIntervals(j).Min_Value Then
                    Population(i)(j) = SearchIntervals(j).Min_Value '(SearchIntervals(j).Max_Value - SearchIntervals(j).Min_Value) * RandomGenerator.NextDouble() + SearchIntervals(j).Min_Value
                End If

                If Population(i)(j) > SearchIntervals(j).Max_Value Then
                    Population(i)(j) = SearchIntervals(j).Max_Value '(SearchIntervals(j).Max_Value - SearchIntervals(j).Min_Value) * RandomGenerator.NextDouble() + SearchIntervals(j).Min_Value
                End If
            Next

            'Calculate the objective function
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            fitnessNew = fitnessValue

            'Update if the solution improves, or not too loud
            If (fitnessNew < fitness_min) AndAlso (RandomGenerator.NextDouble() > A(i)) Then
                BestSol = Population(i)
                fitness_min = fitnessNew
                'A(i) = Alpha * A(i)
                'R(i) = R0 * (1 - Math.Exp((-1 * Gamma * CurrentIteration)))
            End If

        Next

        _BestChart.Add(fitness_min)
        _CurrentBestFitness = fitness_min
        _BestSolution = BestSol
    End Sub

    Public Overrides Sub InitializeOptimizer()
        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)

        N = PopulationSize_N - 1
        D = Dimensions_D - 1

        A = New Double(N) {}
        R = New Double(N) {}
        F = New Double(N) {}
        V = New Double(N, D) {}
        Fitness = New Double(N) {}

        For i = 0 To N
            A(i) = A0
            R(i) = R0
        Next

        'Inintilize search population
        InitializePopulation()

        ' Compute fitnesses
        For i As Integer = 0 To N
            'fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            Fitness(i) = fitnessValue
        Next

        fitness_min = Fitness.Min()
        Iindex = Array.IndexOf(Fitness, fitness_min)
        _BestChart.Add(fitness_min)
        BestSol = Population(Iindex)

    End Sub


    Public Sub Space_Bound()
        'from matlab site :
        'https://www.mathworks.com/matlabcentral/answers/311735-hi-i-try-to-convert-this-matlab-code-to-vb-net-or-c-codes-help-me-please

        'Dim rand As New Random()

        Dim Tp(D) As Int32
        Dim Tm(D) As Int32
        Dim TpTildeTm(D) As Int32
        Dim value As Integer
        Dim TmpArray(D) As Double
        Dim randiDimm(D) As Double

        For i As Integer = 0 To N

            For j As Integer = 0 To D
                If Population(i)(j) > SearchIntervals.Item(j).Max_Value Then
                    Tp(j) = 1I
                Else
                    Tp(j) = 0I
                End If

                If Population(i)(j) < SearchIntervals.Item(j).Min_Value Then
                    Tm(j) = 1I
                Else
                    Tm(j) = 0I
                End If

                value = Tp(j) + Tm(j)

                If value = 0 Then
                    TpTildeTm(j) = 1I
                Else
                    TpTildeTm(j) = 0I
                End If
            Next

            '------------------------------------
            For j As Integer = 0 To D
                TmpArray(j) = Population(i)(j) * TpTildeTm(j)
            Next
            '-----------------------------------
            For t = 0 To D
                randiDimm(t) = (((SearchIntervals.Item(t).Max_Value - SearchIntervals.Item(t).Min_Value) * RandomGenerator.NextDouble()) + SearchIntervals.Item(t).Min_Value) * (Tp(t) + Tm(t))

            Next

            For t = 0 To D
                Population(i)(t) = TmpArray(t) + randiDimm(t)
            Next
        Next

    End Sub


    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub
End Class
