using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AMDTreat.Views
{
    /// <summary>
    /// Interaction logic for VfpWindow.xaml
    /// </summary>
    public partial class ClarifierWindow
    {
        public ClarifierWindow()
        {
            InitializeComponent();
        }

        // Event handler that handles when enter key is pressed in a text box
        // Reference: http://stackoverflow.com/questions/563195/bind-textbox-on-enter-key-press
        //    Answer by Ben
        private void TextBox_KeyEnterUpdate(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }
            }
        }

    }
}
