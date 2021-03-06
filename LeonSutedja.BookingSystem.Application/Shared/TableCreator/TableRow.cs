﻿namespace LeonSutedja.BookingSystem.Shared.TableCreator
{
    using System.Collections.Generic;

    public class TableRow
    {
        public int Id { get; set; }

        public int? NextVersionId { get; set; }

        public int? PendingNextVersionId { get; set; }

        public List<string> Cells { get; set; }
    }
}