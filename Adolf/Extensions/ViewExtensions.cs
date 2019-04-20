using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Adolf.Extensions 
{
    public static class ViewExtensions
    {
        public static View View<T>(this View parent) => parent.Subviews.SingleOrDefault(v => v is T)
            ?? parent.Subviews.SelectMany(v => v.Subviews).SingleOrDefault(v => v is T)
            ?? throw new KeyNotFoundException("Failed to find a child view of type " + typeof(T).Name);
        
        public static void SetFocus<T>(this View parent) => parent.SetFocus(Application.Top.View<T>());
    }
}