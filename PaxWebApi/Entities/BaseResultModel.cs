
namespace Entities
{
    public class BaseResultModel
    {
        public string InfoLog { get; set; }
        public bool OperationResult { get; set; }
        public ErrorsEnum? ResultMessage { get; set; }
    }
}
