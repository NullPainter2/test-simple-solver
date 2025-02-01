public class Solution
{
    public bool deleteMe = false;

    public List<MyAction> actionsINeedToTake = new List<MyAction>();

    public List<State> doneList = new List<State>();

    public List<State> todoList = new List<State>();

    public void ReportSolution()
    {
        Console.WriteLine("Solution:");
        Console.WriteLine("---------");

        if (actionsINeedToTake.Count == 0)
        {
            Console.WriteLine("Solved by doing nothing :)");
            Console.WriteLine();

            return;
        }
        
        for (int i = 0; i < actionsINeedToTake.Count; i++)
        {
            int index = actionsINeedToTake.Count - 1 - i;
            var action = actionsINeedToTake[i];
            
            Console.WriteLine($"{i+1}. {actionsINeedToTake[index].description} [{ListUtils.StringFromEnumList(action.postConditions)}]");
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