﻿using Concert.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Concert.Models
{
    public class TicketViewModel : RegisterTicketViewModel
    {
        public int EntranceId { get; set; }

        public IEnumerable<SelectListItem> Entrances { get; set; }

    }
}