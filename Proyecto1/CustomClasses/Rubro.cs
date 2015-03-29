using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Linq.Mapping;
using System.Text;
using System.Data.Linq;

namespace Proyecto1.CustomClasses 
{
    [Table]
    public class Rubro : INotifyPropertyChanged, INotifyPropertyChanging
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType ="INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int RubroId{get;set;}
        [Column(CanBeNull=false)]
        public string RubroNombre{get;set;}        
        [Column(CanBeNull=false)]
        public string RubroTipo{get;set;}
        private int _valorEsperado;
        [Column(CanBeNull=false)]
        public int RubroValorEsperado 
        {
            get { return _valorEsperado; }
            set 
            { 
                NotifyPropertyChanging("RubroValorEsperado");
                _valorEsperado = value;
                NotifyPropertyChanged("RubroValorEsperado");
            }
        }

        private int _valorActual;
        [Column(CanBeNull = false)]
        public int RubroValorActual {
            get { return _valorActual; }            
            set
            {
                NotifyPropertyChanging("RubroValorActual");
                _valorActual = value;
                NotifyPropertyChanged("RubroValorActual");
            }
        }

        [Column(CanBeNull = false, Name = "Ciclo")]
        internal int _cicloId { get; set; }
        private EntityRef<Ciclo> _ciclo = new EntityRef<Ciclo>();
        [Association(Name = "FK_Rubro_Ciclo", IsForeignKey = true, Storage = "_ciclo", ThisKey = "_cicloId", OtherKey = "CicloId")]
        public Ciclo Ciclo
        {
            get { return _ciclo.Entity; }
            set
            {
                _ciclo.Entity = value;
                if (value != null)
                    _cicloId = value.CicloId;
            }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion

        /* Commented under the version 1.5
        private EntitySet<RubroPorCiclo> _rubrosPorCiclo = new EntitySet<RubroPorCiclo>();
        [Association(Name = "FK_RubrosPorCiclo_Rubros", Storage = "_rubrosPorCiclo", OtherKey = "rubroId", ThisKey = "RubroId")]
        private ICollection<RubroPorCiclo> RubrosPorCiclo 
        {
            get { return _rubrosPorCiclo; }
            set { _rubrosPorCiclo.Assign(value); }
        }
         * */




        
        
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
        

        
        

    }
}
