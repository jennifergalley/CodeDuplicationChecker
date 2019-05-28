using Models;

namespace Interfaces
{
    public interface ICodeComparer
    {
        double Compare(Method method1, Method method2);
    }
}