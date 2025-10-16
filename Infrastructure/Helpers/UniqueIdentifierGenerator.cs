using System.Diagnostics;

namespace Infrastructure.Helpers;

public class UniqueIdentifierGenerator
{
    public static Guid Generate()
    {
        try
        {
            return Guid.NewGuid();
        }
        catch(Exception ex)
        {
            Debug.WriteLine(ex);
            return Guid.Empty;
        }
    }
}
