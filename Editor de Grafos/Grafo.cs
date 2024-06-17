using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public class Grafo : GrafoBase, iGrafo
    {
        private bool[] visitado;

        public void AGM(int v)
        {
            // controle
            int[] chaves = new int[getN()];
            bool[] b = new bool[getN()];
            int[] AGM = new int[getN()];
            Stack<int> verticesv = new Stack<int>(getN());

            chaves[0] = 0;

            for (int i = 0; i < getN(); i++)
            {
                chaves[i] = int.MaxValue;
                b[i] = false;
            }
            getVertice(0).setCor(Color.Pink);

            if (verticesv.Count == 0)
                verticesv.Push(0);

            Aresta arestaMinAtual = null;
            Vertice voMinAtual = null;

            int min;

            while (verticesv.Count != getN())
            {
                min = int.MaxValue;

                // percorre todos os vertices visitados
                foreach (int vVisitado in verticesv)
                {
                    // percorre todos os vertices adjacentes aos visitados que ainda nao foram visitados
                    foreach (Vertice vAdjacente in getAdjacentes(vVisitado))
                    {
                        if (verticesv.Contains(vAdjacente.getNum()))
                            continue;

                        Aresta aresta = getAresta(vVisitado, vAdjacente.getNum());
                        // se o peso da aresta é menor que o atual
                        if(aresta.getPeso() < min)
                        {
                            min = aresta.getPeso();
                            arestaMinAtual = aresta;
                            voMinAtual = vAdjacente;

                        }
                    }

                }

                // colore a menor aresta e marca o vertice como visitado
                if(arestaMinAtual != null)
                {
                    verticesv.Push(voMinAtual.getNum());
                    arestaMinAtual.setCor(Color.Green);
                    voMinAtual.setCor(Color.Green);

                    if (verticesv.Count == getN())
                        voMinAtual.setCor(Color.Gold);
                    

                }

            }

        }

        public void caminhoMinimo(int m)
        {
            // 1° verificar o custo, 2° verificar o precedente 3° checar se está aberto ou fechado
            int[] estimativa = new int[getN()];
            int[] i = new int[getN()];
            bool[] b = new bool[getN()];

            // Todos os campos recebem MaxValue para estimativa
            for (int k = 0; k < getN(); k++)
            {
                estimativa[k] = int.MaxValue;
                i[k] = -1;
                b[k] = true;
            }

            // Recebe o primeiro vértice e coloca estimativa 0
            Vertice ver = getVerticeMarcado();
            estimativa[ver.getNum()] = 0;

            while (ExisteVerticeAberto(b))
            {
                int verticeAtual = ObterMenorEstimativa(b, estimativa);
                b[verticeAtual] = false;

                for (int j = 0; j < getN(); j++)
                {
                    try
                    {
                        if(getAresta(verticeAtual, j) != null)
                        {
                            if (b[j] && getAresta(verticeAtual, j).getPeso() != 0)
                            {
                                int novaEstimativa = estimativa[verticeAtual] + getAresta(verticeAtual, j).getPeso();
                                if (novaEstimativa < estimativa[j])
                                {
                                    estimativa[j] = novaEstimativa;
                                    i[j] = verticeAtual;
                                }
                            }
                        }
                    }
                    catch (Exception )
                    {

                    }
                }
            }

            MessageBox.Show("Custo mínimo saindo do vertice " + (ver.getNum() + 1) + " Para o vértice " + m + ": " + estimativa[m - 1], "Menor caminho", MessageBoxButtons.OK);
        }

        private bool ExisteVerticeAberto(bool[] b)  // Mostrar quais são os vértices que ainda não foram fechados
        {
            foreach (bool valor in b)
            {
                if (valor)
                    return true;
            }
            return false;
        }
        private int ObterMenorEstimativa(bool[] b, int[] estimativa) // retorna o vértice com a menor estimativa
        {
            int menore = int.MaxValue;
            int vertice = -1;
            for (int i = 0; i < getN(); i++)
            {
                if (b[i] && estimativa[i] < menore)
                {
                    menore = estimativa[i];
                    vertice = i;
                }
            }
            return vertice;
        }

        public void completarGrafo()
        {
            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++) // percorre a matriz
                {
                    if (i != j)
                        setAresta(i,j, 1);                  

                }

            }
        }

        public bool isEuleriano()
        {
            
            for (int i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0)
                    return false;
            }

            return (getN() > 0) ; 
             
        }

        public bool isUnicursal()
        {
            // exatos 2 vertices de grau impar
            int qtdVerticesImpar = 0;
            for (int i = 0; i < getN(); i++)
            {
                if (grau(i) % 2 != 0) // grau impar
                    qtdVerticesImpar++;
            }

            return (qtdVerticesImpar == 2);
        }

        public void largura(int v)
        {
            Fila f = new Fila(matAdj.GetLength(0));
            f.enfileirar(v);

            visitado = new bool[getN()];
            visitado[v] = true;

            while (!f.vazia())
            {
                v = f.desenfileirar(); // retira o próximo vértice da fila
                for (int i = 0; i < matAdj.GetLength(0); i++)
                {
                    // se I é adjacente a V e I ainda não foi visitado
                    Aresta aresta = getAresta(v, i);
                    if (aresta != null && !visitado[i])
                    {
                        visitado[i] = true; // marca i como visitado
                        f.enfileirar(i); // enfileira i

                        getVertice(v).setCor(Color.Yellow);
                        aresta.setCor(Color.Orange);

                        Thread.Sleep(500);
                    }
                }
            }

        }

        public void numeroCromatico()
        {
            visitado = new bool[getN()];
            Stack<Color> cores = new Stack<Color>(5);
            cores.Push(Color.Blue);
            cores.Push(Color.Red);
            cores.Push(Color.Green);
            cores.Push(Color.Yellow);
            cores.Push(Color.Orange);


            for (int v = 0; v < getN(); v++)
            {
                if (!visitado[v])
                {
                    visitado[v] = true;

                    List<Color> coresAdjacentes = new List<Color>();
                    for (int i = 0; i < getN(); i++)
                    {
                        if (getAresta(v, i) != null && visitado[i])
                        {
                            coresAdjacentes.Add(getVertice(i).getCor());
                        }
                    }

                    Color corDisponivel = default(Color);
                    foreach(Color cor in cores)
                    {
                        if (!coresAdjacentes.Contains(cor))
                        {
                            corDisponivel = cor;
                        }
                    }

                    if (corDisponivel != default(Color))
                    {
                        getVertice(v).setCor(corDisponivel);
                    }
                }
            }
        }

        public String paresOrdenados()
        {
            string paresOrdenados = null;
            for (int i = 0; i < getN(); i++)
            {
                for (int j = 0; j < getN(); j++) // percorre a matriz
                {
                    if( (getAresta(i,j)) != null)
                    {
                        Aresta a = getAresta(i,j);
                        if (a != null)
                        {
                            if (paresOrdenados != null)
                                paresOrdenados += ", ";
                            
                            paresOrdenados += $"({getVertice(i).getRotulo()},{getVertice(j).getRotulo()})";
                        }
                    }

                }

            }

            return "E = { " + paresOrdenados + " }" ;
        }

        public void profundidade(int v)
        {
            if (visitado == null)
                visitado = new bool[getN()];

            visitado[v] = true;

            for (int i = 0; i < matAdj.GetLength(0); i++)
            {
                Aresta aresta = getAresta(v, i);
                if (aresta != null && !visitado[i])
                {
                    profundidade(i);
                    getVertice(v).setCor(Color.Blue);
                    aresta.setCor(Color.Yellow);

                    Thread.Sleep(200);
                }               

            }
        }

        

    }
}
