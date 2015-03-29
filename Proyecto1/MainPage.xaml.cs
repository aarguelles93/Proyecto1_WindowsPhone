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
using System.Windows.Navigation;
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

            //Display current cicle on title
            PanoramaRubros.Header = "Ciclo "+App.ViewModel.Ciclos.Count + "";
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

        // When back to this page, refresh the collections from DB
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Save changes to the database.
            App.ViewModel.SaveChangesToDB();

            App.ViewModel.LoadCollectionsFromDatabase();

            base.OnNavigatedTo(e);
            
        }

        private void RubroSelected(object sender, GestureEventArgs e)
        {
            //Encuentra el Item Seleccionado
            if (RubrosListBox.SelectedIndex != -1)
            {
                Rubro rubroTemp = (Rubro)RubrosListBox.SelectedItem;
                NavigationService.Navigate(new Uri("/EditRubro.xaml?parameter="+rubroTemp.RubroId, UriKind.Relative));
            }
        }
       
        //=============
        //Start new Cicle
        private void newCicleAppBarrButton_Click(object sender, EventArgs e)
        {
            App.ViewModel.StartNewCicle();
            //Display current cicle on title
            PanoramaRubros.Header = "Ciclo " + App.ViewModel.Ciclos.Count + "";
        }


    }
}