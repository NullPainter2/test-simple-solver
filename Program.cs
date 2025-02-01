public enum State
{
    BE_DRUNK,
    HAVE_BEER,
    OPEN_FRIDGE,
    BE_AT_SHOP,
    HAVE_MONEY,
    HAVE_FRIDGE,
    BE_AT_HOME,
}

public class Utils
{
    public static string StringFromEnumList(List<State> states)
    {
        var result = "";
        for (int stateIndex = 0; stateIndex < states.Count; stateIndex++)
        {
            var state = states[stateIndex];
            if (stateIndex > 0) result += ", ";
            result += state.ToString();
        }
        return result;
    }
}

public class MyAction
{
    public string description = "";
    public List<State> preConditions = new List<State>();
    public List<State> postConditions = new List<State>();
}

public class Solution
{
    public bool deleteMe = false;

    public List<MyAction> actionsINeedToTake = new List<MyAction>();

    // all must be done
    public List<State> todoList = new List<State>();

    public void ReportSolution()
    {
        Console.WriteLine("Solution:");
        for (int i = 0; i < actionsINeedToTake.Count; i++)
        {
            int index = actionsINeedToTake.Count - 1 - i;
            var action = actionsINeedToTake[i];
            
            Console.WriteLine($"{i+1}. {actionsINeedToTake[index].description} [{Utils.StringFromEnumList(action.postConditions)}]");
        }
        Console.WriteLine();
    }

    public Solution Duplicate()
    {
        var copy = new Solution();
        copy.actionsINeedToTake = new List<MyAction>(this.actionsINeedToTake);
        copy.todoList = new List<State>(this.todoList);
        return copy;
    }
}

class Program
{
    public static void Main(string[] args)
    {
        ///////////// CONFIGURATION //////////////

        // Things I can do:
        List<MyAction> allActions = new List<MyAction>();

        // CONFIG
        InitializePossibleActions(allActions);

        Solve(allActions, new List<State> { State.BE_AT_HOME, State.HAVE_MONEY }, new List<State> { State.BE_DRUNK });
        Solve(allActions, new List<State> { State.BE_AT_HOME }, new List<State> { State.BE_DRUNK });
        Solve(allActions, new List<State> { State.BE_AT_SHOP }, new List<State> { State.BE_DRUNK });

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
            Console.WriteLine($"{action.description.PadLeft(firstColumnSize, ' ')} ... ({Utils.StringFromEnumList(action.preConditions)}) -> ({Utils.StringFromEnumList(action.postConditions)})");
        }
    }

    public static void InitializePossibleActions(List<MyAction> allActions)
    {
        {
            var action = new MyAction();
            action.description = "Drink the beer!";
            action.postConditions.Add(State.BE_DRUNK);
            //
            action.preConditions.Add(State.HAVE_BEER);
            allActions.Add(action);
        }

        {
            var action = new MyAction();
            action.description = "Take beer from fridge.";
            action.postConditions.Add(State.HAVE_BEER);
            //
            action.preConditions.Add(State.OPEN_FRIDGE);
            allActions.Add(action);
        }

        {
            var action = new MyAction();
            action.description = "Open fridge.";
            action.postConditions.Add(State.OPEN_FRIDGE);
            //
            action.preConditions.Add(State.HAVE_FRIDGE);
            allActions.Add(action);
        }

        {
            var action = new MyAction();
            action.description = "Buy beer at shop with money.";
            action.postConditions.Add(State.HAVE_BEER);
            //
            action.preConditions.Add(State.BE_AT_SHOP);
            action.preConditions.Add(State.HAVE_MONEY);
            allActions.Add(action);
        }

        {
            var action = new MyAction();
            action.description = "Steal beer from shop.";
            action.postConditions.Add(State.HAVE_BEER);
            //
            action.preConditions.Add(State.BE_AT_SHOP);
            allActions.Add(action);
        }

        {
            var action = new MyAction();
            action.description = "Go to shop.";
            action.postConditions.Add(State.BE_AT_SHOP);
            //
            action.preConditions.Add(State.BE_AT_HOME);
            //action.preConditions.Add(State.HAVE_MONEY); // infer it from the act of buying or stealing
            allActions.Add(action);
        }
    }

    public static void Solve(List<MyAction> allActions, List<State> START_SITUATION, List<State> END_SITUATION)
    {

        Console.WriteLine($"Current state is ({Utils.StringFromEnumList(START_SITUATION)}), and goal is ({Utils.StringFromEnumList(END_SITUATION)}). Solutions: ");
        Console.WriteLine("--------------");

        List<Solution> possibleSolutions = new List<Solution>();

        // add the goal
        var end = new Solution();
        AddListToList(END_SITUATION, end.todoList);
        possibleSolutions.Add(end);
        
        bool madeSomeProgress = true;
        while (madeSomeProgress)
        {
            madeSomeProgress = false;

            for (int i = 0; i < possibleSolutions.Count; i++)
            {
                var attempt = possibleSolutions[i];

                bool isSolution = attempt.todoList.Count == 0;
                if (isSolution)
                {
                    attempt.ReportSolution();

                    // return; // To print just first solution
                    
                    attempt.deleteMe = true; // To print all solutions
                }
                else
                {
                    // We try all we are able to do

                    foreach (var action in allActions)
                    {
                        if (IsSubsetOf(action.postConditions, attempt.todoList))
                        {
                            madeSomeProgress = true;

                            // Do not re-try this one again
                            
                            attempt.deleteMe = true;

                            Solution newAttempt = attempt.Duplicate();

                            // these will need to be solved

                            AddListToList(action.preConditions, newAttempt.todoList);

                            // there are solved

                            RemoveListFromList(action.postConditions, newAttempt.todoList);

                            RemoveListFromList(START_SITUATION, newAttempt.todoList);

                            // what actions were done

                            newAttempt.actionsINeedToTake.Add(action);

                            possibleSolutions.Add(newAttempt);
                        }
                    }
                }
            }

            // To make iteration in previous loop simpler, delete them later
            
            for (int i = 0; i < possibleSolutions.Count;)
            {
                if (possibleSolutions[i].deleteMe)
                {
                    possibleSolutions.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            if (!madeSomeProgress)
            {
                ;
            }
        }
    }


    static void AddListToList<T>(List<T> what, List<T> toWhat)
    {
        foreach (var item in what)
        {
            toWhat.Add(item);
        }
    }

    static void RemoveListFromList<T>(List<T> what, List<T> fromWhat)
    {
        foreach (var item in what)
        {
            fromWhat.Remove(item);
        }
    }

    static bool IsSubsetOf<T>(List<T> what, List<T> ofWhat)
    {
        foreach (var item in what)
        {
            if (!ofWhat.Contains(item))
            {
                return false;
            }
        }

        // @fixme is empty list valid too, I think it should be???

        return true;
    }
}