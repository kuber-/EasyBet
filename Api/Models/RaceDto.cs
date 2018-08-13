using System;
using System.Collections.Generic;

namespace Api.Models
{
    public class RaceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public string Status { get; set; }
        public IEnumerable<HorseDto> Horses { get; set; }
    }
}
