using System;
using System.Collections.Generic;
using System.Text;

namespace TrackingApp.Services.UnitOfWork
{
    public interface IUnitOfWork
    {

        Task SaveChangesAsync();
        void SaveChanges();

    }
}
