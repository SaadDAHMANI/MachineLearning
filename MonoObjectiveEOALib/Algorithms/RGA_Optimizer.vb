Public Class RGA_Optimizer
    '    Inherits EvolutionaryAlgoBase

    '    Public Sub New()
    '    End Sub

    '    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceRanges As List(Of Range))
    '        PopulationSize_N = populationSize
    '        Dimensions_D = searchSpaceDimension
    '        SearchRanges = searchSpaceRanges
    '        PopulationLimit = populationSize
    '    End Sub

    '    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceRanges As List(Of Range), populationSizeLimit As Integer)
    '        PopulationSize_N = populationSize
    '        Dimensions_D = searchSpaceDimension
    '        SearchRanges = searchSpaceRanges
    '        PopulationLimit = populationSizeLimit
    '    End Sub

    '    Public Overrides ReadOnly Property AlgorithmName As Object
    '        Get
    '            Throw New NotImplementedException()
    '        End Get
    '    End Property

    '    Public Overrides ReadOnly Property AlgorithmFullName As Object
    '        Get
    '            Throw New NotImplementedException()
    '        End Get
    '    End Property

    '    Private _BestSolution As Double()
    '    Public Overrides ReadOnly Property BestSolution As Double()
    '        Get
    '            Return _BestSolution
    '        End Get
    '    End Property

    '    Private _BestChart As List(Of Double)
    '    Public Overrides ReadOnly Property BestChart As List(Of Double)
    '        Get
    '            Return _BestChart
    '        End Get
    '    End Property

    '    Private _WorstChart As List(Of Double)
    '    Public Overrides ReadOnly Property WorstChart As List(Of Double)
    '        Get
    '            Return _WorstChart
    '        End Get
    '    End Property

    '    Private _MeanChart As List(Of Double)
    '    Public Overrides ReadOnly Property MeanChart As List(Of Double)
    '        Get
    '            Return _MeanChart
    '        End Get
    '    End Property

    '    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
    '        Get
    '            Throw New NotImplementedException()
    '        End Get
    '    End Property

    '    Private _CurrentBestFitness As Double
    '    Public Overrides ReadOnly Property CurrentBestFitness As Double
    '        Get
    '            Return _CurrentBestFitness
    '        End Get
    '    End Property
    '#Region "Properties"

    '    Private _MutationFrequency As Single = 10.0F
    '    ''' <summary>
    '    ''' Mutation frequency in the range [0, 100[.
    '    ''' </summary>
    '    ''' <returns></returns>
    '    Public Property MutationFrequency As Single
    '        Get
    '            Return _MutationFrequency
    '        End Get
    '        Set(value As Single)
    '            _MutationFrequency = Math.Min(100, Math.Max(0, value))
    '        End Set
    '    End Property


    '    Private _PopulationLimit As Integer
    '    Public Property PopulationLimit As Integer
    '        Get
    '            Return _PopulationLimit
    '        End Get
    '        Set(value As Integer)
    '            _PopulationLimit = Math.Max(2, value)
    '        End Set
    '    End Property

    '    Public Property DeathFitness As Double
    '    'Public Property ReproductionFitness As Double

    '    Private _ReproductionFrequency As Single = 10.0F
    '    Public Property ReproductionFrequency As Single
    '        Get
    '            Return _ReproductionFrequency
    '        End Get
    '        Set(value As Single)
    '            _ReproductionFrequency = Math.Max(0, Math.Min(95, value))
    '        End Set
    '    End Property



    '#End Region


    '    Private Function CanDie(genome As Double()) As Boolean
    '        ComputeObjectiveFunction(genome, FitnessValue)
    '        If FitnessValue >= DeathFitness Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End Function

    '#Region "Private_variables"
    '    Private FitnessValue As Double
    '    Private CrossoverPoint As Integer
    '    Private MutationIndex As Integer
    '    Private CurrentPopulationSize As Integer
    '    Private N, D As Integer
    '    Private Genomes As List(Of Double())
    '    Private GenomeReproducers As List(Of Double())
    '    Private GenomeResults As List(Of Double())
    '    Private GenomeFamily As List(Of Double())
    '    Dim aGene1 As Double()
    '    Dim aGene2 As Double()

    '#End Region

    '    Public Overrides Sub RunEpoch()

    '        For i = 0 To N
    '            If (CanDie(Genomes(i))) Then
    '                Genomes.RemoveAt(i)
    '                i -= 1
    '            End If
    '        Next

    '        'determine who can reproduce
    '        GenomeReproducers.Clear()
    '        GenomeResults.Clear()

    '        For i As Integer = 0 To (Genomes.Count - 1)
    '            If RandomGenerator.Next(100) > _ReproductionFrequency Then
    '                GenomeReproducers.Add(Genomes(i))
    '            End If
    '        Next

    '        'do the crossover of the genes and add them to the population
    '        DoCrossover(GenomeReproducers)

    '    End Sub

    '    Private Sub DoCrossover(genes As List(Of Double()))
    '        Dim GeneMoms As New List(Of Double())
    '        Dim GeneDads As New List(Of Double())

    '        For i As Integer = 0 To (genes.Count - 1)
    '            '// randomly pick the moms And dad's
    '            If (RandomGenerator.Next(100) Mod 2) > 0 Then
    '                GeneMoms.Add(genes(i))
    '            Else
    '                GeneDads.Add(genes(i))
    '            End If
    '        Next

    '        ' //  now make them equal
    '        If (GeneMoms.Count > GeneDads.Count) Then
    '            While (GeneMoms.Count > GeneDads.Count)
    '                GeneDads.Add(GeneMoms((GeneMoms.Count - 1)))
    '                GeneMoms.RemoveAt((GeneMoms.Count - 1))
    '            End While

    '            If (GeneDads.Count > GeneMoms.Count) Then
    '                GeneDads.RemoveAt((GeneDads.Count - 1)) '; // make sure they are equal
    '            End If

    '        Else
    '            While (GeneDads.Count > GeneMoms.Count)
    '                GeneMoms.Add(GeneDads(GeneDads.Count - 1))
    '                GeneDads.RemoveAt((GeneDads.Count - 1))
    '            End While
    '            If (GeneMoms.Count > GeneDads.Count) Then
    '                '// make sure they are equal
    '                GeneMoms.RemoveAt((GeneMoms.Count - 1))
    '            End If
    '        End If

    '        '// now cross them over and add them according to fitness
    '        For i As Integer = 0 To (GeneDads.Count - 1)
    '            '	// pick best 2 from parent And children
    '            Dim babyGene1 = Crossover(GeneDads(i), GeneMoms(i))
    '            Dim babyGene2 = Crossover(GeneMoms(i), GeneDads(i))

    '            GenomeFamily.Clear()
    '            GenomeFamily.Add(GeneDads(i))
    '            GenomeFamily.Add(GeneMoms(i))
    '            GenomeFamily.Add(babyGene1)
    '            GenomeFamily.Add(babyGene2)
    '            CalculateFitnessForAll(GenomeFamily)
    '            GenomeFamily.Sort()

    '            If Best2 = True Then
    '                '// if Best2 Is true, add top fitness genes
    '                GenomeResults.Add(GenomeFamily(0))
    '                GenomeResults.Add(GenomeFamily(1))
    '            Else
    '                GenomeResults.Add(babyGene1)
    '                GenomeResults.Add(babyGene2)
    '            End If
    '        Next

    '    End Sub

    '    Public Function Crossover(g1 As Double(), g2 As Double()) As Double()
    '        CrossoverPoint = RandomGenerator.Next(D)

    '        For i As Integer = 0 To (CrossoverPoint - 1)
    '            aGene1(i) = g1(i)
    '            aGene2(i) = g2(i)
    '        Next
    '        '      
    '        For j As Int32 = CrossoverPoint To D
    '            aGene1(j) = g2(j)
    '            aGene2(j) = g1(j)
    '        Next

    '        '50/50 chance of returning gene1 Or gene2
    '        Dim aGene = New Double(D) {}
    '        If RandomGenerator.Next(2) = 1 Then
    '            For i = 0 To D
    '                aGene(i) = aGene1(i)
    '            Next
    '        Else
    '            For i = 0 To D
    '                aGene(i) = aGene2(i)
    '            Next
    '        End If
    '        Return aGene
    '    End Function


    '    Private Function CanReproduce(fitnessLimit As Double) As Boolean
    '        'If TheSeed.Next(100) >= (fitnessLimit * 100) Then
    '        If RandomGenerator.Next(100) >= fitnessLimit Then
    '            Return True
    '        End If
    '        Return False
    '    End Function
    '    Public Overrides Sub InitializeOptimizer()
    '        N = PopulationSize_N - 1
    '        D = Dimensions_D - 1
    '        _BestSolution = New Double(D) {}
    '        _BestChart = New List(Of Double)
    '        _MeanChart = New List(Of Double)
    '        _WorstChart = New List(Of Double)

    '        CurrentPopulationSize = PopulationSize_N

    '        '' Initilize the population
    '        InitializePopulation()

    '        '' Copie the population in the list of Genomes
    '        Genomes = New List(Of Double())
    '        For i = 0 To N
    '            Genomes.Add(Population(i))
    '        Next

    '        GenomeReproducers = New List(Of Double())
    '        GenomeResults = New List(Of Double())
    '        GenomeFamily = New List(Of Double())
    '        aGene1 = New Double(D) {}
    '        aGene2 = New Double(D) {}

    '    End Sub

    '    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
    '        MyBase.OnObjectiveFunction(positions, fitness_Value)
    '    End Sub


End Class
