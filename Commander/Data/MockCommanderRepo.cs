using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command> {
                new Command { Id = 0, HowTo = "Boil", Line = "Water", Platform = "Kettle" },
                new Command { Id = 1, HowTo = "Boil2", Line = "Water2", Platform = "Kettle2" },
                new Command { Id = 2, HowTo = "Boil3", Line = "Water3", Platform = "Kettle3" }
            };
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil", Line = "Water", Platform = "Kettle"};
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
