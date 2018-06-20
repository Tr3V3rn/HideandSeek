using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek2
{
    class OutsideWithHidingPlace:Outside,IHidingPlace
    {
        public OutsideWithHidingPlace(string name, bool hot, string hidingPlace)
            :base(name,hot)
        {
            HidingPlaceName= hidingPlace;
        }

        public string HidingPlaceName { get; private set; }

        public override string Description
        {
            get
            {
                string description =  base.Description;
                description += " Someone could hide " + HidingPlaceName + ".";
                return description;
            }
        }

    }
}
