Very simple C# solver based on the idea of todo-list; blindly coded after hearing about GOAP. Given goal it adds the actions which fullfil that goal's requirements. Ends if no change can be made.

# Running

Just `dotnet run` or `dotnet watch`.

# Usage

## Define states
_Enum called `State` is expected in rest of code to exist._ 

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

## Add actions (State -> State)
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

## Run solver

For start & goal actions.

```C#
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME, State.HAVE_MONEY },
            new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_HOME }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_AT_SHOP }, new List<State> { State.BE_DRUNK });
        
        Solver.Solve(allActions, new List<State> { State.BE_DRUNK }, new List<State> { State.BE_DRUNK });

```

# Print result
![image](https://github.com/user-attachments/assets/1eb12c1d-9fb7-45df-8213-8d82f086bbaa)
