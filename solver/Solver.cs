public class Solver
{
    public static void Solve(List<MyAction> allActions, List<State> START_SITUATION, List<State> END_SITUATION)
    {

        Console.WriteLine($"Current state is ({ListUtils.StringFromEnumList(START_SITUATION)}), and goal is ({ListUtils.StringFromEnumList(END_SITUATION)}). Solutions: ");
        Console.WriteLine("--------------");

        List<Solution> possibleSolutions = new List<Solution>();

        // add the goal
        var end = new Solution();
        ListUtils.AddListToList(END_SITUATION, end.todoList);
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
                        if (ListUtils.IsSubsetOf(action.postConditions, attempt.todoList))
                        {
                            madeSomeProgress = true;

                            // Do not re-try this one again
                            
                            attempt.deleteMe = true;

                            Solution newAttempt = attempt.Duplicate();

                            // these will need to be solved

                            ListUtils.AddListToList(action.preConditions, newAttempt.todoList);

                            // there are solved

                            if (action.postConditions.Count == 0)
                            {
                                throw new Exception("Can't solve when post conditions are empty.");
                            }

                            ListUtils.RemoveListFromList(action.postConditions, newAttempt.todoList);

                            ListUtils.RemoveListFromList(START_SITUATION, newAttempt.todoList);

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
}