using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Dedup
{
    public class VariableName
    {
        public string Name { get; set; }

        public SyntaxNode ScopeNode { get; set; }

        public int Used { get; set; }

        public int AddedOrSubtracted { get; set; }

        public int MultipliedOrDivided { get; set; }

        public int InvokedAsParameter { get; set; }

        public int InIfStatement { get; set; }

        public int ArraySubScript { get; set; }

        public int Defined { get; set; }

        public int DefinedByAddOrSubtractOp { get; set; }

        public int DefinedByMultiplyOrDivideOp { get; set; }

        public int DefinedByExpressionWithConstant { get; set; }

        public int DefinedByStringLiterals { get; set; }

        public int DefinedByCharacterLiteralExpression { get; set; }

        public int DefinedByNullLiteralExpression { get; set; }

        public int DefinedByBooleanLiteralExpression { get; set; }

        public int DefinedByNumericLiteralExpression { get; set; }

        public int DefinedByOtherVariable { get; set; }

        public int AssignedByExpressionWithLiterals { get; set; }

        public int AssignedByAddOrSubtractOp { get; set; }

        public int AssignedByMultiplyOrDivideOp { get; set; }

        public int AssignedByOtherVariable { get; set; }

        public int InThirdLevelLoop { get; set; }

        public int InSecondLevelLoop { get; set; }

        public int InFirstLevelLoop { get; set; }

        public int ThirdLevelMemberAccessed { get; set; }

        public int SecondLevelMemberAccessed { get; set; }

        public int FirstLevelMemberAccessed { get; set; }

        public int[] ToArray()
        {
            var list = new List<int>
            {
                Used,
                AddedOrSubtracted,
                MultipliedOrDivided,
                InvokedAsParameter,
                InIfStatement,
                ArraySubScript,
                Defined,
                DefinedByAddOrSubtractOp,
                DefinedByMultiplyOrDivideOp,
                DefinedByExpressionWithConstant,
                DefinedByExpressionWithConstant,
                DefinedByStringLiterals,
                DefinedByCharacterLiteralExpression,
                DefinedByNullLiteralExpression,
                DefinedByBooleanLiteralExpression,
                DefinedByNumericLiteralExpression,
                DefinedByOtherVariable,
                AssignedByExpressionWithLiterals,
                AssignedByAddOrSubtractOp,
                AssignedByMultiplyOrDivideOp,
                AssignedByOtherVariable,
                InThirdLevelLoop,
                InSecondLevelLoop,
                InFirstLevelLoop,
                ThirdLevelMemberAccessed,
                SecondLevelMemberAccessed,
                FirstLevelMemberAccessed
            };

            return list.ToArray();
        }

    }
}