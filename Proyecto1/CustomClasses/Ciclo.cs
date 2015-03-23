using System;
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

    }

    // Esta tabla contiene la relación entre Ciclo y Rubro
    [Table]
    public class RubroPorCiclo
    {
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
    }
}