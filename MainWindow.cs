/* Fichier MainWindow.axaml.cs
 * Gère l'interface du jeu de Tetris : la fenêtre graphique et 
 * l'ensemble des interactions du jeu.
 * Auteur : ...
 * Version : alpha
 */


using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System;
using Avalonia.Threading;
// à ajouter à partir de l'itération 1
using NoyauTetris;

namespace InterfaceTetris;

/* Gère la fenêtre principale du jeu de Tetris, et l'ensemble des interactions du jeu. */
public partial class MainWindow : Window
{
    public JeuTetris jeu;
    /* Minuteur qui déclanche régulièrement un évènement. */
    public DispatcherTimer Minuteur;

    //*Ajout Iteration 1*/ 

//* Constantes utilisées par DessinerCadre
    public const int TailleCarre = 22;
    public const int EpaisseurCadre = 12;
    
    public MainWindow()
    {
        InitializeComponent();
        // Défini la taille de la fenêtre à partir des constantes
        Width = 368;
        Height = 514;
        // Définit le texte de InfoText
        InfoText.Text = "Zone de texte";
        // Défini la taille du canvas à partir des constantes
        TetrisCanvas.Width = 288;
        TetrisCanvas.Height = 354;
        // Défini la taille des boutons à partir des constantes
        StartButton.Width = 288;
        StartButton.Height = 40;
        QuitButton.Width = 288;
        QuitButton.Height = 40; 
        // Initialise le minuteur pour faire descendre le tetrino courant toutes les 500 milisecondes
        Minuteur = new DispatcherTimer();
        Minuteur.Interval = TimeSpan.FromMilliseconds(500);
        Minuteur.Tick += (s, e) => { BasInterface();};   
        // détecte le clic sur le bouton Démarrer, déclanche l'évènement Demarrer, puis appelle la méthode DemarrerTetris
        StartButton.Click += (s, e) => { DemarrerInterface();};
        // détecte le clic sur le bouton Quitter, déclanche l'évènement Quiter, puis ferme la fenêtre
        QuitButton.Click += (s, e) => { Close();};
        // détecte la pression d'une touche du clavier, et déclanche l'évènement correspondant
        KeyDown += (s, e) =>
        {
            // Choix des touches à modifier si besoin (voir la documentation de l'énumération Key)
            if (e.Key == Key.Left)
            {
                GaucheInterface();
            }
            else if (e.Key == Key.Right)
            {
                DroiteInterface();
            }
            else if (e.Key == Key.X)
            // si vous disposer d'un pavé numérique, choisir Key.PageUp
            {
                RotationDroiteInterface();
            }
            else if (e.Key == Key.W)
            // si vous disposer d'un pavé numérique, choisir Key.Home
            {
                RotationGaucheInterface();
            }
            else if (e.Key == Key.Down)
            {
                TombeInterface();
            }
        };

        jeu = new JeuTetris();
    } 

    /* Dessine un rectangle dans le TetrisCanvas, à la position (x, y), de largeur width, 
    de hauteur height (en pixels) et de couleur couleur. */
    public void DessinerCarre(int x, int y, int with, int height, Avalonia.Media.IBrush couleur)
    {
        TetrisCanvas.Children.Add(new Avalonia.Controls.Shapes.Rectangle
        {
            Width = with,
            Height = height,
            Fill = couleur,
            Stroke = Avalonia.Media.Brushes.Black,    //contour
            StrokeThickness = 1.5,                      //epaisseur
            Margin = new Thickness(x, y, 0, 0) 
        });
    }

    //*Ajout Iteration 1*/
   
  //* Dessine le cadre du terrain de jeu dans la zone graphique*/
   public void DessinerCadre()
     {
          // calcul de la largeur totale (zone de jeu + bordures) et de la hauteur totale*/
      int largeur = JeuTetris.LargeurGrille * TailleCarre + 2 * EpaisseurCadre;
      int hauteur = JeuTetris.HauteurGrille * TailleCarre + 2 * EpaisseurCadre;

      // cadre autour rectangle noir 
      DessinerCarre(0, 0, largeur, hauteur, ConvertirCouleur(TetrinoCouleur.Noir));

      // rectangle blanc intérieur
      DessinerCarre(
        EpaisseurCadre,
        EpaisseurCadre,
        JeuTetris.LargeurGrille * TailleCarre,
        JeuTetris.HauteurGrille * TailleCarre,
        ConvertirCouleur(TetrinoCouleur.Blanc)
    );
    }

    //* Ajout Iteration 1*/

    //* change les couleurs du noyau du jeu  */
public Avalonia.Media.IBrush ConvertirCouleur(TetrinoCouleur couleur)
{
    if (couleur == TetrinoCouleur.Blanc)
    {
        return Avalonia.Media.Brushes.White;
    }
    else if (couleur == TetrinoCouleur.Noir)
    {
        return Avalonia.Media.Brushes.Black;
    }
    else if (couleur == TetrinoCouleur.Rouge)
    {
        return Avalonia.Media.Brushes.Red;
    }
    else if (couleur == TetrinoCouleur.Jaune)
    {
        return Avalonia.Media.Brushes.Yellow;
    }
    else
    {
        return Avalonia.Media.Brushes.Blue;
    }
}

    /* Modifiction Iteration 1 */
   public void DemarrerInterface()
{
    Console.WriteLine("Démarrage du jeu");

      // efface tous ce qui a déjà été dessiné
    TetrisCanvas.Children.Clear();

      // dessine le cadre 
    DessinerCadre();

     // dessine trois carrés colorés en diagonale pour tester l'affichage
   DessinerCarre(12, 12, 22, 22, ConvertirCouleur(TetrinoCouleur.Rouge));
   DessinerCarre(34, 34, 22, 22, ConvertirCouleur(TetrinoCouleur.Jaune));
   DessinerCarre(56, 56, 22, 22, ConvertirCouleur(TetrinoCouleur.Bleu));
}

    /* ... */
    public void DroiteInterface()
    {
        jeu.AllerDroite();
    }

    /* ... */
    public void GaucheInterface()
    {
        jeu.AllerGauche();
    }

    /* ... */
    public void BasInterface()
    {
        jeu.Descendre();
    }

    /* ... */
    public void TombeInterface()
    {
        Console.WriteLine("Déplacement rapide en bas à coder...");

    }

    /* ... */
    public void RotationDroiteInterface()
    {
        Console.WriteLine("Rotation à droit à coder...");
    }

    /* ... */
    public void RotationGaucheInterface()
    {
        Console.WriteLine("Rotation à gauche à coder...");
    }
}
