using lego.model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lego.Logic
{
    internal class LegoUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IRepository<LegoColor> _legoColorRepository;
        private IRepository<LegoInventory> _legoInventoryRepository;
        private IRepository<LegoInventoryPart> _legoInventoryPartRepository;
        private IRepository<LegoInventorySet> _legoInventorySetRepository;
        private IRepository<LegoPart> _legoPartRepository;
        private IRepository<LegoPartCategory> _legoPartCategoryRepository;
        private IRepository<LegoSet> _legoSetRepository;
        private IRepository<LegoTheme> _legoThemeRepository;

        private IDbContextTransaction? _transaction;

        public LegoUnitOfWork(DbContext context)
        { 
            _context = context;
            _legoColorRepository = new LegoRepository<LegoColor>(_context);
            _legoInventoryRepository = new LegoRepository<LegoInventory>(_context);
            _legoInventoryPartRepository = new LegoRepository<LegoInventoryPart>(_context);
            _legoInventorySetRepository = new LegoRepository<LegoInventorySet>(_context);
            _legoPartRepository = new LegoRepository<LegoPart>(_context);
            _legoPartCategoryRepository = new LegoRepository<LegoPartCategory>(_context);
            _legoSetRepository = new LegoRepository<LegoSet>(_context);
            _legoThemeRepository = new LegoRepository<LegoTheme>(_context);

        }

        public IEnumerable<LegoColor> GetLegoColors()
        {
            return _legoColorRepository.GetAll();
        }

        public void AddLegoColor(LegoColor legoColor, bool isTransaction = false)
        {
            _legoColorRepository.Add(legoColor);
            if (!isTransaction)
            {
                SaveChanges();
            }
        }
        public void BeginTransaction()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null)
                {
                    _context.SaveChanges();
                    _transaction?.Commit();
                    _transaction?.Dispose();
                    _transaction = null;
                }
            }
            catch (Exception)
            {
                Rollback();
            }
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction?.Rollback();
                _transaction?.Dispose();
                _transaction = null;
            }

        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
