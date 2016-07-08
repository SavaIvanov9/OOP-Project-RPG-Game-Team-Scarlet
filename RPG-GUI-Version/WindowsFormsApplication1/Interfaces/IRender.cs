namespace WindowsFormsApplication1.Interfaces
{
    using System.Text;

    public interface IRender
    {
        void WriteLine(string message, params object[] paramaters);

        void PrintScreen(StringBuilder screen);

        void Clear();
    }
}
