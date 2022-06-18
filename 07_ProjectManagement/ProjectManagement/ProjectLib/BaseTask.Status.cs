
namespace ProjectLib
{
    public abstract partial class BaseTask
    {
        /// <summary>
        /// Перечесление - статусы задач.
        /// </summary>
        public enum Status
        {
            Open,
            InProgress,
            Closed
        }
    }
}
