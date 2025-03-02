using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4333Project
{
    public static class ContextExecutor
    {
        public static void ExecuteInUsingContext (IDisposable disposable, Action contextProcedure) {
            using(disposable) {
               contextProcedure();
            }
        }
    }
}
