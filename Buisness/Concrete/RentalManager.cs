using Buisness.Abstract;
using Buisness.Constrant;
using Core.Utilities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Buisness.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rental;
        public RentalManager(IRentalDal rentalDal)
        {
            _rental = rentalDal;
        }
        public IResult Add(Rental rental)
        {
            DateTime rentdate = DateTime.Now;

                rental.RentDate = rentdate;
                _rental.Add(rental);
                return new SuccessResult(Messages.Added);
            

        }

        public IResult Delete(Rental rental)
        {
            _rental.Delete(rental);
            return new SuccessResult(Messages.Deleted);
        }

        public IDataResult<List<RentDetailsDto>> GetRentDetail()
        {
            return new SuccessDataResult<List<RentDetailsDto>>(_rental.GetRentDetails(),Messages.Listed);
        }

        public IResult Update(Rental rental)
        {
            _rental.Update(rental);
            return new SuccessResult(Messages.Updated);
        }
    }
}
