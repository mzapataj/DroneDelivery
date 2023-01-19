using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneDelivery.Domain
{
    public class WeighableEntity
    {
        public string? Name { get; set; }
        public float Weight { get; set; }

        public override bool Equals(object? obj)
        {
            var obj2 = obj as WeighableEntity;

            if (this == null && obj2 == null)
            {
                return true;
            }

            if ((this == null && obj2 != null) || (this != null && obj2 == null))
            {
                return false;
            }

            return Name == obj2?.Name && Weight == obj2?.Weight;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}