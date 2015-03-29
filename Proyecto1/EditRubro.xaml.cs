using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using Proyecto1.CustomClasses;

namespace Proyecto1
{
    public partial class EditRubro : PhoneApplicationPage
    {
        Rubro currentRubro;

        public EditRubro()
        {
            InitializeComponent();
            
           

        }

        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            currentRubro = null;
            //Obtener el rubro pasado por parámetros
            String parameter;
            if (NavigationContext.QueryString.TryGetValue("parameter", out parameter))
            {
                //Busco el rubro por id
                foreach (Rubro temp in App.ViewModel.Rubros)
                {
                    if (temp.RubroId == int.Parse(parameter))
                    {
                        currentRubro = temp;
                    }
                }
            }

            // Adecuar elementos visuales de acuerdo al Rubro
            tBValorEsperado.Text = currentRubro.RubroValorEsperado.ToString();
            tBValorActual.Text = currentRubro.RubroValorActual.ToString();

            slider.Maximum = currentRubro.RubroValorEsperado * 10;
            slider.Minimum = 0;
            slider.SmallChange = 100;
            slider.LargeChange = 100;
            
            
            slider.Value = currentRubro.RubroValorEsperado;
        }

        
        private void Sliding(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if ((e.NewValue != e.OldValue)&&(e.OldValue != 0))
            {
                double cambio = 0;

                cambio = Math.Round(e.NewValue, 0);
                tBValorActual.Text = cambio + "";
            }
            
        }
         


        private void saveChangesInRubro(object sender, EventArgs e)
        {
            foreach (Rubro rubro in App.ViewModel.Rubros)
            {
                if ((rubro.RubroId == currentRubro.RubroId) && (rubro.Ciclo.Cerrado == false))
                {
                    rubro.RubroValorActual = int.Parse(tBValorActual.Text);
                    
                }
            }
                       
            
            returnToMainPage();
        }

        private void appBarCancelButton_Click(object sender, EventArgs e)
        {
            //Regresar a la página principal
            returnToMainPage();
        }
        private void returnToMainPage()
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}