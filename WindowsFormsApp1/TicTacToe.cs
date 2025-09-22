using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TicTacToe : Form
    {
        int panjang=3;
        int lebar=3;
        bool turn = true; // true = X turn; false = Y turn
        public TicTacToe(int panjang, int lebar)
        {
            InitializeComponent();
            this.panjang = panjang;
            this.lebar = lebar;
        }
        private void form_closing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void TicTacToe_Load(object sender, EventArgs e)
        {
            // Define the size of each button
            int buttonSize = 50;
            // Define the gap between buttons
            int padding = 5;

            for (int i = 0; i < panjang; i++) // Controls the column (X position)
            {
                for (int j = 0; j < lebar; j++) // Controls the row (Y position)
                {
                    // 1. Create a new button instance
                    Button newButton = new Button();

                    // 2. Set the button's properties
                    newButton.Name = $"btn_{i}_{j}";
                    newButton.Text = ""; // Initially empty
                    newButton.Size = new Size(buttonSize, buttonSize);

                    // Calculate the position with padding
                    int xPosition = i * (buttonSize + padding);
                    int yPosition = j * (buttonSize + padding);
                    newButton.Location = new Point(xPosition, yPosition);

                    // 3. Add the button to the form's controls
                    newButton.Click += new EventHandler(DynamicButton_Click);
                    this.Controls.Add(newButton);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DynamicButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            // --- 1. Get the coordinates of the clicked button ---
            string[] coordinates = clickedButton.Name.Split('_');
            int col = int.Parse(coordinates[1]);
            int row = int.Parse(coordinates[2]);

            // --- 2. Set the button text and get the current player's symbol ---
            string currentPlayerSymbol;
            if (turn)
            {
                clickedButton.Text = "X";
                currentPlayerSymbol = "X";
            }
            else
            {
                clickedButton.Text = "O";
                currentPlayerSymbol = "O";
            }
            clickedButton.Enabled = false;

            // --- 3. Call your new win-checking function! ---
            if (CheckWinnerAround(col, row, currentPlayerSymbol))
            {
                MessageBox.Show($"Player {currentPlayerSymbol} wins!", "Game Over!");
                
                this.Close(); // Close the game form
                return;       // Exit the function early since the game is over
            }

            // (Add logic for a draw here if you want)

            // --- 4. Switch turns ---
            turn = !turn;
        }

        // Helper function to find a button by its coordinates and get its text
        private string GetButtonText(int col, int row)
        {
            // If coordinates are outside the board, it's not a match.
            if (col < 0 || col >= panjang || row < 0 || row >= lebar)
            {
                return ""; // Return empty string, as it will never equal "X" or "O"
            }

            // Find the control by its generated name
            Button btn = this.Controls.Find($"btn_{col}_{row}", true).FirstOrDefault() as Button;

            if (btn != null)
            {
                return btn.Text;
            }



            return "";
        }

        #region Winning Logic Functions

        /// <summary>
        /// This is the main function. It checks for 3-in-a-row in all 4 directions
        /// around the button that was just clicked.
        /// </summary>
        /// <param name="col">The column of the button just clicked.</param>
        /// <param name="row">The row of the button just clicked.</param>
        /// <param name="playerSymbol">The symbol to check for ("X" or "O").</param>
        /// <returns>True if a winner is found, otherwise false.</returns>
        private bool CheckWinnerAround(int col, int row, string playerSymbol)
        {
            // Check in all 4 possible directions for a line of 3 or more.
            if (CheckLine(col, row, 1, 0, playerSymbol) >= 3 || // Horizontal (→)
                CheckLine(col, row, 0, 1, playerSymbol) >= 3 || // Vertical (↓)
                CheckLine(col, row, 1, 1, playerSymbol) >= 3 || // Diagonal (\)
                CheckLine(col, row, 1, -1, playerSymbol) >= 3)   // Anti-Diagonal (/)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// A reusable helper that counts consecutive symbols in a single direction.
        /// It checks two spots forward and two spots backward from the clicked button.
        /// </summary>
        /// <returns>The number of consecutive matching symbols found.</returns>
        private int CheckLine(int col, int row, int dx, int dy, string playerSymbol)
        {
            int consecutiveCount = 0;
            // Loop from 2 units "behind" the click to 2 units "in front".
            // This covers the 5-tile line you imagined.
            for (int i = -2; i <= 2; i++)
            {
                // Get the text of the button at the current spot in the line
                string text = GetButtonTextAt(col + i * dx, row + i * dy);

                if (text == playerSymbol)
                {
                    consecutiveCount++;
                }
                else
                {
                    // If the chain is broken, reset the counter.
                    consecutiveCount = 0;
                }

                // If we've already found 3, we don't need to keep checking this line.
                if (consecutiveCount >= 3)
                {
                    break;
                }
            }
            return consecutiveCount;
        }


        /// <summary>
        /// A safe way to get the text from a button at a specific (col, row).
        /// If the coordinates are off the board, it returns an empty string.
        /// </summary>
        private string GetButtonTextAt(int col, int row)
        {
            // Safety Check: If the coordinates are outside the board, it's not a match.
            if (col < 0 || col >= panjang || row < 0 || row >= lebar)
            {
                return ""; // Return an empty string which will never equal "X" or "O".
            }

            // Find the button control by its unique name (e.g., "btn_2_3").
            Button btn = this.Controls.Find($"btn_{col}_{row}", true).FirstOrDefault() as Button;

            return (btn != null) ? btn.Text : "";
        }

        #endregion





        private void TicTacToe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
