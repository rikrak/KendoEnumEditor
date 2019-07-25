namespace KendoEnumEditor.Models
{
    public class DeliveryOptionsViewModel
    {
        public DeliveryOptionsViewModel()
        {
            
        }
        public DeliveryOptionsViewModel(int id, DayOfWeek chosenDay, DeliveryType type)
        {
            Id = id;
            ChosenDay = chosenDay;
            Type = type;
        }
        public int Id { get; set; }

        public DayOfWeek ChosenDay { get; set; }

        public DeliveryType Type { get; set; }
    }
}