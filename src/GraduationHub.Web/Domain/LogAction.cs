using System;
using System.ComponentModel.DataAnnotations;
using GraduationHub.Web.Data;

namespace GraduationHub.Web.Domain
{
    public class LogAction
    {
        public LogAction(ApplicationUser performedBy, string action, string controller, string description)
        {
            PerformedBy = performedBy;
            Action = action;
            Controller = controller;
            Description = description;
            PerformedAt = DateTime.Now;
        }

        public int LogActionId { get; set; }

        public DateTime PerformedAt { get; set; }

        [StringLength(FieldLengths.LogAction.Controller)]
        public string Controller { get; set; }

        [StringLength(FieldLengths.LogAction.Action)]
        public string Action { get; set; }

        public ApplicationUser PerformedBy { get; set; }

        [StringLength(FieldLengths.LogAction.Description)]
        public string Description { get; set; }
    }
}