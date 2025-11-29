using System;
using aladang_server_api.Configuration;
using aladang_server_api.Helpers;
using aladang_server_api.Models;
using aladang_server_api.Models.BO.Req;
using ApiLotterySystem.Exceptions;
using Microsoft.AspNetCore.Mvc;
using aladang_server_api.Models.BO.Res;

namespace aladang_server_api.Services
{
	public interface ShopService
	{
        public List<Shop> GetAll(); 
        public List<Shop> GetShops(int page);
        public List<Shop> GetOtherShop(int id, int page);

        public double Count();
        public double CountOtherShop(int id);
        public double Count(string status);
        public double PageCount();
        public double PageCountOtherShop(int id);
        public double PageCount(string status);

        public Shop GetById(int id);
        public List<Shop> GetByStatus(string status,int page);
        public Shop Update(Shop req);
        public Shop CreateNew(Shop req);

        public Shop changePasswordShop(ChangePasswordShop req);
        public Shop resetPasswordShop(ResetPasswordShop req);
        public Shop changeLogoShop(ChangeLogoShop req);
        public Shop changeLogoQRCodeImage(ChangeLogoQRCode req);
        public Shop CreateNewV1(Shop req) ;


    }

    public class ShopServiceImp : ShopService
    {
        private readonly IConfiguration _configuration;
        private string defaultUrlNoImg = "";
        private string basicUrlImg = "", appDomain = "";
        private AppDBContext _contex;
        double pageResult = 10;
        public ShopServiceImp(AppDBContext dbConnection, IConfiguration configuration)
        {
            _contex = dbConnection;
            _configuration = configuration;
            appDomain = _configuration.GetSection("Application:AppDomain").Value;
            basicUrlImg = appDomain + ProviderConnector.Image;
            defaultUrlNoImg = appDomain + ProviderConnector.ImageDefaultNoImg;
        }

        public double Count()
        {
            return _contex.shops!.Count();
        }

        public double CountOtherShop(int id)
        {
            return _contex.shops!.Where(s=>s.id!=id).Count();
        }

        public double Count(string status)
        {
            return _contex.shops!.Where(s => s.status == status).Count();
        }


        public double PageCountOtherShop(int id)
        {
            return Math.Ceiling((double)_contex.shops!.Where(s=>s.id!=id).Count() / pageResult)!;
        }

        public double PageCount()
        {
            return Math.Ceiling((double)_contex.shops!.Count() / pageResult)!;
        }

        public double PageCount(string status)
        {
            return Math.Ceiling((double)_contex.shops!.Where(s => s.status==status).Count() / pageResult)!;
        }

        public List<Shop> GetAll()
        {
            var shops = _contex.shops!
                    .OrderByDescending(d => d.id)
                    .ToList();
            if (shops != null)
            { 
                return shops!;
            }
            return null!;
        }
        public List<Shop> GetShops(int page)
        {
            if (page != 0)
            {
                var shops = _contex.shops!
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return shops!;
            }

            return null!;
        }

        public List<Shop> GetOtherShop(int shopid,int page)
        {
            if (page != 0)
            {
                var shops = _contex.shops!
                    .Where(s => s.id != shopid)
                    .OrderByDescending(d => d.id) 
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    .ToList();
                return shops!;
            }

            return null!;
        }

        //Public List<ShopRes> GetShops()
        //{
        //    List<Shop> temItemList = _contex.shops!.OrderByDescending(d => d.id).ToList();
        //    List<ShopRes> list = new List<ShopRes>();
        //    if (temItemList != null)
        //    {
        //        foreach (Shop item in temItemList)
        //        {
        //            ShopRes temItem = new ShopRes();
        //            temItem.setData(item);
        //            list.Add(temItem);
        //        }
        //        return list;
        //    }
        //    return null!;

        //}

        public Shop GetById(int id)
        {
            var shop = _contex.shops!.Where(l => l.id == id).SingleOrDefault()!;
            if (shop != null)
            {
                return shop;
            }
            return null!;

        }

       

        public List<Shop> GetByStatus(string status,int page)
        {
            if (page != 0)
            {
                var shops = _contex.shops!
                    .Where(s => s.status == status)
                    .OrderByDescending(d => d.id)
                    .Skip((page - 1) * (int)pageResult)
                    .Take((int)pageResult)
                    
                    .ToList();
                return shops!;
            }

            //List<Shop> temItemList = _contex.shops!.Where(s=>s.status==status).ToList();
            //List<ShopRes> list = new List<ShopRes>();
            //if (temItemList != null)
            //{
            //    foreach (Shop item in temItemList)
            //    {
            //        ShopRes temItem = new ShopRes();
            //        temItem.setData(item);
            //        list.Add(temItem);
            //    }
            //    return temItemList;
            //}
            return null!;

        }

        public Shop CreateNew(Shop req)
        {
            var shop = _contex.shops!.SingleOrDefault(s => s.phone == req.phone);
            if(shop == null)
            {
                //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);
                req.password = req.password;
                _contex.Add(req);
                _contex.SaveChanges();
                var result = _contex.shops!.Where(u => u.id == req.id).FirstOrDefault()!;
                return result;
            }

            return null!;
        }

        public Shop Update(Shop req)
        {
            var check = _contex.shops!.SingleOrDefault(s => s.id == req.id);
            if (check != null)
            {
                //var pwdHas = Encrypt.EncriptSha256PassWord(req.password!);
                Shop shop = _contex.shops!.FirstOrDefault(c => c.id == req.id)!;
                shop.shopid = req.shopid;
                shop.shopName = req.shopName;
                shop.gender = req.gender;
                shop.dob = req.dob;
                shop.nationality = req.nationality;
                shop.ownerName = req.ownerName;
                shop.phone = req.phone;
                shop.password = req.password;
                shop.tokenid = req.tokenid;
                shop.facebookPage = req.facebookPage;
                shop.location = req.location;
                shop.logoShop = req.logoShop;
                shop.paymentType = req.paymentType;
                shop.qrCodeImage = req.qrCodeImage;
                shop.bankNameid = req.bankNameid;
                shop.accountNumber = req.accountNumber;
                shop.accountName = req.accountName;
                shop.feetype = req.feetype;
                shop.feecharge = req.feecharge;
                shop.shophistorydate = req.shophistorydate;
                shop.note = req.note;
                shop.status = req.status;
                shop.idcard = req.idcard;
                shop.expiredate = req.expiredate;
                _contex.Update(shop);
                _contex.SaveChanges();
                Shop result = _contex.shops!.Where(c => c.id == req.id).FirstOrDefault()!;
                if (result != null)
                {
                    return result;
                }
            }
            
            return null!;

        }
        
        public Shop changePasswordShop(ChangePasswordShop req)
        {
            Shop shop = _contex.shops!.SingleOrDefault(c => c.id == req.shopid && c.phone == req.phone && c.password == req.currentPassword!);
            if (shop != null)
            {
                shop.password = req.newPassword!;
                _contex.Update(shop);
                _contex.SaveChanges();
                return shop!;
            }
            return null!;
        }

        public Shop resetPasswordShop(ResetPasswordShop req)
        {
            //var pwdHas = Encrypt.EncriptSha256PassWord(req.newPassword!);
            var pwdHas = req.newPassword!;
            Shop shop = _contex.shops!.SingleOrDefault(c => c.phone!.Replace(" ","") == req.phone!.Replace(" ","") && c.shopName==req.shopName && c.expiredate==req.expiredate && c.idcard==req.idcard)!;

            if (shop != null)
            {
                shop.password = pwdHas;
            }
            _contex.Update(shop);
            _contex.SaveChanges();
            Shop result = _contex.shops!.Where(c => c.id == shop!.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }


        public Shop changeLogoShop(ChangeLogoShop req)
        {

            Shop shop = _contex.shops!.SingleOrDefault(c => c.id == req.shopid)!;

            if (shop != null)
            {
                shop.logoShop = req.newlogo;
            }
            _contex.Update(shop);
            _contex.SaveChanges();

            Shop result = _contex.shops!.Where(c => c.id == shop!.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }

        public Shop changeLogoQRCodeImage(ChangeLogoQRCode req)
        {

            Shop shop = _contex.shops!.SingleOrDefault(c => c.id == req.shopid)!;

            if (shop != null)
            {
                shop.qrCodeImage = req.newqr;
            }
            _contex.Update(shop);
            _contex.SaveChanges();
            Shop result = _contex.shops!.Where(c => c.id == shop!.id).FirstOrDefault()!;
            if (result != null)
            {
                return result;
            }
            return null!;

        }

        public Shop CreateNewV1(Shop req)
        {
            var shop = _contex.shops!.SingleOrDefault(s => s.phone == req.phone);
            if(shop != null)
            {
                throw new AppException
                {
                    ErrorCode = "400",
                    Message = "Phone number is register already exist",
                    MessageKh = "Phone number is register already exist",
                    HttpStatus = 200
                };
            }
            req.password = req.password;
            _contex.Add(req);
            _contex.SaveChanges();
            return req;
        }
    }
}

