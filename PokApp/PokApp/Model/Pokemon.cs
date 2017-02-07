using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokApp.Model
{
    public class Pokemon : RealmObject
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Descripcion { get; set; }

        public string Sprite { get; set; }
        
    }
}
