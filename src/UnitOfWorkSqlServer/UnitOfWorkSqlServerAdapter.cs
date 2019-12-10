using KodotiSellsCommon;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using UinitOfworkInterface;

namespace UnitOfWorkSqlServer
{
    public class UnitOfWorkSqlServerAdapter : IUnitOfWorkAdapter
    {
        public SqlConnection _context { get; set; }
        public SqlTransaction _trantaction { get; set; }

        public IUnitOfWorkRepository Repository { get; set; }

        public UnitOfWorkSqlServerAdapter()
        {
            _context = new SqlConnection(Parameters.Connectionstring);
            _context.Open();

            _trantaction = _context.BeginTransaction();

            Repository = new UnitOfWorkSqlServerRepository(_context, _trantaction);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _trantaction.Dispose();

            }

            if (_context != null)
            {
                _context.Close();
                _context.Dispose();
            }

            Repository = null;
        }

      

        public void SaveChange()
        {
            _trantaction.Commit(); 
        }
    }
}
