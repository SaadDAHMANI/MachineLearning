Public Class DBA_Optimizer
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
            Return "DBA"
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Return "Directional Bat Algorithm"
        End Get
    End Property

    Private _BestSolution As Double()
    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Return _BestSolution
        End Get
    End Property

    Dim _BestChart As List(Of Double)
    Public Overrides ReadOnly Property BestChart As List(Of Double)
        Get
            Return _BestChart
        End Get
    End Property

    Dim _WorstChart As List(Of Double)
    Public Overrides ReadOnly Property WorstChart As List(Of Double)
        Get
            Return _WorstChart
        End Get
    End Property

    Dim _MeanChart As List(Of Double)
    Public Overrides ReadOnly Property MeanChart As List(Of Double)
        Get
            Return _MeanChart
        End Get
    End Property

    Dim _Solution_Fitness As Dictionary(Of String, Double)
    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
        Get
            Return _Solution_Fitness
        End Get
    End Property

    Dim _CurrentBestFitness As Double
    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Return _CurrentBestFitness
        End Get
    End Property

#Region "DBA params"

    Private _A0 As Double = 0.9
    Public Property A0 As Double
        Get
            Return _A0
        End Get
        Set(value As Double)
            _A0 = value
        End Set
    End Property

    Private _Ainf As Double = 0.6
    Public Property Ainf As Double
        Get
            Return _Ainf
        End Get
        Set(value As Double)
            _Ainf = value
        End Set
    End Property

    Private _R0 As Double = 0.1
    Public Property R0 As Double
        Get
            Return _R0
        End Get
        Set(value As Double)
            _R0 = value
        End Set
    End Property

    Private _Rinf As Double = 0.7
    Public Property Rinf As Double
        Get
            Return _Rinf
        End Get
        Set(value As Double)
            _Rinf = value
        End Set
    End Property

    Private N As Integer = 0
    Private D As Integer = 0

    Private W0 As Double()
    Private Winf As Double()
    Private A As Double()
    Private R As Double()
    Private Fit As Double()
    Private fitnessValue As Double
    Private Fmin As Double
    Private Fitinn As Double()
    Private Iindex As Integer
    Private Fnew As Double()

    Private ii As Integer = 0
    Private Best As Double()
    Private q As Double = 2
    Private W As Double(,)
    Private V As Double(,)
    Private X2 As Double()

#End Region


    Public Overrides Sub RunEpoch()

        For i As Integer = 0 To N

            ii = RandomGenerator.Next(0, (N + 1))

            While (ii = i)
                ii = RandomGenerator.Next(0, (N + 1))
            End While

            q = 2

            If Fit(ii) < Fit(i) Then
                For j = 0 To D
                    V(i, j) = (Population(ii)(j) - Population(i)(j)) * RandomGenerator.NextDouble() * q + (Best(j) - Population(i)(j)) * RandomGenerator.NextDouble() * q
                Next
            Else
                For j = 0 To D
                    V(i, j) = (Best(j) - Population(i)(j)) * RandomGenerator.NextDouble() * q
                Next
            End If

            For j = 0 To D
                X2(j) = Population(i)(j) + V(i, j)
            Next

            If RandomGenerator.NextDouble() > R(i) Then
                For j = 0 To D
                    'Equation 9 :
                    X2(j) = Population(i)(j) + (W(i, j) * A.Average() * RandomGenerator.Next(-1, 2) * RandomGenerator.NextDouble())

                    'Equation 10:
                    W(i, j) = ((W0(j) - Winf(j)) / (1 - MaxIterations)) * (CurrentIteration - MaxIterations) + Winf(j)
                Next
            End If

            For j = 0 To D
                If X2(j) < SearchIntervals(j).Min_Value Then
                    V(i, j) = -1 * V(i, j)
                    X2(j) = SearchIntervals(j).Min_Value
                End If

                If X2(j) > SearchIntervals(j).Max_Value Then
                    V(i, j) = -1 * V(i, j)
                    X2(j) = SearchIntervals(j).Max_Value
                End If
            Next

            fitnessValue = Double.NaN
            ComputeObjectiveFunction(X2, fitnessValue)
            Fnew(i) = fitnessValue

            If (Fnew(i) < Fit(i)) AndAlso (RandomGenerator.NextDouble() < A(i)) Then

                For j = 0 To D
                    Population(i)(j) = X2(j)
                Next
                Fit(i) = Fnew(i)

                'Equation 13:
                R(i) = ((R0 - Rinf) / (1 - MaxIterations)) * (CurrentIteration - MaxIterations) + Rinf

                'Equation 14:
                A(i) = ((A0 - Ainf) / (1 - MaxIterations)) * (CurrentIteration - MaxIterations) + Ainf
            End If

            If Fnew(i) <= Fmin Then
                Fmin = Fnew(i)
                For j = 0 To D
                    Best(j) = X2(j)
                Next
            End If
        Next
        _BestChart.Add(Fmin)
        _CurrentBestFitness = Fmin
        _BestSolution = Best
    End Sub

    Public Overrides Sub InitializeOptimizer()
        If SearchIntervals.Count < Dimensions_D Then Throw New Exception("Search space intervals must be equal search space dimension.")

        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)

        D = Dimensions_D - 1
        N = PopulationSize_N - 1
        W0 = New Double(D) {}
        Winf = New Double(D) {}
        A = New Double(N) {}
        R = New Double(N) {}
        Fit = New Double(N) {}
        X2 = New Double(D) {}
        W = New Double(N, D) {}
        V = New Double(N, D) {}
        Fnew = New Double(N) {}

        For j As Integer = 0 To D
            W0(j) = (SearchIntervals(j).Max_Value - SearchIntervals(j).Min_Value) / 4
            Winf(j) = W0(j) / 100
        Next

        For i As Integer = 0 To N
            A(i) = A0
            R(i) = R0
        Next

        'Inintilize population
        InitializePopulation()

        For i As Integer = 0 To N
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            Fit(i) = fitnessValue

            For j As Integer = 0 To D
                W(i, j) = W0(j)
                V(i, j) = 0
            Next
        Next

        Fmin = Fit.Min()
        Iindex = Array.IndexOf(Fit, Fmin)
        _BestChart.Add(Fmin)
        Best = Population(Iindex)
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub
End Class
