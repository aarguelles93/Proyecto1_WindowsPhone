using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

//directive 
using Proyecto1.CustomClasses;

namespace Proyecto1.ViewModel
{
    public class RubroViewModel
    {
        private RubroDataContext rubroDB;

        public RubroViewModel(string rubroDBConnectionString)
        {
            rubroDB = new RubroDataContext(rubroDBConnectionString);
        }

        public void SaveChangesToDB()
        {
            rubroDB.SubmitChanges();
        }

        //=====
        public ObservableCollection<Rubro> Rubros { get; set; }

        public ObservableCollection<Ciclo> Ciclos { get; set; }
        public ObservableCollection<RubroPorCiclo> RubrosPorCiclos { get; set; }

        //====
        public void LoadCollectionsFromDatabase()
        {
            //Specify the query & Load the data from DB!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            var rubrosInDB = from Rubro rubro in rubroDB.Rubros
                             select rubro;

            Rubros = new ObservableCollection<Rubro>(rubrosInDB);

            //new
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


            var rubrosPorCiclosInDB = from RubroPorCiclo rpc in rubroDB.RubrosPorCiclos
                                      select rpc;
            RubrosPorCiclos = new ObservableCollection<RubroPorCiclo>(rubrosPorCiclosInDB);
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
            Ciclos.ElementAt(Ciclos.Count - 1).Cerrado = true;
            Ciclo nuevoCiclo = new Ciclo { Cerrado = false };
            //Add to db
            rubroDB.Ciclos.InsertOnSubmit(nuevoCiclo);
            rubroDB.SubmitChanges();
            //Add to collection
            Ciclos.Add(nuevoCiclo);
        }
    }
}