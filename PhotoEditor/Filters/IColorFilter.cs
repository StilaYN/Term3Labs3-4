namespace PhotoEditor.Filters;

public interface IColorFilter
{
    public IImage ApplyFilter(IImage? image);
}