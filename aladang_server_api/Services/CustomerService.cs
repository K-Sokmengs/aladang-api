using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using aladang_server_api.Models.BO.Res;
using ApiLotterySystem.Exceptions;

namespace aladang_server_api.Services
{
	

    public interface CustomerService
    {
        public List<Customer> GetAll();
        public List<Customer> GetCustomers(int page); 
        public List<Customer> GetCustomerByShop(int shopid);
        public double Count();
        public double Count(int shopid);
        public double PageCount();
        public double PageCount(int shopid);

        public CustomerRes GetById(int id);
        public Customer Update(Customer obj);
        public Customer Create(CustomerReq req);
        public Customer CreateV(CustomerReq req);
        public CustomerRes UpdateCustomerChangePwd(UserChangePasswordReq req);
        public CustomerRes ResetCustomerPassword(ResetCustomerPassword req); 
        public CustomerRes ChangeImageProfile( UpdatePhotoUser req);


    }
    public class CustomerServiceImpl : CustomerService
    {
        private AppDBContext _contex;
        private IConfiguration _configuration;
        double pageResult = 10;
        private CustomerService _customerServiceImplementation;

        public CustomerServiceImpl(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration= configuration;  
        }

        public double Count()
        {
            return _contex.customers!.Count();
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.customers!.Count() / pageResult)!;
        }

        public double Count(int shopid)
        {
            return _contex.customers!.Count();
        }

        public double PageCount(int shopid)
        {
            return Math.Ceiling((double)_contex.customers!.Count() / pageResult)!;
        }


        public List<Customer> GetCustomerByShop(int shopid)
        {
            if (shopid != 0)
            {
                var customers = _contex.customers!
                    .OrderByDescending(d => d.id) 
                    .ToList();
                return customers!;
            }

            return null!;
        }

        public Customer Create(CustomerReq req)
        {
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);
            
            Customer newItem = new Customer();
            newItem.password = req.password!;
            newItem.date = DateTime.Today;
            newItem.setData(req);
            _contex.Add(newItem);
            _contex.SaveChanges();
            var findItem = _contex.customers!.OrderByDescending(u => u.id).Where(u => u.customerName == req.customerName && u.phone==req.phone).FirstOrDefault();
            return findItem!;

        }

        public Customer CreateV(CustomerReq req)
        {
            var customer = _contex.customers?.FirstOrDefault(s => s.phone == req.phone);
            if(customer != null)
            {
                throw new AppException
                {
                    ErrorCode = "400",
                    Message = "Phone number is register already exist",
                    MessageKh = "Phone number is register already exist",
                    HttpStatus = 200
                };
            }
            Customer newItem = new Customer();
            newItem.password = req.password!;
            newItem.date = DateTime.Today;
            newItem.setData(req);
            _contex.Add(newItem);
            _contex.SaveChanges();
            var findItem = _contex.customers!.OrderByDescending(u => u.id).Where(u => u.customerName == req.customerName && u.phone==req.phone).FirstOrDefault();
            return findItem!;
            
        }


        public List<Customer> GetAll()
        {
            var customers = _contex.customers!.OrderByDescending(d => d.id) .ToList();
            if (customers != null)
            {
                return customers!;
            } 
            return null!;
        }

        public List<Customer> GetCustomers(int page)
        {
            if (page != 0)
            {
                var customers = _contex.customers!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return customers!;
            }

            return null!;
        }

        public CustomerRes GetById(int id)
        {
            Customer temItems = _contex.customers!.FirstOrDefault(c => c.id == id)!;
            if (temItems != null)
            {
                CustomerRes newTemItem = new CustomerRes();
                newTemItem.setData(temItems);
                return newTemItem;
            }
            return null!;
        }


        public Customer Update(Customer obj)
        {
            Customer temItems = _contex.customers!.FirstOrDefault(c => c.id == obj.id)!;

            temItems.date = obj.date;
            temItems.phone = obj.phone;
            temItems.tokenid = obj.tokenid;
            temItems.currentLocation = obj.currentLocation;
            temItems.customerName = obj.customerName;
            temItems.gender = obj.gender;
            temItems.imageProfile = obj.imageProfile;
            _contex.Update(temItems);
            _contex.SaveChanges();
            //Customer temItems1 = _contex.customers!.Where(c => c.id == obj.id).FirstOrDefault()!;
            //if (temItems1 != null)
            //{
            //    return temItems1;
            //}
            return temItems!;

        }

        public CustomerRes UpdateCustomerChangePwd(UserChangePasswordReq req)
        {
            Customer customer = _contex.customers.SingleOrDefault(c => c.id == req.userId && c.password == req.currentPassword);
            if (customer != null)
            {
                customer.password = req.newPassword;
                _contex.Update(customer);
                _contex.SaveChanges();
                CustomerRes customerRes = new CustomerRes();
                customerRes.setData(customer);
                return customerRes;
            }
            return null;
        }


        public CustomerRes ResetCustomerPassword(ResetCustomerPassword req)
        {
            var pwdHas = Encrypt.EncriptSha256PassWord(req.newPassword!);
            Customer resultCheck = _contex.customers!.FirstOrDefault(c => c.phone == req.phone && c.customerName==req.customerName)!;
            if (resultCheck != null)
            {
                resultCheck.password = pwdHas;
                _contex.SaveChanges();
                CustomerRes customerRes = new CustomerRes(); 
                customerRes.setData(resultCheck);
                return customerRes;
            }
            return null!; 
        }

        public CustomerRes ChangeImageProfile(UpdatePhotoUser req)
        {
            var customer =  _contex.customers!.FirstOrDefault(u => u.id == req.customerid);
            if (customer != null)
            {
                customer.imageProfile = req.imageNew;
            }
            _contex.SaveChanges();
            CustomerRes customerRes = new CustomerRes(); 
            customerRes.setData(customer!); 
            return customerRes;
        }

    }
}

