using System;

namespace PhotoEditor;

public class ImageLoader:IImageLoader
{
    private IPixelMapLoader _pixelMapLoader;
    private IPixelMapBinder _mapBinder;

    public IPixelMapLoader PixelMapLoader
    {
        get => _pixelMapLoader;
        set => _pixelMapLoader = value ?? throw new ArgumentNullException(nameof(value));
    }

    public IPixelMapBinder MapBinder
    {
        get => _mapBinder;
        set => _mapBinder = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ImageLoader(IPixelMapLoader pixelMapLoader, IPixelMapBinder mapBinder)
    {
        _pixelMapLoader = pixelMapLoader;
        _mapBinder = mapBinder;
    }

    public IImage Load(string path)
    {
        (byte[] date,int width,int height) = _pixelMapLoader.Load(path);
        IImage image = new Image(_mapBinder.Create(date,width,height));
        return image;
    }

    
}