using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TitleEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitlePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TitleRed.png", UriKind.Relative)),
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TitleEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Title-Z.png", UriKind.Relative)),
        };

        private readonly Image[,] imageControls;
        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            MessageBox.Show("Main Window Initialized");

            try
            {
                this.Show(); // Ensure the window shows
                imageControls = SetupGameCanvas(gameState.GameGrid);
                Draw(gameState);  // Draw initial state of the game
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    Canvas.SetLeft(imageControl, c * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r, c] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int r = 0; r < grid.Rows; ++r)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    int id = grid[r, c];
                    imageControls[r, c].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right:
                    gameState.MoveBlockRight();
                    break;
                case Key.Down:
                    gameState.MoveBlockDown();
                    break;
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            Draw(gameState);
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            // Reset game logic and UI
        }
    }
}
