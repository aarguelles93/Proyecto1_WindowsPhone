using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Proyecto1.CustomClasses
{
    [Table]
    public class Ciclo
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int CicloId { get; set; }
        [Column(CanBeNull = false)]
        public Boolean Cerrado { get; set; }
        

        /* Commented under the version 1.5
        private EntitySet<RubroPorCiclo> _rubrosPorCiclo = new EntitySet<RubroPorCiclo>();
        [Association(Name = "FK_RubrosPorCiclo_Ciclos", Storage = "_rubrosPorCiclo", OtherKey = "cicloId", ThisKey = "CicloId")]
        private ICollection<RubroPorCiclo> RubrosPorCiclo
        {
            get { return _rubrosPorCiclo; }
            set { _rubrosPorCiclo.Assign(value); }
        }
        */

    }

    /* Commented under the version 1.5
    // Esta tabla contiene la relación entre Ciclo y Rubro
    [Table]
    class RubroPorCiclo
    {
        [Column(IsPrimaryKey = true, Name = "Rubro")]
        private int rubroId;
        private EntityRef<Rubro> _rubro = new EntityRef<Rubro>();
        [Association(Name = "FK_RubrosPorCiclo_Rubros", IsForeignKey = true, Storage = "_rubro", ThisKey = "rubroId", OtherKey = "RubroId")]
        public Rubro Rubro
        {
            get { return _rubro.Entity; }
            set { _rubro.Entity = value; 
                    //add this Rubro to NuevoRubro
            }
        }

        [Column(IsPrimaryKey = true, Name = "Ciclo")]
        private int cicloId;
        private EntityRef<Ciclo> _ciclo = new EntityRef<Ciclo>();
        [Association(Name = "FK_RubrosPorCiclo_Ciclos", IsForeignKey = true, Storage = "_ciclo", ThisKey = "cicloId", OtherKey = "CicloId")]
        public Ciclo Ciclo
        {
            get { return _ciclo.Entity; }
            set { _ciclo.Entity = value; }
        }

        [Column(CanBeNull = false)]
        public int ValorActual { get; set; }

        /*
       // Llave foranea a rubro
       [Column]
       internal int _rubroId;

       private EntityRef<Rubro> _rubro;

       [Association(Storage = "_rubro", ThisKey = "_rubroId", OtherKey = "RubroId", IsForeignKey = true)]
       public Rubro rubro
       {
           get { return _rubro.Entity; }
           set
           {
               _rubro.Entity = value;

               if (value != null)
               {
                   _rubroId = value.RubroId;
               }
           }
       }

       // Llave foranea a Ciclo
       [Column]
       internal int _cicloId;

       private EntityRef<Ciclo> _ciclo;

       [Association(Storage = "_ciclo", ThisKey = "_cicloId", OtherKey = "CicloId", IsForeignKey = true)]
       public Ciclo ciclo
       {
           get { return _ciclo.Entity; }
           set
           {
               _ciclo.Entity = value;

               if (value != null)
               {
                   _cicloId = value.CicloId;
               }
           }
       }
       

    }*/
}