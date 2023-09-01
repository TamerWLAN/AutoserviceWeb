using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace db_TO_web.Models.TO
{
    public class CarList
    {
        public int CarListId { get; set; }
        [Display(Name = "Айди моедли")]
        public int ModelId { get; set; }
        public Model Model { get; set; }
        [Display(Name = "Тип авто")]
        public string AutoType { get; set; }
        [Display(Name = "ВИН номер")]
        public string VIN { get; set; }
        [Display(Name = "Дата регистрации")]
        public DateTime DateOfRegistration { get; set; }
        [Display(Name = "Дата снятия с учета")]
        public DateTime? DateOfDeregistration { get; set; }
        [Display(Name = "Дата последнего ТО")]
        public DateTime? LastToDate { get; set; }
        [Display(Name = "Текущий пробег")]
        public int CurrentTraveled { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }

        public CarList()
        {
           Maintenances = new List<Maintenance>();
        }
    }
}
