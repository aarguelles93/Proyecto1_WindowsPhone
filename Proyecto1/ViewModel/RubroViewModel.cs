using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

//directive 
using Proyecto1.CustomClasses;

namespace Proyecto1.ViewModel
{
    public class RubroViewModel :INotifyPropertyChanged
    {
        private RubroDataContext rubroDB;
        public Ciclo ultimoCiclo;
        public Ciclo selectedCiclo;

        public RubroViewModel(string rubroDBConnectionString)
        {
            rubroDB = new RubroDataContext(rubroDBConnectionString);
        }

        public void SaveChangesToDB()
        {
            rubroDB.SubmitChanges();
        }

        //=====
        private ObservableCollection<Rubro> _rubros;
        public ObservableCollection<Rubro> Rubros
        {
            get { return _rubros; }
            set
            {
                _rubros = value;
                NotifyPropertyChanged("Rubros");
            }
        }
        private ObservableCollection<Ciclo> _ciclos;
        public ObservableCollection<Ciclo> Ciclos
        {
            get {return _ciclos;}
            set
            {
                _ciclos = value;
                NotifyPropertyChanged("Ciclos");
            }
        }
        /*Commented under version 1.5
        ObservableCollection<RubroPorCiclo> RubrosPorCiclos { get; set; }
        */
        //====
        public void LoadCollectionsFromDatabase()
        {
            //Specify the query & Load the data from DB!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var ciclosInDB = from Ciclo ciclo in rubroDB.Ciclos
                             select ciclo;

            Ciclos = new ObservableCollection<Ciclo>(ciclosInDB);
            
            if (Ciclos.Count < 1)
            {
                Ciclo nuevoCiclo = new Ciclo() { Cerrado = false };
                rubroDB.Ciclos.InsertOnSubmit(nuevoCiclo);
                rubroDB.SubmitChanges();
                Ciclos.Add(nuevoCiclo);               
            }

            ultimoCiclo = Ciclos.Last();
            if (selectedCiclo == null)
            {
                selectedCiclo = ultimoCiclo;
            }

            var rubrosInDB = from Rubro rubro in rubroDB.Rubros where (rubro.Ciclo.CicloId == selectedCiclo.CicloId)
                             select rubro;
                             

            Rubros = new ObservableCollection<Rubro>(rubrosInDB);           
            

            /*
            var rubrosPorCiclosInDB = from RubroPorCiclo rpc in rubroDB.RubrosPorCiclos
                                      select rpc;
            RubrosPorCiclos = new ObservableCollection<RubroPorCiclo>(rubrosPorCiclosInDB);
             */
            //
        }

        //===
        public void AddRubro(Rubro nuevoRubro)
        {
            //Add to the DB
            rubroDB.Rubros.InsertOnSubmit(nuevoRubro);
            rubroDB.SubmitChanges();
            //Add to the collection
            Rubros.Add(nuevoRubro);
            
        }


        //=== NEW CICLE        
        public void StartNewCicle()
        {
            //Encuentro el ciclo actual y lo cierro            
            ultimoCiclo.Cerrado = true;
            Ciclo nuevoCiclo = new Ciclo { Cerrado = false };
            //Add to db
            rubroDB.Ciclos.InsertOnSubmit(nuevoCiclo);
            rubroDB.SubmitChanges();
            //Add to collection
            Ciclos.Add(nuevoCiclo);

            //Llamada a NewCiclce_DuplicateRubros
            NewCicle_DuplicateRubros(ultimoCiclo, Ciclos.Last());
            //Actualizo variable cicloActual
            ultimoCiclo = nuevoCiclo;
            
           
            //Vuelvo a consultar la bd
            LoadCollectionsFromDatabase();
        }

        //Duplico los rubros existentes en el ciclo actual y los copio para el nuevo ciclo
        public void NewCicle_DuplicateRubros(Ciclo currentCicle, Ciclo newCicle)
        {
            int tam = Rubros.Count;
            for (int i = 0; i < tam; i++)
            {
                Rubro rubro = Rubros.ElementAt(i);
                if (rubro.Ciclo.CicloId == currentCicle.CicloId)
                {
                    Rubro copiaTemp = new Rubro
                    {
                        RubroNombre = rubro.RubroNombre,
                        RubroTipo = rubro.RubroTipo,
                        RubroValorEsperado = rubro.RubroValorEsperado,
                        RubroValorActual = rubro.RubroValorEsperado,
                        Ciclo = newCicle //!!
                    };
                    AddRubro(copiaTemp);
                }
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}