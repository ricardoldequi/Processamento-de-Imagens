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


namespace ProcessamentoImagens
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                for (int y = 0; y < imgOriginal.Height; y++)
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
            // Certifique-se de que há uma imagem carregada no PictureBox1
            if (pictureBox1.Image != null )
            {
                // Clone a imagem do PictureBox1
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

                // Crie uma função para converter a imagem em negativo
                Bitmap negativeImage = ConverteNegativo(originalImage);

                // Exiba a imagem convertida no PictureBox3
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
                // Clone a imagem do PictureBox1
                Bitmap originalImage = new Bitmap(pictureBox1.Image);

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
                Bitmap imagemRecortada = RecortarImagem(originalImage, x, y, larguraRecorte, alturaRecorte);

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

    }
}
    

