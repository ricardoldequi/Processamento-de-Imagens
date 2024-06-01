using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.DataVisualization.Charting;


namespace ProcessamentoImagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Função para pegar o máximo da matriz
        private int GetMaximo(int[,] matriz)
        {
            int maximo = int.MinValue;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] > maximo)
                    {
                        maximo = matriz[i, j];
                    }
                }
            }

            return maximo;
        }

        // Função para pegar o minimo da matriz
        private int GetMinimo(int[,] matriz)
        {
            int minimo = int.MaxValue;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] < minimo)
                    {
                        minimo = matriz[i, j];
                    }
                }
            }

            return minimo;
        }

        // Função para pegar o valor médio da matriz
        private int GetMedia(int[,] matriz)
        {
            int soma = 0;

            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    soma += matriz[i, j];
                }
            }

            int media = soma / (matriz.GetLength(0) * matriz.GetLength(1));
            return media;
        }

        // Função para pegar o valor mediano da matriz
        private int GetMediana(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            int mediana = QuickSelect(elementos, 0, elementos.Length - 1, elementos.Length / 2);
            return mediana;
        }

        private int GetSuave(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            int meio = QuickSelect(elementos, 0, elementos.Length - 1, 4);
            if (rb3x3.Checked)
            {
                meio = QuickSelect(elementos, 0, elementos.Length - 1, 4);
            }
            else if (rb5x5.Checked)
            {
                meio = QuickSelect(elementos, 0, elementos.Length - 1, 12);
            }
            else if (rb7x7.Checked)
            {
                meio = QuickSelect(elementos, 0, elementos.Length - 1, 24);
            }

            int min = QuickSelect(elementos, 0, elementos.Length - 1, 0);
            int max = QuickSelect(elementos, 0, elementos.Length - 1, elementos.Length - 1);
            if (meio > max)
            {
                meio = max;
            }
            else if (meio < min)
            {
                meio = min;
            }

            return meio;
        }


        private int GetOrdem(int[,] matriz)
        {
            int tamanho = matriz.GetLength(0) * matriz.GetLength(1);
            int[] elementos = new int[tamanho];

            int index = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    elementos[index] = matriz[i, j];
                    index++;
                }
            }

            int ordem = QuickSelect(elementos, 0, elementos.Length - 1, (int)nupOrdem.Value);
            return ordem;
        }

        private int QuickSelect(int[] arr, int left, int right, int k)
        {
            if (left == right)
                return arr[left];

            int pivotIndex = Partition(arr, left, right);

            if (k == pivotIndex)
                return arr[k];
            else if (k < pivotIndex)
                return QuickSelect(arr, left, pivotIndex - 1, k);
            else
                return QuickSelect(arr, pivotIndex + 1, right, k);
        }

        private int Partition(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int storeIndex = left;

            for (int i = left; i < right; i++)
            {
                if (arr[i] < pivot)
                {
                    Swap(arr, storeIndex, i);
                    storeIndex++;
                }
            }

            Swap(arr, storeIndex, right);
            return storeIndex;
        }

        private void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

       
        private double GetGaussian(int[,] vizi, double sigma)
        {
            double soma = 0;

            int size = vizi.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int valorPixel = vizi[i, j];
                    double exponente = -(i * i + j * j) / (2 * sigma * sigma);
                    double peso = Math.Exp(exponente) / (2 * Math.PI * sigma * sigma);
                    soma += valorPixel * peso;
                }
            }

            return soma;
        }



        public Bitmap ConverteEscalaCinza(Bitmap imgOriginal)
        {
            int largura = imgOriginal.Width;
            int altura = imgOriginal.Height;

            // Cria uma nova imagem em escala de cinza
            Bitmap ImagemCinza = new Bitmap(largura, altura);

            // Loop para percorrer todos os pixels da imagem original
            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    // Obtém o pixel da imagem original
                    Color corOriginal = imgOriginal.GetPixel(x, y);

                    // Calcula a luminosidade usando a média ponderada das coordenadas RGB
                    int luminosidade = (int)(0.3 * corOriginal.R + 0.59 * corOriginal.G + 0.11 * corOriginal.B);

                    // Define o pixel na imagem em escala de cinza com a mesma luminosidade em todos os canais RGB
                    Color corCinza = Color.FromArgb(luminosidade, luminosidade, luminosidade);
                    ImagemCinza.SetPixel(x, y, corCinza);
                }
            }

            return ImagemCinza;
        }

        public Bitmap ConverteNegativo(Bitmap imgOriginal)
        {
            // Clone a imagem original
            Bitmap negative = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            // Loop pelos pixels da imagem
            for (int x = 0; x < imgOriginal.Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Color pixelColor = imgOriginal.GetPixel(x, y);

                    // Calcula o negativo da cor
                    Color negativo = Color.FromArgb(255 - pixelColor.R, 255 - pixelColor.G, 255 - pixelColor.B);

                    // Define o pixel na imagem negativa
                    negative.SetPixel(x, y, negativo);
                }
            }

            return negative;
        }



        public Bitmap FlipIN(Bitmap imgOriginal)
        {
            int width = imgOriginal.Width;
            int height = imgOriginal.Height;

            Bitmap flippedImage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = imgOriginal.GetPixel(x, height - y - 1);
                    flippedImage.SetPixel(x, y, pixel);
                }
            }

            return flippedImage;
        }

        public Bitmap FlipOut(Bitmap imgOriginal)
        {
            int width = imgOriginal.Width;
            int height = imgOriginal.Height;

            Bitmap flippedImage = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = imgOriginal.GetPixel(width - x - 1, y);
                    flippedImage.SetPixel(x, y, pixel);
                }
            }

            return flippedImage;
        }

        public string BuscaPixelsImagemString(Bitmap imgOriginal)
        {
            int largura = imgOriginal.Width;
            int altura = imgOriginal.Height;

            // Cria um StringBuilder para armazenar os valores dos pixels
            var sb = new StringBuilder();

            // Percorre os pixels da imagem
            for (int y = 0; y < altura; y++)
            {
                for (int x = 0; x < largura; x++)
                {
                    Color cor = imgOriginal.GetPixel(x, y);
                    // Adiciona os valores dos componentes RGB separados por espaço
                    sb.AppendFormat("{0} {1} {2} ", cor.R, cor.G, cor.B);
                }
                // Adiciona uma quebra de linha no final de cada linha de pixels
                sb.AppendLine();
            }

            // Retorna os valores dos pixels como uma única string
            return sb.ToString();
        }

        public void SalvaPixelsImagemString(string pixelValues, string filename)
        {
            // Escreve os valores dos pixels em um arquivo de texto
            File.WriteAllText(filename, pixelValues);
        }

        public Bitmap RecortarImagem(Bitmap imgOriginal, int x, int y, int larguraRecorte, int alturaRecorte)
        {
            // Verifica se as coordenadas de recorte estão dentro dos limites da imagem
            if (x < 0 || y < 0 || x + larguraRecorte > imgOriginal.Width || y + alturaRecorte > imgOriginal.Height)
            {
                MessageBox.Show("As coordenadas de recorte não coincidem com a imagem.");
            }

            // Cria uma nova imagem para armazenar o recorte
            Bitmap imagemRecortada = new Bitmap(larguraRecorte, alturaRecorte);

            // Loop para copiar os pixels da região de recorte da imagem original para a nova imagem
            for (int i = 0; i < larguraRecorte; i++)
            {
                for (int j = 0; j < alturaRecorte; j++)
                {
                    // Obtém o pixel da posição correspondente na imagem original
                    Color corOriginal = imgOriginal.GetPixel(x + i, y + j);
                    // Define o pixel na nova imagem na mesma posição
                    imagemRecortada.SetPixel(i, j, corOriginal);
                }
            }

            return imagemRecortada;
        }

        public Bitmap ConverteImagemBinaria(Bitmap imgOriginal, bool usarMediana)
        {
            int limiar = (int)numLimiar.Value;

            // Pega dimensões da imagem
            int largura = imgOriginal.Width;
            int altura = imgOriginal.Height;

            // Cria uma nova imagem binária
            Bitmap ImagemBinaria = new Bitmap(largura, altura);

            // Loop pelos pixels da imagem
            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura; y++)
                {
                    Color pixelColor = imgOriginal.GetPixel(x, y);

                    int intensidade;
                    if (usarMediana)
                    {
                        // Armazena os valores de intensidade de cada componente de cor
                        List<int> intensidades = new List<int>();
                        intensidades.Add(pixelColor.R);
                        intensidades.Add(pixelColor.G);
                        intensidades.Add(pixelColor.B);

                        // Ordena a lista de intensidades
                        intensidades.Sort();

                        // Calcula o valor mediano das intensidades
                        intensidade = intensidades[intensidades.Count / 2];
                    }
                    else
                    {
                        // Calcula a média das componentes de cor
                        intensidade = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    }

                    // Define o valor do pixel na imagem binária
                    if (intensidade > limiar)
                    {
                        ImagemBinaria.SetPixel(x, y, Color.White); // Branco
                    }
                    else
                    {
                        ImagemBinaria.SetPixel(x, y, Color.Black); // Preto
                    }
                }
            }

            return ImagemBinaria;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Configure o OpenFileDialog para permitir apenas arquivos de imagem.
            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Carregue a imagem selecionada no PictureBox.
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }
        private void btSalvarImagem_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image == null)
            {
                MessageBox.Show("A imagem resultante não pode ser salva porque ela não existe.");
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Imagem JPEG|*.jpg|Imagem PNG|*.png|Todos os arquivos|*.*";
            saveFileDialog1.Title = "Salvar Imagem Resultante";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                ImageFormat format;
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        format = ImageFormat.Jpeg;
                        break;
                    case 2:
                        format = ImageFormat.Png;
                        break;
                    case 3:
                        format = ImageFormat.Tiff;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }

                pictureBox3.Image.Save(saveFileDialog1.FileName, format);
            }
        }

        private void Negativa_Click(object sender, EventArgs e)
        {
            // validar se existe imagem adicionada
            if (pictureBox1.Image != null)
            {

                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                //chama a funcao de converter pra negativo
                Bitmap negativeImage = ConverteNegativo(imgOriginal);

                // Exibe a imagem gerada
                pictureBox3.Image = negativeImage;
            }
        }
        private void Array_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Clone a imagem do PictureBox1
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                // Obtenha os valores dos pixels como uma string
                string pixelValues = BuscaPixelsImagemString(originalImage);

                // Exiba os valores dos pixels no TextBox1
                textBox1.Text = pixelValues;

                // Especifique o diretório onde você deseja salvar o arquivo
                string pastaDoApp = @"C:\Users\ricardo\Downloads\ProcessamentoImagens\Data";

                // Verifique se o diretório existe; se não, crie-o
                if (!Directory.Exists(pastaDoApp))
                {
                    Directory.CreateDirectory(pastaDoApp);
                }

                // Salve os valores dos pixels em um arquivo de texto
                string caminhoDoArquivo = Path.Combine(pastaDoApp, "arquivo.txt");
                SalvaPixelsImagemString(pixelValues, caminhoDoArquivo);

                // Exiba uma mensagem informando que o arquivo foi salvo com sucesso
                MessageBox.Show("Arquivo salvo com sucesso em:\n" + caminhoDoArquivo, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void carregarImagem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Configure o OpenFileDialog para permitir apenas arquivos de imagem.
            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Carregue a imagem selecionada no PictureBox.
                pictureBox2.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        // Função de adição
        private void btAdicao_Click(object sender, EventArgs e)
        {
            //busca as imagens selecionadas nas duas 
            Bitmap image1 = new Bitmap(pictureBox1.Image);
            Bitmap image2 = new Bitmap(pictureBox2.Image);


            // Trata para ver se não existe imagem em um dos campos
            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            // Verifica se o tamanho e o formato de ambas imagens conhecidem 
            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            // Cria um novo bitmap, com a largura e a altura da primeira imagem
            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);


            // For para mapear todos os pixeis 
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    //Color para pegar o valor dos pixeis R, G, B
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);

                    // Soma cada "camada" da matriz
                    int r = color1.R + color2.R;
                    int g = color1.G + color2.G;
                    int b = color1.B + color2.B;

                    // Trunca para não passar de 255
                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);

                    // Seta os pixeis que estão sem valor com a soma dos valores r, g, b
                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            // Exibe a imagem no pictureBox imagem final
            pictureBox3.Image = imagemResultado;
        }

        // Função de subtração
        // Segue os mesmos passos da adição, até a parte da subtração
        private void btSubtracao_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap(pictureBox1.Image);
            Bitmap image2 = new Bitmap(pictureBox2.Image);

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);

                    // Math.Abs para os valores não serem negativos
                    int r = Math.Abs(color1.R - color2.R);
                    int g = Math.Abs(color1.G - color2.G);
                    int b = Math.Abs(color1.B - color2.B);

                    imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            pictureBox3.Image = imagemResultado;

        }

        private void btCortarImagem_Click(object sender, EventArgs e)
        {
            // Certifique-se de que há uma imagem carregada no PictureBox1
            if (pictureBox1.Image != null)
            {

                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                // Obtenha os valores de x, y, larguraRecorte e alturaRecorte dos controles numéricos
                //x: Essa é a coordenada horizontal do ponto de início do recorte
                //y: Essa é a coordenada vertical do ponto de início do recorte
                //larguraRecorte: Esse valor especifica a largura da região a ser recortada a partir do ponto de início (x) na imagem original
                //alturaRecorte: Esse valor especifica a altura da região a ser recortada a partir do ponto de início (y) na imagem original
                int x = (int)numValorX.Value;
                int y = (int)numValorY.Value;
                int larguraRecorte = (int)numLarguraRecorte.Value;
                int alturaRecorte = (int)numAlturaRecorte.Value;

                // Crie uma função para recortar a imagem
                Bitmap imagemRecortada = RecortarImagem(imgOriginal, x, y, larguraRecorte, alturaRecorte);

                // Exiba a imagem recortada no PictureBox3
                pictureBox3.Image = imagemRecortada;
            }
        }

        private void btConcatenarImagens_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap(pictureBox1.Image);
            Bitmap image2 = new Bitmap(pictureBox2.Image);

            if (image1 == null || image2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem Concatenadas.");
                return;
            }
            Bitmap imagemConcatenada = new Bitmap(image1.Width * 2, image1.Height);

            // Loop para copiar os pixels da primeira imagem para a imagem concatenada
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color corOriginal = image1.GetPixel(x, y);
                    imagemConcatenada.SetPixel(x, y, corOriginal);
                }
            }

            // Loop para copiar os pixels da segunda imagem para a imagem concatenada
            for (int x = 0; x < image2.Width; x++)
            {
                for (int y = 0; y < image2.Height; y++)
                {
                    Color corOriginal = image2.GetPixel(x, y);
                    imagemConcatenada.SetPixel(x + image2.Width, y, corOriginal);
                }
            }

            pictureBox3.Image = imagemConcatenada;
        }

        private void btEscalaCinza_Click(object sender, EventArgs e)
        {
            // Certifique-se de que há uma imagem carregada no PictureBox1
            if (pictureBox1.Image != null)
            {
                // Clone a imagem do PictureBox1
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                // Crie uma função para converter a imagem em negativo
                Bitmap ImagemCinza = ConverteEscalaCinza(originalImage);

                // Exiba a imagem convertida no PictureBox3
                pictureBox3.Image = ImagemCinza;
            }
        }

        private void btRgbBinario_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na  caixa IMAGEM 1");
                return;
            }

            // Clone a imagem do PictureBox1
            Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

            Bitmap ImagemBinaria;
            if (rbPelaMedia.Checked)
            {
                ImagemBinaria = ConverteImagemBinaria(imgOriginal, false); // Usa média
            }
            else if (rbPelaMediana.Checked)
            {
                ImagemBinaria = ConverteImagemBinaria(imgOriginal, true); // Usa mediana
            }
            else
            {
                MessageBox.Show("Selecione um tipo de cálculo (média ou mediana).");
                return;
            }

            // Exibe a imagem convertida no PictureBox3
            pictureBox3.Image = ImagemBinaria;
        }

        private void btlupIN_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na  caixa IMAGEM 1.");
                return;

            }
            // Carrega a imagem original do PictureBox1
            Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

            // Aplica o flip in na imagem original
            Bitmap imagemFlipIN = FlipIN(imgOriginal);

            // Exibe a imagem flip in no PictureBox3
            pictureBox3.Image = imagemFlipIN;

        }



        private void btlupOUT_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na  caixa IMAGEM 1.");
                return;
            }

            Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

            Bitmap imagemFlipOUT = FlipOut(imgOriginal);

            pictureBox3.Image = imagemFlipOUT;
        }


        // Função Histograma
        private void btEqualizarHistograma_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na  caixa IMAGEM 1.");
                return;
            }
            Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

            // Carrega a imagem em escala de cinza
            Bitmap ImagemCinza = ConverteEscalaCinza(imgOriginal);



            // Cria um array de 256 inteiros, correspondendo os valores da escala de cinza
            int[] histograma = new int[256];
            for (int i = 0; i < ImagemCinza.Width; i++)
            {
                for (int j = 0; j < ImagemCinza.Height; j++)
                {
                    // Calcula o peso da escala de cinza
                    Color c = ImagemCinza.GetPixel(i, j);
                    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    histograma[gray]++;
                }
            }

            // Calcula a função de distribuição acumulada (CDF) do histograma
            int[] cdf = new int[256];
            int sum = 0;
            for (int i = 0; i < 256; i++)
            {
                sum += histograma[i];
                cdf[i] = sum;
            }

            // Equaliza o histograma
            int pixels = ImagemCinza.Width * ImagemCinza.Height;
            for (int i = 0; i < 256; i++)
            {
                cdf[i] = (int)(255 * ((float)cdf[i] / pixels));
            }

            // Cria uma nova imagem equalizada
            Bitmap imagemEqualizada = new Bitmap(ImagemCinza.Width, ImagemCinza.Height);
            for (int i = 0; i < ImagemCinza.Width; i++)
            {
                for (int j = 0; j < ImagemCinza.Height; j++)
                {
                    Color c = ImagemCinza.GetPixel(i, j);
                    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    int eqGray = cdf[gray];
                    Color eqColor = Color.FromArgb(eqGray, eqGray, eqGray);
                    imagemEqualizada.SetPixel(i, j, eqColor);
                }
            }

            pictureBox3.Image = imagemEqualizada;

            // Coloca o histograma final em um vetor de 256 valores, correspondendo os valores da escala de cinza
            int[] histogramaFinal = new int[256];
            for (int i = 0; i < imagemEqualizada.Width; i++)
            {
                for (int j = 0; j < imagemEqualizada.Height; j++)
                {
                    Color c = imagemEqualizada.GetPixel(i, j);
                    int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                    histogramaFinal[gray]++;
                }
            }

            // Adiciona o gráfico do primeiro histograma
            chart1.Series.Clear();
            chart1.Series.Add("Imagem Inicial");
            chart1.Series["Imagem Inicial"].ChartType = SeriesChartType.Column;
            chart1.Series["Imagem Inicial"].Points.DataBindY(histograma);
            chart1.ChartAreas[0].AxisY.Maximum = histograma.Max() + 10;

            // Adiciona o gráfico do segundo histograma
            chart2.Series.Clear();
            chart2.Series.Add("Imagem Final");
            chart2.Series["Imagem Final"].ChartType = SeriesChartType.Column;
            chart2.Series["Imagem Final"].Points.DataBindY(histogramaFinal);
            chart2.ChartAreas[0].AxisY.Maximum = histogramaFinal.Max() + 10;

        }

        private void btAnd_Click(object sender, EventArgs e)
        {
            System.Drawing.Image image1 = pictureBox1.Image;
            System.Drawing.Image image2 = pictureBox2.Image;




            if (pictureBox1 == null || pictureBox2 == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (image1.Width != image2.Width || image1.Height != image2.Height || image1.PixelFormat != image2.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    Color color1 = ((Bitmap)image1).GetPixel(x, y);
                    Color color2 = ((Bitmap)image2).GetPixel(x, y);

                    // Operação simples, usando apenas o sinal do AND
                    Color corResultado = Color.FromArgb(color1.R & color2.R, color1.G & color2.G, color1.B & color2.B);
                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            pictureBox3.Image = imagemResultado;
        }

        private void btOr_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (pictureBox1.Image.Width != pictureBox2.Image.Width ||
                pictureBox1.Image.Height != pictureBox2.Image.Height ||
                pictureBox1.Image.PixelFormat != pictureBox2.Image.PixelFormat)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho e formato para serem somadas.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    Color color1 = ((Bitmap)pictureBox1.Image).GetPixel(x, y);
                    Color color2 = ((Bitmap)pictureBox2.Image).GetPixel(x, y);

                    // Aplica a operação lógica OR bitwise às componentes de cor dos pixels
                    Color corResultado = Color.FromArgb(
                        color1.R | color2.R,
                        color1.G | color2.G,
                        color1.B | color2.B);

                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            pictureBox3.Image = imagemResultado;
        }

        private void btNot_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na  caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemResultado = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    Color corOriginal = ((Bitmap)pictureBox1.Image).GetPixel(x, y);

                    // Aplica a operação NOT a cada componente de cor
                    Color corResultado = Color.FromArgb(
                        255 - corOriginal.R,
                        255 - corOriginal.G,
                        255 - corOriginal.B);

                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            pictureBox3.Image = imagemResultado;
        }

        private void brXor_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            if (pictureBox1.Image.Width != pictureBox2.Image.Width || pictureBox1.Image.Height != pictureBox2.Image.Height)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho para serem combinadas");
                return;
            }

            Bitmap imagemResultado = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    Color corImagem1 = ((Bitmap)pictureBox1.Image).GetPixel(x, y);
                    Color corImagem2 = ((Bitmap)pictureBox2.Image).GetPixel(x, y);

                    // Aplica a operação XOR a cada componente de cor dos pixels correspondentes
                    Color corResultado = Color.FromArgb(
                        corImagem1.R ^ corImagem2.R,
                        corImagem1.G ^ corImagem2.G,
                        corImagem1.B ^ corImagem2.B);

                    imagemResultado.SetPixel(x, y, corResultado);
                }
            }

            pictureBox3.Image = imagemResultado;
        }

        private void btBlending_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("Por favor, selecione duas imagens");
                return;
            }

            // blending = α×A+β×B
            double alpha = (double)numericUpDownAlpha.Value;
            double beta = 1 - alpha;

            // Verifica se os coeficientes de ponderação estão entre 0 e 1
            if (alpha < 0 || alpha > 1 || beta < 0 || beta > 1)
            {
                MessageBox.Show("Os coeficientes de ponderação devem estar entre 0 e 1");
                return;
            }

            if (pictureBox1.Image.Width != pictureBox2.Image.Width || pictureBox1.Image.Height != pictureBox2.Image.Height)
            {
                MessageBox.Show("As imagens precisam ter o mesmo tamanho para serem combinadas");
                return;
            }

            Bitmap imagemResultado = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    Color corImagem1 = ((Bitmap)pictureBox1.Image).GetPixel(x, y);
                    Color corImagem2 = ((Bitmap)pictureBox2.Image).GetPixel(x, y);

                    // Calcula a nova cor usando a combinação linear
                    int novoR = (int)(alpha * corImagem1.R + beta * corImagem2.R);
                    int novoG = (int)(alpha * corImagem1.G + beta * corImagem2.G);
                    int novoB = (int)(alpha * corImagem1.B + beta * corImagem2.B);

                    novoR = Math.Max(0, Math.Min(255, novoR));
                    novoG = Math.Max(0, Math.Min(255, novoG));
                    novoB = Math.Max(0, Math.Min(255, novoB));

                    imagemResultado.SetPixel(x, y, Color.FromArgb(novoR, novoG, novoB));
                }
            }


            pictureBox3.Image = imagemResultado;
        }

        // Função Vizinhança Max
        private void btvMaximo_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            // Converte a imagem original em escala de cinza
            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            // Seleciona o tamanho da vizinhança
            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;
            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;
            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;
            }

            // Filtra a imagem
            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    // Cria um array dos pixeis percoridos e pega o tamanho da vizinhança
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    // Percorre esse array
                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    // Usa a função GetMaximo para pegar os valores máximos da vizinhança
                    int maximo = GetMaximo(vizinhanca);

                    // Coloca esses valores conforme é cada vizinhança
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(maximo, maximo, maximo));
                }
            }

            // Exibe a imagem filtrada
            pictureBox3.Image = imagemFiltrada;
        }

        private void btvMinimo_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            // Converte a imagem original em escala de cinza
            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            // Seleciona o tamanho da vizinhança
            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;
            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;
            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;
            }

            // Filtra a imagem
            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    // Cria um array dos pixeis percoridos e pega o tamanho da vizinhança
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    // Percorre esse array
                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    // Usa a função GetMinimo para pegar os valores mínimos da vizinhança
                    int minimo = GetMinimo(vizinhanca);

                    // Coloca esses valores conforme é cada vizinhança
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(minimo, minimo, minimo));
                }
            }

            // Exibe a imagem filtrada
            pictureBox3.Image = imagemFiltrada;


        }

        private void btvMedia_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            // Converte a imagem original em escala de cinza
            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            // Seleciona o tamanho da vizinhança
            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;
            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;
            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;
            }

            // Filtra a imagem
            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    // Cria um array dos pixeis percoridos e pega o tamanho da vizinhança
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    // Percorre esse array
                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    // Usa a função GetMedia para pegar os valores médios da vizinhança
                    int media = GetMedia(vizinhanca);

                    // Coloca esses valores conforme é cada vizinhança
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(media, media, media));
                }
            }

            // Exibe a imagem filtrada
            pictureBox3.Image = imagemFiltrada;
        }

        private void btvMediana_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            // Converte a imagem original em escala de cinza
            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            // Seleciona o tamanho da vizinhança
            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;
            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;
            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;
            }

            // Filtra a imagem
            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    // Cria um array dos pixeis percoridos e pega o tamanho da vizinhança
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    // Percorre esse array
                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            // Trata os casos em que xIndex e yIndex estão fora dos limites da imagem
                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    // Usa a função GetMediana para pegar os valores medianos da vizinhança
                    int mediana = GetMediana(vizinhanca);

                    // Coloca esses valores conforme é cada vizinhança
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(mediana, mediana, mediana));
                }
            }

            // Exibe a imagem filtrada
            pictureBox3.Image = imagemFiltrada;
        }

        private void btOrdem_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;
                nupOrdem.Maximum = 8;

            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;
                nupOrdem.Maximum = 17;

            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;
                nupOrdem.Maximum = 35;

            }

            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    int ordem = GetOrdem(vizinhanca);
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(ordem, ordem, ordem));
                }
            }

            pictureBox3.Image = imagemFiltrada;
        }

        private void btSuave_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            int tamanhoVizinhanca = 0;
            if (!rb3x3.Checked && !rb5x5.Checked && !rb7x7.Checked)
            {
                MessageBox.Show("Selecione o tamanho da vizinhança para filtrar!");
                return;
            }
            if (rb3x3.Checked)
            {
                tamanhoVizinhanca = 3;

            }
            if (rb5x5.Checked)
            {
                tamanhoVizinhanca = 5;

            }
            if (rb7x7.Checked)
            {
                tamanhoVizinhanca = 7;

            }

            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    int ordem = GetSuave(vizinhanca);
                    imagemFiltrada.SetPixel(x, y, Color.FromArgb(ordem, ordem, ordem));
                }
            }

            pictureBox3.Image = imagemFiltrada;
        }

        private void btGaussiana_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nenhuma imagem informada, por favor, insira a imagem na caixa IMAGEM 1.");
                return;
            }

            Bitmap imagemOriginal = (Bitmap)pictureBox1.Image;
            Bitmap imagemCinza = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            for (int x = 0; x < imagemOriginal.Width; x++)
            {
                for (int y = 0; y < imagemOriginal.Height; y++)
                {
                    Color color1 = imagemOriginal.GetPixel(x, y);
                    int r = color1.R;
                    int g = color1.G;
                    int b = color1.B;
                    int gray = (r + g + b) / 3;

                    Color novaCor = Color.FromArgb(color1.A, gray, gray, gray);
                    imagemCinza.SetPixel(x, y, novaCor);
                }
            }

            int tamanhoVizinhanca = 5;
            double sigma = (double)nupGaussiana.Value;
            Bitmap imagemFiltrada = new Bitmap(imagemCinza.Width, imagemCinza.Height);

            for (int x = 0; x < imagemCinza.Width; x++)
            {
                for (int y = 0; y < imagemCinza.Height; y++)
                {
                    int[,] vizinhanca = new int[tamanhoVizinhanca, tamanhoVizinhanca];

                    for (int i = 0; i < tamanhoVizinhanca; i++)
                    {
                        for (int j = 0; j < tamanhoVizinhanca; j++)
                        {
                            int xIndex = x + i - tamanhoVizinhanca / 2;
                            int yIndex = y + j - tamanhoVizinhanca / 2;

                            if (xIndex < 0)
                            {
                                xIndex = 0;
                            }
                            if (xIndex >= imagemCinza.Width)
                            {
                                xIndex = imagemCinza.Width - 1;
                            }
                            if (yIndex < 0)
                            {
                                yIndex = 0;
                            }
                            if (yIndex >= imagemCinza.Height)
                            {
                                yIndex = imagemCinza.Height - 1;
                            }

                            vizinhanca[i, j] = imagemCinza.GetPixel(xIndex, yIndex).R;
                        }
                    }

                    double gaussian = GetGaussian(vizinhanca, sigma);

                    int pixelNovo = (int)Math.Round(gaussian);
                    if (pixelNovo < 0) pixelNovo = 0;
                    else if (pixelNovo > 255) pixelNovo = 255;
                    Color imagemNova = Color.FromArgb(pixelNovo, pixelNovo, pixelNovo);
                    imagemFiltrada.SetPixel(x, y, imagemNova);
                }
            }

            pictureBox3.Image = imagemFiltrada;
        }
    }
}
    












