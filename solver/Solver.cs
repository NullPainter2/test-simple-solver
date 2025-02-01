public class Solver
{
    public static void Solve(List<MyAction> allActions, List<State> START_SITUATION, List<State> END_SITUATION)
    {
        var title =
            $"Current state is ({ListUtils.StringFromEnumList(START_SITUATION)}), and goal is ({ListUtils.StringFromEnumList(END_SITUATION)}). Solutions: ";
        Console.WriteLine(title);
        Console.WriteLine("".PadLeft(title.Length, '-'));
        
        List<Solution> possibleSolutions = new List<Solution>();

        // add the goal
        var end = new Solution();
        ListUtils.AddListToListUniquely(END_SITUATION, end.todoList);
        ListUtils.RemoveListFromList(START_SITUATION, end.todoList);
        possibleSolutions.Add(end);

        bool madeSomeProgress = true;
        while (madeSomeProgress)
        {
            madeSomeProgress = false;

            for (int i = 0; i < possibleSolutions.Count; i++)
            {
                var solution = possibleSolutions[i];

                bool isFinalSolution = solution.todoList.Count == 0;
                if (isFinalSolution)
                {
                    solution.ReportSolution();

                    // return; // To print just first solution

                    solution.deleteMe = true; // To print all solutions
                }
                else
                {
                    foreach (var action in allActions)
                    {
                        if (ListUtils.IsSubsetOf(action.postConditions, solution.todoList))
                        {
                            madeSomeProgress = true;

                            // Do not re-try this one again
                            
                            solution.deleteMe = true;

                            Solution newAttempt = solution.Duplicate();
                            
                            // We are assuming there are no circles in the actions.
                            
                            if (action.postConditions.Count == 0)
                            {
                                throw new Exception("Can't solve when post conditions are empty.");
                            }
                            ListUtils.RemoveListFromList(action.postConditions, newAttempt.todoList);

                            // these will need to be solved
                            ListUtils.AddListToListUniquely(action.preConditions, newAttempt.todoList);

                            // initial state is solved by default
                            ListUtils.RemoveListFromList(START_SITUATION, newAttempt.todoList);

                            // what actions were done

                            newAttempt.actionsTaken.Add(action);

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
                ; // @debug
            }
        }
    }    
}