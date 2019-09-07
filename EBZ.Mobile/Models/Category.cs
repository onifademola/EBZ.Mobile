//using PropertyChanged;

namespace EBZ.Mobile.Models
{
    //[AddINotifyPropertyChangedInterface]
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Detail { get; set; }
    }
}
