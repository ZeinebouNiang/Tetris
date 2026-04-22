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
        public static int LargeurGrille = 10;
        public static int HauteurGrille = 20;
        public TetrinoCouleur[,] Grille;
        public int PosX;    //position horizontale dans la grille
        public int PosY;    //position verticale
        public TetrinoCouleur CouleurCourante;  //couleur du bloc
        public Tetrino TetrinoCourant; //Tetrino en cours de chute.
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
            //Creation du Tetrino en cours de chute.
            TetrinoCourant = new Tetrino();

            //Generation d'un nouveau Tetrino aléatoire.
            TetrinoCourant.NouveauTetrino();

        }

        public void Bas()
        {
            if(PeutDescendre())
            {
                TetrinoCourant.PositionOrigine.Y++;
            }
            else 
            {
                FigerTetrino();
                TetrinoCourant.NouveauTetrino();
            }
        }

        public void Gauche()
        {
            TetrinoCourant.PositionOrigine.X--;
        }

        public void Droite()
        {
            TetrinoCourant.PositionOrigine.X++;
        }

        public bool PeutDescendre()
        {
            Position[] position = TetrinoCourant.Position();

            for(int i = 0; i < 4; i++)
            {
               //Empeche collision avec le bas de la grille.
                if (position[i].Y + 1 >= HauteurGrille || 
                    (position[i].Y >= 0 && Grille[position[i].X, position[i].Y] != TetrinoCouleur.Blanc))
                {
                    return false;
                }
            }
            return true;
        }

        public void PoserBloc()
        {
            if (PosX >= 0 && PosX < LargeurGrille &&
                PosY >= 0 && PosY < HauteurGrille)
            {
                Grille[PosX, PosY] = TetrinoCourant.Couleur;
            }
        }

         public void Demarrer()
        {
            //reinitialiser Grille
            for(int x = 0; x < LargeurGrille; x++)
            {
                for (int y = 0; y < HauteurGrille; y++)
                {
                    Grille[x, y] = TetrinoCouleur.Blanc;
                }
            }

            //Generation d'un nouveau Tetrino aléatoire.
            PosX = LargeurGrille / 2;
            PosY = 0;
            CouleurCourante = TetrinoCouleur.Rouge;
        }

        public void Tombe()
        {
            while (PeutDescendre())
            {
                TetrinoCourant.PositionOrigine.Y++;
            }

            FigerTetrino();
            TetrinoCourant.NouveauTetrino();
        }
        public void FigerTetrino()
        {
            Position[] positions = TetrinoCourant.Position();

            for (int i = 0; i < 4; i++)
            {
                int X = positions[i].X;
                int Y = positions[i].Y;

                //eviter les erreurs hors grille
                if (X >=0 && X < LargeurGrille && Y >= 0 && Y < HauteurGrille)
                {
                    Grille[X, Y] = TetrinoCourant.Couleur;
                }
            }
            SupprimerLignesPleines();
        }

        //Detecter une ligne pleine
        public bool LignePleine(int y)
        {
            for(int x = 0; x < LargeurGrille; x++)
            {
                if (Grille[x, y] == TetrinoCouleur.Blanc)
                {
                    return false;
                }
            }
            return true;
        }

        public void SupprimerLigne(int y)
        {
            //Faire descendre toutes les lignes au-dessus
            for(int j = y; y > 0; j--)
            {
                for(int x = 0; x < LargeurGrille; x++)
                {
                    Grille[x, j] = Grille[x, j - 1];
                }
            }
            //Vider la ligne du haut
            for(int x = 0; x < LargeurGrille; x++)
            {
                Grille[x, 0] = TetrinoCouleur.Blanc;
            }
        }

        //Verifier toutes les lignes
        public void SupprimerLignesPleines()
        {
            for(int y = 0; y < HauteurGrille; y++)
            {
                if(LignePleine(y))
                {
                    SupprimerLigne(y);
                }
            }
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

    //Creation d'un Tetrino compose de 4 carres.
    public class Tetrino
    {
        public int Indice;

        // Position de l'origine du Tetrino dans le repere du jeu.
        public Position PositionOrigine;
        public TetrinoCouleur Couleur;
      
      //Tableau de quadruplets de positions possibles.
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
        
        //Tableau des couleurs possibles d'un Tetrino.
          public static TetrinoCouleur[] CouleursTetrinos =
        {
            TetrinoCouleur.Rouge,
            TetrinoCouleur.Jaune,
            TetrinoCouleur.Bleu,
        };

        //Constructeur par défaut.
        public Tetrino()
        {
            Indice = 0;
            PositionOrigine = new Position(0, 0);
            Couleur = TetrinoCouleur.Rouge;

        }

        //Methode qui retourne le quadruplet de positions du Tetrino.
        public Position[] Position()
        {
           Position[] positionsJeu = new Position[4];
              for(int i = 0; i < 4; i++)
              {
                 positionsJeu[i] = new Position(
                      PositionOrigine.X + TetrinosTab[Indice][i].X,
                      PositionOrigine.Y + TetrinosTab[Indice][i].Y
                 );
              }
            return positionsJeu;
        }

        //Methode qui genere un nouveau tetrino aléatoire.
        public void NouveauTetrino()
        {
            //Choix de la forme du Tetrino.
            Indice = new Random().Next(0, TetrinosTab.Length);

            //Choix de la couleur du Tetrino.
            Couleur = CouleursTetrinos[new Random().Next(0, CouleursTetrinos.Length)];

            //Position d'apparition du Tetrino.
            PositionOrigine = new Position(JeuTetris.LargeurGrille / 2, 0);
        }
    }
}
