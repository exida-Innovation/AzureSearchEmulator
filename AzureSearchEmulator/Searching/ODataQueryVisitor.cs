﻿using Lucene.Net.Index;
using Lucene.Net.Search;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace AzureSearchEmulator.Searching;

public class ODataQueryVisitor : ISyntacticTreeVisitor<Query>
{
    public Query Visit(AllToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(AnyToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(BinaryOperatorToken tokenIn)
    {
        if (tokenIn.OperatorKind is BinaryOperatorKind.Or or BinaryOperatorKind.And)
        {
            var left = tokenIn.Left.Accept(this);
            var right = tokenIn.Right.Accept(this);
            var occur = GetOccurFromOperator(tokenIn.OperatorKind);

            return new BooleanQuery
            {
                Clauses =
                {
                    new BooleanClause(left, occur),
                    new BooleanClause(right, occur),
                }
            };
        }

        if (tokenIn.Left is EndPathToken endPathToken
            && tokenIn.OperatorKind == BinaryOperatorKind.Equal
            && tokenIn.Right is LiteralToken { Value: {} } literalToken)
        {
            string path = endPathToken.Identifier;

            return literalToken.Value switch
            {
                string stringValue => new TermQuery(new Term(path, stringValue)),
                int intValue => NumericRangeQuery.NewInt32Range(path, intValue, intValue, true, true),
                long longValue => NumericRangeQuery.NewInt64Range(path, longValue, longValue, true, true),
                float floatValue => NumericRangeQuery.NewSingleRange(path, floatValue, floatValue, true, true),
                double doubleValue => NumericRangeQuery.NewDoubleRange(path, doubleValue, doubleValue, true, true),
                bool boolValue => NumericRangeQuery.NewInt32Range(path, boolValue ? 1 : 0, boolValue ? 1 : 0, true, true),
                _ => throw new NotImplementedException()
            };
        }

        throw new NotImplementedException();
    }

    private static Occur GetOccurFromOperator(BinaryOperatorKind operatorKind)
    {
        return operatorKind switch
        {
            BinaryOperatorKind.Or => Occur.SHOULD,
            BinaryOperatorKind.And => Occur.MUST,
            _ => throw new NotImplementedException()
        };
    }

    public Query Visit(CountSegmentToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(InToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(DottedIdentifierToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(ExpandToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(ExpandTermToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(FunctionCallToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(LambdaToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(LiteralToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(InnerPathToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(OrderByToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(EndPathToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(CustomQueryOptionToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(RangeVariableToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(SelectToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(SelectTermToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(StarToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(UnaryOperatorToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(FunctionParameterToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(AggregateToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(AggregateExpressionToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(EntitySetAggregateToken tokenIn)
    {
        throw new NotImplementedException();
    }

    public Query Visit(GroupByToken tokenIn)
    {
        throw new NotImplementedException();
    }
}