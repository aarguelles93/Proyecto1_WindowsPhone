using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

using Proyecto1.CustomClasses;

namespace Proyecto1
{
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //Set the page DataContext property to the ViewModel
            this.DataContext = App.ViewModel;
        }      


        //=====================
        //Executed when entering the NuevoRubro Window
        private void newRubroAppBarButton_Click(Object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NuevoRubro.xaml", UriKind.Relative));
        }
        // Executed when leaving the mainPage
        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();
        }

        private void RubroSelected(object sender, GestureEventArgs e)
        {
            //Encuentra el Item Seleccionado
            if (RubrosListBox.SelectedIndex != -1)
            {
                Rubro rubroTemp = (Rubro)RubrosListBox.SelectedItem;
                MessageBox.Show(rubroTemp.RubroNombre);
            }
        }


    }
}