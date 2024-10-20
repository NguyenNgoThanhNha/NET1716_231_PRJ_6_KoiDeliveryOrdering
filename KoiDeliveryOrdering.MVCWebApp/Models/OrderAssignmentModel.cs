﻿using KoiDeliveryOrdering.Data.Entities;

namespace KoiDeliveryOrdering.MVCWebApp.Models
{
    public class OrderAssignmentModel
    {
        public int OrderAssignmentId { get; set; }

        public int DeliveryOrderId { get; set; }

        public int DriverId { get; set; }

        public int FishCarerId { get; set; }

        public int AssignedTruckId { get; set; }

        public DateTime AssignedDate { get; set; }

        public string DeliveryStatus { get; set; } = null!;

        public virtual Truck AssignedTruck { get; set; } = null!;

        public virtual DeliveryOrder DeliveryOrder { get; set; } = null!;

        public virtual Staff Driver { get; set; } = null!;

        public virtual Staff FishCarer { get; set; } = null!;
    }
}
