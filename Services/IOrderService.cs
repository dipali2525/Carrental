using Carrental.Extensons;
using Carrental.Models;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Carrental.Services
{
    public interface IOrderService
    {
        bool Add(IEnumerable<OrderViewModel> orders);
        bool Add(OrderViewModel order, out string message);
        OrderViewModel GetOrder(int id);
        OrderViewModel GetEmptyOrder();
        IEnumerable<OrderViewModel> GetAllOrders();
        bool RemoveOrder(OrderViewModel order);
        bool ModifyOrder(OrderViewModel order);
        IEnumerable<CarViewModel> GetAllCars();
        CalendarViewModel GetCalendarData(SearchViewModel search);
    }
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarRepository _carRepository;

        public OrderService(IOrderRepository orderRepository, ICarRepository carRepository)
        {
            this._orderRepository = orderRepository;
            this._carRepository = carRepository;
        }
        public bool Add(IEnumerable<OrderViewModel> orders)
        {
            return _orderRepository.Add(orders);
        }

        public bool Add(OrderViewModel order, out string message)
        {
            message = string.Empty;
            if (IsCarAvailableForBooking(order))
            {
                return _orderRepository.Add(order);
            }
            message = "Car is not availalabe for booking for selected date";
            return false;
        }
        private bool IsCarAvailableForBooking(OrderViewModel order)
        {
            var orders = _orderRepository.FindByCarId(order.CarId);
            return !orders.Any(o => o.StartDate <= order.StartDate && o.EndDate >= order.EndDate);
        }
        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            var list = _orderRepository.GetAll();
            return list;
        }

        public OrderViewModel GetEmptyOrder()
        {
            var order = new OrderViewModel();
            order.Cars = _carRepository.GetAll();
            return order;
        }

        public OrderViewModel GetOrder(int id)
        {
            var order = _orderRepository.Find(id);
            order.Cars = _carRepository.GetAll();
            return order;
        }

        public bool ModifyOrder(OrderViewModel order)
        {
            return _orderRepository.Update(order);
        }

        public bool RemoveOrder(OrderViewModel order)
        {
            return _orderRepository.Delete(order);
        }

        public IEnumerable<CarViewModel> GetAllCars()
        {
            return _carRepository.GetAll();
        }

        public CalendarViewModel GetCalendarData(SearchViewModel search)
        {
            var calendar = new CalendarViewModel();
            var dates = search.StartDate.Range(search.EndDate).OrderBy(x => x);
            calendar.DateTimes = dates;
            var cars = _carRepository.FindByBrand(search.Brand);
            cars = cars.OrderBy(c => c.CarName);
            var orders = _orderRepository.FindByDateAndBrand(search.StartDate, search.EndDate, search.Brand);

            foreach (var car in cars)
            {
                var bookingRecords =  new List<CarBookingRecord>();
                foreach (var date in dates)
                {
                    var bookingRecord = new CarBookingRecord()
                    {
                        Car = car,
                        DateTime = date,
                        IsBooked = orders.Any(o => o.CarId == car.ID && o.StartDate <= date && o.EndDate >= date)
                    };
                    bookingRecords.Add(bookingRecord);
                }
                calendar.Records.TryAdd(car.ID, bookingRecords);
            }
            return calendar;
        }
    }
}
