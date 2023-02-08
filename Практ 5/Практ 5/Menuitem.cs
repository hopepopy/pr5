using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практ_5
{
    internal class Menuitem
    {
        public string name;
        public Subitem? selected;
        public List<Subitem> all;

        public Menuitem(string name)
        {
            this.name = name;
            all = new List<Subitem>();
        }
    }
}
