using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PasswordManager.Models;

namespace PasswordManager.Data
{
    public class PasswordManagerDataBase
    {
        readonly SQLiteAsyncConnection _database;
        public PasswordManagerDataBase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<AccountList>().Wait();
            _database.CreateTableAsync<Password>().Wait();
            _database.CreateTableAsync<ListPassword>().Wait();

        }
        public Task<int> SavePasswordAsync(Password password)
        {
            if (password.ID != 0)
            {
                return _database.UpdateAsync(password);
            }
            else
            {
                return _database.InsertAsync(password);
            }
        }
        public Task<int> DeletePasswordAsync(Password password)
        {
            return _database.DeleteAsync(password);
        }
        public Task<List<Password>> GetPasswordsAsync()
        {
            return _database.Table<Password>().ToListAsync();
        }


        public Task<List<AccountList>> GetAccountListsAsync()
        {
            return _database.Table<AccountList>().ToListAsync();
        }
        public Task<AccountList> GetAccountListAsync(int id)
        {
            return _database.Table<AccountList>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveAccountListAsync(AccountList alist)
        {
            if (alist.ID != 0)
            {
                return _database.UpdateAsync(alist);
            }
            else
            {
                return _database.InsertAsync(alist);
            }
        }
        public Task<int> DeleteAccountListAsync(AccountList alist)
        {
            return _database.DeleteAsync(alist);
        }
        public Task<int> SaveListPasswordAsync(ListPassword listp)
        {
            if (listp.ID != 0)
            {
                return _database.UpdateAsync(listp);
            }
            else
            {
                return _database.InsertAsync(listp);
            }
        }
        public Task<List<Password>> GetListPasswordsAsync(int accountlistid)
        {
            return _database.QueryAsync<Password>(
            "select P.ID, P.Description from Password P"
            + " inner join ListPassword LP"
            + " on P.ID = LP.PasswordID where LP.AccountListID = ?",
            accountlistid);
        }

    }
}
    


