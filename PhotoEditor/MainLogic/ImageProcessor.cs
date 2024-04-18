using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PhotoEditor.MainLogic;

public class ImageProcessor:IImageProcessor
{
    private int _width;
    private int _height;
    private ILayer? _currentLayer;

    public HashSet<string> BusyLayersName { get; }
    public IImage ResultImage => ResultImageMethod();
    public List<ILayer> Layers { get; }

    public ILayer? CurrentLayer
    {
        get
        {
            return _currentLayer;
        }
        set
        {
            _currentLayer = value;
            OnPropertyChanged("CurrentLayer");
        }
    }

    public void SetCurrentLayer(string name)
    {
        CurrentLayer = Layers[SearchLayerPosition(name)];
    }

    public ImageProcessor(int width ,int height)
    {
        Layers = new List<ILayer>();
        Layers.Add(new Layer("default",new Image(width,height)));
        _currentLayer = (Layer)Layers[0];
        _currentLayer.PropertyChanged+=LayerChangedEventHandler;
        BusyLayersName = new HashSet<string>();
        BusyLayersName.Add("default");
        _width=width;
        _height=height;
    }

    public void AddLayer(string newLayerName, string? path)
    {
        if (!BusyLayersName.Contains(newLayerName))
        {
            Layer newLayer = path == null ? new Layer(newLayerName) : new Layer(newLayerName, path);
            newLayer.X = 0;
            newLayer.Y = 0;
            newLayer.PropertyChanged += LayerChangedEventHandler;
            Layers.Add(newLayer);
            BusyLayersName.Add(newLayerName);
            OnPropertyChanged("LayersList");
        }
        else throw new ArgumentException("Name is occupied");
    }

    public int SearchLayerPosition(string layerName)
    {
        for (int i = 0; i<Layers.Count;i++)
        {
            if (Layers[i].Name==layerName) return i;
        }
        throw new ArgumentException("Layer with this name not found");
    }

    public void RemoveLayer(string layerName)
    {
        foreach (var i in Layers)
        {
            if (i.Name == layerName) {
                CurrentLayer = null;
                Layers.Remove(i);
                BusyLayersName.Remove(layerName);
                OnPropertyChanged("LayersList");
                break;
            }
        }
    }

    public void UnRemoveLayer(ILayer layer, int pos)
    {
        BusyLayersName.Add(layer.Name);
        Layers.Insert(pos,layer);
        OnPropertyChanged("LayersList");
    }

    public IImage ResultImageMethod()
    {
        var result = new Image(_width,_height);
        foreach (var i in Layers)
        {
            SumImage(result,(Layer)i);
        }
        return result;
    }

    private void SumImage(IImage result, Layer layer)
    {
        int offsetX = layer.X;
        int offsetY = layer.Y;
        if (layer.ResultImage != null)
        {
            for (int i = 0; i + offsetY < result.Height && i < layer.ResultImage.Height; i++)
            {
                for (int j = 0; j + offsetX < result.Width && j < layer.ResultImage.Width; j++)
                {
                    if (i + offsetY >= 0 && j + offsetX >= 0)
                    {
                        result.Pixels[i + offsetY, j + offsetX] = layer.ResultImage.Pixels[i, j];
                    }
                }
            }
        }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string prop)
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
    private void LayerChangedEventHandler(object sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged(e.PropertyName);
    }
}