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
    public partial class NuevoRubro : PhoneApplicationPage
    {

        private string tipoRubro;
                
        public NuevoRubro()
        {
            InitializeComponent();

            

            //Set the page DataContext property to the ViewModel
            this.DataContext = App.ViewModel;
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            tipoRubro = rb.Content.ToString();
            
        }



        private void appBarCreateButton_Click(object sender, EventArgs e)
        {
            // Validaciones en las entradas
            if (nombreRubro.Text.Length > 0 )
            {
                //Crear nuevo Rubro
                Rubro nuevoRubro = new Rubro
                {
                    RubroNombre = nombreRubro.Text,
                    RubroTipo = tipoRubro,
                    RubroValorEsperado = int.Parse(valorRubro.Text.ToString()),
                    RubroValorActual = int.Parse(valorRubro.Text.ToString()),
                    Ciclo = App.ViewModel.Ciclos.Last()
                    
                    
                };
                //Añadir al ViewModel
                App.ViewModel.AddRubro(nuevoRubro);
                //Regresar al main page
                returnToMainPage();
            }
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