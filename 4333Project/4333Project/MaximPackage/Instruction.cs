using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project.MaximPackage
{
    public class Instruction
    {
        public Action<object[]> action;
        public object[] args;
        public void Execute() {
            action(args);
        }
    }
}
