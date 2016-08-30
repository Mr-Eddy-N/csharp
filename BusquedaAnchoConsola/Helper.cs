using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusquedaAnchoConsola
{
   public static class Helper
    {
       #region Enumerables
       public enum Moves
       {
           Derecha, Izquierda, Arriba, Abajo,Inicial
       }
       #endregion
       #region Metodos
       public static int[,] getMatrix()
       {
            // int[,] matrix = new int[3, 3];
            //int[,] matrix = new int[2, 2];
            // matrix = new int[2, 2] { { 1,2 }, { 3,0 } };
            //int [,]matrix = new int[3,3]{{5,4,6},{2,0,1},{3,8,7}};
            int[,] matrix = new int[3, 3] { { 1, 2, 3 }, { 4, 0, 5 }, { 6, 7, 8 } };//me encanta
            //int[,] matrix = new int[4, 4] { {13,10,4,1},{3,15,5,8},{14,11,7,2},{9,12,0,6}};
            // matrix = new int[3, 3] { { 1, 2, 3 }, { 4, 5,6 }, {  7, 8,0 } };
            //bool isSol=  Enumerable.Range(0, matrix.Rank).All(dimension => matrix.GetLength(dimension)
            //    == 
            //    matrix.GetLength(dimension)) && matrix.Cast<int>().SequenceEqual(matrix.Cast<int>());
            //matrix.Cast<int>().SequenceEqual(matrix.Cast<int>());

            return matrix;
       }
       public static int[,] getSolution()
       {
            int[,] matrix = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
            //int[,] matrix = new int[3, 3];
            //int[,] matrix = new int[2, 2];
            // matrix = new int[2, 2] { { 2, 2 }, { 3, 0 } };
            // matrix = new int[2, 2] { { 0, 1 }, { 2, 3 } };
            //matrix = new int[3, 3] { { 1, 2, 3 }, { 6, 5, 4 }, { 7, 8, 0 } };
            //int[,]matrix = new int[3, 3] { { 1,2,3}, {4,5,6}, {7,8,0} };
            //int[,] matrix = new int[4, 4] {{1,2,3,4},{5,6,7,8},{9,10,11,12},{13,14,15,0}};
            // matrix = new int[3, 3] { { 4, 2, 5 },{  1,0,3 }, { 6, 7, 8 } };
            // matrix = new int[3, 3] { { 0, 1, 2 }, { 3, 4, 5 }, { 6, 7, 8 } };
            //matrix = new int[3, 3] { {0,1,3 }, { 4, 2, 5 }, { 6, 7, 8 } };
            return matrix;
       }
       /// <summary>
       /// imprime en consola  la Matrix
       /// </summary>
       /// <param name="M"> Matrix a imprimir</param>
       public static void printMatrix(int[,] M)
       {
           //Console.Clear();
           Console.WriteLine("");
           for (int r = 0; r < M.GetLength(0); r++)
           {
               for (int c = 0; c < M.GetLength(1); c++)
               {
                   Console.Write(""+(c != 0 ? "," : "          ") + M[r, c]);
               }
               Console.WriteLine("");

           }
          // Console.ReadLine();
       }
       public static Position getZero(int[,] M)
       {
           int rango =(int)M.GetLongLength(0);
           Position _P = new Position();
           for (int r = 0; r < rango; r++)
           {
               for (int c = 0; c < rango; c++)
               {
                   if(M[r,c]==0)
                   {
                       _P.r = r;
                       _P.c = c;
                       c = rango;
                       r = rango;
                   }
               }
           }
               return _P;

       }
       public static bool IsPossibleMoveIt(Moves move,int[,] _M)
       {
           int factor = (int)_M.GetLongLength(0);
           bool response = false;
           Position p = getZero(_M);
           switch(move){
               case Moves.Izquierda:
                   if (p.c > 0)
                       response= true;
                   break;
               case Moves.Derecha:
                   if (p.c < factor-1)
                       response = true;

                   break;
               case Moves.Arriba:
                   if (p.r > 0)
                       response = true;

                   break;
               case Moves.Abajo:
                   if (p.r < factor-1)
                       response = true;

                   break;
           }

           return response;
       }
       public static string MatrizAsString(int[,] M)
       {
           string response="";

           return response;


       }
       public static bool NodeExist(Dictionary<int, Helper.Record> R, int[,] M)
       {
           bool response = false;
           //return response;
           KeyValuePair<int,Helper.Record> r = R.Where(x => x.Value.M.Cast<int>().SequenceEqual(M.Cast<int>())).FirstOrDefault();
           if (r.Value != null)
               response = true;
           return response;
       }
       
       #endregion
       #region Clases
       public  class Record
       {
           public int[,] M { get; set; }
           public int parent { get; set; }
           public int ID { get; set; }
           public Moves move { get; set; }

       }
       public class Position
       {
           public int r { get; set; }
           public int c { get; set; }
       }
       public class Historial
       {
          public List<Historial> Arriba { get; set; }
          public List<Historial> Abajo { get; set; }
          public List<Historial> Derecha { get; set; }
          public List<Historial> Izquierda { get; set; }
          public  Record Record { get; set; }
       }

       #endregion
      

    }
   

}
