using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage {
    public class Role {
        public int id;
        public string name;
        public Role(string name) {
            this.name = name;
        }
        public Role(int id, string name) {
            this.id = id;
            this.name = name;
        }
    }
}
