using Core.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Constrant
{
   public static class Messages
    {
        public static string Added="Araç Eklendi";
        public static string ErrorMin = "Araç ismi en az iki karakter olmalıdır";
        public static string Deleted = "Araç Silindi";
        public static string Updated = "Araç Güncellendi";
        public static string CarsListed = "Araçlar Listelendi";
        public static string Listed = "Listelendi";
        public static string CarGet = "Araç Getirildi";
        public static string CarNotReturn = "Araç Teslim Edilmedi";
        public static string CarImageLimitExceeded = "Araç başına resim sayısı";
        public static string ImageAdded = "Resim Eklendi";
        public static string ImageDeleted = "Resim Silindi";
        public static string ImageUpdated = "Resim Güncellendi";
        public static string UserNotFound = "Kullanıcı bullunamadı";
        public static string CustomersListed = "Müşteriler listelendi";
        public static string PasswordWrong = "Şifre yanlış";
        public static string LoginSuccess = "Giriş Başarılı";
        public static string UserAlreadyExist = "Böyle bir kullanıcı zaten mevcut";
        public static string UserAdded = "Kullanıcı eklendi";
        public static string AccessTokenCreated = "Token Başarıyla Oluşturuldu";
    }
}
