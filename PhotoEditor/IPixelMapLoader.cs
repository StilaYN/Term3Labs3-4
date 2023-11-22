namespace PhotoEditor;

public interface IPixelMapLoader
{
    (byte[],int,int) Load(string path);
}