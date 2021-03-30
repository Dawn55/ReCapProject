using Buisness.Abstract;
using Buisness.Constrant;
using Core.Aspect.Autofac.Validation;
using Core.Helpers;
using Core.Utilities;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Buisness.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDAL;

        public CarImageManager(ICarImageDal carImageDAL)
        {
            _carImageDAL = carImageDAL;
        }
        public IResult Add(IFormFile file, CarImage carImage)
        {
            BusinessRules.Run(CheckImageLimitForCar(carImage));
            carImage.ImagePath =  FileHelper.Add(file);
            carImage.Date = DateTime.Now;
            _carImageDAL.Add(carImage);
            return new SuccessResult(Messages.ImageAdded);
        }
        public IResult Delete(CarImage carImage)
        {
           var result = _carImageDAL.Get(p=>p.Id == carImage.Id);
            _carImageDAL.Delete(result);
            return new SuccessResult(Messages.ImageDeleted);
        }
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.Update(_carImageDAL.Get(p=>p.Id == carImage.Id).ImagePath,file);
            carImage.Date = DateTime.Now;
            _carImageDAL.Add(carImage);
            return new SuccessResult(Messages.ImageUpdated);
        }
        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDAL.Get(p => p.Id == id));
        }
        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDAL.GetAll());
        }
        public IDataResult<List<CarImage>> GetImagesByCarId(int id)
        {
            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(id));
        }


        //business rules

        private List<CarImage> CheckIfCarImageNull(int id)
        {
            string path = @"\Images\logo.jpg";
            var result = _carImageDAL.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new List<CarImage> { new CarImage { CarId = id, ImagePath = path, Date = DateTime.Now } };
            }
            return _carImageDAL.GetAll(p => p.CarId == id);
        }

        private IResult CarImageDelete(CarImage carImage)
        {
            try
            {
                File.Delete(carImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }
        private IResult CheckImageLimitForCar(CarImage carImage)
        {

           var result = _carImageDAL.GetAll(p=>p.CarId == carImage.CarId);
            if (result.Count > 5)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
    
}
