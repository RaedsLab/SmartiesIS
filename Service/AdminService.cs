using Data.Infrastructure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminService : IAdminService
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        IUnitOfWork utOfWork = new UnitOfWork(dbFactory);
        ////////////

        public IEnumerable<Admin> GetAllAdmins()
        {
            var admins = utOfWork.AdminRepository.GetAll();
            return admins;
        }
        public IEnumerable<Admin> GetSuperAdmins()
        {
            var admins = utOfWork.AdminRepository.GetMany(a => a.IsVendor == false);
            return admins;
        }
        public IEnumerable<Admin> GetVendors()
        {
            var admins = utOfWork.AdminRepository.GetMany(a => a.IsVendor == true);
            return admins;
        }
        public Admin GetAdminById(int id)
        {
            var admin = utOfWork.AdminRepository.GetById(id);
            return admin;
        }

        public bool IsSuperAdmin(int id)
        {
            bool res = true;
            var admin = utOfWork.AdminRepository.GetById(id);
            if (admin.IsVendor.HasValue)
            {
                res = (bool)admin.IsVendor;
            }
            return !res;
        }
        public Admin LogAdmin(string email, string pwd)
        {
            var admin = utOfWork.AdminRepository.Get(a => (a.Email.Equals(email) && a.Password.Equals(pwd)) && a.IsVendor==false);
            return admin; // return null if dosn't exist
        }

        public Admin LogVendor(string email, string pwd)
        {
            var admin = utOfWork.AdminRepository.Get(a => (a.Email.Equals(email) && a.Password.Equals(pwd)) && a.IsVendor == true);
            return admin; // return null if dosn't exist
        }


    }

    /// 
    public interface IAdminService
    {
        IEnumerable<Admin> GetAllAdmins();
        IEnumerable<Admin> GetSuperAdmins();
        IEnumerable<Admin> GetVendors();
        Admin GetAdminById(int id);
        bool IsSuperAdmin(int id);
        Admin LogAdmin(string email, string pwd);
        Admin LogVendor(string email, string pwd);

    }
}
