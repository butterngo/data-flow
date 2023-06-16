namespace DataEngine.Abstraction.Models
{
    public class StateMachineModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IRowDataModel Data { get; set; }
    }
}
