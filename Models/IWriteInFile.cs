using System.Threading.Tasks;
public interface IWriteInFile
{
    void Write();
    Task WriteAsync();
}