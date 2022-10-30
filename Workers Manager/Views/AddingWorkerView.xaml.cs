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
using Workers_Manager.Models;
using Workers_Manager.ViewModels;

namespace Workers_Manager.Views
{
    /// <summary>
    /// Interaction logic for AddingWorkerView.xaml
    /// </summary>
    public partial class AddingWorkerView : Window
    {
        public AddingWorkerView(MainViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
