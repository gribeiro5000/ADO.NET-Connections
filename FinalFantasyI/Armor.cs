using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalFantasyI
{
    public class Armor : Entity
    {
        public int Absorb { get; set; }
        public int Evade { get; set; }
        public ArmorType ArmorType { get; set; }
    }
}
