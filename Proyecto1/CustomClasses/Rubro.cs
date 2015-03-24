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
    public class Rubro 
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType ="INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int RubroId{get;set;}
        [Column(CanBeNull=false)]
        public string RubroNombre{get;set;}        
        [Column(CanBeNull=false)]
        public string RubroTipo{get;set;}
        [Column(CanBeNull=false)]
        public int RubroValor {get;set;}

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;
        
        
        

    }
}
