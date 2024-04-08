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
            //pega dimensoes da imagem
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
            //pega dimensoes da imagem
            int largura = imgOriginal.Width;
            int altura = imgOriginal.Height;

            // Cria uma nova imagem negativa
            Bitmap negative = new Bitmap(imgOriginal.Width, imgOriginal.Height);

            // Loop pelos pixels da imagem
            for (int x = 0; x < largura; x++)
            {
                for (int y = 0; y < altura;  y++)
                {
                    Color pixelColor = imgOriginal.GetPixel(x, y);

                    // Calcula o negativo da cor em cada matriz 
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

        public Bitmap ConverteImagemBinaria(Bitmap imgOriginal)
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

                    // Calcula a média das componentes de cor
                    int mediaCor = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // Define o valor do pixel na imagem binária
                    if (mediaCor > limiar)
                    {
                        ImagemBinaria.SetPixel(x, y, Color.FromArgb(255, 255, 255)); // Branco
                    }
                    else
                    {
                        ImagemBinaria.SetPixel(x, y, Color.FromArgb(0, 0, 0)); // Preto
                    }
                }
            }

            return ImagemBinaria;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // OpenFileDialog para permitir apenas arquivos de imagem
            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Carrega a imagem selecionada no PictureBox.
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
            if (pictureBox1.Image != null )
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
                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                string pixelValues = BuscaPixelsImagemString(imgOriginal);

                // mostra os valores no textbox
                textBox1.Text = pixelValues;

                //Diretorio onde salvará o TXT
                string pastaDoApp = @"C:\Users\ricardo\Downloads\ProcessamentoImagens\Data";

                // verifica se existe o diretorio, se sim substitui, se não cria
                if (!Directory.Exists(pastaDoApp))
                {
                    Directory.CreateDirectory(pastaDoApp);
                }

                // Salvando o pixel em um arquivo de texto
                string caminhoDoArquivo = Path.Combine(pastaDoApp, "arquivo.txt");
                SalvaPixelsImagemString(pixelValues, caminhoDoArquivo);

                MessageBox.Show("Arquivo salvo com sucesso em:\n" + caminhoDoArquivo, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void carregarImagem2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos os Arquivos|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        // Função de adição
        private void btAdicao_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap(pictureBox1.Image);

            // Verifica se a primeira imagem foi carregada
            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem.");
                return;
            }

            // Verifica se a operação é adicionar um valor fixo
            if (rbValorFixo.Checked)
            {
                // Obtém o valor fixo fornecido pelo usuário
                int valorFixo = (int)numOperacao.Value;

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = image1.GetPixel(x, y);

                        // Adiciona o valor fixo a cada componente de cor
                        int r = Math.Min(255, color1.R + valorFixo);
                        int g = Math.Min(255, color1.G + valorFixo);
                        int b = Math.Min(255, color1.B + valorFixo);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox3.Image = imagemResultado;
            }
            // Verifica se a operação é somar duas imagens
            else if (rbPelaImagem.Checked)
            {
                Bitmap image2 = new Bitmap(pictureBox2.Image);

                // Verifica se a segunda imagem foi carregada
                if (image2 == null)
                {
                    MessageBox.Show("Por favor, selecione duas imagens para realizar a operação de soma.");
                    return;
                }

                // Verifica se as imagens têm as mesmas dimensões
                if (image1.Width != image2.Width || image1.Height != image2.Height)
                {
                    MessageBox.Show("As imagens precisam ter o mesmo tamanho para serem somadas.");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = image1.GetPixel(x, y);
                        Color color2 = image2.GetPixel(x, y);

                        // Soma os valores das componentes de cor das duas imagens
                        int r = Math.Min(255, color1.R + color2.R);
                        int g = Math.Min(255, color1.G + color2.G);
                        int b = Math.Min(255, color1.B + color2.B);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox3.Image = imagemResultado;
            }
            else
            {
                MessageBox.Show("Por favor, selecione o modo de operação.");
            }
        }


        // Função de subtração
        // Segue os mesmos passos da adição, até a parte da subtração
        private void btSubtracao_Click(object sender, EventArgs e)
        {
            Bitmap image1 = new Bitmap(pictureBox1.Image);

            // Verifica se a primeira imagem foi carregada
            if (image1 == null)
            {
                MessageBox.Show("Por favor, selecione uma imagem.");
                return;
            }

            // Verifica se a operação é subtrair por um valor fixo
            if (rbValorFixo.Checked)
            {
                // Obtém o valor fixo fornecido pelo usuário
                int valorFixo = (int)numOperacao.Value;

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = image1.GetPixel(x, y);

                        // Subtrai o valor fixo de cada componente de cor
                        int r = Math.Max(0, color1.R - valorFixo);
                        int g = Math.Max(0, color1.G - valorFixo);
                        int b = Math.Max(0, color1.B - valorFixo);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox3.Image = imagemResultado;
            }
            // Verifica se a operação é subtrair uma imagem da outra
            else if (rbPelaImagem.Checked)
            {
                Bitmap image2 = new Bitmap(pictureBox2.Image);

                // Verifica se a segunda imagem foi carregada
                if (image2 == null)
                {
                    MessageBox.Show("Por favor, selecione duas imagens para realizar a operação de subtração.");
                    return;
                }

                // Verifica se as imagens têm as mesmas dimensões
                if (image1.Width != image2.Width || image1.Height != image2.Height)
                {
                    MessageBox.Show("As imagens precisam ter o mesmo tamanho para serem subtraídas.");
                    return;
                }

                Bitmap imagemResultado = new Bitmap(image1.Width, image1.Height);

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color color1 = image1.GetPixel(x, y);
                        Color color2 = image2.GetPixel(x, y);

                        // Subtrai os valores das componentes de cor das duas imagens
                        int r = Math.Max(0, color1.R - color2.R);
                        int g = Math.Max(0, color1.G - color2.G);
                        int b = Math.Max(0, color1.B - color2.B);

                        imagemResultado.SetPixel(x, y, Color.FromArgb(r, g, b));
                    }
                }

                pictureBox3.Image = imagemResultado;
            }
            else
            {
                MessageBox.Show("Por favor, selecione o modo de operação.");
            }
        }

        public Bitmap FlipOut(Bitmap imgOriginal)
        {
            // Cria uma cópia da imagem original
            Bitmap imagemFlipOUT = (Bitmap)imgOriginal.Clone();

            // Inverte a imagem horizontalmente
            imagemFlipOUT.RotateFlip(RotateFlipType.Rotate180FlipNone);


            return imagemFlipOUT;
        }

        public Bitmap FlipIN(Bitmap imgOriginal)
        {
            // Cria uma cópia da imagem original
            Bitmap imagemFlipIN = (Bitmap)imgOriginal.Clone();

            // Inverte a imagem verticalmente
            imagemFlipIN.RotateFlip(RotateFlipType.RotateNoneFlipX);

            return imagemFlipIN;
        }


        private void btCortarImagem_Click(object sender, EventArgs e)
        {

            if (pictureBox1.Image == null)
            {
                MessageBox.Show("A imagem Não foi informada, por favor, insira a imagem.");
                return;
            }

            if (pictureBox1.Image != null)
            {
                
                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                // Pega os valores de x, y, larguraRecorte e alturaRecorte
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

            if (pictureBox1.Image == null || pictureBox2.Image == null)
            {
                MessageBox.Show("A imagem Não foi informada, por favor, insira a imagem no box IMAGEM 1.");
                return;
            }
            // Certifique-se de que há uma imagem carregada no PictureBox1
            if (pictureBox1.Image != null)
            {
                // Clone a imagem do PictureBox1
                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                // Crie uma função para converter a imagem em negativo
                Bitmap ImagemCinza = ConverteEscalaCinza(imgOriginal);

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
            if (pictureBox1.Image != null )

            {
                // Clone a imagem do PictureBox1
                Bitmap imgOriginal = new Bitmap(pictureBox1.Image);

                Bitmap ImagemBinaria = ConverteImagemBinaria(imgOriginal);

                // Exiba a imagem convertida no PictureBox3
                pictureBox3.Image = ImagemBinaria;
            }
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
        
    }
}
    

