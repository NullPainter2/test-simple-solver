Very simple C# solver.

# Define states
```C#
﻿public enum State
{
    BE_DRUNK,
    HAVE_BEER,
    OPEN_FRIDGE,
    BE_AT_SHOP,
    HAVE_MONEY,
    HAVE_FRIDGE,
    BE_AT_HOME,
}
```

# Add actions (State -> State)
```C#
       List<MyAction> allActions = new List<MyAction>();

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
        ...
```

# Solve

For start & goal actions.

```C#
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME, State.HAVE_MONEY },
            new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_SHOP }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_DRUNK }, new List<State> { State.BE_DRUNK });

```

# Print result
![image](https://github.com/user-attachments/assets/1741d6bc-7b29-446c-a604-0fcb8ac263e3)
