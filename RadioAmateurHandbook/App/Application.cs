using RadioAmateurHandbook.Data;
using RadioAmateurHandbook.Flow;

namespace RadioAmateurHandbook.App
{
    internal class Application
    {
        private readonly ApplicationContext _ctx;
        private readonly StartupFlow _startup;
        private readonly MainLoop _mainLoop;

        public Application()
        {
            var repo = new RadioRepository();
            var persistence = new RadioPersistenceService(repo);

            _ctx = new ApplicationContext();
            _startup = new StartupFlow(_ctx, persistence);
            _mainLoop = new MainLoop(_ctx, persistence);
        }

        public void Run()
        {
            _startup.Run();
            _mainLoop.Run();
        }
    }
}
