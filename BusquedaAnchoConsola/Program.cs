using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusquedaAnchoConsola
{
    class Program
    {
        //public static  
        private static Dictionary<int, Helper.Record> Registry = new Dictionary<int, Helper.Record>();
        private static Dictionary<string, List<Helper.Record>> Index = new Dictionary<string, List<Helper.Record>>();
        public static Helper.Position _p = new Helper.Position();
        public static int count = 0;
        public static int _p1 = 0,_p2=1;
        public static int[,] Solution = Helper.getSolution();
        public static DateTime start = System.DateTime.Now;
        public static int lvl=0;
        public static string time_expand;
        public static List<Helper.Record> Index0 = new List<Helper.Record>(),
            Index1 = new List<Helper.Record>(),
            Index2 = new List<Helper.Record>(),
            Index3 = new List<Helper.Record>(),
            Index4 = new List<Helper.Record>(),
            Index5 = new List<Helper.Record>(),
            Index6 = new List<Helper.Record>(),
            Index7 = new List<Helper.Record>(),
            Index8 = new List<Helper.Record>(),
            Index9 = new List<Helper.Record>();
        static void Main(string[] args)
        {
            string TimeElapsed = "";
            List<Helper.Historial> _H = new List<Helper.Historial>();
            //Helper.Historial H_Node = new Helper.Historial();      
            Helper.Record Record_init=new Helper.Record();
            int[,] M= Helper.getMatrix();
            //Helper.printMatrix(M);
            bool isSolution = false;
            //_p = Helper.getZero(M);
            Record_init.ID = count;
            Record_init.M = M;
            Record_init.parent = 0;
            Record_init.move = Helper.Moves.Inicial;
            Registry.Add(count, Record_init);
            RecordExist(Record_init);
           // _H.Add(H_Node);
            while(!isSolution){
                lvl += 1;
                int N_expand = 0;
                for (int p = _p1; p < _p2; p++)
                {
                    List<Helper.Record> _records = Expande(Registry[p]);
                     N_expand += 1;
                     Console.WriteLine("factor de ramificacion: " + _records.Count);
                    foreach(Helper.Record _r in _records ){
                        count += 1;
                        _r.ID = count;
                        Registry.Add(count, _r);
                        if (Solution.Cast<int>().SequenceEqual(_r.M.Cast<int>()))
                        { 
                            isSolution = true;
                            List<Helper.Record> solutions = new List<Helper.Record>();
                            Console.Clear();
                            Console.WriteLine("Soluciones: " + Registry.Count);
                            //Console.WriteLine("factor de ramificacion: " + _records.Count);
                            Console.WriteLine("Ramificacion: " + (_p2 - _p1));
                            Console.WriteLine("Nivel: " + lvl);
                            Console.WriteLine("Numero De indices: " + Index.Count);
                            //Console.WriteLine("Valores Index:  Index_0: " + Index0.Count + " Index_1: " + Index1.Count + " Index_2: " + Index2.Count + " Index_3: " + Index3.Count + " Index_4: " + Index4.Count + " Index_5: " + Index5.Count + " Index_6: " + Index6.Count + " Index_7: " + Index7.Count + " Index_8: " + Index8.Count);
                            Console.WriteLine(TimeElapsed);                           
                            Console.WriteLine();
                            Console.WriteLine("Solucion");
                            Helper.printMatrix(_r.M);
                            Console.WriteLine();
                            Console.WriteLine("Origen");
                            Helper.printMatrix(Helper.getMatrix());
                            Console.WriteLine();
                            int parent = _r.parent;
                            Helper.Record record = new Helper.Record();
                            record = _r;
                            bool finish = false;
                            solutions.Add(record);
                            while (!finish)
                            {
                               
                                record = new Helper.Record();
                                record = Registry[parent];
                                solutions.Add(record);
                                parent = record.parent;
                                //Console.Clear();
                                //Console.WriteLine("Parent: "+parent);
                                if (parent == 0)
                                {
                                    record = new Helper.Record();
                                    record = Registry[parent];
                                    solutions.Add(record);
                                    finish = true;
                                }
                                
                            }
                            Console.WriteLine();
                            Console.WriteLine("Moves:");
                            for (int d = solutions.Count - 1; d >=0; d--)
                            {
                                string moves = ""; 
                                moves+=("," + solutions[d].move.ToString());
                                Console.Write(moves);
                            }

                                Console.WriteLine();
                                Console.WriteLine("Simulator:");
                                Console.ReadLine();
                               // Console.Clear();
                                for (int d = solutions.Count - 1; d >= 0; d--)
                                {
                                    Thread.Sleep(500);
                                    Console.Clear();
                                    Helper.printMatrix(solutions[d].M);
                                   
                                }
                                Console.WriteLine("FIN :)");
                                Console.ReadLine();
                                p = _p2-1;
                        }
                        Console.Clear();
                        Console.WriteLine("Soluciones: "+Registry.Count);
                        //Console.WriteLine("factor de ramificacion: " + _records.Count);
                        Console.WriteLine("Ramificacion: " + (_p2 -_p1));
                        Console.WriteLine("Expandiendo:" + (_p2 - _p1)+"/"+N_expand+" TME: "+time_expand+" Milisegundos");
                        Console.WriteLine("Nivel: " + lvl);
                       // Console.WriteLine("Valores Index:  Index_0: " + Index0.Count + " Index_1: " + Index1.Count + " Index_2: " + Index2.Count + " Index_3: " + Index3.Count + " Index_4: " + Index4.Count + " Index_5: " + Index5.Count + " Index_6: " + Index6.Count + " Index_7: " + Index7.Count + " Index_8: " + Index8.Count );
                        Console.WriteLine("Numero De indices: "+Index.Count);
                        Console.WriteLine("Tiempo: Horas:" + (DateTime.Now.Subtract(start).Hours) + " Minutos: " + (DateTime.Now.Subtract(start).Minutes) + " Segundos:" + (DateTime.Now.Subtract(start).Seconds));
                        TimeElapsed = "Tiempo: Horas:" + (DateTime.Now.Subtract(start).Hours) + " Minutos: " + (DateTime.Now.Subtract(start).Minutes) + " Segundos:" + (DateTime.Now.Subtract(start).Seconds);
                        //Helper.printMatrix(_r.M);
                        Console.WriteLine();
                        //Helper.printMatrix(Helper.getSolution());
                        
                    }
                    //Console.WriteLine("Factor de ramificacion: " + _records.Count);

                }
              
                _p1 = _p2;
                _p2 = count;  
            }
            //Console.WriteLine(isSolution);
            //Console.ReadLine();
        }
        public static List<Helper.Record> Expande(Helper.Record Node)
        {
            DateTime startExpand = DateTime.Now;
            List<Helper.Record> Nodos = new List<Helper.Record>();
            //Helper.Historial T_H_Node = new Helper.Historial();
            Helper.Record new_node = new Helper.Record();
            Helper.Position p_0 = Helper.getZero(Node.M);
            //int[,] M = (int[,])Node.M.Clone();
            if (/*Node.move != Helper.Moves.Arriba && */Helper.IsPossibleMoveIt(Helper.Moves.Abajo, Node.M))
            //if ( Helper.IsPossibleMoveIt(Helper.Moves.Abajo, Node.M))
            {
                int[,] _M = (int[,])Node.M.Clone();
                new_node = new Helper.Record();
                int temp = _M[p_0.r + 1, p_0.c];
                _M[p_0.r, p_0.c] = temp;
                _M[p_0.r + 1, p_0.c] = 0;
                new_node.M = (int[,])_M.Clone();
                new_node.parent = Node.ID;
                new_node.move = Helper.Moves.Abajo;
                //Helper.printMatrix(new_node.M);
                // if (!Helper.NodeExist(Registry, new_node.M))
                if (!RecordExist(new_node))
                    Nodos.Add(new_node);
            }
            if (/*Node.move != Helper.Moves.Abajo &&*/ Helper.IsPossibleMoveIt(Helper.Moves.Arriba, Node.M))
            //if ( Helper.IsPossibleMoveIt(Helper.Moves.Arriba, Node.M))
            {
                int[,] _M = (int[,])Node.M.Clone();
                new_node = new Helper.Record();
                int temp = _M[p_0.r - 1, p_0.c];
                _M[p_0.r, p_0.c] = temp;
                _M[p_0.r - 1, p_0.c] = 0;
                new_node.M = (int[,])_M.Clone();
                new_node.parent = Node.ID;
                new_node.move = Helper.Moves.Arriba;
                //Helper.printMatrix(new_node.M);
                // if (!Helper.NodeExist(Registry, new_node.M)) 
                if (!RecordExist(new_node))
                    Nodos.Add(new_node);
            }
            if (/*Node.move != Helper.Moves.Izquierda && */Helper.IsPossibleMoveIt(Helper.Moves.Derecha, Node.M))
            //if ( Helper.IsPossibleMoveIt(Helper.Moves.Derecha,Node.M))
            {
                int[,] _M = (int[,])Node.M.Clone();
                new_node = new Helper.Record();
                int temp = _M[p_0.r, p_0.c + 1];
                _M[p_0.r, p_0.c] = temp;
                _M[p_0.r, p_0.c + 1] = 0;
                new_node.M = (int[,])_M.Clone();
                new_node.parent = Node.ID;
                new_node.move = Helper.Moves.Derecha;
                //Helper.printMatrix(new_node.M);
                //if (!Helper.NodeExist(Registry, new_node.M)) 
                if (!RecordExist(new_node))
                    Nodos.Add(new_node);
            }
            if (/*Node.move != Helper.Moves.Derecha && */Helper.IsPossibleMoveIt(Helper.Moves.Izquierda, Node.M))
            //if (Helper.IsPossibleMoveIt(Helper.Moves.Izquierda, Node.M))
            {
                int[,] _M = (int[,])Node.M.Clone();
                new_node = new Helper.Record();
                int temp = _M[p_0.r, p_0.c - 1];
                _M[p_0.r, p_0.c] = temp;
                _M[p_0.r, p_0.c - 1] = 0;
                new_node.M = (int[,])_M.Clone();
                new_node.parent = Node.ID;
                new_node.move = Helper.Moves.Izquierda;
                //Helper.printMatrix(new_node.M);
                //if (!Helper.NodeExist(Registry, new_node.M)) 
                if (!RecordExist(new_node))
                    Nodos.Add(new_node);
            }
            time_expand = DateTime.Now.Subtract(startExpand).Milliseconds.ToString();
            return Nodos;
        }
        public static bool RecordExist(Helper.Record R)
        {
            bool response = false;
            //string idx=R.M[0,0]+""+R.M[0,1]+R.M[0,2];

            string idx = "";
            for (int ix = 0; ix < R.M.GetLongLength(0); ix++)
            {
                idx += R.M[0, ix];
            }
                //R.M[0, 0] + "" + R.M[0, 1];
            KeyValuePair<string,List<Helper.Record>> _idx = /*Index[idx]*/Index.Where(x => x.Key.Equals(idx)).FirstOrDefault();

            if (_idx.Key != null)
            {
               Helper.Record _r= _idx.Value.Where(x => x.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault();
               if (_r != null)
                   response = true;
               else
                   Index[idx].Add(R);
            }
            else
            {
                List<Helper.Record> n_idx= new List<Helper.Record>();
                Index.Add(idx, n_idx);
                Index[idx].Add(R);
                
            }

            return response;
        }
        //public static bool RecordExist(Helper.Record R)
        //{
        //    bool response = false;
        //    int idx = R.M[0, 0];
        //    Helper.Record _r = new Helper.Record();
        //    _r = R;
        //    switch (idx)
        //    {
        //        case 0 :
        //            if (Index0!=null)
        //               // var xi =Index0.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>()));
        //            //response = Index0.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index0.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index0.Add(R);
        //            if (Index0.Count > 2)
        //                Index0.Sort();
        //            break;
        //        case 1:
        //            if (Index1 != null)
        //                //response = Index1.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index1.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index1.Add(_r);
        //            if (Index1.Count > 2)
        //                Index1.Sort();
        //            break;
        //        case 2:
        //            if (Index2 != null)
        //                //response = Index2.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index2.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index2.Add(R);
        //            if (Index2.Count > 2)
        //                Index2.Sort();
        //            break;
        //        case 3:
        //            if (Index3 != null)
        //                //response = Index3.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index3.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index3.Add(R);
        //            if (Index3.Count > 2)
        //                Index3.Sort();
        //            break;
        //        case 4:
        //            if (Index4 != null)
        //                //response = Index4.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index4.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index4.Add(R);
        //            if (Index4.Count > 2)
        //                Index4.Sort();
        //            break;
        //        case 5:
        //            if (Index5 != null)
        //                //response = Index5.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index5.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index5.Add(R);
        //            if (Index5.Count > 2)
        //                Index5.Sort();
        //            break;
        //        case 6:
        //            if (Index6 != null)
        //                //response = Index6.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index6.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index6.Add(R);
        //            if (Index6.Count > 2)
        //                Index6.Sort();
        //            break;
        //        case 7:
        //            if (Index7 != null)
        //                //response = Index7.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index7.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index7.Add(R);
        //            if (Index7.Count > 2)
        //                Index7.Sort();
        //            break;
        //        case 8:
        //            if (Index8 != null)
        //                //response = Index8.Find(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())) != null ? true : false;
        //            response = Index8.Where(i => i.M.Cast<int>().SequenceEqual(R.M.Cast<int>())).FirstOrDefault() != null ? true : false;
        //            if (!response)
        //                Index8.Add(R);
        //            if (Index8.Count > 2)
        //                Index8.Sort();
        //            break;
        //    }
        //    return response;
        //}

        
    }
}
