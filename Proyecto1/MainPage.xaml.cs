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
        private int porcentaje = 30;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            //Set the page DataContext property to the ViewModel
            this.DataContext = App.ViewModel;

            //Display current cicle on title
            PanoramaRubros.Header = "Ciclo "+App.ViewModel.selectedCiclo.CicloId+ "";

            // Nuevo Cambio// Selecciona el último index en listboxcicle
            
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
                //Valido que el rubro seleccionado no pertenezca a un ciclo ya cerrado
                if(rubroTemp.Ciclo.Cerrado == false)
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

      

        private void CicloSelected(object sender, GestureEventArgs e)
        {
            if (CiclosListBox.SelectedIndex > -1)
            {
                Ciclo cicloTemp = (Ciclo)CiclosListBox.SelectedItem;
                PanoramaRubros.Header = "Ciclo " + cicloTemp.CicloId;
                App.ViewModel.selectedCiclo = cicloTemp;
                App.ViewModel.LoadCollectionsFromDatabase();

                LoadStadistics(App.ViewModel.selectedCiclo);
            }
            
        }

        private void StadisticsLoaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Loaded");
            LoadStadistics(App.ViewModel.selectedCiclo);
            
        }

        private void LoadStadistics(Ciclo cicle)
        {
            int ingresosReales = 0; int ingresosEsperados = 0;
            int egresosReales = 0; int egresosEsperados = 0;
            int balanceGeneral=0;
            int balanceIngresos = 0; int balanceEgresos = 0;
            String balanceString = ""; String notif = "";
            
            
            foreach (Rubro rubro in App.ViewModel.Rubros)
            {
                if ((rubro.Ciclo.CicloId == cicle.CicloId))
                {
                    if (rubro.RubroTipo == "Ingreso")
                    {
                        ingresosReales += rubro.RubroValorActual;
                        ingresosEsperados += rubro.RubroValorEsperado;
                    }
                    else if (rubro.RubroTipo == "Egreso")
                    {
                        egresosReales += rubro.RubroValorActual;
                        egresosEsperados += rubro.RubroValorEsperado;
                    }

                    //Verificar con porcentaje de tolerancia
                    //Buscar textblock con el id de rubroactual en su tag y eliminarlo
                    
                    int dif = rubro.RubroValorActual - rubro.RubroValorEsperado;                    
                    if (dif > (100 + porcentaje) * rubro.RubroValorEsperado/100)
                    {                        
                        notif += rubro.RubroNombre + " excedió valor esperado: " + dif + "(" + (dif*100/rubro.RubroValorEsperado) + "%)\n";
                    }
                }
            }            
            //Balance General
            balanceGeneral = ingresosReales - egresosReales;
            balanceString += ingresosReales + " - " + egresosReales + " = " + balanceGeneral;
            tBBalanceGeneral.Text = balanceString;
            // Balange Ingresos
            balanceIngresos = ingresosReales - ingresosEsperados;
            balanceString = ingresosReales + " - " + ingresosEsperados + " = " + balanceIngresos;
            tBBalanceIngresos.Text = balanceString;
            //Balance Egresos
            balanceEgresos = egresosEsperados - egresosReales;
            balanceString = egresosEsperados +  " - " + egresosReales + " = " + balanceEgresos;
            tBBalanceEgresos.Text = balanceString;
            //Notif
            tBNotifications.Text = notif;
        }

        

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (tbPorcentaje != null)
            {
                porcentaje = (int) Math.Round(e.NewValue, 0);
                tbPorcentaje.Text = "Valor porcentual = " + porcentaje;

                //Recargar estadísticas de acuerdo al nuevo porcentaje
                LoadStadistics(App.ViewModel.selectedCiclo);
                
            }
        }

    }
}