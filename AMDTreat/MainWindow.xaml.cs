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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using AMDTreat.ViewModels;
using AMDTreat.Views;
using AMDTreat.Models;
using MahApps.Metro.Controls.Dialogs;

namespace AMDTreat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Tile moduleTile { get; set; }
        public Window vfpWindow { get; set; }
        public Window limestoneBedWindow { get; set; }
        public Window manganeseRemovalBedWindow { get; set; }
        public Window wetlandWindow { get; set; }
        public Window bioReactorWindow { get; set; }
        public Window anoxicLimestoneDrainWindow { get; set; }

        public Window causticSodaWindow { get; set; }
        public Window limeSlurryWindow { get; set; }
        public Window dryLimeWindow { get; set; }
        public Window reactionTankWindow { get; set; }

        public Window pumpingWindow { get; set; }
        public Window conveyanceDitchWindow { get; set; }
        public Window samplingWindow { get; set; }
        public Window pondsWindow { get; set; }

        public Window clarifierWindow { get; set; }

        public Window siteDevelopmentWindow { get; set; }

        public Window chemicalCostWindow { get; set; }
        public TestWindow testWindow { get; set; }
        public ModuleItem moduleItem { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(DialogCoordinator.Instance);
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

        /// <summary>
        /// Event fired when user clicks on a tile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            moduleTile = (Tile)sender;
            moduleItem = (ModuleItem)moduleTile.DataContext;

            switch (moduleItem.ModuleType.Name)
            {
                case "VerticalFlowPondViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    vfpWindow = new VerticalFlowPondWindow();
                    vfpWindow.DataContext = moduleItem.ViewModel;
                    vfpWindow.Show();
                    break;
                case "LimestoneBedViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    limestoneBedWindow = new LimestoneBedWindow();
                    limestoneBedWindow.DataContext = moduleItem.ViewModel;
                    limestoneBedWindow.Show();
                    break;
                case "ManganeseRemovalBedViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    manganeseRemovalBedWindow = new ManganeseRemovalBedWindow();
                    manganeseRemovalBedWindow.DataContext = moduleItem.ViewModel;
                    manganeseRemovalBedWindow.Show();
                    break;
                case "WetlandViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    wetlandWindow = new WetlandWindow();
                    wetlandWindow.DataContext = moduleItem.ViewModel;
                    wetlandWindow.Show();
                    break;
                case "BioReactorViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    bioReactorWindow = new BioReactorWindow();
                    bioReactorWindow.DataContext = moduleItem.ViewModel;
                    bioReactorWindow.Show();
                    break;
                case "AnoxicLimestoneDrainViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    anoxicLimestoneDrainWindow = new AnoxicLimestoneDrainWindow();
                    anoxicLimestoneDrainWindow.DataContext = moduleItem.ViewModel;
                    anoxicLimestoneDrainWindow.Show();
                    break;
                case "CausticSodaViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    causticSodaWindow = new CausticSodaWindow();
                    causticSodaWindow.DataContext = moduleItem.ViewModel;
                    causticSodaWindow.Show();
                    break;
                case "LimeSlurryViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    limeSlurryWindow = new LimeSlurryWindow();
                    limeSlurryWindow.DataContext = moduleItem.ViewModel;
                    limeSlurryWindow.Show();
                    break;
                case "DryLimeViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    dryLimeWindow = new DryLimeWindow();
                    dryLimeWindow.DataContext = moduleItem.ViewModel;
                    dryLimeWindow.Show();
                    break;
                case "PumpingViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    pumpingWindow = new PumpingWindow();
                    pumpingWindow.DataContext = moduleItem.ViewModel;
                    pumpingWindow.Show();
                    break;
                case "ConveyanceDitchViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    conveyanceDitchWindow = new ConveyanceDitchWindow();
                    conveyanceDitchWindow.DataContext = moduleItem.ViewModel;
                    conveyanceDitchWindow.Show();
                    break;
                case "SamplingViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    samplingWindow = new SamplingWindow();
                    samplingWindow.DataContext = moduleItem.ViewModel;
                    samplingWindow.Show();
                    break;
                case "PondsViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    pondsWindow = new PondsWindow();
                    pondsWindow.DataContext = moduleItem.ViewModel;
                    pondsWindow.Show();
                    break;
                case "ClarifierViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    clarifierWindow = new ClarifierWindow();
                    clarifierWindow.DataContext = moduleItem.ViewModel;
                    clarifierWindow.Show();
                    break;
                case "ReactionTankViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    reactionTankWindow = new ReactionTankWindow();
                    reactionTankWindow.DataContext = moduleItem.ViewModel;
                    reactionTankWindow.Show();
                    break;
                case "SiteDevelopmentViewModel":
                    // Create a new window and assign the corresponding view module to the window's data context
                    siteDevelopmentWindow = new SiteDevelopmentWindow();
                    siteDevelopmentWindow.DataContext = moduleItem.ViewModel;
                    siteDevelopmentWindow.Show();
                    break;
                default:
                    break;
            }
        }

    }

}
