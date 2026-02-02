using RadioAmateurHandbook.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioAmateurHandbook.App
{
    internal class Application
    {
        private readonly ApplicationContext _ctx;
        private readonly StartupFlow _startup;
        private readonly MainLoop _mainLoop;

        public Application()
        {
            _ctx = new ApplicationContext();
            _startup = new StartupFlow(_ctx);
            _mainLoop = new MainLoop(_ctx);
        }

        public void Run()
        {
            _startup.Run();
            _mainLoop.Run();
        }
    }
}
