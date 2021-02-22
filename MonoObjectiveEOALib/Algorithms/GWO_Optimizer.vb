Imports System.ComponentModel

Public Class GWO_Optimizer
    Inherits EvolutionaryAlgoBase

    Public Sub New()
    End Sub

    Public Sub New(populationSize As Integer, searchSpaceDimension As Integer, searchSpaceIntervals As List(Of Interval))
        PopulationSize_N = populationSize
        Dimensions_D = searchSpaceDimension
        SearchIntervals = searchSpaceIntervals
        InitializePopulation()
    End Sub

    Dim _BestSolution As Double()
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

    Public Overrides ReadOnly Property Solution_Fitness As Dictionary(Of String, Double)
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Dim _CurrentBestFitness As Double
    Public Overrides ReadOnly Property CurrentBestFitness As Double
        Get
            Return _CurrentBestFitness
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmName As Object
        Get
            Return "GWO"
        End Get
    End Property

    Public Overrides ReadOnly Property AlgorithmFullName As Object
        Get
            Return "Grey Wolf Optimizer"
        End Get
    End Property

    Public Overrides Sub RunEpoch()
        If OptimizationType = OptimizationTypeEnum.Minimization Then
            RunEpoch_Minimization()
        Else
            'RunEpoch_Maximization()
        End If

    End Sub

    Public Overrides Sub InitializeOptimizer()
        _BestChart = New List(Of Double)
        _MeanChart = New List(Of Double)
        _WorstChart = New List(Of Double)
        _BestSolution = New Double(Dimensions_D - 1) {}

        If OptimizationType = OptimizationTypeEnum.Minimization Then
            bestObjectiveFunct = Double.MaxValue
            value_Alpha = Double.MaxValue  'la valeur de loup Alpha.
            value_Beta = Double.MaxValue 'la valeur de loup Beta.
            value_Delta = Double.MaxValue 'la valeur de loup Delta.

        Else
            bestObjectiveFunct = Double.MinValue
            value_Alpha = Double.MinValue  'la valeur de loup Alpha.
            value_Beta = Double.MinValue 'la valeur de loup Beta.
            value_Delta = Double.MinValue 'la valeur de loup Delta.

        End If

        bestPositions = New Double(Dimensions_D - 1) {}
        objectiveFunctValues = New Double(PopulationSize_N - 1) {}
        Positions_Alpha = New Double(Dimensions_D - 1) {}
        Positions_Beta = New Double(Dimensions_D - 1) {}
        Positions_Delta = New Double(Dimensions_D - 1) {}
        '-----------------------------------------------------------------
        ''1. faire des bouclages autant que le nombre de loup (points 1a - 1b).
        'For i As Integer = 0 To (PopulationSize_N - 1)
        '    'daftarPosisi : Liste des positions
        '    positionsList(i) = New Double((Dimensions_D - 1)) {}

        '    '1 a. donner des positions al�atoires sur chacun des loups gris,
        '    ' autant que le nombre de dimensions
        '    'Calculer ensuite la valeur de la fonction � cette position
        '    'Description plus de d�tails sur cette fonction peut �tre vu
        '    ' dans l'explication du script ci-dessous.

        '    For j As Integer = 0 To (Dimensions_D - 1)
        '        positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
        '    Next j

        '    objectiveFunctValues(i) = ObjectiveFunction(positionsList(i))

        '    '-------------------------------------------------------------------------------------

        '    '1 b. Si la valeur d'une position al�atoire est meilleure que la meilleure,
        '    ' la valeur de fonction 
        '    'alors, prenez cette position comme la meilleure position tout en
        '    If objectiveFunctValues(i) < bestObjectiveFunct Then
        '        bestObjectiveFunct = objectiveFunctValues(i)
        '        Array.Copy(positionsList(i), bestPositions, dimensionD)
        '        bestPositionIndex = i
        '    End If
        'Next i

        InitializePopulation()
    End Sub

    Public Overrides Sub ComputeObjectiveFunction(positions() As Double, ByRef fitness_Value As Double)
        MyBase.OnObjectiveFunction(positions, fitness_Value)
    End Sub

#Region "Optimization By GWO"

    'PosisiTerbaik : La meilleure position
    'Dim bestPositions(dimensionD - 1) As Double
    Dim bestPositions() As Double

    'nilaiFungsiTerbaik : la valeur de la fonction est la meilleure
    Dim bestObjectiveFunct As Double = Double.MaxValue

    'daftarPosisi : Liste des positions
    'Dim positionsList As Double()() = New Double((wolfCountN - 1))() {}
    'Dim positionsList As Double()()

    'nilaiFungsi : la valeur de la fonction
    'Dim objectiveFunctValues(wolfCountN - 1) As Double
    Dim objectiveFunctValues() As Double

    '*L'initialisation du loup gris qui a utilis� autant que le nombre de param�tres du Loup
    'Console.WriteLine("l'initialisation des Loup gris sur de la position al�atoire")

    'indeksPosisiTerbaik : le meilleur indice de position
    Dim bestPositionIndex As Integer = -1

    'Dim Positions_Alpha(dimensionD - 1) As Double
    Dim Positions_Alpha() As Double
    Dim value_Alpha As Double = Double.MaxValue 'la valeur de loup Alpha.

    'Dim Positions_Beta(dimensionD - 1) As Double
    Dim Positions_Beta() As Double
    Dim value_Beta As Double = Double.MaxValue 'la valeur de loup Beta.

    'Dim Positions_Delta(dimensionD - 1) As Double
    Dim Positions_Delta() As Double
    Dim value_Delta As Double = Double.MaxValue 'la valeur de loup Delta.

    '---------------------------------- Variables -------------------------------
    Dim newObjectiveFunctValue As Double = 0R
    Dim tmpa As Double = 0R
    Dim a As Double = 0R
    Dim r1 As Double = 0R
    Dim r2 As Double = 0R

    Dim A_Alpha As Double = 0
    Dim C_Alpha As Double = 0
    Dim D_Alpha As Double = 0
    Dim X_Alpha As Double = 0

    Dim A_Beta As Double = 0
    Dim C_Beta As Double = 0
    Dim D_Beta As Double = 0
    Dim X_Beta As Double = 0

    Dim A_Delta As Double = 0
    Dim C_Delta As Double = 0
    Dim D_Delta As Double = 0
    Dim X_Delta As Double = 0
    '----------------------------------------------------------------------------
    Dim worstFitness As Double = 0R
    Dim meanFitness As Double = 0R
    '----------------------------------------------------------------------------

    Private Sub RunEpoch_Minimization()
        Try


            '---------------------------------
            worstFitness = Double.MinValue
            meanFitness = 0R
            '---------------------------------

            '2a. a fait le calcul sur chaque loup gris (points 2a1 - 2a6))�
            For i As Integer = 0 To (Population.Length - 1)

                '2a1. Faire le calcul sur les positions respectives du loup gris
                'Si une position sur le calcul pr�c�dent s'est av�r�e �tre en dehors
                ' des limites de la position qui sont autoris�s,
                ' puis la valeur de retour afin de s'adapter � la limite
                For j As Integer = 0 To Population(i).Length - 1
                    If Population(i)(j) < SearchIntervals(j).Min_Value Then
                        'positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
                        Population(i)(j) = SearchIntervals(j).Min_Value

                    ElseIf Population(i)(j) > SearchIntervals(j).Max_Value Then
                        'Population(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
                        Population(i)(j) = SearchIntervals(j).Max_Value

                    End If
                Next j

                '2a2. Calculer la valeur de la fonction � cette position
                'nilaiFungsiBaru : la valeur des nouvelles fonctions
                'Dim newObjectiveFunctValue As Double = ObjectiveFunction(Population(i))

                newObjectiveFunctValue = ObjectiveFunctionComputation(Population(i))
                'Debug.Print("It={0}, W={1}, Fit = {2} ", iteration, i, newObjectiveFunctValue.ToString())

                '2a3. Si la valeur de la nouvelle fonction est meilleure que la valeur alpha,
                ' alors prenez la position comme position alpha de la meilleure

                If newObjectiveFunctValue < value_Alpha Then

                    value_Alpha = newObjectiveFunctValue

                    Positions_Alpha = Population(i)

                End If

                '2a4. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha,
                ' mais il est mieux que la valeur b�ta, puis prendre cette position que la position 
                ' de la meilleure Beta.
                If newObjectiveFunctValue > value_Alpha AndAlso newObjectiveFunctValue < value_Beta Then

                    value_Beta = newObjectiveFunctValue

                    Positions_Beta = Population(i)

                End If

                '2a5. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha et b�ta, 
                ' mais mieux que la valeur Delta, puis prendre cette position comme la meilleure position 
                ' Delta.
                If newObjectiveFunctValue > value_Alpha AndAlso newObjectiveFunctValue > value_Beta AndAlso newObjectiveFunctValue < value_Delta Then

                    value_Delta = newObjectiveFunctValue

                    Positions_Delta = Population(i)

                End If

                'Si la valeur alpha alors qu'il s'av�re mieux que les valeurs de fonction en g�n�ral,
                ' puis prendre la position alpha comme la meilleure position
                If value_Alpha < bestObjectiveFunct Then

                    Array.Copy(Positions_Alpha, bestPositions, Dimensions_D)

                    bestObjectiveFunct = value_Alpha

                End If

                '-------------Me-----------
                If worstFitness < newObjectiveFunctValue Then
                    worstFitness = newObjectiveFunctValue
                End If

                meanFitness += newObjectiveFunctValue
                '--------------------------

            Next i

            '2b. Sp�cifiez la valeur d'un
            'Un a aura des valeurs initiales 2 et diminuera graduellement vers le 0 autant 
            ' de fois que le nombre d'it�rations

            a = 2 * (1 - (CurrentIteration / MaxIterations))

            '2c. Faire le calcul � chaque position du loup gris qui existe (poin 2c1 - 2c4)
            For i As Integer = 0 To (Population.Length - 1)

                For j As Integer = 0 To (Population(i).Length - 1)

                    '2c1. Faire le calcul pour calculer la valeur de la X_Alpha (poin 2c1a - 2c1e)

                    '2c1a. Specifiez une valeur aleatoire de 2  utiliser dans le calcul de

                    r1 = RandomGenerator.NextDouble()
                    r2 = RandomGenerator.NextDouble()

                    '2c1b. Calculer la valeur d'un alpha avec la formule : A1=2*a*r1-a
                    'Dim A_Alpha As Double = 2 * a * r1 - a

                    A_Alpha = (2 * a * r1) - a

                    '2c1c. Calculer la valeur de l'alpha C avec la formule : C1=2*r2
                    'Dim C_Alpha As Double = 2 * r2
                    C_Alpha = (2 * r2)

                    '2c1d. Calculer la valeur de D, alpha avec la formule : D_alpha=abs(C1*posisiAlpha(j)-daftarPosisi(i,j))
                    'Dim D_Alpha As Double = Math.Abs(C_Alpha * Positions_Alpha(j) - Population(i)(j))
                    D_Alpha = Math.Abs(((C_Alpha * Positions_Alpha(j)) - Population(i)(j)))

                    '2c1e. Calculer la valeur de X alpha avec la formule : X1=posisiAlpha(j)-A1*D_alpha;
                    'Dim X_Alpha As Double = Positions_Alpha(j) - A_Alpha * D_Alpha
                    X_Alpha = Positions_Alpha(j) - (A_Alpha * D_Alpha)

                    '2c2. Faire le m�me calcul (2c1 points) pour calculer la valeur de X Beta
                    r1 = RandomGenerator.NextDouble()
                    r2 = RandomGenerator.NextDouble()

                    A_Beta = (2 * a * r1) - a
                    C_Beta = (2 * r2)
                    D_Beta = Math.Abs(((C_Beta * Positions_Beta(j)) - Population(i)(j)))
                    X_Beta = Positions_Beta(j) - (A_Beta * D_Beta)

                    '2c3. Faire le m�me calcul (point 2c1) pour calculer la valeur de Delta X
                    r1 = RandomGenerator.NextDouble()
                    r2 = RandomGenerator.NextDouble()

                    A_Delta = (2 * a * r1) - a
                    C_Delta = (2 * r2)
                    D_Delta = Math.Abs(((C_Delta * Positions_Delta(j)) - Population(i)(j)))
                    X_Delta = Positions_Delta(j) - (A_Delta * D_Delta)

                    '2c4. Calculez la nouvelle valeur de position avec la formule : daftarPosisi(i,j)=(X1+X2+X3)/3
                    Population(i)(j) = (X_Alpha + X_Beta + X_Delta) / 3
                Next j
            Next i
            '-----------------Best Chart : Best fitness evolution----------------
            _CurrentBestFitness = value_Alpha
            _BestChart.Add(value_Alpha)
            _WorstChart.Add(worstFitness)
            _MeanChart.Add((meanFitness / PopulationSize_N))

            '--------------------------The best solution-------------------------------------------
            For ss As Integer = 0 To (Dimensions_D - 1)
                BestSolution(ss) = bestPositions(ss)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Dim fitnessValue As Double = 0R
    Private Function ObjectiveFunctionComputation(ByRef positions() As Double) As Double
        fitnessValue = Double.NaN
        ComputeObjectiveFunction(positions, fitnessValue)
        Return fitnessValue
    End Function

    'Private Sub RunEpoch_Maximization()
    '    Try
    '        If iteration = 0 Then
    '            Initialize()
    '        End If

    '        ''1. faire des bouclages autant que le nombre de loup (points 1a - 1b).
    '        'For i As Integer = 0 To (wolfCountN - 1)
    '        '    'daftarPosisi : Liste des positions
    '        '    positionsList(i) = New Double(dimensionD - 1) {}

    '        '    '1 a. donner des positions al�atoires sur chacun des loups gris,
    '        '    ' autant que le nombre de dimensions
    '        '    'Calculer ensuite la valeur de la fonction � cette position
    '        '    'Description plus de d�tails sur cette fonction peut �tre vu
    '        '    ' dans l'explication du script ci-dessous.

    '        '    For j As Integer = 0 To (dimensionD - 1)

    '        '        positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
    '        '    Next j

    '        '    objectiveFunctValues(i) = ObjectiveFunction(positionsList(i))

    '        '    REM------------------------------------------------------------------------------------------
    '        '    'Console.Write("Grey Wolf " & (i + 1).ToString.PadRight(2) & ", ")
    '        '    'Console.Write("Position de la : [")

    '        '    'For j As Integer = 0 To positionsList(i).Length - 1
    '        '    '    Console.Write(positionsList(i)(j).ToString("F4").PadLeft(7) & " ")
    '        '    'Next j

    '        '    'Console.Write("], ")
    '        '    'Console.WriteLine("la valeur de la fonction = " & objectiveFunctValues(i).ToString("F2"))
    '        '    REM------------------------------------------------------------------------------------------

    '        '    '1 b. Si la valeur d'une position al�atoire est meilleure que la meilleure,
    '        '    ' la valeur de fonction 
    '        '    'alors, prenez cette position comme la meilleure position tout en
    '        '    If objectiveFunctValues(i) > bestObjectiveFunct Then
    '        '        bestObjectiveFunct = objectiveFunctValues(i)
    '        '        Array.Copy(positionsList(i), bestPositions, dimensionD)
    '        '        bestPositionIndex = i
    '        '    End If
    '        'Next i

    '        '---------------------------------
    '        worstFitness = Double.MaxValue
    '        meanFitness = 0R
    '        '---------------------------------
    '        REM------------------------------------------------------------------------------------------
    '        'Console.WriteLine("")
    '        'Console.WriteLine("La meilleure position")
    '        'Console.WriteLine("Grey Wolf " & bestPositionIndex & " la valeur de la fonction = " & bestObjectiveFunct.ToString("F2"))
    '        'Console.WriteLine("")
    '        REM------------------------------------------------------------------------------------------

    '        '* Effectuer le processus de recherche la meilleure position
    '        'Console.WriteLine("D�marrez le processus de recherche la meilleure position")

    '        '2a. a fait le calcul sur chaque loup gris (points 2a1 - 2a6))�
    '        For i As Integer = 0 To (positionsList.Length - 1)

    '            '2a1. Faire le calcul sur les positions respectives du loup gris
    '            'Si une position sur le calcul pr�c�dent s'est av�r�e �tre en dehors
    '            ' des limites de la position qui sont autoris�s,
    '            ' puis la valeur de retour afin de s'adapter � la limite
    '            For j As Integer = 0 To positionsList(i).Length - 1
    '                If positionsList(i)(j) < Intervalles(j).Min_Value Then

    '                    positionsList(i)(j) = Intervalles(j).Min_Value

    '                ElseIf positionsList(i)(j) > Intervalles(j).Max_Value Then

    '                    positionsList(i)(j) = Intervalles(j).Max_Value

    '                End If
    '            Next j

    '            '2a2. Calculer la valeur de la fonction � cette position
    '            'nilaiFungsiBaru : la valeur des nouvelles fonctions
    '            'Dim newObjectiveFunctValue As Double = ObjectiveFunction(positionsList(i))

    '            newObjectiveFunctValue = ObjectiveFunction(positionsList(i))

    '            '2a3. Si la valeur de la nouvelle fonction est meilleure que la valeur alpha,
    '            ' alors prenez la position comme position alpha de la meilleure

    '            If newObjectiveFunctValue > value_Alpha Then

    '                value_Alpha = newObjectiveFunctValue

    '                Positions_Alpha = positionsList(i)

    '            End If

    '            '2a4. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha,
    '            ' mais il est mieux que la valeur b�ta, puis prendre cette position que la position 
    '            ' de la meilleure Beta.
    '            If newObjectiveFunctValue < value_Alpha AndAlso newObjectiveFunctValue > value_Beta Then

    '                value_Beta = newObjectiveFunctValue

    '                Positions_Beta = positionsList(i)

    '            End If

    '            '2a5. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha et b�ta, 
    '            ' mais mieux que la valeur Delta, puis prendre cette position comme la meilleure position 
    '            ' Delta.
    '            If newObjectiveFunctValue < value_Alpha AndAlso newObjectiveFunctValue < value_Beta AndAlso newObjectiveFunctValue > value_Delta Then

    '                value_Delta = newObjectiveFunctValue

    '                Positions_Delta = positionsList(i)

    '            End If

    '            'Si la valeur alpha alors qu'il s'av�re mieux que les valeurs de fonction en g�n�ral,
    '            ' puis prendre la position alpha comme la meilleure position
    '            If value_Alpha > bestObjectiveFunct Then

    '                Array.Copy(Positions_Alpha, bestPositions, dimensionD)

    '                bestObjectiveFunct = value_Alpha

    '                'Console.Write("Iteraion  = " & iteration.ToString.PadRight(2) & ", ")
    '                'Console.Write("Grey Wolf = " & (i + 1).ToString.PadRight(2) & ", ")
    '                'Console.Write("La meilleure position : [")

    '                'For j As Integer = 0 To bestPositions.Length - 1
    '                '    Console.Write(bestPositions(j).ToString("F4").PadLeft(7) & " ")
    '                'Next j

    '                'Console.Write("], ")
    '                'Console.WriteLine("La valeur de la fonction = " & value_Alpha.ToString("F6"))

    '            End If

    '        Next i

    '        '2b. Sp�cifiez la valeur d'un
    '        'Un a aura des valeurs initiales 2 et diminuera graduellement vers le 0 autant 
    '        ' de fois que le nombre d'it�rations

    '        If GWOVersion = GWOVersionEnum.StandardGWO Then

    '            a = 2 * (1 - (iteration / iterationsMax)) '2 - (iteration * (2 / iterationsMax))

    '        ElseIf GWOVersion = GWOVersionEnum.mGWO Then
    '            tmpa = Math.Pow(iteration, 2) / Math.Pow(iterationsMax, 2)
    '            a = 2 * (1 - tmpa)

    '        ElseIf GWOVersion = GWOVersionEnum.IGWO Then

    '            tmpa = (iteration / iterationsMax) * IGWO_uParameter
    '            tmpa = (1 - tmpa)
    '            a = (1 - (iteration / iterationsMax)) / tmpa

    '        End If

    '        '2c. Faire le calcul � chaque position du loup gris qui existe (poin 2c1 - 2c4)
    '        For i As Integer = 0 To (positionsList.Length - 1)
    '            For j As Integer = 0 To (positionsList(i).Length - 1)

    '                '2c1. Faire le calcul pour calculer la valeur de la X_Alpha (poin 2c1a - 2c1e)

    '                '2c1a. Sp�cifiez une valeur al�atoire de 2 � utiliser dans le calcul de
    '                'Dim r1 As Double = rnd.NextDouble
    '                'Dim r2 As Double = rnd.NextDouble

    '                r1 = Rndm.NextDouble()
    '                r2 = Rndm.NextDouble()

    '                '2c1b. Calculer la valeur d'un alpha avec la formule : A1=2*a*r1-a
    '                'Dim A_Alpha As Double = 2 * a * r1 - a

    '                A_Alpha = (2 * a * r1) - a

    '                '2c1c. Calculer la valeur de l'alpha C avec la formule : C1=2*r2
    '                'Dim C_Alpha As Double = 2 * r2
    '                C_Alpha = 2 * r2

    '                '2c1d. Calculer la valeur de D, alpha avec la formule : D_alpha=abs(C1*posisiAlpha(j)-daftarPosisi(i,j))
    '                'Dim D_Alpha As Double = Math.Abs(C_Alpha * Positions_Alpha(j) - positionsList(i)(j))
    '                D_Alpha = Math.Abs(C_Alpha * Positions_Alpha(j) - positionsList(i)(j))

    '                '2c1e. Calculer la valeur de X alpha avec la formule : X1=posisiAlpha(j)-A1*D_alpha;
    '                'Dim X_Alpha As Double = Positions_Alpha(j) - A_Alpha * D_Alpha
    '                X_Alpha = Positions_Alpha(j) - (A_Alpha * D_Alpha)

    '                '2c2. Faire le m�me calcul (2c1 points) pour calculer la valeur de X Beta
    '                r1 = Rndm.NextDouble()
    '                r2 = Rndm.NextDouble()

    '                A_Beta = 2 * a * r1 - a
    '                C_Beta = 2 * r2
    '                D_Beta = Math.Abs(((C_Beta * Positions_Beta(j)) - positionsList(i)(j)))
    '                X_Beta = Positions_Beta(j) - (A_Beta * D_Beta)

    '                '2c3. Faire le m�me calcul (point 2c1) pour calculer la valeur de Delta X
    '                r1 = Rndm.NextDouble()
    '                r2 = Rndm.NextDouble()

    '                A_Delta = (2 * a * r1) - a
    '                C_Delta = (2 * r2)
    '                D_Delta = Math.Abs(C_Delta * Positions_Delta(j) - positionsList(i)(j))
    '                X_Delta = Positions_Delta(j) - (A_Delta * D_Delta)

    '                '2c4. Calculez la nouvelle valeur de position avec la formule : daftarPosisi(i,j)=(X1+X2+X3)/3
    '                positionsList(i)(j) = (X_Alpha + X_Beta + X_Delta) / 3
    '                '
    '            Next j
    '        Next i

    '        '-----------------Best Chart : Best fitness evolution----------------
    '        mCurrentFitness = value_Alpha
    '        mBestChart.Add(value_Alpha)
    '        mWorstChart.Add(worstFitness)
    '        mMeanChart.Add((meanFitness / wolfCountN))
    '        '--------------------------The best solution-------------------------------------------
    '        For ss As Integer = 0 To (dimensionD - 1)
    '            mBestSolution(ss) = bestPositions(ss)
    '        Next
    '        iteration += 1
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


#End Region



End Class

'Public Class GWO_OptimizerOld
'    Implements IEvolutionaryAlgo


'    Public Sub New(dimensions As Integer, agents As Integer, iterationMax As Integer, gwoVersion As GWOVersionEnum, IGWOParm As Double)
'        Dimensions_D = dimensions
'        Agents_N = agents
'        MaxIterations = iterationMax
'        Me.GWOVersion = gwoVersion
'        Me.IGWO_uParameter = IGWOParm
'    End Sub

'    Public Sub New(gwoVersion As GWOVersionEnum, IGWOParm As Double)
'        Me.GWOVersion = gwoVersion
'        Me.IGWO_uParameter = IGWOParm
'    End Sub

'    Private Shared Rndm As New Random(0)

'    Dim dimensionD As Integer
'    Public Property Dimensions_D As Integer Implements IEvolutionaryAlgo.Dimensions_D
'        Get
'            Return dimensionD
'        End Get
'        Set(value As Integer)
'            dimensionD = Math.Max(0, value)
'        End Set
'    End Property

'    Dim wolfCountN As Integer
'    Public Property Agents_N As Integer Implements IEvolutionaryAlgo.Agents_N
'        Get
'            Return wolfCountN
'        End Get
'        Set(value As Integer)
'            wolfCountN = Math.Max(value, 0)
'        End Set
'    End Property

'    Dim mIntervalles As List(Of Interval)
'    Public Property Intervalles As List(Of Interval) Implements IEvolutionaryAlgo.Intervalles
'        Get
'            Return mIntervalles
'        End Get
'        Set(value As List(Of Interval))
'            mIntervalles = value
'        End Set
'    End Property

'    Dim iterationsMax As Integer
'    Public Property MaxIterations As Integer Implements IEvolutionaryAlgo.MaxIterations
'        Get
'            Return iterationsMax
'        End Get
'        Set(value As Integer)
'            iterationsMax = Math.Max(value, 0)
'        End Set
'    End Property

'    Dim mOptimizationType As OptimizationTypeEnum = OptimizationTypeEnum.Minimization
'    Public Property OptimizationType As OptimizationTypeEnum Implements IEvolutionaryAlgo.OptimizationType
'        Get
'            Return mOptimizationType
'        End Get
'        Set(value As OptimizationTypeEnum)
'            mOptimizationType = value
'        End Set
'    End Property

'    Dim Chronos As Stopwatch
'    Public ReadOnly Property ComputationTime As Long Implements IEvolutionaryAlgo.ComputationTime
'        Get
'            If Object.Equals(Chronos, Nothing) Then
'                Return 0
'            Else
'                Return Chronos.ElapsedMilliseconds
'            End If
'        End Get
'    End Property

'    Dim mBestSolution As Double()
'    Public ReadOnly Property BestSolution As Double() Implements IEvolutionaryAlgo.BestSolution
'        Get
'            Return mBestSolution
'        End Get
'    End Property

'    Public ReadOnly Property BestScore As Double Implements IEvolutionaryAlgo.BestScore
'        Get
'            If (Equals(BestChart, Nothing) OrElse (BestChart.Count = 0)) Then
'                If OptimizationType = OptimizationTypeEnum.Minimization Then
'                    Return Double.MaxValue
'                Else
'                    Return Double.MinValue
'                End If
'            Else
'                Return BestChart.Last()
'            End If
'        End Get
'    End Property



'    Dim mBestChart As List(Of Double)
'    Public ReadOnly Property BestChart As List(Of Double) Implements IEvolutionaryAlgo.BestChart
'        Get
'            Return mBestChart
'        End Get
'    End Property

'    Dim mWorstChart As List(Of Double)
'    Public ReadOnly Property WorstChart As List(Of Double) Implements IEvolutionaryAlgo.WorstChart
'        Get
'            Return mWorstChart
'        End Get
'    End Property

'    Dim mMeanChart As List(Of Double)
'    Public ReadOnly Property MeanChart As List(Of Double) Implements IEvolutionaryAlgo.MeanChart
'        Get
'            Return mMeanChart
'        End Get
'    End Property

'    Dim mSolution_Fitness As Dictionary(Of String, Double)
'    Public ReadOnly Property Solution_Fitness As Dictionary(Of String, Double) Implements IEvolutionaryAlgo.Solution_Fitness
'        Get
'            Return mSolution_Fitness
'        End Get
'    End Property

'    Public Event ObjectiveFunctionComputation(positions() As Double, ByRef fitnessValue As Double) Implements IEvolutionaryAlgo.ObjectiveFunctionComputation

'    Dim mCurrentFitness As Double = Double.NaN
'    Public ReadOnly Property CurrentBestFitness As Double Implements IEvolutionaryAlgo.CurrentBestFitness
'        Get
'            Return mCurrentFitness
'        End Get
'    End Property

'#Region "GWO Params"
'    Public Property GWOVersion As GWOVersionEnum = GWOVersionEnum.StandardGWO

'    ''' <summary>
'    ''' u parameter.
'    ''' </summary>
'    ''' <returns></returns>
'    Public Property IGWO_uParameter As Double = 1.1

'#End Region

'#Region "Optimization By GWO"

'    'PosisiTerbaik : La meilleure position
'    'Dim bestPositions(dimensionD - 1) As Double
'    Dim bestPositions() As Double

'    'nilaiFungsiTerbaik : la valeur de la fonction est la meilleure
'    Dim bestObjectiveFunct As Double = Double.MaxValue

'    'daftarPosisi : Liste des positions
'    'Dim positionsList As Double()() = New Double((wolfCountN - 1))() {}
'    Dim positionsList As Double()()

'    'nilaiFungsi : la valeur de la fonction
'    'Dim objectiveFunctValues(wolfCountN - 1) As Double
'    Dim objectiveFunctValues() As Double

'    '*L'initialisation du loup gris qui a utilis� autant que le nombre de param�tres du Loup
'    'Console.WriteLine("l'initialisation des Loup gris sur de la position al�atoire")

'    'indeksPosisiTerbaik : le meilleur indice de position
'    Dim bestPositionIndex As Integer = -1

'    'Dim Positions_Alpha(dimensionD - 1) As Double
'    Dim Positions_Alpha() As Double
'    Dim value_Alpha As Double = Double.MaxValue 'la valeur de loup Alpha.

'    'Dim Positions_Beta(dimensionD - 1) As Double
'    Dim Positions_Beta() As Double
'    Dim value_Beta As Double = Double.MaxValue 'la valeur de loup Beta.

'    'Dim Positions_Delta(dimensionD - 1) As Double
'    Dim Positions_Delta() As Double
'    Dim value_Delta As Double = Double.MaxValue 'la valeur de loup Delta.

'    '2. le processus de calcul autant que le nombre d'it�rations (point 2a - 2c)�
'    Dim iteration As Integer = 0

'    '---------------------------------- Variables -------------------------------
'    Dim newObjectiveFunctValue As Double = 0R
'    Dim tmpa As Double = 0R
'    Dim a As Double = 0R
'    Dim r1 As Double = 0R
'    Dim r2 As Double = 0R

'    Dim A_Alpha As Double = 0
'    Dim C_Alpha As Double = 0
'    Dim D_Alpha As Double = 0
'    Dim X_Alpha As Double = 0

'    Dim A_Beta As Double = 0
'    Dim C_Beta As Double = 0
'    Dim D_Beta As Double = 0
'    Dim X_Beta As Double = 0

'    Dim A_Delta As Double = 0
'    Dim C_Delta As Double = 0
'    Dim D_Delta As Double = 0
'    Dim X_Delta As Double = 0
'    '----------------------------------------------------------------------------
'    Dim worstFitness As Double = 0R
'    Dim meanFitness As Double = 0R
'    '----------------------------------------------------------------------------

'    ''' <summary>
'    ''' Obsolet. Change this to separte algorithms and to above from if check.
'    ''' </summary>
'    Public Sub RunEpoch() Implements IEvolutionaryAlgo.RunEpoch
'        If mOptimizationType = OptimizationTypeEnum.Minimization Then
'            RunEpoch_Minimization()
'        Else
'            RunEpoch_Maximization()
'        End If
'    End Sub

'    Private Sub RunEpoch_Minimization()
'        Try
'            If iteration = 0 Then
'                Initialize()
'            End If
'            '---------------------------------
'            worstFitness = Double.MinValue
'            meanFitness = 0R
'            '---------------------------------

'            '2a. a fait le calcul sur chaque loup gris (points 2a1 - 2a6))�
'            For i As Integer = 0 To (positionsList.Length - 1)

'                '2a1. Faire le calcul sur les positions respectives du loup gris
'                'Si une position sur le calcul pr�c�dent s'est av�r�e �tre en dehors
'                ' des limites de la position qui sont autoris�s,
'                ' puis la valeur de retour afin de s'adapter � la limite
'                For j As Integer = 0 To positionsList(i).Length - 1
'                    If positionsList(i)(j) < Intervalles(j).Min_Value Then
'                        'positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
'                        positionsList(i)(j) = Intervalles(j).Min_Value

'                    ElseIf positionsList(i)(j) > Intervalles(j).Max_Value Then
'                        'positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
'                        positionsList(i)(j) = Intervalles(j).Max_Value

'                    End If
'                Next j

'                '2a2. Calculer la valeur de la fonction � cette position
'                'nilaiFungsiBaru : la valeur des nouvelles fonctions
'                'Dim newObjectiveFunctValue As Double = ObjectiveFunction(positionsList(i))

'                newObjectiveFunctValue = ObjectiveFunction(positionsList(i))
'                'Debug.Print("It={0}, W={1}, Fit = {2} ", iteration, i, newObjectiveFunctValue.ToString())

'                '2a3. Si la valeur de la nouvelle fonction est meilleure que la valeur alpha,
'                ' alors prenez la position comme position alpha de la meilleure

'                If newObjectiveFunctValue < value_Alpha Then

'                    value_Alpha = newObjectiveFunctValue

'                    Positions_Alpha = positionsList(i)

'                End If

'                '2a4. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha,
'                ' mais il est mieux que la valeur b�ta, puis prendre cette position que la position 
'                ' de la meilleure Beta.
'                If newObjectiveFunctValue > value_Alpha AndAlso newObjectiveFunctValue < value_Beta Then

'                    value_Beta = newObjectiveFunctValue

'                    Positions_Beta = positionsList(i)

'                End If

'                '2a5. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha et b�ta, 
'                ' mais mieux que la valeur Delta, puis prendre cette position comme la meilleure position 
'                ' Delta.
'                If newObjectiveFunctValue > value_Alpha AndAlso newObjectiveFunctValue > value_Beta AndAlso newObjectiveFunctValue < value_Delta Then

'                    value_Delta = newObjectiveFunctValue

'                    Positions_Delta = positionsList(i)

'                End If

'                'Si la valeur alpha alors qu'il s'av�re mieux que les valeurs de fonction en g�n�ral,
'                ' puis prendre la position alpha comme la meilleure position
'                If value_Alpha < bestObjectiveFunct Then

'                    Array.Copy(Positions_Alpha, bestPositions, dimensionD)

'                    bestObjectiveFunct = value_Alpha

'                End If

'                '-------------Me-----------
'                If worstFitness < newObjectiveFunctValue Then
'                    worstFitness = newObjectiveFunctValue
'                End If

'                meanFitness += newObjectiveFunctValue
'                '--------------------------

'            Next i

'            '2b. Sp�cifiez la valeur d'un
'            'Un a aura des valeurs initiales 2 et diminuera graduellement vers le 0 autant 
'            ' de fois que le nombre d'it�rations

'            If GWOVersion = GWOVersionEnum.StandardGWO Then

'                a = 2 * (1 - (iteration / iterationsMax))
'                '2 - (iteration * (2 / iterationsMax))

'            ElseIf GWOVersion = GWOVersionEnum.mGWO Then

'                tmpa = Math.Pow(iteration, 2) / Math.Pow(iterationsMax, 2)
'                a = 2 * (1 - tmpa)

'            ElseIf GWOVersion = GWOVersionEnum.IGWO Then

'                tmpa = (iteration / iterationsMax) * IGWO_uParameter
'                tmpa = (1 - tmpa)
'                a = (1 - (iteration / iterationsMax)) / tmpa

'            End If

'            '2c. Faire le calcul � chaque position du loup gris qui existe (poin 2c1 - 2c4)
'            For i As Integer = 0 To (positionsList.Length - 1)

'                For j As Integer = 0 To (positionsList(i).Length - 1)

'                    '2c1. Faire le calcul pour calculer la valeur de la X_Alpha (poin 2c1a - 2c1e)

'                    '2c1a. Sp�cifiez une valeur al�atoire de 2 � utiliser dans le calcul de

'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    '2c1b. Calculer la valeur d'un alpha avec la formule : A1=2*a*r1-a
'                    'Dim A_Alpha As Double = 2 * a * r1 - a

'                    A_Alpha = (2 * a * r1) - a

'                    '2c1c. Calculer la valeur de l'alpha C avec la formule : C1=2*r2
'                    'Dim C_Alpha As Double = 2 * r2
'                    C_Alpha = (2 * r2)

'                    '2c1d. Calculer la valeur de D, alpha avec la formule : D_alpha=abs(C1*posisiAlpha(j)-daftarPosisi(i,j))
'                    'Dim D_Alpha As Double = Math.Abs(C_Alpha * Positions_Alpha(j) - positionsList(i)(j))
'                    D_Alpha = Math.Abs(((C_Alpha * Positions_Alpha(j)) - positionsList(i)(j)))

'                    '2c1e. Calculer la valeur de X alpha avec la formule : X1=posisiAlpha(j)-A1*D_alpha;
'                    'Dim X_Alpha As Double = Positions_Alpha(j) - A_Alpha * D_Alpha
'                    X_Alpha = Positions_Alpha(j) - (A_Alpha * D_Alpha)

'                    '2c2. Faire le m�me calcul (2c1 points) pour calculer la valeur de X Beta
'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    A_Beta = (2 * a * r1) - a
'                    C_Beta = (2 * r2)
'                    D_Beta = Math.Abs(((C_Beta * Positions_Beta(j)) - positionsList(i)(j)))
'                    X_Beta = Positions_Beta(j) - (A_Beta * D_Beta)

'                    '2c3. Faire le m�me calcul (point 2c1) pour calculer la valeur de Delta X
'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    A_Delta = (2 * a * r1) - a
'                    C_Delta = (2 * r2)
'                    D_Delta = Math.Abs(((C_Delta * Positions_Delta(j)) - positionsList(i)(j)))
'                    X_Delta = Positions_Delta(j) - (A_Delta * D_Delta)

'                    '2c4. Calculez la nouvelle valeur de position avec la formule : daftarPosisi(i,j)=(X1+X2+X3)/3

'                    'positionsList(i)(j) = (X_Alpha + X_Beta + X_Delta) / 3
'                    positionsList(i)(j) = ((1 * X_Alpha) + (1 * X_Beta) + (1 * X_Delta)) / 3

'                Next j
'            Next i

'            iteration += 1
'            '-----------------Best Chart : Best fitness evolution----------------
'            mCurrentFitness = value_Alpha
'            mBestChart.Add(value_Alpha)
'            mWorstChart.Add(worstFitness)
'            mMeanChart.Add((meanFitness / wolfCountN))

'            '--------------------------The best solution-------------------------------------------
'            For ss As Integer = 0 To (dimensionD - 1)
'                mBestSolution(ss) = bestPositions(ss)
'            Next

'        Catch ex As Exception
'            Stop
'            Throw ex

'        End Try
'    End Sub

'    Private Sub RunEpoch_Maximization()
'        Try
'            If iteration = 0 Then
'                Initialize()
'            End If

'            ''1. faire des bouclages autant que le nombre de loup (points 1a - 1b).
'            'For i As Integer = 0 To (wolfCountN - 1)
'            '    'daftarPosisi : Liste des positions
'            '    positionsList(i) = New Double(dimensionD - 1) {}

'            '    '1 a. donner des positions al�atoires sur chacun des loups gris,
'            '    ' autant que le nombre de dimensions
'            '    'Calculer ensuite la valeur de la fonction � cette position
'            '    'Description plus de d�tails sur cette fonction peut �tre vu
'            '    ' dans l'explication du script ci-dessous.

'            '    For j As Integer = 0 To (dimensionD - 1)

'            '        positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
'            '    Next j

'            '    objectiveFunctValues(i) = ObjectiveFunction(positionsList(i))

'            '    REM------------------------------------------------------------------------------------------
'            '    'Console.Write("Grey Wolf " & (i + 1).ToString.PadRight(2) & ", ")
'            '    'Console.Write("Position de la : [")

'            '    'For j As Integer = 0 To positionsList(i).Length - 1
'            '    '    Console.Write(positionsList(i)(j).ToString("F4").PadLeft(7) & " ")
'            '    'Next j

'            '    'Console.Write("], ")
'            '    'Console.WriteLine("la valeur de la fonction = " & objectiveFunctValues(i).ToString("F2"))
'            '    REM------------------------------------------------------------------------------------------

'            '    '1 b. Si la valeur d'une position al�atoire est meilleure que la meilleure,
'            '    ' la valeur de fonction 
'            '    'alors, prenez cette position comme la meilleure position tout en
'            '    If objectiveFunctValues(i) > bestObjectiveFunct Then
'            '        bestObjectiveFunct = objectiveFunctValues(i)
'            '        Array.Copy(positionsList(i), bestPositions, dimensionD)
'            '        bestPositionIndex = i
'            '    End If
'            'Next i

'            '---------------------------------
'            worstFitness = Double.MaxValue
'            meanFitness = 0R
'            '---------------------------------
'            REM------------------------------------------------------------------------------------------
'            'Console.WriteLine("")
'            'Console.WriteLine("La meilleure position")
'            'Console.WriteLine("Grey Wolf " & bestPositionIndex & " la valeur de la fonction = " & bestObjectiveFunct.ToString("F2"))
'            'Console.WriteLine("")
'            REM------------------------------------------------------------------------------------------

'            '* Effectuer le processus de recherche la meilleure position
'            'Console.WriteLine("D�marrez le processus de recherche la meilleure position")

'            '2a. a fait le calcul sur chaque loup gris (points 2a1 - 2a6))�
'            For i As Integer = 0 To (positionsList.Length - 1)

'                '2a1. Faire le calcul sur les positions respectives du loup gris
'                'Si une position sur le calcul pr�c�dent s'est av�r�e �tre en dehors
'                ' des limites de la position qui sont autoris�s,
'                ' puis la valeur de retour afin de s'adapter � la limite
'                For j As Integer = 0 To positionsList(i).Length - 1
'                    If positionsList(i)(j) < Intervalles(j).Min_Value Then

'                        positionsList(i)(j) = Intervalles(j).Min_Value

'                    ElseIf positionsList(i)(j) > Intervalles(j).Max_Value Then

'                        positionsList(i)(j) = Intervalles(j).Max_Value

'                    End If
'                Next j

'                '2a2. Calculer la valeur de la fonction � cette position
'                'nilaiFungsiBaru : la valeur des nouvelles fonctions
'                'Dim newObjectiveFunctValue As Double = ObjectiveFunction(positionsList(i))

'                newObjectiveFunctValue = ObjectiveFunction(positionsList(i))

'                '2a3. Si la valeur de la nouvelle fonction est meilleure que la valeur alpha,
'                ' alors prenez la position comme position alpha de la meilleure

'                If newObjectiveFunctValue > value_Alpha Then

'                    value_Alpha = newObjectiveFunctValue

'                    Positions_Alpha = positionsList(i)

'                End If

'                '2a4. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha,
'                ' mais il est mieux que la valeur b�ta, puis prendre cette position que la position 
'                ' de la meilleure Beta.
'                If newObjectiveFunctValue < value_Alpha AndAlso newObjectiveFunctValue > value_Beta Then

'                    value_Beta = newObjectiveFunctValue

'                    Positions_Beta = positionsList(i)

'                End If

'                '2a5. Si la valeur de la nouvelle fonction est inf�rieure � la valeur de l'alpha et b�ta, 
'                ' mais mieux que la valeur Delta, puis prendre cette position comme la meilleure position 
'                ' Delta.
'                If newObjectiveFunctValue < value_Alpha AndAlso newObjectiveFunctValue < value_Beta AndAlso newObjectiveFunctValue > value_Delta Then

'                    value_Delta = newObjectiveFunctValue

'                    Positions_Delta = positionsList(i)

'                End If

'                'Si la valeur alpha alors qu'il s'av�re mieux que les valeurs de fonction en g�n�ral,
'                ' puis prendre la position alpha comme la meilleure position
'                If value_Alpha > bestObjectiveFunct Then

'                    Array.Copy(Positions_Alpha, bestPositions, dimensionD)

'                    bestObjectiveFunct = value_Alpha

'                    'Console.Write("Iteraion  = " & iteration.ToString.PadRight(2) & ", ")
'                    'Console.Write("Grey Wolf = " & (i + 1).ToString.PadRight(2) & ", ")
'                    'Console.Write("La meilleure position : [")

'                    'For j As Integer = 0 To bestPositions.Length - 1
'                    '    Console.Write(bestPositions(j).ToString("F4").PadLeft(7) & " ")
'                    'Next j

'                    'Console.Write("], ")
'                    'Console.WriteLine("La valeur de la fonction = " & value_Alpha.ToString("F6"))

'                End If

'            Next i

'            '2b. Sp�cifiez la valeur d'un
'            'Un a aura des valeurs initiales 2 et diminuera graduellement vers le 0 autant 
'            ' de fois que le nombre d'it�rations

'            If GWOVersion = GWOVersionEnum.StandardGWO Then

'                a = 2 * (1 - (iteration / iterationsMax)) '2 - (iteration * (2 / iterationsMax))

'            ElseIf GWOVersion = GWOVersionEnum.mGWO Then
'                tmpa = Math.Pow(iteration, 2) / Math.Pow(iterationsMax, 2)
'                a = 2 * (1 - tmpa)

'            ElseIf GWOVersion = GWOVersionEnum.IGWO Then

'                tmpa = (iteration / iterationsMax) * IGWO_uParameter
'                tmpa = (1 - tmpa)
'                a = (1 - (iteration / iterationsMax)) / tmpa

'            End If

'            '2c. Faire le calcul � chaque position du loup gris qui existe (poin 2c1 - 2c4)
'            For i As Integer = 0 To (positionsList.Length - 1)
'                For j As Integer = 0 To (positionsList(i).Length - 1)

'                    '2c1. Faire le calcul pour calculer la valeur de la X_Alpha (poin 2c1a - 2c1e)

'                    '2c1a. Sp�cifiez une valeur al�atoire de 2 � utiliser dans le calcul de
'                    'Dim r1 As Double = rnd.NextDouble
'                    'Dim r2 As Double = rnd.NextDouble

'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    '2c1b. Calculer la valeur d'un alpha avec la formule : A1=2*a*r1-a
'                    'Dim A_Alpha As Double = 2 * a * r1 - a

'                    A_Alpha = (2 * a * r1) - a

'                    '2c1c. Calculer la valeur de l'alpha C avec la formule : C1=2*r2
'                    'Dim C_Alpha As Double = 2 * r2
'                    C_Alpha = 2 * r2

'                    '2c1d. Calculer la valeur de D, alpha avec la formule : D_alpha=abs(C1*posisiAlpha(j)-daftarPosisi(i,j))
'                    'Dim D_Alpha As Double = Math.Abs(C_Alpha * Positions_Alpha(j) - positionsList(i)(j))
'                    D_Alpha = Math.Abs(C_Alpha * Positions_Alpha(j) - positionsList(i)(j))

'                    '2c1e. Calculer la valeur de X alpha avec la formule : X1=posisiAlpha(j)-A1*D_alpha;
'                    'Dim X_Alpha As Double = Positions_Alpha(j) - A_Alpha * D_Alpha
'                    X_Alpha = Positions_Alpha(j) - (A_Alpha * D_Alpha)

'                    '2c2. Faire le m�me calcul (2c1 points) pour calculer la valeur de X Beta
'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    A_Beta = 2 * a * r1 - a
'                    C_Beta = 2 * r2
'                    D_Beta = Math.Abs(((C_Beta * Positions_Beta(j)) - positionsList(i)(j)))
'                    X_Beta = Positions_Beta(j) - (A_Beta * D_Beta)

'                    '2c3. Faire le m�me calcul (point 2c1) pour calculer la valeur de Delta X
'                    r1 = Rndm.NextDouble()
'                    r2 = Rndm.NextDouble()

'                    A_Delta = (2 * a * r1) - a
'                    C_Delta = (2 * r2)
'                    D_Delta = Math.Abs(C_Delta * Positions_Delta(j) - positionsList(i)(j))
'                    X_Delta = Positions_Delta(j) - (A_Delta * D_Delta)

'                    '2c4. Calculez la nouvelle valeur de position avec la formule : daftarPosisi(i,j)=(X1+X2+X3)/3
'                    positionsList(i)(j) = (X_Alpha + X_Beta + X_Delta) / 3
'                    '
'                Next j
'            Next i

'            '-----------------Best Chart : Best fitness evolution----------------
'            mCurrentFitness = value_Alpha
'            mBestChart.Add(value_Alpha)
'            mWorstChart.Add(worstFitness)
'            mMeanChart.Add((meanFitness / wolfCountN))
'            '--------------------------The best solution-------------------------------------------
'            For ss As Integer = 0 To (dimensionD - 1)
'                mBestSolution(ss) = bestPositions(ss)
'            Next
'            iteration += 1
'        Catch ex As Exception
'            Throw ex
'        End Try
'    End Sub

'    Private Sub Initialize()

'        mBestChart = New List(Of Double)
'        mWorstChart = New List(Of Double)
'        mMeanChart = New List(Of Double)
'        ReDim mBestSolution(dimensionD - 1)
'        iteration = 0I

'        If OptimizationType = OptimizationTypeEnum.Minimization Then

'            bestObjectiveFunct = Double.MaxValue
'            value_Alpha = Double.MaxValue  'la valeur de loup Alpha.
'            value_Beta = Double.MaxValue 'la valeur de loup Beta.
'            value_Delta = Double.MaxValue 'la valeur de loup Delta.

'        Else
'            bestObjectiveFunct = Double.MinValue
'            value_Alpha = Double.MinValue  'la valeur de loup Alpha.
'            value_Beta = Double.MinValue 'la valeur de loup Beta.
'            value_Delta = Double.MinValue 'la valeur de loup Delta.

'        End If

'        bestPositions = New Double(dimensionD - 1) {}
'        positionsList = New Double(wolfCountN - 1)() {}
'        objectiveFunctValues = New Double(wolfCountN - 1) {}
'        Positions_Alpha = New Double(dimensionD - 1) {}
'        Positions_Beta = New Double(dimensionD - 1) {}
'        Positions_Delta = New Double(dimensionD - 1) {}
'        '-----------------------------------------------------------------
'        '1. faire des bouclages autant que le nombre de loup (points 1a - 1b).
'        For i As Integer = 0 To (wolfCountN - 1)
'            'daftarPosisi : Liste des positions
'            positionsList(i) = New Double((dimensionD - 1)) {}

'            '1 a. donner des positions al�atoires sur chacun des loups gris,
'            ' autant que le nombre de dimensions
'            'Calculer ensuite la valeur de la fonction � cette position
'            'Description plus de d�tails sur cette fonction peut �tre vu
'            ' dans l'explication du script ci-dessous.

'            For j As Integer = 0 To (dimensionD - 1)
'                positionsList(i)(j) = (Intervalles(j).Max_Value - Intervalles(j).Min_Value) * Rndm.NextDouble() + Intervalles(j).Min_Value
'            Next j

'            objectiveFunctValues(i) = ObjectiveFunction(positionsList(i))

'            '-------------------------------------------------------------------------------------

'            '1 b. Si la valeur d'une position al�atoire est meilleure que la meilleure,
'            ' la valeur de fonction 
'            'alors, prenez cette position comme la meilleure position tout en
'            If objectiveFunctValues(i) < bestObjectiveFunct Then
'                bestObjectiveFunct = objectiveFunctValues(i)
'                Array.Copy(positionsList(i), bestPositions, dimensionD)
'                bestPositionIndex = i
'            End If
'        Next i

'    End Sub

'    Dim fitnessValue As Double = 0R
'    Private Function ObjectiveFunction(ByRef positions() As Double) As Double
'        fitnessValue = 0R
'        RaiseEvent ObjectiveFunctionComputation(positions, fitnessValue)
'        Return fitnessValue
'    End Function

'#End Region

'    Public Sub LuanchComputation() Implements IEvolutionaryAlgo.LuanchComputation
'        Initialize()
'        For iteration = 0 To MaxIterations
'            RunEpoch()
'        Next
'    End Sub
'End Class

'Public Enum GWOVersionEnum
'    ''' <summary>
'    ''' Standard version, by Seyedali Mirjalili.
'    ''' </summary>
'    StandardGWO = 0
'    ''' <summary>
'    ''' Modified GWO, by : DOI 10.1155/2016/7950348 
'    ''' </summary>
'    mGWO = 1
'    ''' <summary>
'    ''' Improved GWO, by : DOI 10.1007/s00521-016-2357-x
'    ''' </summary>
'    IGWO = 2

'    ''' <summary>
'    ''' Adaptive GWO, by : http://dx.doi.org/10.1080/23311916.2016.1256083
'    ''' </summary>
'    AdaptiveGWO = 3

'End Enum



