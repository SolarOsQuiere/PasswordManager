namespace PassManager.Services;

public sealed class TextVisibilityService
{
    public bool IsVisible { get; private set; } = true;

    public event Action? Changed;

    public void Toggle()
    {
        IsVisible = !IsVisible;
        Changed?.Invoke();
    }
}
