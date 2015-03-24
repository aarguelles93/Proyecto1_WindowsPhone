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
            tBValorEsperado.Text = currentRubro.RubroValor.ToString();
            tBValorActual.Text = currentRubro.RubroValor.ToString(); //EDIT

            slider.Maximum = currentRubro.RubroValor * 10;
            slider.Minimum = slider.Maximum * -1;
            slider.SmallChange = 100;
            slider.LargeChange = 100;
            slider.Value = currentRubro.RubroValor;
        }


        private void Sliding(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double cambio = 0;

            cambio = Math.Round ( e.NewValue, 0);            
            tBValorActual.Text = cambio + "";
        }


        private void saveChangesInRubro(object sender, EventArgs e)
        {
            foreach (RubroPorCiclo rpc in App.ViewModel.RubrosPorCiclos)
            {
                if (rpc.Rubro.RubroId == currentRubro.RubroId)
                {
                    rpc.ValorActual = int.Parse(tBValorActual.Text);
                    MessageBox.Show("El valor actual es = " + rpc.ValorActual);
                }
            }
            //returnToMainPage();
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