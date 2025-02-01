class Program
{
    public static void Main(string[] args)
    {
        // Configure actions
        
        List<MyAction> allActions = new List<MyAction>();
        
        InitializePossibleActions(allActions);

        // Try three solutions
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME, State.HAVE_MONEY },
            new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_SHOP }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_DRUNK }, new List<State> { State.BE_DRUNK });

        PrintActions(allActions);
    }

    public static void PrintActions(List<MyAction> actions)
    {
        Console.WriteLine("Actions:");
        Console.WriteLine("--------");

        int firstColumnSize = 0;
        foreach (var action in actions)
        {
            firstColumnSize = Math.Max(firstColumnSize, action.description.Length);
        }

        foreach (var action in actions)
        {
            Console.WriteLine(
                $"{action.description.PadLeft(firstColumnSize, ' ')} ... ({ListUtils.StringFromEnumList(action.preConditions)}) -> ({ListUtils.StringFromEnumList(action.postConditions)})");
        }
    }

    public static void InitializePossibleActions(List<MyAction> allActions)
    {
        allActions.Add(new MyAction
        {
            description = "Drink the beer!",
            preConditions = { State.HAVE_BEER },
            postConditions = { State.BE_DRUNK },
        });

        allActions.Add(new MyAction
        {
            description = "Drink the beer!",
            preConditions = { State.HAVE_BEER },
            postConditions = { State.BE_DRUNK },
        });


        allActions.Add(new MyAction
        {
            description = "Take beer from fridge.",
            preConditions = { State.OPEN_FRIDGE },
            postConditions = { State.HAVE_BEER },
        });

        allActions.Add(new MyAction
        {
            description = "Open fridge.",
            preConditions = { State.HAVE_FRIDGE },
            postConditions = { State.OPEN_FRIDGE },
        });

        allActions.Add(new MyAction
        {
            description = "Buy beer at shop with money.",
            preConditions = { State.BE_AT_SHOP, State.HAVE_MONEY },
            postConditions = { State.HAVE_BEER },
        });

        allActions.Add(new MyAction
        {
            description = "Steal beer from shop.",
            preConditions = { State.BE_AT_SHOP },
            postConditions = { State.HAVE_BEER },
        });
        
        allActions.Add(new MyAction
        {
            description = "Go to shop.",
            preConditions = { State.BE_AT_HOME },
            postConditions = { State.BE_AT_SHOP },
        });
    }
}