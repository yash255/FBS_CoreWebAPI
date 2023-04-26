﻿using FBS_CoreApi.Data;
using FlightBooking.DTOs;
using FlightBooking.Models;
using Microsoft.EntityFrameworkCore;




namespace FlightBooking.Repos
{
    public interface IAdminRepo
    {

        Task<IEnumerable<Flight>> GetFlights();

        Task<Flight> GetFlightById(int id);

        Task<Flight> CreateFlight(AdminFlightDto flightDto);

        Task UpdateFlight(int id, AdminFlightDto flightDto);

        Task DeleteFlight(int id);
    }



    public class AdminRepository : IAdminRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public AdminRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IEnumerable<Flight>> GetFlights()
        {



            return await _context.Flights.ToListAsync();


        }




        public async Task<Flight> GetFlightById(int id)
        {

            return await _context.Flights.FindAsync(id);

            // return await _context.Flights.FindAsync(id);
        }

        public async Task<Flight> CreateFlight(AdminFlightDto flightDto)
        {
            var flight = new Flight
            {
                FlightNumber = flightDto.FlightNumber,
                DepartureAirport = flightDto.DepartureAirport,
                ArrivalAirport = flightDto.ArrivalAirport,
                DepartureTime = flightDto.DepartureTime,
                ArrivalTime = flightDto.ArrivalTime,
                Price = flightDto.Price,

            };

            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();

            return flight;
        }


        public async Task UpdateFlight(int id, AdminFlightDto flightDto)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                throw new ArgumentException("Flight not found");
            }

            flight.FlightNumber = flightDto.FlightNumber;
            flight.DepartureAirport = flightDto.DepartureAirport;
            flight.ArrivalAirport = flightDto.ArrivalAirport;
            flight.DepartureTime = flightDto.DepartureTime;
            flight.ArrivalTime = flightDto.ArrivalTime;
            flight.Price = flightDto.Price;


            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlight(int id)
        {
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                throw new ArgumentException("Flight not found");
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }
}