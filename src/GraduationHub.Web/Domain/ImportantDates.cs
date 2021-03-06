﻿using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class ImportantDate
    {
        public int Id { get; set; }

        public DateTime DueDate { get; set; }

        [StringLength(FieldLengths.ImportantDate.Description)]
        public string Comments { get; set; }

        public int GraduatingClassId { get; set; }

    }
}