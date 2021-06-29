---
puppeteer:
    pdf:
        format: A4
        displayHeaderFooter: true
        
        margin:
            top: 1.5cm
            right: 1.5cm
            bottom: 1.5cm
            left: 2cm
---

<center> 
Université de Bouira -UAMOB-

*_2^ème^ Master Ressources Hydrauliques_*
 </center>

# <center> Logiciels spécialisés (EPA-SWMM) </center>

 <center> 
 
 **Maitrise EPA-SWMM** 

  
CTP/ S.DAHMANI (s.dahmani@univ-bouira.dz)

 Version 1.1 (2021)
 </center>
 
---

**Sommaire :**

[[TOC]]

---

> # Téléchargement et installation du logiciel EPA-SWMM

> ## Buts du TP
1. Avoir une connaissance primaire sur le logiciel de simulation SWMM;
2. Téléchargement du logiciel EPA-SWMM;
3. Installation du logiciel.

> ## EPA-SWMM ?
 Le modèle de gestion des eaux pluviales "Storm Water Management Model (SWMM)" est un modèle dynamique de simulation de l'acheminement des eaux pluviales et des précipitations utilisé pour une simulation à un seul événement ou à long terme (continue) de la quantité et de la qualité du ruissellement provenant principalement des zones urbaines. La composante de ruissellement de SWMM fonctionne sur une collection de zones de sous-couche qui reçoivent des précipitations et génèrent des charges de ruissellement et de polluants. Le SWMM transporte ce ruissellement à travers un système de tuyaux, de canaux, de dispositifs de stockage/traitement, de pompes et de régulateurs. SWMM suit aussi la quantité et la qualité du ruissellement généré dans chaque sous-couche, ainsi que le débit, la profondeur d'écoulement et la qualité de l'eau dans chaque tuyau et canal au cours d'une période de simulation divisée en plusieurs étapes temporelles.

 SWMM est développé par l'agence de protection de l'environnement des états-unis "United States Environmental Protection Agency, EPA" de même que le logiciel "EPANET".  

> ## Téléchargement du EPA-SWMM  
 On peut télécharger SWMM depuis la page du site officiel:

 1. [Page de téléchargement- Cliquer ici-](https://www.epa.gov/water-research/storm-water-management-model-swmm)
   
   Ou coller le lien suivant dans la barre de recherche de votre navigateur internet:

   https://www.epa.gov/water-research/storm-water-management-model-swmm

 2. Aller au tableau **Software** et cliquer sur : *Self-Extracting Installation Program for SWMM 5.1.015 (EXE)(32 MB)*. Après téléchargement, on obtient le package d'installation **swmm51015_setup.exe**.      

 On peut télécharger les manuels du logiciel à partir du tableau **Manuals and Guides**

> ## Installation du EPA-SWMM
  L'installation du SWMM est facile. Cliquer sur le package téléchargé **swmm51015_setup.exe**, puis suivre les étapes. 

---

> # Initialisation d'un nouveau projet sous SWMM


> ## Buts du TP
 Le but du TP est de commencer correctement la manipulation sur EPA-SWMM. L'approche d'apprentissage **Learning by doing** est adoptée.
     
 Donc, nous allons voir comment :
     
 1. Créer un nouveau projet sous SWMM;
 2. Ajouter un titre au projet;
 3. Changer les unités de mesures;
 4. Changer les paramètres de simulation et calculs;
 5. Enregistrer le projet;
 6. Consulter l'aide (la documentation) du logiciel.   

 !!! Note
     Consulter la série de vidéos (vidéo 1) sur Youtube: [Giswater](https://www.youtube.com/watch?v=g-3xPFLx9HU&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq)

     Lien (vidéo 1) : https://www.youtube.com/watch?v=g-3xPFLx9HU&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq

> ## Procédure

> ### Création d'un nouveau projet
 Créer un nouveau projet par un clique sur le bouton **Start a new project**, comme suit (la figure ci-dessous).
 
   ![Figure 0](./Figures/Capture_0.jpg)  

> ### Ajout d'un Titre ou Note au projet 
 Pour ajouter un titre ou un note au projet, suivre les étapes mentionnées sur la figure suivante (par ordre). Comme titre ajouter *Système d'assainissement urbain*.   

 ![Figure 1](./Figures/Capture_1.jpg)  

> ### Changement des unités de mesures
 Le système de mesure Anglo-Saxon est paramétré par défaut. Il faut le changer en SI (Système International) comme suit: 
    
 1. Changement d'unité de mesure de débit vers **CMS** (Cubic Meter (m^3^)/ Seconde (s)) :

 ![Figure 2](./Figures/Capture_2.png)       

 2. Changement d'unité de mesure de la carte (map) comme suit :
    
 ![Figure 2](./Figures/Capture_3.png) 

 Puis, 

 ![Figure 4](./Figures/Capture_4.png) 

> ### Changement des paramètres de simulation
 Ensuite, changer les paramètres de simulation comme suit (Suivre les étapes par ordre):
   
 ![Figure 5](./Figures/Capture_5.png) 

 !!! Important
 * Routnig model:  Le routage est une technique utilisée pour prédire les changements de forme d'un hydrographe lorsque l'eau se déplace à travers un chenal ou un réservoir.
 * Dynamic wave : onde dynamique.
 * Infiltration model : Modèle d'infiltration.
 * Curve number : Courbe élaborée par des chiffres.
  
 > ### Enregistrement du projet 
 Enregistrer le projet dans un dossier (répertoire) et lui donner le comme nom du ficher **Exercice_1. suit :

 ![Figure 6](./Figures/Capture_6.png) 
     
 Puis,

 ![Figure 7](./Figures/Capture_7.png)  

 > ## Avoir l'aide du logiciel EPA-SWMM
 Pour chercher l'aide, vous pouvez consulter la documentation du logiciel comme suit :

 ![Figure 8](./Figures/Capture_8.png)

 Ensuite, entrer le mot à chercher et consulter la rubrique correspondante : 

 ![Figure 9](./Figures/Capture_9.png)

 ---

> # Géoréférencement d'un carte et tracé du réseau d'assainissement

> ## Buts du TP
 Le but du TP est de continuer correctement la manipulation sur EPA-SWMM. L'approche d'apprentissage **Learning by doing** est adoptée.
     
 Dans ce TP, nous allons voir comment :
    
 1. Ajouter une carte (fond) au projet;
 2. Caler (géoréférencement) la carte en utilisant les coordonnées réelles;
 3. Tracer les réseau d'assainissement.
     
 !!! Info
     Consulter les séries de vidéos sous le lien suivant: 
     
     https://www.youtube.com/watch?v=fl0hwxNqh7o&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq&index=3

> ## Ajout et géoréférencement de la carte 
 Avant de commencer le travail sur SWMM, télécharger l'image **Carte_Projet_IT.p,g** et l'enregistrer dans le dossier contenant le fichier "Exercice_1.inp" (qu'on a créé pendant le TP2).  

> ### Ouverture du projet 
 Aller au menu **File -> Open**, puis ouvrir le projet déjà créé au cours du TP précédent (TP N°2).
     
> ### Ajout d'une carte fond au projet
 Dans ces TPs, nous allons travailler sur un réseau fictif d'assainissement de l'Institut de Technologie (IT-Bouira).
     
 La carte du fond sur laquelle nous allons tracer le réseau est **Carte_Projet_IT.png** (jointe avec la brochure du TP).
     
 Pour ajouter la carte **Carte_Projet_IT.png** au projet, suivre les étapes mentionnées sur la figure ci-dessous. 
 
 ![Figure 10](./Figures/Capture_10.png)

 Puis, 
 
 ![Figure 11](./Figures/Capture_11.png) 

 Vous allez avoir l'affichage suivant :

 ![Figure 12](./Figures/Capture_12.png)   

 > ### Calage (géoréférencement) de la carte

 Pour référencer la carte dans le système de projection, procéder comme suit :

 ![Figure 13](./Figures/Capture_13.png)

 Ensuite, entrer les coordonnées comme suit :
 1. Lower Left (coin bas-gauche de la carte, c-à-d., point B): X(m)= 431011.1 | Y(m)= 4352679.8
 2. Upper Right (coin haut-droit de la carte, c-à-d., point A): X(m)= 431640.50 | Y(m)= 4352976.0     

 ![Figure 14](./Figures/Capture_14.png)

 Pour ré-afficher la carte, procéder comme suit :

 ![Figure 15](./Figures/Capture_15.png) 

 Puis, 

 ![Figure 16](./Figures/Capture_16.png)

 !!! Check
     N'oublier pas de sauvegarder (enregistrer) les modifications réalisées sur le projet.

> ## Tracé du réseau

> ### Ajout d'un noeud de sortie (Outfall) au réseau
 Les sorties sont des nœuds terminaux du système de drainage utilisés pour définir les limites finales en aval sous un écoulement d’onde dynamique. Un seul lien peut être connecté à un nœud de sortie, et l’option existe pour avoir la décharge de sortie sur la surface d’une sous-couche.

 Pour plus de détails, consulter la documentation d'aide associée du logiciel.
     
 Pour ajouter un noeud de sortie, procéder comme suit (cliquer sur le bouton **Add an outfall node**, puis sur l'endroit approprié sur la carte) :
     
 ![Figure 17](./Figures/Capture_17.png)

> ### Ajout des noeuds de jonction au réseau
 Les jonctions (ou nœuds de jonction) (en anglais: "junctions or node junctions") sont des nœuds du système d'assainissement où les canalisations s’unissent. Physiquement, ils peuvent représenter la confluence de canaux de surface naturels, de regards dans un système d’égout ou de raccords de raccordement de canalisation. Les entrées externes peuvent entrer dans le système aux jonctions. L’excès d’eau à une jonction peut devenir partiellement pressurisé pendant que les conduits de raccordement sont surchargés et peuvent être perdus du système ou être autorisés à étang au sommet de la jonction et par la suite drainer de nouveau dans la jonction.

 Pour ajouter les nœuds aux réseau, procéder comme suit (cliquer sur le bouton **Add a junction node**, puis sur les endroits appropriés sur la carte):

 ![Figure 18](./Figures/Capture_18.png) 

 > ### Ajout des canalisations au réseau
 Pour ajouter les canalisations (liens) entre les nœuds, procéder comme suit :

 1. Cliquer sur le bouton **Add a conduit link** (ajouter un lien de conduite);
 2. Changer l'option **Auto-Length : On**, pour que le logiciel calcule les longueurs des canalisation automatiquement, à partir de la carte déjà calée.
       
 ![Figure 18](./Figures/Capture_19.png)

 Par la suite, tracer les canalisation dans le sens correcte c-à-d, **Dans le sens de l'écoulement** : 
 * Du jonction 9 ---> vers 8 (cliquer sur 9, puis 8 comme si vous travaillez sous Epanet);
 * Du jonction 9 ---> vers 8; 
 * Du jonction 8 ---> vers 7, ... etc.
   
 ![Figure 19](./Figures/Capture_19.png)

 Puis, 
     
 ![Figure 20](./Figures/Capture_20.png)   

 A la fin, vous aurez le réseau suivant:

 ![Figure 21](./Figures/Capture_21.png)  

 !!! Check
     * N'oublier pas de sauvegarder (enregistrer) les modifications réalisées sur le projet.
     * Consulter la documentation du logiciel EPA-SWMM pour comprendre plus les éléments de modélisation.   

 Comme ça, le tracé du réseau est terminé, ...  RDV TP N°4.
           
---

> # Saisie des données du réseau d'assainissement

> ## Buts du TP       
 Dans ce TP, nous allons voir comment :
 1. Introduire les données des noeuds (regards ou jonctions);
 2. Introduire les données des points de sortie;
 3. Introduire les données des canalisations.

 !!! Info
     Consulter les séries de vidéos sous le lien [suivant](https://www.youtube.com/watch?v=ZfKzCv2x5k4&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq&index=4): 
     
     https://www.youtube.com/watch?v=ZfKzCv2x5k4&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq&index=4

> ### Ouverture du projet 
 1. Lancer (démarrer) le logiciel EPA-SWMM 5.1;
 2. Le projet **Exercice_1.inp** s'ouvre automatiquement, sinon .. ; 
 3. Aller au menu **File -> Open**, puis ouvrir le projet déjà créé au cours du TP précédent (TP N°3).

> ## Saisie des données des neouds

> ### Données à introduire et signification 
 Les données nécessaires à introduire aux noeuds (regards ou jonctions) avec leurs significations sont les suivantes:
     
| Propriété (Property) | Unité SI | Signification |
| ---------------------- | ------ | ---------- |
| Invert El.| m | Cote inférieure du nœud (cote du radier du regard de jonction). |
| Max. Depth | m | Profondeur Maximale du Nœud : distance verticale entre le radier du regard de jonction et le terrain naturel. |
| Ponded Area | m^2^ | Surface d'eau stockée au dessus du nœud après débordement. Si l'option de calcul correspondante est activée, le volume de débordement est stocké puis évacué par le réseau lorsque celui-ci en a retrouvé la capacité.|

 Les données à introduire aux noeuds :

| Nœuds | Invert El.  (m) | Max. Depth (m) | Ponded Area (m^2^) |
| ------ | --------------- | -------------- | ---------------- |
| 2      | 528.6           | 1.6            | 0                |
| 3      | 531             | 1.4            | 0                |
| 4      | 531.1           | 1.4            | 0                |
| 5      | 529             | 1.4            | 0                |
| 6      | 530             | 1.4            | 0                |
| 7      | 531             | 1.4            | 0                |
| 8      | 535             | 1.4            | 0                |
| 9      | 542             | 1.4            | 0                |

> ### Méthodologie de saisie
 Pour introduire les données aux noeuds (regards et jonctions), procéder comme suite (suivre les étapes par ordre, (1), puis (2), ... etc):
 
 ![Figure 22](./Figures/Capture_22.png)

 Puis, le logiciel SWMM affiche la fenêtre de propriétés comme suit, dans laquelle nous devons introduire (saisir) les données du tableau précédent, comme si nous travaillons sous le logiciel Epanet:

 ![Figure 23](./Figures/Capture_23.png)

 !!! check
     * Revérifier les données que vous avez saisi;
     * Enregistrer votre travail.  

> ## Saisie des données des points de sortie

> ### Données à introduire et signification
 Pour les points de sortie, nous avons besoin d'introduire la donnée suivante :

| Propriété (Property) | Unité SI | Signification |
| ---------------------- | ------ | ---------- |
| Invert El.| m | Cote inférieure du point de sortie (cote du radier).|

 Les données à introduire sont :

| Outfall | Invert El.  (m) |
| ------- | --------------- |
| 1       | 528.5           |

> ### Méthodologie de saisie
 De même que les noeuds, mais cette fois-ci allez avec le point de sortie (Outfall)

 ![Figure 24](./Figures/Capture_24.png)     

 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail.  

> ## Affichage des identificateurs des objets 
 Pour afficher les nom (identificateurs ID) des différents objets du réseau (noeuds, point de sortie et canalisation), procéder comme si vous travaillez sous EPANET.
 
 ![Figure 25](./Figures/Capture_25.png)

 Puis, 
 
 ![Figure 26](./Figures/Capture_26.png)

> ## Saisie des données des canalisations

> ### Données à introduire et signification
 Le tableaux suivant montre les principales données relatives aux canalisations (conduites).

| Propriété (Property) | Unité SI | Signification |
| -------------------- | -------- |-------------- |
| Length | m | Longueur de la  conduite |
| Shape  | - | Forme de la  section |
| Max. Depth | m | Hauteur d'eau maximale dans la section (diamètre pour une section circulaire) |
| Conduit Roughness | s/m^(1/3)^ | Rugosité de la conduite au sens de Manning-Strickler |
| Link Offsets  | - | Décalage du collecteur par rapport au fond du regard. La position du radier du collecteur peut être indiquée sous forme d'une hauteur au dessus du fond du regard (DEPTH) = distance entre les points 1 et 2 ) ou sous forme d'une cote absolue (ELEVATION) = cote absolue du point 1 |
| Inlet offset | m | Décalage du collecteur par rapport au fond du regard coté  entrée. |
| Outlet offset | m | Décalage du collecteur par rapport au fond du regard coté sortie. |
| Flow Units | -  | Unités de débit. Choisir une unité métrique (CMS : m3/s, LPS : L/s, MLD : 1000m3/j) implique que toutes les autres grandeurs du logiciel sont exprimées en unités métriques. Les valeurs déjà rentrées ne sont pas automatiquement converties lorsque l'on change de système d'unités.                                             |
| Routing Model | - | Modèle de transfert. Trois modèles sont disponibles : Steady Flow : Écoulement permanent et uniforme dans chaque tronçon et à chaque pas de temps (Translation simple des hydrogrammes) Kinematic Wave : Modèle de l'onde cinématique Dynamic Wave : Modèle de l'onde dynamique (Résolution des équations de Barré de Saint Venant) |

 ![Figure 27](./Figures/Capture_27.png)

 Les données à saisir sont les suivantes :

 | Name | Shape (Forme) | Max. Depth  (m) | Roughness | Inlet offset (m) | Outlet offset (m) |
| ---- | ------------- | --------------- | ---------- | ---------------- | ----------------- |
| 1    | CIRCULAR      | 0.3             | 0.014      | 0.1              | 0.1               |
| 2    | CIRCULAR      | 0.3             | 0.014      | 0.1              | 0.1               |
| 3    | CIRCULAR      | 0.4             | 0.014      | 0.1              | 0.1               |
| 4    | CIRCULAR      | 0.5             | 0.014      | 0.1              | 0.1               |
| 5    | CIRCULAR      | 0.6             | 0.014      | 0.1              | 0.1               |
| 6    | CIRCULAR      | 0.4             | 0.014      | 0.1              | 0.1               |
| 7    | CIRCULAR      | 0.5             | 0.014      | 0.1              | 0.1               |
| 8    | CIRCULAR      | 0.8             | 0.014      | 0.1              | 0.1               |

 !!! Attention
     Les longueurs des conduites sont des données très importantes. Le logiciel EPA-SWMM calcule les longueurs automatiquement (dans ce cas) à partir de la carte calée. Cette option on a la fixé auparavant (voir les TPs précédents).      

> ### Méthodologie de saisie
 De la même manière que les objets précédents, introduire les données des conduites (canalisations) mentionnées dans le tableau précédent. La figure suivante explique la méthode:

 ![Figure 28](./Figures/Capture_28.png)
 
 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail.

Fin du TP4, ... RDV TP5. 

---

> # Saisie des données pluviométriques et tracé des sous bassins.

> ## Buts du TP       
 Dans ce TP, nous allons voir comment :
 1. Saisir les données pluviométriques;
 2. Tracer les sous-bassins à drainer par le réseau d'assainissement.
   
> ## Les données pluviométriques 
> ### La série temporelle de précipitation
  Pour introduire la série de précipitation sur laquelle le réseau est simulé, on procède comme suit :

 ![Figure 29](./Figures/Capture_29.png)

 * Le nom de la série de précipitation (Time séries name): Série_Pluvio_1
 * Description : Série de données pluviométrique 

 Les données de la série de précipitation temporelles sont :

 | Temps Heure : Minutes | Valeur |
| --------------------- | ------ |
| Time H:M             | Value |
| 0:00                  | 1.17   |
| 0:05                  | 1.73   |
| 0:10                  | 3.33   |
| 0:15                  | 6.00   |
| 0:20                  | 7.32   |
| 0:25                  | 13.50  |
| 0:30                  | 7.35   |
| 0:35                  | 4.80   |
| 0:40                  | 3.50   |
| 0:45                  | 2.00   |
| 0:50                  | 1.40   |
| 0:55                  | 0.74   |

> ### Modification des données 
 Si on veut modifier les données de n'import objet (noeuds, points de sortie, conduites, ... etc), on procède comme suit :
 
 1. Sélectionner l'objet;
 2. Cliquer sur le bouton modifier, comme suit.
   
 ![Figure 30](./Figures/Capture_30.png) 

 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail. 
  
> ### Définition de la pluviométrie
 Après avoir entré la série de précipitation, on doit spécifier la manière de considérer cette précipitation, en utilisant le modèle de la pluviométrie **Rain gages**, comme suite.

 1. Cliquer sur l'élément **hydrology**, puis **Rain Gages**;
 2. Ensuit, cliquer sur le bouton **Add Object**;
 3. Cliquer sur n'import quel endroit dans la carte pôur ajouter la pluviométrie (voir la figure ci-après).  

 ![Figure 31](./Figures/Capture_31.png)

 Les significations des données à saisir sont mentionnées dans les tableaux suivant :

 1. Rain gage (Pluviométrie) :   

 | Propriété (property) | Unité SI |Signification |
| ------------- | ---------- | ------------------ |
| Rain Format  | - | Format de représentation des données pluviométriques : INTENSITY : intensité en (mm/h) mesurée sur chaque intervalle de temps. **VOLUME** : hauteur précipitée en (mm) mesurée sur chaque intervalle de temps. **CUMULATIVE** : cumul de hauteur précipitée en (mm) depuis le début de l'événement pluvieux |
| Rain Interval | h ou hh:mm | Intervalle de temps de mesure du pluviomètre |
| Data Source  | - | Source des données pluviométriques : **TIMESERIES** : données entrées par l'utilisateur. **FILE** : données dans un fichier externe.|

2. TIMESERIES :

| Property | Unité SI | Signification  |
| -------- | -------- | --------------- |  
| Series Name | - | Nom de la série temporelle de pluviométrie. Double cliquer pour éditer la série |

 Pour introduire correctement les paramètres, il faut suivre la figure suivante (étapes par ordre):

 ![Figure 32](./Figures/Capture_32.png)

 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail.

> ## Saisie des données des sous-bassins versants
 Le réseau d'assainissement est conçu pour drainer principalement les eaux pluviales.

 Selon la topographie, on décompose le terrain en sous bassins (**Subcatchments**). Les eaux de chaque sous-bassin s'écoulent en surface jusqu'à entrer dans un avaloir puis dans un regard (noeud).

 Pour le terrain du projet (le site de l'institut IT), on suppose le découpage suivant:

 ![Figure 34](./Figures/Capture_34.png)

> ### Tracé des sous-bassins 
 Pour tracer les sous bassins à drainer par le réseau d'assainissement, on procède comme indique la figure suivante:
 1. Cliquer sur le bouton **Add a sub catchment**;
 2. Assurer que la mesure des surfaces est automatique, c-à-d., à partir de la carte.
    
 ![Figure 33](./Figures/Capture_33.png)

 Ensuite, tracer les sous sous-bassins comme si vous travaillez sous le logiciel **MapInfo**. 

 !!! Important
     Pour fermer la forme tracée d'un sous-bassin, cliquer à droite.  
 
 ![Figure 35](./Figures/Capture_35.png)

Continuer le tracé des (06) sous bassins, puis les renommer (chaque bassin doit avoir son nom) comme suit:

 ![Figure 36](./Figures/Capture_36.png)

 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail.

Fin du TP5 ... RDV TP6. 

---

> # Paramètres et lancement de simulation 

> ## Buts du TP       
 Dans ce TP, nous allons voir comment :
 1. Introduire les données nécessaires aux sous-bassins (subcatchments);
 2. Choisir les paramètres de simulation;
 3. Lancer la simulation et visualiser les résultats.

 !!! Info
     Consulter la série de vidéos sous le lien [suivant](https://www.youtube.com/watch?v=g-3xPFLx9HU&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq) pour regarder la manière de manipuler les différentes étapes :

     https://www.youtube.com/watch?v=g-3xPFLx9HU&list=PLQ-seRm9Djl6WvlKn3-1jJPpxNxUDaRjq 
     
> ### Ouverture du projet 
 1. Lancer (démarrer) le logiciel EPA-SWMM 5.1;
 2. Le projet **Exercice_1.inp** s'ouvre automatiquement, sinon .. ; 
 3. Aller au menu **File -> Open**, puis ouvrir le projet déjà créé au cours des TPs précédents.      

> ### Vérification des données pluviométriques
 Avant de commencer les taches relatives à ce TP, vérifie est ce que les données pluviométriques sont bien paramétrées.

 ![Figure 37](./Figures/Capture_37.png)

 !!! Attention Important
     1. Vérifier l'intervalle du temps **Time Interval**, il doit correspondre (égale) au pas du temps de la série de précipitation (Voir la séction : **La série temporelle de précipitation** TP5). Dans ce cas mettre **00:05**, c-à-d., un pas de cinq (05) minutes.
     2. Vérifier la série de précipitation utilisée. Elle peut être stockée dans un fichier, donc on doit indiquer le nom du fichier **Detal File : File Name & Rain Units**. Dans ce cas, on utilise la série de précipitation déjà créée, donc assurer que son nom est choisi dans la propriété **Séries Name**.    

> ## Saisie des données nécessaires des sous-bassins (subcatchments)
 Dans le TP précédent (N°05) nous avons tracé et renommé les sous bassins conformément les données (SB1, SB2, ... SB6). Dans la suite, nous devons compléter les données nécessaires.

 1. **Name** : indique le nom de sous-bassin;
 2. **Rain Gage** : indique la la source des données pluviométriques. Dans ce cas, on met **RG1** (voir le TP précédent) pour tous les sous-bassins;
 3. **Outlet** : indique le noeud (le regard) qui reçoit l'eau ruissellée du sous bassin. Par exemple, les eaux du sous bassin **SB1** s'écoulent vers le regard **9**. Pour les autres sous bassins, voir le tableau ci-après.
 4. **Area** : c'est la surface de sous bassin (donnée en ha). Dans ce cas, SWMM calcule les surfaces automatiquement, car on a met l'option **Auto-Length :On**. Dans le cas contraire, on doit introduire la surface de chaque sous bassin manuellement.
 5. **%Slope** : indique la pente moyenne de sous-bassin (donnée en %).       

 ![Figure 38](./Figures/Capture_38.png)

> ### Saisie des regards de drainage de chaque sous bassin (Outlet)
 Dans la suite, on suppose que les eaux de ruissellement des sous bassins (SB1, SB2, ... SB6) seront reçu par les regards suivants:

 |Sous bassin (Subcatchment) | Source de données pluvio (Rain gage) | Noeud (regard) recevant le ruissellement (Outlet) |
 | --- | --- | --------------------- |
 | SB1 | RG1 | 9 |
 | SB2 | RG1 | 7 |
 | SB3 | RG1 | 5 |
 | SB4 | RG1 | 4 |
 | SB5 | RG1 | 3 |
 | SB6 | RG1 | 2 |
 
 !!! check
     * Revérifier les données que vous avez saisi;
     * N'oubliez jamais d'enregistrer le travail.

> ## Choix des paramètres de simulation
 Pour réaliser les calculs et la simulation du fonctionnement du réseau d'assainissement élaborer, on procède comme suit :

1. **Start Analysis on** : Indique le jour et l'heure de début d'analyse du réseau. Laisser les valeurs telles qu'elles sont (sans changement);
2. **Start Reporting on** : Indique la date et l'heure de début du rapport d'analyse. Laisser les valeurs telles qu'elles sont (sans changement);
3. **End Analysis on** : Indique la date et l'heure de fin d'analyse. Pour ce cas, laisser la date sans changement et changer l'heure à **04:00**, c-à-d., le logiciel réalise une simulation sur 04 heures.

 Changer les valeurs comme suit:      

 ![Figure 39](./Figures/Capture_39.png)

 Ensuite,

 ![Figure 40](./Figures/Capture_40.png)

 !!! check
     * N'oubliez jamais d'enregistrer le travail.

> ## Lancement du calcul et simulation
 Pour lancer la simulation, procéder comme suit. Si tous les paramètres sont bien choisis et mis en place, la simulation réussie sans problème :

 ![Figure 41](./Figures/Capture_41.png)

> ## Affichage des résultats
 Le logiciel EPA-SWMM permet l'affichage des résultats sous différentes formes:

> ### Affichage des rapports des résultats
 Pour afficher les résultats du calcul et simulation, suivre les étapes suivantes:

 ![Figure 42](./Figures/Capture_42.png)

> ### Affichage des profils en long des canalisations
 Dans la figure suivante, nous voulons tracer le profil des canalisation d'assainissement du réseau en commençant par le noeud (regard) N°09, jusqu'au noeud N°01.

 ![Figure 43](./Figures/Capture_43.png)

 EPA-SWMM affiche le profil comme suit :

 ![Figure 44](./Figures/Capture_44.png)

 De même pour les tronçons (du noeud 4 au noeud 2):

 ![Figure 45](./Figures/Capture_45.png)

> ### Affichage de propagation de l'écoulement dans un tronçon
 SWMM permet de visualiser la propagation des ondes de l'écoulement à un instant donné.

 Dans l'exemple suivant, on s'intéresse au tronçon 5 par exemple.

 ![Figure 46](./Figures/Capture_46.png)

 Le résultat sera comme suit :

 ![Figure 47](./Figures/Capture_47.png)
   
> ### Affichage des principaux résultats d'un tronçon
 Les principaux résultats relatifs à un tronçon de canalisation sont:
 
 1. Le débit d'écoulement;
 2. La vitesse d'écoulement;
 3. La profondeur d'écoulement.

 Pour les afficher, procéder comme suit:

 ![Figure 48](./Figures/Capture_48.png)

 Les résultats sont :

  ![Figure 49](./Figures/Capture_49.png)

> ## Devoir 





 
