using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Entities.Enums;
using FlightManager.Data.Entities.Identity;
using FlightManager.InputModels.Reservations;
using FlightManager.Interfaces;
using FlightManager.ViewModels.Flights;
using FlightManager.ViewModels.Passengers;
using FlightManager.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FlightManager.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IEmailSenderService emailSenderService;

        public ReservationsService(ApplicationDbContext dbContext, IEmailSenderService emailSenderService)
        {
            this.dbContext = dbContext;
            this.emailSenderService = emailSenderService;
        }
        public async Task CreateAsync(CreateReservationInputModel createReservationInputModel)
        {
            using var transaction = await this.dbContext.Database.BeginTransactionAsync();

            try
            {
                int occupiedBusinessCount = await this.dbContext.Reservations
                    .Include(r => r.Flight)
                    .Where(r => r.FlightId == createReservationInputModel.FlightId)
                    .SumAsync(r => r.Flight.CapacityBusiness);

                int occupiedStandardCount = await this.dbContext.Reservations
                    .Include(r => r.Flight)
                    .Where(r => r.FlightId == createReservationInputModel.FlightId)
                    .SumAsync(r => r.Flight.CapacityStandard);

                int currentBusinessCount = createReservationInputModel.Passengers
                    .Count(p => p.TicketType == "BUSINESS");

                int currentStandardCount = createReservationInputModel.Passengers
                  .Count(p => p.TicketType == "STANDARD");

                Flight flight = await this.dbContext.Flights
                    .FirstOrDefaultAsync(f => f.Id == createReservationInputModel.FlightId);

                if (currentBusinessCount + occupiedBusinessCount > flight.CapacityBusiness)
                {
                    throw new InvalidOperationException("Not enough business seats!");
                }

                if (currentStandardCount + occupiedStandardCount > flight.CapacityStandard)
                {
                    throw new InvalidOperationException("Not enough standard seats!");
                }

                Reservation reservation = new Reservation()
                {
                    Email = createReservationInputModel.Email,
                    FlightId = createReservationInputModel.FlightId
                };

                await this.dbContext.Reservations.AddAsync(reservation);
                await this.dbContext.SaveChangesAsync();

                StringBuilder sb = new StringBuilder();

                foreach (var passenger in createReservationInputModel.Passengers)
                {
                    Passenger passenger1 = new Passenger()
                    {
                        EGN = passenger.EGN,
                        FirstName = passenger.FirstName,
                        LastName = passenger.LastName,
                        MiddleName = passenger.MiddleName,
                        Nationality = passenger.Nationality,
                        Phone = passenger.Phone,
                        TicketType = (TicketType)Enum.Parse(typeof(TicketType), passenger.TicketType),
                        ReservationId = reservation.Id
                    };

                    await this.dbContext.Passengers.AddAsync(passenger1);

                    sb.AppendLine($"{passenger.FirstName} {passenger.MiddleName} {passenger.LastName}");
                    sb.AppendLine($"{passenger.EGN} {passenger.Nationality} {passenger.Phone}");
                    sb.AppendLine($"Ticket type - {passenger.TicketType}");
                }

                await this.dbContext.SaveChangesAsync();

                await this.emailSenderService.SendEmailAsync
                    (createReservationInputModel.Email, "Reservation Confrmation", sb.ToString().Trim());

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<IEnumerable<ReservationBasicViewModel>> GetAllAsync()
        {
            IEnumerable<ReservationBasicViewModel> reservations = await this.dbContext.Reservations
                .Select(r => new ReservationBasicViewModel
                {
                    Id = r.Id,
                    Email = r.Email
                })
                .ToListAsync();

            return reservations;
        }

        public async Task<ReservationDetailsViewModel> GetDetailsAsync(int id)
        {
            ReservationDetailsViewModel reservationDetailsViewModel = await this.dbContext.Reservations
                .Include(r => r.Passengers)
                .Include(r => r.Flight)
                .Where(r => r.Id == id)
                .Select(r => new ReservationDetailsViewModel
                {
                    Id = r.Id,
                    Email = r.Email,
                    FlightId = r.FlightId,
                    Flight = r.Flight,
                    Passengers = r.Passengers
                    .Select(f => new PassengerViewModel
                    {
                        Id = f.Id,
                        FirstName = f.FirstName,
                        MiddleName = f.MiddleName,
                        LastName = f.LastName,
                        EGN = f.EGN,
                        Nationality = f.Nationality,
                        Phone = f.Phone,
                        TicketType = f.TicketType,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return reservationDetailsViewModel;
        }
    }
}
