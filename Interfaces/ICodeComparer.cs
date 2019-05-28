using Microsoft.CodeAnalysis;

namespace Interfaces
{
    public interface ICodeComparer
    {
        double Compare(SyntaxNode methodNode1, SyntaxNode methodNode2);
    }
}