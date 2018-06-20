using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek2
{
    class Room: Location
    {
        private string decoration;

        public Room(string name ,string decoration)
                :base(name)
        {
            this.decoration = decoration;
        }

        public override string Description
        {
            get
            {
               string description = base.Description;
               description += "You see " + decoration + ".";
               return description;
            }
        }
    }
}
