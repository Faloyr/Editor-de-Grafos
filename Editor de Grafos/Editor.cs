using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Editor_de_Grafos
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();            
        }

       
        private void BtParesOrd_Click(object sender, EventArgs e)
        {
            string pares = g.paresOrdenados();
            MessageBox.Show(pares, "Pares Ordenados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ;
        }

        private void BtGrafoEuleriano_Click(object sender, EventArgs e)
        {
            if(g.isEuleriano())
                MessageBox.Show("O grafo é Euleriano", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não é Euleriano", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtGrafoUnicursal_Click(object sender, EventArgs e)
        {
            if (g.isUnicursal())
                MessageBox.Show("O grafo é Unicursal", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("O grafo não é Unicursal", "Mensagem", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtBuscaProfundidade_Click(object sender, EventArgs e)
        {
        }



        private void BtNovo_Click(object sender, EventArgs e)
        {
            g.limpar();
        }

        private void BtAbrir_Click(object sender, EventArgs e)
        {
            if(OPFile.ShowDialog() == DialogResult.OK)
            {
                g.abrirArquivo(OPFile.FileName);
                g.Refresh();
            }
        }

        private void BtSalvar_Click(object sender, EventArgs e)
        {
            if(SVFile.ShowDialog() == DialogResult.OK)
            {
                g.SalvarArquivo(SVFile.FileName);
            }
        }

        private void BtSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtPeso_Click(object sender, EventArgs e)
        {
            if(BtPeso.Checked)
            {
                BtPeso.Checked = false;
                g.setExibirPesos(false);

            }
            else
            {
                BtPeso.Checked = true;
                g.setExibirPesos(true);
            }
            g.Refresh();
        }

        private void BtPesoAleatorio_Click(object sender, EventArgs e)
        {
            if(BtPesoAleatorio.Checked)
            {
                BtPesoAleatorio.Checked = false;
                g.setPesosAleatorios(false);
            }
            else
            {
                BtPesoAleatorio.Checked = true;
                g.setPesosAleatorios(true);
            }
        }

        private void BtSobre_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Editor de Grafos - 2024/1\n\nDesenvolvido por:\nNatan Macedo de Magalhaes\nVirgilio Borges de Oliveira\nLuis Gonzaga Barbosa Silva 72201231 \n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.", "Sobre o Editor de Grafos...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void completarGrafoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.completarGrafo();
            g.setVerticeMarcado(null);
        }

        private void profundidadeToolStripMenuItem_Click(object sender, EventArgs e) {
            Vertice v = g.getVerticeMarcado();
            if (v == null)
            {
                MessageBox.Show("Selecione um vértice!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                g.profundidade( v.getNum() );
                g.setVerticeMarcado(v);
            }
        }

        private void g_Paint(object sender, PaintEventArgs e) {

        }

        private void larguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vertice v = g.getVerticeMarcado();
            if (v == null)
            {
                MessageBox.Show("Selecione um vértice!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                g.largura(v.getNum());
                g.setVerticeMarcado(v);
            }
        }

        private void aGMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.AGM(0);

        }

        private void nCromaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.numeroCromatico();
        }

        private void caminhoMinimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Vertice v = g.getVerticeMarcado();


            try
            {

                if (v.getRotulo() != "")
                {
                    int vertice = int.Parse(Interaction.InputBox("Informe o rótulo do vértice destino, apenas números", "Menor Caminho"));
                    g.caminhoMinimo(vertice);
                }
            }
            catch (Exception err)
            {
                string msgErro = err.ToString().Contains("NullReferenceException") ? "Selecione o vértice inicial" :
                    err.ToString().Contains("IndexOutOfRangeException") ? "Índice inválido para o grafo" :
                    "Insira o rótulo utilizando apenas números inteiros";

                MessageBox.Show(msgErro, "Erro menor caminho", MessageBoxButtons.OK);
            }
        }
    }
}
