namespace api.helpers;

public class DataHelper<T>
{
  public static List<object> ExpandData(T data)
  {
    var propsList = typeof(T).GetProperties().Select(x => x.Name).ToList();
    List<object> valuesList = [];
    foreach (var prop in propsList)
    {
      object value = data?.GetType().GetProperty(prop)!.GetValue(data, null)!;
      valuesList.Add(value);
    }
    return valuesList;
  }
}