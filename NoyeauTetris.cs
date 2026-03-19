
namespace NoyauTetris
{
    /* Représente les couleurs utilisées dans le jeu. */
    public enum TetrinoCouleur
    {
        Blanc,
        Noir,
        Rouge,
        Jaune,
        Bleu
    }

    /* Définit les dimensions de la grille du jeu. */
    public class JeuTetris
    {
        public static int LargeurGrille = 12;
        public static int HauteurGrille = 15;
    }
        //Defition de la position d'un carré.
    public class Position
    {
        public int X;
        public int Y;
        
        public Position(int x, int y)
        {
            X=x;
            Y=y;
        }

        public Position DeplaceGauche()
        {
            return new Position(this.X + 1, this.Y);
        }

        public Position DeplaceDroite()
        {
             return new Position(this.X - 1, this.Y);
        }

        public Position DeplaceBas()
        {
             return new Position(this.X, this.Y + 1);
        }
    }

    //Definition d'un tableau de quadruplets de positions.
    public class Tetrino
    {
          public static Position[][] TetrinosTab = new Position[][]
        {
            // carre
            new Position[] { new Position(0, 0), new Position(1, 0),
            new Position(0, -1), new Position(1, -1) },
            // barre horizontale
            new Position[] { new Position(0, 0), new Position(1, 0),
            new Position(2, 0), new Position(3, 0) },
            // barre verticale
            new Position[] { new Position(0, 0), new Position(0, -1),
            new Position(0, -2), new Position(0, -3) }
        };

        public Position[][] Indice = TetrinosTab;
        public Position PositionOrigine 
    }



}
}
