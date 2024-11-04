using System;
using System.Collections.Generic;

namespace LogiDriveBE.DAL.Models
{
    public partial class VehicleAssignment
    {
        public int IdVehicleAssignment { get; set; }
        public string? Comment { get; set; }
        public string TripType { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int IdVehicle { get; set; }
        public int IdLogReservation { get; set; }
        public bool Status { get; set; }
        public DateTime CreationDate { get; set; }

        // Nuevas propiedades
        public bool StatusTrip { get; set; } // Columna añadida en la base de datos
        public int DayQuantity { get; set; } // Columna añadida en la base de datos

        public virtual LogReservation IdLogReservationNavigation { get; set; } = null!;
        public virtual Vehicle IdVehicleNavigation { get; set; } = null!;
        public virtual ICollection<LogInspection> LogInspections { get; set; } = new List<LogInspection>();
        public virtual ICollection<LogProcess> LogProcesses { get; set; } = new List<LogProcess>();
        public virtual ICollection<LogTrip> LogTrips { get; set; } = new List<LogTrip>();
        public virtual ICollection<PreliminaryInspectionSheet> PreliminaryInspectionSheets { get; set; } = new List<PreliminaryInspectionSheet>();
    }
}
