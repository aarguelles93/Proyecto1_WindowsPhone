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

        //====
        public void LoadCollectionsFromDatabase()
        {
            //Specify the query
            var rubrosInDB = from Rubro todo in rubroDB.Rubros
                             select todo;

            Rubros = new ObservableCollection<Rubro>(rubrosInDB);

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
    }
}