using Buisness.Abstract;
using Buisness.Constrant;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colordal;
        public ColorManager(IColorDal colorDal)
        {
            _colordal = colorDal;
        }

        public void Add(Color color)
        {
            _colordal.Add(color);
        }
        public void Update(Color color)
        {
            _colordal.Update(color);
        }


        public void Delete(Color color)
        {
            _colordal.Delete(color);
        }

        public List<Color> GetAll()
        {
            return _colordal.GetAll();

        }

        public Color GetById(int id)
        {
            return _colordal.Get(c => c.Id == id);
        }

        IDataResult<List<Color>> IColorService.GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colordal.GetAll(),Messages.Listed);
        }
    }
}
