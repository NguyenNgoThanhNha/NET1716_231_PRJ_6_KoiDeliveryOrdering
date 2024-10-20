﻿using System;
using System.Collections.Generic;

namespace KoiDeliveryOrdering.Data.Entities;

public partial class UserDTO
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; }

    public string Phone { get; set; } = null!;

    public string? AvatarImage { get; set; }

    public string? IdentityCard { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Address { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public string? Username { get; set; }

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }

    //public virtual ICollection<DeliveryOrder> DeliveryOrders { get; set; } = new List<DeliveryOrder>();

    public virtual ICollection<SenderInformation> SenderInformations { get; set; } = new List<SenderInformation>();
    public virtual ICollection<VoucherPromotion> VoucherPromotions { get; set; } = new List<VoucherPromotion>();
}
