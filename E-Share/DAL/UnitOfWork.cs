using E_Share.DAL.Repository;
using E_Share.Data;
using E_Share.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Share.DAL
{
    public class UnitOfWork
    {
        protected readonly ApplicationDbContext _context;

        private VehicleRepository _vehicleRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UnitOfWork(ApplicationDbContext context,
                          UserManager<ApplicationUser> userManager,
                          RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public VehicleRepository VehicleRepository
        {
            get
            {

                if (_vehicleRepository == null)
                {
                    _vehicleRepository = new VehicleRepository(_context);
                }
                return _vehicleRepository;
            }
        }

        private CityRepository _cityRepository;
        public CityRepository CityRepository
        {
            get
            {

                if (_cityRepository == null)
                {
                    _cityRepository = new CityRepository(_context);
                }
                return _cityRepository;
            }
        }

        private ProvinceRepository _provinceRepository;
        public ProvinceRepository ProvinceRepository
        {
            get
            {

                if (_provinceRepository == null)
                {
                    _provinceRepository = new ProvinceRepository(_context);
                }
                return _provinceRepository;
            }
        }

        private CategoryRepository _categoryRepository;
        public CategoryRepository CategoryRepository
        {
            get
            {

                if (_categoryRepository == null)
                {
                    _categoryRepository = new CategoryRepository(_context);
                }
                return _categoryRepository;
            }
        }

        private StatusRepository _statusRepository;
        public StatusRepository StatusRepository
        {
            get
            {

                if (_statusRepository == null)
                {
                    _statusRepository = new StatusRepository(_context);
                }
                return _statusRepository;
            }
        }

        private UserRepository _userRepository;
        public UserRepository UserRepository
        {
            get
            {

                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context, _userManager, _roleManager);
                }
                return _userRepository;
            }
        }

        private RideRepository _rideRepository;
        public RideRepository RideRepository
        {
            get
            {

                if (_rideRepository == null)
                {
                    _rideRepository = new RideRepository(_context);
                }
                return _rideRepository;
            }
        }

        private AvalailableRepository _avalailableRepository;
        public AvalailableRepository AvalailableRepository
        {
            get
            {

                if (_avalailableRepository == null)
                {
                    _avalailableRepository = new AvalailableRepository(_context);
                }
                return _avalailableRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
