using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HideAndSeek2
{
    class RoomWithHidingPlace: Room,IHidingPlace
    {
        public RoomWithHidingPlace(string name, string decoration, string hidingPlace)
            :base(name, decoration)
        {
            HidingPlaceName = hidingPlace;
        }

        public string HidingPlaceName { get; private set; }

        public override string Description
        {
            get
            {
                string description = base.Description;
                description += " Someone could hide " + HidingPlaceName + ".";
                return description;
            }
        }

    }
}
