public interface IActionAble
{
    void DoAction();
    void UnDoAction();
    bool IsDefaultState();

    bool CanExecuteAction();
}
