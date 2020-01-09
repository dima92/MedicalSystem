using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalSystem
{
    class Patient
    {
        public string Age { get; set; } //возраст 
        public string Sex { get; set; } //пол 
        public string ChestPainType { get; set; } //тип боли
        public string BloodPressure { get; set; } //кровяное давление
        public string Cholestoral { get; set; } //кол-во холестерина в крови
        public string Sugar { get; set; } //кол-во сахара в крови
        public string Electrocardiographic { get; set; } //электрокардиография
        public string HeartRate { get; set; } //Макс. частота сердцебиения
        public string InducedAngina { get; set; } //Стенокардия
        public string StDepression { get; set; } //депрессия
        public string Slope { get; set; } //уклон пика упражнений
        public string NumberMajorVessels { get; set; } //кол-во крупных сосудов
        public string Thal { get; set; } //дефект
    }
}