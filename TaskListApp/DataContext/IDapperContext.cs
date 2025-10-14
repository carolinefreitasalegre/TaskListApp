using System.Data.Common;

namespace TaskListApp.DataContext
{
    public interface IDapperContext
    {
        DbConnection CreateConnection();
    }
}
