using System;
using System.Collections.Generic;
using System.Text;
using AppContext = TrackingApp.Repository.AppContext;
namespace TrackingApp.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppContext _context;

        public UnitOfWork(AppContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
       
        public void SaveChanges() => _context.SaveChanges();
         
    }
}
