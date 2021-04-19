/***************************************************************
* Name        : IFinalUnitOfWork.cs
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
    public interface IFinalUnitOfWork
    {
        Repository<Instrument> Instruments { get; }
        Repository<Brand> Brands { get; }
        Repository<InstrumentBrand> InstrumentBrands { get; }
        Repository<Department> Departments { get; }

        void DeleteCurrentInstrumentBrands(Instrument instrument);
        void AddNewInstrumentBrands(Instrument instrument, int[] brandids);
        void Save();
    }
}
