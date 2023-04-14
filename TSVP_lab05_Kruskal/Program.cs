using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSVP_lab05_Kruskal
{
    internal class Program
    {
        static void Kruskal(int[,] graph)
        {
            int n = graph.GetLength(0);
            List<Edge> edges = new List<Edge>();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int weight = graph[i, j];

                    if (weight > 0)
                    {
                        edges.Add(new Edge(i, j, weight));
                    }
                }
            }

            edges.Sort(); // сортировка ребер по весу

            int[] parents = new int[n];

            for (int i = 0; i < n; i++)
            {
                parents[i] = i; // инициализация всех вершин как отдельных деревьев
            }

            List<Edge> mst = new List<Edge>();

            foreach (Edge edge in edges)
            {
                int parent1 = Find(parents, edge.Vertex1);
                int parent2 = Find(parents, edge.Vertex2);

                if (parent1 != parent2) // если ребро не образует цикл
                {
                    mst.Add(edge);
                    parents[parent1] = parent2; // объединяем два дерева в одно
                }
            }

            Console.WriteLine("Минимальное остовное дерево:");
            foreach (Edge edge in mst)
            {
                Console.WriteLine("Ребро ({0}, {1}) весом {2}", edge.Vertex1, edge.Vertex2, edge.Weight);
            }
        }

        static int Find(int[] parents, int vertex)
        {
            if (parents[vertex] != vertex)
            {
                parents[vertex] = Find(parents, parents[vertex]); // компрессия пути
            }

            return parents[vertex];
        }

        class Edge : IComparable<Edge>
        {
            public int Vertex1 { get; set; }
            public int Vertex2 { get; set; }
            public int Weight { get; set; }

            public Edge(int vertex1, int vertex2, int weight)
            {
                Vertex1 = vertex1;
                Vertex2 = vertex2;
                Weight = weight;
            }

            public int CompareTo(Edge other)
            {
                return Weight.CompareTo(other.Weight);
            }
        }
        static void Main(string[] args)
        {
            int[,] graph = {
            { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
            { 0, 0, 2, 0, 0, 0, 6, 7, 0 }
        };

            Kruskal(graph); // вызов алгоритма Краскала для графа
            Console.ReadKey();
        }
    }
}
