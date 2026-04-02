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
        public TetrinoCouleur[,] Grille;
        public int PosX;    //position horizontale dans la grille
        public int PosY;    //position verticale
        public TetrinoCouleur CouleurCourante;  //couleur du bloc

        public JeuTetris()
        {
            Grille = new TetrinoCouleur[LargeurGrille, HauteurGrille];
            for(int x = 0; x < LargeurGrille; x++)
            {
                for(int y = 0; y < HauteurGrille; y++)
                {
                    Grille[x, y]= TetrinoCouleur.Blanc;
                }
            }
            PosX = LargeurGrille / 2;
            PosY = 0;
            CouleurCourante = TetrinoCouleur.Rouge;
        }

        public void Bas()
        {
            if(PosY < HauteurGrille - 1)
            {
                PosY++;
            }

            if(PeutDescendre())
            {
                PosY++;
            }
            else 
            {
                PoserBloc();
                NouveauBloc();
            }
        }

        public void Gauche()
        {
            if(PosX > 0)
            {
                PosX--;
            }
        }

        public void Droite()
        {
            if(PosX < LargeurGrille - 1)
            {
                PosX++;
            }
        }

        public bool PeutDescendre()
        {
            //collision avec le bas
            if(PosY >= HauteurGrille - 1)
                return false;

            //collision avec un bloc déjà posé
            if(Grille[PosX, PosY + 1] != TetrinoCouleur.Blanc)
                return false;

            else return true;
        }

        public void PoserBloc()
        {
            Grille[PosX, PosY] = CouleurCourante;
        }

         public void Demarrer()
        {
            //vider Grille
            for(int x = 0; x < LargeurGrille; x++)
            {
                for (int y = 0; y < HauteurGrille; y++)
                {
                    Grille[x, y] = TetrinoCouleur.Blanc;
                }
            }

             NouveauBloc();
        }

        public void NouveauBloc()
        {
            PosX = LargeurGrille / 2;
            PosY = 0;
            CouleurCourante = TetrinoCouleur.Rouge;
        }

        public void Tombe()
        {
            while (PeutDescendre())
            {
                PosY++;
            }

            PoserBloc();
            NouveauBloc();
        }
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
        public int Indice;
        public Position PositionOrigine;
        public TetrinoCouleur Couleur;
      
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
          public static TetrinoCouleur[][] CouleursTetrinos = new TetrinoCouleur[][]
        {
            new TetrinoCouleur[] {TetrinoCouleur.Rouge},
            new TetrinoCouleur[]{TetrinoCouleur.Jaune},
            new TetrinoCouleur[]{TetrinoCouleur.Bleu},
        };

        public Position[] Position()
        {
           return TetrinosTab[Indice];
        }
        public NouveauTetrino()
        {
              Random random = new Random();
            int Indice = random.Next(3);
            
        }  
    }
}
