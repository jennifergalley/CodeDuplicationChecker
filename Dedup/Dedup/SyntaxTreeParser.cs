using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Dedup
{
    public static class SyntaxTreeParser
    {
        public static List<VariableName> GetVariablesCount(SyntaxNode methodNode)
        {
            var variablesCount = new List<VariableName>();
            Queue<SyntaxNode> queue = new Queue<SyntaxNode>();
            queue.Enqueue(methodNode);

            //BFS
            while (queue.Count > 0)
            {
                var temp = new Queue<SyntaxNode>();

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    foreach (var childNode in current.ChildNodes())
                    {
                        temp.Enqueue(childNode);
                    }

                    var kind = current.Kind();

                    if (kind == SyntaxKind.VariableDeclarator || kind == SyntaxKind.Parameter)
                    {
                        PopulateDeclaration(current, variablesCount);
                    }

                    if (kind == SyntaxKind.VariableDeclarator)
                    {
                        var tokenNode = current.GetFirstToken();
                        var item = FindVariableName(variablesCount.Where(f => f.Name == tokenNode.Text).ToList(), current);
                        if (item != null)
                        {
                            if (IsDefinedByOperations(current, SyntaxKind.AddExpression, SyntaxKind.SubtractExpression))
                            {
                                item.DefinedByAddOrSubtractOp += 1;
                            }
                            if (IsDefinedByOperations(current, SyntaxKind.MultiplyExpression, SyntaxKind.DivideExpression))
                            {
                                item.DefinedByMultiplyOrDivideOp += 1;
                            }
                            if (IsDefinedByOperations(current, SyntaxKind.StringLiteralExpression))
                            {
                                item.DefinedByStringLiterals += 1;
                            }
                            if (IsDefinedByOperations(current, SyntaxKind.CharacterLiteralExpression))
                            {
                                item.DefinedByCharacterLiteralExpression += 1;
                            }

                            if (IsDefinedByOperations(current, SyntaxKind.NumericLiteralExpression))
                            {
                                item.DefinedByNumericLiteralExpression += 1;
                            }

                            if (IsDefinedByOperations(current, SyntaxKind.TrueLiteralExpression, SyntaxKind.FalseLiteralExpression))
                            {
                                item.DefinedByBooleanLiteralExpression += 1;
                            }

                            if (IsDefinedByOperations(current, SyntaxKind.NullLiteralExpression))
                            {
                                item.DefinedByNullLiteralExpression += 1;
                            }

                            if (IsDefinedByOperationsWithKind(current, IsLiteralExpressionKind))
                            {
                                item.DefinedByExpressionWithConstant += 1;
                            }

                            if (IsDefinedByOperationsWithKind(current, IsIdentifierNameKind))
                            {
                                item.DefinedByOtherVariable += 1;
                            }
                        }
                    }

                    if (kind == SyntaxKind.IdentifierName)
                    {
                        var currentVariable = GetSingleVariable(variablesCount, current);
                        if (currentVariable != null)
                        {
                            if (IsAssignedByOperationsWithKind(current, IsIdentifierNameKind))
                            {
                                currentVariable.AssignedByOtherVariable += 1;
                            }

                            if (IsAssignedByOperationsWithKind(current, IsLiteralExpressionKind))
                            {
                                currentVariable.AssignedByExpressionWithLiterals += 1;
                            }

                            if (IsAssignedByOperations(current, SyntaxKind.AddExpression,
                                SyntaxKind.SubtractExpression))
                            {
                                currentVariable.AssignedByAddOrSubtractOp += 1;
                            }

                            if (IsAssignedByOperations(current, SyntaxKind.MultiplyExpression,
                                SyntaxKind.DivideExpression))
                            {
                                currentVariable.AssignedByMultiplyOrDivideOp += 1;
                            }

                            if (current.Parent != null && (current.Parent.Kind() == SyntaxKind.AddExpression || current.Parent.Kind() == SyntaxKind.SubtractExpression))
                            {
                                currentVariable.AddedOrSubtracted += 1;
                            }

                            if (current.Parent != null && current.Parent.Kind() == SyntaxKind.Argument
                                                       && current.Parent.Parent != null &&
                                                       current.Parent.Parent.Kind() == SyntaxKind.ArgumentList
                                                       && current.Parent.Parent.Parent != null &&
                                                       current.Parent.Parent.Parent.Kind() ==
                                                       SyntaxKind.InvocationExpression)
                            {
                                currentVariable.InvokedAsParameter += 1;
                            }

                            if (IsInStatement(current))
                            {
                                currentVariable.InIfStatement += 1;
                            }

                            var loopStatementLevel = GetLoopStatementDepth(current);
                            if (loopStatementLevel > 0)
                            {
                                switch (loopStatementLevel)
                                {
                                    case 1:
                                        currentVariable.InFirstLevelLoop += 1;
                                        break;
                                    case 2:
                                        currentVariable.InSecondLevelLoop += 1;
                                        break;
                                    case 3:
                                        currentVariable.InThirdLevelLoop += 1;
                                        break;
                                }
                            }

                            var accessMemberLevel = GetMemberAccessDepth(current);
                            if (accessMemberLevel > 0)
                            {
                                switch (accessMemberLevel)
                                {
                                    case 1:
                                        currentVariable.FirstLevelMemberAccessed += 1;
                                        break;
                                    case 2:
                                        currentVariable.SecondLevelMemberAccessed += 1;
                                        break;
                                    case 3:
                                        currentVariable.ThirdLevelMemberAccessed += 1;
                                        break;
                                }
                            }

                            if (current.Parent != null && current.Parent.Kind() != SyntaxKind.VariableDeclarator)
                            {
                                currentVariable.Used += 1;
                            }

                            if (current.Parent != null &&
                                current.Parent.Kind() == SyntaxKind.Argument && current.Parent.Parent != null &&
                                current.Parent.Parent.Kind() == SyntaxKind.BracketedArgumentList)
                            {
                                currentVariable.ArraySubScript += 1;
                            }

                            if (current.Parent != null && (current.Parent.Kind() == SyntaxKind.MultiplyExpression || current.Parent.Kind() == SyntaxKind.DivideExpression))
                            {
                                currentVariable.MultipliedOrDivided += 1;
                            }
                        }
                    }
                }

                foreach (var node in temp)
                {
                    queue.Enqueue(node);
                }
            }

            return variablesCount;
        }
        
        public static IEnumerable<SyntaxNode> GetMethodsFromClassNode(SyntaxNode syntaxNode)
        {
            var methods = new List<SyntaxNode>();
            foreach (var node in syntaxNode.ChildNodes())
            {
                if (node.Kind() == SyntaxKind.MethodDeclaration)
                {
                    methods.Add(node);
                }
            }
            return methods;
        }

        public static IEnumerable<SyntaxNode> GetMethodsFromNamespace(SyntaxNode syntaxNode)
        {
            var methods = new List<SyntaxNode>();
            foreach (var node in syntaxNode.ChildNodes())
            {
                if (node.Kind() == SyntaxKind.ClassDeclaration)
                {
                    methods.AddRange(GetMethodsFromClassNode(node));
                }
            }
            return methods;
        }

        private static bool IsDefinedByOperations(SyntaxNode node, SyntaxKind first, SyntaxKind second = SyntaxKind.UnknownAccessorDeclaration)
        {
            if (node == null)
            {
                return false;
            }

            foreach (var childNode in node.ChildNodes())
            {
                if (childNode != null && childNode.Kind() == SyntaxKind.EqualsValueClause)
                {
                    foreach (var syntaxNode in childNode.ChildNodes())
                    {
                        if (syntaxNode != null && (syntaxNode.Kind() == first ||
                                                   (second != SyntaxKind.UnknownAccessorDeclaration && syntaxNode.Kind() == second)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static bool IsDefinedByOperationsWithKind(SyntaxNode node, Func<SyntaxKind, bool> kindEvaluate)
        {
            if (node == null)
            {
                return false;
            }

            foreach (var childNode in node.ChildNodes())
            {
                if (childNode == null || childNode.Kind() != SyntaxKind.EqualsValueClause)
                {
                    continue;
                }

                foreach (var syntaxNode in childNode.ChildNodes())
                {
                    if (syntaxNode == null)
                    {
                        continue;
                    }

                    var kind = syntaxNode.Kind();

                    //Simple assignment to other variables
                    if (kindEvaluate(kind))
                    {
                        return true;
                    }

                    if (kind != SyntaxKind.AddExpression && kind != SyntaxKind.SubtractExpression &&
                        kind != SyntaxKind.MultiplyExpression && kind != SyntaxKind.DivideExpression)
                    {
                        continue;
                    }

                    if ((from subChildNode in syntaxNode.ChildNodes() where subChildNode != null select kindEvaluate(subChildNode.Kind())).Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool IsAssignedByOperations(SyntaxNode node, SyntaxKind first, SyntaxKind second = SyntaxKind.UnknownAccessorDeclaration)
        {
            if (node?.Parent == null || !IsAssignmentExpressionKind(node.Parent) && node.Parent.ChildNodes().First() != node)
            {
                return false;
            }

            foreach (var childNode in node.Parent.ChildNodes())
            {
                if (childNode != null && (childNode.Kind() == first || second != SyntaxKind.UnknownAccessorDeclaration && childNode.Kind() == second))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsAssignedByOperationsWithKind(SyntaxNode node, Func<SyntaxKind, bool> kindEvaluate)
        {
            if (node?.Parent == null || !IsAssignmentExpressionKind(node.Parent) && node.Parent.ChildNodes().First() == node)
            {
                return false;
            }

            foreach (var childNode in node.Parent.ChildNodes())
            {
                if (childNode == null || childNode == node)
                {
                    continue;
                }

                var kind = childNode.Kind();

                //Simple assignment to other variables
                if (kindEvaluate(kind))
                {
                    return true;
                }

                if (kind != SyntaxKind.AddExpression && kind != SyntaxKind.SubtractExpression &&
                    kind != SyntaxKind.MultiplyExpression && kind != SyntaxKind.DivideExpression)
                {
                    continue;
                }

                foreach (var syntaxNode in childNode.ChildNodes())
                {
                    if (syntaxNode == null)
                    {
                        continue;
                    }

                    if ((from subChildNode in syntaxNode.ChildNodes() where subChildNode != null select kindEvaluate(subChildNode.Kind())).Any())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static int GetLoopStatementDepth(SyntaxNode node)
        {
            if (node == null)
            {
                return 0;
            }

            var depth = 0;
            var temp = node;
            while (temp.Parent != null)
            {
                if (temp.Parent.Kind() == SyntaxKind.ForStatement
                    || temp.Parent.Kind() == SyntaxKind.ForEachStatement
                    || temp.Parent.Kind() == SyntaxKind.WhileStatement
                    || temp.Parent.Kind() == SyntaxKind.DoStatement)
                {
                    depth += 1;
                }

                temp = temp.Parent;
            }

            return depth;
        }

        private static int GetMemberAccessDepth(SyntaxNode node)
        {
            if (node?.Parent == null)
            {
                return 0;
            }

            var depth = 0;
            var temp = node;
            while (temp.Parent != null)
            {
                if (temp.Parent.Kind() == SyntaxKind.SimpleMemberAccessExpression)
                {
                    depth += 1;
                }

                temp = temp.Parent;
            }

            return depth;
        }

        private static bool IsInStatement(SyntaxNode node)
        {
            if (node == null)
            {
                return false;
            }

            var temp = node;
            while (temp.Parent != null)
            {
                if (temp.Parent.Kind() == SyntaxKind.IfStatement)
                {
                    return true;
                }

                temp = temp.Parent;
            }

            return false;
        }

        private static VariableName GetSingleVariable(IEnumerable<VariableName> dict, SyntaxNode top)
        {
            var variables = dict.Where(e => e.Name == top.GetFirstToken().Text).ToList();
            var singleVariable = variables.Count == 1 ? variables[0] : FindVariableName(variables, top);
            return singleVariable;
        }

        private static VariableName FindVariableName(IList<VariableName> variableNames, SyntaxNode node)
        {
            var temp = node;
            while (temp.Parent != null)
            {
                foreach (var v in variableNames)
                {
                    if (temp.Parent == v.ScopeNode)
                    {
                        return v;
                    }
                }

                temp = temp.Parent;
            }

            return null;
        }

        private static void PopulateDeclaration(SyntaxNode current, ICollection<VariableName> dict)
        {
            var tokenNode = current.GetFirstToken();
            if (current.Kind() == SyntaxKind.Parameter)
            {
                tokenNode = current.GetLastToken();
            }

            var currentScope = GetScope(current);

            var item = dict.FirstOrDefault(e => e.Name == tokenNode.Text && e.ScopeNode == currentScope);
            if (item == null)
            {
                dict.Add(new VariableName { Name = tokenNode.Text, ScopeNode = currentScope, Defined = 1 });
            }
            else
            {
                item.Defined += 1;
            }
        }

        private static bool IsAssignmentExpressionKind(SyntaxNode node)
        {
            if (node == null)
            {
                return false;
            }

            var kind = node.Kind();

            return kind == SyntaxKind.SimpleAssignmentExpression
                   || kind == SyntaxKind.AddAssignmentExpression
                   || kind == SyntaxKind.SubtractAssignmentExpression
                   || kind == SyntaxKind.MultiplyAssignmentExpression
                   || kind == SyntaxKind.DivideAssignmentExpression
                   || kind == SyntaxKind.ExclusiveOrAssignmentExpression
                   || kind == SyntaxKind.ModuloAssignmentExpression
                   || kind == SyntaxKind.AndAssignmentExpression
                   || kind == SyntaxKind.OrAssignmentExpression
                   || kind == SyntaxKind.RightShiftAssignmentExpression
                   || kind == SyntaxKind.LeftShiftAssignmentExpression;
        }

        private static bool IsLiteralExpressionKind(SyntaxKind kind)
        {
            return kind == SyntaxKind.NullLiteralExpression
                   || kind == SyntaxKind.StringLiteralExpression
                   || kind == SyntaxKind.TrueLiteralExpression
                   || kind == SyntaxKind.FalseLiteralExpression
                   || kind == SyntaxKind.NullLiteralExpression
                   || kind == SyntaxKind.NumericLiteralExpression
                   || kind == SyntaxKind.CharacterLiteralExpression;
        }

        private static bool IsIdentifierNameKind(SyntaxKind kind)
        {
            return kind == SyntaxKind.IdentifierName;
        }

        private static SyntaxNode GetScope(SyntaxNode current)
        {
            if (current.Parent!= null && current.Parent.Kind() == SyntaxKind.ForEachStatement)
            {
                return current.Parent;
            }

            if (current.Parent?.Parent != null && current.Parent.Parent.Kind() == SyntaxKind.LocalDeclarationStatement)
            {
                return current.Parent.Parent.Parent;
            }

            if (current.Parent?.Parent != null && current.Parent.Parent.Kind() == SyntaxKind.ForStatement)
            {
                return current.Parent.Parent;
            }


            return current.Parent?.Parent;
        }
    }
}
