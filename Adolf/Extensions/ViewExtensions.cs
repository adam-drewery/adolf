using System.Collections.Generic;
using System.Linq;
using Terminal.Gui;

namespace Adolf.Extensions 
{
    public static class ViewExtensions
    {
        public static T View<T>(this View parent) where T : View => parent.Subviews.SingleOrDefault(v => v is T) as T
            ?? parent.Subviews.SelectMany(v => v.Subviews).SingleOrDefault(v => v is T) as T
            ?? throw new KeyNotFoundException("Failed to find a child view of type " + typeof(T).Name);
        
        public static void SetFocus<T>(this View parent) where T : View
        {
            var window = Application.Top.View<T>();
            parent.SetFocus(window);
        }
    }
}