using System;
using System.Drawing;
using System.Windows.Forms;

namespace flappKus
{
    public partial class Form1 : Form
    {
        // Oyun değişkenleri
        int pipeSpeed = 8; // Boruların hareket hızı
        int gravity = 5;   // Yer çekimi kuvveti
        int score = 0;     // Skor

        Rectangle flappyBird = new Rectangle(50, 200, 40, 40);  // Kuş
        Rectangle pipeTop = new Rectangle(300, 0, 50, 150);     // Üst boru
        Rectangle pipeBottom = new Rectangle(300, 300, 50, 200);// Alt boru
        Rectangle ground = new Rectangle(0, 400, 800, 50);      // Zemin

        public Form1()
        {
            InitializeComponent();
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Kuş yer çekimi etkisiyle düşer
            flappyBird.Y += gravity;

            // Borular sola doğru hareket eder
            pipeBottom.X -= pipeSpeed;
            pipeTop.X -= pipeSpeed;

            // Skor güncellemesi
            scoreText.Text = "Score: " + score;

            // Borular ekran dışına çıkarsa yeniden konumlandır
            if (pipeBottom.X < -50)
            {
                pipeBottom.X = 800;
                score++;
            }
            if (pipeTop.X < -50)
            {
                pipeTop.X = 800;
                score++;
            }

            // Kuşun borulara ya da yere çarpmasını kontrol et
            if (flappyBird.IntersectsWith(pipeBottom) ||
                flappyBird.IntersectsWith(pipeTop) ||
                flappyBird.IntersectsWith(ground) || flappyBird.Y < 0)
            {
                endGame();
            }

            // Oyun zorlaştıkça boruların hızını arttır
            if (score > 5)
            {
                pipeSpeed = 12;
            }

            // Oyun ekranını yeniden çiz
            Invalidate();  // Formun yeniden çizilmesini tetikler
        }

        private void gameKeyDown(object sender, KeyEventArgs e)
        {
            // Boşluk tuşuna basıldığında kuş yukarı hareket eder
            if (e.KeyCode == Keys.Space)
            {
                gravity = -5;
            }
        }

        private void gameKeyUp(object sender, KeyEventArgs e)
        {
            // Boşluk tuşu bırakıldığında yer çekimi etkisiyle kuş aşağıya düşer
            if (e.KeyCode == Keys.Space)
            {
                gravity = 5;
            }
        }

        private void endGame()
        {
            // Oyun bittiğinde timer durdurulur
            gameTimer.Stop();
            scoreText.Text += "  Game over!";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Kuşu, boruları ve zemini çiz
            Graphics g = e.Graphics;

            // Kuşu çiz (Sarı renkli bir kare)
            g.FillRectangle(Brushes.Yellow, flappyBird);

            // Üst boruyu çiz (Yeşil renkli bir dikdörtgen)
            g.FillRectangle(Brushes.Green, pipeTop);

            // Alt boruyu çiz (Yeşil renkli bir dikdörtgen)
            g.FillRectangle(Brushes.Green, pipeBottom);

            // Zemini çiz (Kahverengi bir dikdörtgen)
            g.FillRectangle(Brushes.Brown, ground);
        }
    }
}
