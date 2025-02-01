public class Solution
{
    public bool deleteMe = false;

    public List<MyAction> actionsTaken = new List<MyAction>();

    public List<State> todoList = new List<State>();

    public void ReportSolution()
    {
        Console.WriteLine("Solution:");
        Console.WriteLine("---------");

        if (actionsTaken.Count == 0)
        {
            Console.WriteLine("Solved by doing nothing :)");
            Console.WriteLine();

            return;
        }
        
        // Solver solves from GOAL, so we print from the end 
        
        for (int i = 0; i < actionsTaken.Count; i++)
        {
            int index = actionsTaken.Count - 1 - i;
            var action = actionsTaken[i];
            
            Console.WriteLine($"{i+1}. {actionsTaken[index].description} [{ListUtils.StringFromEnumList(action.postConditions)}]");
        }
        Console.WriteLine();
    }

    public Solution Duplicate()
    {
        var copy = new Solution();
        copy.actionsTaken = new List<MyAction>(this.actionsTaken);
        copy.todoList = new List<State>(this.todoList);
        return copy;
    }
    
}