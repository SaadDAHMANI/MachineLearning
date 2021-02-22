Imports System

'Namespace MonoObjectiveEOALib

Public Class GSA_Optimizer
    Inherits EvolutionaryAlgoBase

    ''' <summary>
    ''' Algo constructor. This constructor initialize the search population.
    ''' </summary>
    ''' <param name="populationSize">Search population size</param>
    ''' <param name="searchSpaceDimension">Search space dimension</param>
    ''' <param name="searchSpaceIntervals">Lower and upper bounds of each search space dimension</param>
    ''' <param name="Go">GSA Parameter</param>
    ''' <param name="alpha">GSA Parameter </param>
    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceIntervals As List(Of Interval), Go As Double, alpha As Double)
        PopulationSize_N = populationSize
        Dimensions_D = searchSpaceDimension
        SearchIntervals = searchSpaceIntervals
        G0 = Go
        Me.Alpha = alpha

        InitializePopulation()
    End Sub
    Public Sub New(gO As Double, alpha_g As Double)
        G0 = gO
        Me.Alpha = alpha_g
    End Sub

    Public Overrides ReadOnly Property BestSolution As Double()
        Get
            Return BestLine
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

    Dim mSolution_Fitness As Dictionary(Of String, Double)
    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
        Get
            Return mSolution_Fitness
        End Get
    End Property

    Dim _CurrentBestFitness As Double
    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Return _CurrentBestFitness
        End Get
    End Property

#Region "GSA_Optimization"
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

    Public Overrides Sub RunEpoch()

        '0: Checking allowable range :
        Space_Bound()

        Dim test = Population
        '1: Fitness Evaluation :
        'Dim fitness As Double() = EvaluateF()
        EvaluateFitness(Me.Fitness)
        '-----------------------
        'Show_X_D(fitness, "----------------->Fitness<----------------")

        '-----------------------
        If OptimizationType = OptimizationTypeEnum.Minimization Then
            best = Fitness.Min
            best_X = Array.IndexOf(Fitness, best)
        Else
            best = Fitness.Max
            best_X = Array.IndexOf(Fitness, best)
        End If

        If CurrentIteration = 1 Then
            Fbest = best
            GetBestLine(Population, Lbest, best_X)
        End If

        If OptimizationType = OptimizationTypeEnum.Minimization Then
            '%minimization.
            If best < Fbest Then
                Fbest = best
                GetBestLine(Population, Lbest, best_X)

            End If

        Else
            '%maximization
            If best > Fbest Then
                Fbest = best
                GetBestLine(Population, Lbest, best_X)
            End If

        End If

        Me.BestChart.Add(Fbest)

        meanValue = Get_MeanValue(Fitness)
        worstValue = Get_WorstValue(Fitness)

        Me.MeanChart.Add(meanValue)
        Me.WorstChart.Add(worstValue)


        '2: Calculation of M. eq.14-20 :
        Mass_Calculation(M, Fitness)

        'Show_X(M, "Masses :")

        '3 : Calculation of Gravitational constant. eq.13 :
        Gvalue = Gconstant(CurrentIteration, MaxIterations)

        ' Console.WriteLine(Gvalue.ToString())

        '4: Calculation of accelaration in gravitational field. eq.7-10,21 :
        Gfield(CurrentIteration, M, Ms, Ds, E, accelerations, Gvalue)

        '5: Agent movement. eq.11-12
        Move(Population, accelerations, V)

        _CurrentBestFitness = Fbest
        BestLine = Lbest

    End Sub


    Private BestLine() As Double
    Private Positions() As Double

    Public Property Alpha As Double = 20.0R
    Public Property G0 As Double = 100.0R

    Private Const Eps As Double = 0.00000000000000022204
    Private N As Integer
    Private D As Integer
    Private Rnorme As Integer = 2I
    Private Rpower As Integer = 1I

    Dim best As Double = Double.NaN
    Dim best_X As Integer = 0I
    Dim Fbest As Double = Double.NaN
    Dim Lbest() As Double 'Best line (solution)

    Dim meanValue As Double = Double.NaN
    Dim worstValue As Double = Double.NaN
    '---------------------------------
    Dim M() As Double
    Dim Ms() As Double
    Dim Ds() As Integer
    '---------------------------------
    Dim Gvalue As Double = Double.NaN
    Dim accelerations(,) As Double
    Dim E(,) As Double
    Dim V(,) As Double

    Private Fitness() As Double = Nothing

    ''' <summary>
    ''' ElitistCheck: If ElitistCheck=1, algorithm runs with eq.21 and if =0, runs with eq.9.
    ''' </summary>
    ''' <returns></returns>
    Public Property ElitistCheck As GSAElitistCheckEnum = GSAElitistCheckEnum.Equation21

    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Return "GSA"
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Return "Gravitational Search Algorithm"
        End Get
    End Property

    Public Overrides Sub InitializeOptimizer()

        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)

        D = Dimensions_D - 1
        N = PopulationSize_N - 1

        BestLine = New Double(D) {}
        Positions = New Double(D) {}
        Fitness = New Double(N) {}

        '-------------------------------------------
        Lbest = New Double(D) {} 'Best line (solution)
        M = New Double(N) {}
        Ms = New Double(N) {}
        Ds = New Integer(N) {}
        accelerations = New Double(N, D) {}
        E = New Double(N, D) {}
        V = New Double(N, D) {}

        InitializePopulation()
    End Sub



#Region "Fitness_Evaluation"

    Dim fitnessValue As Double = 0R

    ''' <summary>
    ''' Evaluate fintess for All
    ''' </summary>
    ''' <param name="fitnessArray">Fitness array of Agents X(N,D)</param>
    Private Sub EvaluateFitness(ByRef fitnessArray() As Double)
        For i = 0 To N
            fitnessValue = Double.NaN
            ComputeObjectiveFunction(Population(i), fitnessValue)
            fitnessArray(i) = fitnessValue
        Next
    End Sub

    Private Sub GetBestLine(ByRef Xx As Double()(), lbest() As Double, ByVal best_x As Integer)
        For i As Integer = 0 To Me.D
            lbest(i) = Xx(best_x)(i)
        Next
    End Sub


    Private Function Get_MeanValue(ByRef fitness As Double()) As Double

        Dim meanValue As Double = Double.NaN
        Dim dimI As Integer = fitness.GetLength(0)
        Dim sum As Double = fitness.Sum
        meanValue = (sum / dimI)
        Return meanValue

    End Function

    Private Function Get_WorstValue(ByRef fitness As Double()) As Double

        Dim worstValue As Double = Double.NaN

        If Me.OptimizationType = OptimizationTypeEnum.Minimization Then
            worstValue = Double.MinValue
            For i As Integer = 0 To (fitness.GetLength(0) - 1)
                If worstValue < fitness(i) Then
                    worstValue = fitness(i)
                End If
            Next
        Else
            worstValue = Double.MaxValue
            For i As Integer = 0 To (fitness.GetLength(0) - 1)
                If worstValue > fitness(i) Then
                    worstValue = fitness(i)
                End If
            Next
        End If

        Return worstValue

    End Function

    Private Sub Mass_Calculation(ByRef massM() As Double, ByRef fitnes() As Double)
        If IsNothing(massM) Then Return
        If IsNothing(fitnes) Then Return

        Dim fmax As Double = fitnes.Max
        Dim fmin As Double = fitnes.Min

        If fmin = fmax Then
            For i As Int32 = 0 To Me.N
                massM(i) = 1.0R
            Next

        Else
            Dim best, worst As Double

            If OptimizationType = OptimizationTypeEnum.Minimization Then
                best = fmin
                worst = fmax
            Else
                best = fmax
                worst = fmin
            End If

            Dim denominator As Double = (best - worst)

            For i As Integer = 0 To Me.N
                massM(i) = (fitnes(i) - worst) / denominator
            Next

            Dim sumMi As Double = massM.Sum

            For i As Integer = 0 To Me.N
                massM(i) = massM(i) / sumMi
            Next

        End If

    End Sub

    Private Function Gconstant(ByVal iteration As Integer, ByVal max_it As Integer) As Double
        Dim gValue As Double = 0R
        Dim expose As Double = (-1 * Me.Alpha * iteration) / max_it
        gValue = G0 * Math.Exp(expose)
        Return gValue
    End Function

    Private Sub Gfield(ByVal iteration As Integer, ByRef M() As Double, ByRef Ms() As Double, ByRef Ds() As Integer, ByRef E(,) As Double, ByRef accelerations(,) As Double, ByRef Gval As Double)

        Dim final_per = 2
        Dim kbestDbl As Double
        Dim kbest As Integer

        'total force calculation :

        If Me.ElitistCheck = GSAElitistCheckEnum.Equation9 Then

            kbest = N

        ElseIf Me.ElitistCheck = GSAElitistCheckEnum.Equation21 Then

            kbestDbl = final_per + ((1 - (iteration / MaxIterations)) * (100 - final_per))
            kbestDbl = (N * kbestDbl) / 100
            kbest = Convert.ToInt32(Math.Round(kbestDbl))
        End If

        'Descend
        Sort(M, Ms, Ds)
        '----------------------------------------------------------
        Dim j As Integer = 0I
        Dim R As Double = 0R

        For i As Integer = 0 To N
            'Initialisation :
            For t As Integer = 0 To D
                E(i, t) = 0R
            Next

            For ii As Integer = 0 To (kbest - 1)
                j = Ds(ii)

                If i <> j Then
                    R = Norm(Population, i, j)
                    R = (R ^ Rpower) + Eps

                    For k = 0 To Me.D
                        ' E(i, k) = E(i, k) + (0.5 * (M(j) * ((X(j, k) - X(i, k)) / R)))
                        E(i, k) = E(i, k) + (RandomGenerator.NextDouble() * (M(j) * ((Population(j)(k) - Population(i)(k)) / R)))
                    Next

                End If
            Next
        Next
        'Acceleration
        'a = E.* G; %note that Mp(i)/Mi(i)=1
        For s As Integer = 0 To N
            For t As Integer = 0 To D
                accelerations(s, t) = Gval * E(s, t)
            Next
        Next

        'Show_X(E, "E := ")
        'Show_X(accelerations, "Accelerations := ")

    End Sub

    Function Norm(ByRef Xij As Double()(), ByRef iIndex As Integer, ByRef jIndex As Integer, Optional norme As Integer = 2) As Double
        Dim result As Double = 0R
        If IsNothing(Xij) Then Return Nothing
        Try
            Dim summ As Double = 0R
            Dim tmpValue As Double = 0R

            For j As Integer = 0 To D

                tmpValue = (Xij(iIndex)(j) - Xij(jIndex)(j))
                summ += tmpValue ^ norme
            Next

            result = Math.Sqrt(summ)

        Catch ex As Exception
            Throw ex
        End Try
        Return result
    End Function

    Private Sub Move(ByRef Xx As Double()(), ByRef accelerations(,) As Double, ByRef V(,) As Double)

        For i As Integer = 0 To N
            For j As Integer = 0 To D
                V(i, j) = ((MyBase.RandomGenerator.NextDouble() * V(i, j))) + accelerations(i, j)
                'V(i, j) = (0.5 * V(i, j)) + accelerations(i, j)
            Next
        Next
        For r As Integer = 0 To Me.N
            For s As Integer = 0 To Me.D
                Xx(r)(s) = Xx(r)(s) + V(r, s)
            Next
        Next

    End Sub

    Private Sub Sort(ByRef M() As Double, ByRef Ms() As Double, ByRef Ds() As Integer)

        If IsNothing(M) Then Return
        If IsNothing(Ms) Then Return
        If IsNothing(Ds) Then Return

        Dim tmpM(Me.N) As Double

        For i As Integer = 0 To Me.N
            Ds(i) = i
            tmpM(i) = M(i)
        Next

        Dim minValue As Double = (tmpM.Min() - 10)
        Dim maxValue As Double = (tmpM.Max)

        For i As Integer = 0 To Me.N

            For j = 0 To Me.N

                maxValue = tmpM.Max

                If tmpM(j) = maxValue Then

                    Ms(i) = maxValue

                    Ds(i) = j

                    tmpM(j) = minValue

                    Exit For
                End If
            Next
        Next

    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub


#End Region

#End Region

End Class

Public Enum GSAElitistCheckEnum
    Equation9 = 0
    Equation21 = 1
End Enum
