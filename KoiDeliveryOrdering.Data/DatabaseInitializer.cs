﻿using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrdering.Data;

public interface IDatabaseInitializer
{
    Task InitializeAsync();
    Task TrySeedAsync();
    Task SeedAsync();
}

public class DatabaseInitializer(KoiDeliveryOrderingDbContext dbContext) : IDatabaseInitializer
{
    //  Summary:
    //      Initialize Database
    public async Task InitializeAsync()
    {
        try
        {
            // Check whether the database exists and can be connected to
            if (!await dbContext.Database.CanConnectAsync())
            {
                // Check for applied migrations
                var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();
                if (appliedMigrations.Any())
                {
                    Console.WriteLine("Migrations have been applied.");
                    return;
                }

                // Perform migration if necessary
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Database initialized successfully");
            }
            else
            {
                Console.WriteLine("Database cannot be connected to.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //  Summary:
    //      Try to perform seeding data
    public async Task TrySeedAsync()
    {
        try
        {
            await SeedAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //  Summary:
    //      Seeding data
    public async Task SeedAsync()
    {
        try
        {
            // Users
            if (!dbContext.Users.Any()) await SeedUserAsync();
            // Payment Types
            if (!dbContext.Payments.Any()) await SeedPaymentTypeAsync();
            // Shipping fees
            if (!dbContext.ShippingFees.Any()) await SeedShippingFeeAsync();
            // Delivery Orders
            if (!dbContext.DeliveryOrders.Any()) await SeedDeliveryOrderAsync();
            // Animal Types
            if (!dbContext.AnimalTypes.Any()) await SeedAnimalTypesAsync();
            // Animal
            if (!dbContext.Animals.Any()) await SeedAnimalsAsync();
            // Delivery Order Details
            if (!dbContext.DeliveryOrderDetails.Any()) await SeedDeliveryOrderDetailsAsync();
            // Care Task
            if (!dbContext.CareTasks.Any()) await SeedCareTasksAsync();
            // Staff
            if (!dbContext.Staff.Any()) await SeedStaffAsync();
            // Document
            if (!dbContext.Documents.Any()) await SeedDocumentAsync();

            // More seeding here...
            // Each table need to create private method to seeding data

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //  Summary:
    //      Seeding Users
    private async Task SeedUserAsync()
    {
        List<UserDTO> users = new()
        {
            new UserDTO()
            {
                Username = "admin",
                Password = "Admin123",
                FullName = "Admin",
                Phone = "0123456789",
                Email = "admin@admin.com",
                SenderInformations = new List<SenderInformation>()
                {
                    new()
                    {
                        District = "ABC",
                        Latitude = 12,
                        Longitude = 12,
                        Street = "123 Main Street",
                        SenderName = "Random Name",
                        CityProvince = "Random City",
                        Ward = "Random Ward",
                        SenderPhone = "0123456789"
                    },
                    new()
                    {
                        District = "ABC",
                        Latitude = 12,
                        Longitude = 12,
                        Street = "123 Main Street",
                        SenderName = "Random Name",
                        CityProvince = "Random City",
                        Ward = "Random Ward",
                        SenderPhone = "0123456789"
                    }
                },
                VoucherPromotions = new List<VoucherPromotion>()
                {
                    new()
                    {
                        VoucherPromotionCode = "ABC",
                        PromotionRate = 10
                    },
                    new()
                    {
                        VoucherPromotionCode = "ABC_2",
                        PromotionRate = 15
                    },
                    new()
                    {
                        VoucherPromotionCode = "ABC_3",
                        PromotionRate = 15
                    }
                }
            }
        };

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedStaffAsync()
    {
        List<Staff> staffList = new()
        {
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Nguyen Van A",
                Email = "nguyenvana@example.com",
                DateOfBirth = new DateTime(1990, 1, 1),
                Phone = "0987654321",
                AvatarImage = "avatar_nguyenvana.jpg",
                IdentityCard = "123456789",
                CreateDate = DateTime.Now,
                Address = "456 Another Street",
                Longitude = 105.8342,
                Latitude = 21.0285,
                Username = "nguyenvana",
                Password = "@Password123",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Tran Thi B",
                Email = "tranthib@example.com",
                DateOfBirth = new DateTime(1985, 5, 5),
                Phone = "0987654322",
                AvatarImage = "avatar_tranthib.jpg",
                IdentityCard = "987654321",
                CreateDate = DateTime.Now.AddMonths(-1),
                Address = "789 Example Road",
                Longitude = 105.8442,
                Latitude = 21.0385,
                Username = "tranthib",
                Password = "@Password1234",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Le Van C",
                Email = "levanc@example.com",
                DateOfBirth = new DateTime(1992, 3, 3),
                Phone = "0987654323",
                AvatarImage = "avatar_levanc.jpg",
                IdentityCard = "123123123",
                CreateDate = DateTime.Now.AddMonths(-2),
                Address = "123 Example Avenue",
                Longitude = 105.8542,
                Latitude = 21.0485,
                Username = "levanc",
                Password = "@Password1235",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Pham Thi D",
                Email = "phamthid@example.com",
                DateOfBirth = new DateTime(1988, 7, 7),
                Phone = "0987654324",
                AvatarImage = "avatar_phamthid.jpg",
                IdentityCard = "321321321",
                CreateDate = DateTime.Now.AddMonths(-3),
                Address = "321 Another Road",
                Longitude = 105.8642,
                Latitude = 21.0585,
                Username = "phamthid",
                Password = "@Password1236",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Hoang Van E",
                Email = "hoangvane@example.com",
                DateOfBirth = new DateTime(1995, 8, 8),
                Phone = "0987654325",
                AvatarImage = "avatar_hoangvane.jpg",
                IdentityCard = "456456456",
                CreateDate = DateTime.Now.AddMonths(-4),
                Address = "654 Example Street",
                Longitude = 105.8742,
                Latitude = 21.0685,
                Username = "hoangvane",
                Password = "@Password1237",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Nguyen Thi F",
                Email = "nguyenthif@example.com",
                DateOfBirth = new DateTime(1993, 6, 6),
                Phone = "0987654326",
                AvatarImage = "avatar_nguyenthif.jpg",
                IdentityCard = "654654654",
                CreateDate = DateTime.Now.AddMonths(-5),
                Address = "987 Another Street",
                Longitude = 105.8842,
                Latitude = 21.0785,
                Username = "nguyenthif",
                Password = "@Password1238",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Vu Van G",
                Email = "vuvang@example.com",
                DateOfBirth = new DateTime(1980, 12, 12),
                Phone = "0987654327",
                AvatarImage = "avatar_vuvang.jpg",
                IdentityCard = "987987987",
                CreateDate = DateTime.Now.AddMonths(-6),
                Address = "654 Another Avenue",
                Longitude = 105.8942,
                Latitude = 21.0885,
                Username = "vuvang",
                Password = "@Password1239",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Mai Thi H",
                Email = "maithih@example.com",
                DateOfBirth = new DateTime(1987, 9, 9),
                Phone = "0987654328",
                AvatarImage = "avatar_maithih.jpg",
                IdentityCard = "321654987",
                CreateDate = DateTime.Now.AddMonths(-7),
                Address = "159 Example Place",
                Longitude = 105.9042,
                Latitude = 21.0985,
                Username = "maithih",
                Password = "@Password1240",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Tran Van I",
                Email = "tranvani@example.com",
                DateOfBirth = new DateTime(1994, 2, 2),
                Phone = "0987654329",
                AvatarImage = "avatar_tranvanI.jpg",
                IdentityCard = "654123987",
                CreateDate = DateTime.Now.AddMonths(-8),
                Address = "753 Example Way",
                Longitude = 105.9142,
                Latitude = 21.1085,
                Username = "tranvanI",
                Password = "@Password1241",
                IsActive = true,
            },
            new Staff()
            {
                StaffId = Guid.NewGuid(),
                FullName = "Nguyen Van J",
                Email = "nguyenvanj@example.com",
                DateOfBirth = new DateTime(1996, 4, 4),
                Phone = "0987654330",
                AvatarImage = "avatar_nguyenvanj.jpg",
                IdentityCard = "789321456",
                CreateDate = DateTime.Now.AddMonths(-9),
                Address = "852 Another Way",
                Longitude = 105.9242,
                Latitude = 21.1185,
                Username = "nguyenvanj",
                Password = "@Password1242",
                IsActive = true,
            },
        };

        await dbContext.Staff.AddRangeAsync(staffList);
        await dbContext.SaveChangesAsync();
    }


    //  Summary:
    //      Seeding Payment type
    private async Task SeedPaymentTypeAsync()
    {
        List<Payment> payments = new()
        {
            new()
            {
                PaymentMethod = "Cash"
            },
            new()
            {
                PaymentMethod = "Visa"
            },
            new()
            {
                PaymentMethod = "Debit"
            },
            new()
            {
                PaymentMethod = "Momo"
            },
            new()
            {
                PaymentMethod = "VNPay"
            }
        };

        await dbContext.Payments.AddRangeAsync(payments);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Shipping fee
    private async Task SeedShippingFeeAsync()
    {
        var shippingFees = new List<ShippingFee>
        {
            new ShippingFee
            {
                DistanceRangeFrom = 0m,
                DistanceRangeTo = 5m,
                ServiceCode = "STD",
                WeightClass = 3,
                BaseFee = 22000m,
                EstimatedTime = "1-2 days"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 5.01m,
                DistanceRangeTo = 10m,
                ServiceCode = "EXP",
                WeightClass = 2,
                BaseFee = 36000m,
                EstimatedTime = "Same day"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 10.01m,
                DistanceRangeTo = 20m,
                ServiceCode = "STD",
                WeightClass = 3,
                BaseFee = 50000m,
                EstimatedTime = "2-3 days"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 20.01m,
                DistanceRangeTo = 50m,
                ServiceCode = "PRM",
                WeightClass = 1,
                BaseFee = 60000m,
                EstimatedTime = "Next day"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 50.01m,
                DistanceRangeTo = 100m,
                ServiceCode = "EXP",
                WeightClass = 2,
                BaseFee = 75000m,
                EstimatedTime = "Same day"
            }
        };

        await dbContext.ShippingFees.AddRangeAsync(shippingFees);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Delivery Orders
    private async Task SeedDeliveryOrderAsync()
    {
        var rnd = new Random();
        var paymentTypes = await dbContext.Payments.ToListAsync();
        var shippingFees = await dbContext.ShippingFees.ToListAsync();
        var senders = await dbContext.SenderInformations.ToListAsync();

        var deliveryOrders = new List<DeliveryOrder>
        {
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "123 Elm St, New York, NY",
                RecipientLongitude = -73.935242,
                RecipientLatitude = 40.730610,
                RecipientAppointmentTime = "2024-09-28 10:00 AM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-10),
                DeliveryDate = DateTime.Now.AddDays(-1),
                OrderStatus = "Delivered",
                TotalAmount = 100.50m,
                TaxFee = 8.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Oak St, San Francisco, CA",
                RecipientLongitude = -122.4194,
                RecipientLatitude = 37.7749,
                RecipientAppointmentTime = "2024-09-30 2:00 PM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-3),
                DeliveryDate = null,
                OrderStatus = "Out for Delivery",
                TotalAmount = 95.50m,
                TaxFee = 5.50m,
                IsPurchased = true,
                IsSenderPurchase = true,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "202 Elm St, Miami, FL",
                RecipientLongitude = -80.1918,
                RecipientLatitude = 25.7617,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-5),
                DeliveryDate = DateTime.Now.AddDays(2),
                OrderStatus = "Pending",
                TotalAmount = 120.00m,
                TaxFee = null,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "456 Willow Rd, Austin, TX",
                RecipientLongitude = -97.7431,
                RecipientLatitude = 30.2672,
                RecipientAppointmentTime = "2024-10-05 1:30 PM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-7),
                DeliveryDate = null,
                OrderStatus = "Dispatched",
                TotalAmount = 150.25m,
                TaxFee = 10.25m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Maple Dr, Denver, CO",
                RecipientLongitude = -104.9903,
                RecipientLatitude = 39.7392,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-2),
                DeliveryDate = DateTime.Now.AddDays(1),
                OrderStatus = "Scheduled",
                TotalAmount = 110.00m,
                TaxFee = 7.50m,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Birch St, Las Vegas, NV",
                RecipientLongitude = -115.1398,
                RecipientLatitude = 36.1699,
                RecipientAppointmentTime = "2024-09-27 4:00 PM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-4),
                DeliveryDate = DateTime.Now,
                OrderStatus = "Delivered",
                TotalAmount = 140.75m,
                TaxFee = 9.75m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "333 Cedar Ln, Dallas, TX",
                RecipientLongitude = -96.7970,
                RecipientLatitude = 32.7767,
                RecipientAppointmentTime = null,
                CreateDate = DateTime.Now.AddDays(-1),
                RecipientPhone = "0123456789",
                DeliveryDate = null,
                OrderStatus = "Processing",
                TotalAmount = 175.00m,
                TaxFee = 12.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Elm Dr, Salt Lake City, UT",
                RecipientLongitude = -111.8910,
                RecipientLatitude = 40.7608,
                RecipientAppointmentTime = "2024-09-26 10:00 AM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-6),
                DeliveryDate = DateTime.Now.AddDays(1),
                OrderStatus = "Pending",
                TotalAmount = 90.00m,
                TaxFee = 6.00m,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Pine St, Charlotte, NC",
                RecipientLongitude = -80.8431,
                RecipientLatitude = 35.2271,
                RecipientAppointmentTime = "2024-09-29 11:00 AM",
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-8),
                DeliveryDate = null,
                OrderStatus = "Out for Delivery",
                TotalAmount = 130.50m,
                TaxFee = 8.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Birch Ln, Nashville, TN",
                RecipientLongitude = -86.7816,
                RecipientLatitude = 36.1627,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
                CreateDate = DateTime.Now.AddDays(-5),
                DeliveryDate = DateTime.Now.AddDays(3),
                OrderStatus = "Scheduled",
                TotalAmount = 160.75m,
                TaxFee = 11.75m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            }
        };

        await dbContext.DeliveryOrders.AddRangeAsync(deliveryOrders);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Document
    private async Task SeedDocumentAsync()
    {
        List<Document> documents = new()
        {
            new Document
            {
                DocumentNumber = "DOC001534",
                DocumentType = "Import",
                IssueDate = new DateOnly(2023, 1, 15),
                ExpirationDate = new DateOnly(2024, 1, 15),
                ConsigneeName = "ABC Company",
                ConsigneePhone = "+1234567890",
                ConsigneeAddress = "123 Main Street, Springfield",
                ExporterName = "Global Exports Ltd.",
                ExporterPhone = "+1987654321",
                ExporterAddress = "789 Business Rd, Shelbyville",
                DispatchMethod = "air",
                // FinalDestination = "New York, USA",
                // TransportationNo = "TRANS001",
                TransportationType = "airplan",
                DeliveryOrderId = 1,
                // PortOfLoading = "London Heathrow Airport",
                // PortOfDischarge = "JFK International Airport",
                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 1.4,
                        ItemQuantity = 2,
                        ItemEstimatePrice = 1.4 * 2 * 7000,
                        ItemName = "Black Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.3,
                        ItemQuantity = 4,
                        ItemEstimatePrice = 0.3 * 4 * 7000,
                        ItemName = "Medicine Koi",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 28000m,
            },
            new Document
            {
                DocumentNumber = "DOC002345",
                DocumentType = "Packing List",
                IssueDate = new DateOnly(2023, 2, 1),
                ExpirationDate = null,
                ConsigneeName = "XYZ Industries",
                ConsigneePhone = "+9876543210",
                ConsigneeAddress = "456 Industry Ave, Metropolis",
                ExporterName = "FastShip Logistics",
                ExporterPhone = "+1234098765",
                ExporterAddress = "12 Logistic Ln, Gotham City",
                DispatchMethod = "sea",
                // FinalDestination = "San Francisco, USA",
                // TransportationNo = "TRANS002",
                TransportationType = "ship",
                DeliveryOrderId = 2,
                // PortOfLoading = "Port of Rotterdam",
                // PortOfDischarge = "Port of Oakland",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 2.0, ItemQuantity = 1, ItemEstimatePrice = 2.0 * 1 * 7000,
                        ItemName = "Platinum Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.5, ItemQuantity = 3, ItemEstimatePrice = 0.5 * 3 * 7000,
                        ItemName = "Blue Koi Net",
                        ItemCategory = "Fish"
                    }
                },
                ShippingFee = 24500m
            },
            new Document
            {
                DocumentNumber = "DOC003456",
                DocumentType = "Bill of Lading",
                IssueDate = new DateOnly(2023, 3, 10),
                ExpirationDate = new DateOnly(2023, 9, 10),
                ConsigneeName = "PQR Traders",
                ConsigneePhone = "+1122334455",
                ConsigneeAddress = "789 Market St, Star City",
                ExporterName = "TradeMasters Inc.",
                ExporterPhone = "+9988776655",
                ExporterAddress = "99 Export Blvd, Central City",
                DispatchMethod = "road",
                DeliveryOrderId = 3,
                // FinalDestination = "Chicago, USA",
                // TransportationNo = "TRANS003",
                TransportationType = "truck",
                // PortOfLoading = "Houston Port",
                // PortOfDischarge = "Chicago Terminal",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 1.2, ItemQuantity = 2, ItemEstimatePrice = 1.2 * 2 * 7000, ItemName = "Red Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.4, ItemQuantity = 5, ItemEstimatePrice = 0.4 * 5 * 7000,
                        ItemName = "Feeding Bucket",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 30800m
            },
            new Document
            {
                DocumentNumber = "DOC004567",
                DocumentType = "Certificate of Origin",
                IssueDate = new DateOnly(2023, 4, 5),
                ExpirationDate = null,
                ConsigneeName = "LMN Enterprises",
                ConsigneePhone = "+1029384756",
                ConsigneeAddress = "101 Commerce Blvd, Coast City",
                ExporterName = "Origin Exports",
                ExporterPhone = "+5647382910",
                ExporterAddress = "55 Heritage St, Emerald City",
                DispatchMethod = "rail",
                DeliveryOrderId = 4,
                // FinalDestination = "Dallas, USA",
                // TransportationNo = "TRANS004",
                TransportationType = "train",
                // PortOfLoading = "Los Angeles Terminal",
                // PortOfDischarge = "Dallas Terminal",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 1.7, ItemQuantity = 1, ItemEstimatePrice = 1.7 * 1 * 7000,
                        ItemName = "Golden Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.7, ItemQuantity = 2, ItemEstimatePrice = 0.7 * 2 * 7000,
                        ItemName = "Koi Care Kit",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 21700m
            },
            new Document
            {
                DocumentNumber = "DOC005678",
                DocumentType = "Commercial Invoice",
                IssueDate = new DateOnly(2023, 5, 18),
                ExpirationDate = new DateOnly(2024, 5, 18),
                ConsigneeName = "RST Company",
                ConsigneePhone = "+6758493021",
                ConsigneeAddress = "45 Trade Dr, Gotham City",
                ExporterName = "Global Trade Ltd.",
                ExporterPhone = "+7849301023",
                ExporterAddress = "600 Export Way, Metropolis",
                DispatchMethod = "air",
                DeliveryOrderId = 5,
                // FinalDestination = "Seattle, USA",
                // TransportationNo = "TRANS005",
                TransportationType = "airplane",
                // PortOfLoading = "Paris Charles de Gaulle Airport",
                // PortOfDischarge = "Seattle-Tacoma International Airport",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 0.9, ItemQuantity = 3, ItemEstimatePrice = 0.9 * 3 * 7000,
                        ItemName = "Butterfly Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.2, ItemQuantity = 10, ItemEstimatePrice = 0.2 * 10 * 7000,
                        ItemName = "Koi Fish Food",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 32900m
            },
            new Document
            {
                DocumentNumber = "DOC006789",
                DocumentType = "Export Declaration",
                IssueDate = new DateOnly(2023, 6, 25),
                ExpirationDate = null,
                ConsigneeName = "UVW Group",
                ConsigneePhone = "+1231231234",
                ConsigneeAddress = "111 Business Park, Starling City",
                ExporterName = "Export Partners Ltd.",
                ExporterPhone = "+3213214321",
                ExporterAddress = "99 Export Street, Central City",
                DispatchMethod = "sea",
                DeliveryOrderId = 6,
                // FinalDestination = "Miami, USA",
                // TransportationNo = "TRANS006",
                TransportationType = "ship",
                // PortOfLoading = "Shanghai Port",
                // PortOfDischarge = "Port of Miami",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 1.1, ItemQuantity = 1, ItemEstimatePrice = 1.1 * 1 * 7000, ItemName = "Snow Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.6, ItemQuantity = 2, ItemEstimatePrice = 0.6 * 2 * 7000,
                        ItemName = "Aquarium Heater",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 16100m
            },
            new Document
            {
                DocumentNumber = "DOC007890",
                DocumentType = "Insurance Certificate",
                IssueDate = new DateOnly(2023, 7, 12),
                ExpirationDate = new DateOnly(2024, 7, 12),
                ConsigneeName = "DEF Logistics",
                ConsigneePhone = "+4564564567",
                ConsigneeAddress = "23 Shipping Lane, Keystone City",
                ExporterName = "Assured Exports Inc.",
                ExporterPhone = "+6546547654",
                ExporterAddress = "32 Insurance Blvd, Star City",
                DispatchMethod = "road",
                DeliveryOrderId = 7,
                // FinalDestination = "Los Angeles, USA",
                // TransportationNo = "TRANS007",
                TransportationType = "truck",
                // PortOfLoading = "Houston Port",
                // PortOfDischarge = "Los Angeles Terminal",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 2.5, ItemQuantity = 2, ItemEstimatePrice = 2.5 * 2 * 7000,
                        ItemName = "Big Head Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.8, ItemQuantity = 3, ItemEstimatePrice = 0.8 * 3 * 7000,
                        ItemName = "Water Filter",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 51800m
            },
            new Document
            {
                DocumentNumber = "DOC008901",
                DocumentType = "Shipping Order",
                IssueDate = new DateOnly(2023, 8, 20),
                ExpirationDate = null,
                ConsigneeName = "GHI Retailers",
                ConsigneePhone = "+7897897890",
                ConsigneeAddress = "678 Commerce Ave, Coast City",
                ExporterName = "WorldWide Shippers",
                ExporterPhone = "+8908908901",
                ExporterAddress = "77 Global Rd, Emerald City",
                DispatchMethod = "air",
                DeliveryOrderId = 8,
                // FinalDestination = "Denver, USA",
                // TransportationNo = "TRANS008",
                TransportationType = "airplane",
                // PortOfLoading = "Frankfurt Airport",
                // PortOfDischarge = "Denver International Airport",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 1.6, ItemQuantity = 4, ItemEstimatePrice = 1.6 * 4 * 7000,
                        ItemName = "Orange Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.3, ItemQuantity = 5, ItemEstimatePrice = 0.3 * 5 * 7000,
                        ItemName = "Koi Pond Pump",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 55300m
            },
            new Document
            {
                DocumentNumber = "DOC009012",
                DocumentType = "Proforma Invoice",
                IssueDate = new DateOnly(2023, 9, 5),
                ExpirationDate = new DateOnly(2024, 9, 5),
                ConsigneeName = "JKL Suppliers",
                ConsigneePhone = "+0980980987",
                ConsigneeAddress = "88 Supplier St, Metropolis",
                ExporterName = "ExportKing Ltd.",
                ExporterPhone = "+8768768765",
                ExporterAddress = "123 Export Avenue, Gotham City",
                DispatchMethod = "rail",
                DeliveryOrderId = 9,
                // FinalDestination = "Boston, USA",
                // TransportationNo = "TRANS009",
                TransportationType = "train",
                // PortOfLoading = "Vancouver Terminal",
                // PortOfDischarge = "Boston Terminal",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 0.5, ItemQuantity = 6, ItemEstimatePrice = 0.5 * 6 * 7000,
                        ItemName = "Silver Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 1.3, ItemQuantity = 1, ItemEstimatePrice = 1.3 * 1 * 7000,
                        ItemName = "Koi Pond Liner",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 30100m
            },
            new Document
            {
                DocumentNumber = "DOC010123",
                DocumentType = "Inspection Certificate",
                IssueDate = new DateOnly(2023, 10, 15),
                ExpirationDate = null,
                ConsigneeName = "MNO Warehousing",
                ConsigneePhone = "+6543219870",
                ConsigneeAddress = "99 Distribution Blvd, Star City",
                ExporterName = "Inspection Ready Exports",
                ExporterPhone = "+3459872345",
                ExporterAddress = "456 Verification Ln, Central City",
                DispatchMethod = "sea",
                DeliveryOrderId = 10,
                // FinalDestination = "Houston, USA",
                // TransportationNo = "TRANS010",
                TransportationType = "ship",
                // PortOfLoading = "Port of Singapore",
                // PortOfDischarge = "Port of Houston",

                DocumentDetails = new List<DocumentDetail>()
                {
                    new()
                    {
                        ItemWeight = 0.7, ItemQuantity = 4, ItemEstimatePrice = 0.7 * 4 * 7000,
                        ItemName = "Yellow Koi",
                        ItemCategory = "Fish"
                    },
                    new()
                    {
                        ItemWeight = 0.9, ItemQuantity = 2, ItemEstimatePrice = 0.9 * 2 * 7000,
                        ItemName = "Koi Treatment Kit",
                        ItemCategory = "Accessory"
                    }
                },
                ShippingFee = 32200m
            }
        };

        await dbContext.Documents.AddRangeAsync(documents);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Animal Type
    private async Task SeedAnimalTypesAsync()
    {
        List<AnimalType> animalTypes = new()
        {
            new() { AnimalTypeDesc = "Dog" },
            new() { AnimalTypeDesc = "Bird" },
            new() { AnimalTypeDesc = "Cat" },
            new() { AnimalTypeDesc = "Fish" },
        };

        await dbContext.AnimalTypes.AddRangeAsync(animalTypes);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedAnimalsAsync()
    {
        List<Animal> animals = new()
        {
            new Animal
            {
                AnimalId = Guid.NewGuid(),
                Breed = "Golden Retriever",
                ColorPattern = "Golden",
                Size = 30.5m,
                Age = 2,
                EstimatedPrice = 1200.50m,
                HealthStatus = "Healthy",
                IsAvailable = true,
                OriginCountry = "USA",
                Description = "Friendly and intelligent dog.",
                ImageUrl = "https://example.com/golden_retriever.jpg",
                AnimalTypeId = 1 // Giả sử AnimalTypeId = 1 là Dog
            },
            new Animal
            {
                AnimalId = Guid.NewGuid(),
                Breed = "Persian",
                ColorPattern = "White",
                Size = 10.3m,
                Age = 3,
                EstimatedPrice = 800.00m,
                HealthStatus = "Healthy",
                IsAvailable = true,
                OriginCountry = "Iran",
                Description = "Calm and loving cat.",
                ImageUrl = "https://example.com/persian_cat.jpg",
                AnimalTypeId = 2 // Giả sử AnimalTypeId = 2 là Cat
            },
            new Animal
            {
                AnimalId = Guid.NewGuid(),
                Breed = "Parrot",
                ColorPattern = "Green",
                Size = 0.8m,
                Age = 1,
                EstimatedPrice = 300.00m,
                HealthStatus = "Healthy",
                IsAvailable = true,
                OriginCountry = "Australia",
                Description = "Talkative and colorful bird.",
                ImageUrl = "https://example.com/parrot.jpg",
                AnimalTypeId = 3 // Giả sử AnimalTypeId = 3 là Bird
            }
        };

        await dbContext.Animals.AddRangeAsync(animals);
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedCareTasksAsync()
    {
        List<CareTask> careTasks = new()
        {
            new CareTask
            {
                TaskName = "Feed Dog",
                Description = "Provide daily meals for the dog.",
                Unit = "kg",
                Priority = "High",
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(1),
                AssignedTo = "John Doe",
                IsRecurring = true,
                Notes = "Feed the dog twice a day.",
            },
            new CareTask
            {
                TaskName = "Groom Cat",
                Description = "Regular grooming for the Persian cat.",
                Unit = null,
                Priority = "Medium",
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(7),
                AssignedTo = "Jane Smith",
                IsRecurring = true,
                Notes = "Groom the cat once a week.",
            },
            new CareTask
            {
                TaskName = "Clean Bird Cage",
                Description = "Ensure the parrot's cage is clean.",
                Unit = null,
                Priority = "Low",
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(2),
                AssignedTo = "Jim Brown",
                IsRecurring = false,
                Notes = "Use disinfectant to clean the cage.",
            }
        };

        await dbContext.CareTasks.AddRangeAsync(careTasks);
        await dbContext.SaveChangesAsync();
    }


    private async Task SeedDeliveryOrderDetailsAsync()
    {
        var rnd = new Random();
        var deliveryOrders = await dbContext.DeliveryOrders.ToListAsync();
        var animals = await dbContext.Animals.ToListAsync();

        var deliveryOrderDetails = new List<DeliveryOrderDetail>();

        foreach (var deliveryOrder in deliveryOrders)
        {
            // Giả sử mỗi đơn giao hàng có 2 chi tiết giao hàng
            for (int i = 0; i < 2; i++)
            {
                deliveryOrderDetails.Add(new DeliveryOrderDetail
                {
                    DeliveryOrderDetailId = Guid.NewGuid(),
                    AnimalId = animals[rnd.Next(animals.Count)].Id, // Chọn một con vật ngẫu nhiên
                    DeliveryOrderId = deliveryOrder.Id,
                    PreDeliveryHealthStatus = "Healthy", // Thông tin sức khỏe trước khi giao
                    PostDeliveryHealthStatus =
                        i % 2 == 0 ? "Healthy" : "Tired", // Thông tin sức khỏe sau khi giao (ngẫu nhiên)
                });
            }
        }

        await dbContext.DeliveryOrderDetails.AddRangeAsync(deliveryOrderDetails);
        await dbContext.SaveChangesAsync();
    }
}