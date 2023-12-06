using System.Collections.Generic;
using Microsoft.SqlServer.TransactSql.ScriptDom;


public class ViewVisitor : TSqlFragmentVisitor
{
    public List<CreateViewStatement> Statements = new List<CreateViewStatement>();

    public override void Visit(CreateViewStatement node)
    {
        Statements.Add(node);
    }
}

public class TableVisitor : TSqlFragmentVisitor
{
    public List<CreateTableStatement> Statements = new List<CreateTableStatement>();

    public override void Visit(CreateTableStatement node)
    {
        Statements.Add(node);
    }
}

public class ProcedureVisitor : TSqlFragmentVisitor
{
    public List<CreateProcedureStatement> Statements = new List<CreateProcedureStatement>();

    public override void Visit(CreateProcedureStatement node)
    {
        Statements.Add(node);
    }
}

public class FunctionVisitor : TSqlFragmentVisitor
{
    public List<CreateFunctionStatement> Statements = new List<CreateFunctionStatement>();

    public override void Visit(CreateFunctionStatement node)
    {
        Statements.Add(node);
    }
}



