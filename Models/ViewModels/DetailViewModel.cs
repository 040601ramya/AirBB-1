using System;
using System.Collections.Generic;

namespace AirBB.Models.ViewModels
{
    public class DetailViewModel
    {
        public Residence Residence { get; set; } = null!;
        public List<DateTime> StartDateOptions { get; set; } = new();
        public List<DateTime> EndDateOptions { get; set; } = new();
    }
}
