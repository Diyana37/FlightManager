using FlightManager.Data.Entities.Identity;
using FlightManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using FlightManager.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FlightManager.Seeders
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
           ApplicationDbContext dbContext,
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            await SeedUsersAsync(userManager, roleManager);
            await SeedFlightsAsync(dbContext);
            await SeedReservationsAsync(dbContext);
        }

        private static async Task SeedUsersAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                IdentityRole admin = new IdentityRole(Constants.ADMINISTRATOR_ROLE);
                IdentityRole employee = new IdentityRole(Constants.EMPLOYEE_ROLE);

                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(employee);
            }
        }

        private static async Task SeedFlightsAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.Flights.AnyAsync())
            {
                return;
            }

            IEnumerable<Flight> flights = new List<Flight>()
            {
                new Flight
                {
                    From = "Sofia",
                    To = "Paris",
                    DepartureAt = new DateTime(2024, 07, 28, 17, 30, 0), // 28/07/2024 at 17:30:00
                    LandAt = new DateTime(2024, 07, 28, 19, 35, 0), // 28/07/2024 at 19:35:00
                    CapacityBusiness = 30,
                    CapacityStandard = 100,
                    PilotName = "Ivan Ivanov",
                    Type = "One-Way Flight",
                    UniqieId = "#SOFPA8695"
                },
                new Flight
                {
                    From = "New York",
                    To = "London",
                    DepartureAt = new DateTime(2024, 06, 15, 22, 00, 0), // 15/06/2024 at 22:00:00
                    LandAt = new DateTime(2024, 06, 16, 10, 15, 0), // 16/06/2024 at 10:15:00
                    CapacityBusiness = 40,
                    CapacityStandard = 200,
                    PilotName = "John Smith",
                    Type = "Round-Trip Flight",
                    UniqieId = "#NYL2024"
                },
                new Flight
                {
                    From = "Tokyo",
                    To = "Los Angeles",
                    DepartureAt = new DateTime(2024, 08, 05, 7, 30, 0), // 05/08/2024 at 07:30:00
                    LandAt = new DateTime(2024, 08, 05, 14, 45, 0), // 05/08/2024 at 14:45:00
                    CapacityBusiness = 50,
                    CapacityStandard = 250,
                    PilotName = "Taro Tanaka",
                    Type = "One-Way Flight",
                    UniqieId = "#TLA5589"
                },
                new Flight
                {
                    From = "Berlin",
                    To = "Rome",
                    DepartureAt = new DateTime(2024, 09, 12, 09, 15, 0), // 12/09/2024 at 09:15:00
                    LandAt = new DateTime(2024, 09, 12, 11, 05, 0), // 12/09/2024 at 11:05:00
                    CapacityBusiness = 20,
                    CapacityStandard = 150,
                    PilotName = "Hans Müller",
                    Type = "One-Way Flight",
                    UniqieId = "#BRR0912"
                },
                new Flight
                {
                    From = "Los Angeles",
                    To = "Sydney",
                    DepartureAt = new DateTime(2024, 07, 10, 23, 45, 0), // 10/07/2024 at 23:45:00
                    LandAt = new DateTime(2024, 07, 11, 07, 30, 0), // 11/07/2024 at 07:30:00 
                    CapacityBusiness = 35,
                    CapacityStandard = 220,
                    PilotName = "Michael Johnson",
                    Type = "One-Way Flight",
                    UniqieId = "#LASYD745"
                },
                new Flight
                {
                    From = "Dubai",
                    To = "Singapore",
                    DepartureAt = new DateTime(2024, 08, 22, 21, 00, 0), // 22/08/2024 at 21:00:00
                    LandAt = new DateTime(2024, 08, 23, 09, 15, 0), // 23/08/2024 at 09:15:00
                    CapacityBusiness = 45,
                    CapacityStandard = 260,
                    PilotName = "Ahmed Hassan",
                    Type = "Round-Trip Flight",
                    UniqieId = "#DXBSIN899"
                },
                new Flight
                {
                    From = "Toronto",
                    To = "Miami",
                    DepartureAt = new DateTime(2024, 09, 05, 08, 30, 0), // 05/09/2024 at 08:30:00
                    LandAt = new DateTime(2024, 09, 05, 12, 15, 0), // 05/09/2024 at 12:15:00
                    CapacityBusiness = 25,
                    CapacityStandard = 180,
                    PilotName = "Robert Martinez",
                    Type = "One-Way Flight",
                    UniqieId = "#TORMIA450"
                },
                new Flight
                {
                    From = "Madrid",
                    To = "Amsterdam",
                    DepartureAt = new DateTime(2024, 10, 14, 16, 20, 0), // 14/10/2024 at 16:20:00
                    LandAt = new DateTime(2024, 10, 14, 18, 45, 0), // 14/10/2024 at 18:45:00
                    CapacityBusiness = 20,
                    CapacityStandard = 150,
                    PilotName = "Carlos Fernandez",
                    Type = "One-Way Flight",
                    UniqieId = "#MADAMS032"
                },
                new Flight
                {
                    From = "Beijing",
                    To = "Bangkok",
                    DepartureAt = new DateTime(2024, 11, 03, 11, 10, 0), // 03/11/2024 at 11:10:00
                    LandAt = new DateTime(2024, 11, 03, 15, 30, 0), // 03/11/2024 at 15:30:00
                    CapacityBusiness = 30,
                    CapacityStandard = 200,
                    PilotName = "Wei Zhang",
                    Type = "One-Way Flight",
                    UniqieId = "#BJKBKK662"
                }
        };

            await dbContext.Flights.AddRangeAsync(flights);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedReservationsAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.Reservations.AnyAsync())
            {
                return;
            }

            Flight flight = await dbContext.Flights.FirstOrDefaultAsync(f => f.UniqieId == "#SOFPA8695");
            Flight flight1 = await dbContext.Flights.FirstOrDefaultAsync(f => f.UniqieId == "#NYL2024");
            Flight flight2 = await dbContext.Flights.FirstOrDefaultAsync(f => f.UniqieId == "#TLA5589");
            Flight flight3 = await dbContext.Flights.FirstOrDefaultAsync(f => f.UniqieId == "#LASYD745");
            Flight flight4 = await dbContext.Flights.FirstOrDefaultAsync(f => f.UniqieId == "#DXBSIN899");

            IEnumerable<Reservation> reservations = new List<Reservation>()
            {
                new Reservation
                {
                    Email = "petar@gmail.com",
                    FlightId = flight.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "0646305678",
                            FirstName = "Petar",
                            LastName = "Petrov",
                            MiddleName = "Georgiev",
                            Nationality = "Bulgarian",
                            Phone = "+359881234567",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        },
                        new Passenger
                        {
                            EGN = "0546300987",
                            FirstName = "Ivana",
                            LastName = "Ivanova",
                            MiddleName = "Georgieva",
                            Nationality = "Bulgarian",
                            Phone = "+359887654321",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        },
                    }
                },
                new Reservation
                {
                    Email = "john.doe@example.com",
                    FlightId = flight1.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "1234567890",
                            FirstName = "John",
                            LastName = "Doe",
                            MiddleName = "Michael",
                            Nationality = "American",
                            Phone = "+1 555-123-4567",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        },
                        new Passenger
                        {
                            EGN = "0987654321",
                            FirstName = "Jane",
                            LastName = "Doe",
                            MiddleName = "Elizabeth",
                            Nationality = "American",
                            Phone = "+1 555-987-6543",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                },
                new Reservation
                {
                    Email = "tanaka.taro@example.com",
                    FlightId = flight2.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "2345678901",
                            FirstName = "Taro",
                            LastName = "Tanaka",
                            MiddleName = "Hiroshi",
                            Nationality = "Japanese",
                            Phone = "+81 90-1234-5678",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        }
                    }
                },
                new Reservation
                {
                    Email = "maria.gonzalez@example.com",
                    FlightId = flight1.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "3456789012",
                            FirstName = "Maria",
                            LastName = "Gonzalez",
                            MiddleName = "Fernanda",
                            Nationality = "Spanish",
                            Phone = "+34 600-987-654",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "4567890123",
                            FirstName = "Carlos",
                            LastName = "Martinez",
                            MiddleName = "Luis",
                            Nationality = "Spanish",
                            Phone = "+34 611-223-334",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                },
                new Reservation
                {
                    Email = "john.smith@example.com",
                    FlightId = flight3.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "0123456789",
                            FirstName = "John",
                            LastName = "Smith",
                            MiddleName = "Smith",
                            Nationality = "American",
                            Phone = "+1 765-123-5678",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "0987654321",
                            FirstName = "Maria",
                            LastName = "Smith",
                            MiddleName = "Elizabeth",
                            Nationality = "American",
                            Phone = "+1 983-987-7211",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                },
                new Reservation
                {
                    Email = "mia.maples@example.com",
                    FlightId = flight4.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "2345678901",
                            FirstName = "Mia",
                            LastName = "Maples",
                            MiddleName = "Smith",
                            Nationality = "Canadian",
                            Phone = "+81 90-1234-5678",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        }
                    }
                },
                new Reservation
                {
                    Email = "maria.georgieva@example.com",
                    FlightId = flight4.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "5678901234",
                            FirstName = "Maria",
                            LastName = "Georgieva",
                            MiddleName = "Ivanova",
                            Nationality = "Bulgarian",
                            Phone = "+359881234567",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "4567890123",
                            FirstName = "Carlos",
                            LastName = "Martinez",
                            MiddleName = "Luis",
                            Nationality = "Spanish",
                            Phone = "+34 611-223-334",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                },
                new Reservation
                {
                    Email = "smith.family@example.com",
                    FlightId = flight4.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "5678901234",
                            FirstName = "David",
                            LastName = "Smith",
                            MiddleName = "Edward",
                            Nationality = "Australian",
                            Phone = "+61 400-567-890",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        },
                        new Passenger
                        {
                            EGN = "6789012345",
                            FirstName = "Emma",
                            LastName = "Smith",
                            MiddleName = "Grace",
                            Nationality = "Australian",
                            Phone = "+61 401-234-567",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "7890123456",
                            FirstName = "Liam",
                            LastName = "Smith",
                            MiddleName = "James",
                            Nationality = "Australian",
                            Phone = "+61 402-345-678",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                },
                new Reservation
                {
                    Email = "wong.family@example.com",
                    FlightId = flight.Id,
                    Passengers = new List<Passenger>()
                    {
                        new Passenger
                        {
                            EGN = "8901234567",
                            FirstName = "Henry",
                            LastName = "Wong",
                            MiddleName = "Kai",
                            Nationality = "Singaporean",
                            Phone = "+65 8123-4567",
                            TicketType = Data.Entities.Enums.TicketType.BUSINESS
                        },
                        new Passenger
                        {
                            EGN = "9012345678",
                            FirstName = "Linda",
                            LastName = "Wong",
                            MiddleName = "Mei",
                            Nationality = "Singaporean",
                            Phone = "+65 8234-5678",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "0123456789",
                            FirstName = "Kevin",
                            LastName = "Wong",
                            MiddleName = "Jun",
                            Nationality = "Singaporean",
                            Phone = "+65 8345-6789",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        },
                        new Passenger
                        {
                            EGN = "1234509876",
                            FirstName = "Sophia",
                            LastName = "Wong",
                            MiddleName = "Xin",
                            Nationality = "Singaporean",
                            Phone = "+65 8456-7890",
                            TicketType = Data.Entities.Enums.TicketType.STANDARD
                        }
                    }
                }
            };

            await dbContext.Reservations.AddRangeAsync(reservations);
            await dbContext.SaveChangesAsync();
        }

    }
}
