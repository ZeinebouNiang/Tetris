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
    public const int TailleCarre = 20;
    public const int EpaisseurCadre = 10;
    public const int TailleBouton = 36;
    public const int Marge = 50;
    
    public MainWindow()
    {
        InitializeComponent();
        // Défini la taille de la fenêtre à partir des constantes
        Width = 2 * Marge + JeuTetris.LargeurGrille * TailleCarre + 2 * EpaisseurCadre;
        Height = 4 * Marge + JeuTetris.HauteurGrille * TailleCarre + 2 * EpaisseurCadre + 2 * TailleBouton;
        // Définit le texte de InfoText
        InfoText.Text = "Zone de texte";
        // Défini la taille du canvas à partir des constantes
        TetrisCanvas.Width = JeuTetris.LargeurGrille * TailleCarre + 2 * EpaisseurCadre;
        TetrisCanvas.Height = JeuTetris.HauteurGrille * TailleCarre + 2 * EpaisseurCadre;
        // Défini la taille des boutons à partir des constantes
        StartButton.Width = JeuTetris.LargeurGrille * TailleCarre + 2 * EpaisseurCadre;
        StartButton.Height = TailleBouton;
        QuitButton.Width = JeuTetris.LargeurGrille * TailleCarre + 2 * EpaisseurCadre;
        QuitButton.Height = TailleBouton;
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
    //Ajout Iteration 2.

    //Met à jour l'affichage du jeu en fonction de l'état du jeu (position des blocs, etc.)
    public void DessinerJeu()
    {
        //Netroyer le canvas.
        TetrisCanvas.Children.Clear();

        //Dessiner le cadre du jeu.
        DessinerCadre();

        //Dessiner les blocs figés dans la grille.
        for(int x = 0; x < JeuTetris.LargeurGrille; x++)
        {
            for(int y = 0; y < JeuTetris.HauteurGrille; y++)
            {
                if(jeu.Grille[x, y] != TetrinoCouleur.Blanc)
                {
                    DessinerCarre(
                        EpaisseurCadre + x * TailleCarre,
                        EpaisseurCadre + y * TailleCarre,
                        TailleCarre,
                        TailleCarre,
                    ConvertirCouleur(jeu.Grille[x, y])
                    );
                }
            }
        }

        //Dessiner le tetrino en cours de chute.
        Position[] position = jeu.TetrinoCourant.Position();

        for(int i = 0; i < 4; i++)
        {
            //Ne pas dessiner les parties du tetrino qui sont au dessus du cadre.
            if (position[i].Y >= 0) 
            {
                DessinerCarre(
                    EpaisseurCadre + position[i].X * TailleCarre,
                    EpaisseurCadre + position[i].Y * TailleCarre,
                    TailleCarre,
                    TailleCarre,
                ConvertirCouleur(jeu.TetrinoCourant.Couleur)
                );
             }
        }
    }

    /* Modifiction Iteration 1 */
public void DemarrerInterface()
{
    jeu.Demarrer();

    InfoText.Text = "Jeu en cours";

    DessinerJeu();

    Minuteur.Start();
}

    /* ... */
    public void DroiteInterface()
    {
        jeu.Droite();
        DessinerJeu();
    }

    /* ... */
public void GaucheInterface()
{
    if (jeu.Perdu) return;

    jeu.Gauche();
    DessinerJeu();
}
    /* ... */
public void BasInterface()
{
    jeu.Bas();
    DessinerJeu();

    if (jeu.Perdu)
    {
        Minuteur.Stop();
        InfoText.Text = "GAME OVER";
    }
}

    /* ... */
public void TombeInterface()
{
    if (jeu.Perdu) return; // bloque le jeu apres game over

    jeu.Tombe();
    DessinerJeu();
}

    /* ... */
    public void RotationDroiteInterface()
{
    jeu.RotationDroite();
    DessinerJeu();
}

public void RotationGaucheInterface()
{
    jeu.RotationGauche();
    DessinerJeu();
}
}
