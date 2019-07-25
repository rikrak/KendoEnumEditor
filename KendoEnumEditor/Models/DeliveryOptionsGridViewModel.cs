using System.ComponentModel.DataAnnotations;

namespace KendoEnumEditor.Models
{
    public class DeliveryOptionsGridViewModel
    {
        public DeliveryOptionsGridViewModel()
        {
            
        }
        public DeliveryOptionsGridViewModel(int id, DayOfWeek chosenDay, DeliveryType type)
        {
            Id = id;
            ChosenDay = chosenDay;
            Type = type;
        }
        public int Id { get; set; }

        [UIHint("DayOfWeekGridEditor")]
        public DayOfWeek ChosenDay { get; set; }

        [UIHint("DeliveryTypeGridEditor")]
        public DeliveryType Type { get; set; }
    }
}