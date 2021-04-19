/***************************************************************
* Name        : FinalUnitOfWork.cs
* Author      : Tom Sorteberg
* Created     : 04/19/2021
* Course      : CIS 174
* Version     : 1.0
* OS          : Windows 10 Pro, Visual Studio Community 2019
* Copyright   : This is my own original work based on
*               specifications issued by our instructor
* Description : Final Project.
* I have not used unauthorized source code, either modified or 
* unmodified. I have not given other fellow student(s) access 
* to my program.         
***************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class FinalUnitOfWork : IFinalUnitOfWork
    {
        private FinalContext context { get; set; }
        public FinalUnitOfWork(FinalContext ctx) => context = ctx;

        private Repository<Instrument> instrumentData;
        public Repository<Instrument> Instruments
        {
            get
            {
                if (instrumentData == null)
                    instrumentData = new Repository<Instrument>(context);
                return instrumentData;
            }
        }

        private Repository<Brand> brandData;
        public Repository<Brand> Brands
        {
            get
            {
                if (brandData == null)
                    brandData = new Repository<Brand>(context);
                return brandData;
            }
        }

        private Repository<InstrumentBrand> instrumentbrandData;
        public Repository<InstrumentBrand> InstrumentBrands
        {
            get
            {
                if (instrumentbrandData == null)
                    instrumentbrandData = new Repository<InstrumentBrand>(context);
                return instrumentbrandData;
            }
        }

        private Repository<Department> departmentData;
        public Repository<Department> Departments
        {
            get
            {
                if (departmentData == null)
                    departmentData = new Repository<Department>(context);
                return departmentData;
            }
        }

        public void DeleteCurrentInstrumentBrands(Instrument instrument)
        {
            var currentBrands = InstrumentBrands.List(new QueryOptions<InstrumentBrand>
            {
                Where = ib => ib.BrandId == instrument.InstrumentId
            });
            foreach (InstrumentBrand ib in currentBrands)
            {
                InstrumentBrands.Delete(ib);
            }
        }

        public void AddNewInstrumentBrands(Instrument instrument, int[] brandids)
        {
            foreach (int id in brandids)
            {
                InstrumentBrand ib = new InstrumentBrand { InstrumentId = instrument.InstrumentId, BrandId = id };
                InstrumentBrands.Insert(ib);
            }
        }

        public void Save() => context.SaveChanges();
    }
}
