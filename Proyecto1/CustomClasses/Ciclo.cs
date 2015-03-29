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
}