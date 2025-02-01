public class ListUtils
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
    
    
    public static void AddListToListUniquely<T>(List<T> what, List<T> toWhat)
    {
        foreach (var item in what)
        {
            if (!toWhat.Contains(item))
            {
                toWhat.Add(item);
            }
        }
    }

    public static void RemoveListFromList<T>(List<T> what, List<T> fromWhat)
    {
        foreach (var item in what)
        {
            fromWhat.Remove(item);
        }
    }

    public static bool IsSubsetOf<T>(List<T> what, List<T> ofWhat)
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