using FlightManager.Data;
using FlightManager.Data.Entities;
using FlightManager.Data.Entities.Enums;
using FlightManager.Data.Entities.Identity;
using FlightManager.InputModels.Reservations;
using FlightManager.Interfaces;
using FlightManager.ViewModels.Reservations;
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
            Reservation reservation = new Reservation()
            {
                Email = createReservationInputModel.Email
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

            await this.emailSenderService.SendEmailAsync(createReservationInputModel.Email, "Reservation Confrmation", sb.ToString().Trim());
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
    }
}
