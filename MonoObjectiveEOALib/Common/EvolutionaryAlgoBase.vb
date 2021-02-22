Imports System
Imports System.Collections
Public MustInherit Class EvolutionaryAlgoBase
    Implements IEvolutionaryAlgo

#Region "Common of Interface: IEvolutionaryAlgo"

    ''' <summary>
    ''' Algo constructor.
    ''' </summary>
    ''' <param name="searchAgentCount"></param>
    ''' <param name="searchSpaceDimension"></param>
    ''' <param name="algoParams"></param>
    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceIntervals As List(Of Interval))
        PopulationSize_N = populationSize
        Dimensions_D = searchSpaceDimension
        SearchIntervals = searchSpaceIntervals
        'InitializePopulation()
    End Sub
    Public Sub New()
    End Sub

    Public MustOverride ReadOnly Property AlgorithmName Implements IEvolutionaryAlgo.AlgorithmName
    Public MustOverride ReadOnly Property AlgorithmFullName Implements IEvolutionaryAlgo.AlgorithmFullName

    Dim _Dimensions_D As Integer = 1
    ''' <summary>
    ''' Search Space dimension.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property Dimensions_D As Integer Implements IEvolutionaryAlgo.Dimensions_D
        Get
            Return _Dimensions_D
        End Get
        Set(value As Integer)
            _Dimensions_D = Math.Max(value, 1)
        End Set
    End Property

    Dim Intervals As List(Of Interval)
    ''' <summary>
    ''' Search space intervals.
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Property SearchIntervals As List(Of Interval) Implements IEvolutionaryAlgo.SearchIntervals
        Get
            Return Intervals
        End Get
        Set(value As List(Of Interval))
            Intervals = value
        End Set
    End Property

    Dim iterationsMax As Integer
    Public Overridable Property MaxIterations As Integer Implements IEvolutionaryAlgo.MaxIterations
        Get
            Return iterationsMax
        End Get
        Set(value As Integer)
            iterationsMax = Math.Max(value, 1)
        End Set
    End Property

    Dim mOptimizationType As OptimizationTypeEnum = OptimizationTypeEnum.Minimization
    Public Property OptimizationType As OptimizationTypeEnum Implements IEvolutionaryAlgo.OptimizationType
        Get
            Return mOptimizationType
        End Get
        Set(value As OptimizationTypeEnum)
            mOptimizationType = value
        End Set
    End Property

    Dim Chronos As Stopwatch
    ''' <summary>
    ''' Comutation time in Milliseconds MS.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ComputationTime As Long Implements IEvolutionaryAlgo.ComputationTime
        Get
            If Object.Equals(Chronos, Nothing) Then
                Return 0
            Else
                Return Chronos.ElapsedMilliseconds
            End If
        End Get
    End Property

    Public MustOverride ReadOnly Property BestSolution As Double() Implements IEvolutionaryAlgo.BestSolution
    Public MustOverride ReadOnly Property BestChart As List(Of Double) Implements IEvolutionaryAlgo.BestChart
    Public MustOverride ReadOnly Property WorstChart As List(Of Double) Implements IEvolutionaryAlgo.WorstChart
    Public MustOverride ReadOnly Property MeanChart As List(Of Double) Implements IEvolutionaryAlgo.MeanChart
    Public MustOverride ReadOnly Property Solution_Fitness As Dictionary(Of String, Double) Implements IEvolutionaryAlgo.Solution_Fitness
    Public MustOverride ReadOnly Property CurrentBestFitness As Double Implements IEvolutionaryAlgo.CurrentBestFitness
    Public Overridable ReadOnly Property BestScore As Double Implements IEvolutionaryAlgo.BestScore
        Get
            If (Equals(BestChart, Nothing) OrElse (BestChart.Count = 0)) Then
                Return Double.NaN
            Else
                Return BestChart.Last()
            End If
        End Get
    End Property

    Dim populationSizeN As Integer = 1
    Public Property PopulationSize_N As Integer Implements IEvolutionaryAlgo.PopulationSize_N
        Get
            Return populationSizeN
        End Get
        Set(value As Integer)
            populationSizeN = Math.Max(value, 1)
        End Set
    End Property

    Private mPopulation As Double()()
    Public Property Population As Double()() Implements IEvolutionaryAlgo.Population
        Get
            Return mPopulation
        End Get
        Set(value As Double()())
            mPopulation = value
        End Set
    End Property

    Dim _CurrentIteration As Integer
    Public Overridable ReadOnly Property CurrentIteration As Integer Implements IEvolutionaryAlgo.CurrentIteration
        Get
            Return _CurrentIteration
        End Get
    End Property

    Public MustOverride Sub RunEpoch() Implements IEvolutionaryAlgo.RunEpoch
    Public MustOverride Sub InitializeOptimizer()
    Public Overridable Sub LuanchComputation() Implements IEvolutionaryAlgo.LuanchComputation

        RaiseEvent OptimizationStarting(Me, New EventArgs)


        If Object.Equals(Chronos, Nothing) Then
            Chronos = New Stopwatch()
            Chronos.Start()
        Else
            Chronos.Restart()
        End If

        ''Initialize the optimizer
        InitializeOptimizer()

        _CurrentIteration = 1

        For i As Integer = 0 To (MaxIterations - 1)
            RunEpoch()
            _CurrentIteration += 1
        Next

        Chronos.Stop()

        RaiseEvent OptimizationTerminated(Me, New EventArgs)
    End Sub

    Public Overridable Sub InitializePopulation() Implements IEvolutionaryAlgo.InitializePopulation
        InitializePopulation(SolutionsInitializationStrategyEnum.Random)
    End Sub

    Public Shared RandomGenerator As Random
    Public Overridable Sub InitializePopulation(strategy As SolutionsInitializationStrategyEnum)

        'Check intervals count
        If (Me.Intervals.Count < _Dimensions_D) Then
            Throw New Exception("Search space interval must equal the search space dimension")
        End If

        Select Case strategy

            Case SolutionsInitializationStrategyEnum.Grid
                Throw New NotImplementedException()

            Case Else
                ' Random 
                If Object.Equals(RandomGenerator, Nothing) Then RandomGenerator = New Random()

                Population = New Double((populationSizeN - 1))() {}

                For i As Integer = 0 To (populationSizeN - 1)
                    Dim X = New Double((_Dimensions_D - 1)) {}

                    For j = 0 To (_Dimensions_D - 1)
                        X(j) = (Intervals.Item(j).Max_Value - Intervals.Item(j).Min_Value) * RandomGenerator.NextDouble() + Intervals.Item(j).Min_Value
                    Next
                    Population(i) = X
                Next
        End Select
    End Sub


    Public Event OptimizationStarting(sender As Object, e As EventArgs)
    Public Event OptimizationTerminated(sender As Object, e As EventArgs)
    Public Event ObjectiveFunction(positions() As Double, ByRef fitnessValue As Double) Implements IEvolutionaryAlgo.ObjectiveFunction
    Protected Overridable Sub OnObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
        RaiseEvent ObjectiveFunction(positions, fitnessValue)
    End Sub
    Public MustOverride Sub ComputeObjectiveFunction(positions() As Double, ByRef fitnessValue As Double)
#End Region

End Class
