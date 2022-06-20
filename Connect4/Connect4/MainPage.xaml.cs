using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Connect4
{
    public partial class MainPage : ContentPage
    {
        // global variable 
        bool _IsRTurn;

        public MainPage()
        {
            InitializeComponent();
            InitialiseBoard();
            _IsRTurn = true;
            LblStatus.Text = "Red to move!";
        }

        private void InitialiseBoard()
        {
            // variables
            Label label;
            int iRow, iCol;

            // tap gesture code
            #region tap gesture
            TapGestureRecognizer tapGestureRecognizer;
            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            tapGestureRecognizer.NumberOfTapsRequired = 1;
            #endregion

            //labels for grids
            for (iRow = 0; iRow < 6; iRow++)
            {
                for(iCol = 0; iCol <7; iCol++)
                {
                    #region setting up label on grid
                    label = new Label();
                    label.BackgroundColor = Color.Gray;
                    label.Text = iRow.ToString() + "," + iCol.ToString();
                    label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
                    label.SetValue(Grid.RowProperty, iRow);
                    label.SetValue(Grid.ColumnProperty, iCol);
                    label.HorizontalTextAlignment = TextAlignment.Center;
                    label.VerticalTextAlignment = TextAlignment.Center;
                    label.GestureRecognizers.Add(tapGestureRecognizer);

                    // to find the label in tapped event handler
                    label.StyleId = "Lb1R" + iRow.ToString() +
                       "C" + iCol.ToString();

                    GrdGame.Children.Add(label);
                    #endregion
                }// iCol
            }// iRow
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // check illegal move
            Label l = (Label)sender;
            
            if (l.Text == "R" || l.Text == "Y")
            {
                LblStatus.Text = "illegal move";
                return;
            }

            // decide whose turn it is
           
            switch (_IsRTurn) 
            {
                case true:
                    {
                        l.Text = "R";
                        l.BackgroundColor = Color.Red;
                        _IsRTurn = false;
                        LblStatus.Text = "Yellow to move";
                        break;
                    }
                case false:
                    {
                        l.Text = "Y";
                        l.BackgroundColor = Color.Yellow;
                        _IsRTurn = true;
                        LblStatus.Text = "Red to move";
                        break;
                    }

            }
        }

        private void BtnReset_Clicked(object sender, EventArgs e)
        {
            // resetting labels
            foreach (var item in GrdGame.Children)
            {
                ((Label)item).Text = "";
                ((Label)item).BackgroundColor = Color.Gray;
            }

            // set bool to say it is x turn
            _IsRTurn = true;

            // update status
            LblStatus.Text = "Red to move!";
        }
    }
}
