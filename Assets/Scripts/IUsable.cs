/// <summary>
/// The IUsable interface is the main method of connecting any usable class and the player's inventory/hands.
/// </summary>
public interface IUsable
{
    void Use();
    void RightClick();
    void OnItemChange();
}